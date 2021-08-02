SET IDENTITY_INSERT [dbo].[FieldData] ON

INSERT INTO [dbo].[FieldData]([Id], [FormDataId], [FieldId], [AssesmentId], [Order], [Comment] )
VALUES (1, 1, 1, 1, 1, 'Test Field Data 1')

INSERT INTO [dbo].[FieldData]([Id], [FormDataId], [FieldId], [AssesmentId], [Order], [Comment] )
VALUES (2, 2, 2, 2, 2, 'Test Field Data 2')

INSERT INTO [dbo].[FieldData]([Id], [FormDataId], [FieldId], [Order], [AssesmentId] )
VALUES (3, 3, 3, 3, 3)

SET IDENTITY_INSERT [dbo].[FieldData] OFF