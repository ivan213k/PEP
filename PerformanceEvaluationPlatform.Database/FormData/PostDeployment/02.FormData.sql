SET IDENTITY_INSERT [dbo].[FormData] ON
INSERT INTO [dbo].[FormData]([Id], [SurveyId], [ReviewerId], [FormDataStateId])
VALUES (1, 1, 1, 1)

INSERT INTO [dbo].[FormData]([Id], [SurveyId], [ReviewerId], [FormDataStateId])
VALUES (2, 2, 2, 2)

INSERT INTO [dbo].[FormData]([Id], [SurveyId], [ReviewerId], [FormDataStateId])
VALUES (3, 3, 3, 1)
SET IDENTITY_INSERT [dbo].[FormData] OFF