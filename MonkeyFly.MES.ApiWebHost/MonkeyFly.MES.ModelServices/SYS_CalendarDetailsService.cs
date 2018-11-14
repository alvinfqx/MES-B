using MonkeyFly.Core;
using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.Models;
using MonkeyFly.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MonkeyFly.MES.ModelServices
{
    public class SYS_CalendarDetailsService : SuperModel<SYS_CalendarDetails>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年5月8日17:02:57
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_CalendarDetails Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_CalendarDetails]([CalendarDetailID],[CalendarID],[Yeardate],[Wkhour],[Status],[Sequence],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                  (@CalendarDetailID,@CalendarID,@Yeardate,@Wkhour,@Status,@Sequence,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')",
                  userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CalendarDetailID",SqlDbType.VarChar),
                    new SqlParameter("@CalendarID",SqlDbType.VarChar),
                    new SqlParameter("@Yeardate",SqlDbType.DateTime),
                    new SqlParameter("@Wkhour",SqlDbType.Decimal),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    };

                parameters[0].Value = (Object)Model.CalendarDetailID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.CalendarID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Yeardate ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Wkhour ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 更新
        /// SAM 2017年5月8日17:03:26
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_CalendarDetails Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_CalendarDetails] set {0},
                [Wkhour]=@Wkhour,[Status]=@Status,[Sequence]=@Sequence
                where [CalendarDetailID]=@CalendarDetailID", UniversalService.getUpdateUTC(userId));

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CalendarDetailID",SqlDbType.VarChar),
                    new SqlParameter("@Wkhour",SqlDbType.Decimal),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int)
                    };

                parameters[0].Value = Model.CalendarDetailID;
                parameters[1].Value = (Object)Model.Wkhour ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Sequence ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 获取单一实体
        /// SAM 2017年5月8日17:03:38
        /// </summary>
        /// <param name="CalendarDetailID"></param>
        /// <returns></returns>
        public static SYS_CalendarDetails get(string CalendarDetailID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_CalendarDetails] where [CalendarDetailID] = '{0}'  and [SystemID] = '{1}' ", CalendarDetailID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据行事历流水号获取明细
        /// SAM 2017年5月8日18:02:45
        /// </summary>
        /// <param name="CalendarID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00014GetDetailsList(string CalendarID)
        {
            string select = String.Format(@"select convert(varchar(7),Yeardate,20) as Date,year(Yeardate) as Year,month(Yeardate) as Month ");

            string sql = String.Format(@" from [SYS_CalendarDetails] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' and [CalendarID] ='{1}'", Framework.SystemID, CalendarID);

            sql += String.Format(@" group by convert(varchar(7),Yeardate,20),Year(Yeardate),month(Yeardate) order by [Date] ");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text, null);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据行事历和年月获取那个月的具体信息
        /// SAM 2017年5月9日09:37:08
        /// </summary>
        /// <param name="CalendarID"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00014GetMonthList(string CalendarID, string Month)
        {
            string select = String.Format(@"select *,CONVERT(VARCHAR(2),Yeardate,103) as Day ");

            string sql = String.Format(@" from [SYS_CalendarDetails] where convert(varchar(7),Yeardate,20)='{0}' and [SystemID]='{1}' and [Status] <> '{1}0201213000003' and CalendarID='{2}' order By Yeardate ", Month, Framework.SystemID, CalendarID);

            //if (!string.IsNullOrWhiteSpace(FactoryCode))
            //    sql += String.Format(@" and [FactoryID] ='{0}'", FactoryCode);

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 更新SQL
        /// SAM 2017年5月9日11:48:49
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static string insertSQL(string userid, SYS_CalendarDetails Model)
        {
            string sql = string.Format(@"insert [SYS_CalendarDetails](
            CalendarDetailID,CalendarID,Yeardate,Wkhour,Sequence,Comments,Status,
            SystemID,Modifier,ModifiedTime,ModifiedLocalTime,Creator,CreateTime,CreateLocalTime) values 
           ('{0}','{1}','{2}','{3}','{4}','{5}','{6}',{7});",
           Model.CalendarDetailID,
           Model.CalendarID,
           Model.Yeardate,
           Model.Wkhour,
           Model.Sequence,
           Model.Comments,
           Model.Status,
           UniversalService.getInsertnew(userid));

            return sql;
        }

        /// <summary>
        /// 更新SQL
        /// SAM 2017年5月9日11:48:39
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="Date"></param>
        /// <param name="CalendarID"></param>
        /// <param name="Wkhour"></param>
        /// <returns></returns>
        public static string updateSQL(string userid, string Date, string CalendarID, string Wkhour)
        {
            string sql = String.Format(@"update [SYS_CalendarDetails] set {0},[Wkhour]='{1}'
                              where convert(varchar(10),Yeardate,120)='{2}' and [CalendarID]='{3}' and [SystemID]='{4}';"
                       , UniversalService.getUpdate(userid), Wkhour, Date, CalendarID, Framework.SystemID);

            return sql;
        }


        /// <summary>
        /// 根据行事历删除他的所有明细，如果Date有值，只删除指定月
        /// SAM 2017年5月9日11:52:51
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="CalendarID"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static bool delete(string userId, string CalendarID, string Date)
        {
            try
            {
                string sql = String.Format(@"update [SYS_CalendarDetails] set {0},[Status]='{1}0201213000003'
                where [CalendarID]='{2}'", UniversalService.getUpdateUTC(userId), Framework.SystemID, CalendarID);

                if (!string.IsNullOrWhiteSpace(Date))
                    sql += string.Format(@" and convert(varchar(10),Yeardate,120)='{0}' ", Date);
                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 删除指定年月日的行事历
        /// SAM 2017年5月9日12:01:10
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static bool delete(string userId, string Date)
        {
            try
            {
                string sql = String.Format(@"update [SYS_CalendarDetails] set {0},[Status]='{1}0201213000003'
                where [Yeardate]='{2}'", UniversalService.getUpdateUTC(userId), Framework.SystemID, Date);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 更新工时
        /// SAM 2017年6月14日15:21:39
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Date"></param>
        /// <param name="Wkhour"></param>
        /// <returns></returns>
        public static bool UpdateWkhour(string userId, string Date,string Wkhour,string CalendarID)
        {
            try
            {
                string sql = String.Format(@"update [SYS_CalendarDetails] set {0},[Wkhour]='{2}'
                where [Yeardate]='{1}' and [CalendarID] = '{3}'", UniversalService.getUpdateUTC(userId), Date, Wkhour, CalendarID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }


        /// <summary>
        /// 判断指定指定年月日是否存在行事历
        /// SAM 2017年5月9日11:57:56
        /// </summary>
        /// <param name="Yeardate"></param>
        /// <returns></returns>
        public static bool CheckYeardate(string Yeardate,string CalendarID)
        {
            string sql = string.Format(@"select * from [SYS_CalendarDetails] where [Yeardate]=@Yeardate and [CalendarID]='{1}' and [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID, CalendarID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Yeardate",SqlDbType.VarChar),
            };
            parameters[0].Value = Yeardate;

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据行事历代号，统一更新他的明细
        /// SAM 2017年5月17日11:46:49
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool updateByCalendar(string userId, string CalendarID,string Status)
        {
            try
            {
                string sql = String.Format(@"update[SYS_CalendarDetails] set {0},
                [Status]=@Status
                where [CalendarID]=@CalendarID", UniversalService.getUpdateUTC(userId));

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CalendarID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar)
                    };

                parameters[0].Value = CalendarID;
                parameters[1].Value = Status;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }


        /// <summary>
        /// 判断行事历是否已存在期间
        /// SAM 2017年8月1日21:54:46
        /// </summary>
        /// <param name="CalendarID"></param>
        /// <returns></returns>
        public static bool inf00014Check(string CalendarID)
        {
            string sql = string.Format(@"select * from [SYS_CalendarDetails] where [CalendarID] = '{0}' and [SystemID] = '{1}' and [Status] <> '{1}0201213000003' ", CalendarID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

    }
}

