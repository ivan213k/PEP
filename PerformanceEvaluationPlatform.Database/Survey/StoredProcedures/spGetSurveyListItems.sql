CREATE PROCEDURE [dbo].[spGetSurveyListItems]
@Search NVARCHAR(256),
@StateIds [dbo].[IntList] READONLY,
@AssigneeIds [dbo].[IntList] READONLY,
@SupervisorIds [dbo].[IntList] READONLY,
@AppointmentDateFrom DATETIME2,
@AppointmentDateTo DATETIME2,
@FormNameSortOrder INT,
@AssigneeSortOrder INT,
@Skip INT,
@Take INT
AS
BEGIN
	DECLARE @SearchClause NVARCHAR(258) = ''
	DECLARE @WhereClause NVARCHAR(MAX) = ''
	DECLARE @JoinClause NVARCHAR(MAX) = ''
	DECLARE @OrderClause NVARCHAR(MAX) = ''
 
	IF(@Search IS NOT NULL)
	BEGIN
		SET @SearchClause = '%' + @Search + '%';
		IF(@WhereClause = '')
			SET @WhereClause = 'WHERE';
		ELSE
			SET @WhereClause = @WhereClause + ' AND '

		SET @WhereClause = @WhereClause + ' ([FT].[Name] LIKE @SearchClause OR [AU].[FirstName] + '' '' + [AU].[LastName] Like @SearchClause) '
	END
	IF(EXISTS(SELECT * FROM @StateIds))
		BEGIN
			SET @JoinClause = @JoinClause + ' INNER JOIN @StateIds [SI] ON [SI].[Id] = [S].[StateId] '
		End
	IF(EXISTS(SELECT * FROM @AssigneeIds))
		BEGIN
			SET @JoinClause = @JoinClause + ' INNER JOIN @AssigneeIds [AI] ON [AI].[Id] = [S].[AssigneeId] '
		END
	IF(EXISTS(SELECT * FROM @SupervisorIds))
		BEGIN
			SET @JoinClause = @JoinClause + ' INNER JOIN @SupervisorIds [SVI] ON [SVI].[Id] = [S].[SupervisorId] '
		END
	IF(@AppointmentDateFrom IS NOT NULL)
		BEGIN
			IF(@WhereClause = '')
				SET @WhereClause = 'WHERE';
			ELSE
				SET @WhereClause = @WhereClause + ' AND '
			SET @WhereClause = @WhereClause + ' [S].[AppointmentDate] >= @AppointmentDateFrom '
		END
	IF(@AppointmentDateTo IS NOT NULL)
		BEGIN
			IF(@WhereClause = '')
				SET @WhereClause = 'WHERE';
			ELSE
				SET @WhereClause = @WhereClause + ' AND '
			SET @WhereClause = @WhereClause + ' [S].[AppointmentDate] <= @AppointmentDateTo '
	END
	IF (@FormNameSortOrder IS NOT NULL)
		BEGIN
			IF (@FormNameSortOrder = 1)
				SET @OrderClause = '[FT].[Name] ASC'
			ELSE
				SET @OrderClause = '[FT].[Name] DESC'
		END
	IF (@AssigneeSortOrder IS NOT NULL)
		BEGIN
			IF (@AssigneeSortOrder = 1)
				SET @OrderClause = '[AU].[FirstName] + '' '' + [AU].[LastName] ASC'
			ELSE
				SET @OrderClause = '[AU].[FirstName] + '' '' + [AU].[LastName] DESC'
		END
	IF (@OrderClause = '')
		SET @OrderClause = '[FT].[Name] ASC'
	IF(@Skip IS NULL)
		SET @Skip = 0
	IF(@Take IS NULL)
		SET @Take = 30

	DECLARE @Sql NVARCHAR(MAX) = '
		SELECT [S].Id,
			[FT].[Name] AS [FormName],
			[FT].[Id] AS [FormId],
			[AU].[FirstName] AS [AssigneeFirstName],
			[AU].[LastName] AS [AssigneeLastName],
			[AU].[Id] AS [AssigneeId],
			[SU].[FirstName] AS [SupervisorFirstName],
			[SU].[LastName] AS [SupervisorLastName],
			[SU].[Id] AS [SupervisorId],
			[S].[AppointmentDate],
			[SS].[Name] AS [StateName],
			[SS].[Id] AS [StateId],
			[Dl].UserId AS [AssignedUserId],
			[FD].[UserId] AS [FormDataAssignedUserId],
			[FD].[FormDataStateId] AS [AssignedUserStateId]
		FROM [dbo].Survey [S]
		INNER JOIN [dbo].[SurveyState] [SS] ON [S].[StateId] = [SS].[Id]
		INNER JOIN [dbo].[FormTemplate] [FT] ON [S].[FormTemplateId] = [FT].[Id]
		INNER JOIN [dbo].[User] [AU] ON [S].[AssigneeId] = [AU].[Id]
		INNER JOIN [dbo].[User] [SU] ON [S].[SupervisorId] = [SU].[Id]
		LEFT JOIN [dbo].[Deeplink] [DL] ON [S].[Id] = [DL].[SurveyId]
		LEFT JOIN [dbo].[FormData] [FD] ON [S].[Id] = [FD].[SurveyId]
		'+ @JoinClause + ' 
		'+ @WhereClause + ' 
		ORDER BY ' + @OrderClause + '
		OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
			';
 
	 DECLARE @Params NVARCHAR(MAX) = '
		@SearchClause NVARCHAR(258),
		@StateIds [dbo].[IntList] READONLY,
		@AssigneeIds [dbo].[IntList] READONLY,
		@SupervisorIds [dbo].[IntList] READONLY,
		@AppointmentDateFrom DATETIME2,
		@AppointmentDateTo DATETIME2,
		@Skip INT,
		@Take INT
	';
 
	EXECUTE sp_executesql @Sql, @Params,
		@SearchClause,
		@StateIds,
		@AssigneeIds,
		@SupervisorIds,
		@AppointmentDateFrom,
		@AppointmentDateTo,
		@Skip,
		@Take
END