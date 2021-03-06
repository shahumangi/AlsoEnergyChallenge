﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class ServerResponseLogRepository: IServerResponseLogRepository
    {
        AlsoEnergyContext _alsoEnergyContext;
        public ServerResponseLogRepository(AlsoEnergyContext context)
        {
            _alsoEnergyContext = context;
        }

        public void Save(Server_Response_Log server_Response_Log)
        {
            if (_alsoEnergyContext.Server_Response_Logs.Contains(server_Response_Log))
            {
                _alsoEnergyContext.Server_Response_Logs.Update(server_Response_Log);
            }
            else
                _alsoEnergyContext.Server_Response_Logs.Add(server_Response_Log);
            _alsoEnergyContext.SaveChanges();
        }

        public List<dynamic> GetFiveRecentServerLogsInTimeSpan(DateTime startDateTime, DateTime endDateTime)
        {
            IEnumerable<dynamic> list = _alsoEnergyContext.CollectionFromSql("EXEC	[dbo].[spGetFiveRecentServerLogsInTimeSpan] @startTime, @endTime ", new SqlParameter("startTime",startDateTime), new SqlParameter("endTime", endDateTime));
            var returnList = list.ToList();
            return returnList;
        }

        public Dictionary<int,IEnumerable<KeyValuePair<int,int>>> GetErrorCodeAndCount(DateTime dateTime)
        {
           var list =  _alsoEnergyContext.Server_Response_Logs.Where( l => l.StartTime.Date == dateTime.Date).GroupBy(l =>  l.StartTime.Hour ).Select(group => new KeyValuePair<int,IEnumerable<KeyValuePair<int,int>>>(group.Key,group.GroupBy(log => log.HttpStatusCode).Select(group2 => new KeyValuePair<int, int>(group2.Key, group2.Count()))));
           return list.ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }

    public interface IServerResponseLogRepository
    {
        void Save(Server_Response_Log server_Response_Log);
        List<dynamic> GetFiveRecentServerLogsInTimeSpan(DateTime startDateTime, DateTime endDateTime);
        Dictionary<int, IEnumerable<KeyValuePair<int, int>>> GetErrorCodeAndCount(DateTime dateTime);

    }
}
