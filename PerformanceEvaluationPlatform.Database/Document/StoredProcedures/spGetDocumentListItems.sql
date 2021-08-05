CREATE PROCEDURE [dbo].[sp_GetDocumentListItem]
@Search NVARCHAR(256), 
@UserListIds [dbo].[IntList] READONLY,
@TypeListIds [dbo].[IntList] READONLY,
@ValidUpToDate DATETIME2,
@NameSortOrder INT,
@TypeSortOrder INT,
@Skip INT,
@Take INT

AS
BEGIN
DECLARE @SearchWildcard NVARCHAR(MAX) = '%' + @Search + '%'
DECLARE @WhereClause NVARCHAR(MAX)=''
DECLARE @JoinClause NVARCHAR(MAX)=''
DECLARE @OrderClause NVARCHAR(MAX)=''


IF (@Search IS NOT NULL)
	BEGIN
			IF (@WhereClause ='')
				SET @WhereClause='WHERE '
			ELSE
				SET @WhereClause=@WhereClause+' AND '
		SET @WhereClause= @WhereClause+'[D].[FileName] LIKE '''+@SearchWildcard+''' '
	END
IF (EXISTS ( SELECT * FROM @UserListIds))
	BEGIN
		SET @JoinClause=@JoinClause+ ' INNER JOIN @UserListIds ULI ON [ULI].[ID]=[D].[UserId] '
	END
IF (EXISTS ( SELECT * FROM @TypeListIds))
	BEGIN
		SET @JoinClause=@JoinClause+ ' INNER JOIN @TypeListIds TLI ON [TLI].[ID]=[D].[TypeId] '
	END
IF (@ValidUpToDate IS NOT NULL)
	BEGIN
		IF (@WhereClause = '')
			SET @WhereClause = 'WHERE '
		 ELSE
			SET @WhereClause = @WhereClause + ' AND '
 
		SET @WhereClause = @WhereClause + ' [D].[ValidToDate] < @ValidUpToDate '
	END
IF(@NameSortOrder IS NOT NULL)
	BEGIN
		IF(@NameSortOrder=1)
			SET @OrderClause=' [U].[FirstName] ASC , [U].[LastName] ASC '
		ELSE
			SET @OrderClause=' [U].[FirstName] DESC, [U].[LastName] DESC '
	END
IF(@TypeSortOrder IS NOT NULL)
	BEGIN
		IF(@TypeSortOrder=1)
			SET @OrderClause=' [DT].[Name] ASC '
		ELSE
			SET @OrderClause=' [DT].[Name] DESC '
	END
IF(@OrderClause='')
	SET @OrderClause=' [U].[FirstName] ASC , [U].[LastName] ASC '
	
DECLARE @SqlQuery NVARCHAR(MAX)='
SELECT 
	[D].[Id],
	[U].[FirstName],
	[U].[LastName],
	[DT].[Name] AS [DocumentTypeName],
	[D].[ValidToDate],
	[D].[FileName] 

FROM [dbo].[Document] AS [D]

	INNER JOIN [dbo].[User] [U] ON [U].[Id]=[D].[UserId]
	INNER JOIN [dbo].[DocumentType] [DT] ON [DT].Id=[D].[TypeId] '
+ @JoinClause + '
' + @WhereClause +'
ORDER BY ' + @OrderClause + '
OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
';

DECLARE @Params NVARCHAR(MAX) = '
		@Search NVARCHAR(256),
		@UserListIds [dbo].[IntList] READONLY,
		@TypeListIds [dbo].[IntList] READONLY,
		@ValidUpToDate DATETIME2,
		@NameSortOrder INT,
		@TypeSortOrder INT,
		@Skip INT,
		@Take INT
	';
 
	EXECUTE sp_executesql @SqlQuery, @Params,
		@Search , 
		@UserListIds ,
		@TypeListIds ,
		@ValidUpToDate ,
		@NameSortOrder ,
		@TypeSortOrder ,
		@Skip ,
		@Take 

END