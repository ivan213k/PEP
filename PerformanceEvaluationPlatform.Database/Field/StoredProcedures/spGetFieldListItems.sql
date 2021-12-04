CREATE PROCEDURE [dbo].[spGetFieldListItems]
@Search NVARCHAR(256),
@AssesmentGroupIds [dbo].[IntList] READONLY,
@TypeIds [dbo].[IntList] READONLY,
@TitleSortOrder INT,
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
 
		SET @WhereClause = @WhereClause + '[F].[Name] LIKE ''' + @SearchClause + ''' '
	END
 
	IF (EXISTS ( SELECT * FROM @AssesmentGroupIds))
	BEGIN
		IF (@WhereClause = '')
			SET @WhereClause = 'WHERE '
		ELSE
			SET @WhereClause = @WhereClause + ' AND '

		SET @WhereClause = @WhereClause + ' [F].[AssesmentGroupId] IN (SELECT [Id] FROM @AssesmentGroupIds) '
	END
 
	IF (EXISTS ( SELECT * FROM @TypeIds))
	BEGIN
		IF (@WhereClause = '')
			SET @WhereClause = 'WHERE '
		ELSE
			SET @WhereClause = @WhereClause + ' AND '

		SET @WhereClause = @WhereClause + ' [F].[FieldTypeId] IN (SELECT [Id] FROM @TypeIds) '
	END

	IF (@TitleSortOrder IS NOT NULL)
	BEGIN
		IF (@TitleSortOrder = 1)
			SET @OrderClause = '[F].[Name] ASC'
		ELSE
			SET @OrderClause = '[F].[Name] DESC'
	END


	IF (@OrderClause = '')
		SET @OrderClause = '[F].[Name] ASC'

	DECLARE @Sql NVARCHAR(MAX) = '
	SELECT 
		[F].[Id],
		[F].[Name],
		[F].[IsRequired],
		[FT].[Name] AS [Type],
		[FT].[Id] AS [TypeId],
		[AG].[Name] AS [AssesmentGroup],
		[AG].[Id] AS [AssesmentGroupId]
	FROM [dbo].[Field] [F]
		INNER JOIN [dbo].[AssesmentGroup] [AG] ON [AG].[Id] = [F].[AssesmentGroupId]
		INNER JOIN [dbo].[FieldType] [FT] ON [FT].[Id] = [F].[FieldTypeId]

	'+ @WhereClause + '
	ORDER BY ' + @OrderClause + '
	OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
	';

	SET @Sql = @Sql + ' SELECT COUNT(*) AS [TotalItemsCount] FROM [dbo].[Field] [F]
	'+ @WhereClause ;

	 DECLARE @Params NVARCHAR(MAX) = '
		@SearchClause NVARCHAR(256),
		@AssesmentGroupIds[dbo].[IntList] READONLY,
		@TypeIds [dbo].[IntList] READONLY,
		@Skip INT,
		@Take INT
	';
 
	EXECUTE sp_executesql @Sql, @Params,
		@SearchClause,
		@AssesmentGroupIds,
		@TypeIds,
		@Skip,
		@Take
END