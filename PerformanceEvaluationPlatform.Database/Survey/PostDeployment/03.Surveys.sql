SET IDENTITY_INSERT [dbo].[Survey] ON
INSERT INTO [dbo].[Survey]([Id], [StateId], [FormId], [AssigneeId], [SupervisorId], [RecommendedLevelId], [AppointmentDate], [Summary])
VALUES (1, 1, 0,0,0, 1, '10-07-2021','Summary text')

INSERT INTO [dbo].[Survey]([Id], [StateId], [FormId], [AssigneeId], [SupervisorId], [RecommendedLevelId], [AppointmentDate], [Summary])
VALUES (2, 2, 0,0,0, 2, '11-07-2021','Summary text 1')


INSERT INTO [dbo].[Survey]([Id], [StateId], [FormId], [AssigneeId], [SupervisorId], [RecommendedLevelId], [AppointmentDate], [Summary])
VALUES (3, 1, 0,0,0, 3, '12-07-2021','Summary text 2')

SET IDENTITY_INSERT [dbo].[Survey] OFF