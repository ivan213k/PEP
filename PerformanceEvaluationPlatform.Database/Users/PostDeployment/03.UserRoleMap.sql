SET IDENTITY_INSERT [dbo].[UserRoleMap] ON
INSERT INTO [dbo].[UserRoleMap]([Id],[RoleId],[UserId])
VALUES
(1,1,1),
(2,2,3),
(3,3,2)
SET IDENTITY_INSERT [dbo].[UserRoleMap] OFF
