namespace ghumpo.model.Mobile
{
    public class ImageRatingPostMobile : Common
    {
        public string fk_imageid { get; set; }
        public string rating_value { get; set; }
        public string comment { get; set; }
        public string created_by { get; set; }
        public string is_update { get; set; }
    }
}