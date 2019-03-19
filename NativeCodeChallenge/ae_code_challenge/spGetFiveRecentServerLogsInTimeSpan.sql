CREATE PROCEDURE spGetFiveRecentServerLogsInTimeSpan @startTime DATETIME, @endTime DATETIME
AS
BEGIN
SELECT TOP 5 server_response_log.ResponseText, server_response_log.StartTime FROM server_response_log WHERE server_response_log.StartTime >= @startTime AND server_response_log.StartTime >= @startTime
ORDER BY Id DESC

END
