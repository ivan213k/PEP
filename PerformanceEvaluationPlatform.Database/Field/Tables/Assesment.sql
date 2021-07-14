CREATE TABLE [dbo].[Assesment]
(
	[Id] INT NOT NULL,
	[Name] NVARCHAR(128) NOT NULL,
	[AssesmentGroupId] INT NOT NULL,
	[IsCommentRequired] BIT NOT NULL,

	CONSTRAINT [PK_Assesment] PRIMARY KEY([Id]),
	CONSTRAINT [FK_Field_AssesmentGroupId] FOREIGN KEY ([AssesmentGroupId]) REFERENCES [dbo].[AssesmentGroup]([Id])

)
