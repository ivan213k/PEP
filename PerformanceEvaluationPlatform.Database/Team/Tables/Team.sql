﻿CREATE TABLE [dbo].[Team]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[Title] NVARCHAR(128) NOT NULL,
	[ProjectId] INT NOT NULL,

	CONSTRAINT [PK_Team] PRIMARY KEY([Id])
)
