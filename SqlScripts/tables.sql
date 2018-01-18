USE [NorthwindModel]
GO

ALTER TABLE [dbo].[Resume] DROP CONSTRAINT [DF_Resume_IsDeleted]
GO

/****** Object:  Table [dbo].[Resume]    Script Date: 16.01.2018 16:55:55 ******/
DROP TABLE [dbo].[Resume]
GO

/****** Object:  Table [dbo].[Resume]    Script Date: 16.01.2018 16:55:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Resume](
	[Id] [bigint] NOT NULL,
	[CandidateName] [nvarchar](50) NOT NULL,
	[Photo] [nvarchar](50) NULL,
	[ContactInformation] [nvarchar](4000) NOT NULL,
	[DesiredJobDescription] [nvarchar](4000) NOT NULL,
	[MainSkills] [nvarchar](4000) NOT NULL,
	[ExperienceDescription] [nvarchar](4000) NOT NULL,
	[EducationDescription] [nvarchar](4000) NOT NULL,
	[AchievementsDescription] [nvarchar](4000) NULL,
	[IsDeleted] [bit] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Resume] ADD  CONSTRAINT [DF_Resume_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO


