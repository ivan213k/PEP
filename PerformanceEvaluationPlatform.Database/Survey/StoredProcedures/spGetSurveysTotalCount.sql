CREATE PROCEDURE [dbo].[spGetSurveysTotalCount]
	@Search NVARCHAR(256),
	@StateIds [dbo].[IntList] READONLY,
	@AssigneeIds [dbo].[IntList] READONLY,
	@SupervisorIds [dbo].[IntList] READONLY,
	@AppointmentDateFrom DATETIME2,
	@AppointmentDateTo DATETIME2
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

	DECLARE @Sql NVARCHAR(MAX) = '
		SELECT COUNT(*) AS [TotalItemsCount]
		FROM [dbo].Survey [S]
		'+ @JoinClause + ' 
		'+ @WhereClause + ' ';
 
	 DECLARE @Params NVARCHAR(MAX) = '
		@SearchClause NVARCHAR(258),
		@StateIds [dbo].[IntList] READONLY,
		@AssigneeIds [dbo].[IntList] READONLY,
		@SupervisorIds [dbo].[IntList] READONLY,
		@AppointmentDateFrom DATETIME2,
		@AppointmentDateTo DATETIME2
	';
 
	EXECUTE sp_executesql @Sql, @Params,
		@SearchClause,
		@StateIds,
		@AssigneeIds,
		@SupervisorIds,
		@AppointmentDateFrom,
		@AppointmentDateTo
END
