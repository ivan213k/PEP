SET IDENTITY_INSERT [dbo].[Example] ON
INSERT INTO [dbo].[Example]([Id], [Title], [ExampleStateId], [ExampleTypeId])
VALUES (1, 'Example 1', 1, 1)

INSERT INTO [dbo].[Example]([Id], [Title], [ExampleStateId], [ExampleTypeId])
VALUES (2, 'Example 2', 2, 2)

INSERT INTO [dbo].[Example]([Id], [Title], [ExampleStateId], [ExampleTypeId])
VALUES (3, 'Example 3', 1, 1)
SET IDENTITY_INSERT [dbo].[Example] OFF