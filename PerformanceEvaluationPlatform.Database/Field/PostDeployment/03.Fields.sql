SET IDENTITY_INSERT [dbo].[Field] ON

INSERT INTO [dbo].[Field]([Id], [FieldTypeId], [FieldGroupId], [Name], [Description], [AssesmentGroupId], [IsRequired])
VALUES (1, 2, 1, 'Communication skills', 'Communication skills description', 2, 1)

INSERT INTO [dbo].[Field]([Id], [FieldTypeId], [FieldGroupId], [Name], [Description], [AssesmentGroupId], [IsRequired])
VALUES (2, 2, 1, 'Written communication', 'Written communication description', 2, 1)

INSERT INTO [dbo].[Field]([Id], [FieldTypeId], [Name], [Description], [AssesmentGroupId], [IsRequired])
VALUES (3, 1, 'Full-bleed divider', '', 1, 0)

SET IDENTITY_INSERT [dbo].[Field] OFF