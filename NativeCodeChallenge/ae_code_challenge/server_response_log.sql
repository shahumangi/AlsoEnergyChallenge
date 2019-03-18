CREATE TABLE [dbo].server_response_log
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [StartTime] DATETIME NOT NULL, 
    [EndTime] DATETIME NULL, 
    [HttpStatusCode] INT NULL, 
    [ResponseText] VARCHAR(MAX) NULL, 
    [ErrorCode] SMALLINT NULL
)

GO
