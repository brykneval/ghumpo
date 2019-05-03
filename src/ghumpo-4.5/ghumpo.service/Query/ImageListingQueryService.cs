using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Script.Serialization;
using Dapper;
using ghumpo.model;
using ghumpo.model.Mobile;
using ghumpo.search;

namespace ghumpo.service.Query
{
    public interface IImageListingQueryService
    {
        IList<ImageListingMobile> Images(string query, int isHome);
        ImagePointGetMobile GetImagePoint(string createdBy, string imageId);
    }

    public class ImageListingQueryService : IImageListingQueryService
    {
        private readonly IDbConnection cnn;
        private readonly ILuceneSearchImage<ImageListingMapper, ImageListingMobile> search;

        public ImageListingQueryService(IDbConnection cnn)
        {
            search = LuceneSearch.ImageSearch();
            this.cnn = cnn;
        }

        public IList<ImageListingMobile> Images(string query, int type)
        {
            IList<ImageListingMobile> images = new List<ImageListingMobile>();
            switch (type)
            {
                case 1:
                    images = IndexSearch(query);
                    break;
                case 2:
                    images = IndividualSearch(query);
                    break;
                case 3:
                    images = RestaurantImageSearch(query);
                    break;
                case 4:
                    images = RestaurantOverviewImageSearch(query);
                    break;
            }
            return images;
        }

        public ImagePointGetMobile GetImagePoint(string createdBy, string imageId)
        {
            createdBy = new JavaScriptSerializer().Deserialize(createdBy, null).ToString();
            imageId = new JavaScriptSerializer().Deserialize(imageId, null).ToString();
            var oImagePointGetMobile = cnn.Query<ImagePointGetMobile>("spGetImagePoint",
                new {imageId, createdBy}, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return oImagePointGetMobile;
        }

        public IList<ImageListingMobile> RestaurantOverviewImageSearch(string restaurantId)
        {
            if (!string.IsNullOrEmpty(restaurantId))
            {
                restaurantId = new JavaScriptSerializer().Deserialize(restaurantId, null).ToString();
            }
            var images = cnn.Query<ImageListingMobile>("spGetRestaurantOverviewImages", new {restaurantId},
                commandType: CommandType.StoredProcedure).ToList();
            return images;
        }

        private IList<ImageListingMobile> IndexSearch(string query)
        {
            var images = search.SearchIndex(query);
            return images;
        }

        private IList<ImageListingMobile> RestaurantImageSearch(string restaurantId)
        {
            if (!string.IsNullOrEmpty(restaurantId))
            {
                restaurantId = new JavaScriptSerializer().Deserialize(restaurantId, null).ToString();
            }
            var images = cnn.Query<ImageListingMobile>("spGetRestaurantImages", new {restaurantId},
                commandType: CommandType.StoredProcedure).ToList();
            return images;
        }

        private IList<ImageListingMobile> IndividualSearch(string createdBy)
        {
            if (!string.IsNullOrEmpty(createdBy))
            {
                createdBy = new JavaScriptSerializer().Deserialize(createdBy, null).ToString();
            }
            var images = cnn.Query<ImageListingMobile>("spGetImages", new {createdBy},
                commandType: CommandType.StoredProcedure).ToList();
            return images;
        }
    }
}