using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ghumpo.common
{
    public interface IBlobCore
    {
        Task<bool> UploadFileAndImage(string title, HttpPostedFileBase file, short width = -9,
            short height = -9);

        Task<bool> UploadFromBase64(string fileName, string file,
            short dimensionWidth = -9,
            short dimensionHeight = -9, string container = null);
    }

    public sealed class BlobCore : IBlobCore
    {
        private CloudBlobContainer CloudBlobContainer { get; set; }

        public async Task<bool> UploadFromBase64(string fileName, string file,
            short dimensionWidth = -9,
            short dimensionHeight = -9, string container = null)
        {
            var containerName = string.IsNullOrEmpty(container) ? "upload-reference-image" : container;
            InitializeImageContainer(containerName);
            var blockBlob = CloudBlobContainer.GetBlockBlobReference(fileName);

            var jpgInfo = ImageCodecInfo.GetImageEncoders().First(codecInfo => codecInfo.MimeType == "image/jpeg");
            var plainTextBytes = Convert.FromBase64String(file);
            using (var stream = new MemoryStream(plainTextBytes))
            using (var image = Image.FromStream(stream, true, true))
            using (var encParams = new EncoderParameters(1))
            {
                encParams.Param[0] = new EncoderParameter(Encoder.Quality, 90L);
                if (image.Width > dimensionWidth && image.Height > dimensionHeight)
                {
                    using (var bitMapImage = ResizeImage(image, dimensionWidth, dimensionHeight))
                    {
                        using (var streamResized = new MemoryStream())
                        {
                            bitMapImage.Save(streamResized, jpgInfo, encParams);
                            streamResized.Position = 0;
                            await blockBlob.UploadFromStreamAsync(streamResized);
                            blockBlob.Properties.CacheControl = "public, max-age=864000";
                            blockBlob.SetProperties();
                        }
                    }
                }
                else
                {
                    stream.Position = 0;
                    await blockBlob.UploadFromStreamAsync(stream);
                    blockBlob.Properties.CacheControl = "public, max-age=864000";
                    blockBlob.SetProperties();
                }
            }
            return true;
        }


        public async Task<bool> UploadFileAndImage(string title, HttpPostedFileBase file,
            short dimensionWidth = -9,
            short dimensionHeight = -9)
        {
            InitializeImageContainer();
            var blockBlob = CloudBlobContainer.GetBlockBlobReference(title);
            if (HasImageExtension(file.FileName))
            {
                var jpgInfo = ImageCodecInfo.GetImageEncoders().First(codecInfo => codecInfo.MimeType == "image/jpeg");
                using (var image = Image.FromStream(file.InputStream, true, true))
                {
                    using (var stream = new MemoryStream())
                    using (var encParams = new EncoderParameters(1))
                    {
                        encParams.Param[0] = new EncoderParameter(Encoder.Quality, 90L);
                        if (image.Width > dimensionWidth && image.Height > dimensionHeight)
                        {
                            using (var bitMapImage = ResizeImage(image, dimensionWidth, dimensionHeight))
                            {
                                //bitMapImage.Save(stream, jpgInfo, encParams);
                                using (var streamResized = new MemoryStream())
                                {
                                    bitMapImage.Save(streamResized, jpgInfo, encParams);
                                    streamResized.Position = 0;
                                    await blockBlob.UploadFromStreamAsync(streamResized);
                                    blockBlob.Properties.CacheControl = "public, max-age=864000";
                                    blockBlob.SetProperties();
                                }
                            }
                        }
                        else
                        {
                            image.Save(stream, jpgInfo, encParams);
                        }
                        stream.Position = 0;
                        await blockBlob.UploadFromStreamAsync(stream);
                        blockBlob.Properties.CacheControl = "public, max-age=864000";
                        blockBlob.SetProperties();
                    }
                }
            }
            else
            {
                await blockBlob.UploadFromStreamAsync(file.InputStream);
            }
            return true;
        }

        private void InitializeImageContainer(string containerReference = "upload-reference")
        {
            var storageAccount =
                CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(ConfigurationManager.AppSettings[containerReference]);
            blobClient.DefaultRequestOptions.ParallelOperationThreadCount = 64;

            container.CreateIfNotExists();
            var oPermission = new BlobContainerPermissions {PublicAccess = BlobContainerPublicAccessType.Blob};
            container.SetPermissions(oPermission);
            CloudBlobContainer = container;
        }

        private bool HasImageExtension(string source)
        {
            return source.EndsWith(".png") || source.EndsWith(".jpg");
        }

        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var sourceWidth = image.Width;
            var sourceHeight = image.Height;
            var sourceX = 0;
            var sourceY = 0;
            double destX = 0;
            double destY = 0;

            double nScale = 0;
            double nScaleW = 0;
            double nScaleH = 0;
            nScaleW = (width/(double) sourceWidth);
            nScaleH = (height/(double) sourceHeight);
            nScale = Math.Min(nScaleH, nScaleW);
            if (nScale > 1)
                nScale = 1;

            var destWidth = (int) Math.Round(sourceWidth*nScale);
            var destHeight = (int) Math.Round(sourceHeight*nScale);
            Bitmap bmPhoto = null;
            try
            {
                bmPhoto = new Bitmap(destWidth + (int) Math.Round(2*destX), destHeight + (int) Math.Round(2*destY));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                    string.Format("destWidth:{0}, destX:{1}, destHeight:{2}, desxtY:{3}, Width:{4}, Height:{5}",
                        destWidth, destX, destHeight, destY, width, height), ex);
            }

            using (var graphics = Graphics.FromImage(bmPhoto))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                var to = new Rectangle((int) Math.Round(destX), (int) Math.Round(destY), destWidth, destHeight);
                var from = new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);

                if (image.Width != 0 && image.Height != 0)
                    graphics.DrawImage(image, to, from, GraphicsUnit.Pixel);
            }
            return bmPhoto;
        }
    }
}