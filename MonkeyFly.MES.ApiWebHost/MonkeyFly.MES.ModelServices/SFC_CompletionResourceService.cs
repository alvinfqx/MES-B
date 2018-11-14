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
    public class SFC_CompletionResourceService : SuperModel<SFC_CompletionResource>
    {
       /// <summary>
       /// 新增
       /// SAM 2017年9月18日14:30:00
       /// </summary>
       /// <param name="userId"></param>
       /// <param name="Model"></param>
       /// <returns></returns>
        public static bool insert(string userId, SFC_CompletionResource Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_CompletionResource]([CompletionResourceID],[CompletionOrderID],
                [Sequence],[ResourceClassID],[EquipmentID],
                [Hour],[Status],[Comments],
                [Modifier],[ModifiedTime],[ModifiedLocalTime],
                [Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@CompletionResourceID,@CompletionOrderID,
                @Sequence,@ResourceClassID,@EquipmentID,
                @Hour,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CompletionResourceID",SqlDbType.VarChar),
                    new SqlParameter("@CompletionOrderID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@ResourceClassID",SqlDbType.VarChar),
                    new SqlParameter("@EquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@Hour",SqlDbType.BigInt),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.CompletionResourceID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.CompletionOrderID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ResourceClassID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.EquipmentID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Hour ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017年7月20日10:19:52
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SFC_CompletionResource Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_CompletionResource] set {0},
                [Sequence]=@Sequence,[ResourceClassID]=@ResourceClassID,
                [EquipmentID]=@EquipmentID,[Hour]=@Hour,[Status]=@Status,
                [Comments]=@Comments 
                where [CompletionResourceID]=@CompletionResourceID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CompletionResourceID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@ResourceClassID",SqlDbType.VarChar),
                    new SqlParameter("@EquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@Hour",SqlDbType.BigInt),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.CompletionResourceID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ResourceClassID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.EquipmentID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Hour ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Status ?? DBNull.Value;
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
        /// 获取单一实体
        /// SAM 2017年7月20日10:19:44
        /// </summary>
        /// <param name="CompletionResourceID"></param>
        /// <returns></returns>
        public static SFC_CompletionResource get(string CompletionResourceID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_CompletionResource] where [CompletionResourceID] = '{0}'  and [SystemID] = '{1}' ", CompletionResourceID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 完工單回報作業-資源報工列表
        /// SAM 2017年7月20日09:45:53
        /// </summary>
        /// <param name="CompletionOrderID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00007GetResourceReportingList(string CompletionOrderID, int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select A.CompletionResourceID,A.CompletionOrderID,A.Sequence,A.ResourceClassID,A.EquipmentID,A.Status,
            convert(varchar(20),cast(A.Hour/3600 as int))+':'+right('00'+convert(varchar(20),A.Hour%3600/60),2)+':'+right('00'+convert(varchar(20),A.Hour%3600%60),2) as Hour,
            (CASE WHEN A.EquipmentID like '{0}045%' THEN (select [Code] From [EMS_Equipment] where EquipmentID=A.EquipmentID) ELSE (Select [Emplno] from [SYS_MESUsers] where A.EquipmentID=MESUserID) END) as DisplayCode,
            (CASE WHEN A.EquipmentID like '{0}045%' THEN (select [Name] From [EMS_Equipment] where EquipmentID=A.EquipmentID) ELSE (Select [UserName] from [SYS_MESUsers] where A.EquipmentID=MESUserID) END) as DisplayName,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,A.Comments,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);
            string sql = string.Format(
                @"from [SFC_CompletionResource] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' and A.[CompletionOrderID]='{1}'", Framework.SystemID, CompletionOrderID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 人工时统计分析列表
        /// SAM 2017年7月22日18:14:58
        /// </summary>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartUserCode"></param>
        /// <param name="EndUserCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00014GetList(string StartWorkCenterCode, string EndWorkCenterCode, string StartUserCode, string EndUserCode, string StartDate, string EndDate, int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select A.CompletionResourceID,D.Code as WorkCenterCode,D.Name as WorkCenterName,
            (CASE WHEN D.DepartmentID = '{0}005%' THEN (select [Code] From [SYS_Organization] where OrganizationID=D.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where D.DepartmentID=ManufacturerID) END) as DeptCode,
            (CASE WHEN D.DepartmentID = '{0}005%' THEN (select [Name] From [SYS_Organization] where OrganizationID=D.DepartmentID) ELSE (Select [Name] from [SYS_Manufacturers] where D.DepartmentID=ManufacturerID) END) as DeptName,
            E.Emplno,E.UserName,
            (select Code from [SYS_Organization] where [OrganizationID] = (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where E.MESUserID = UserID )) as UserDeptCode,
            (select Name from [SYS_Organization] where [OrganizationID] = (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where E.MESUserID = UserID )) as UserDeptName,
             convert(varchar(8),dateadd(ss,B.LaborHour,108),108) as LaborHour,
             convert(varchar(8),dateadd(ss,B.UnLaborHour,108),108) as UnLaborHour,
             convert(varchar(8),dateadd(ss,A.Hour,108),108) as Hour ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_CompletionResource] A               
                 left join [SFC_CompletionOrder] B on A.[CompletionOrderID] = B.[CompletionOrderID]
                 left join [SFC_FabMoProcess] C on B.[FabMoProcessID] =C.[FabMoProcessID]
                 left join [SYS_WorkCenter] D on C.[WorkCenterID] =D.[WorkCenterID]
                left join [SYS_MESUsers] E on A.[EquipmentID] =E.[MESUserID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' 
                and A.[ResourceClassID] ='{0}0201213000047' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartUserCode",SqlDbType.VarChar),
                new SqlParameter("@EndUserCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;
            parameters[5].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartUserCode",SqlDbType.VarChar),
                new SqlParameter("@EndUserCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartWorkCenterCode))
            {
                sql = sql + String.Format(@" and D.[Code] >= @StartWorkCenterCode ");
                parameters[0].Value = StartWorkCenterCode;
                Parcount[0].Value = StartWorkCenterCode;
            }

            if (!string.IsNullOrWhiteSpace(EndWorkCenterCode))
            {
                sql = sql + String.Format(@" and D.[Code] <= @EndWorkCenterCode ");
                parameters[1].Value = EndWorkCenterCode;
                Parcount[1].Value = EndWorkCenterCode;
            }

            if (!string.IsNullOrWhiteSpace(StartUserCode))
            {
                sql = sql + String.Format(@" and E.[Emplno] >= @StartUserCode ");
                parameters[2].Value = StartUserCode;
                Parcount[2].Value = StartUserCode;
            }

            if (!string.IsNullOrWhiteSpace(EndUserCode))
            {
                sql = sql + String.Format(@" and E.[Emplno] <= @EndUserCode ");
                parameters[3].Value = EndUserCode;
                Parcount[3].Value = EndUserCode;
            }

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + String.Format(@" and B.[Date] >= @StartDate ");
                parameters[4].Value = StartDate;
                Parcount[4].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + String.Format(@" and B.[Date] <= @EndDate ");
                parameters[5].Value = EndDate;
                Parcount[5].Value = EndDate;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "D.[Code],E.[Emplno] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 人工时统计分析导出
        /// SAM 2017年7月22日18:46:59
        /// </summary>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartUserCode"></param>
        /// <param name="EndUserCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static DataTable Sfc00014Export(string StartWorkCenterCode, string EndWorkCenterCode, string StartUserCode, string EndUserCode, string StartDate, string EndDate)
        {
            string select = string.Format(
             @"select ROW_NUMBER() OVER (ORDER BY D.[Code],E.[Emplno]),
                D.Code as WorkCenterCode,D.Name as WorkCenterName,
            (CASE WHEN D.DepartmentID = '{0}005%' THEN (select [Code] From [SYS_Organization] where OrganizationID=D.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where D.DepartmentID=ManufacturerID) END) as DeptCode,
            (CASE WHEN D.DepartmentID = '{0}005%' THEN (select [Name] From [SYS_Organization] where OrganizationID=D.DepartmentID) ELSE (Select [Name] from [SYS_Manufacturers] where D.DepartmentID=ManufacturerID) END) as DeptName,
            E.Emplno,E.UserName,
            (select Code from [SYS_Organization] where [OrganizationID] = (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where E.MESUserID = UserID )) as UserDeptCode,
            (select Name from [SYS_Organization] where [OrganizationID] = (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where E.MESUserID = UserID )) as UserDeptName,
             convert(varchar(8),dateadd(ss,B.LaborHour,108),108) as LaborHour,
             convert(varchar(8),dateadd(ss,B.UnLaborHour,108),108) as UnLaborHour,
             convert(varchar(8),dateadd(ss,A.Hour,108),108) as Hour ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_CompletionResource] A               
                 left join [SFC_CompletionOrder] B on A.[CompletionOrderID] = B.[CompletionOrderID]
                 left join [SFC_FabMoProcess] C on B.[FabMoProcessID] =C.[FabMoProcessID]
                 left join [SYS_WorkCenter] D on C.[WorkCenterID] =D.[WorkCenterID]
                left join [SYS_MESUsers] E on A.[EquipmentID] =E.[MESUserID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' 
                and A.[ResourceClassID] in (select [ParameterID] from [SYS_Parameters] where Code ='L' and [ParameterTypeID] ='{0}0191213000013' and [IsEnable]=1)", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartUserCode",SqlDbType.VarChar),
                new SqlParameter("@EndUserCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;
            parameters[5].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartUserCode",SqlDbType.VarChar),
                new SqlParameter("@EndUserCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartWorkCenterCode))
            {
                sql = sql + String.Format(@" and D.[Code] >= @StartWorkCenterCode ");
                parameters[0].Value = StartWorkCenterCode;
                Parcount[0].Value = StartWorkCenterCode;
            }

            if (!string.IsNullOrWhiteSpace(EndWorkCenterCode))
            {
                sql = sql + String.Format(@" and D.[Code] <= @EndWorkCenterCode ");
                parameters[1].Value = EndWorkCenterCode;
                Parcount[1].Value = EndWorkCenterCode;
            }

            if (!string.IsNullOrWhiteSpace(StartUserCode))
            {
                sql = sql + String.Format(@" and E.[Emplno] >= @StartUserCode ");
                parameters[2].Value = StartUserCode;
                Parcount[2].Value = StartUserCode;
            }

            if (!string.IsNullOrWhiteSpace(EndUserCode))
            {
                sql = sql + String.Format(@" and E.[Emplno] <= @EndUserCode ");
                parameters[3].Value = EndUserCode;
                Parcount[3].Value = EndUserCode;
            }

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + String.Format(@" and B.[Date] >= @StartDate ");
                parameters[4].Value = StartDate;
                Parcount[4].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + String.Format(@" and B.[Date] <= @EndDate ");
                parameters[5].Value = EndDate;
                Parcount[5].Value = EndDate;
            }

            string orderby = "order By D.[Code],E.[Emplno] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select+sql+orderby,CommandType.Text, parameters);

            return dt;
        }


        /// <summary>
        /// 机器工时统计分析
        /// SAM 2017年7月22日18:42:37
        /// </summary>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartUserCode"></param>
        /// <param name="EndUserCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00015GetList(string StartWorkCenterCode, string EndWorkCenterCode, string StartUserCode, string EndUserCode, string StartDate, string EndDate, int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select A.CompletionResourceID,D.Code as WorkCenterCode,D.Name as WorkCenterName,
            (CASE WHEN D.DepartmentID = '{0}005%' THEN (select [Code] From [SYS_Organization] where OrganizationID=D.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where D.DepartmentID=ManufacturerID) END) as DeptCode,
            (CASE WHEN D.DepartmentID = '{0}005%' THEN (select [Name] From [SYS_Organization] where OrganizationID=D.DepartmentID) ELSE (Select [Name] from [SYS_Manufacturers] where D.DepartmentID=ManufacturerID) END) as DeptName,
            E.Code as EquipmentCode,E.Name as EquipmentName,
             convert(varchar(8),dateadd(ss,B.MachineHour,108),108) as MachineHour,
             convert(varchar(8),dateadd(ss,B.UnMachineHour,108),108) as UnMachineHour,
             convert(varchar(8),dateadd(ss,A.Hour,108),108) as Hour ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_CompletionResource] A               
                 left join [SFC_CompletionOrder] B on A.[CompletionOrderID] = B.[CompletionOrderID]
                 left join [SFC_FabMoProcess] C on B.[FabMoProcessID] =C.[FabMoProcessID]
                 left join [SYS_WorkCenter] D on C.[WorkCenterID] =D.[WorkCenterID]
                left join [EMS_Equipment] E on A.[EquipmentID] =E.[EquipmentID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' 
                and A.[ResourceClassID] ='{0}0201213000048' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartUserCode",SqlDbType.VarChar),
                new SqlParameter("@EndUserCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;
            parameters[5].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartUserCode",SqlDbType.VarChar),
                new SqlParameter("@EndUserCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartWorkCenterCode))
            {
                sql = sql + String.Format(@" and D.[Code] >= @StartWorkCenterCode ");
                parameters[0].Value = StartWorkCenterCode;
                Parcount[0].Value = StartWorkCenterCode;
            }

            if (!string.IsNullOrWhiteSpace(EndWorkCenterCode))
            {
                sql = sql + String.Format(@" and D.[Code] <= @EndWorkCenterCode ");
                parameters[1].Value = EndWorkCenterCode;
                Parcount[1].Value = EndWorkCenterCode;
            }

            if (!string.IsNullOrWhiteSpace(StartUserCode))
            {
                sql = sql + String.Format(@" and E.[Code] >= @StartUserCode ");
                parameters[2].Value = StartUserCode;
                Parcount[2].Value = StartUserCode;
            }

            if (!string.IsNullOrWhiteSpace(EndUserCode))
            {
                sql = sql + String.Format(@" and E.[Code] <= @EndUserCode ");
                parameters[3].Value = EndUserCode;
                Parcount[3].Value = EndUserCode;
            }

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + String.Format(@" and B.[Date] >= @StartDate ");
                parameters[4].Value = StartDate;
                Parcount[4].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + String.Format(@" and B.[Date] <= @EndDate ");
                parameters[5].Value = EndDate;
                Parcount[5].Value = EndDate;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "D.[Code],E.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 机器工时统计分析导出
        /// SAM 2017年7月22日18:53:25
        /// </summary>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartUserCode"></param>
        /// <param name="EndUserCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static DataTable Sfc00015Export(string StartWorkCenterCode, string EndWorkCenterCode, string StartUserCode, string EndUserCode, string StartDate, string EndDate)
        {
            string select = string.Format(
             @"select ROW_NUMBER() OVER (ORDER BY D.[Code],E.[Code]),D.Code as WorkCenterCode,D.Name as WorkCenterName,
            (CASE WHEN D.DepartmentID = '{0}005%' THEN (select [Code] From [SYS_Organization] where OrganizationID=D.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where D.DepartmentID=ManufacturerID) END) as DeptCode,
            (CASE WHEN D.DepartmentID = '{0}005%' THEN (select [Name] From [SYS_Organization] where OrganizationID=D.DepartmentID) ELSE (Select [Name] from [SYS_Manufacturers] where D.DepartmentID=ManufacturerID) END) as DeptName,
            E.Code as EquipmentCode,E.Name as EquipmentName,
             convert(varchar(8),dateadd(ss,B.MachineHour,108),108) as MachineHour,
             convert(varchar(8),dateadd(ss,B.UnMachineHour,108),108) as UnMachineHour,
             convert(varchar(8),dateadd(ss,A.Hour,108),108) as Hour ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_CompletionResource] A               
                 left join [SFC_CompletionOrder] B on A.[CompletionOrderID] = B.[CompletionOrderID]
                 left join [SFC_FabMoProcess] C on B.[FabMoProcessID] =C.[FabMoProcessID]
                 left join [SYS_WorkCenter] D on C.[WorkCenterID] =D.[WorkCenterID]
                left join [EMS_Equipment] E on A.[EquipmentID] =E.[EquipmentID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' 
                and A.[ResourceClassID] ='{0}0201213000048' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartUserCode",SqlDbType.VarChar),
                new SqlParameter("@EndUserCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;
            parameters[5].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartUserCode",SqlDbType.VarChar),
                new SqlParameter("@EndUserCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartWorkCenterCode))
            {
                sql = sql + String.Format(@" and D.[Code] >= @StartWorkCenterCode ");
                parameters[0].Value = StartWorkCenterCode;
                Parcount[0].Value = StartWorkCenterCode;
            }

            if (!string.IsNullOrWhiteSpace(EndWorkCenterCode))
            {
                sql = sql + String.Format(@" and D.[Code] <= @EndWorkCenterCode ");
                parameters[1].Value = EndWorkCenterCode;
                Parcount[1].Value = EndWorkCenterCode;
            }

            if (!string.IsNullOrWhiteSpace(StartUserCode))
            {
                sql = sql + String.Format(@" and E.[Code] >= @StartUserCode ");
                parameters[2].Value = StartUserCode;
                Parcount[2].Value = StartUserCode;
            }

            if (!string.IsNullOrWhiteSpace(EndUserCode))
            {
                sql = sql + String.Format(@" and E.[Code] <= @EndUserCode ");
                parameters[3].Value = EndUserCode;
                Parcount[3].Value = EndUserCode;
            }

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + String.Format(@" and B.[Date] >= @StartDate ");
                parameters[4].Value = StartDate;
                Parcount[4].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + String.Format(@" and B.[Date] <= @EndDate ");
                parameters[5].Value = EndDate;
                Parcount[5].Value = EndDate;
            }


            string orderby = " order by D.[Code],E.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select+sql+orderby,CommandType.Text, parameters);

            return dt;
        }


        /// <summary>
        /// 根据完工单号和资源类别获取资源报工工时总和
        /// SAM 2017年9月18日14:37:27
        /// </summary>
        /// <param name="ResourceClassID"></param>
        /// <param name="CompletionOrderID"></param>
        /// <returns></returns>
        public static int GetSum(string ResourceClassID, string CompletionOrderID)
        {
            string sql = string.Format(@"select isnull(sum(Hour), 0) from [SFC_CompletionResource] where [ResourceClassID] = '{0}'  and [SystemID] = '{1}' and [Status] <> '{0}0201213000003' ", ResourceClassID, Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(CompletionOrderID))
                sql += string.Format(@" and [CompletionOrderID] ='{0}'", CompletionOrderID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            return int.Parse(dt.Rows[0][0].ToString());
        }
    }
}

