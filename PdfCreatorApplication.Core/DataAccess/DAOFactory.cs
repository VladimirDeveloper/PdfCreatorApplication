using PdfCreatorApplication.Core.DataAccess.DAO;

namespace PdfCreatorApplication.Core.DataAccess
{
    public static class DAOFactory
    {
        public static IResumeDAO GetResumeDAOrdb()
        {
            return new ResumeDAOrdb();
        }
    }
}
