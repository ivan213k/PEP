SET IDENTITY_INSERT [dbo].[UserRoleMap] ON
INSERT INTO [dbo].[UserRoleMap]([Id],[RoleId],[UserId])
VALUES
(1,1,1),
(2,2,3),
(3,3,2),
(4,4,4),
(5,4,5),
(6,3,6),
(7,4,7),
(8,2,8),
(9,4,9)
SET IDENTITY_INSERT [dbo].[UserRoleMap] OFF
