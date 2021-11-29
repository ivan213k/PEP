CREATE PROCEDURE [dbo].[spGetProjectListItems]
@Search NVARCHAR(256),
@CoordinatorIds [dbo].[IntList] READONLY,
@TitleSortOrder INT,
@StartDateSortOrder INT,
@CoordinatorSortOrder INT,
@Skip INT,
@Take INT
AS
BEGIN
	DECLARE @SearchClause NVARCHAR(258) = ''
	DECLARE @WhereClause NVARCHAR(MAX) = ''
	DECLARE @OrderClause NVARCHAR(MAX) = ''
	DECLARE @JoinClause NVARCHAR(MAX) = ''
 
	IF (@Search IS NOT NULL)
	BEGIN
		SET @SearchClause = '%' + @Search + '%';
		IF (@WhereClause = '')
			SET @WhereClause = 'WHERE '
		ELSE
			SET @WhereClause = @WhereClause + ' AND '
 
		SET @WhereClause = @WhereClause + '[P].[Title] LIKE ''' + @SearchClause + ''' '
	END
	
	IF(EXISTS(SELECT * FROM @CoordinatorIds))
		BEGIN
			SET @JoinClause = @JoinClause + ' INNER JOIN @CoordinatorIds [CI] ON [CI].[Id] = [P].[CoordinatorId] '
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
			IF (@StartDateSortOrder = 1)
				SET @OrderClause = '[P].[StartDate] ASC'
			ELSE
				SET @OrderClause = '[P].[StartDate] DESC'
		END

	IF (@CoordinatorSortOrder IS NOT NULL)
		BEGIN
			IF (@CoordinatorSortOrder = 1)
				SET @OrderClause = '[PC].[FirstName] + '' '' + [PC].[LastName] ASC'
			ELSE
				SET @OrderClause = '[PC].[FirstName] + '' '' + [PC].[LastName] DESC'
		END
	IF (@OrderClause = '')
		SET @OrderClause = '[P].[Title] ASC'

	DECLARE @Sql NVARCHAR(MAX) = '
	SELECT
		[P].[Id],
		[P].[Title],
		[P].[StartDate],
		[PC].[FirstName] AS [CoordinatorFirstName],
		[PC].[LastName] AS [CoordinatorLastName],
		[P].[CoordinatorId]
	FROM [dbo].[Project] [P]
	INNER JOIN [dbo].[User] [PC] ON [P].[CoordinatorId] = [PC].[Id]
	'+ @JoinClause + ' 
	'+ @WhereClause + '
	ORDER BY ' + @OrderClause + '
	OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
		';
 
	 DECLARE @Params NVARCHAR(MAX) = '
		@SearchClause NVARCHAR(258),
		@CoordinatorIds [dbo].[IntList] READONLY,
		@StartDateSortOrder INT,
		@Skip INT,
		@Take INT
	';
	CREATE TABLE #Temp
	(
		[Id] INT,
		[Title] NVARCHAR(MAX),
		[StartDate] DATETIME2,
		[FirstName] NVARCHAR(MAX),
		[LastName] NVARCHAR(MAX),
		[CoordinatorId] INT
	)

	INSERT INTO #Temp
	EXECUTE sp_executesql @Sql, @Params,
		@SearchClause,
		@CoordinatorIds,
		@StartDateSortOrder,
		@Skip,
		@Take

	SELECT  [Id],
			[Title],
			[StartDate],
			[FirstName] AS [CoordinatorFirstName],
			[LastName] AS [CoordinatorLastName],
			[CoordinatorId]
	FROM #Temp
	
	SELECT COUNT(*) AS [TotalItemsCount] FROM #Temp

	DROP TABLE #Temp
END