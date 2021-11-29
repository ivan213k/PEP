SET IDENTITY_INSERT [dbo].[Role] ON
INSERT INTO [dbo].[Role]([Id], [Title], [IsPrimary])
VALUES (1, 'Backend', 1)

INSERT INTO [dbo].[Role]([Id], [Title], [IsPrimary])
VALUES (2, 'Frontend', 1)

INSERT INTO [dbo].[Role]([Id], [Title], [IsPrimary])
VALUES (3, 'QA', 1)

INSERT INTO [dbo].[Role]([Id], [Title], [IsPrimary])
VALUES (4, 'Team Lead', 0)

INSERT INTO [dbo].[Role]([Id], [Title], [IsPrimary])
VALUES (5, 'Project Coordinator', 0)
SET IDENTITY_INSERT [dbo].[Role] OFF
