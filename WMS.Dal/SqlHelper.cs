using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using WMS.Model;
using WMS.Common;
using System.Data;

namespace WMS.Dal
{
    public class SqlHelper
    {
        static string _conStr = string.Empty;
        static SqlHelper()
        {
            _conStr = SystemConfig.WMSConnectionString;
        }

        public static List<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_conStr))
                {
                    return con.Query<T>(sql, param, transaction).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error<SqlHelper>(ex.ToString());
                return null;
            }

        }
        public static T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_conStr))
                {
                    return con.ExecuteScalar<T>(sql, param, transaction);
                }
            }
            catch (Exception ex)
            {
                Log.Error<SqlHelper>(ex.ToString());
                return default(T);
            }
        }

        public static int Execute(string sql, object param = null, IDbTransaction transaction = null)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_conStr))
                {
                    return con.Execute(sql, param, transaction);
                }
            }
            catch (Exception ex)
            {
                Log.Error<SqlHelper>(ex.ToString());
                return 0;
            }
        }
    }
}
