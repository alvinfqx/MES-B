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
    public class SYS_ResourceDetailsService : SuperModel<SYS_ResourceDetails>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年5月12日11:09:25
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_ResourceDetails Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_ResourceDetails]([ResourceDetailID],[ResourceID],[DetailID],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                (@ResourceDetailID,@ResourceID,@DetailID,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ResourceDetailID",SqlDbType.VarChar),
                    new SqlParameter("@ResourceID",SqlDbType.VarChar),
                    new SqlParameter("@DetailID",SqlDbType.VarChar),             
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    };

                parameters[0].Value = (Object)Model.ResourceDetailID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.ResourceID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.DetailID ?? DBNull.Value;
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
        /// SAM 2017年5月12日11:09:43
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_ResourceDetails Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_ResourceDetails] set {0},
                    [Status]=@Status,[Comments]=@Comments 
                    where [ResourceDetailID]=@ResourceDetailID", UniversalService.getUpdateUTC(userId));

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ResourceDetailID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.ResourceDetailID;
                parameters[1].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017年5月12日11:09:59
        /// </summary>
        /// <param name="ResourceDetailID"></param>
        /// <returns></returns>
        public static SYS_ResourceDetails get(string ResourceDetailID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_ResourceDetails] where [ResourceDetailID] = '{0}'  and [SystemID] = '{1}' ", ResourceDetailID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 判断代号是否重复
        /// SAM 2017年5月12日11:45:50
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="ResourceDetailID"></param>
        /// <returns></returns>
        public static bool CheckDetail(string Detail,string ResourceID, string ResourceDetailID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_ResourceDetails] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' and [ResourceID] = '{1}'", Framework.SystemID, ResourceID);

            /*先定义Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Detail",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            /*因为Code是通过手动输入的，所以需要用参数的形式去拼SQL*/
            if (!string.IsNullOrWhiteSpace(Detail))
            {
                sql = sql + string.Format(@" and [DetailID] =@Detail ");
                parameters[0].Value = Detail;
            }

            /*ResourceID（流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(ResourceDetailID))
                sql = sql + String.Format(@" and [ResourceDetailID] <> '{0}' ", ResourceDetailID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }



        /// <summary>
        /// 资源明细列表
        /// SAM 2017年5月12日11:34:22
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00015GetDetailsList(string ResourceID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ResourceDetailID,A.DetailID,A.ResourceID,A.Status,A.Comments,
            B.Code as DetailCode,B.Name as DetailName,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_ResourceDetails] A 
            left join [EMS_Equipment] B on A.DetailID = B.EquipmentID
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003' and A.[ResourceID]='{1}' ", Framework.SystemID, ResourceID);

    
            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Status],B.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 资源明细列表（L）
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00015GetLDetailsList(string ResourceID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ResourceDetailID,A.DetailID,A.ResourceID,A.Status,A.Comments,
            B.Emplno as DetailCode,B.UserName as DetailName,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_ResourceDetails] A 
            left join [SYS_MESUsers] B on A.DetailID = B.MESUserID
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003' and A.[ResourceID]='{1}' ", Framework.SystemID, ResourceID);


            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Status],B.[Emplno] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }



        /// <summary>
        /// 判断资源明细是否已使用设备
        /// SAM 2017年5月22日14:56:12
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        public static bool CheckEquipment(string EquipmentID)
        {
            string sql = string.Format(@"select * from [SYS_ResourceDetails] where  [DetailID] = '{0}' and [SystemID] = '{1}' and [Status] <> '{1}0201213000003' ", EquipmentID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// Sfc00004获取资源明细的专属弹窗（其他）
        /// SAM 2017年7月10日17:30:05
        /// </summary>
        /// <param name="FabricatedProcessID"></param>
        /// <param name="FabricatedOPerationID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00004GetResourceDetailList(string FabricatedProcessID,string FabMoOperationID, int page,int rows,ref int count)
        {
            string select = string.Format(@"select F.EquipmentID,F.Code,F.Name,E.ClassID,A.ResourceID,E.Code as ResourceCode,E.ClassID as ResourceClassID,
            (select Code from [SYS_Parameters] where ParameterID=E.ClassID) as ClassCode,
            (select Top 1 [IfMain] from  [SFC_FabMoResource] where ResourceID=A.ResourceID and ([FabMoProcessID]='{0}' or [FabMoOperationID]='{1}')) as IfMain,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", FabricatedProcessID, FabMoOperationID);
            
            string sql = string.Format(@"  from [SYS_ResourceDetails] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Resources] E on A.[ResourceID] = E.[ResourceID]
            left join [EMS_Equipment] F on A.[DetailID] = F.[EquipmentID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID);

            if(string.IsNullOrWhiteSpace(FabMoOperationID))
                sql += String.Format(@" and A.[ResourceID] in ( select [ResourceID] from [SFC_FabMoResource] where [FabMoProcessID] ='{1}' and [Type] ='{0}0201213000086' and [Status]='{0}0201213000001')",Framework.SystemID, FabricatedProcessID);
            else
                sql += String.Format(@" and A.[ResourceID] in ( select [ResourceID] from [SFC_FabMoResource] where [FabMoOperationID] ='{1}' and [Type] ='{0}0201213000086' and [Status]='{0}0201213000001')", Framework.SystemID, FabMoOperationID);

            count = UniversalService.getCount(sql, null);

            string orderby = " A.[ID] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// Sfc00004获取资源明细的专属弹窗（机器）
        /// SAM 2017年7月10日17:29:53
        /// </summary>
        /// <param name="FabricatedProcessID"></param>
        /// <param name="FabricatedOPerationID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00004GetMResourceDetailList(string FabricatedProcessID, string FabMoOperationID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select F.EquipmentID,F.Code,F.Name,E.ClassID,A.ResourceID,E.Code as ResourceCode,E.ClassID as ResourceClassID,
            (select Code from  [SYS_Parameters] where ParameterID=E.ClassID) as ClassCode, F.Condition,
            (select Top 1 [IfMain] from  [SFC_FabMoResource] where ResourceID=A.ResourceID and ([FabMoProcessID]='{0}' or [FabMoOperationID]='{1}')) as IfMain,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", FabricatedProcessID, FabMoOperationID);

            string sql = string.Format(@"  from [SYS_ResourceDetails] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Resources] E on A.[ResourceID] = E.[ResourceID]
            left join [EMS_Equipment] F on A.[DetailID] = F.[EquipmentID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID);

            if (string.IsNullOrWhiteSpace(FabMoOperationID))
                sql += String.Format(@" and A.[ResourceID] in ( select [ResourceID] from [SFC_FabMoResource] where [FabMoProcessID] ='{1}' and [Type] ='{0}0201213000084' and [Status]='{0}0201213000001')", Framework.SystemID, FabricatedProcessID);
            else
                sql += String.Format(@" and A.[ResourceID] in ( select [ResourceID] from [SFC_FabMoResource] where [FabMoOperationID] ='{1}' and [Type] ='{0}0201213000084' and [Status]='{0}0201213000001')", Framework.SystemID, FabMoOperationID);

            count = UniversalService.getCount(sql, null);

            string orderby = " A.[ID] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// Sfc00004获取资源明细的专属弹窗（人工）
        /// SAM 2017年7月10日17:30:19
        /// </summary>
        /// <param name="FabricatedProcessID"></param>
        /// <param name="FabricatedOPerationID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00004GetLResourceDetailList(string FabMoProcessID, string FabMoOperationID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select F.MESUserID as EquipmentID,F.Emplno,F.UserName,F.Status,E.ClassID,A.ResourceID,E.Code as ResourceCode,E.ClassID as ResourceClassID,
            (select Code from  [SYS_Parameters] where ParameterID=E.ClassID) as ClassCode,
            (select Top 1 [IfMain] from  [SFC_FabMoResource] where ResourceID=A.ResourceID and ([FabMoProcessID]='{0}' or [FabMoOperationID]='{1}')) as IfMain,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", FabMoProcessID, FabMoOperationID);

            string sql = string.Format(@"  from [SYS_ResourceDetails] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Resources] E on A.[ResourceID] = E.[ResourceID]
            left join [SYS_MESUsers] F on A.[DetailID] = F.[MESUserID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID);

            if (string.IsNullOrWhiteSpace(FabMoOperationID))
                sql += String.Format(@" and A.[ResourceID] in ( select [ResourceID] from [SFC_FabMoResource] where [FabMoProcessID] ='{1}' and [Type] ='{0}0201213000085' and [Status]='{0}0201213000001')", Framework.SystemID, FabMoProcessID);
            else
                sql += String.Format(@" and A.[ResourceID] in ( select [ResourceID] from [SFC_FabMoResource] where [FabMoOperationID] ='{1}' and [Type] ='{0}0201213000085' and [Status]='{0}0201213000001')", Framework.SystemID, FabMoOperationID);

            count = UniversalService.getCount(sql, null);

            string orderby = " A.[ID] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }



        /// <summary>
        ///  根据资源流水号获取资源明细（不分页）（非人工）
        ///  SAM 2017年7月30日22:28:53
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00015MDetailList(string ResourceID)
        {
            string select = string.Format(@"select A.ResourceDetailID,A.DetailID,A.ResourceID,
           (select [Name] from [SYS_Parameters] where B.[Status] = [ParameterID]) as Status,
            B.Code as DetailCode,B.Name as DetailName,
            (select [Code] from [SYS_Organization] where [OrganizationID] = B.[OrganizationID]) as  OrganizationCode,
            (select [Name] from [SYS_Organization] where [OrganizationID] = B.[OrganizationID]) as  OrganizationName ");

            string sql = string.Format(@"  from [SYS_ResourceDetails] A 
            left join [EMS_Equipment] B on A.[DetailID] = B.[EquipmentID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and [ResourceID] = '{1}'", Framework.SystemID, ResourceID);


            String orderby = "order by B.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据资源流水号获取资源明细（不分页）（人工）
        /// SAM 2017年7月30日22:39:47
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00015LDetailList(string ResourceID)
        {
            string select = string.Format(@"select A.ResourceDetailID,A.DetailID,A.ResourceID,
            B.Emplno as DetailCode,B.UserName as DetailName,B.[Status],
            (select [Code] from [SYS_Organization] where [OrganizationID] = (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where [UserID] =A.[DetailID])) as  OrganizationCode,
            (select [Name] from [SYS_Organization] where [OrganizationID] = (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where [UserID] =A.[DetailID])) as  OrganizationName ");

            string sql = string.Format(@"  from [SYS_ResourceDetails] A 
            left join [SYS_MESUsers] B on A.[DetailID] = B.[MESUserID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and [ResourceID] = '{1}'", Framework.SystemID, ResourceID);


            String orderby = "order by B.[Emplno] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }



        /// <summary>
        /// 根据资源流水号获取不属于他的资源明细（不分页）（非人工）
        /// SAM 2017年7月30日22:43:14
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00015NoMDetailList(string ResourceID)
        {
            string select = string.Format(@"select null as ResourceDetailID,
             A.EquipmentID as DetailID,A.Code as DetailCode,A.Name as DetailName,
            (select [Name] from [SYS_Parameters] where A.[Status] = [ParameterID]) as Status,
            (select [Code] from [SYS_Organization] where [OrganizationID] = A.[OrganizationID]) as  OrganizationCode,
            (select [Name] from [SYS_Organization] where [OrganizationID] = A.[OrganizationID]) as  OrganizationName ");

            string sql = string.Format(@"  from [EMS_Equipment] A 
            where [EquipmentID] not in (select [DetailID] from [SYS_ResourceDetails] where [ResourceID] ='{1}' and [Status] = '{0}0201213000001')
            and A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID, ResourceID);

            string orderBy = "order By A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据资源流水号获取不属于他的资源明细（不分页）（人工）
        /// SAM 2017年7月30日22:46:27
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00015NoLDetailList(string ResourceID)
        {
            string select = string.Format(@"select null as ResourceDetailID,
             A.MESUserID as DetailID,A.Emplno as DetailCode,A.UserName as DetailName, A.Status,
            (select [Code] from [SYS_Organization] where [OrganizationID] = (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where [UserID] =A.[MESUserID])) as  OrganizationCode,
            (select [Name] from [SYS_Organization] where [OrganizationID] = (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where [UserID] =A.[MESUserID])) as  OrganizationName ");

            string sql = string.Format(@"  from [SYS_MESUsers] A 
            where [MESUserID] not in (select [DetailID] from [SYS_ResourceDetails] where [ResourceID] ='{1}' and [Status] = '{0}0201213000001')
            and A.[SystemID]='{0}' and A.[Status] = 1 ", Framework.SystemID, ResourceID);

            string orderBy = "order By [OrganizationCode],A.[Emplno] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 根据资源流水号获取不属于他的资源明细列表（不分页）（非人工）
        /// SAM 2017年10月26日22:03:17
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00015NoMDetailListV1(string ResourceID,string Code)
        {
            string select = string.Format(@"select null as ResourceDetailID,
             A.EquipmentID as DetailID,A.Code as DetailCode,A.Name as DetailName,
            (select [Name] from [SYS_Parameters] where A.[Status] = [ParameterID]) as Status,
            (select [Code] from [SYS_Organization] where [OrganizationID] = A.[OrganizationID]) as  OrganizationCode,
            (select [Name] from [SYS_Organization] where [OrganizationID] = A.[OrganizationID]) as  OrganizationName ");

            string sql = string.Format(@"  from [EMS_Equipment] A 
            where [EquipmentID] not in (select [DetailID] from [SYS_ResourceDetails] where [ResourceID] ='{1}' and [Status] = '{0}0201213000001')
            and A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID, ResourceID);

            if (!string.IsNullOrWhiteSpace(Code))
                sql += string.Format(@"and A.Code collate Chinese_PRC_CI_AS like '%{0}%'", Code);

            string orderBy = "order By A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据资源流水号获取不属于他的资源明细（不分页）（人工）
        /// SAM 2017年7月30日22:46:27
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00015NoLDetailListV1(string ResourceID, string Code)
        {
            string select = string.Format(@"select null as ResourceDetailID,
             A.MESUserID as DetailID,A.Emplno as DetailCode,A.UserName as DetailName, A.Status,
            (select [Code] from [SYS_Organization] where [OrganizationID] = (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where [UserID] =A.[MESUserID])) as  OrganizationCode,
            (select [Name] from [SYS_Organization] where [OrganizationID] = (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where [UserID] =A.[MESUserID])) as  OrganizationName ");

            string sql = string.Format(@"  from [SYS_MESUsers] A 
            where [MESUserID] not in (select [DetailID] from [SYS_ResourceDetails] where [ResourceID] ='{1}' and [Status] = '{0}0201213000001')
            and A.[SystemID]='{0}' and A.[Status] = 1 ", Framework.SystemID, ResourceID);

            if (!string.IsNullOrWhiteSpace(Code))
                sql += string.Format(@"and A.Emplno collate Chinese_PRC_CI_AS like '%{0}%'", Code);

            string orderBy = "order By A.[Emplno] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 将不在本资源范围内的映射都删除
        /// SAM 2017年7月30日22:57:13
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="DetailIDs"></param>
        /// <param name="ResourceID"></param>
        /// <returns></returns>
        public static bool Delete(string userId, string DetailIDs, string ResourceID)
        {
            try
            {
                string sql = string.Format(@"update[SYS_ResourceDetails] set {0},
               [Status]='{1}0201213000003' where [ResourceID]='{2}' ", UniversalService.getUpdateUTC(userId), Framework.SystemID, ResourceID);

                if (!string.IsNullOrWhiteSpace(DetailIDs))
                    sql += string.Format(@" and [ResourceDetailID] not in ('{0}')", DetailIDs);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 判断是否已存在资源与明细的映射
        /// SAM 2017年7月30日23:00:08
        /// </summary>
        /// <param name="DetailID"></param>
        /// <param name="ResourceID"></param>
        /// <returns></returns>
        public static bool CheckDetail(string DetailID, string ResourceID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_ResourceDetails] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(DetailID))
                sql = sql + String.Format(@" and [DetailID] = '{0}' ", DetailID);

            if (!string.IsNullOrWhiteSpace(ResourceID))
                sql = sql + String.Format(@" and [ResourceID] = '{0}' ", ResourceID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
    }
}

