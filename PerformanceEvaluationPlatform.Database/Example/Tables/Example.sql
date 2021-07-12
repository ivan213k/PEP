CREATE TABLE [dbo].[Example]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[Title] NVARCHAR(256) NOT NULL,
	[ExampleTypeId] INT NOT NULL,
	[ExampleStateId] INT NOT NULL,

	CONSTRAINT [PK_Example] PRIMARY KEY([Id]),
	CONSTRAINT [FK_Example_ExampleTypeId] FOREIGN KEY ([ExampleTypeId]) REFERENCES [dbo].[ExampleType]([Id]),
	CONSTRAINT [FK_Example_ExampleStateId] FOREIGN KEY ([ExampleStateId]) REFERENCES [dbo].[ExampleState]([Id])
)
