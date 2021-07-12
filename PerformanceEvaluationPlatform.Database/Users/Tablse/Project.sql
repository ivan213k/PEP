CREATE TABLE [dbo].[Project]
(
	[Id] INT NOT NULL Identity (1,1),
	[Startdate] date Not Null,
	[CoordinatorId] int Not Null

	CONSTRAINT [PK_Project] PRIMARY KEY([Id]),
	CONSTRAINT [FK_Project_User] FOREIGN KEY([CoordinatorId]) REFERENCES [dbo].[User]([Id])
)
