using System.IO;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace PdfCreatorApplication.WebSite.Utils
{
    public class ImageResult : ActionResult
    {
        public string AppBaseDirectory { get; set; }
        public string ImageId { get; set; }

        private const string ImagesFilenamePattern = "{0}Images\\PhotoBank\\{1}.png";

        public ImageResult(string appBaseDirectory, string imageId)
        {
            AppBaseDirectory = appBaseDirectory;
            ImageId = imageId;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var imageName = string.Format(ImagesFilenamePattern, AppBaseDirectory, ImageId);
            var res = context.HttpContext.Response;
            res.Clear();
            res.Cache.SetCacheability(HttpCacheability.NoCache);
            res.ContentType = "image/png";
            res.TransmitFile(imageName);
        }
    }
}