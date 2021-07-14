CREATE TABLE [dbo].[FormData]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[SurveyId] INT NOT NULL,
	[UserId] INT NOT NULL,
	[FormDataStateId] INT NOT NULL,

	CONSTRAINT [PK_FormData] PRIMARY KEY([Id]),
	CONSTRAINT [FK_FormData_SurveyId] FOREIGN KEY ([SurveyId]) REFERENCES [dbo].[Survey]([Id]),
	CONSTRAINT [FK_FormData_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([Id]),
    CONSTRAINT [FK_FormData_FormDataStateId] FOREIGN KEY ([FormDataStateId]) REFERENCES [dbo].[FormDataState]([Id])
)
