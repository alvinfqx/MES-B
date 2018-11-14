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
    public class SYS_ClassService : SuperModel<SYS_Class>
    {

        public static bool insert(string userId, SYS_Class Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_Class]([ClassID],[Code],[Name],[Status],[Comments],[CrossDay],[OnTime],[OffTime],[OffHour],[WorkHour],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
 (@ClassID,@Code,@Name,@Status,@Comments,@CrossDay,@OnTime,@OffTime,@OffHour,@WorkHour,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ClassID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    new SqlParameter("@CrossDay",SqlDbType.VarChar),
                    new SqlParameter("@OnTime",SqlDbType.VarChar),
                    new SqlParameter("@OffTime",SqlDbType.VarChar),
                    new SqlParameter("@OffHour",SqlDbType.VarChar),
                    new SqlParameter("@WorkHour",SqlDbType.VarChar),
                    };

                parameters[0].Value = (Object)Model.ClassID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[5].Value = (Object)Model.CrossDay ?? DBNull.Value;
                parameters[6].Value = (Object)Model.OnTime ?? DBNull.Value;
                parameters[7].Value = (Object)Model.OffTime ?? DBNull.Value;
                parameters[8].Value = (Object)Model.OffHour ?? DBNull.Value;
                parameters[9].Value = (Object)Model.WorkHour ?? DBNull.Value;
                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static bool update(string userId, SYS_Class Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_Class] set {0},
                [Code]=@Code,[Name]=@Name,[Status]=@Status,[Comments]=@Comments,
                [CrossDay]=@CrossDay,[OnTime]=@OnTime,[OffTime]=@OffTime,[OffHour]=@OffHour,[WorkHour]=@WorkHour 
                where [ClassID]=@ClassID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ClassID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.VarChar),               
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    new SqlParameter("@CrossDay",SqlDbType.VarChar),
                    new SqlParameter("@OnTime",SqlDbType.VarChar),
                    new SqlParameter("@OffTime",SqlDbType.VarChar),
                    new SqlParameter("@OffHour",SqlDbType.VarChar),
                    new SqlParameter("@WorkHour",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.ClassID;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[5].Value = (Object)Model.CrossDay ?? DBNull.Value;
                parameters[6].Value = (Object)Model.OnTime ?? DBNull.Value;
                parameters[7].Value = (Object)Model.OffTime ?? DBNull.Value;
                parameters[8].Value = (Object)Model.OffHour ?? DBNull.Value;
                parameters[9].Value = (Object)Model.WorkHour ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static SYS_Class get(string ClassID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Class] where [ClassID] = '{0}'  and [SystemID] = '{1}' ", ClassID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

    
        public static IList<Hashtable> GetPage(string code, string status, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.ClassID, A.Code, A.Name, A.Comments, A.Status,A.CrossDay,A.OnTime,A.OffTime,A.OffHour,A.WorkHour,
                         C.UserName as Creator,A.CreateLocalTime as CreateTime,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else B.UserName END) as Modifier,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime  ");

            string sql = string.Format(
                @" from [SYS_Class] A
                   left join SYS_MESUsers B on A.Modifier = B.MESUserID
                   left join SYS_MESUsers C on A.Creator = C.MESUserID
                   where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003'",
                Framework.SystemID);

            List<SqlParameter> paramList = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(code))
            {
                sql += string.Format(" and A.Code like @Code ");
                SqlParameter sp = new SqlParameter("@Code", SqlDbType.VarChar);
                sp.Value = "%" + code + "%";
                paramList.Add(sp);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql += string.Format(" and A.Status = @Status ");
                SqlParameter sp = new SqlParameter("@Status", SqlDbType.VarChar);
                sp.Value = status;
                paramList.Add(sp);
            }

            count = UniversalService.getCount(sql, paramList.ToArray());

            String orderby = "A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby,
                paramList.Select(p =>
                {
                    SqlParameter sp = new SqlParameter(p.ParameterName, p.DbType);
                    sp.Value = p.Value;
                    return sp;
                }).ToArray(), page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 班别导出
        /// </summary>
        /// <param name="code"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static DataTable GetExportList(string code, string status)
        {
            string sql = string.Format(
                @" select ROW_NUMBER() OVER (ORDER BY A.[Status],A.[Code]),
                            A.Code, A.Name, A.Comments,
                            (CASE WHEN A.CrossDay='true' then 'Y' else 'N' end),
                            A.OnTime,A.OffTime,A.OffHour,A.WorkHour,
                          (case when A.Status = '{0}0201213000001' then '正常' else '作废' end), 
                          C.UserName as Creator,A.CreateLocalTime as CreateTime,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else B.UserName END) as Modifier,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime  
                   from [SYS_Class] A
                   left join SYS_MESUsers B on A.Modifier = B.MESUserID
                   left join SYS_MESUsers C on A.Creator = C.MESUserID
                   where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003'",
                Framework.SystemID);

            List<SqlParameter> paramList = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(code))
            {
                sql += string.Format(" and A.Code like @Code ");
                SqlParameter sp = new SqlParameter("@Code", SqlDbType.VarChar);
                sp.Value = "%" + code + "%";
                paramList.Add(sp);
            }
            if (!string.IsNullOrWhiteSpace(status))
            {
                sql += string.Format(" and A.Status = @Status ");
                SqlParameter sp = new SqlParameter("@Status", SqlDbType.VarChar);
                sp.Value = status;
                paramList.Add(sp);
            }

            sql += " order by A.[Status],A.[Code] ";

            return SQLHelper.ExecuteDataTable(sql, CommandType.Text, paramList.Select(p =>
            {
                SqlParameter sp = new SqlParameter(p.ParameterName, p.DbType);
                sp.Value = p.Value;
                return sp;
            }).ToArray());
        }

        public static bool ChecCode(string code)
        {
            string sql = string.Format(
                @"select count(*)
                  from SYS_Class
                  where Code = '{0}' and Status <> '{1}0201213000003'",
                code, Framework.SystemID);

            if (SQLHelper.getValue<int>(sql) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ChecCode(string code, string id)
        {
            string sql = string.Format(
                @"select count(*)
                  from SYS_Class
                  where Code = '{0}' and ClassID <> '{1}' and Status <> '{2}0201213000003'",
                code, id, Framework.SystemID);

            if (SQLHelper.getValue<int>(sql) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取班别的弹窗
        /// SAM 2017年6月14日09:41:12
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetClassList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ClassID,A.Code,A.Name,A.Comments,D.Name as Status,
             B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Class] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[Status] = D.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' ",Framework.SystemID);

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

            string orderby = "A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 根据部门流水号获取不属于他的班别（不分页）
        /// SAM 2017年7月25日11:44:53
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static object Inf00005NoClassList(string OrganizationID)
        {
            string select = string.Format(@"select null as OrganizationClassID,A.ClassID,A.Code,A.Name,
            A.OnTime,A.OffTime, (select [Name] from [SYS_Parameters] where A.[Status] = [ParameterID]) as Status ");

            string sql = string.Format(@"  from [SYS_Class] A 
            where [ClassID] not in (select [ClassID] from [SYS_OrganizationClass] where [OrganizationID] ='{1}' and [Status] = '{0}0201213000001')
            and A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID, OrganizationID);

            string orderBy = "order By A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text);

            return ToHashtableList(dt);
        }
    }
}

