﻿SET IDENTITY_INSERT [dbo].[User] ON
INSERT INTO [dbo].[User]([Id],[Email],[FirstName],[LastName],[FirstDayInIndustry],[FirstDayInCompany],[StateId],[TeamId],[LevelId])
VALUES
(1,'userExample@gor.com','Artur','Grugon','20170721','20200819',1,101,1),
(2,'KodKiller@gmail.com','Kiril','Krigan','20160423','20211223',1,102,3),
(3,'bestmanager@ukr.net','Kristina','Lavruk','20050711','20190624',1,101,2)

SET IDENTITY_INSERT [dbo].[User] OFF


