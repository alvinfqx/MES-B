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
    public class SYS_AutoNumberService : SuperModel<SYS_AutoNumber>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年5月17日15:44:31
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_AutoNumber Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_AutoNumber]([AutoNumberID],[Code],[Description],[DefaultCharacter],
                [YearLength],[MonthLength],[DateLength],[NumLength],[Length],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                (@AutoNumberID,@Code,@Description,@DefaultCharacter,@YearLength,
                @MonthLength,@DateLength,@NumLength,@Length,@Status,
                @Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@AutoNumberID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.VarChar),
                    new SqlParameter("@Description",SqlDbType.VarChar),
                    new SqlParameter("@DefaultCharacter",SqlDbType.VarChar),
                    new SqlParameter("@YearLength",SqlDbType.TinyInt),
                    new SqlParameter("@MonthLength",SqlDbType.TinyInt),
                    new SqlParameter("@DateLength",SqlDbType.TinyInt),
                    new SqlParameter("@NumLength",SqlDbType.TinyInt),
                    new SqlParameter("@Length",SqlDbType.TinyInt),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    };

                parameters[0].Value = (Object)Model.AutoNumberID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Description ?? DBNull.Value;
                parameters[3].Value = (Object)Model.DefaultCharacter ?? DBNull.Value;
                parameters[4].Value = (Object)Model.YearLength ?? DBNull.Value;
                parameters[5].Value = (Object)Model.MonthLength ?? DBNull.Value;
                parameters[6].Value = (Object)Model.DateLength ?? DBNull.Value;
                parameters[7].Value = (Object)Model.NumLength ?? DBNull.Value;
                parameters[8].Value = (Object)Model.Length ?? DBNull.Value;
                parameters[9].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[10].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM2017年5月17日15:44:51
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_AutoNumber Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_AutoNumber] set {0},
                [DefaultCharacter]=@DefaultCharacter,[YearLength]=@YearLength,[MonthLength]=@MonthLength,
                [DateLength]=@DateLength,[NumLength]=@NumLength,[Length]=@Length,[Status]=@Status 
                where [AutoNumberID]=@AutoNumberID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@AutoNumberID",SqlDbType.VarChar),
                    new SqlParameter("@DefaultCharacter",SqlDbType.VarChar),
                    new SqlParameter("@YearLength",SqlDbType.TinyInt),
                    new SqlParameter("@MonthLength",SqlDbType.TinyInt),
                    new SqlParameter("@DateLength",SqlDbType.TinyInt),
                    new SqlParameter("@NumLength",SqlDbType.TinyInt),
                    new SqlParameter("@Length",SqlDbType.TinyInt),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.AutoNumberID;
                parameters[1].Value = (Object)Model.DefaultCharacter ?? DBNull.Value;
                parameters[2].Value = (Object)Model.YearLength ?? DBNull.Value;
                parameters[3].Value = (Object)Model.MonthLength ?? DBNull.Value;
                parameters[4].Value = (Object)Model.DateLength ?? DBNull.Value;
                parameters[5].Value = (Object)Model.NumLength ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Length ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Status ?? DBNull.Value;

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
        /// SAM 2017年5月17日15:45:09
        /// </summary>
        /// <param name="AutoNumberID"></param>
        /// <returns></returns>
        public static SYS_AutoNumber get(string AutoNumberID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_AutoNumber] where [AutoNumberID] = '{0}'  and [SystemID] = '{1}' ", AutoNumberID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 判断代号是否重复
        /// SAM 2017年5月17日15:51:03
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="AutoNumberID"></param>
        /// <returns></returns>
        public static bool CheckCode(string Code,string Description, string AutoNumberID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_AutoNumber] where [SystemID]='{0}' and Status <> '{0}0201213000003'", Framework.SystemID);

            /*先定义Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Description",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            /*因为Code是通过手动输入的，所以需要用参数的形式去拼SQL*/
            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql = sql + String.Format(@" and [Code] =@Code ");
                parameters[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(Description))
            {
                sql = sql + String.Format(@" and [Description] =@Description ");
                parameters[1].Value = Description;
            }

            /*AutoNumberID（流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(AutoNumberID))
                sql = sql + String.Format(@" and [AutoNumberID] <> '{0}' ", AutoNumberID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据代号获取正常的批号实体
        /// SAM  2017年5月24日11:30:32
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static SYS_AutoNumber getByCode(string Code)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_AutoNumber] where [Code] = '{0}'  and [SystemID] = '{1}' and [Status]='{1}0201213000001'", Code, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 批号自动编号设定列表
        /// SAM 2017年5月17日15:46:02
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00023GetList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.AutoNumberID,A.Code,A.Description,A.DefaultCharacter,A.Status,
            A.YearLength,A.MonthLength,A.DateLength,A.NumLength,A.Length,A.Comments,     
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_AutoNumber] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003' ", Framework.SystemID);

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
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Status],A.[Code]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 批号自动编号的导出
        /// SAM 2017年5月17日15:52:18
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static DataTable Inf00023Export(string Code)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Code]),A.Code,A.Description,A.DefaultCharacter,
            (CASE WHEN A.YearLength=0 THEN 'N' ELSE 'Y' END),
            (CASE WHEN A.MonthLength=0 THEN 'N' ELSE 'Y' END),
            (CASE WHEN A.DateLength=0 THEN 'N' ELSE 'Y' END),A.NumLength,A.Length,A.Comments,     
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_AutoNumber] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003' ", Framework.SystemID);

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
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            string orderBy = "order By A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }


        public static IList<Hashtable> Inf00023GetVaildList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.AutoNumberID,A.Code,A.Description,A.DefaultCharacter,A.Status,
            A.YearLength,A.MonthLength,A.DateLength,A.NumLength,A.Length,A.Comments,     
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_AutoNumber] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.Status = '{0}0201213000001' ", Framework.SystemID);

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
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }
    }
}

