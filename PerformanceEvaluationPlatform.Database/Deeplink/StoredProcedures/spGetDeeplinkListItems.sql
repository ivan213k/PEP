CREATE PROCEDURE [dbo].[spGetDeeplinkListItems]
@Search NVARCHAR(256),
@SentToId INT,
@StateIds [dbo].[IntList] READONLY,
@ExpiresAtFrom DateTime2,
@ExpiresAtTo DateTime2,
@SentToOrder INT,
@ExpiresAtOrder  INT,
@Skip int,
@Take int
AS
BEGIN
	DECLARE @SearchClause NVARCHAR(258) = ''
	DECLARE @WhereClause NVARCHAR(MAX) = ''
	DECLARE @JoinClause NVARCHAR(MAX) = ''
	DECLARE @OrderClause NVARCHAR(MAX) = ''
 
	IF (@Search IS NOT NULL)	
	BEGIN
		SET @SearchClause = '%' + @Search + '%';
		IF (@WhereClause = '')
			SET @WhereClause = 'WHERE '
		ELSE
			SET @WhereClause = @WhereClause + ' AND '
 
		SET @WhereClause = @WhereClause + '([U].[FirstName] + '' '' + [U].[LastName] LIKE ''' + @SearchClause + ''' OR [FT].[Name] LIKE '''  + @SearchClause +  ''')'
	END


	IF(@ExpiresAtFrom IS NOT NULL)
	BEGIN
		IF(@WhereClause = '')
			SET @WhereClause = 'WHERE'
		ELSE
			SET @WhereClause = @WhereClause + ' AND '

		SET @WhereClause = @WhereClause + '[D].[ExpireDate] >= @ExpiresAtFrom'
	END

	IF(@ExpiresAtTo IS NOT NULL)
	BEGIN
		IF(@WhereClause = '')
			SET @WhereClause = 'WHERE'
		ELSE
			SET @WhereClause = @WhereClause + ' AND '

		SET @WhereClause = @WhereClause + '[D].[ExpireDate] <= @ExpiresAtTo'
	END

	IF (@SentToId IS NOT NULL)
	BEGIN
		 IF (@WhereClause = '')
			SET @WhereClause = 'WHERE '
		 ELSE
			SET @WhereClause = @WhereClause + ' AND '
 
		SET @WhereClause = @WhereClause + '[U].[Id] = @SentToId'
	END
 

	IF (EXISTS ( SELECT * FROM @StateIds))
	BEGIN
		SET @JoinClause = @JoinClause + ' INNER JOIN @StateIds [SI] ON [SI].[Id] = [D].[StateId] '
	END
	
	
	IF(@SentToOrder IS NOT NULL)
	BEGIN
		IF(@SentToOrder = 1)
			SET @OrderClause = '[U].[FirstName] + '' '' + [U].[LastName] ASC'
		ELSE
			SET @OrderClause = '[U].[FirstName] + '' '' + [U].[LastName] DESC'
	END


	IF(@ExpiresAtOrder IS NOT NULL)
	BEGIN
		IF(@ExpiresAtOrder = 1)
			SET @OrderClause = '[D].[ExpireDate] ASC'
		ELSE
			SET @OrderClause = '[D].[ExpireDate] DESC'
	END

	IF(@OrderClause = '')
		SET @OrderClause =  '[U].[FirstName] + '' '' + [U].[LastName] ASC'

	IF(@Skip IS NULL)
		SET @Skip = 0

	IF(@Take IS NULL)
		SET @Take = 30


	DECLARE @Sql NVARCHAR(MAX) = '
	SELECT 
		[U].[FirstName] AS SentToFirstName,
		[U].[LastName] AS SentToLastName,
		[D].[ExpireDate],
		[DS].[Name] AS State,
		[FT].[Name] AS FormTemplate

	FROM [dbo].[Deeplink] [D]
	INNER JOIN [dbo].[User] [U] ON [D].[UserId] = [U].[Id]
	INNER JOIN [dbo].[Survey] [S] ON [S].[Id] = [D].[SurveyId]
	INNER JOIN [dbo].[FormTemplate] [FT] ON [FT].[Id] = [S].[FormTemplateId]
	INNER JOIN [dbo].[DeeplinkState] [DS] ON [D].[StateId] = [DS].[ID]
	' 
	+ @JoinClause + 
	' '+ @WhereClause + ''
	+ ' ORDER BY ' + @OrderClause  +
	' OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY' ;
 
	DECLARE @Params NVARCHAR(MAX) = '
		@SearchClause NVARCHAR(256),
		@SentToId INT,
		@StateIds [dbo].[IntList] READONLY,
		@ExpiresAtFrom DateTime2,
		@ExpiresAtTo DateTime2,
		@Skip INT,
		@Take INT
	';
	EXECUTE sp_executesql @Sql, @Params,
		@SearchClause,
		@SentToId,
		@StateIds ,
		@ExpiresAtFrom,
		@ExpiresAtTo,
		@Skip,
		@Take
END
