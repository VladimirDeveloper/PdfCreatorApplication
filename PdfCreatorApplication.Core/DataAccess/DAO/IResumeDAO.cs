
using System.Collections.Generic;
using PdfCreatorApplication.Core.BusinessLogic.Models;

namespace PdfCreatorApplication.Core.DataAccess.DAO
{
    public interface IResumeDAO
    {
        /// <summary>
        /// Gets the resumes.
        /// </summary>
        /// <returns></returns>
        List<Resume> GetResumes();

        /// <summary>
        /// Gets the resume of specified candidate.
        /// </summary>
        /// <param name="candidateId">The candidate identifier.</param>
        /// <returns></returns>
        Resume GetResume(long candidateId);
    }
}
