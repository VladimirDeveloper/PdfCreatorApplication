using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PdfCreatorApplication.WebSite.Controllers
{
    public class ErrorController : BaseController
    {
        /// <summary>
        /// Default page of Error section.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            HandleErrorInfo errorInfo = null;
            string errorMessage = "";

	    // Analyzes model
            if (ViewData != null && ViewData.Model != null)
            {
                errorInfo = (HandleErrorInfo)ViewData.Model;
                errorMessage = (errorInfo != null ? errorInfo.Exception.ToString() : "Impossible to get error description...");
            }

            if (HttpContext.Session != null && errorInfo != null)
            {
                Debug.WriteLine(errorInfo.Exception.Message + " " + errorInfo.Exception);
            }

            if (HttpContext.Request != null &&
                HttpContext.Request.Headers != null &&
                HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                object jsonData = new
                {
                    error = true,
                    message = errorMessage
                };

                return new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = jsonData
                };
            }
                
            return View();
        }

        /// <summary>
        /// Resource not found.
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            string errorMessage = "";
            HandleErrorInfo errorInfo = null;
            if (ViewData != null && ViewData.Model != null)
            {
                errorInfo = (HandleErrorInfo)ViewData.Model;
                errorMessage = (errorInfo != null ? errorInfo.Exception.ToString() : "Impossible to get error description...");
            }

            if (HttpContext.Session != null && errorInfo != null)
            {
                Debug.WriteLine(errorInfo.Exception.Message + " " + errorInfo.Exception);
            }

            if (HttpContext.Request != null &&
                HttpContext.Request.Headers != null &&
                HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                object jsonData = new
                {
                    error = true,
                    message = errorMessage
                };

                return new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = jsonData
                };
            }
            else
            {
                return View();
            }
        }
    }
}
