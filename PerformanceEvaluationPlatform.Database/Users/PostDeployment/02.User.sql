SET IDENTITY_INSERT [dbo].[User] ON
INSERT INTO [dbo].[User]([Id],[Email],[FirstName],[LastName],[FirstDayInIndustry],[FirstDayInCompany],[StateId],[TeamId],[TechnicalLevelId],[EnglishLevelId],[SystemRole])
VALUES
(1,'userExample@gor.com','Artur','Grugon','20170721','20200819',2,101,1,4,0),
(2,'KodKiller@gmail.com','Kiril','Krigan','20160423','20211223',1,102,3,5,1),
(3,'bestmanager@ukr.net','Kristina','Lavruk','20050711','20190624',2,101,2,4,2),
(4,'qwerty123@gmail.com','FirstName123','LastName123','20050505','20150405',1,101,1,4,0),
(5,'qwerty234@gmail.com','FirstName234','LastName123','20050505','20150405',1,102,1,4,0),
(6,'qwerty345@gmail.com','FirstName345','LastName123','20050505','20150405',1,103,1,4,0),
(7,'qwerty456@gmail.com','FirstName456','LastName123','20050505','20150405',1,103,1,5,0),
(8,'qwerty567@gmail.com','FirstName567','LastName123','20050505','20150405',1,104,1,5,0),
(9,'qwerty678@gmail.com','FirstName678','LastName123','20050505','20150405',1,104,1,4,0)

SET IDENTITY_INSERT [dbo].[User] OFF


