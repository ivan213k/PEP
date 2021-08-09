SET IDENTITY_INSERT [dbo].[Survey] ON
INSERT INTO [dbo].[Survey]([Id], [StateId], [FormTemplateId], [AssigneeId], [SupervisorId], [RecommendedLevelId], [AppointmentDate], [Summary])
VALUES (1, 1, 1, 2, 1, 1, '10-07-2021','Summary text')

INSERT INTO [dbo].[Survey]([Id], [StateId], [FormTemplateId], [AssigneeId], [SupervisorId], [RecommendedLevelId], [AppointmentDate], [Summary])
VALUES (2, 2, 2, 3, 1, 2, '11-07-2021','Summary text 1')


INSERT INTO [dbo].[Survey]([Id], [StateId], [FormTemplateId], [AssigneeId], [SupervisorId], [RecommendedLevelId], [AppointmentDate])
VALUES (3, 3, 3, 2, 1, 3, '12-07-2021')

INSERT INTO [dbo].[Survey]([Id], [StateId], [FormTemplateId], [AssigneeId], [SupervisorId], [RecommendedLevelId], [AppointmentDate], [Summary])
VALUES (4, 4, 3, 2, 1, 3, '12-07-2021','Summary text 2')

INSERT INTO [dbo].[Survey]([Id], [StateId], [FormTemplateId], [AssigneeId], [SupervisorId], [RecommendedLevelId], [AppointmentDate])
VALUES (5, 4, 3, 2, 1, 3, '12-09-2021')

SET IDENTITY_INSERT [dbo].[Survey] OFF