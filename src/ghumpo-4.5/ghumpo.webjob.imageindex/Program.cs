using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ghumpo.common;
using ghumpo.model;
using ghumpo.search;

namespace ghumpo.webjob.imageindex
{
    internal class Program
    {
        private static void Main()
        {
            var images = GetImages();
            var status = WriteIndex(images);
            var statusDelete = DeleteIndex(images);
            UpdateDb(status, statusDelete);
        }

        private static IList<ImageListingMapper> GetImages()
        {
            IDbConnection con =
                new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var images = con.Query<ImageListingMapper>("spGetImageIndex", null,
                commandType: CommandType.StoredProcedure);
            return images.ToList();
        }

        private static bool WriteIndex(IList<ImageListingMapper> images)
        {
            var imageSearch = LuceneSearch.ImageSearch();
            if (images.Count != 0)
                imageSearch.BuildIndex(images);
            return true;
        }

        private static void UpdateDb(bool status, bool statusDelete)
        {
            IDbConnection con =
                new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var query = @"UPDATE ImageListingMapper SET EStatus = " + (status ? 3 : 4) + " WHERE EStatus = 2 " +
                        "UPDATE ImageListingMapper SET EStatus = " + (statusDelete ? 7 : 8) + " WHERE EStatus = 6";
            var images =
                con.Execute(query, null,
                    commandType: CommandType.Text);
        }

        private static bool DeleteIndex(IList<ImageListingMapper> images)
        {
            var imageSearch = LuceneSearch.ImageSearch();
            foreach (var item in images)
            {
                if (item.EStatus == EnumHelper.EStatus.Delete)
                {
                    imageSearch.DeleteIndex(item.ImageListingMapperId.ToString());
                }
            }
            return true;
        }
    }
}