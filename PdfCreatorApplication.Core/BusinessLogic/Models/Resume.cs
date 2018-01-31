using PdfCreatorApplication.Core.Utils.Helpers;

namespace PdfCreatorApplication.Core.BusinessLogic.Models
{
    public class Resume
    {
        public long Id { get; set; }

        public bool IsDeleted { get; set; }

        public string CandidateName { get; set; }

        public string CandidateUrl { get; set; }

        public string Photo { get; set; }

        public string ContactInformation { get; set; }

        public string DesiredJobDescription { get; set; }

        public string MainSkills { get; set; }

        public string ExperienceDescription { get; set; }

        public string EducationDescription { get; set; } 
        
        public string AchievementsDescription { get; set; }

        public Resume()
        {

        }
    }
}
