﻿CREATE PROCEDURE [dbo].[spGetUserListItems]
@Search NVARCHAR(258),
@StateIds [dbo].[IntList] READONLY,
@RoleIds [dbo].[IntList]READONLY,
@PreviousPeDate DATE,
@NextPeDate DATE,
@UserNameSort INT,
@UserPreviousPE INt,
@UserNextPE INT,
@Skip INT,
@Take INT

AS
Begin
	DECLARE @SearchClause NVARCHAR(258) = ''
	DECLARE @WhereClause NVARCHAR(MAX) = ''
	DECLARE @JoinClause NVARCHAR(MAX) = ''
	DECLARE @OrderClause NVARCHAR(MAX) = ''

	CREATE TABLE #UserNextPeDate ([userId] INT, [NextPe]DATE)
	CREATE TABLE #UserPreviousPeDate ([userId] INT, [PreviousPe]DATE)
	CREATE TABLE #UserPEs ([userId] INT, [PreviousPE] DATE, [NextPE] DATE)

	INSERT INTO #UserNextPeDate ([userId],[NextPE])
	SELECT  
		[U].[Id],
		MAX([S].[AppointmentDate])
	FROM  [dbo].[User] AS[U]
	LEFT JOIN [dbo].[Survey] AS [S] ON [S].[AssigneeId] = [U].[Id] GROUP BY[U].[Id];

	WITH[UserAppointments] 
	AS (SELECT  
			[U].[Id],
			[S].[AppointmentDate], 
			ROW_NUMBER() 
			OVER(PARTITION BY [U].Id ORDER BY[S].[AppointmentDate]DESC)AS [Number]
		FROM [dbo].[User] AS [U]
	LEFT JOIN [dbo].[Survey] AS [S] ON [S].AssigneeId = [U].Id)

	INSERT INTO #UserPreviousPeDate ([userId],[PreviousPe])
	SELECT 
		[UA].[Id], 
		[UA].AppointmentDate 
	FROM [UserAppointments] AS [UA] WHERE [Number] = 2
 
	INSERT  INTO #UserPEs ([userId],[PreviousPE],[NextPE])
	SELECT 
		[UNPD].userId,
		[UPPD].PreviousPe,
		[UNPD].NextPe
	FROM #UserNextPeDate AS [UNPD] LEFT JOIN #UserPreviousPeDate AS [UPPD]ON[UPPD].userId = [UNPD].userId 

 --Search Where Clause
	IF(@Search IS NOT NULL)
	BEGIN
		SET @SearchClause = '%' + @Search + '%'

		IF(@WhereClause ='')
			SET @WhereClause = 'WHERE '

		ELSE
			SET @WhereClause = @WhereClause+' AND '
	
		SET @WhereClause = @WhereClause + 'concat ([U].[FirstName], ' +'[U].[LastName], '+ '[U].[Email])'+ ' LIKE ''' + @SearchClause +''' '

	END

	--Previous PE DATE WHERE CLAUSE
	IF(@PreviousPeDate IS NOT NULL)
	BEGIN

		If(@WhereClause ='')
			SET @WhereClause = 'WHERE '

		ELSE
			SET @WhereClause = @WhereClause+' AND '

	SET @WhereClause = @WhereClause + '[UPES].[PreviousPE] = @PreviousPeDate'

	END

	--NEXT PE DATE WHERE CLAUSE
		IF(@NextPeDate IS NOT NULL)
	BEGIN

		If(@WhereClause ='')
			SET @WhereClause = 'WHERE '

		ELSE
			SET @WhereClause = @WhereClause+' AND '

		SET @WhereClause = @WhereClause + '[UPES].[NextPE] = @NextPeDate'

	END

	--INSERT INTO @StateIds VALUES(1);
	IF(EXISTS(SELECT* FROM @StateIds))
	BEGIN
		SET @JoinClause = @JoinClause + ' INNER JOIN @StateIds AS [SI] ON [SI].Id = [U].[StateId] '
	END

	--INSERT INTO @RoleIds VALUES(1);
	IF(EXISTS(SELECT * FROM @RoleIds))

	BEGIN 
		SET @JoinClause = @JoinClause+ 'INNER JOIN @RoleIds AS [RI] ON [R].[Id] = [RI].[Id]'
	END

	-- UserName Or Email Order  CLause
	IF(@UserNameSort IS NOT NULL)
	BEGIN
		IF(@UserNameSort = 1)
			SET @OrderClause = '[U].[FirstName] ASC'

		Else
			SET @OrderClause = '[U].[FirstName] DESC'

	END

	
	If(@OrderClause  = '')
	Begin
		SET @OrderClause = '[U].[FirstName] ASC'
	End


	IF(@UserNextPE IS NOT NULL)
	BEGIN
		IF(@UserNextPE = 1)
		Set @OrderClause ='[UPES].[NextPE] ASC'

		ELSE
		Set @OrderClause ='[UPES].[NextPE] DESC'
	END

	IF(@UserPreviousPE IS NOT NULL)
	BEGIN
		IF(@UserPreviousPE = 1)
		Set @OrderClause ='[UPES].[PreviousPE] ASC'

		ELSE
		Set @OrderClause ='[UPES].[PreviousPE] DESC'
	END

	IF(@Skip IS NULL)
		SET @Skip = 0
	IF(@Take IS NULL)
		SET @Take = 30

	DECLARE @Sql NVARCHAR(MAX)= 'SELECT DISTINCT [U].[Id],[U].[FirstName],[U].[LastName],[U].[Email],[US].[Name] AS [StateName],
	[T].[Title] as [TeamName], [R].[Title] as [RoleName],[L].[Name] AS [LevelName],[UPES].[PreviousPE], [UPES].[NextPE]
	FROM [dbo].[User] AS [U]
	INNER JOIN [dbo].[UserState] AS [US] ON [U].StateId = [US].[Id] 
	INNER JOIN [dbo].[Team] AS [T] ON [T].Id = [U].[TeamId]
	INNER JOIN [dbo].[UserRoleMap] AS [URM] ON [U].Id = [URM].[UserId]
	INNER JOIN [dbo].[Role] AS [R] ON [URM].[ROleId] = [R].[Id]
	INNER JOIN [dbo].[Level] AS [L] ON [U].[TechnicalLevelId] = [L].[Id]
	INNER JOIN #UserPEs AS [UPES] ON [UPES].userId = [U].[Id]
	'+@JoinClause+'
	' + @WhereClause +'
	ORDER BY ' + @OrderClause + '
	' + ' OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY';

	DECLARE @Params NVARCHAR(Max) ='
	@StateIds [dbo].[IntList] READONLY,
	@RoleIds [dbo].[IntList] READONLY,
	@PreviousPeDate date,
	@NextPeDate date,
	@Skip INT,
	@Take INT
	'
	EXECUTE sp_executesql @Sql,@Params,
	@StateIds,
	@RoleIds,
	@PreviousPeDate,
	@NextPeDate,
	@Skip,
	@Take
End