SET IDENTITY_INSERT [dbo].[Project] ON
INSERT INTO [dbo].[Project]([Id], [Title], [StartDate]) 
VALUES 
(1, 'Project1', '10-07-2021'), 
(2, 'Project2', '01-07-2021'), 
(3, 'Project3', '05-07-2021')

SET IDENTITY_INSERT [dbo].[Project] OFF