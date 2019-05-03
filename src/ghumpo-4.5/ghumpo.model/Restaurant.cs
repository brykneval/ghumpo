using System;

namespace ghumpo.model
{
    public class Restaurant : Common
    {
        public Guid RestaurantId { get; set; }
        public bool HasReservation { get; set; }
        public bool HasCard { get; set; }
        public bool HasParking { get; set; }
        public bool HasAlcohol { get; set; }
        public bool HasSmoking { get; set; }
        public bool HasWifi { get; set; }
        public string Cuisine { get; set; }
        public string MenuSpecial { get; set; }
        public string Contact { get; set; }

        public bool HasFloorSeating { get; set; }
        public bool HasLounge { get; set; }
        public bool HasIndoor { get; set; }
        public bool HasOutdoor { get; set; }

        public Guid FkLocalBusinessId { get; set; }
        public Guid? FkGeoCoordinatesId { get; set; }
        public GeoCoordinates GeoCoordinates { get; set; }
    }
}