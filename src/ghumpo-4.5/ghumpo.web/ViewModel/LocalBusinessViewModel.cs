using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ghumpo.web.ViewModel
{
    public class LocalBusinessViewModel
    {
        [Display(Name = "Restaurant Name")]
        [RegularExpression(@"^[a-zA-Z0-9'& ]+$", ErrorMessage = "User Only Letters Or Numbers Please")]
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [MaxLength(200)]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Image")]
        [MaxLength(100)]
        public string Image { get; set; }

        [Required]
        public HttpPostedFileBase ImageFile { get; set; }

        public string Thumbnail { get; set; }

        [Display(Name = "Website")]
        [MaxLength(150)]
        public string Url { get; set; }

        [Display(Name = "Email")]
        [MaxLength(150)]
        public string Email { get; set; }

        [Display(Name = "Logo")]
        [MaxLength(100)]
        public string Logo { get; set; }

        public HttpPostedFileBase LogoFile { get; set; }

        [Display(Name = "Currencies Accepted")]
        [MaxLength(100)]
        public string CurrenciesAccepted { get; set; }

        [Display(Name = "Payment Accepted")]
        [MaxLength(100)]
        public string PaymentAccepted { get; set; }

        [Display(Name = "Opening Hours")]
        [MaxLength(100)]
        public string OpeningHours { get; set; }

        [Display(Name = "Business Type")]
        public SelectList LocalBusinessTypes { get; set; }

        public int ELocalBusinessType { get; set; }

        [Display(Name = "Parent Groups")]
        public SelectList BusinessGroups { get; set; }

        public long FkBusinessGroupId { get; set; }
    }
}