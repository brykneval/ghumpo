namespace ghumpo.model.Mobile
{
    public class RestaurantMobile
    {
        public string id { get; set; }
        public string restaurant_name { get; set; }
        public string description { get; set; }
        public string thumbnail { get; set; }
        public string url { get; set; }
        public string email { get; set; }
        public string opening_hours { get; set; }
        public bool has_reservation { get; set; }
        public bool has_card { get; set; }
        public bool has_parking { get; set; }
        public bool has_alcohol { get; set; }
        public bool has_smoking { get; set; }
        public bool has_wifi { get; set; }
        public bool has_floorseating { get; set; }
        public bool has_lounge { get; set; }
        public bool has_indoor { get; set; }
        public bool has_outdoor { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string address { get; set; }
        public string image { get; set; }
        public string contact { get; set; }
        public string cuisine { get; set; }
        public string menu_special { get; set; }
    }
}