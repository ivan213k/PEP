
SET IDENTITY_INSERT [dbo].[Deeplink] ON

INSERT INTO [dbo].[Deeplink]([Id], [Code], [SurveyId], [UserId], [ExpireDate], [StateId])
VALUES (1, '0f8fad5b-d9cb-469f-a165-70867728950e', 1, 1, '20191125', 1)

INSERT INTO [dbo].[Deeplink]([Id], [Code], [SurveyId], [UserId], [ExpireDate], [StateId])
VALUES (2, '3AAAAAAA-BBBB-CCCC-DDDD-2EEEEEEEEEEE', 2, 2, '20200705', 1)

INSERT INTO [dbo].[Deeplink]([id],[Code], [SurveyId], [UserId], [ExpireDate], [StateId])
VALUES (3,'936DA01F-9ABD-4d9d-80C7-02AF85C822A8', 3, 3, '20210109', 2)

SET IDENTITY_INSERT [dbo].[Deeplink] OFF

