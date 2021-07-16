SET IDENTITY_INSERT [dbo].[Field] ON

INSERT INTO [dbo].[Field]([Id], [FieldTypeId], [FieldGroupId], [Name], [Description], [AssesmentGroupId], [IsRequired])
VALUES (1, 2, 1, 'Communication skills', 'Communication skills description', 2, 1)

INSERT INTO [dbo].[Field]([Id], [FieldTypeId], [FieldGroupId], [Name], [Description], [AssesmentGroupId], [IsRequired])
VALUES (2, 2, 2, 'Written communication', 'Written communication description', 2, 1)

INSERT INTO [dbo].[Field]([Id], [FieldTypeId], [FieldGroupId], [Name], [Description], [AssesmentGroupId], [IsRequired])
VALUES (3, 2, 3, 'Active listening', 'Active listening description', 2, 1)

INSERT INTO [dbo].[Field]([Id], [FieldTypeId], [Name], [Description], [AssesmentGroupId], [IsRequired])
VALUES (4, 1, 'Full-bleed divider', '', 1, 0)

SET IDENTITY_INSERT [dbo].[Field] OFF