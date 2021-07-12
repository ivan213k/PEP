CREATE TABLE [dbo].[Team]
(
	[Id] INT NOT NULL Identity (1,1),
	[Title] nvarchar(50) NOT NULL,
	[ProjectId] int NOT NULL

		CONSTRAINT [PK_Team] PRIMARY KEY([Id]),
	CONSTRAINT [FK_Team_Project] FOREIGN KEY([ProjectId]) REFERENCES [dbo].Project([Id])
)
