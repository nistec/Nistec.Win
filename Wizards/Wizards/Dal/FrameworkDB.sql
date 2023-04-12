/****** Object:  Table [dbo].[Alert_Config]    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Alert_Config]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Alert_Config]
GO

/****** Object:  Table [dbo].[Alert_ExceptionType]    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Alert_ExceptionType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Alert_ExceptionType]
GO

/****** Object:  Table [dbo].[Alert_Exceptions]    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Alert_Exceptions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Alert_Exceptions]
GO

/****** Object:  Table [dbo].[Alert_Priority]    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Alert_Priority]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Alert_Priority]
GO

/****** Object:  Table [dbo].[Alert_Site]    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Alert_Site]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Alert_Site]
GO

/****** Object:  Table [dbo].[Alert_Users_Exception]    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Alert_Users_Exception]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Alert_Users_Exception]
GO

/****** Object:  Table [dbo].[App_Config]    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[App_Config]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[App_Config]
GO

/****** Object:  Table [dbo].[App_Counters]    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[App_Counters]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[App_Counters]
GO

/****** Object:  Table [dbo].[App_Enum]    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[App_Enum]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[App_Enum]
GO

/****** Object:  Table [dbo].[App_Summarize]    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[App_Summarize]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[App_Summarize]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Users]
GO

/****** Object:  Table [dbo].[Users_Group]    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Users_Group]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Users_Group]
GO

/****** Object:  Table [dbo].[Users_Lvl]    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Users_Lvl]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Users_Lvl]
GO

/****** Object:  Table [dbo].[Users_Permissions]    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Users_Permissions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Users_Permissions]
GO

/****** Object:  Table [dbo].[Users_UIObjects]    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Users_UIObjects]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Users_UIObjects]
GO

/****** Object:  Table [dbo].[Users_UISystem]    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Users_UISystem]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Users_UISystem]
GO

/****** Object:  Table [dbo].[Users_UITypes]    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Users_UITypes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Users_UITypes]
GO

/****** Object:  Table [dbo].[Alert_Config]    Script Date: 23/09/2007 15:58:48 ******/
CREATE TABLE [dbo].[Alert_Config] (
	[ConfigId] [int] NOT NULL ,
	[ConfigKey] [varchar] (50) COLLATE Hebrew_CI_AS NOT NULL ,
	[ConfigValue] [nvarchar] (1000) COLLATE Hebrew_CI_AS NULL ,
	[ConfigSection] [varchar] (50) COLLATE Hebrew_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Alert_ExceptionType]    Script Date: 23/09/2007 15:58:49 ******/
CREATE TABLE [dbo].[Alert_ExceptionType] (
	[ExceptionType] [int] NOT NULL ,
	[ExceptionName] [varchar] (50) COLLATE Hebrew_CI_AS NOT NULL ,
	[AlertLevel] [tinyint] NOT NULL ,
	[Priority] [tinyint] NOT NULL ,
	[siteId] [int] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Alert_Exceptions]    Script Date: 23/09/2007 15:58:49 ******/
CREATE TABLE [dbo].[Alert_Exceptions] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[CreationDate] [datetime] NOT NULL ,
	[Namespace] [varchar] (50) COLLATE Hebrew_CI_AS NULL ,
	[ExceptionText] [varchar] (4000) COLLATE Hebrew_CI_AS NULL ,
	[Priority] [tinyint] NOT NULL ,
	[Method] [varchar] (50) COLLATE Hebrew_CI_AS NULL ,
	[OrderId]  uniqueidentifier ROWGUIDCOL  NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Alert_Priority]    Script Date: 23/09/2007 15:58:49 ******/
CREATE TABLE [dbo].[Alert_Priority] (
	[Priority] [tinyint] NOT NULL ,
	[PriorityName] [varchar] (50) COLLATE Hebrew_CI_AS NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Alert_Site]    Script Date: 23/09/2007 15:58:49 ******/
CREATE TABLE [dbo].[Alert_Site] (
	[SiteId] [int] NOT NULL ,
	[Url] [varchar] (255) COLLATE Hebrew_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Alert_Users_Exception]    Script Date: 23/09/2007 15:58:49 ******/
CREATE TABLE [dbo].[Alert_Users_Exception] (
	[UserId] [int] NOT NULL ,
	[ExceptionId] [int] NOT NULL ,
	[AlertType] [tinyint] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[App_Config]    Script Date: 23/09/2007 15:58:49 ******/
CREATE TABLE [dbo].[App_Config] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[ConfigType] [varchar] (20) COLLATE Hebrew_CI_AS NOT NULL ,
	[ConfigValue] [nvarchar] (50) COLLATE Hebrew_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[App_Counters]    Script Date: 23/09/2007 15:58:49 ******/
CREATE TABLE [dbo].[App_Counters] (
	[CounterType] [varchar] (50) COLLATE Hebrew_CI_AS NOT NULL ,
	[CounterValue] [int] NOT NULL ,
	[CounterID] [int] IDENTITY (1, 1) NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[App_Enum]    Script Date: 23/09/2007 15:58:49 ******/
CREATE TABLE [dbo].[App_Enum] (
	[AppKey] [varchar] (50) COLLATE Hebrew_CI_AS NOT NULL ,
	[AppValue] [nvarchar] (50) COLLATE Hebrew_CI_AS NOT NULL ,
	[Name] [varchar] (50) COLLATE Hebrew_CI_AS NOT NULL ,
	[AppTable] [varchar] (50) COLLATE Hebrew_CI_AS NOT NULL ,
	[KeyIndex] [int] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[App_Summarize]    Script Date: 23/09/2007 15:58:49 ******/
CREATE TABLE [dbo].[App_Summarize] (
	[SumKey] [varchar] (50) COLLATE Hebrew_CI_AS NOT NULL ,
	[SumValue] [numeric](18, 0) NOT NULL ,
	[Subject] [varchar] (50) COLLATE Hebrew_CI_AS NOT NULL ,
	[Modified] [datetime] NOT NULL ,
	[id] [int] IDENTITY (1, 1) NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 23/09/2007 15:58:49 ******/
CREATE TABLE [dbo].[Users] (
	[UserID] [int] IDENTITY (1, 1) NOT NULL ,
	[UserName] [nvarchar] (50) COLLATE Hebrew_CI_AS NOT NULL ,
	[PermsGroup] [int] NOT NULL ,
	[Details] [nvarchar] (50) COLLATE Hebrew_CI_AS NULL ,
	[LogInName] [varchar] (50) COLLATE Hebrew_CI_AS NULL ,
	[Password] [varchar] (10) COLLATE Hebrew_CI_AS NULL ,
	[AccountId] [int] NULL ,
	[Lang] [char] (2) COLLATE Hebrew_CI_AS NULL ,
	[MailAddress] [varchar] (250) COLLATE Hebrew_CI_AS NULL ,
	[Phone] [varchar] (20) COLLATE Hebrew_CI_AS NULL ,
	[Url] [varchar] (255) COLLATE Hebrew_CI_AS NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Users_Group]    Script Date: 23/09/2007 15:58:49 ******/
CREATE TABLE [dbo].[Users_Group] (
	[PermsGroupID] [int] IDENTITY (1, 1) NOT NULL ,
	[PermsGroupName] [nvarchar] (20) COLLATE Hebrew_CI_AS NOT NULL ,
	[IsBuiltIn] [bit] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Users_Lvl]    Script Date: 23/09/2007 15:58:49 ******/
CREATE TABLE [dbo].[Users_Lvl] (
	[Lvl] [tinyint] NOT NULL ,
	[LvlName] [varchar] (50) COLLATE Hebrew_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Users_Permissions]    Script Date: 23/09/2007 15:58:49 ******/
CREATE TABLE [dbo].[Users_Permissions] (
	[PermsID] [int] NOT NULL ,
	[ObjectID] [int] NOT NULL ,
	[Lvl] [tinyint] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Users_UIObjects]    Script Date: 23/09/2007 15:58:49 ******/
CREATE TABLE [dbo].[Users_UIObjects] (
	[ObjectID] [int] IDENTITY (1, 1) NOT NULL ,
	[ObjectName] [varchar] (50) COLLATE Hebrew_CI_AS NOT NULL ,
	[Description] [nvarchar] (50) COLLATE Hebrew_CI_AS NOT NULL ,
	[UItype] [int] NOT NULL ,
	[UISystem] [int] NOT NULL ,
	[UIParent] [int] NOT NULL ,
	[Tag] [varchar] (255) COLLATE Hebrew_CI_AS NULL ,
	[GroupOrder] [tinyint] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Users_UISystem]    Script Date: 23/09/2007 15:58:49 ******/
CREATE TABLE [dbo].[Users_UISystem] (
	[UIsystemID] [int] IDENTITY (1, 1) NOT NULL ,
	[UIsystem] [varchar] (50) COLLATE Hebrew_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Users_UITypes]    Script Date: 23/09/2007 15:58:50 ******/
CREATE TABLE [dbo].[Users_UITypes] (
	[UItypeID] [int] IDENTITY (1, 1) NOT NULL ,
	[UITypeName] [varchar] (50) COLLATE Hebrew_CI_AS NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Alert_Config] WITH NOCHECK ADD 
	CONSTRAINT [PK_Alert_Config] PRIMARY KEY  CLUSTERED 
	(
		[ConfigKey]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Alert_ExceptionType] WITH NOCHECK ADD 
	CONSTRAINT [PK_Alert_ExceptionType] PRIMARY KEY  CLUSTERED 
	(
		[ExceptionType]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Alert_Exceptions] WITH NOCHECK ADD 
	CONSTRAINT [PK_Alert_Exceptions] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Alert_Priority] WITH NOCHECK ADD 
	CONSTRAINT [PK_Alert_Priority] PRIMARY KEY  CLUSTERED 
	(
		[Priority]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Alert_Site] WITH NOCHECK ADD 
	CONSTRAINT [PK_Alert_Site] PRIMARY KEY  CLUSTERED 
	(
		[SiteId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Alert_Users_Exception] WITH NOCHECK ADD 
	CONSTRAINT [PK_Alert_Users_Exception] PRIMARY KEY  CLUSTERED 
	(
		[UserId],
		[ExceptionId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[App_Config] WITH NOCHECK ADD 
	CONSTRAINT [PK_App_Config] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[App_Counters] WITH NOCHECK ADD 
	CONSTRAINT [PK_App_Counters] PRIMARY KEY  CLUSTERED 
	(
		[CounterID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[App_Summarize] WITH NOCHECK ADD 
	CONSTRAINT [PK_App_Summarize] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Users] WITH NOCHECK ADD 
	CONSTRAINT [PK_Users] PRIMARY KEY  CLUSTERED 
	(
		[UserID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Users_Group] WITH NOCHECK ADD 
	CONSTRAINT [PK_Users_Group] PRIMARY KEY  CLUSTERED 
	(
		[PermsGroupID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Users_Lvl] WITH NOCHECK ADD 
	CONSTRAINT [PK_Users_Lvl] PRIMARY KEY  CLUSTERED 
	(
		[Lvl]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Users_Permissions] WITH NOCHECK ADD 
	CONSTRAINT [PK_Users_Permissions] PRIMARY KEY  CLUSTERED 
	(
		[PermsID],
		[ObjectID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Users_UIObjects] WITH NOCHECK ADD 
	CONSTRAINT [PK_Users_UIObjects] PRIMARY KEY  CLUSTERED 
	(
		[ObjectID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Users_UISystem] WITH NOCHECK ADD 
	CONSTRAINT [PK_Users_UISystem] PRIMARY KEY  CLUSTERED 
	(
		[UIsystemID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Users_UITypes] WITH NOCHECK ADD 
	CONSTRAINT [PK_Users_UITypes] PRIMARY KEY  CLUSTERED 
	(
		[UItypeID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Alert_ExceptionType] ADD 
	CONSTRAINT [DF_Alert_ExceptionType_AlertLevel] DEFAULT (0) FOR [AlertLevel],
	CONSTRAINT [DF_Alert_ExceptionType_Priority] DEFAULT (0) FOR [Priority],
	CONSTRAINT [DF_Alert_ExceptionType_siteId] DEFAULT (0) FOR [siteId]
GO

ALTER TABLE [dbo].[Alert_Exceptions] ADD 
	CONSTRAINT [DF_Alert_Exceptions_CreationDate] DEFAULT (getdate()) FOR [CreationDate],
	CONSTRAINT [DF_Alert_Exceptions_Priority] DEFAULT (0) FOR [Priority]
GO

ALTER TABLE [dbo].[Alert_Users_Exception] ADD 
	CONSTRAINT [DF_Alert_Users_Exception_AlertType] DEFAULT (0) FOR [AlertType]
GO

ALTER TABLE [dbo].[Users_UIObjects] ADD 
	CONSTRAINT [DF_Users_UIObjects_UItype] DEFAULT (1) FOR [UItype],
	CONSTRAINT [DF_Users_UIObjects_GroupOrder] DEFAULT (0) FOR [GroupOrder]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  View dbo.vw_AlertsUserException    Script Date: 23/09/2007 15:58:50 ******/
CREATE VIEW dbo.vw_AlertsUserException
AS
SELECT     dbo.Alert_Users_Exception.UserId, dbo.Alert_Users_Exception.ExceptionId, dbo.Alert_Users_Exception.AlertType, dbo.Users.UserName, 
                      dbo.Users.MailAddress, dbo.Users.Phone, dbo.Users.Url
FROM         dbo.Alert_Users_Exception INNER JOIN
                      dbo.Users ON dbo.Alert_Users_Exception.UserId = dbo.Users.UserID

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  View dbo.vw_Users_PermissionUI    Script Date: 23/09/2007 15:58:50 ******/
CREATE VIEW dbo.vw_Users_PermissionUI
AS
SELECT     UP.PermsID, UP.Lvl, UO.ObjectID, UO.ObjectName, UO.UItype, UO.UISystem, UO.UIParent
FROM         dbo.Users_Permissions UP INNER JOIN
                      dbo.Users_UIObjects UO ON UP.ObjectID = UO.ObjectID

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  View dbo.vw_Users_UIObjects    Script Date: 23/09/2007 15:58:50 ******/
CREATE VIEW dbo.vw_Users_UIObjects
AS
SELECT     ObjectID, ObjectName, 3 AS Lvl
FROM         dbo.Users_UIObjects

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


/****** Object:  View dbo.vw_AlertsUserException    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vw_AlertsUserException]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[vw_AlertsUserException]
GO

/****** Object:  View dbo.vw_Users_PermissionUI    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vw_Users_PermissionUI]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[vw_Users_PermissionUI]
GO

/****** Object:  View dbo.vw_Users_UIObjects    Script Date: 23/09/2007 15:58:48 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vw_Users_UIObjects]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[vw_Users_UIObjects]
GO



