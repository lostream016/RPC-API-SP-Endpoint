using RPC_API_SP_EndPoint.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RPC_API_SP_EndPoint.Helper
{
    public class SPExecutor
    {
        public static bool ExecuteSp(String sp_ext, NameValueCollection param, out string query_result)
        {
            try
            {
                var con_string = ConfigurationManager.ConnectionStrings["CorporationEntities"].ToString();
                var builder = new EntityConnectionStringBuilder(con_string);
                var regularConnectionString = builder.ProviderConnectionString;

                using (CorporationEntities db = new CorporationEntities())
                {
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    if (param != null) // parameter request
                    {

                        for (int i = 0; i < param.Count; i++)
                        {
                            string key, value;
                            key = param.GetKey(i);
                            value = param[key];
                            SqlParameter newParam = new SqlParameter("@" + key, SqlDbType.VarChar) { Value = value };

                            parameters.Add(newParam);
                        }

                        string listParam = String.Join(", ", parameters.Select(item => item.ParameterName));

                        string sqlQuery = "SET NOCOUNT ON\n";
                        sqlQuery += "DECLARE @t TABLE (data VARCHAR(MAX))\n";
                        sqlQuery += "INSERT INTO @t EXEC " + sp_ext + " " + listParam + "\n";
                        sqlQuery += "SELECT * FROM @t";

                        query_result = db.Database.SqlQuery<string>(sqlQuery, parameters.ToArray()).FirstOrDefault();

                        if (query_result != null)
                            query_result = System.Text.RegularExpressions.Regex.Unescape(query_result);                        
                    }
                    else // No Parameter request
                    {
                        string sqlQuery = "SET NOCOUNT ON\n";
                        sqlQuery += "DECLARE @t TABLE (data VARCHAR(MAX))\n";
                        sqlQuery += "INSERT INTO @t EXEC " + sp_ext + "\n";
                        sqlQuery += "SELECT * FROM @t";

                        query_result = db.Database.SqlQuery<string>(sqlQuery).FirstOrDefault();
                        
                        if (query_result != null)
                            query_result = System.Text.RegularExpressions.Regex.Unescape(query_result);
                        
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                query_result = null;
                throw ex;
            }
        }
    }
}