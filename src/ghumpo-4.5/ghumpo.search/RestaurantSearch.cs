using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public class RestaurantSearch : ILuceneSearchRestaurant<SearchListingMapper, RestaurantListingMobile>
    {
        private const string containerName = "restaurantluceneindex";
        private const int HIT_LIMIT = 1000;
        private readonly StandardAnalyzer analyzer;
        private readonly AzureDirectory azureDirectory;

        public RestaurantSearch(CloudStorageAccount cloudStorageAccount, DirectoryInfo directoryInfo)
        {
            azureDirectory = new AzureDirectory(cloudStorageAccount, containerName, new SimpleFSDirectory(directoryInfo),
                true);
            analyzer = new StandardAnalyzer(Version.LUCENE_30);
        }

        public IList<RestaurantListingMobile> SearchIndex(string searchQuery, string filterCity, string lat, string lon,
            string sort, string type, string offset)
        {
            IList<RestaurantListingMobile> restaurantListings = new List<RestaurantListingMobile>();
            using (var searcher = new IndexSearcher(azureDirectory))
            {
                var parser = new QueryParser(Version.LUCENE_30, "RestaurantAndSpecial", analyzer);
                searchQuery = new JavaScriptSerializer().Deserialize(searchQuery, null).ToString();
                filterCity = new JavaScriptSerializer().Deserialize(filterCity, null).ToString();
                lat = new JavaScriptSerializer().Deserialize(lat, null).ToString();
                lon = new JavaScriptSerializer().Deserialize(lon, null).ToString();
                sort = new JavaScriptSerializer().Deserialize(sort, null).ToString();
                type = new JavaScriptSerializer().Deserialize(type, null).ToString();
                offset = new JavaScriptSerializer().Deserialize(offset, null).ToString();
                if (type == "2")
                {
                    filterCity = "";
                    searchQuery = "";
                    parser.AllowLeadingWildcard = true;
                    sort = "true";
                }
                var hasFilter = !string.IsNullOrEmpty(filterCity);
                Query query;
                if (string.IsNullOrEmpty(searchQuery))
                {
                    if (hasFilter)
                    {
                        parser.AllowLeadingWildcard = true;
                    }
                    query = parser.Parse("*");
                }
                else
                {
                    query = parser.Parse(searchQuery.Trim() + "*");
                }

                var hits = searcher.Search(query, HIT_LIMIT);
                Parallel.ForEach(hits.ScoreDocs, scoreDoc =>
                {
                    var doc = searcher.Doc(scoreDoc.Doc);
                    var locality = doc.GetField("Locality").StringValue;
                    var subLocality = doc.GetField("SubLocality").StringValue;
                    var restaurant = new RestaurantListingMobile
                    {
                        id = doc.GetField("Id").StringValue,
                        restaurant_name = doc.GetField("RestaurantName").StringValue,
                        restaurant_id = doc.GetField("RestaurantId").StringValue,
                        description = doc.GetField("Description").StringValue,
                        thumbnail = doc.GetField("Thumbnail").StringValue,
                        has_reservation = Convert.ToBoolean(doc.GetField("HasReservation").StringValue),
                        has_card = Convert.ToBoolean(doc.GetField("HasCard").StringValue),
                        has_parking = Convert.ToBoolean(doc.GetField("HasParking").StringValue),
                        has_alcohol = Convert.ToBoolean(doc.GetField("HasAlcohol").StringValue),
                        has_smoking = Convert.ToBoolean(doc.GetField("HasSmoking").StringValue),
                        has_wifi = Convert.ToBoolean(doc.GetField("HasWifi").StringValue),
                        longitude = doc.GetField("Longitude").StringValue,
                        latitude = doc.GetField("Latitude").StringValue,
                        address = doc.GetField("Address").StringValue,
                        location = doc.GetField("SubLocality").StringValue,
                        distance = CommonHelper.GetDistance(lat, lon, doc.GetField("Latitude").StringValue,
                            doc.GetField("Longitude").StringValue)
                    };
                    if (hasFilter)
                    {
                        if (locality.Equals(filterCity, StringComparison.CurrentCultureIgnoreCase) ||
                            subLocality.Equals(filterCity, StringComparison.CurrentCultureIgnoreCase))
                        {
                            restaurantListings.Add(restaurant);
                        }
                    }
                    else
                    {
                        restaurantListings.Add(restaurant);
                    }
                });
                if (!string.IsNullOrEmpty(sort))
                {
                    restaurantListings = string.IsNullOrEmpty(offset)
                        ? restaurantListings.OrderBy(x => x.distance).ToList()
                        : restaurantListings.OrderBy(x => x.distance).Take(Convert.ToInt32(offset)).ToList();
                }
            }
            return restaurantListings;
        }

        public IList<RestaurantHomeMobile> SearchRestaurantHomeIndex(string searchQuery)
        {
            IList<RestaurantHomeMobile> restaurantListings = new List<RestaurantHomeMobile>();
            using (var searcher = new IndexSearcher(azureDirectory))
            {
                var parser = new QueryParser(Version.LUCENE_30, "RestaurantAndSpecial", analyzer);
                searchQuery = new JavaScriptSerializer().Deserialize(searchQuery, null).ToString();
                var query = parser.Parse(searchQuery.Trim() + "*");
                var hits = searcher.Search(query, HIT_LIMIT);
                Parallel.ForEach(hits.ScoreDocs, scoreDoc =>
                {
                    var doc = searcher.Doc(scoreDoc.Doc);
                    restaurantListings.Add(new RestaurantHomeMobile
                    {
                        id = doc.GetField("RestaurantId").StringValue,
                        restaurant_name = doc.GetField("RestaurantName").StringValue,
                        thumbnail = doc.GetField("Thumbnail").StringValue
                    });
                });
            }
            restaurantListings = restaurantListings.OrderBy(m => Guid.NewGuid()).Take(20).ToList();
            return restaurantListings;
        }

        public IList<LocationSuggestionMobile> SearchLocationIndex(string searchQuery, string filterCity, string lat,
            string lon,
            string sort, string type, string offset)
        {
            IList<LocationSuggestionMobile> locations = new List<LocationSuggestionMobile>();
            using (var searcher = new IndexSearcher(azureDirectory))
            {
                var parser = new QueryParser(Version.LUCENE_30, "RestaurantAndSpecial", analyzer);
                searchQuery = new JavaScriptSerializer().Deserialize(searchQuery, null).ToString();
                filterCity = new JavaScriptSerializer().Deserialize(filterCity, null).ToString();
                lat = new JavaScriptSerializer().Deserialize(lat, null).ToString();
                lon = new JavaScriptSerializer().Deserialize(lon, null).ToString();
                sort = new JavaScriptSerializer().Deserialize(sort, null).ToString();
                type = new JavaScriptSerializer().Deserialize(type, null).ToString();
                offset = new JavaScriptSerializer().Deserialize(offset, null).ToString();
                if (type == "2")
                {
                    filterCity = "";
                    searchQuery = "";
                    parser.AllowLeadingWildcard = true;
                    sort = "true";
                }
                Query query;
                if (string.IsNullOrEmpty(searchQuery))
                {
                    query = parser.Parse("*");
                }
                else
                {
                    query = parser.Parse(searchQuery.Trim() + "*");
                }
                var hits = searcher.Search(query, HIT_LIMIT);
                Parallel.ForEach(hits.ScoreDocs, scoreDoc =>
                {
                    var doc = searcher.Doc(scoreDoc.Doc);
                    var locality = doc.GetField("SubLocality").StringValue;
                    if (!string.IsNullOrEmpty(locality))
                    {
                        locations.Add(new LocationSuggestionMobile
                        {
                            name = locality
                        });
                    }
                });
            }
            locations = locations.Distinct().ToList();
            return locations;
        }

        public void AddIndex(SearchListingMapper listing)
        {
            using (
                var indexWriter = new IndexWriter(azureDirectory, analyzer, !IndexReader.IndexExists(azureDirectory),
                    IndexWriter.MaxFieldLength.UNLIMITED))
            {
                DeleteRestaurantDocument(listing.SearchListingMapperId.ToString());
                var doc = new Document();

                doc.Add(new Field("Id", listing.SearchListingMapperId.ToString(), Field.Store.YES,
                    Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                doc.Add(new Field("RestaurantName", listing.RestaurantName, Field.Store.YES, Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("RestaurantAndSpecial",
                    SearchWords(listing.RestaurantName, listing.MenuSpecial, listing.AddressRoute,
                        listing.AddressSubLocality, listing.AddressLocality), Field.Store.YES,
                    Field.Index.ANALYZED,
                    Field.TermVector.NO));
                doc.Add(new Field("RestaurantId",
                    listing.FkRestaurantId.ToString(), Field.Store.YES,
                    Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("Address",
                    string.IsNullOrEmpty(listing.Address) ? "" : listing.Address.RemoveNumberAndComma(), Field.Store.YES,
                    Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("Country",
                    string.IsNullOrEmpty(listing.AddressCountry) ? "" : listing.AddressCountry.RemoveNumberAndComma(),
                    Field.Store.YES,
                    Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("Locality",
                    string.IsNullOrEmpty(listing.AddressLocality) ? "" : listing.AddressLocality.RemoveNumberAndComma(),
                    Field.Store.YES,
                    Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("SubLocality",
                    string.IsNullOrEmpty(listing.AddressSubLocality)
                        ? ""
                        : listing.AddressSubLocality.RemoveNumberAndComma(), Field.Store.YES,
                    Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("Route",
                    string.IsNullOrEmpty(listing.AddressRoute) ? "" : listing.AddressRoute.RemoveNumberAndComma(),
                    Field.Store.YES,
                    Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("Description", listing.Description ?? "", Field.Store.YES, Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("Thumbnail", listing.Thumbnail ?? "", Field.Store.YES, Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("HasReservation", listing.HasReservation.ToString(), Field.Store.YES,
                    Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("HasCard", listing.HasCard.ToString(), Field.Store.YES, Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("HasParking", listing.HasParking.ToString(), Field.Store.YES,
                    Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("HasAlcohol", listing.HasAlcohol.ToString(), Field.Store.YES,
                    Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("HasSmoking", listing.HasSmoking.ToString(), Field.Store.YES,
                    Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("HasWifi", listing.HasWifi.ToString(), Field.Store.YES, Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("Longitude", listing.Longitude ?? "", Field.Store.YES, Field.Index.NO,
                    Field.TermVector.NO));
                doc.Add(new Field("Latitude", listing.Latitude ?? "", Field.Store.YES, Field.Index.NO,
                    Field.TermVector.NO));
                indexWriter.AddDocument(doc);

                var reader = IndexReader.Open(azureDirectory, true);
                var num = reader.NumDocs();
                if (num%10 == 0)
                    indexWriter.Optimize();
            }
        }

        public void Dispose()
        {
            if (azureDirectory != null)
                azureDirectory.Dispose();
            if (analyzer != null)
                analyzer.Dispose();
            GC.SuppressFinalize(this);
        }

        public void BuildIndex(IList<SearchListingMapper> listings)
        {
            AddRestaurantListingToDocument(listings);
        }

        public void RebuildIndex(IList<SearchListingMapper> listings)
        {
            ClearLuceneIndex();
            AddRestaurantListingToDocument(listings);
        }

        public void DeleteIndex(string id)
        {
            throw new NotImplementedException();
        }

        private void DeleteRestaurantDocument(string id)
        {
            using (
                var indexWriter = new IndexWriter(azureDirectory, analyzer, false, IndexWriter.MaxFieldLength.UNLIMITED)
                )
            {
                var searchQuery = new TermQuery(new Term("Id", id));
                indexWriter.DeleteDocuments(searchQuery);
            }
        }

        private string SearchWords(string restaurantName, string menuSpecial, string route, string subLocality,
            string locality)
        {
            menuSpecial = string.IsNullOrEmpty(menuSpecial) ? "" : menuSpecial.Replace(",", " ");
            var location = (string.IsNullOrEmpty(route) ? "" : route + " ") +
                           (string.IsNullOrEmpty(subLocality) ? "" : subLocality + " ") +
                           (string.IsNullOrEmpty(locality) ? "" : locality);
            return restaurantName + " " + menuSpecial + " " + location;
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

        private void AddRestaurantListingToDocument(IList<SearchListingMapper> listings)
        {
            using (
                var indexWriter = new IndexWriter(azureDirectory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (var listing in listings)
                {
                    //DeleteRestaurantDocument(listing.SearchListingMapperId.ToString());
                    var doc = new Document();

                    doc.Add(new Field("Id", listing.SearchListingMapperId.ToString(), Field.Store.YES,
                        Field.Index.NOT_ANALYZED, Field.TermVector.NO));
                    doc.Add(new Field("RestaurantName", listing.RestaurantName, Field.Store.YES, Field.Index.NO,
                        Field.TermVector.NO));
                    doc.Add(new Field("RestaurantAndSpecial",
                        SearchWords(listing.RestaurantName, listing.MenuSpecial, listing.AddressRoute,
                            listing.AddressSubLocality, listing.AddressLocality), Field.Store.YES,
                        Field.Index.ANALYZED,
                        Field.TermVector.NO));
                    doc.Add(new Field("RestaurantId",
                        listing.FkRestaurantId.ToString(), Field.Store.YES,
                        Field.Index.NO,
                        Field.TermVector.NO));
                    doc.Add(new Field("Address",
                        string.IsNullOrEmpty(listing.Address) ? "" : listing.Address.RemoveNumberAndComma(),
                        Field.Store.YES,
                        Field.Index.NO,
                        Field.TermVector.NO));
                    doc.Add(new Field("Country",
                        string.IsNullOrEmpty(listing.AddressCountry)
                            ? ""
                            : listing.AddressCountry.RemoveNumberAndComma(),
                        Field.Store.YES,
                        Field.Index.NO,
                        Field.TermVector.NO));
                    doc.Add(new Field("Locality",
                        string.IsNullOrEmpty(listing.AddressLocality)
                            ? ""
                            : listing.AddressLocality.RemoveNumberAndComma(),
                        Field.Store.YES,
                        Field.Index.NO,
                        Field.TermVector.NO));
                    doc.Add(new Field("SubLocality",
                        string.IsNullOrEmpty(listing.AddressSubLocality)
                            ? ""
                            : listing.AddressSubLocality.RemoveNumberAndComma(), Field.Store.YES,
                        Field.Index.NO,
                        Field.TermVector.NO));
                    doc.Add(new Field("Route",
                        string.IsNullOrEmpty(listing.AddressRoute) ? "" : listing.AddressRoute.RemoveNumberAndComma(),
                        Field.Store.YES,
                        Field.Index.NO,
                        Field.TermVector.NO));
                    doc.Add(new Field("Description", listing.Description ?? "", Field.Store.YES, Field.Index.NO,
                        Field.TermVector.NO));
                    doc.Add(new Field("Thumbnail", listing.Thumbnail ?? "", Field.Store.YES, Field.Index.NO,
                        Field.TermVector.NO));
                    doc.Add(new Field("HasReservation", listing.HasReservation.ToString(), Field.Store.YES,
                        Field.Index.NO,
                        Field.TermVector.NO));
                    doc.Add(new Field("HasCard", listing.HasCard.ToString(), Field.Store.YES, Field.Index.NO,
                        Field.TermVector.NO));
                    doc.Add(new Field("HasParking", listing.HasParking.ToString(), Field.Store.YES,
                        Field.Index.NO,
                        Field.TermVector.NO));
                    doc.Add(new Field("HasAlcohol", listing.HasAlcohol.ToString(), Field.Store.YES,
                        Field.Index.NO,
                        Field.TermVector.NO));
                    doc.Add(new Field("HasSmoking", listing.HasSmoking.ToString(), Field.Store.YES,
                        Field.Index.NO,
                        Field.TermVector.NO));
                    doc.Add(new Field("HasWifi", listing.HasWifi.ToString(), Field.Store.YES, Field.Index.NO,
                        Field.TermVector.NO));
                    doc.Add(new Field("Longitude", listing.Longitude ?? "", Field.Store.YES, Field.Index.NO,
                        Field.TermVector.NO));
                    doc.Add(new Field("Latitude", listing.Latitude ?? "", Field.Store.YES, Field.Index.NO,
                        Field.TermVector.NO));
                    indexWriter.AddDocument(doc);
                }
                indexWriter.Optimize();
            }
        }
    }
}