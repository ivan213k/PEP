CREATE PROCEDURE [dbo].[spGetExampleListItems]
@Search NVARCHAR(256),
@StateId INT,
@TypeIds [dbo].[IntList] READONLY,
@TitleSortOrder INT,
@Skip INT,
@Take INT
AS
BEGIN
	-- local variables
	DECLARE @SearchClause NVARCHAR(258) = ''
	DECLARE @WhereClause NVARCHAR(MAX) = ''
	DECLARE @JoinClause NVARCHAR(MAX) = ''
	DECLARE @OrderClause NVARCHAR(MAX) = ''
 
	 -- query builder
	IF (@Search IS NOT NULL)
	BEGIN
		SET @SearchClause = '%' + @Search + '%';
		IF (@WhereClause = '')
			SET @WhereClause = 'WHERE '
		ELSE
			SET @WhereClause = @WhereClause + ' AND '
 
		SET @WhereClause = @WhereClause + '[E].[Title] LIKE ''' + @SearchClause + ''' '
	END
 
	IF (@StateId IS NOT NULL)
	BEGIN
		 IF (@WhereClause = '')
			SET @WhereClause = 'WHERE '
		 ELSE
			SET @WhereClause = @WhereClause + ' AND '
 
		SET @WhereClause = @WhereClause + '[E].[ExampleStateId] = @StateId'
	END
 
	IF (EXISTS ( SELECT * FROM @TypeIds))
	BEGIN
		SET @JoinClause = @JoinClause + ' INNER JOIN @TypeIds [TI] ON [TI].[Id] = [E].[ExampleTypeId] '
	END


	IF (@TitleSortOrder IS NOT NULL)
	BEGIN
		IF (@TitleSortOrder = 1)
			SET @OrderClause = '[E].[Title] ASC'
		ELSE
			SET @OrderClause = '[E].[Title] DESC'
	END


	IF (@OrderClause = '')
		SET @OrderClause = '[E].[Title] ASC'

	DECLARE @Sql NVARCHAR(MAX) = '
	SELECT 
		[E].[Id],
		[E].[Title],
		[ET].[Name] AS [TypeName],
		[ES].[Name] AS [StateName]
	FROM [dbo].[Example] [E]
		INNER JOIN [dbo].[ExampleType] [ET] ON [ET].[Id] = [E].[ExampleTypeId]
		INNER JOIN [dbo].[ExampleState] [ES] ON [ES].[Id] = [E].[ExampleStateId]
		'+ @JoinClause + 
	' 
	'+ @WhereClause + '
	ORDER BY ' + @OrderClause + '
	OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
	';
 
	 DECLARE @Params NVARCHAR(MAX) = '
		@SearchClause NVARCHAR(256),
		@StateId INT,
		@TypeIds [dbo].[IntList] READONLY,
		@Skip INT,
		@Take INT
	';
 
	EXECUTE sp_executesql @Sql, @Params,
		@SearchClause,
		@StateId,
		@TypeIds,
		@Skip,
		@Take
END