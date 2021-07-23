CREATE PROCEDURE [dbo].[spGetFieldGroupListItems]
@Search NVARCHAR(256),
@TitleSetOrder INT,
@FieldCountSetOrder INT,
@Skip INT,
@Take INT,
@CountFrom INT,
@CountTo INT,
@IsNotEmptyOnly INT
AS
BEGIN
	DECLARE @WhereClause NVARCHAR (MAX) = ''
	DECLARE @SearchClause NVARCHAR(258) = ''
	DECLARE @OrderClause NVARCHAR (MAX) = ''
	DECLARE @HavingClause NVARCHAR (MAX) = ''

	IF (@Search IS NOT NULL)
BEGIN
	SET @SearchClause = '%' + @Search + '%'
	IF (@WhereClause = '')
		SET @WhereClause = 'WHERE '
	ELSE
		SET @WhereClause = @WhereClause + ' AND '

	SET @WhereClause = @WhereClause + '[Fg].[Title] LIKE ''' + @SearchClause + ''' '
END

IF (@CountFrom IS NOT NULL)
BEGIN
	IF (@HavingClause = '')
		SET @HavingClause = 'HAVING '
	ELSE
		SET @HavingClause = @HavingClause + ' AND '

	SET @HavingClause = @HavingClause + 'COUNT([F].[FieldGroupId]) >= @CountFrom '
END

IF (@CountTo IS NOT NULL)
BEGIN
	IF (@HavingClause = '')
		SET @HavingClause = 'HAVING '
	ELSE
		SET @HavingClause = @HavingClause + ' AND '

	SET @HavingClause = @HavingClause + 'COUNT([F].[FieldGroupId]) <= @CountTo '
END

IF (@IsNotEmptyOnly IS NOT NULL)
BEGIN
	IF (@HavingClause = '')
		SET @HavingClause = 'HAVING '
	ELSE
		SET @HavingClause = @HavingClause + ' AND '

	IF (@IsNotEmptyOnly = 1)
		SET @HavingClause = @HavingClause + 'COUNT([F].[FieldGroupId]) > 0'
END

IF (@TitleSetOrder IS NOT NULL)
BEGIN
	IF (@OrderClause != '')
		SET @OrderClause = @OrderClause + ', '

	IF (@TitleSetOrder = 1) 
		SET @OrderClause = @OrderClause + '[Fg].[Title] ASC'
	ELSE 
		SET @OrderClause = @OrderClause + '[Fg].[Title] DESC'
END

IF (@FieldCountSetOrder IS NOT NULL)
BEGIN
	IF (@OrderClause != '')
		SET @OrderClause = @OrderClause + ', '

	IF (@FieldCountSetOrder = 1) 
		SET @OrderClause = @OrderClause + 'COUNT([F].[FieldGroupId]) ASC'
	ELSE 
		SET @OrderClause = @OrderClause + 'COUNT([F].[FieldGroupId]) DESC'
END

IF (@OrderClause = '')
	SET @OrderClause = '[Fg].[Title] ASC, COUNT([F].[FieldGroupId]) ASC'

DECLARE @Sql NVARCHAR(MAX) ='
SELECT 
	[Fg].[Id],
	[Fg].[Title],
	COUNT([F].[FieldGroupId]) As [Count]
FROM [dbo].[FieldGroup] [Fg]
	LEFT JOIN [dbo].[Field] [F] ON [F].FieldGroupId = [Fg].Id
'+ @WhereClause +'
GROUP BY [Fg].[Id], [Fg].[Title]
'+ @HavingClause +'
ORDER BY ' + @OrderClause +'
OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
'

print @Sql

DECLARE @Params NVARCHAR(MAX) = '
	@OrderClause NVARCHAR(MAX),
	@SearchClause NVARCHAR(258),
	@WhereClause NVARCHAR(MAX),
	@HavingClause NVARCHAR(MAX),
	@Skip INT,
	@Take INT,
	@CountTo INT,
	@CountFrom INT
'
EXECUTE sp_executesql @Sql, @Params,
	@OrderClause,
	@SearchClause,
	@WhereClause,
	@HavingClause,
	@Skip,
	@Take,
	@CountTo,
	@CountFrom
END
