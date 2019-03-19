using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace DataAccess
{
        public static class DbContextExtensions
        {

            public static IEnumerable<dynamic> CollectionFromSql(this DbContext dbContext,
                                                                 string sql,
                                                                 params DbParameter[] parameters)
            {
                using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = sql;
                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();

                    foreach (var param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }

                    var retObject = new List<dynamic>();
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var dataRow = new ExpandoObject() as IDictionary<string, object>;
                            for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                                dataRow.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);

                            retObject.Add((ExpandoObject)dataRow);
                        }
                    }

                    return retObject;
                }
            }
        }
    }
