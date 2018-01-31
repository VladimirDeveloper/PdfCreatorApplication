using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace PdfCreatorApplication.WebSite.Utils.Handlers
{
    public class ImageHandler : ActionResult
    {
        private const string ImagesFilenamePattern = "{0}Images\\PhotoBank\\{1}.png";

        public override void ExecuteResult(ControllerContext context)
        {
            var photoId = context.HttpContext.Request["photo"];
            context.HttpContext.Response.ContentType = "image/png";
            var imageName = string.Format(ImagesFilenamePattern, AppDomain.CurrentDomain.BaseDirectory, photoId);
            context.HttpContext.Response.WriteFile(imageName);
        }
    }
}