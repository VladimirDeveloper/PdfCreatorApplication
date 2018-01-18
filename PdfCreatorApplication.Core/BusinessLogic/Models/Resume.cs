using PdfCreatorApplication.Core.Utils.Helpers;

namespace PdfCreatorApplication.Core.BusinessLogic.Models
{
    public class Resume
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [is deleted].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is deleted]; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the name of the candidate.
        /// </summary>
        /// <value>
        /// The name of the candidate.
        /// </value>
        public string CandidateName { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        /// <value>
        /// The photo.
        /// </value>
        public string Photo { get; set; }

        /// <summary>
        /// Gets or sets the contact information.
        /// </summary>
        /// <value>
        /// The contact information.
        /// </value>
        public string ContactInformation { get; set; }

        /// <summary>
        /// Gets or sets the desired job description.
        /// </summary>
        /// <value>
        /// The desired job description.
        /// </value>
        public string DesiredJobDescription { get; set; }

        /// <summary>
        /// Gets or sets the main skills.
        /// </summary>
        /// <value>
        /// The main skills.
        /// </value>
        public string MainSkills { get; set; }

        /// <summary>
        /// Gets or sets the experience description.
        /// </summary>
        /// <value>
        /// The experience description.
        /// </value>
        public string ExperienceDescription { get; set; }

        /// <summary>
        /// Gets or sets the education description.
        /// </summary>
        /// <value>
        /// The education description.
        /// </value>
        public string EducationDescription { get; set; }

        /// <summary>
        /// Gets or sets the achievements description.
        /// </summary>
        /// <value>
        /// The achievements description.
        /// </value>
        public string AchievementsDescription { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resume"/> class.
        /// </summary>
        public Resume()
        {

        }
    }
}
