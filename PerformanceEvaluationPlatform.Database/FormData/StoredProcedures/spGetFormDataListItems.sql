CREATE PROCEDURE [dbo].[spGetFormDataListItems]
@Search NVARCHAR(256),
@StateId INT,
@AssigneeIds [dbo].[IntList] READONLY,
@ReviewersIds [dbo].[IntList] READONLY,
@AppointmentDateFrom DATETIME2,
@AppointmentDateTo DATETIME2,
@AssigneeSortOrder INT, 
@FormNameSortOrder INT,
@Skip INT = 0,
@Take INT = 30
AS
BEGIN
	-- local variables
	DECLARE @SearchClause NVARCHAR(258) = '%' + @Search + '%'
	DECLARE @WhereClause NVARCHAR(MAX) = ''
	DECLARE @JoinClause NVARCHAR(MAX) = ''
	DECLARE @OrderClause NVARCHAR(MAX) = ''
 
	 -- query builder
	IF (@Search IS NOT NULL)
	BEGIN
		IF (@WhereClause = '')
			SET @WhereClause = 'WHERE '
		ELSE
			SET @WhereClause = @WhereClause + ' AND '
 
		SET @WhereClause = @WhereClause + ' ([FT].[Name] LIKE ''' + @SearchClause + ''' OR [U].[FirstName] + '' '' + [U].[LastName] LIKE ''' + @SearchClause + '''  OR [U].[FirstName] + '' '' + [U].[LastName] LIKE ''' + @SearchClause + ''') '
	END
 
	IF (@AppointmentDateFrom IS NOT NULL)
	BEGIN
		 IF (@WhereClause = '')
			SET @WhereClause = 'WHERE '
		 ELSE
			SET @WhereClause = @WhereClause + ' AND '
 
		SET @WhereClause = @WhereClause + '[S].[AppointmentDate] >= @AppointmentDateFrom'
	END

	IF (@AppointmentDateTo IS NOT NULL)
	BEGIN
		 IF (@WhereClause = '')
			SET @WhereClause = 'WHERE '
		 ELSE
			SET @WhereClause = @WhereClause + ' AND '
 
		SET @WhereClause = @WhereClause + '[S].[AppointmentDate] <= @AppointmentDateTo'
	END
 
	IF (EXISTS ( SELECT * FROM @AssigneeIds))
	BEGIN
		SET @JoinClause = @JoinClause + ' INNER JOIN @AssigneeIds [AI] ON [AI].[Id] = [FD].[UserId] '
	END

	IF (EXISTS ( SELECT * FROM @ReviewersIds))
	BEGIN
		SET @JoinClause = @JoinClause + ' INNER JOIN @ReviewerIds [RI] ON [RI].[Id] = [FD].[UserId] '
	END

	IF (@StateId IS NOT NULL)
	BEGIN
		 IF (@WhereClause = '')
			SET @WhereClause = 'WHERE '
		 ELSE
			SET @WhereClause = @WhereClause + ' AND '
 
		SET @WhereClause = @WhereClause + '[FD].[FormDataStateId] = @StateId'
	END

	IF (@AssigneeSortOrder IS NOT NULL)
	BEGIN
		IF (@AssigneeSortOrder = 1)
			SET @OrderClause = '[FD].[UserId] ASC'
		ELSE
			SET @OrderClause = '[FD].[UserId] DESC'
	END

	IF (@FormNameSortOrder IS NOT NULL)
	BEGIN
		IF (@FormNameSortOrder = 1)
			SET @OrderClause = '[FT].[Name] ASC'
		ELSE
			SET @OrderClause = '[FT].[Name] DESC'
	END


	IF (@OrderClause = '')
		SET @OrderClause = '[FT].[Name] ASC'

	DECLARE @Sql NVARCHAR(MAX) = '
	SELECT 
		[FD].[Id],
		[FT].[Name] AS [FormName],  
		[FT].[Id] AS [FormId],
		[U].[FirstName] AS [AssigneeFirstName],
		[U].[LastName] AS [AssigneeLastName],
		[U].[Id] AS [AssigneeId],
		[U].[FirstName] AS [ReviewerFirstName],
		[U].[LastName] AS [ReviewerLastName],
		[U].[Id] AS [ReviewerId],
		[S].[AppointmentDate],
		[FDS].[Name] AS [StateName],
		[FDS].[Id] AS [StateId]
	FROM [dbo].[FormData] [FD]
		INNER JOIN [dbo].[Survey] [S] ON [S].[Id] = [FD].[SurveyId]
		INNER JOIN [dbo].[FormTemplate] [FT] ON [FT].[Id] = [S].[FormTemplateId]
		INNER JOIN [dbo].[User] [U] ON [U].[Id] = [FD].[UserId]
		INNER JOIN [dbo].[FormDataState] [FDS] ON [FDS].[Id] = [FD].[FormDataStateId]
		'+ @JoinClause + 
	' '+ @WhereClause + '
	ORDER BY ' + @OrderClause + '
	OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
	';
 
	 DECLARE @Params NVARCHAR(MAX) = '
		@SearchClause NVARCHAR(256),
		@StateId INT,
		@AppointmentDateFrom DATETIME2,
	    @AppointmentDateTo DATETIME2,
		@AssigneeIds [dbo].[IntList] READONLY,
		@ReviewerIds [dbo].[IntList] READONLY,
		@Skip INT,
		@Take INT
	';
 
	EXECUTE sp_executesql @Sql, @Params,
		@SearchClause,
		@StateId,
		@AppointmentDateFrom,
	    @AppointmentDateTo,
		@AssigneeIds,
		@ReviewersIds,
		@Skip,
		@Take
END