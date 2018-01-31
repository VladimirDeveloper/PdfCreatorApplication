using System.Web.Mvc;
using System.Web.Routing;
using PdfCreatorApplication.Core.BusinessLogic.Export;
using PdfCreatorApplication.Core.Utils.Helpers;
using PdfCreatorApplication.WebSite.ViewModels;
using PdfCreatorApplication.WebSite.ViewModels.ResumeViewModels;

namespace PdfCreatorApplication.WebSite.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = new ResumeListViewModel();
            model.Initialize();
            if (model.Message == null)
            {
                model.Message = TempData["Error"] as MessageModel;
                TempData["Error"] = null;
            }

            return View(model);
        }

        /// <summary>
        /// Exports to document of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="candidateId">The identifier of candidate.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(FileType type, long candidateId)
        {

            var model = new ResumeViewModel(candidateId);
            model.Initialize();
            var protocol = Request.Url != null ? Request.Url.Scheme : "http";
            model.Resume.CandidateUrl = Url.Action("ShowResume", "Home", new { candidateId }, protocol);

            model.ResumeBody = ViewToString("../Templates/PDF/UsualResume", model);

            byte[] fileBytes = model.Export(type);
            if (!model.HasError)
            {
                return File(fileBytes, "application/pdf", string.Format("CV-{0}.pdf",
                            string.IsNullOrEmpty(model.Resume.CandidateName) ? 
                            candidateId.ToString("") : 
                            model.Resume.CandidateName.ToLatin()));
            }

            TempData["Error"] = model.Message;

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Shows the resume of specified candidate.
        /// </summary>
        /// <param name="candidateId">The candidate identifier.</param>
        /// <returns></returns>
        public string ShowResume(long candidateId)
        {
            var model = new ResumeViewModel(candidateId);
            model.Initialize();
            var protocol = Request.Url != null ? Request.Url.Scheme : "http";
            model.Resume.CandidateUrl = Url.Action("ShowResume", "Home", new { candidateId }, protocol);

            return ViewToString("../Templates/PDF/UsualResume", model);
        }


        public void Search()
        {
            SearchEngine.Search();
        }
        
    }
}
