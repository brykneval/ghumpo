using System;
using System.Configuration;
using System.IO;
using ghumpo.model;
using ghumpo.model.Mobile;
using Microsoft.WindowsAzure.Storage;

namespace ghumpo.search
{
    public static class LuceneSearch
    {
        private static readonly CloudStorageAccount storageAccount =
            CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

        public static ILuceneSearchRestaurant<SearchListingMapper, RestaurantListingMobile> RestaurantSearch(
            DirectoryInfo info = null)
        {
            return new RestaurantSearch(storageAccount,
                info ??
                new DirectoryInfo(Environment.GetEnvironmentVariable("HOME") + "\\site\\wwwroot\\restaurantindex"));
        }

        public static ILuceneSearchImage<ImageListingMapper, ImageListingMobile> ImageSearch(DirectoryInfo info = null)
        {
            return new ImageSearch(storageAccount,
                info ?? new DirectoryInfo(Environment.GetEnvironmentVariable("HOME") + "\\site\\wwwroot\\imageindex"));
        }
    }
}