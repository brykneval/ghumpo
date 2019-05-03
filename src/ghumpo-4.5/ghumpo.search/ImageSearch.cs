using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using ghumpo.common;
using ghumpo.model;
using ghumpo.model.Mobile;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Store.Azure;
using Microsoft.WindowsAzure.Storage;
using Version = Lucene.Net.Util.Version;

namespace ghumpo.search
{
    public class ImageSearch : ILuceneSearchImage<ImageListingMapper, ImageListingMobile>
    {
        private const string containerName = "imageluceneindex";
        private const int HIT_LIMIT = 100;
        private readonly StandardAnalyzer analyzer;
        private readonly AzureDirectory azureDirectory;

        public ImageSearch(CloudStorageAccount cloudStorageAccount, DirectoryInfo directoryInfo)
        {
            azureDirectory = new AzureDirectory(cloudStorageAccount, containerName, new SimpleFSDirectory(directoryInfo),
                true);
            analyzer = new StandardAnalyzer(Version.LUCENE_30);
        }

        public IList<ImageListingMobile> SearchIndex(string searchQuery)
        {
            IList<ImageListingMobile> imageListings = new List<ImageListingMobile>();
            using (var searcher = new IndexSearcher(azureDirectory))
            {
                var parser = new QueryParser(Version.LUCENE_30, "Tags", analyzer);
                searchQuery = new JavaScriptSerializer().Deserialize(searchQuery, null).ToString();
                var query = parser.Parse(searchQuery.Trim() + "*");
                var hits = searcher.Search(query, HIT_LIMIT);
                Parallel.ForEach(hits.ScoreDocs, scoreDoc =>
                {
                    var doc = searcher.Doc(scoreDoc.Doc);
                    imageListings.Add(new ImageListingMobile
                    {
                        fk_imageid = new Guid(doc.GetField("FkImageId").StringValue),
                        caption = doc.GetField("Caption").StringValue,
                        image_url = doc.GetField("ImageUrl").StringValue,
                        thumb_url = doc.GetField("ThumbUrl").StringValue,
                        profile_name = doc.GetField("ProfileName").StringValue,
                        restaurant_name = doc.GetField("RestaurantName").StringValue
                    });
                });
            }
            return imageListings;
        }

        public void AddIndex(ImageListingMapper listing)
        {
            using (
                var indexWriter = new IndexWriter(azureDirectory, analyzer, !IndexReader.IndexExists(azureDirectory),
                    IndexWriter.MaxFieldLength.UNLIMITED))
            {
                DeleteImageDocument(listing.ImageListingMapperId.ToString());
                var doc = new Document();

                doc.Add(new Field("ImageUserMapperId", listing.ImageListingMapperId.ToString(), Field.Store.YES,
                    Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                doc.Add(new Field("FkImageId", listing.FkImageId.ToString(), Field.Store.YES, Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("Tags",
                    SearchWords(listing.RestaurantName, listing.MenuSpecial, listing.Address), Field.Store.YES,
                    Field.Index.ANALYZED,
                    Field.TermVector.NO));
                doc.Add(new Field("Caption", listing.Caption ?? "", Field.Store.YES, Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("FkRestaurantId", listing.FkRestaurantId.ToString() ?? "", Field.Store.YES,
                    Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("ImageUrl", listing.ImageUrl ?? "", Field.Store.YES, Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("ThumbUrl", listing.ThumbUrl ?? "", Field.Store.YES,
                    Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("ProfileName", listing.ProfileName ?? "", Field.Store.YES, Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("RestaurantName", listing.RestaurantName ?? "", Field.Store.YES,
                    Field.Index.NO,
                    Field.TermVector.NO));
                indexWriter.AddDocument(doc);
                var reader = IndexReader.Open(azureDirectory, true);
                var num = reader.NumDocs();
                if (num%10 == 0)
                    indexWriter.Optimize();
            }
        }

        public void RebuildIndex(IList<ImageListingMapper> listings)
        {
            ClearLuceneIndex();
            AddImageListingToDocument(listings, false);
        }

        public void BuildIndex(IList<ImageListingMapper> listings)
        {
            AddImageListingToDocument(listings, true);
        }

        public void Dispose()
        {
            if (azureDirectory != null)
                azureDirectory.Dispose();
            if (analyzer != null)
                analyzer.Dispose();
            GC.SuppressFinalize(this);
        }

        public void DeleteIndex(string id)
        {
            DeleteImageDocument(id);
        }

        private void DeleteImageDocument(string id)
        {
            using (
                var indexWriter = new IndexWriter(azureDirectory, analyzer, false, IndexWriter.MaxFieldLength.UNLIMITED)
                )
            {
                var searchQuery = new TermQuery(new Term("ImageUserMapperId", id));
                indexWriter.DeleteDocuments(searchQuery);
                indexWriter.Commit();
            }
        }

        private string SearchWords(string restaurantName, string menuSpecial, string address)
        {
            menuSpecial = string.IsNullOrEmpty(menuSpecial) ? "" : menuSpecial.Replace(",", " ");
            address = string.IsNullOrEmpty(address) ? "" : address.RemoveNumberAndComma();
            return restaurantName + " " + menuSpecial + " " + address;
        }

        private bool ClearLuceneIndex()
        {
            try
            {
                using (
                    var writer = new IndexWriter(azureDirectory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    writer.DeleteAll();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private void AddImageListingToDocument(IList<ImageListingMapper> listings, bool isBuild)
        {
            using (
                var indexWriter = new IndexWriter(azureDirectory, analyzer,
                    isBuild ? !IndexReader.IndexExists(azureDirectory) : true, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (var listing in listings)
                {
                    if (listing.EStatus == EnumHelper.EStatus.New)
                    {
                        DeleteImageDocument(listing.ImageListingMapperId.ToString());
                        var doc = new Document();

                        doc.Add(new Field("ImageUserMapperId", listing.ImageListingMapperId.ToString(), Field.Store.YES,
                            Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                        doc.Add(new Field("FkImageId", listing.FkImageId.ToString(), Field.Store.YES, Field.Index.NO,
                            Field.TermVector.NO));
                        doc.Add(new Field("Tags", listing.Tags, Field.Store.YES, Field.Index.ANALYZED,
                            Field.TermVector.NO));
                        doc.Add(new Field("Caption", listing.Caption ?? "", Field.Store.YES, Field.Index.NO,
                            Field.TermVector.NO));
                        doc.Add(new Field("FkRestaurantId", listing.FkRestaurantId.ToString() ?? "", Field.Store.YES,
                            Field.Index.NO,
                            Field.TermVector.NO));
                        doc.Add(new Field("ImageUrl", listing.ImageUrl ?? "", Field.Store.YES, Field.Index.NO,
                            Field.TermVector.NO));
                        doc.Add(new Field("ThumbUrl", listing.ThumbUrl ?? "", Field.Store.YES,
                            Field.Index.NO,
                            Field.TermVector.NO));
                        doc.Add(new Field("ProfileName", listing.ProfileName ?? "", Field.Store.YES, Field.Index.NO,
                            Field.TermVector.NO));
                        doc.Add(new Field("RestaurantName", listing.RestaurantName ?? "", Field.Store.YES,
                            Field.Index.NO,
                            Field.TermVector.NO));
                        indexWriter.AddDocument(doc);
                    }
                }
                indexWriter.Optimize();
            }
        }
    }
}