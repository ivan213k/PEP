CREATE TABLE [dbo].[FieldData]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[FormDataId] INT NOT NULL,
	[FieldId] INT NOT NULL,
	[AssesmentId] INT NOT NULL,
	[Comment] NVARCHAR (MAX),

	CONSTRAINT [PK_FieldData] PRIMARY KEY([Id]),
	CONSTRAINT [FK_Field_FieldId_Data] FOREIGN KEY ([FieldId]) REFERENCES [dbo].[Field]([Id]),
	CONSTRAINT [FK_Field_FormDataId] FOREIGN KEY ([FormDataId]) REFERENCES [dbo].[FormData]([Id]),
	CONSTRAINT [FK_Field_AssesmentId] FOREIGN KEY ([AssesmentId]) REFERENCES [dbo].[Assesment]([Id])
)
