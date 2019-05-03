using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ghumpo.common;
using ghumpo.model;
using ghumpo.web.Core;
using ghumpo.web.ViewModel;
using Nelibur.ObjectMapper;

namespace ghumpo.web.Controllers
{
    public class LocalBusinessController : Controller
    {
        private const string URI = "api/localbusiness";
        private const string businessGroupURI = "api/businessgroups";
        private readonly IBlobCore _blobCore;
        private readonly IWebApiCore<LocalBusiness, bool> webApiCore;
        private readonly IWebApiCore<BusinessGroupViewModel, bool> webApiGroupCore;

        public LocalBusinessController()
        {
            _blobCore = new BlobCore();
            webApiCore = new WebApiCoreRestSharp<LocalBusiness, bool>(URI);
            webApiGroupCore = new WebApiCoreRestSharp<BusinessGroupViewModel, bool>(businessGroupURI);
        }

        public async Task<ActionResult> Create()
        {
            var localBusinessViewModel = new LocalBusinessViewModel();
            localBusinessViewModel.CurrenciesAccepted = "NPR";
            localBusinessViewModel.PaymentAccepted = "Cash";
            localBusinessViewModel.LocalBusinessTypes =
                WebCommon.ConvertEnumToSelectList((EnumHelper.ELocalBusinessType) 0);
            var list = await webApiGroupCore.GetAsync(true);
            localBusinessViewModel.BusinessGroups = WebCommon.ConvertListToSelectList(list.data);
            return View(localBusinessViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(LocalBusinessViewModel localBusinessViewModel)
        {
            if (ModelState.IsValid)
            {
                var id = Guid.NewGuid();
                var imageName = id + "-image";
                var logoName = id + "-logo";
                var thumbName = id + "-thumb";
                localBusinessViewModel.Image = FullImageFileName(localBusinessViewModel.ImageFile, imageName);
                localBusinessViewModel.Logo = FullImageFileName(localBusinessViewModel.LogoFile, logoName);
                localBusinessViewModel.Thumbnail = FullImageFileName(localBusinessViewModel.ImageFile, thumbName);

                TinyMapper.Bind<LocalBusinessViewModel, LocalBusiness>();
                var localBusiness = TinyMapper.Map<LocalBusiness>(localBusinessViewModel);
                localBusiness.LocalBusinessId = id;
                localBusiness.CreatedBy = id;
                localBusiness.ELocalBusinessType =
                    (EnumHelper.ELocalBusinessType) localBusinessViewModel.ELocalBusinessType;
                try
                {
                    IList<Task> tasks = new List<Task>();
                    tasks.Add(UploadFile(imageName, localBusinessViewModel.ImageFile));
                    tasks.Add(UploadFile(logoName, localBusinessViewModel.LogoFile, true));
                    tasks.Add(UploadFile(thumbName, localBusinessViewModel.ImageFile, true));
                    tasks.Add(webApiCore.PostAsync(localBusiness));
                    await Task.WhenAll(tasks.ToArray());
                    return RedirectToAction("Create", "Restaurant", new {id});
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        private async Task<bool> UploadFile(string fileName, HttpPostedFileBase file, bool isThumb = false)
        {
            if (file != null)
            {
                if (isThumb)
                {
                    await _blobCore.UploadFileAndImage(fileName, file, 210, 210);
                }
                await _blobCore.UploadFileAndImage(fileName, file, 720, 1280);
            }
            return true;
        }

        private string FullImageFileName(HttpPostedFileBase file, string fileName)
        {
            return file == null ? "" : fileName + Path.GetExtension(file.FileName);
        }
    }
}