CREATE PROCEDURE [dbo].[sp_GetDocumentDetailById]
@Id INT
AS
BEGIN
SELECT [D].[Id],
[U].[FirstName],
[U].[LastName],
[DT].[Name] AS [DocumentType],
[D].[FileName],
[D].[ValidToDate],
[U1].[FirstName]AS [CreatedByFirstName],
[U1].[LastName] AS [CreatedByLastName],
[D].[CreatedAt],
[U2].[FirstName]AS [LastUpdatesByFirstName],
[U2].[LastName] AS [LastUpdatesByLastName],
[D].[LastUpdatesAt]

FROM [dbo].Document AS [D]
INNER JOIN [dbo].[User]  [U] ON [U].[Id]=[D].[UserId]
INNER JOIN [dbo].[DocumentType]  [DT] ON [DT].Id=[D].[TypeId]
LEFT JOIN [dbo].[User]  [U1] ON [U1].[Id]=[D].[CreatedById]
LEFT JOIN [dbo].[User]  [U2] ON [U2].[Id]=[D].[LastUpdatesById]
WHERE [D].Id=@Id
END