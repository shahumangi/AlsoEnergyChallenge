CREATE NONCLUSTERED INDEX [server_response_log_ID_StartTime__ResponseText_HttpStatusCode] ON [dbo].[server_response_log]
(
	[Id] ASC,
	[StartTime] ASC
)
INCLUDE ( 	[HttpStatusCode],
	[ResponseText]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
