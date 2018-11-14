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
    public class EMS_CalledRepairOrderService : SuperModel<EMS_CalledRepairOrder>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年5月24日10:53:51
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, EMS_CalledRepairOrder Model)
        {
            try
            {
                string sql = string.Format(@"insert[EMS_CalledRepairOrder]([CalledRepairOrderID],[Code],[Date],[EquipmentID],
                [Status],[CallMESUserID],[CallOrganizationID],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@CalledRepairOrderID,@Code,@Date,@EquipmentID,@Status,
                @CallMESUserID,@CallOrganizationID,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CalledRepairOrderID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.NVarChar),
                    new SqlParameter("@Date",SqlDbType.DateTime),
                    new SqlParameter("@EquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@CallMESUserID",SqlDbType.VarChar),
                    new SqlParameter("@CallOrganizationID",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.CalledRepairOrderID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Date ?? DBNull.Value;
                parameters[3].Value = (Object)Model.EquipmentID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[5].Value = (Object)Model.CallMESUserID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.CallOrganizationID ?? DBNull.Value;
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
        /// SAM 2017年5月24日10:54:03
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, EMS_CalledRepairOrder Model)
        {
            try
            {
                string sql = String.Format(@"update[EMS_CalledRepairOrder] set {0},
                [Date]=@Date,[EquipmentID]=@EquipmentID,
                [Status]=@Status,[CallMESUserID]=@CallMESUserID,[CallOrganizationID]=@CallOrganizationID,
                [Comments]=@Comments where [CalledRepairOrderID]=@CalledRepairOrderID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CalledRepairOrderID",SqlDbType.VarChar),
                    new SqlParameter("@Date",SqlDbType.DateTime),
                    new SqlParameter("@EquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@CallMESUserID",SqlDbType.VarChar),
                    new SqlParameter("@CallOrganizationID",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.CalledRepairOrderID;
                parameters[1].Value = (Object)Model.Date ?? DBNull.Value;
                parameters[2].Value = (Object)Model.EquipmentID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[4].Value = (Object)Model.CallMESUserID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.CallOrganizationID ?? DBNull.Value;
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
        /// 更新负责人，维修厂，內外修
        /// SAM 2017年5月29日23:18:01
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool Ems00005update(string userId, EMS_CalledRepairOrder Model)
        {
            try
            {
                string sql = String.Format(@"update[EMS_CalledRepairOrder] set {0},
                [ManufacturerID]=@ManufacturerID,[InOutRepair]=@InOutRepair,
                [MESUserID]=@MESUserID where [CalledRepairOrderID]=@CalledRepairOrderID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CalledRepairOrderID",SqlDbType.VarChar),
                    new SqlParameter("@ManufacturerID",SqlDbType.VarChar),
                    new SqlParameter("@InOutRepair",SqlDbType.VarChar),
                    new SqlParameter("@MESUserID",SqlDbType.VarChar)
                    };

                parameters[0].Value = Model.CalledRepairOrderID;
                parameters[1].Value = (Object)Model.ManufacturerID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.InOutRepair ?? DBNull.Value;
                parameters[3].Value = (Object)Model.MESUserID ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 更新结案日期，结案人，状态
        /// SAM 2017年6月5日16:35:20
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool Ems00006update(string userId, EMS_CalledRepairOrder Model)
        {
            try
            {
                string sql = String.Format(@"update[EMS_CalledRepairOrder] set {0},
                [CloseMESUserID]=@CloseMESUserID,[CloseDate]=@CloseDate,[Status]=@Status 
                 where [CalledRepairOrderID]=@CalledRepairOrderID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CalledRepairOrderID",SqlDbType.VarChar),
                    new SqlParameter("@CloseMESUserID",SqlDbType.VarChar),
                    new SqlParameter("@CloseDate",SqlDbType.DateTime),
                    new SqlParameter("@Status",SqlDbType.VarChar)
                    };

                parameters[0].Value = Model.CalledRepairOrderID;
                parameters[1].Value = (Object)Model.CloseMESUserID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.CloseDate ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Status ?? DBNull.Value;

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
        /// SAM 2017年5月24日10:54:15
        /// </summary>
        /// <param name="CalledRepairOrderID"></param>
        /// <returns></returns>
        public static EMS_CalledRepairOrder get(string CalledRepairOrderID)
        {
            string sql = string.Format(@"select Top 1 * from [EMS_CalledRepairOrder] where [CalledRepairOrderID] = '{0}'  and [SystemID] = '{1}' ", CalledRepairOrderID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }



        /// <summary>
        /// 获取设备叫修单的列表
        /// SAM 2017年5月24日14:19:09 
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00004GetList(string Code, string Status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.CalledRepairOrderID,A.Code,A.Date,A.EquipmentID,
            A.Status,A.CallMESUserID,A.CallOrganizationID,A.Comments,
            D.Emplno as CallMESUserCode,D.UserName as CallMESUserName,
            E.Code as CallOrganizationCode,E.Name as CallOrganizationName,
            F.Code as EquipmentCode,(select Code from [SYS_Manufacturers] where [ManufacturerID] = F.[ManufacturerID]) as EquipmentManufacturerCode,
            (select Name from SYS_Organization where OrganizationID = F.PlantID) as PlantName,
            (select Name from SYS_PlantArea where PlantAreaID = F.PlantAreaID) as PlantAreaName, 
            I.Emplno as MESUserCode,I.UserName as MESUserName,
            G.Name as InOutRepair,H.Code as ManufacturerCode,H.Name as ManufacturerName,
            A.CloseDate, J.Emplno as CloseMESUserCode,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_CalledRepairOrder] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_MESUsers] D on A.[CallMESUserID] = D.[MESUserID]
            left join [SYS_Organization] E on A.[CallOrganizationID] = E.[OrganizationID]
            left join [EMS_Equipment] F on A.[EquipmentID] = F.[EquipmentID]
            left join [SYS_MESUsers] I on A.[MESUserID] = I.[MESUserID]
            left join [SYS_Parameters] G on A.[InOutRepair] = G.[ParameterID]
            left join [SYS_Manufacturers] H on A.[ManufacturerID] = H.[ManufacturerID]
            left join [SYS_MESUsers] J on A.[CloseMESUserID] = J.[MESUserID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql = sql + string.Format(@" and A.[Status] = @Status ");
                parameters[1].Value = Status;
                Parcount[1].Value = Status;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 设备维修作业的列表
        /// SAM 2017年5月29日23:12:17
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00005GetList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.CalledRepairOrderID,A.Code,A.Date,A.EquipmentID,
            A.Status,A.CallMESUserID,A.CallOrganizationID,A.Comments,F.Condition,
            D.Emplno as CallMESUserCode,D.UserName as CallMESUserName,
            E.Code as CallOrganizationCode,E.Name as CallOrganizationName,
            F.Code as EquipmentCode,(select Code from [SYS_Manufacturers] where [ManufacturerID] = F.[ManufacturerID]) as EquipmentManufacturerCode,
            (select Name from SYS_Organization where OrganizationID = F.PlantID) as PlantName,
            (select Name from SYS_PlantArea where PlantAreaID = F.PlantAreaID) as PlantAreaName, 
            I.UserName as MESUserName,
            (CASE WHEN (select COUNT(*) from [SYS_UserOrganizationMapping] where [UserID] = A.[MESUserID]) = 0 THEN I.Emplno ELSE I.Emplno+'-'+(select [Code] from [SYS_Organization] where [OrganizationID] = (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where [UserID] = A.[MESUserID])) END) as MESUserDept, 
            InOutRepair,
            (CASE WHEN A.InOutRepair='{0}020121300002C' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = A.ManufacturerID) ELSE '' END) as DeptCode,
            (CASE WHEN A.InOutRepair='{0}020121300002C' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = A.ManufacturerID) ELSE '' END) as DeptName,
            H.Code as ManufacturerCode,H.Name as ManufacturerName,
            A.CloseDate, J.Emplno as CloseMESUserCode,
            (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@"  from [EMS_CalledRepairOrder] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_MESUsers] D on A.[CallMESUserID] = D.[MESUserID]
            left join [SYS_Organization] E on A.[CallOrganizationID] = E.[OrganizationID]
            left join [EMS_Equipment] F on A.[EquipmentID] = F.[EquipmentID]
            left join [SYS_MESUsers] I on A.[MESUserID] = I.[MESUserID]
            left join [SYS_Manufacturers] H on A.[ManufacturerID] = H.[ManufacturerID]
            left join [SYS_MESUsers] J on A.[CloseMESUserID] = J.[MESUserID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000029' ", Framework.SystemID);

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

            String orderby = " A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 判断单据号是否已存在
        /// SAM 2017年6月2日17:37:09
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="CalledRepairOrderID"></param>
        /// <returns></returns>
        public static bool CheckCode(string Code)
        {
            string sql = String.Format(@"select Top 1 * from [EMS_CalledRepairOrder] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

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

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断制定设备是否已存在叫修或者维护
        /// SAM 2017年6月5日18:30:23
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        public static bool CheckEquipmentID(string EquipmentID)
        {
            string sql = String.Format(@"select Top 1 * from [EMS_CalledRepairOrder] where [SystemID]='{0}' and [EquipmentID]='{1}' and [Status] in ('{0}0201213000028','{0}0201213000029') ", Framework.SystemID, EquipmentID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 设备叫修结案处理列表
        /// SAM 2017年6月5日16:30:55
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00006GetList(string Code, string status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.CalledRepairOrderID,A.Code,A.Date,A.EquipmentID,
            A.Status,A.CallMESUserID,A.CallOrganizationID,A.Comments,
            D.Emplno as CallMESUserCode,D.UserName as CallMESUserName,
            E.Code as CallOrganizationCode,E.Name as CallOrganizationName,
            F.Code as EquipmentCode,(select Code from [SYS_Manufacturers] where [ManufacturerID] = F.[ManufacturerID]) as EquipmentManufacturerCode,
            (select Name from SYS_Organization where OrganizationID = F.PlantID) as PlantName,
            (select Name from SYS_PlantArea where PlantAreaID = F.PlantAreaID) as PlantAreaName, 
            I.Emplno as MESUserCode,I.UserName as MESUserName,
            (CASE WHEN A.InOutRepair='{0}020121300002C' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = A.ManufacturerID) ELSE '' END) as DeptCode,
            (CASE WHEN A.InOutRepair='{0}020121300002C' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = A.ManufacturerID) ELSE '' END) as DeptName,
            G.Name as InOutRepair,H.Code as ManufacturerCode,H.Name as ManufacturerName,
            A.CloseDate, J.Emplno as CloseMESUserCode,
            (CASE WHEN A.[Status]='{0}0201213000029' THEN 0 ELSE 1 END) as IsClose,
            (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@"  from [EMS_CalledRepairOrder] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_MESUsers] D on A.[CallMESUserID] = D.[MESUserID]
            left join [SYS_Organization] E on A.[CallOrganizationID] = E.[OrganizationID]
            left join [EMS_Equipment] F on A.[EquipmentID] = F.[EquipmentID]
            left join [SYS_MESUsers] I on A.[MESUserID] = I.[MESUserID]
            left join [SYS_Parameters] G on A.[InOutRepair] = G.[ParameterID]
            left join [SYS_Manufacturers] H on A.[ManufacturerID] = H.[ManufacturerID]
            left join [SYS_MESUsers] J on A.[CloseMESUserID] = J.[MESUserID]
            where A.[SystemID]='{0}' and A.[Status] in('{0}0201213000029','{0}020121300002A') ", Framework.SystemID);

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

            if (!string.IsNullOrWhiteSpace(status))
                sql = sql + string.Format(@" and A.[Status] = '{0}' ", status);
            else
                sql = sql + string.Format(@" and A.[Status] = '{0}0201213000029' ", Framework.SystemID);

            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 根据原因码获取查询条件内的叫修单明细
        /// SAM 2017年8月3日15:46:55
        /// </summary>
        /// <param name="ReasonID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartEquipmentCode"></param>
        /// <param name="EndEquipmentCode"></param>
        /// <param name="Type"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00007GetReasonDetailList(string ReasonID, string StartDate, string EndDate, string StartEquipmentCode, string EndEquipmentCode, string Type, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.Date,A.Code,B.Code as EquipmentCode,B.Name as EquipmentName,
                (select Name from  [SYS_Parameters]  where [ParameterID] = A.[InOutRepair]) as InOutRepair,
                (CASE WHEN B.ManufacturerID like '{0}005%' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = B.ManufacturerID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = B.ManufacturerID) END) as DeptCode,
                (CASE WHEN B.ManufacturerID like '{0}005%' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = B.ManufacturerID) ELSE (Select [Name] from [SYS_Manufacturers] where [ManufacturerID] = B.ManufacturerID) END) as DeptName
                ", Framework.SystemID);

            string sql = string.Format(@" from [EMS_CalledRepairOrder]  A
                  left join [EMS_Equipment] B on A.[EquipmentID] = B.[EquipmentID]
                where A.[Status] <> '{0}0201213000003' and A.[SystemID]='{0}' 
            and A.[CalledRepairOrderID] in (select [CalledRepairOrderID] from [EMS_CalledRepairReason] where [ReasonID] ='{1}' and [SystemID] ='{0}') ", Framework.SystemID, ReasonID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartEquipmentCode",SqlDbType.VarChar),
                new SqlParameter("@EndEquipmentCode",SqlDbType.VarChar),
                 new SqlParameter("@Type",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartEquipmentCode",SqlDbType.VarChar),
                new SqlParameter("@EndEquipmentCode",SqlDbType.VarChar),
                  new SqlParameter("@Type",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + string.Format(@" and A.[Date] >= @StartDate ");
                parameters[0].Value = StartDate;
                Parcount[0].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + string.Format(@" and A.[Date] <= @EndDate ");
                parameters[1].Value = EndDate;
                Parcount[1].Value = EndDate;
            }

            if (!string.IsNullOrWhiteSpace(StartEquipmentCode))
            {
                sql = sql + string.Format(@" and B.[Code] >= @StartEquipmentCode ");
                parameters[2].Value = StartEquipmentCode;
                Parcount[2].Value = StartEquipmentCode;
            }


            if (!string.IsNullOrWhiteSpace(EndEquipmentCode))
            {
                sql = sql + string.Format(@" and B.[Code] <= @EndEquipmentCode ");
                parameters[3].Value = EndEquipmentCode;
                Parcount[3].Value = EndEquipmentCode;
            }


            if (!string.IsNullOrWhiteSpace(Type))
            {
                sql = sql + string.Format(@" and B.[ResourceCategory] = @Type ");
                parameters[4].Value = Type;
                Parcount[4].Value = Type;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[Date],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        ///  維修原因統計分析-设备叫修单明细列表
        ///  SAM 2017年8月3日16:05:29
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartEquipmentCode"></param>
        /// <param name="EndEquipmentCode"></param>
        /// <param name="Type"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00007GetEquipmentDetailList(string EquipmentID, string StartDate, string EndDate, string StartEquipmentCode, string EndEquipmentCode, string Type, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.Date,A.Code,B.Code as EquipmentCode,B.Name as EquipmentName,
                (select Name from  [SYS_Parameters]  where [ParameterID] = A.[InOutRepair]) as InOutRepair,
                (CASE WHEN A.ManufacturerID like '{0}005%' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = B.ManufacturerID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = A.ManufacturerID) END) as DeptCode,
                (CASE WHEN A.ManufacturerID like '{0}005%' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = B.ManufacturerID) ELSE (Select [Name] from [SYS_Manufacturers] where [ManufacturerID] = A.ManufacturerID) END) as DeptName
                ", Framework.SystemID);

            string sql = string.Format(@" from [EMS_CalledRepairOrder]  A
                  left join [EMS_Equipment] B on A.[EquipmentID] = B.[EquipmentID]
                where A.[Status] <> '{0}0201213000003' and A.[SystemID]='{0}' and A.[EquipmentID]='{1}' ", Framework.SystemID, EquipmentID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartEquipmentCode",SqlDbType.VarChar),
                new SqlParameter("@EndEquipmentCode",SqlDbType.VarChar),
                 new SqlParameter("@Type",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartEquipmentCode",SqlDbType.VarChar),
                new SqlParameter("@EndEquipmentCode",SqlDbType.VarChar),
                new SqlParameter("@Type",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + string.Format(@" and A.[Date] >= @StartDate ");
                parameters[0].Value = StartDate;
                Parcount[0].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + string.Format(@" and A.[Date] <= @EndDate ");
                parameters[1].Value = EndDate;
                Parcount[1].Value = EndDate;
            }

            if (!string.IsNullOrWhiteSpace(StartEquipmentCode))
            {
                sql = sql + string.Format(@" and B.[Code] >= @StartEquipmentCode ");
                parameters[2].Value = StartEquipmentCode;
                Parcount[2].Value = StartEquipmentCode;
            }


            if (!string.IsNullOrWhiteSpace(EndEquipmentCode))
            {
                sql = sql + string.Format(@" and B.[Code] <= @EndEquipmentCode ");
                parameters[3].Value = EndEquipmentCode;
                Parcount[3].Value = EndEquipmentCode;
            }

            if (!string.IsNullOrWhiteSpace(Type))
            {
                sql = sql + string.Format(@" and B.[ResourceCategory] = @Type ");
                parameters[4].Value = Type;
                Parcount[4].Value = Type;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[Date],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }
    }
}

