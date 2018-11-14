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
    public class SYS_ProjectsService : SuperModel<SYS_Projects>
    {

        public static bool insert(string userId, SYS_Projects Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_Projects]([ProjectID],Code,[Description],[Attribute],[Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
 (@ProjectID,@Code,@Description,@Attribute,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ProjectID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.VarChar),
                    new SqlParameter("@Description",SqlDbType.VarChar),
                    new SqlParameter("@Attribute",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    };

                parameters[0].Value = (Object)Model.ProjectID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Description ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Attribute ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static bool update(string userId, SYS_Projects Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_Projects] set {0},
Code=@Code,[Description]=@Description,[Attribute]=@Attribute,[Status]=@Status,[Comments]=@Comments where [ProjectID]=@ProjectID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ProjectID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.VarChar),
                    new SqlParameter("@Description",SqlDbType.VarChar),
                    new SqlParameter("@Attribute",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar)
                    };

                parameters[0].Value = Model.ProjectID;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Description ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Attribute ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static SYS_Projects get(string ProjectID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Projects] where [ProjectID] = '{0}'  and [SystemID] = '{1}' ", ProjectID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }


        public static IList<Hashtable> GetPage(string code, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.ProjectID, A.Code, A.Description, A.Comments, A.Status, A.Attribute,
                          C.UserName as Creator,A.CreateLocalTime as CreateTime,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else B.UserName END) as Modifier,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime  ");

            string sql = string.Format(
                @" from [SYS_Projects] A
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

            count = UniversalService.getCount(sql, paramList.ToArray());

            String orderby = " A.[Status],A.[Code]";

            DataTable dt = UniversalService.getTable(select, sql, orderby,
                paramList.Select(p =>
                {
                    SqlParameter sp = new SqlParameter(p.ParameterName, p.DbType);
                    sp.Value = p.Value;
                    return sp;
                }).ToArray(), page, rows);

            return ToHashtableList(dt);
        }

        public static DataTable GetExportList(string code)
        {
            string sql = string.Format(
                @" select ROW_NUMBER() OVER (ORDER BY A.[Status],A.[Code]),A.Code, A.Description, D.Name, A.Comments, case when A.Status = '{0}0201213000001' then '正常' else '作废' end, 
                          C.UserName as Creator,A.CreateLocalTime as CreateTime,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else B.UserName END) as Modifier,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime  
                   from [SYS_Projects] A
                   left join SYS_MESUsers B on A.Modifier = B.MESUserID
                   left join SYS_MESUsers C on A.Creator = C.MESUserID
                   left join SYS_Parameters D on A.Attribute = D.ParameterID
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

            sql += " order by A.[Status],A.[Code]";

            return SQLHelper.ExecuteDataTable(sql, CommandType.Text, paramList.Select(p => {
                SqlParameter sp = new SqlParameter(p.ParameterName, p.DbType);
                sp.Value = p.Value;
                return sp;
            }).ToArray());
        }

        public static bool ChecCode(string code)
        {
            string sql = string.Format(
                @"select count(*)
                  from SYS_Projects
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
                  from SYS_Projects
                  where Code = '{0}' and ProjectID <> '{1}' and Status <> '{2}0201213000003'",
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
        /// 根据代号获取项目
        /// SAM 2017年5月23日11:17:17
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static SYS_Projects getByCode(string Code)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Projects] where [Code] = '{0}'  and [SystemID] = '{1}'  and Status='{1}0201213000001' ", Code, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }


        /// <summary>
        /// 获取项目的弹窗
        /// SAM 2017年5月25日17:11:23
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetProjectList(string code, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.ProjectID, A.Code, A.Description, A.Comments, D.Name as Status, A.Attribute,
                          C.UserName as Creator,A.CreateLocalTime as CreateTime,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else B.UserName END) as Modifier,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime  ");

            string sql = string.Format(
                @" from [SYS_Projects] A
                   left join SYS_MESUsers B on A.Modifier = B.MESUserID
                   left join SYS_MESUsers C on A.Creator = C.MESUserID
                left join SYS_Parameters D on A.Status = D.ParameterID
                   where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001'",
                Framework.SystemID);

            List<SqlParameter> paramList = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(code))
            {
                sql += string.Format(" and A.Code like @Code ");
                SqlParameter sp = new SqlParameter("@Code", SqlDbType.VarChar);
                sp.Value = "%" + code + "%";
                paramList.Add(sp);
            }

            count = UniversalService.getCount(sql, paramList.ToArray());

            String orderby = " A.[Status],A.[Code]";

            DataTable dt = UniversalService.getTable(select, sql, orderby,
                paramList.Select(p =>
                {
                    SqlParameter sp = new SqlParameter(p.ParameterName, p.DbType);
                    sp.Value = p.Value;
                    return sp;
                }).ToArray(), page, rows);

            return ToHashtableList(dt);
        }

        public static IList<SYS_Projects> Iot00003GetEquipmentProject(string EquipmentID)
        {
            string sql = string.Format(@"select B.Code from [EMS_EquipmentProject] A
                                left join [SYS_Projects] B on A.ProjectID=B.ProjectID
                                where A.[EquipmentID]='{0}' ",EquipmentID);
            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);
            //List<SYS_Projects> a = new List<SYS_Projects>();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    a.Add(ToEntity(dt.Rows[i]));
            //}
            return ToList(dt);
        }

    }
}
