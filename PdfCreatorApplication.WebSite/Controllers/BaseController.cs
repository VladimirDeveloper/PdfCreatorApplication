using System.IO;
using System.Web.Mvc;

namespace PdfCreatorApplication.WebSite.Controllers
{
    public class BaseController : Controller
    {
        public string ViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var writer = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, writer);
                viewResult.View.Render(viewContext, writer);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);

                return writer.GetStringBuilder().ToString();
            }
        }

    }
}
