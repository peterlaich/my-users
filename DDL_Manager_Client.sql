USE [users]
GO

/****** Object: Table [Domain].[Users] Script Date: 6/24/2019 8:43:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Domain].[Users] (
    [UserId]    INT             IDENTITY (1, 1) NOT NULL,
    [UserName]  NVARCHAR (200)  NOT NULL,
    [Email]     NVARCHAR (1000) NULL,
    [Alias]     NVARCHAR (1000) NULL,
    [FirstName] NVARCHAR (1000) NULL,
    [LastName]  NVARCHAR (1000) NULL
	CONSTRAINT [PK_Domain.Users] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

create table [Domain].[Managers] (
	[UserId]	INT	NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES [Domain].[Users](UserId),
	[Position]	INT NOT NULL);

create table [Domain].[Clients] (
	[UserId]	INT NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES [Domain].[Users](UserId),
	[Level]		INT NOT NULL,
	[MgrId]		INT NOT NULL FOREIGN KEY REFERENCES [Domain].[Managers](UserId));

