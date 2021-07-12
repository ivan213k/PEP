﻿CREATE TABLE [dbo].[Survey]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[StateId] INT NOT NULL,
	[FormId] INT NOT NULL,
	[AssigneeId] INT NOT NULL,
	[SupervisorId] INT NOT NULL,
	[RecommendedLevelId] INT NOT NULL,
	[AppointmentDate] DATETIME2 NOT NULL,
	[Summary] NVARCHAR(256),

	CONSTRAINT [PK_Survey] PRIMARY KEY([Id]),
	CONSTRAINT [FK_Survey_SurveyStateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[SurveyState]([Id]),
	CONSTRAINT [FK_Survey_RecommendedLevelId] FOREIGN KEY ([RecommendedLevelId]) REFERENCES [dbo].[Level]([Id])
)
