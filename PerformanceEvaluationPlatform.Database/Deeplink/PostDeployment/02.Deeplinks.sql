
SET IDENTITY_INSERT [dbo].[Deeplink] ON

INSERT INTO [dbo].[Deeplink]([Id], [Code], [SurveyId], [UserId], [ExpireDate], [StateId],[SentById],[SentAt])
VALUES (1, '0f8fad5b-d9cb-469f-a165-70867728950e', 1, 2, '20191125', 2,1,'20210725')

INSERT INTO [dbo].[Deeplink]([Id], [Code], [SurveyId], [UserId], [ExpireDate], [StateId],[SentById],[SentAt])
VALUES (2, '3AAAAAAA-BBBB-CCCC-DDDD-2EEEEEEEEEEE', 2, 3, '20200705', 1,1,'20210725')

INSERT INTO [dbo].[Deeplink]([id],[Code], [SurveyId], [UserId], [ExpireDate], [StateId],[SentById],[SentAt])
VALUES (3,'936DA01F-9ABD-4d9d-80C7-02AF85C822A8', 3, 4, '20210109', 2,1,'20210725')

INSERT INTO [dbo].[Deeplink]([id],[Code], [SurveyId], [UserId], [ExpireDate], [StateId],[SentById],[SentAt])
VALUES (4,'836A150F-1611-4BB0-AD98-9C41BC2CE442', 1, 5, '20210709', 1,1,'20210725')

INSERT INTO [dbo].[Deeplink]([id],[Code], [SurveyId], [UserId], [ExpireDate], [StateId],[SentById],[SentAt])
VALUES (5,'E910FC20-F150-4B66-BB47-B7303F666E90', 3, 6, '20221109', 2,1,'20210725')

SET IDENTITY_INSERT [dbo].[Deeplink] OFF

