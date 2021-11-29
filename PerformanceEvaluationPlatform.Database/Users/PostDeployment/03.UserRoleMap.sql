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
(9,4,9),
(10,5,1), --Project Coordinator role
(11,5,2), --Project Coordinator role
(12,5,3) --Project Coordinator role
SET IDENTITY_INSERT [dbo].[UserRoleMap] OFF
