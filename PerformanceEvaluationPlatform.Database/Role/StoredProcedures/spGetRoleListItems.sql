CREATE PROCEDURE [dbo].[spGetRoleListItems]
@Search NVARCHAR(256),
@IsPrimary BIT,
@UsersCountFrom INT,
@UsersCountTo INT,
@TitleSortOrder INT,
@IsPrimarySortOrder INT,
@Skip INT,
@Take INT
AS
BEGIN
	DECLARE @SearchClause NVARCHAR(258) = ''
	DECLARE @WhereClause NVARCHAR(MAX) = ''
	DECLARE @HavingClause NVARCHAR(MAX) = ''
	DECLARE @OrderClause NVARCHAR(MAX) = ''
 
	IF (@Search IS NOT NULL)
	BEGIN
		SET @SearchClause = '%' + @Search + '%';
		IF (@WhereClause = '')
			SET @WhereClause = 'WHERE '
		ELSE
			SET @WhereClause = @WhereClause + ' AND '
 
		SET @WhereClause = @WhereClause + '[R].[Title] LIKE ''' + @SearchClause + ''' '
	END

	IF (@IsPrimary IS NOT NULL)
	BEGIN
		 IF (@WhereClause = '')
			SET @WhereClause = 'WHERE '
		 ELSE
			SET @WhereClause = @WhereClause + ' AND '
 
		SET @WhereClause = @WhereClause + '[R].[IsPrimary] = @IsPrimary'
	END

	IF (@UsersCountFrom IS NOT NULL)
	BEGIN
		 IF (@HavingClause = '')
			SET @HavingClause = 'HAVING '
		 ELSE
			SET @HavingClause = @HavingClause + ' AND '
 
		SET @HavingClause = @HavingClause + ' COUNT([U].RoleId) >= @UsersCountFrom'
	END

	IF (@UsersCountTo IS NOT NULL)
	BEGIN
		 IF (@HavingClause = '')
			SET @HavingClause = 'HAVING '
		 ELSE
			SET @HavingClause = @HavingClause + ' AND '
 
		SET @HavingClause = @HavingClause + ' COUNT([U].RoleId) <= @UsersCountTo'
	END
	
	IF (@TitleSortOrder IS NOT NULL)
		BEGIN
			IF (@TitleSortOrder = 1)
				SET @OrderClause = '[R].[Title] ASC'
			ELSE
				SET @OrderClause = '[R].[Title] DESC'
		END

	IF (@IsPrimarySortOrder IS NOT NULL)
		BEGIN
			IF (@IsPrimarySortOrder = 1)
				SET @OrderClause = '[R].[IsPrimary] ASC'
			ELSE
				SET @OrderClause = '[R].[IsPrimary] DESC'
		END
	IF (@OrderClause = '')
		SET @OrderClause = '[R].[Title] ASC'

	DECLARE @Sql NVARCHAR(MAX) = '
	SELECT
		[R].[Id],
		[R].Title,
		[R].IsPrimary,
	COUNT([U].RoleId) AS [UsersCount]
	FROM [dbo].[Role] [R]
	LEFT JOIN [dbo].[UserRoleMap] [U] ON [U].[RoleId] = [R].[Id]
	'+ @WhereClause + '
	GROUP BY [R].[Id], [R].Title, [R].IsPrimary
	'+ @HavingClause + '
	ORDER BY ' + @OrderClause + '
	OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
		';
 
	 DECLARE @Params NVARCHAR(MAX) = '
		@SearchClause NVARCHAR(258),
		@IsPrimary BIT,
		@UsersCountFrom INT,
		@UsersCountTo INT,
		@Skip INT,
		@Take INT
	';
 
	EXECUTE sp_executesql @Sql, @Params,
		@SearchClause,
		@IsPrimary,
		@UsersCountFrom,
		@UsersCountTo,
		@Skip,
		@Take
END