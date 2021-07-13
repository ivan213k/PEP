CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL Identity (1,1),
	[FirstName] nvarchar(70) Not Null,
	[LasName] nvarchar(120) Not Null,
	[Email] nvarchar(40) Not Null,
	[TeamId] int Not Null,
	[StateId] int Not Null,

	CONSTRAINT [Pk_User] Primary Key(Id),
	CONSTRAINT [Fk_User_UserState] FOREIGN KEY ([StateId]) REFERENCES [dbo].[UserState](Id),
)
