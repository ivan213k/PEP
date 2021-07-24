﻿CREATE PROCEDURE [dbo].[spGetTeamListItems]
@Search NVARCHAR(256),
@ProjectIds [dbo].[IntList] READONLY,
@TitleSortOrder INT,
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
	
	IF (@OrderClause = '')
		SET @OrderClause = '[T].[Title] ASC'
	
	DECLARE @Sql NVARCHAR(MAX) = '
	SELECT 
		[T].[Id],
		[T].[Title] AS [TeamTitle],
		[P].[Title] AS [ProjectTitle],
		COUNT([U].[Id]) AS [TeamSize],
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
	+ @WhereClause + ' GROUP BY [T].[Id], [T].[Title], [P].[Title] ' + '
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