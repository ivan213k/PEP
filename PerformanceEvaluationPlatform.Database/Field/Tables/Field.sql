CREATE TABLE [dbo].[Field]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[FieldTypeId] INT NOT NULL,
	[FieldGroupId] INT,
	[Name] NVARCHAR(256) NOT NULL,
	[Description] NVARCHAR(256) NOT NULL,
	[AssesmentGroupId] INT NOT NULL,
	[IsRequired] BIT NOT NULL,

	CONSTRAINT [PK_Field] PRIMARY KEY([Id]),
	CONSTRAINT [FK_Field_FieldTypeId] FOREIGN KEY ([FieldTypeId]) REFERENCES [dbo].[FieldType]([Id]),
	CONSTRAINT [FK_Field_Field_AssesmentGroupId] FOREIGN KEY ([AssesmentGroupId]) REFERENCES [dbo].[AssesmentGroup]([Id]),
	CONSTRAINT [FK_Field_FieldGroupId] FOREIGN KEY ([FieldGroupId]) REFERENCES [dbo].[FieldGroup]([Id])
	
)
