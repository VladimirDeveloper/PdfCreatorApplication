using PdfCreatorApplication.Core.DataAccess.DAO;

namespace PdfCreatorApplication.Core.DataAccess
{
    public static class DAOFactory
    {
        /// <summary>
        /// Gets the resume DAO implemnentor.
        /// </summary>
        /// <returns></returns>
        public static IResumeDAO GetResumeDAOrdb()
        {
            return new ResumeDAOrdb();
        }
    }
}
