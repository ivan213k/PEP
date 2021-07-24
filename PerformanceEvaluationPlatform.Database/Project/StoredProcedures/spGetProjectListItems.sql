CREATE PROCEDURE [dbo].[spGetProjectListItems]
@Search NVARCHAR(256),
@Coordinator INT,
@TitleSortOrder INT,
@StartDateSortOrder DATETIME2,
@CoordinatorSortOrder INT,
@Skip INT,
@Take INT
AS
BEGIN
	DECLARE @SearchClause NVARCHAR(258) = ''
	DECLARE @WhereClause NVARCHAR(MAX) = ''
	DECLARE @OrderClause NVARCHAR(MAX) = ''
 
	IF (@Search IS NOT NULL)
	BEGIN
		SET @SearchClause = '%' + @Search + '%';
		IF (@WhereClause = '')
			SET @WhereClause = 'WHERE '
		ELSE
			SET @WhereClause = @WhereClause + ' AND '
 
		SET @WhereClause = @WhereClause + '[P].[Title] LIKE ''' + @SearchClause + ''' '
	END

	IF (@Coordinator IS NOT NULL)
	BEGIN
		 IF (@WhereClause = '')
			SET @WhereClause = 'HAVING '
		 ELSE
			SET @WhereClause = @WhereClause + ' AND '
 
		SET @WhereClause = @WhereClause + ' [P].[CoordinatorId] = @Coordinator'
	END
	
	IF (@TitleSortOrder IS NOT NULL)
		BEGIN
			IF (@TitleSortOrder = 1)
				SET @OrderClause = '[P].[Title] ASC'
			ELSE
				SET @OrderClause = '[P].[Title] DESC'
		END

	IF(@StartDateSortOrder IS NOT NULL)
		BEGIN
			IF (@TitleSortOrder = 1)
				SET @OrderClause = '[P].[StartDate] ASC'
			ELSE
				SET @OrderClause = '[P].[StartDate] DESC'
		END

	IF (@CoordinatorSortOrder IS NOT NULL)
		BEGIN
			IF (@CoordinatorSortOrder = 1)
				SET @OrderClause = '[P].[Coordinator] ASC'
			ELSE
				SET @OrderClause = '[P].[Coordinator] DESC'
		END
	IF (@OrderClause = '')
		SET @OrderClause = '[P].[Title] ASC'

	DECLARE @Sql NVARCHAR(MAX) = '
	SELECT
		[P].[Id],
		[P].Title,
		[P].StartDateSortOrder AS [StartDate],
		[P].[CoordinatorId] AS [Coordinator]
	FROM [dbo].[Project] [P]
	INNER JOIN [dbo].[User] AS [U] ON [U].Id = [P].[Id]
	'+ @WhereClause + '
	ORDER BY ' + @OrderClause + '
	OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
		';
 
	 DECLARE @Params NVARCHAR(MAX) = '
		@SearchClause NVARCHAR(258),
		@Coordinator INT,
		@Skip INT,
		@Take INT
	';
 
	EXECUTE sp_executesql @Sql, @Params,
		@SearchClause,
		@Coordinator,
		@Skip,
		@Take
END