CREATE TABLE [dbo].[FormTemplateFieldMap]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[FormTemplateId] INT NOT NULL,
	[FieldId] INT NOT NULL,
	[Order] INT NOT NULL,

	CONSTRAINT [PK_FormTemplateFieldMap] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_FormTemplateFieldMap_FormTemplate] FOREIGN KEY ([FormTemplateId]) REFERENCES [dbo].[FormTemplate]([Id]),
	CONSTRAINT [FK_FormTemplateFieldMap_Field] FOREIGN KEY ([FieldId]) REFERENCES [dbo].[Field]([Id])
)
