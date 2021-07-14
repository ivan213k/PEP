CREATE TABLE [dbo].[DocumentType]
(
	[Id] INT NOT NULL,
	[Name] NVARCHAR(128) NOT NULL,

	CONSTRAINT [PK_DocumentType] PRIMARY KEY([Id])
)