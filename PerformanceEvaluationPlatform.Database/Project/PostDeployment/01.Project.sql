SET IDENTITY_INSERT [dbo].[Project] ON
INSERT INTO [dbo].[Project]([Id], [Title], [StartDate], [CoordinatorId]) 
VALUES 
(1, 'Project1', '10-07-2021', 1), 
(2, 'Project2', '01-07-2021', 2), 
(3, 'Project3', '05-07-2021', 3)

SET IDENTITY_INSERT [dbo].[Project] OFF