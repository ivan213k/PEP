﻿CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL IDENTITY (1,1),
	[FirstName] NVARCHAR(70) NOT NULL,
	[LastName] NVARCHAR(120) NOT NULL,
	[Email] NVARCHAR(40) NOT NULL,
	[FirstDayInIndustry] DATE NOT NULL,
	[FirstDayInCompany] DATE NOT NULL,
	[TeamId] INT NOT NULL,
	[StateId] INT  NOT NULL,
	[TechnicalLevelId] INT NOT NULL,
	[EnglishLevelId] INT NOT NULL 

	CONSTRAINT [Pk_User] PRIMARY KEY(Id),
    [SystemRoleId] NVARCHAR(40) NOT NULL, 
    CONSTRAINT [Fk_User_UserState] FOREIGN KEY ([StateId]) REFERENCES [dbo].[UserState](Id),
	CONSTRAINT [Fk_User_Team] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Team]([Id]),
	CONSTRAINT [Fk_User_Level] FOREIGN KEY ([TechnicalLevelId]) REFERENCES [dbo].[Level]([Id]),
	CONSTRAINT [Fk_User_EnglishLevel] FOREIGN KEY ([EnglishLevelId]) REFERENCES [dbo].[Level]([Id]),
	CONSTRAINT [Fk_User_SystemRole] FOREIGN KEY ([SystemRoleId]) REFERENCES [dbo].[SystemRole]([Id])


)
