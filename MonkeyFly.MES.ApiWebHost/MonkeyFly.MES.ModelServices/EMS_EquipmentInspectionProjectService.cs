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
    public class EMS_EquipmentInspectionProjectService : SuperModel<EMS_EquipmentInspectionProject>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年5月23日15:40:11
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, EMS_EquipmentInspectionProject Model)
        {
            try
            {
                string sql = string.Format(@"insert[EMS_EquipmentInspectionProject]([EIProjectID],[EquipmentID],[EquipmentProjectID],
                    [Sequence],[Status],[ProjectID],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                    (@EIProjectID,@EquipmentID,@EquipmentProjectID,@Sequence,@Status,@ProjectID,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@EIProjectID",SqlDbType.VarChar),
                    new SqlParameter("@EquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@EquipmentProjectID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    new SqlParameter("@ProjectID",SqlDbType.VarChar)
                    };

                parameters[0].Value = (Object)Model.EIProjectID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.EquipmentID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.EquipmentProjectID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[6].Value = (Object)Model.ProjectID ?? DBNull.Value;
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
        /// SAM 2017年5月23日15:40:23
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, EMS_EquipmentInspectionProject Model)
        {
            try
            {
                string sql = String.Format(@"update[EMS_EquipmentInspectionProject] set {0},[ProjectID]=@ProjectID,
                [EquipmentProjectID]=@EquipmentProjectID,[Sequence]=@Sequence,[Status]=@Status 
                where [EIProjectID]=@EIProjectID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@EIProjectID",SqlDbType.VarChar),
                    new SqlParameter("@EquipmentProjectID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                        new SqlParameter("@ProjectID",SqlDbType.VarChar)
                    };

                parameters[0].Value = Model.EIProjectID;
                parameters[1].Value = (Object)Model.EquipmentProjectID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[4].Value = (Object)Model.ProjectID ?? DBNull.Value;
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
        /// SAM 2017年5月23日15:42:29
        /// </summary>
        /// <param name="EIProjectID"></param>
        /// <returns></returns>
        public static EMS_EquipmentInspectionProject get(string EIProjectID)
        {
            string sql = string.Format(@"select Top 1 * from [EMS_EquipmentInspectionProject] where [EIProjectID] = '{0}'  and [SystemID] = '{1}' ", EIProjectID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        public static bool Check(string EquipmentProjectID, string EquipmentID, string EIProjectID)
        {
            string sql = String.Format(@"select Top 1 * from [EMS_EquipmentInspectionProject] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' and [EquipmentID]='{1}' ", Framework.SystemID, EquipmentID);

            /*先定义Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@EquipmentProjectID",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            /*因为Code是通过手动输入的，所以需要用参数的形式去拼SQL*/
            if (!string.IsNullOrWhiteSpace(EquipmentProjectID))
            {
                sql = sql + string.Format(@" and [EquipmentProjectID] =@EquipmentProjectID ");
                parameters[0].Value = EquipmentProjectID;
            }

            /*EIProjectID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(EIProjectID))
                sql = sql + string.Format(@" and [EIProjectID] <> '{0}' ", EIProjectID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取一个设备对应的巡检项目列表
        /// SAM 2017年5月23日15:43:27
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00002GetList(string EquipmentID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.EIProjectID,A.EquipmentID,A.EquipmentProjectID,A.Sequence,A.Status,
            F.Code as ProjectCode,F.Description as ProjectDescription,A.ProjectID,
            (Select [Code] from [SYS_Parameters] where ParameterID = F.[Attribute]) as AttributeCode,
            (Select [Name] from [SYS_Parameters] where ParameterID = F.[Attribute]) as AttributeName,
            D.StandardValue,D.MaxValue,D.MinValue,A.Comments,     
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_EquipmentInspectionProject] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [EMS_EquipmentProject] D on A.[EquipmentProjectID] = D.[EquipmentProjectID]
            left join [EMS_Equipment] E on A.[EquipmentID] = E.[EquipmentID]
            left join [SYS_Projects] F on A.[ProjectID] = F.[ProjectID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' and A.[EquipmentID] = '{1}' ", Framework.SystemID, EquipmentID);

            count = UniversalService.getCount(sql, null);

            String orderby = " A.[Status],F.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据设备获取他的巡检项目（不分页，所有的信息）
        /// SAM 2017年6月8日15:17:14
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetList(string EquipmentID)
        {
            string select = string.Format(@"select A.EIProjectID,A.EquipmentID,A.EquipmentProjectID,A.Sequence,A.Status,
            F.Code as ProjectCode,F.Description as ProjectDescription,A.ProjectID,
            (Select [Code] from [SYS_Parameters] where ParameterID = F.[Attribute]) as AttributeCode,
            (Select [Name] from [SYS_Parameters] where ParameterID = F.[Attribute]) as AttributeName,
            D.StandardValue,D.MaxValue,D.MinValue,A.Comments ");

            string sql = string.Format(@"  from [EMS_EquipmentInspectionProject] A 
            left join [EMS_EquipmentProject] D on A.[EquipmentProjectID] = D.[EquipmentProjectID]
            left join [EMS_Equipment] E on A.[EquipmentID] = E.[EquipmentID]
            left join [SYS_Projects] F on A.[ProjectID] = F.[ProjectID]
            where A.[SystemID]='{0}' and A.[Status] ='{0}0201213000001' and A.[EquipmentID] = '{1}' ", Framework.SystemID, EquipmentID);

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据设备流水号获取巡检项目列表
        /// MOUSE 2017年7月31日15:50:05
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00002GetProjectList(string EquipmentID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.EIProjectID,A.EquipmentID,A.EquipmentProjectID,A.Sequence,A.Status,
            F.Code as ProjectCode,F.Description as ProjectDescription,A.ProjectID,
            (Select [Code] from [SYS_Parameters] where ParameterID = F.[Attribute]) as AttributeCode,
            (Select [Name] from [SYS_Parameters] where ParameterID = F.[Attribute]) as AttributeName,
            D.StandardValue,D.MaxValue,D.MinValue,A.Comments ");

            string sql = string.Format(@"  from [EMS_EquipmentInspectionProject] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [EMS_EquipmentProject] D on A.[EquipmentProjectID] = D.[EquipmentProjectID]
            left join [EMS_Equipment] E on A.[EquipmentID] = E.[EquipmentID]
            left join [SYS_Projects] F on A.[ProjectID] = F.[ProjectID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' and A.[EquipmentID] = '{1}' ", Framework.SystemID, EquipmentID);

            count = UniversalService.getCount(sql, null);

            String orderby = " A.[Status],F.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据设备流水号获取他的巡检项目（不分页）
        /// MOUSE 2017年7月31日16:27:44
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00002ProjectList(string EquipmentID)
        {
            string select = string.Format(@"select A.EIProjectID,A.EquipmentID,A.EquipmentProjectID,A.Sequence,A.Status,A.ProjectID,
            F.Code as ProjectCode,F.Description as ProjectDescription,
            (Select [Code] from [SYS_Parameters] where ParameterID = F.[Attribute]) as AttributeCode,
            (Select [Name] from [SYS_Parameters] where ParameterID = F.[Attribute]) as AttributeName,
            D.StandardValue,D.MaxValue,D.MinValue,A.Comments,     
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_EquipmentInspectionProject] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [EMS_EquipmentProject] D on A.[EquipmentProjectID] = D.[EquipmentProjectID]
            left join [EMS_Equipment] E on A.[EquipmentID] = E.[EquipmentID]

            left join [SYS_Projects] F on A.[ProjectID] = F.[ProjectID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' and A.[EquipmentID] = '{1}' ", Framework.SystemID, EquipmentID);

            string orderBy = "order By A.[Sequence] ";
            DataTable dt = SQLHelper.ExecuteDataTable(select + sql+orderBy, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据设备流水号获取不属于他的巡检项目（不分页）
        /// MOUSE 2017年7月31日16:27:44
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        public static object Ems00002NoProjectList(string EquipmentID)
        {
            string select = string.Format(@"select A.EquipmentID,A.EquipmentProjectID,A.Status,
            (Select TOP 1 [Code] from [SYS_Parameters] where ParameterID = F.[Attribute]) as AttributeCode,
            (Select TOP 1 [Name] from [SYS_Parameters] where ParameterID = F.[Attribute]) as AttributeName,

            (Select TOP 1 [Sequence] from [EMS_EquipmentInspectionProject] where ProjectID = A.[ProjectID]) as Sequence,

            (select TOP 1 [Code]from [SYS_Projects] where A.[ProjectID] = [ProjectID]) as ProjectCode,
            (select TOP 1 [Description]from [SYS_Projects] where A.[ProjectID] = [ProjectID]) as ProjectDescription,
            A.StandardValue,A.MaxValue,A.MinValue,A.Comments");


            string sql = string.Format(@"  from [EMS_EquipmentProject] A 
            left join [SYS_Projects] F on A.[ProjectID] = F.[ProjectID]
            where [EquipmentProjectID] not in (select [EquipmentProjectID] from [EMS_EquipmentInspectionProject] where [EquipmentID] ='{1}' and [Status] = '{0}0201213000001')
            and A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and [EquipmentID]='{1}' ", Framework.SystemID, EquipmentID);

            string orderBy = "order By [ProjectCode] ";
            DataTable dt = SQLHelper.ExecuteDataTable(select + sql+orderBy, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 将不在范围内的设备里的巡检设备都删除了
        /// MOUSE 2017年7月31日17:39:22
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="EIProjectIDs"></param>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        public static bool Delete(string userId,  string EquipmentID)
        {
            try
            {
                string sql = string.Format(@"update[EMS_EquipmentInspectionProject] set {0},
               [Status]='{1}0201213000003' where [EquipmentID]='{2}' ", UniversalService.getUpdateUTC(userId), Framework.SystemID, EquipmentID);
                
                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 判断是否已存在设备和巡检设备的映射
        /// MOUSE 2017年7月31日17:50:10
        /// </summary>
        /// <param name="EquipmentProjectID"></param>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        public static bool CheckProject(string EquipmentProjectID, string EquipmentID)
        {
            string sql = String.Format(@"select Top 1 * from [EMS_EquipmentInspectionProject] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(EquipmentProjectID))
                sql = sql + String.Format(@" and [EquipmentProjectID] = '{0}' ", EquipmentProjectID);

            if (!string.IsNullOrWhiteSpace(EquipmentID))
                sql = sql + String.Format(@" and [EquipmentID] = '{0}' ", EquipmentID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取设备巡检项目的弹窗
        /// SAM 2017年6月8日15:09:29
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetEquipmentInspectionProjectList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.EIProjectID,A.EquipmentID,A.ProjectID,A.EquipmentProjectID,A.Sequence,
            E.Code as EquipmentCode,E.Name as EquipmentName,F.Name as Status,
            D.Code as ProjectCode,D.Description as ProjectDescription,       
            (Select [Code] from [SYS_Parameters] where ParameterID = D.[Attribute]) as AttributeCode,
            G.StandardValue,G.MaxValue,G.MinValue,A.Comments,     
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [EMS_EquipmentInspectionProject] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Projects] D on A.[ProjectID] = D.[ProjectID]
            left join [EMS_Equipment] E on A.[EquipmentID] = E.[EquipmentID]
            left join [SYS_Parameters] F on A.[Status] = F.[ParameterID]
            left join [EMS_EquipmentProject] G on A.[EquipmentProjectID] = G.[EquipmentProjectID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and E.[Status] = '{0}0201213000001'", Framework.SystemID);

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
                sql = sql + string.Format(@" and E.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            count = UniversalService.getCount(sql, Parcount);

            String orderby = " E.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


    }
}

