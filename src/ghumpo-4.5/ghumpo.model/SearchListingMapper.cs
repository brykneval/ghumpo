using System;
using ghumpo.common;

namespace ghumpo.model
{
    public class SearchListingMapper : Common
    {
        public Guid SearchListingMapperId { get; set; }
        public string RestaurantName { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public string MenuSpecial { get; set; }
        public bool HasReservation { get; set; }
        public bool HasCard { get; set; }
        public bool HasParking { get; set; }
        public bool HasAlcohol { get; set; }
        public bool HasSmoking { get; set; }
        public bool HasWifi { get; set; }
        public string Address { get; set; }
        public string AddressRoute { get; set; }
        public string AddressSubLocality { get; set; }
        public string AddressLocality { get; set; }
        public string AddressCountry { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public EnumHelper.EStatus EStatus { get; set; }
        public Guid FkRestaurantId { get; set; }
        public Guid FkLocalBusinessId { get; set; }
    }
}