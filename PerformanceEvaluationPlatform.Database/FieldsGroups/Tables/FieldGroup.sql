﻿CREATE TABLE [dbo].[FieldGroup]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[Title] NVARCHAR(256) NOT NULL,

	CONSTRAINT [PK_FieldGroup] PRIMARY KEY ([Id])
)
