namespace ghumpo.model.Mobile
{
    public class RestaurantListingMobile
    {
        public string id { get; set; }
        public string restaurant_id { get; set; }
        public string restaurant_name { get; set; }
        public string description { get; set; }
        public string thumbnail { get; set; }
        public bool has_reservation { get; set; }
        public bool has_card { get; set; }
        public bool has_parking { get; set; }
        public bool has_alcohol { get; set; }
        public bool has_smoking { get; set; }
        public bool has_wifi { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string address { get; set; }
        public double distance { get; set; }
        public string location { get; set; }
    }
}