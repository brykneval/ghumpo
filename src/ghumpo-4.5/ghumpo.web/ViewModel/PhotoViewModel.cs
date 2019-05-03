using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ghumpo.web.ViewModel
{
    public class PhotoViewModel
    {
        [Required]
        public IEnumerable<HttpPostedFileBase> ImageFile { get; set; }
    }
}