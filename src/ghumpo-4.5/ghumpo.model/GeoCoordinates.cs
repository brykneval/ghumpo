using System;
using ghumpo.common;

namespace ghumpo.model
{
    public class GeoCoordinates : Common
    {
        public GeoCoordinates()
        {
            GeoCoordinatesId = Guid.NewGuid();
        }

        public Guid GeoCoordinatesId { get; set; }
        public string Address { get; set; }
        public string AddressByGoogle { get; set; }
        public string AddressByGoogleRoute { get; set; }
        public string AddressByGoogleSubLocality { get; set; }
        public string AddressByGoogleLocality { get; set; }
        public string AddressByGoogleCountry { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Elevation { get; set; }
        public string PostalCode { get; set; }
        public EnumHelper.ECountry ECountry { get; set; }
    }
}