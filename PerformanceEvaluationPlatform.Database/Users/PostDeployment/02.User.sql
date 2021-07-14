SET IDENTITY_INSERT [dbo].[User] ON
INSERT INTO [dbo].[User]([Id],[Email],[FirstName],[LastName],[StateId],[TeamId])
VALUES
(1,'userExample@gor.com','Artur','Grugon',1,101),
(2,'KodKiller@gmail.com','Kiril','Krigan',1,102),
(3,'bestmanager@ukr.net','Kristina','Lavruk',1,101)

SET IDENTITY_INSERT [dbo].[User] OFF


