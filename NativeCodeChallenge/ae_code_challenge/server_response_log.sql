CREATE TABLE [dbo].server_response_log
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [StartTime] DATETIME NOT NULL, 
    [EndTime] DATETIME NULL, 
    [HttpStatusCode] INT NULL, 
    [ResponseText] VARCHAR(MAX) NULL, 
    [Staus] SMALLINT NULL
)

GO
