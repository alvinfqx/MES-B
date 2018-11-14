using MonkeyFly.Core;
using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.Models;
using MonkeyFly.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MonkeyFly.MES.ModelServices
{
    public class SYS_MFCLogService : SuperModel<SYS_MFCLog>
    {
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(SYS_MFCLog Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_MFCLog]([SystemID],[UserID],[UserName],[Tag],[Position],[Target],[Type],[Message],[CreateTime]) values
                (@SystemID,@UserID,@UserName,@Tag,@Position,@Target,@Type,@Message,'{0}')", DateTime.Now);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@SystemID",SqlDbType.VarChar),
                    new SqlParameter("@UserID",SqlDbType.VarChar),
                    new SqlParameter("@UserName",SqlDbType.VarChar),
                    new SqlParameter("@Tag",SqlDbType.VarChar),
                    new SqlParameter("@Position",SqlDbType.VarChar),
                    new SqlParameter("@Target",SqlDbType.VarChar),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@Message",SqlDbType.VarChar,-1),
                    };

                parameters[0].Value = (Object)Model.SystemID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.UserID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.UserName ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Tag ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Postion ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Target ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Message ?? DBNull.Value;


                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 获取系统操作列表
        /// Tom 2017年5月12日09:42:09
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetPage(DateTime? startTime, DateTime? endTime, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select B.Account, B.UserName, A.Position, A.Message as Request, A.Type, '' as Result, A.CreateTime");

            string sql = string.Format(
                @" from [SYS_MFCLog] A
                   left join SYS_MESUsers B on A.UserID = B.MESUserID
                   where A.[SystemID]='{0}' ",
                Framework.SystemID);

            List<SqlParameter> paramList = new List<SqlParameter>();
            if (startTime != null)
            {
                sql += string.Format(" and @StartTime < A.CreateTime ");
                SqlParameter sp = new SqlParameter("@StartTime", SqlDbType.VarChar);
                sp.Value = startTime;
                paramList.Add(sp);
            }

            if (endTime != null)
            {
                sql += string.Format(" and A.CreateTime <= @EndTime ");
                SqlParameter sp = new SqlParameter("@EndTime", SqlDbType.VarChar);
                sp.Value = endTime;
                paramList.Add(sp);
            }

            count = UniversalService.getCount(sql, paramList.ToArray());

            String orderby = "A.[CreateTime] desc";

            DataTable dt = UniversalService.getTable(select, sql, orderby,
                paramList.Select(p =>
                {
                    SqlParameter sp = new SqlParameter(p.ParameterName, p.DbType);
                    sp.Value = p.Value;
                    return sp;
                }).ToArray(), page, rows);

            return ToHashtableList(dt);
        }
    }
}

