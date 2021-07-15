CREATE TABLE [dbo].[Project]
(
	[Id] INT NOT NULL IDENTITY(1,1), 
    [Title] NVARCHAR(256) NOT NULL,
    [StartDate] DATETIME2 NOT NULL, 
    [CoordinatorId] INT NOT NULL

    CONSTRAINT [PK_Project] PRIMARY KEY([Id])
    CONSTRAINT [FK_Project_User] FOREIGN KEY ([CoordinatorId]) REFERENCES [dbo].[User]([Id]) 
)