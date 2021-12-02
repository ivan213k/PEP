CREATE PROCEDURE [dbo].[spGetFormTemplatesListItems]
@Search NVARCHAR(256),
@StatusIds [dbo].[IntList] READONLY,
@NameSortOrder INT,
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
		IF(@Search <> '')
		BEGIN
			SET @SearchClause = '%' + @Search + '%'
			IF(@WhereClause = '')
				SET @WhereClause = 'WHERE '
			ELSE
				SET @WhereClause = @WhereClause + ' AND '
			SET @WhereClause = @WhereClause + '[FT].[Name] LIKE ''' + @SearchClause + ''' '  
		END
	END

	IF (EXISTS (SELECT * FROM @StatusIds))
	BEGIN
		SET @JoinClause = @JoinClause + ' INNER JOIN @StatusIds [SI] ON [SI].[Id] = [FT].[StatusId] '
	END

	IF (@NameSortOrder IS NOT NULL)
	BEGIN
		IF (@NameSortOrder = 1)
			SET @OrderClause = '[FT].[Name] ASC'
		ELSE
			SET @OrderClause = '[FT].[Name] DESC'
	END


	IF (@OrderClause = '')
		SET @OrderClause = '[FT].[Name] ASC'

	DECLARE @Sql NVARCHAR(MAX) = '
		SELECT DISTINCT
		[FT].[Id],
		[FT].[Name],
		[FT].[Version],
		[FT].[StatusId],
		[FTS].[Name] AS [StatusName],
		[FT].[CreatedAt]
		FROM [dbo].[FormTemplate] [FT]
		INNER JOIN [dbo].[FormTemplateStatus] [FTS] ON [FTS].[Id] = [FT].[StatusId]
		'+ @JoinClause +'
		'+ @WhereClause +'
		ORDER BY ' + @OrderClause + '
		OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
	';

	SET @Sql = @Sql + ' SELECT COUNT(*) AS [TotalItemsCount] FROM [dbo].[FormTemplate] [FT]
	'+ @JoinClause + '
	'+ @WhereClause ;

	DECLARE @Params NVARCHAR(MAX) = '
		@Search NVARCHAR(256),
		@StatusIds [dbo].[IntList] READONLY,
		@NameSortOrder INT,
		@Skip INT,
		@Take INT
	';

	EXECUTE sp_executesql @Sql, @Params,
		@Search,
		@StatusIds,
		@NameSortOrder,
		@Skip,
		@Take
END