CREATE TABLE [dbo].[FormTemplate]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[Name] Nvarchar(128) NOT NULL,
	[CreatedAt] DateTime2 NOT NULL,
	[Version] INT NOT NULL,
	[StatusId] INT NOT NULL,

	CONSTRAINT [PK_FormTemplate] PRIMARY KEY([Id]),
	CONSTRAINT [FK_FormTemplate_FormTemplateStatus] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[FormTemplateStatus]([Id])

)
