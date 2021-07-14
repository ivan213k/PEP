SET IDENTITY_INSERT [dbo].[Document] ON
INSERT INTO [dbo].[Document]([Id],[UserId],[TypeId],[ValidToDate],[FileName],[CreatedById],[CreatedAt],[MetaData])
VALUES
(1,1,1,'2021-12-31','Eng.crf',1,'2021-05-15','some metadate1'),
(2,2,2,'2021-10-28','Deu.crf',2,'2021-04-27','some metadate2'),
(3,3,3,'2021-07-01','Fren.crf',3,'2022-02-25','some metadate3')

SET IDENTITY_INSERT [dbo].[User] OFF
