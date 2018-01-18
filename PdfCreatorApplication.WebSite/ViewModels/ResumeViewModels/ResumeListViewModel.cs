using System;
using System.Collections.Generic;
using PdfCreatorApplication.Core.BusinessLogic.Export;
using PdfCreatorApplication.Core.BusinessLogic.Models;
using PdfCreatorApplication.Core.DataAccess;
using PdfCreatorApplication.Core.Utils.ApplicationEnvironment;

namespace PdfCreatorApplication.WebSite.ViewModels.ResumeViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class ResumeListViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets the resumes.
        /// </summary>
        /// <value>
        /// The resumes.
        /// </value>
        public List<Resume> Resumes { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResumeListViewModel"/> class.
        /// </summary>
        public ResumeListViewModel()
        {
            Resumes = new List<Resume>();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            try
            {
                Resumes = DAOFactory.GetResumeDAOrdb().GetResumes();
            }
            catch (Exception ex)
            {
                HasError = true;
                Message = new MessageModel(MessageType.Error,
                    ApplicationConstants.Error,
                    "Возникли проблемы при загрузке списка резюме.",
                    ex.Message);
            }
        }

    }
}