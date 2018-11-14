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
    public class SYS_CalendarService : SuperModel<SYS_Calendar>
    {
        /// <summary>
        /// 添加
        /// SAM 2017年5月8日17:01:28
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_Calendar Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_Calendar]([CalendarID],[Code],[Name],[Ifdefault],[Status],[Sequence],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                (@CalendarID,@Code,@Name,@Ifdefault,@Status,@Sequence,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CalendarID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.VarChar),
                    new SqlParameter("@Ifdefault",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    };

                parameters[0].Value = (Object)Model.CalendarID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Ifdefault ?? DBNull.Value;
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
        /// SAM 2017年5月8日17:02:03
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_Calendar Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_Calendar] set {0},
                    [Name]=@Name,[Ifdefault]=@Ifdefault,[Status]=@Status,[Sequence]=@Sequence,[Comments]=@Comments 
                    where [CalendarID]=@CalendarID", UniversalService.getUpdateUTC(userId));

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CalendarID",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.VarChar),
                    new SqlParameter("@Ifdefault",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.CalendarID;
                parameters[1].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Ifdefault ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017年5月8日17:02:27
        /// </summary>
        /// <param name="CalendarID"></param>
        /// <returns></returns>
        public static SYS_Calendar get(string CalendarID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Calendar] where [CalendarID] = '{0}'  and [SystemID] = '{1}' ", CalendarID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 判断是否存在主行事历
        /// SAM 2017年8月1日17:02:43
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="CalendarID"></param>
        /// <returns></returns>
        public static bool CheckDefault(string CalendarID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Calendar] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' and [Ifdefault]=1", Framework.SystemID);

            //用于更新判定是，排除自身
            if (!string.IsNullOrWhiteSpace(CalendarID))
                sql += string.Format(@" and [CalendarID] <> '{0}'", CalendarID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        public static bool CheckCode(string Code, string CalendarID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Calendar] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            /*先定义Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            /*因为Code是通过手动输入的，所以需要用参数的形式去拼SQL*/
            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql = sql + string.Format(@" and [Code] =@Code ");
                parameters[0].Value = Code;
            }

            /*CalendarID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(CalendarID))
                sql = sql + String.Format(@" and [CalendarID] <> '{0}' ", CalendarID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// 根据代号获取正常的行事历
        /// SAM 2017年5月25日11:39:03
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static SYS_Calendar getByCode(string Code)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Calendar] where [Code] = '{0}' and  [SystemID] = '{1}'  and [Status]='{1}0201213000001' ", Code, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 获取行事历的列表
        /// SAM 2017年5月8日17:49:14
        /// TODO
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00014GetList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.CalendarID,A.Code,A.Name,A.Ifdefault,A.Status,A.Sequence,A.Comments,
             B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Calendar] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' ",
            Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            count = UniversalService.getCount(sql, Parcount);

            String orderby = "A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 导出行事历
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static DataTable Inf00014Export(string Code)
        {
         string select = string.Format(@"select distinct null as RowNumber,A.Code, A.Name,
            (CASE WHEN A.Ifdefault=1 THEN '是' ELSE '否' END),
            D.Name as Status, A.Comments,
             B.UserName as Creator, A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime,
            CONVERT(varchar(7), E.Yeardate, 120) as Date,
            null as A1,null as A2,null as A3,null as A4,null as A5,null as A6,null as A7,null as A8,null as A9,null as A10,
            null as A11,null as A12,null as A13,null as A14,null as A15,null as A16,null as A17,null as A18,null as A19,null as A20,
            null as A21,null as A22,null as A23,null as A24,null as A25,null as A26,null as A27,null as A28,null as A29,null as A30,
            null as A31 ");

            string sql = string.Format(@" from [SYS_Calendar] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[Status] = D.[ParameterID]
            left join [SYS_CalendarDetails] E on A.CalendarID =E.CalendarID 
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' ",
            Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            string orderBy = "order By A.[Code],[Date] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        /// 获取行事历的弹窗
        /// SAM 2017年5月26日09:37:56
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetCalendarList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.CalendarID,A.Code,A.Name,A.Ifdefault,D.Name as Status,A.Sequence,A.Comments,
             B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Calendar] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[Status] = D.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' ",
            Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            count = UniversalService.getCount(sql, Parcount);

            String orderby = "A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 判断是否已存在主行事历
        /// SAM 2017年7月28日11:53:39
        /// </summary>
        /// <returns></returns>
        public static bool Inf00014CheckIfdefault()
        {
            string sql = string.Format(@" select * from [SYS_Calendar] A 
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' and [Ifdefault]=1",
            Framework.SystemID);
         
            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
    }
}

