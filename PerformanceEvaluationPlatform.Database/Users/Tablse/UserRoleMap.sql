CREATE TABLE [dbo].[UserRoleMap]
(
	[Id] INT NOT NULL IDENTITY (1,1),
	[UserId] INT NOT NULL,
	[RoleId] INT NOT NULL

	CONSTRAINT [Pk_UserRoleMap] PRIMARY KEY([Id]),
	CONSTRAINT [Fk_UserRoleMap_User] FOREIGN KEY([UserId]) REFERENCES [dbo].[User]([Id]),
	CONSTRAINT [Fk_UserRoleMap_Role] FOREIGN KEY([RoleId]) REFERENCES [dbo].[Role]([Id])

)
