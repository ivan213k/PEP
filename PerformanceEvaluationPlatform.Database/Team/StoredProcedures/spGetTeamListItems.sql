CREATE PROCEDURE [dbo].[spGetTeamListItems]
@Search NVARCHAR(256),
@ProjectIds [dbo].[IntList] READONLY,
@TitleSortOrder INT,
@ProjectTitleSortOrder INT,
@TeamSizeSortOrder INT,
@Skip INT,
@Take INT
AS
BEGIN
	DECLARE @SearchClause NVARCHAR(258) = ''
	DECLARE @WhereClause NVARCHAR(MAX) = ''
	DECLARE @JoinClause NVARCHAR(MAX) = ''
	DECLARE @OrderClause NVARCHAR(MAX) = ''
	
	IF (@Search IS NOT NULL)
	BEGIN
	 SET @SearchClause = '%' + @Search + '%';
	 IF (@WhereClause != '')
		SET @WhereClause = 'WHERE '
	 ELSE 
		SET @WhereClause = @WhereClause + ' AND '
	 SET @WhereClause = @WhereClause + '[T].[Title] LIKE ''' + @SearchClause + ''' '
	END
	
	IF (EXISTS (SELECT * FROM @ProjectIds))
	BEGIN
		SET @JoinClause = @JoinClause + ' INNER JOIN @ProjectIds [PI] ON [PI].[Id] = [T].[ProjectId] '
	END
	
	IF (@TitleSortOrder IS NOT NULL)
	BEGIN
		IF (@TitleSortOrder = 1)
			SET @OrderClause = '[T].[Title] ASC'
		ELSE
			SET @OrderClause = '[T].[Title] DESC'
	END

	IF (@ProjectTitleSortOrder IS NOT NULL)
	BEGIN
		IF (@ProjectTitleSortOrder = 1)
			SET @OrderClause = '[P].[Title] ASC'
		ELSE
			SET @OrderClause = '[P].[Title] DESC'
	END

	IF (@TeamSizeSortOrder IS NOT NULL)
	BEGIN
		IF (@TeamSizeSortOrder = 1)
			SET @OrderClause = 'COUNT([U].[Id]) ASC'
		ELSE
			SET @OrderClause = 'COUNT([U].[Id]) DESC'
	END
	
	IF (@OrderClause = '')
		SET @OrderClause = '[T].[Title] ASC'
	
	DECLARE @Sql NVARCHAR(MAX) = '
	SELECT 
		[T].[Id],
		[T].[Title],
		[P].[Title] AS [ProjectTitle],
		[P].[Id] AS [ProjectId],
		COUNT([U].[Id]) AS [Size],
		(
			SELECT
				[U].[FirstName]
			FROM [dbo].[User] [U] 
				LEFT JOIN [dbo].[UserRoleMap] [URM] ON [URM].[UserId] = [U].[Id]
			WHERE [URM].[RoleId] = 4 AND [U].[TeamId] = [T].[Id]
		) AS [TeamLead]
	FROM [dbo].[Team] [T]
		INNER JOIN [dbo].[Project] [P] ON [P].[Id] = [T].[ProjectId]
		LEFT JOIN [dbo].[User] [U] ON [U].[TeamId] = [T].[Id]
	' 
	+ @JoinClause + ' ' 
	+ @WhereClause + ' GROUP BY [T].[Id], [T].[Title], [P].[Title], [P].[Id] ' + '
	ORDER BY ' + @OrderClause + '
	OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
	';
	
	DECLARE @Params NVARCHAR(MAX) = '
		@SearchClause NVARCHAR(258),
		@ProjectIds [dbo].[IntList] READONLY,
		@Skip INT,
		@Take INT
	';
	
	EXECUTE sp_executesql @Sql, @Params,
		@SearchClause,
		@ProjectIds,
		@Skip,
		@Take
END