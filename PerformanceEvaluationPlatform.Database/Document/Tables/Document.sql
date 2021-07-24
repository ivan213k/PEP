CREATE TABLE [dbo].[Document]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[UserId] INT NOT NULL,
	[TypeId] INT NOT NULL,
	[ValidToDate] DATETIME2 NOT NULL,
	[FileName] NVARCHAR(512) NOT NULL,
	[CreatedById] INT NOT NULL,
	[CreatedAt] DATETIME2 NOT NULL,
	[LastUpdatesById] INT,
	[LastUpdatesAt] DATETIME2,
	[MetaData] NVARCHAR(MAX) NOT NULL,


	CONSTRAINT [PK_Document] PRIMARY KEY([Id]),
	CONSTRAINT [FK_Document_DocumentTypeId] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[DocumentType]([Id]),
	CONSTRAINT [FK_Document_DocumentUserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([Id]),
	CONSTRAINT [FK_Document_DocumentCreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [dbo].[User]([Id]),
	CONSTRAINT [FK_Document_DocumentLastUpdatesById] FOREIGN KEY ([LastUpdatesById]) REFERENCES [dbo].[User]([Id]),

)