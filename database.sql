USE [master]
GO
/****** Object:  Database [GroupManager]    Script Date: 02/08/2022 09:15:27 ******/
CREATE DATABASE [GroupManager] ON  PRIMARY 
( NAME = N'GroupManager', FILENAME = N'e:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\GroupManager.mdf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'GroupManager_log', FILENAME = N'e:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\GroupManager.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [GroupManager] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GroupManager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GroupManager] SET ANSI_NULL_DEFAULT ON
GO
ALTER DATABASE [GroupManager] SET ANSI_NULLS ON
GO
ALTER DATABASE [GroupManager] SET ANSI_PADDING ON
GO
ALTER DATABASE [GroupManager] SET ANSI_WARNINGS ON
GO
ALTER DATABASE [GroupManager] SET ARITHABORT ON
GO
ALTER DATABASE [GroupManager] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [GroupManager] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [GroupManager] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [GroupManager] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [GroupManager] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [GroupManager] SET CURSOR_DEFAULT  LOCAL
GO
ALTER DATABASE [GroupManager] SET CONCAT_NULL_YIELDS_NULL ON
GO
ALTER DATABASE [GroupManager] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [GroupManager] SET QUOTED_IDENTIFIER ON
GO
ALTER DATABASE [GroupManager] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [GroupManager] SET  DISABLE_BROKER
GO
ALTER DATABASE [GroupManager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [GroupManager] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [GroupManager] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [GroupManager] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [GroupManager] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [GroupManager] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [GroupManager] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [GroupManager] SET  READ_WRITE
GO
ALTER DATABASE [GroupManager] SET RECOVERY FULL
GO
ALTER DATABASE [GroupManager] SET  MULTI_USER
GO
ALTER DATABASE [GroupManager] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [GroupManager] SET DB_CHAINING OFF
GO
USE [GroupManager]
GO
/****** Object:  Table [dbo].[User]    Script Date: 02/08/2022 09:15:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Gender] [bit] NOT NULL,
	[Date] [date] NULL,
	[Img] [nvarchar](500) NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Stt] [bit] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 02/08/2022 09:15:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] NOT NULL,
	[Title] [nvarchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Role]    Script Date: 02/08/2022 09:15:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [int] NULL,
	[Role_ID] [int] NULL,
 CONSTRAINT [PK_User_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuizCategory]    Script Date: 02/08/2022 09:15:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuizCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Desc] [nvarchar](200) NULL,
	[Title] [nvarchar](200) NULL,
	[Created_Date] [date] NULL,
	[Created_By] [int] NULL,
 CONSTRAINT [PK_QuizCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostComment]    Script Date: 02/08/2022 09:15:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostComment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Post_ID] [int] NULL,
	[Content] [nvarchar](max) NULL,
	[Created_By] [int] NULL,
	[Created_Date] [date] NULL,
	[Modified_Date] [date] NULL,
	[Parrent_ID] [int] NULL,
	[Stt] [bit] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostCategory]    Script Date: 02/08/2022 09:15:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Desc] [nvarchar](200) NULL,
	[Created_Date] [date] NOT NULL,
	[Created_By] [int] NULL,
 CONSTRAINT [PK_PostCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionComment]    Script Date: 02/08/2022 09:15:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionComment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Question_ID] [int] NULL,
	[Content] [nvarchar](max) NULL,
	[Created_By] [int] NULL,
	[Created_Date] [date] NULL,
	[Modified_Date] [date] NULL,
	[Parrent_ID] [int] NULL,
	[Stt] [bit] NULL,
 CONSTRAINT [PK_QuestionComment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionCategory]    Script Date: 02/08/2022 09:15:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Desc] [nvarchar](200) NULL,
	[Title] [nvarchar](200) NULL,
	[Created_Date] [date] NULL,
	[Created_By] [int] NULL,
 CONSTRAINT [PK_QuestionCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LessonComment]    Script Date: 02/08/2022 09:15:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LessonComment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Lesson_ID] [int] NULL,
	[Content] [nvarchar](max) NULL,
	[Created_By] [int] NULL,
	[Created_Date] [date] NULL,
	[Modified_Date] [date] NULL,
	[Parrent_ID] [int] NULL,
	[Stt] [bit] NULL,
 CONSTRAINT [PK_Lesson_Comment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lesson]    Script Date: 02/08/2022 09:15:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lesson](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Group_ID] [int] NULL,
	[Content] [nvarchar](max) NULL,
	[Created_Date] [date] NULL,
	[Created_By] [int] NULL,
	[Stt] [bit] NULL,
 CONSTRAINT [PK_Lesson] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupCategory]    Script Date: 02/08/2022 09:15:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Created_Date] [date] NULL,
	[Created_By] [int] NULL,
 CONSTRAINT [PK_GroupCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 02/08/2022 09:15:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Desc] [nvarchar](500) NULL,
	[Created_Date] [date] NULL,
	[Created_By] [int] NULL,
	[Category_ID] [int] NULL,
	[Stt] [bit] NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 02/08/2022 09:15:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Content] [nvarchar](max) NULL,
	[Created_Date] [date] NULL,
	[Created_By] [int] NULL,
	[Group_ID] [int] NULL,
	[Category_ID] [int] NULL,
	[Stt] [bit] NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Group]    Script Date: 02/08/2022 09:15:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Group](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [int] NULL,
	[Group_ID] [int] NULL,
	[Created_Date] [date] NULL,
 CONSTRAINT [PK_User_Group] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quiz]    Script Date: 02/08/2022 09:15:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quiz](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Content] [nvarchar](max) NULL,
	[Created_Date] [date] NULL,
	[Created_By] [int] NULL,
	[Group_ID] [int] NULL,
	[Category_ID] [int] NULL,
	[Stt] [bit] NULL,
 CONSTRAINT [PK_Quiz] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 02/08/2022 09:15:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Created_Date] [date] NULL,
	[Created_By] [int] NULL,
	[Category_ID] [int] NULL,
	[Group_ID] [int] NULL,
	[Stt] [bit] NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Default [DF_User_Gender]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Gender]  DEFAULT ((1)) FOR [Gender]
GO
/****** Object:  Default [DF_User_Created_Date]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Created_Date]  DEFAULT (getdate()) FOR [Date]
GO
/****** Object:  Default [DF_User_Stt]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Stt]  DEFAULT ((1)) FOR [Stt]
GO
/****** Object:  Default [DF_QuizCategory_Created_Date]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[QuizCategory] ADD  CONSTRAINT [DF_QuizCategory_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
/****** Object:  Default [DF_Comment_Created_Date]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[PostComment] ADD  CONSTRAINT [DF_Comment_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
/****** Object:  Default [DF_Comment_Stt]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[PostComment] ADD  CONSTRAINT [DF_Comment_Stt]  DEFAULT ((1)) FOR [Stt]
GO
/****** Object:  Default [DF_PostCategory_Created_Date]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[PostCategory] ADD  CONSTRAINT [DF_PostCategory_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
/****** Object:  Default [DF_QuestionComment_Created_Date]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[QuestionComment] ADD  CONSTRAINT [DF_QuestionComment_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
/****** Object:  Default [DF_QuestionComment_Stt]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[QuestionComment] ADD  CONSTRAINT [DF_QuestionComment_Stt]  DEFAULT ((1)) FOR [Stt]
GO
/****** Object:  Default [DF_QuestionCategory_Created_Date]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[QuestionCategory] ADD  CONSTRAINT [DF_QuestionCategory_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
/****** Object:  Default [DF_Group_Created_Date]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
/****** Object:  Default [DF_Group_Stt]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_Stt]  DEFAULT ((1)) FOR [Stt]
GO
/****** Object:  Default [DF_Question_Created_Date]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Question] ADD  CONSTRAINT [DF_Question_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
/****** Object:  Default [DF_Question_Stt]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Question] ADD  CONSTRAINT [DF_Question_Stt]  DEFAULT ((1)) FOR [Stt]
GO
/****** Object:  Default [DF_User_Group_Created_Date]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[User_Group] ADD  CONSTRAINT [DF_User_Group_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
/****** Object:  Default [DF_Quiz_Created_Date]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Quiz] ADD  CONSTRAINT [DF_Quiz_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
/****** Object:  Default [DF_Quiz_Stt]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Quiz] ADD  CONSTRAINT [DF_Quiz_Stt]  DEFAULT ((1)) FOR [Stt]
GO
/****** Object:  Default [DF_Post_Created_Date]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Post] ADD  CONSTRAINT [DF_Post_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
/****** Object:  Default [DF_Post_Stt]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Post] ADD  CONSTRAINT [DF_Post_Stt]  DEFAULT ((1)) FOR [Stt]
GO
/****** Object:  ForeignKey [FK_User_Role_Role]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[User_Role]  WITH CHECK ADD  CONSTRAINT [FK_User_Role_Role] FOREIGN KEY([Role_ID])
REFERENCES [dbo].[Role] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User_Role] CHECK CONSTRAINT [FK_User_Role_Role]
GO
/****** Object:  ForeignKey [FK_User_Role_User]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[User_Role]  WITH CHECK ADD  CONSTRAINT [FK_User_Role_User] FOREIGN KEY([User_ID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[User_Role] CHECK CONSTRAINT [FK_User_Role_User]
GO
/****** Object:  ForeignKey [FK_QuizCategory_User]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[QuizCategory]  WITH CHECK ADD  CONSTRAINT [FK_QuizCategory_User] FOREIGN KEY([Created_By])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[QuizCategory] CHECK CONSTRAINT [FK_QuizCategory_User]
GO
/****** Object:  ForeignKey [FK_Comment_User]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[PostComment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_User] FOREIGN KEY([Created_By])
REFERENCES [dbo].[User] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PostComment] CHECK CONSTRAINT [FK_Comment_User]
GO
/****** Object:  ForeignKey [FK_PostCategory_User]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[PostCategory]  WITH CHECK ADD  CONSTRAINT [FK_PostCategory_User] FOREIGN KEY([Created_By])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[PostCategory] CHECK CONSTRAINT [FK_PostCategory_User]
GO
/****** Object:  ForeignKey [FK_QuestionComment_User]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[QuestionComment]  WITH CHECK ADD  CONSTRAINT [FK_QuestionComment_User] FOREIGN KEY([Created_By])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[QuestionComment] CHECK CONSTRAINT [FK_QuestionComment_User]
GO
/****** Object:  ForeignKey [FK_QuestionCategory_User]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[QuestionCategory]  WITH CHECK ADD  CONSTRAINT [FK_QuestionCategory_User] FOREIGN KEY([Created_By])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[QuestionCategory] CHECK CONSTRAINT [FK_QuestionCategory_User]
GO
/****** Object:  ForeignKey [FK_Lesson_Comment_User]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[LessonComment]  WITH CHECK ADD  CONSTRAINT [FK_Lesson_Comment_User] FOREIGN KEY([Created_By])
REFERENCES [dbo].[User] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LessonComment] CHECK CONSTRAINT [FK_Lesson_Comment_User]
GO
/****** Object:  ForeignKey [FK_Lesson_User]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Lesson]  WITH CHECK ADD  CONSTRAINT [FK_Lesson_User] FOREIGN KEY([Created_By])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Lesson] CHECK CONSTRAINT [FK_Lesson_User]
GO
/****** Object:  ForeignKey [FK_GroupCategory_User]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[GroupCategory]  WITH CHECK ADD  CONSTRAINT [FK_GroupCategory_User] FOREIGN KEY([Created_By])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[GroupCategory] CHECK CONSTRAINT [FK_GroupCategory_User]
GO
/****** Object:  ForeignKey [FK_Group_GroupCategory]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_GroupCategory] FOREIGN KEY([Category_ID])
REFERENCES [dbo].[GroupCategory] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_Group_GroupCategory]
GO
/****** Object:  ForeignKey [FK_Group_User]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_User] FOREIGN KEY([Created_By])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_Group_User]
GO
/****** Object:  ForeignKey [FK_Question_QuestionCategory]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_QuestionCategory] FOREIGN KEY([Category_ID])
REFERENCES [dbo].[QuestionCategory] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_QuestionCategory]
GO
/****** Object:  ForeignKey [FK_Question_User]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_User] FOREIGN KEY([Created_By])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_User]
GO
/****** Object:  ForeignKey [FK_Question_User1]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_User1] FOREIGN KEY([Created_By])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_User1]
GO
/****** Object:  ForeignKey [FK_User_Group_Group]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[User_Group]  WITH CHECK ADD  CONSTRAINT [FK_User_Group_Group] FOREIGN KEY([Group_ID])
REFERENCES [dbo].[Group] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User_Group] CHECK CONSTRAINT [FK_User_Group_Group]
GO
/****** Object:  ForeignKey [FK_User_Group_User]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[User_Group]  WITH CHECK ADD  CONSTRAINT [FK_User_Group_User] FOREIGN KEY([User_ID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[User_Group] CHECK CONSTRAINT [FK_User_Group_User]
GO
/****** Object:  ForeignKey [FK_Quiz_Group]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Quiz]  WITH CHECK ADD  CONSTRAINT [FK_Quiz_Group] FOREIGN KEY([Group_ID])
REFERENCES [dbo].[Group] ([ID])
GO
ALTER TABLE [dbo].[Quiz] CHECK CONSTRAINT [FK_Quiz_Group]
GO
/****** Object:  ForeignKey [FK_Quiz_QuizCategory]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Quiz]  WITH CHECK ADD  CONSTRAINT [FK_Quiz_QuizCategory] FOREIGN KEY([Category_ID])
REFERENCES [dbo].[QuizCategory] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Quiz] CHECK CONSTRAINT [FK_Quiz_QuizCategory]
GO
/****** Object:  ForeignKey [FK_Quiz_User]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Quiz]  WITH CHECK ADD  CONSTRAINT [FK_Quiz_User] FOREIGN KEY([Created_By])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Quiz] CHECK CONSTRAINT [FK_Quiz_User]
GO
/****** Object:  ForeignKey [FK_Post_Group]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_Group] FOREIGN KEY([Group_ID])
REFERENCES [dbo].[Group] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_Group]
GO
/****** Object:  ForeignKey [FK_Post_PostCategory]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_PostCategory] FOREIGN KEY([Category_ID])
REFERENCES [dbo].[PostCategory] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_PostCategory]
GO
/****** Object:  ForeignKey [FK_Post_User]    Script Date: 02/08/2022 09:15:28 ******/
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_User] FOREIGN KEY([Category_ID])
REFERENCES [dbo].[User] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_User]
GO
