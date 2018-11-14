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
    public class EMS_EquipmentMaintenanceListDetailsService : SuperModel<EMS_EquipmentMaintenanceListDetails>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年7月5日15:17:03
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, EMS_EquipmentMaintenanceListDetails Model)
        {
            try
            {
                string sql = string.Format(@"insert[EMS_EquipmentMaintenanceListDetails]([EquipmentMaintenanceListDetailID],[EquipmentMaintenanceListID],
              [Sequence],[Type],[DetailID],[Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
             (@EquipmentMaintenanceListDetailID,@EquipmentMaintenanceListID,
            @Sequence,@Type,@DetailID,
            @Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@EquipmentMaintenanceListDetailID",SqlDbType.VarChar),
                    new SqlParameter("@EquipmentMaintenanceListID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Type",SqlDbType.TinyInt),
                    new SqlParameter("@DetailID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.EquipmentMaintenanceListDetailID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.EquipmentMaintenanceListID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[4].Value = (Object)Model.DetailID ?? DBNull.Value;
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
        /// 更新
        /// SAM 2017年7月5日15:17:08
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, EMS_EquipmentMaintenanceListDetails Model)
        {
            try
            {
                string sql = String.Format(@"update[EMS_EquipmentMaintenanceListDetails] set {0},
                [DetailID]=@DetailID,[Status]=@Status 
                where [EquipmentMaintenanceListDetailID]=@EquipmentMaintenanceListDetailID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@EquipmentMaintenanceListDetailID",SqlDbType.VarChar),
                    new SqlParameter("@DetailID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.EquipmentMaintenanceListDetailID;
                parameters[1].Value = (Object)Model.DetailID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Status ?? DBNull.Value;

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
        /// SAM 2017年7月5日15:17:20
        /// </summary>
        /// <param name="InspectionDocumentRemarkID"></param>
        /// <returns></returns>
        public static EMS_EquipmentMaintenanceListDetails get(string InspectionDocumentRemarkID)
        {
            string sql = string.Format(@"select Top 1 * from [EMS_EquipmentMaintenanceListDetails] where [EquipmentMaintenanceListDetailID] = '{0}'  and [SystemID] = '{1}' ", InspectionDocumentRemarkID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 判断是否重复
        /// SAM 2017年7月5日15:34:27
        /// </summary>
        /// <param name="DetailID"></param>
        /// <param name="Type"></param>
        /// <param name="EquipmentMaintenanceListDetailID"></param>
        /// <returns></returns>
        public static bool CheckDetail(string DetailID, int Type,string EquipmentMaintenanceListID,string EquipmentMaintenanceListDetailID)
        {
            string sql = String.Format(@"select Top 1 * from [EMS_EquipmentMaintenanceListDetails] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' and [Type] = {1}", Framework.SystemID, Type);

            if (!string.IsNullOrWhiteSpace(EquipmentMaintenanceListID))
                sql = sql + String.Format(@" and [EquipmentMaintenanceListID] = '{0}' ", EquipmentMaintenanceListID);

            /*先定义Name和Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@DetailID",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(DetailID))
            {
                sql = sql + String.Format(@" and [DetailID] =@DetailID ");
                parameters[0].Value = DetailID;
            }

            /*EquipmentMaintenanceListID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(EquipmentMaintenanceListDetailID))
                sql = sql + String.Format(@" and [EquipmentMaintenanceListDetailID] <> '{0}' ", EquipmentMaintenanceListDetailID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 设备设定列表
        /// SAM 2017年7月5日15:19:22
        /// </summary>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00008GetDeviceSettingList(string EquipmentMaintenanceListID, string Code, string Name, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.EquipmentMaintenanceListDetailID,A.EquipmentMaintenanceListID,A.Sequence,A.Comments,A.DetailID,
            D.Code as EquipmentCode,D.Name as EquipmentName,
            (Select [Name] from [SYS_Parameters] where [ParameterID] = D.[Status]) as Status,
            (Select [Code] from [SYS_Parameters] where [ParameterID] = D.[ResourceCategory]) as ResourceCategory,
            (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [EMS_EquipmentMaintenanceListDetails] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [EMS_Equipment] D on A.DetailID = D.EquipmentID
            where A.[SystemID]='{0}' and  A.[Status] <> '{0}0201213000003' and A.[EquipmentMaintenanceListID] ='{1}' and A.[Type]=2 ", Framework.SystemID, EquipmentMaintenanceListID);

            if (!string.IsNullOrWhiteSpace(Code))
                sql += string.Format(@" and D.Code like '%{0}%' ", Code);

            if (!string.IsNullOrWhiteSpace(Name))
                sql += string.Format(@" and D.Name like '%{0}%' ", Name);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Status],D.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        ///  保養清單:設備開窗-已選擇資料
        ///  SAM 2017年7月14日15:23:53
        /// </summary>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00008DeviceSettingList(string EquipmentMaintenanceListID, string Code, string Name)
        {
            string select = string.Format(@"select A.EquipmentMaintenanceListDetailID,A.EquipmentMaintenanceListID,A.Sequence,A.Comments,A.DetailID,
            D.Code as EquipmentCode,D.Name as EquipmentName,
            (Select [Name] from [SYS_Parameters] where [ParameterID] = D.[Status]) as Status,
            (Select [Code] from [SYS_Parameters] where [ParameterID] = D.[ResourceCategory]) as ResourceCategory,
            (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [EMS_EquipmentMaintenanceListDetails] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [EMS_Equipment] D on A.DetailID = D.EquipmentID
            where A.[SystemID]='{0}' and  A.[Status] <> '{0}0201213000003' and A.[EquipmentMaintenanceListID] ='{1}' and A.[Type]=2 ", Framework.SystemID, EquipmentMaintenanceListID);

            if (!string.IsNullOrWhiteSpace(Code))
                sql += string.Format(@" and D.Code like '%{0}%' ", Code);

            if (!string.IsNullOrWhiteSpace(Name))
                sql += string.Format(@" and D.Name like '%{0}%' ", Name);

            string orderby = " order by A.[Status],D.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select+sql+orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取清单明细列表
        /// SAM 2017年7月5日15:42:03
        /// </summary>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00008GetDetailList(string EquipmentMaintenanceListID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.EquipmentMaintenanceListDetailID,A.EquipmentMaintenanceListID,A.Sequence,A.DetailID,
            D.Code as ProjectCode,D.Name as ProjectName,D.Comments,
            (Select [Name] from [SYS_Parameters] where [ParameterID] = D.[Description]) as Attribute,
            (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [EMS_EquipmentMaintenanceListDetails] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.DetailID = D.ParameterID
            where A.[SystemID]='{0}' and  A.[Status] <> '{0}0201213000003' and A.[EquipmentMaintenanceListID] ='{1}' and A.[Type]=1 ", Framework.SystemID, EquipmentMaintenanceListID);


            count = UniversalService.getCount(sql, null);

            string orderby = "D.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取清单明细列表（不分页）
        /// SAM 2017年7月14日11:42:23
        /// </summary>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00008DetailList(string EquipmentMaintenanceListID)
        {
            string select = string.Format(@"select A.EquipmentMaintenanceListDetailID,A.EquipmentMaintenanceListID,A.Sequence,A.DetailID,
            D.Code,D.Name,D.Comments,
            (Select [Name] from [SYS_Parameters] where [ParameterID] = D.[Description]) as Attribute,
            (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [EMS_EquipmentMaintenanceListDetails] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.DetailID = D.ParameterID
            where A.[SystemID]='{0}' and  A.[Status] <> '{0}0201213000003' and A.[EquipmentMaintenanceListID] ='{1}' and A.[Type]=1 ", Framework.SystemID, EquipmentMaintenanceListID);

            string orderby = " order by D.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select+sql+orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据保养清单设定获取项目设定
        /// SAM 2017年7月9日11:26:05
        /// </summary>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00009GetDetailList(string EquipmentMaintenanceListID)
        {
            string select = string.Format(@"select A.EquipmentMaintenanceListDetailID,A.EquipmentMaintenanceListID,A.Sequence,A.Comments,A.DetailID,B.Description as Attribute ");

            string sql = string.Format(@" from [EMS_EquipmentMaintenanceListDetails] A 
            left join [SYS_Parameters] B on A.DetailID = B.ParameterID
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' 
            and A.[EquipmentMaintenanceListID] ='{1}' and A.[Type]=1 ", Framework.SystemID, EquipmentMaintenanceListID);

            string orderby = " order by B.[Sequence] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select+ sql+ orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据保养清单设定获取设备设定
        /// SAM 2017年7月9日16:30:57
        /// </summary>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00009GetEquMaiDetailList(string EquipmentMaintenanceListID)
        {
           string select = string.Format(@"select A.EquipmentMaintenanceListDetailID,A.EquipmentMaintenanceListID,A.Sequence,A.Comments,
            B.EquipmentID,B.Code as EquipmentCode,B.Name as EquipmentName ");

            string sql = string.Format(@" from [EMS_EquipmentMaintenanceListDetails] A 
            left join [EMS_Equipment] B on A.DetailID = B.EquipmentID
            where A.[SystemID]='{0}' and  A.[Status] = '{0}0201213000001' 
            and A.[EquipmentMaintenanceListID] ='{1}' and A.[Type]=2 ", Framework.SystemID, EquipmentMaintenanceListID);

            string orderby = " order by A.[Sequence] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 据保养清单获取设备设定列表，同时根据保养工单将保养工单已存在的点亮
        /// SAM 2017年7月31日23:28:24
        /// </summary>
        /// <param name="MaintenanceOrderID"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00009GetEquDetailList(string MaintenanceOrderID, string EquipmentMaintenanceListID)
        {
            string select = string.Format(@"select A.EquipmentMaintenanceListDetailID,
            A.EquipmentMaintenanceListID,A.Sequence,A.Comments,A.DetailID,
            B.EquipmentID,B.Code as EquipmentCode,B.Name as EquipmentName,
            (CASE WHEN (select COUNT(*) from [EMS_MaiOrderEquipment]
              where [Status] <> '{0}0201213000003' 
              and A.[DetailID]=[EquipmentID] and [MaintenanceOrderID]='{1}' and [SystemID]='{0}') > 0 THEN 1 ELSE 0 END) as Selected
            ", Framework.SystemID,MaintenanceOrderID);

            string sql = string.Format(@" from [EMS_EquipmentMaintenanceListDetails] A 
            left join [EMS_Equipment] B on A.[DetailID] = B.[EquipmentID]
            where A.[SystemID]='{0}' and  A.[Status] <> '{0}0201213000003' 
            and A.[EquipmentMaintenanceListID] ='{1}' and A.[Type]=2 ", Framework.SystemID, EquipmentMaintenanceListID);

            string orderby = " order by A.[Sequence] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }





        /// <summary>
        /// 将不在范围内的清单的明细都删除了
        /// SAM 2017年7月14日14:49:39
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="DetailIDs"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <returns></returns>
        public static bool Delete(string userId, string DetailIDs, string EquipmentMaintenanceListID,int Type)
        {
            try
            {
                string sql = String.Format(@"update[EMS_EquipmentMaintenanceListDetails] set {0},
               [Status]='{1}0201213000003' where [EquipmentMaintenanceListID]='{2}' and Type ={3} ", UniversalService.getUpdateUTC(userId), Framework.SystemID, EquipmentMaintenanceListID, Type);

                if (!string.IsNullOrWhiteSpace(DetailIDs))
                    sql += string.Format(@" and [EquipmentMaintenanceListDetailID] not in ('{0}')", DetailIDs);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, null) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 判断保养项目是否在清单中使用
        /// SAM 2017年7月19日11:27:16
        /// </summary>
        /// <param name="DetailID"></param>
        /// <returns></returns>
        public static bool CheckDetail(string DetailID)
        {
            string sql = string.Format(@"select * from [EMS_EquipmentMaintenanceListDetails] where [Type] =1 and [SystemID] = '{1}' and [DetailID]='{0}' and [Status] <> '{1}0201213000003'", DetailID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
    }
}

