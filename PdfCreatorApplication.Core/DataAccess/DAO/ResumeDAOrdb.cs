using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using PdfCreatorApplication.Core.BusinessLogic.Models;
using PdfCreatorApplication.Core.Utils.ApplicationEnvironment;
using PdfCreatorApplication.Core.Utils.Database;
using PdfCreatorApplication.Core.Utils.Exceptions;

namespace PdfCreatorApplication.Core.DataAccess.DAO
{
    /// <summary>
    /// 
    /// </summary>
    public class ResumeDAOrdb : IResumeDAO
    {   
        /// <summary>
        /// Initializes a new instance of the <see cref="ResumeDAOrdb"/> class.
        /// </summary>
        public ResumeDAOrdb()
        {

        }

        /// <summary>
        /// Gets the resumes.
        /// </summary>
        /// <returns></returns>
        public List<Resume> GetResumes()
        {
            var resumes = new List<Resume>();
            try
            {
                using (var helper = new SqlDbHelper())
                {
                    string getQuery = string.Format(@"SELECT * FROM {0} WHERE [{1}] = 0",
                                                            ApplicationTables.Resume.TableName,
                                                            ApplicationTables.Resume.ColumnIsDeleted);
                    var data = helper.GetDataRowCollection(getQuery);
                    if (data != null && data.Count > 0)
                    {
                        for (int index = 0; index < data.Count; index++)
                        {
                            resumes.Add(GetResume(data[index]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, ex);
                throw new CoreException(ex.Message, ex);
            }

            return resumes;
        }

        /// <summary>
        /// Gets the resume.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        private Resume GetResume(DataRow row)
        {
            var resume = new Resume();
            if (row != null)
            {
                resume.Id = row.Get<long>(ApplicationTables.Resume.ColumnId);
                resume.IsDeleted = row.Get<bool>(ApplicationTables.Resume.ColumnIsDeleted);
                resume.Photo = row.Get<string>(ApplicationTables.Resume.ColumnPhoto);
                resume.MainSkills = row.Get<string>(ApplicationTables.Resume.ColumnMainSkills);
                resume.CandidateName = row.Get<string>(ApplicationTables.Resume.ColumnCandidateName);
                resume.ContactInformation = row.Get<string>(ApplicationTables.Resume.ColumnContactInformation);
                resume.EducationDescription = row.Get<string>(ApplicationTables.Resume.ColumnEducationDescription);
                resume.DesiredJobDescription = row.Get<string>(ApplicationTables.Resume.ColumnDesiredJobDescription);
                resume.ExperienceDescription = row.Get<string>(ApplicationTables.Resume.ColumnExperienceDescription);
                resume.AchievementsDescription = row.Get<string>(ApplicationTables.Resume.ColumnAchievementsDescription);
            }

            return resume;
        }

        /// <summary>
        /// Gets the resume of specified candidate.
        /// </summary>
        /// <param name="candidateId">The candidate identifier.</param>
        /// <returns></returns>
        public Resume GetResume(long candidateId)
        {
            Resume resume = null;
            try
            {
                string getQuery = string.Format(@"SELECT * FROM {0} WHERE [{1}]=@{1} AND [{2}]=0",
                    ApplicationTables.Resume.TableName,
                    ApplicationTables.Resume.ColumnId,
                    ApplicationTables.Resume.ColumnIsDeleted);
                using (var helper = new SqlDbHelper())
                {
                    helper.AddSQLCommandParameter(ApplicationTables.Resume.ColumnId, candidateId);
                    var data = helper.GetDataRowCollection(getQuery);
                    if (data != null && data.Count > 0)
                    {
                        resume = GetResume(data[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, ex);
                throw new CoreException(ex.Message, ex);
            }

            return resume;
        }
    }
}
