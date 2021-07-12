/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

:r .\Example\PostDeployment\01.ExampleTypes.sql
:r .\Example\PostDeployment\02.ExampleStates.sql
:r .\Example\PostDeployment\03.Examples.sql

:r .\Survey\PostDeployment\01.SurveyStates.sql
:r .\Survey\PostDeployment\02.Levels.sql
:r .\Survey\PostDeployment\03.Surveys.sql

:r .\FormData\PostDeployment\01.FormDataStates.sql
:r .\FormData\PostDeployment\02.FormData.sql