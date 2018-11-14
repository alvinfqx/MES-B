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
    public class SYS_OrganizationClassService : SuperModel<SYS_OrganizationClass>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年7月25日11:04:36
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_OrganizationClass Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_OrganizationClass]([OrganizationClassID],[OrganizationID],
                [ClassID],[Status],[Comments],
                [Modifier],[ModifiedTime],[ModifiedLocalTime],
                [Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@OrganizationClassID,@OrganizationID,
                @ClassID,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@OrganizationClassID",SqlDbType.VarChar),
                    new SqlParameter("@OrganizationID",SqlDbType.VarChar),
                    new SqlParameter("@ClassID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.OrganizationClassID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.OrganizationID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ClassID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017年7月25日11:04:40
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_OrganizationClass Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_OrganizationClass] set {0},
                [Status]=@Status 
                where [OrganizationClassID]=@OrganizationClassID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@OrganizationClassID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.OrganizationClassID;
                parameters[1].Value = (Object)Model.Status ?? DBNull.Value;

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
        /// SAM 2017年7月25日11:04:56
        /// </summary>
        /// <param name="OrganizationClassID"></param>
        /// <returns></returns>
        public static SYS_OrganizationClass get(string OrganizationClassID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_OrganizationClass] where [OrganizationClassID] = '{0}'  and [SystemID] = '{1}' ", OrganizationClassID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据部门，删除他的所有班别信息
        /// SAM 2017年7月25日11:09:21
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool deleteByDept(string OrganizationID)
        {
            try
            {
                string sql = string.Format(@"update[SYS_OrganizationClass] set {0},
                [Status]=2 where [Organization] = '{0}'  and [SystemID] = '{1}' ", OrganizationID, Framework.SystemID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }

            catch (Exception ex)
            {

                DataLogerService.writeerrlog(ex);
                return false;
            }
        }



        public static bool delete(string OrganizationClassID)
        {
            try
            {
                string sql = string.Format(@"delete from [SYS_OrganizationClass] where [OrganizationClassID] = '{0}'  and [SystemID] = '{1}' ", OrganizationClassID, Framework.SystemID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }

            catch (Exception ex)
            {

                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 根据部门流水号获取班别列表
        /// SAM 2017年7月25日11:38:05
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00005GetClassList(string OrganizationID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.OrganizationClassID,A.OrganizationID,A.ClassID,A.Comments,
            D.Code,D.Name,D.OnTime,D.OffTime,
            (select [Name] from [SYS_Parameters] where D.[Status] = [ParameterID]) as Status,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_OrganizationClass] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Class] D on A.[ClassID] = D.[ClassID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and [OrganizationID] = '{1}'", Framework.SystemID, OrganizationID);

            count = UniversalService.getCount(sql, null);

            String orderby = " D.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据部门流水号获取班别（不分页）
        /// SAM 2017年7月25日11:40:15
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00005ClassList(string OrganizationID)
        {
            string select = string.Format(@"select A.OrganizationClassID,A.OrganizationID,A.ClassID,A.Comments,
            D.Code,D.Name,D.OnTime,D.OffTime,
            (select [Name] from [SYS_Parameters] where D.[Status] = [ParameterID]) as Status,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_OrganizationClass] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Class] D on A.[ClassID] = D.[ClassID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and [OrganizationID] = '{1}'", Framework.SystemID, OrganizationID);

       
            String orderby = "order by D.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select+sql+orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 将不在范围内的部门里的班别都删除了
        /// SAM 2017年7月25日11:49:32
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="OrganizationClassIDs"></param>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool Delete(string userId, string OrganizationClassIDs, string OrganizationID)
        {
            try
            {
                string sql = string.Format(@"update[SYS_OrganizationClass] set {0},
               [Status]='{1}0201213000003' where [OrganizationID]='{2}' ", UniversalService.getUpdateUTC(userId), Framework.SystemID, OrganizationID);

                if (!string.IsNullOrWhiteSpace(OrganizationClassIDs))
                    sql += string.Format(@" and [OrganizationClassID] not in ('{0}')", OrganizationClassIDs);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 判断是否已存在部门和班别的映射
        /// SAM 2017年7月25日11:54:08
        /// </summary>
        /// <param name="ClassID"></param>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool CheckClass(string ClassID, string OrganizationID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_OrganizationClass] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(ClassID))
                sql = sql + String.Format(@" and [ClassID] = '{0}' ", ClassID);

            if (!string.IsNullOrWhiteSpace(OrganizationID))
                sql = sql + String.Format(@" and [OrganizationID] = '{0}' ", OrganizationID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
    }
}

