using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ghumpo.model.Mobile;
using ghumpo.web.Core;
using ghumpo.web.ViewModel;

namespace ghumpo.web.Controllers
{
    public class PhotoController : Controller
    {
        private const string URI = "api/images";
        private readonly IWebApiCore<ImagePostMobile, bool> webApiCore;

        public PhotoController()
        {
            webApiCore = new WebApiCoreRestSharp<ImagePostMobile, bool>(URI);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(Guid id)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Guid id, PhotoViewModel photoViewModel)
        {
            try
            {
                foreach (var file in photoViewModel.ImageFile)
                {
                    var oImage = new ImagePostMobile();
                    oImage.fk_restaurantid = Convert.ToString(id);
                    oImage.type = "2";
                    oImage.image_byte = Convert.ToBase64String(ConvertToByte(file));
                    await webApiCore.PostAsync(oImage);
                }
                return RedirectToAction("Create", "LocalBusiness");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        private byte[] ConvertToByte(HttpPostedFileBase file)
        {
            using (var target = new MemoryStream())
            {
                file.InputStream.CopyTo(target);
                var data = target.ToArray();
                return data;
            }
        }
    }
}