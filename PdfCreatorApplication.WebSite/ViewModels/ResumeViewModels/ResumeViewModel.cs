using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PdfCreatorApplication.Core.BusinessLogic.Export;
using PdfCreatorApplication.Core.BusinessLogic.Export.Exporters;
using PdfCreatorApplication.Core.BusinessLogic.Models;
using PdfCreatorApplication.Core.DataAccess;
using PdfCreatorApplication.Core.Utils.ApplicationEnvironment;
using PdfCreatorApplication.WebSite.Controllers;

namespace PdfCreatorApplication.WebSite.ViewModels.ResumeViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class ResumeViewModel : BaseViewModel
    {
        /// <summary>
        /// The identifier of candidate 
        /// </summary>
        private readonly long _candidateId;

        /// <summary>
        /// Gets the resume.
        /// </summary>
        /// <value>
        /// The resume.
        /// </value>
        public Resume Resume { get; private set; }

        /// <summary>
        /// Gets or sets the resume body.
        /// </summary>
        /// <value>
        /// The resume body.
        /// </value>
        public string ResumeBody { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResumeViewModel"/> class.
        /// </summary>
        /// <param name="candidateId">The candidate identifier.</param>
        public ResumeViewModel(long candidateId)
        {
            _candidateId = candidateId;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            try
            {
                Resume = DAOFactory.GetResumeDAOrdb().GetResume(_candidateId);
            }
            catch (Exception ex)
            {
                HasError = true;
                Message = new MessageModel(MessageType.Error,
                    ApplicationConstants.Error,
                    "Возникли проблемы при получении резюме кандидата.",
                    ex.Message);
            }
        }

        /// <summary>
        /// Exports the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public byte[] Export(FileType type)
        {
            try
            {
                var pdfExporter = (PdfExporter) ExporterFactory.GetExporter(type);
                pdfExporter.DocumentBody = ResumeBody;

                return pdfExporter.Export();
            }
            catch (Exception ex)
            {
                HasError = true;
                Message = new MessageModel(MessageType.Error,
                    ApplicationConstants.Error,
                    "Возникли проблемы при получении резюме в виде PDF-файла.",
                    ex.Message);
            }

            return null;
        }
    }
}