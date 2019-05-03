using System;
using System.ComponentModel.DataAnnotations;
using ghumpo.model;

namespace ghumpo.web.ViewModel
{
    public class RestaurantViewModel
    {
        public bool HasReservation { get; set; }
        public bool HasCard { get; set; }
        public bool HasParking { get; set; }
        public bool HasAlcohol { get; set; }
        public bool HasSmoking { get; set; }
        public bool HasWifi { get; set; }
        public bool HasFloorSeating { get; set; }
        public bool HasLounge { get; set; }
        public bool HasIndoor { get; set; }
        public bool HasOutdoor { get; set; }
        public string Contact { get; set; }

        [Display(Name = "Cuisine")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "User Only Letters Or Numbers Please")]
        [MaxLength(100)]
        public string Cuisine { get; set; }

        [Display(Name = "Special Menu")]
        [RegularExpression(@"^[a-zA-Z0-9, ]+$", ErrorMessage = "User Only Letters Or Numbers Please")]
        [MaxLength(100)]
        public string MenuSpecial { get; set; }

        public Guid FkLocalBusinessId { get; set; }
        public GeoCoordinates GeoCoordinates { get; set; }
    }
}