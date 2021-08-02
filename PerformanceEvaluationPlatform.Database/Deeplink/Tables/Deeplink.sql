CREATE TABLE [dbo].[Deeplink]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[Code] UNIQUEIDENTIFIER NOT NULL,
	[SurveyId] INT NOT NULL,
	[UserId] INT NOT NULL,
	[ExpireDate] DATETIME2 NOT NULL,
	[StateId] INT NOT NULL,
	[SentById] INT NOT NULL,
	[SentAt] DATETIME2 NOT NULL

	CONSTRAINT [PK_Deeplink] PRIMARY KEY([Id]),
	CONSTRAINT [FK_Deeplink_DeeplinkStateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[DeeplinkState]([Id]),
	CONSTRAINT [FK_Deeplink_DeeplinkSurveyId] FOREIGN KEY ([SurveyId]) REFERENCES [dbo].[Survey]([Id]),
	CONSTRAINT [FK_Deeplink_DeeplinkUserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([Id]),
	CONSTRAINT [FK_Deeplink_DeeplinkUserById] FOREIGN KEY ([SentById]) REFERENCES [dbo].[User]([Id])
)
