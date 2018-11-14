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
    public class EMS_MaintenanceOrderService : SuperModel<EMS_MaintenanceOrder>
    {

        public static bool insert(string userId, EMS_MaintenanceOrder Model)
        {
            try
            {
                string sql = string.Format(@"insert[EMS_MaintenanceOrder]([MaintenanceOrderID],[MaintenanceNo],[Date],[MaintenanceDate],
                [Type],[EquipmentMaintenanceListID],[OrganizationID],[ManufacturerID],[MESUserID],
                [Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@MaintenanceOrderID,@MaintenanceNo,
                @Date,@MaintenanceDate,@Type,
                @EquipmentMaintenanceListID,@OrganizationID,@ManufacturerID,
                @MESUserID,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@MaintenanceOrderID",SqlDbType.VarChar),
                    new SqlParameter("@MaintenanceNo",SqlDbType.NVarChar),
                    new SqlParameter("@Date",SqlDbType.DateTime),
                    new SqlParameter("@MaintenanceDate",SqlDbType.DateTime),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@EquipmentMaintenanceListID",SqlDbType.VarChar),
                    new SqlParameter("@OrganizationID",SqlDbType.VarChar),
                    new SqlParameter("@ManufacturerID",SqlDbType.VarChar),
                    new SqlParameter("@MESUserID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.MaintenanceOrderID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.MaintenanceNo ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Date ?? DBNull.Value;
                parameters[3].Value = (Object)Model.MaintenanceDate ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[5].Value = (Object)Model.EquipmentMaintenanceListID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.OrganizationID ?? DBNull.Value;
                parameters[7].Value = (Object)Model.ManufacturerID ?? DBNull.Value;
                parameters[8].Value = (Object)Model.MESUserID ?? DBNull.Value;
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

        public static bool update(string userId, EMS_MaintenanceOrder Model)
        {
            try
            {
                string sql = String.Format(@"update[EMS_MaintenanceOrder] set {0},
[MaintenanceDate]=@MaintenanceDate,
[Type]=@Type,[EquipmentMaintenanceListID]=@EquipmentMaintenanceListID,[OrganizationID]=@OrganizationID,[ManufacturerID]=@ManufacturerID,
[MESUserID]=@MESUserID,[Status]=@Status,[Comments]=@Comments where [MaintenanceOrderID]=@MaintenanceOrderID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@MaintenanceOrderID",SqlDbType.VarChar),
                    new SqlParameter("@MaintenanceDate",SqlDbType.DateTime),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@EquipmentMaintenanceListID",SqlDbType.VarChar),
                    new SqlParameter("@OrganizationID",SqlDbType.VarChar),
                    new SqlParameter("@ManufacturerID",SqlDbType.VarChar),
                    new SqlParameter("@MESUserID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.MaintenanceOrderID;
                parameters[1].Value = (Object)Model.MaintenanceDate ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[3].Value = (Object)Model.EquipmentMaintenanceListID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.OrganizationID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ManufacturerID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.MESUserID ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[8].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static EMS_MaintenanceOrder get(string MaintenanceOrderID)
        {
            string sql = string.Format(@"select Top 1 * from [EMS_MaintenanceOrder] where [MaintenanceOrderID] = '{0}'  and [SystemID] = '{1}' ", MaintenanceOrderID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 判断单号是否已存在
        /// SAM 2017年7月9日10:49:39
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="MaintenanceOrderID"></param>
        /// <returns></returns>
        public static bool CheckCode(string Code, string MaintenanceOrderID)
        {
            string sql = String.Format(@"select Top 1 * from [EMS_MaintenanceOrder] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            /*先定义Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            /*因为Code是通过手动输入的，所以需要用参数的形式去拼SQL*/
            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql = sql + string.Format(@" and [MaintenanceNo] =@Code ");
                parameters[0].Value = Code;
            }

            /*MaintenanceOrderID（流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(MaintenanceOrderID))
                sql = sql + string.Format(@" and [MaintenanceOrderID] <> '{0}' ", MaintenanceOrderID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// 获取保养单主列表
        /// SAM 2017年7月9日10:18:33
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Status"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="UserID"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00009GetList(string Type, string Status, string EquipmentID, string UserID, string StartCode, string EndCode, string StartDate, string EndDate, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.MaintenanceOrderID,A.MaintenanceNo,A.Date,A.MaintenanceDate,
            A.Type,A.EquipmentMaintenanceListID,A.OrganizationID,
            A.ManufacturerID,A.MESUserID,A.Status,A.Comments,I.Code as EquMaiListCode,
            D.Name as TypeName,H.Name as StatusName,
            E.Code as OrganizationCode,E.Name as OrganizationName,
            F.Code as ManufacturerCode,F.Name as ManufacturerName,
            G.Emplno as MESUserCode,G.UserName as MESUserName,
            I.Code as ListCode,
            (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_MaintenanceOrder] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[Type] = D.[ParameterID]
            left join [SYS_Organization] E on A.[OrganizationID] = E.[OrganizationID]
            left join [SYS_Manufacturers] F on A.[ManufacturerID] = F.[ManufacturerID]
            left join [SYS_MESUsers] G on A.[MESUserID] = G.[MESUserID]
            left join [SYS_Parameters] H on A.[Status] = H.[ParameterID]
            left join [EMS_EquipmentMaintenanceList] I on A.[EquipmentMaintenanceListID] = I.[EquipmentMaintenanceListID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Type",SqlDbType.VarChar),
                new SqlParameter("@UserID",SqlDbType.VarChar),
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar),
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
                new SqlParameter("@Type",SqlDbType.VarChar),
                new SqlParameter("@UserID",SqlDbType.VarChar),
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Type))
            {
                sql = sql + string.Format(@" and A.[Type] = @Type ");
                parameters[0].Value = Type;
                Parcount[0].Value = Type;
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                sql = sql + string.Format(@" and A.[MESUserID] = @UserID ");
                parameters[1].Value = UserID;
                Parcount[1].Value = UserID;
            }

            if (!string.IsNullOrWhiteSpace(StartCode))
            {
                sql = sql + string.Format(@" and A.[MaintenanceNo] >= @StartCode ");
                parameters[2].Value = StartCode;
                Parcount[2].Value = StartCode;
            }

            if (!string.IsNullOrWhiteSpace(EndCode))
            {
                sql = sql + string.Format(@" and A.[MaintenanceNo] <= @EndCode ");
                parameters[3].Value = EndCode;
                Parcount[3].Value = EndCode;
            }


            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + string.Format(@" and A.[MaintenanceDate] >= @StartDate ");
                parameters[4].Value = StartDate;
                Parcount[4].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + string.Format(@" and A.[MaintenanceDate] <= @EndDate ");
                parameters[5].Value = EndDate;
                Parcount[5].Value = EndDate;
            }

            if (!string.IsNullOrWhiteSpace(Status))
                sql = sql + string.Format(@" and A.[Status] in ('{0}') ", Status.Replace(",", "','"));



            if (!string.IsNullOrWhiteSpace(EquipmentID))
                sql = sql + string.Format(@" and A.[MaintenanceOrderID] in (select [MaintenanceOrderID] from [EMS_MaiOrderEquipment] where [EquipmentID] ='{0}' and [Status]<>'{1}0201213000003') ", EquipmentID,Framework.SystemID);


            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[Status],A.[MaintenanceNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }



        /// <summary>
        /// 设备保养工单的导出
        /// SAM 2017年8月2日15:34:37
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Status"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="UserID"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static DataTable Ems00009Export(string Type, string Status, string EquipmentID, string UserID, string StartCode, string EndCode, string StartDate, string EndDate)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status],A.[MaintenanceNo]),
            A.MaintenanceNo,CONVERT(varchar(10), A.Date, 20),CONVERT(varchar(10), A.MaintenanceDate, 20),
            D.Name as Type,I.Code as EquipmentMaintenanceList,
            E.Name as Organization,F.Name as Manufacturer,G.Emplno,G.UserName,
            A.Comments,H.Name as Status,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime,M.Sequence,
            N.Code as EquipmentCode,N.Name as EquipmentName,
            (select [Name] from [SYS_Organization] where N.[OrganizationID] = [OrganizationID] ) as EquipmentOrganization,
            (select [Name] from [SYS_Parameters] where N.[Status] = [ParameterID]) as EquipmentStatus,
            N.Model,CONVERT(varchar(10), N.ExpireDate, 20),
            (select [Emplno]+'-'+[UserName] from [SYS_MESUsers] where M.[Creator] = [MESUserID]) as DCreator,M.CreateLocalTime as DTime,
            (CASE WHEN M.CreateLocalTime=M.ModifiedLocalTime THEN NULL else (select [Emplno]+'-'+[UserName] from [SYS_MESUsers] where M.[Modifier] = [MESUserID]) END) as DModifier,
            (CASE WHEN M.CreateLocalTime=M.ModifiedLocalTime THEN NULL else M.ModifiedLocalTime END) as DTime ");

            string sql = string.Format(@"  from [EMS_MaintenanceOrder] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[Type] = D.[ParameterID]
            left join [SYS_Organization] E on A.[OrganizationID] = E.[OrganizationID]
            left join [SYS_Manufacturers] F on A.[ManufacturerID] = F.[ManufacturerID]
            left join [SYS_MESUsers] G on A.[MESUserID] = G.[MESUserID]
            left join [SYS_Parameters] H on A.[Status] = H.[ParameterID]
            left join [EMS_EquipmentMaintenanceList] I on A.[EquipmentMaintenanceListID] = I.[EquipmentMaintenanceListID]
            left Join [EMS_MaiOrderEquipment] M on A.[MaintenanceOrderID] =M.[MaintenanceOrderID]
            left Join [EMS_Equipment] N on M.[EquipmentID] =N.[EquipmentID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Type",SqlDbType.VarChar),
                new SqlParameter("@UserID",SqlDbType.VarChar),
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;
            parameters[5].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Type))
            {
                sql = sql + string.Format(@" and A.[Type] = @Type ");
                parameters[0].Value = Type;
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                sql = sql + string.Format(@" and A.[MESUserID] = @UserID ");
                parameters[1].Value = UserID;
            }

            if (!string.IsNullOrWhiteSpace(StartCode))
            {
                sql = sql + string.Format(@" and A.[MaintenanceNo] >= @StartCode ");
                parameters[2].Value = StartCode;
            }

            if (!string.IsNullOrWhiteSpace(EndCode))
            {
                sql = sql + string.Format(@" and A.[MaintenanceNo] <= @EndCode ");
                parameters[3].Value = EndCode;
            }


            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + string.Format(@" and A.[MaintenanceDate] >= @StartDate ");
                parameters[4].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + string.Format(@" and A.[MaintenanceDate] <= @EndDate ");
                parameters[5].Value = EndDate;
            }

            if (!string.IsNullOrWhiteSpace(Status))
                sql = sql + string.Format(@" and A.[Status] in ('{0}') ", Status.Replace(",", "','"));

   

            string orderby = "order by A.[Status],A.[MaintenanceNo] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select+ sql+ orderby, CommandType.Text, parameters);

            return dt;
        }




        /// <summary>
        /// 設備保養結案與還原-主列表
        /// SAM 2017年7月11日14:07:47
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Status"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="UserID"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00011GetList(string Type, string Status, string EquipmentID, string UserID, string StartCode, string EndCode, string StartDate, string EndDate, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.MaintenanceOrderID,A.MaintenanceNo,A.Date,A.MaintenanceDate,
            A.Type,A.EquipmentMaintenanceListID,A.OrganizationID,
            A.ManufacturerID,A.MESUserID,A.Status,A.Comments,I.Code as EquMaiListCode,
            D.Name as TypeName,H.Name as StatusName,
            E.Code as OrganizationCode,E.Name as OrganizationName,
            F.Code as ManufacturerCode,F.Name as ManufacturerName,
            G.Emplno as MESUserCode,G.UserName as MESUserName,
            (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_MaintenanceOrder] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[Type] = D.[ParameterID]
            left join [SYS_Organization] E on A.[OrganizationID] = E.[OrganizationID]
            left join [SYS_Manufacturers] F on A.[ManufacturerID] = F.[ManufacturerID]
            left join [SYS_MESUsers] G on A.[MESUserID] = G.[MESUserID]
            left join [SYS_Parameters] H on A.[Status] = H.[ParameterID]
            left join [EMS_EquipmentMaintenanceList] I on A.[EquipmentMaintenanceListID] = I.[EquipmentMaintenanceListID]
            where A.[SystemID]='{0}' and A.[Status] in ('{0}0201213000029','{0}020121300002A') ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Type",SqlDbType.VarChar),
                new SqlParameter("@UserID",SqlDbType.VarChar),
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar),
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
                new SqlParameter("@Type",SqlDbType.VarChar),
                new SqlParameter("@UserID",SqlDbType.VarChar),
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Type))
            {
                sql = sql + string.Format(@" and A.[Type] = @Type ");
                parameters[0].Value = Type;
                Parcount[0].Value = Type;
            }

            if (!string.IsNullOrWhiteSpace(UserID))
            {
                sql = sql + string.Format(@" and A.[MESUserID] = @UserID ");
                parameters[1].Value = UserID;
                Parcount[1].Value = UserID;
            }

            if (!string.IsNullOrWhiteSpace(StartCode))
            {
                sql = sql + string.Format(@" and A.[MaintenanceNo] >= @StartCode ");
                parameters[2].Value = StartCode;
                Parcount[2].Value = StartCode;
            }

            if (!string.IsNullOrWhiteSpace(EndCode))
            {
                sql = sql + string.Format(@" and A.[MaintenanceNo] <= @EndCode ");
                parameters[3].Value = EndCode;
                Parcount[3].Value = EndCode;
            }


            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + string.Format(@" and A.[MaintenanceDate] >= @StartDate ");
                parameters[4].Value = StartDate;
                Parcount[4].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + string.Format(@" and A.[MaintenanceDate] <= @EndDate ");
                parameters[5].Value = EndDate;
                Parcount[5].Value = EndDate;
            }

            if (!string.IsNullOrWhiteSpace(Status))
                sql = sql + string.Format(@" and A.[Status] in ('{0}') ", Status.Replace(",", "','"));

            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[MaintenanceDate],D.[Code],A.[MaintenanceNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 設備保養資料主列表（保养单与明细一起显示，以明细为主）
        /// SAM 2017年7月9日11:56:19
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Status"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="UserID"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00010GetList(string TypeCode, string Status, string EquipmentCode, string UserCode, string StartCode, string EndCode, string StartDate, string EndDate, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.MaintenanceOrderID,A.MaintenanceNo,A.Date,
            A.MaintenanceDate,A.Type,A.EquipmentMaintenanceListID,A.OrganizationID,J.Condition,J.Code as EquipmentCode,
            J.Name as EquipmentName,A.ManufacturerID,A.MESUserID,A.Status,A.Comments,
            D.Name as TypeName,H.Name as StatusName,
            E.Code as OrganizationCode,E.Name as OrganizationName,
            F.Code as ManufacturerCode,F.Name as ManufacturerName,
            G.Emplno as MESUserCode,G.UserName as MESUserName,
            (Select Code from [SYS_Organization] where J.OrganizationID=OrganizationID) as EquOrganizationCode,
            (Select Name from [SYS_Organization] where J.OrganizationID=OrganizationID) as EquOrganizationName,
            J.Model,J.MachineNo,J.FixedAssets,J.ExpireDate,I.Sequence,I.MaiOrderEquipmentID,I.EquipmentID,I.StartDate,I.EndDate,
            (CASE WHEN I.[StartDate] is NULL THEN 0 ELSE 1 END) as IsStart,
            (CASE WHEN I.[EndDate] is NULL THEN 0 ELSE 1 END) as IsEnd,
            (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_MaintenanceOrder] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[Type] = D.[ParameterID]
            left join [SYS_Organization] E on A.[OrganizationID] = E.[OrganizationID]
            left join [SYS_Manufacturers] F on A.[ManufacturerID] = F.[ManufacturerID]
            left join [SYS_MESUsers] G on A.[MESUserID] = G.[MESUserID]
            left join [SYS_Parameters] H on A.[Status] = H.[ParameterID]
            left join [EMS_MaiOrderEquipment] I on A.[MaintenanceOrderID] = I.[MaintenanceOrderID]
            left join [EMS_Equipment] J on I.[EquipmentID] = J.[EquipmentID]
            where A.[SystemID]='{0}' and A.[Status] in ('{0}0201213000029','{0}020121300002A') ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@TypeCode",SqlDbType.VarChar),
                new SqlParameter("@UserCode",SqlDbType.VarChar),
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar),
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
                new SqlParameter("@TypeCode",SqlDbType.VarChar),
                new SqlParameter("@UserCode",SqlDbType.VarChar),
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(TypeCode))
            {
                TypeCode = "%" + TypeCode + "%";
                sql = sql + string.Format(@" and D.[Code] like @TypeCode ");
                parameters[0].Value = TypeCode;
                Parcount[0].Value = TypeCode;
            }

            if (!string.IsNullOrWhiteSpace(UserCode))
            {
                UserCode = "%" + UserCode + "%";
                sql = sql + string.Format(@" and G.[Emplno] like @UserCode ");
                parameters[1].Value = UserCode;
                Parcount[1].Value = UserCode;
            }

            if (!string.IsNullOrWhiteSpace(StartCode))
            {
                sql = sql + string.Format(@" and A.[MaintenanceNo] >= @StartCode ");
                parameters[2].Value = StartCode;
                Parcount[2].Value = StartCode;
            }

            if (!string.IsNullOrWhiteSpace(EndCode))
            {
                sql = sql + string.Format(@" and A.[MaintenanceNo] <= @EndCode ");
                parameters[3].Value = EndCode;
                Parcount[3].Value = EndCode;
            }

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + string.Format(@" and A.[MaintenanceDate] >= @StartDate ");
                parameters[4].Value = StartDate;
                Parcount[4].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + string.Format(@" and A.[MaintenanceDate] <= @EndDate ");
                parameters[5].Value = EndDate;
                Parcount[5].Value = EndDate;
            }

            if (!string.IsNullOrWhiteSpace(Status))
                sql = sql + string.Format(@" and A.[Status] in ('{0}') ", Status.Replace(",", "','"));

            if (!string.IsNullOrWhiteSpace(EquipmentCode))
                sql = sql + string.Format(@" and J.[Code] like '%{0}%' ", EquipmentCode);

            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[Status],A.[MaintenanceNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 判断保养清单是否存在与保养工单中
        /// SAM 2017年7月28日13:17:40
        /// </summary>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <returns></returns>
        public static bool CheckList(string EquipmentMaintenanceListID)
        {
            string sql = string.Format(@"select * from [EMS_MaintenanceOrder] where [SystemID] = '{1}' and [EquipmentMaintenanceListID]='{0}' and [Status] <> '{1}0201213000003'", EquipmentMaintenanceListID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 設備保養資料維護导出
        /// Joint 2017年8月1日18:12:25
        /// </summary>
        /// <param name="StartMaintenanceDate"></param>
        /// <param name="EndMaintenanceDate"></param>
        /// <param name="Type"></param>
        /// <param name="StartMaintenanceNo"></param>
        /// <param name="EndMaintenanceNo"></param>
        /// <param name="Status"></param>
        /// <param name="Emplno"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static DataTable Ems00010Export(string StartMaintenanceDate, string EndMaintenanceDate, string Type, string StartMaintenanceNo, string EndMaintenanceNo, string Status, string Emplno, string Code)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.MaintenanceNo),A.MaintenanceDate,A.Type,A.MaintenanceNo,
            (select Name from [SYS_Parameters] where ParameterID=A.Status),
            B.Sequence,C.Code,C.Name,D.Emplno,D.UserName,E.Name,F.Name,
            (select Name from SYS_Organization where C.OrganizationID=OrganizationID),
            C.Model,C.MachineNo,C.FixedAssets,C.ExpireDate,
            (select [Code] from [SYS_Parameters] where B.MaiProjectID=ParameterID) as MaiProjectCode,
            (select [Name] from [SYS_Parameters] where B.MaiProjectID=ParameterID) as MaiProjectName,B.Attribute,B.AttributeValue,B.Comments");

            string sql = string.Format(@"  from [EMS_MaintenanceOrder] A 
            left join EMS_MaiOrderProject B on A.MaintenanceOrderID=B.MaintenanceOrderID
            left join EMS_Equipment C on A.ManufacturerID=C.ManufacturerID
            left join SYS_MESUsers D on A.MESUserID=D.MESUserID
            left join SYS_Organization E on A.OrganizationID=E.OrganizationID
            left join SYS_Manufacturers F on A.ManufacturerID=F.ManufacturerID
            where A.SystemID='{0}' and A.Status<>'{0}0201213000003' and B.[Status]<>'{0}0201213000003'", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartMaintenanceDate",SqlDbType.VarChar),
                new SqlParameter("@EndMaintenanceDate",SqlDbType.VarChar),
                new SqlParameter("@Type",SqlDbType.VarChar),
                new SqlParameter("@StartMaintenanceNo",SqlDbType.VarChar),
                new SqlParameter("@EndMaintenanceNo",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar),
                new SqlParameter("@Emplno",SqlDbType.VarChar),
                new SqlParameter("@CodeName",SqlDbType.VarChar)

            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;
            parameters[5].Value = DBNull.Value;
            parameters[6].Value = DBNull.Value;
            parameters[7].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                 new SqlParameter("@StartMaintenanceDate",SqlDbType.VarChar),
                new SqlParameter("@EndMaintenanceDate",SqlDbType.VarChar),
                new SqlParameter("@Type",SqlDbType.VarChar),
                new SqlParameter("@StartMaintenanceNo",SqlDbType.VarChar),
                new SqlParameter("@EndMaintenanceNo",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar),
                new SqlParameter("@Emplno",SqlDbType.VarChar),
                new SqlParameter("@CodeName",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;
            Parcount[6].Value = DBNull.Value;
            Parcount[7].Value = DBNull.Value;


            if (!string.IsNullOrWhiteSpace(StartMaintenanceDate))
            {
                sql = sql + String.Format(@" and A.[MaintenanceDate]  >=  @StartMaintenanceDate ");
                parameters[0].Value = StartMaintenanceDate;
                Parcount[0].Value = StartMaintenanceDate;
            }
            if (!string.IsNullOrWhiteSpace(EndMaintenanceDate))
            {
                sql = sql + String.Format(@" and A.[MaintenanceDate]  <=  @EndMaintenanceDate ");
                parameters[1].Value = EndMaintenanceDate;
                Parcount[1].Value = EndMaintenanceDate;
            }
            if (!string.IsNullOrWhiteSpace(Type))
            {
                sql = sql + String.Format(@" and A.[Type]  =  @Type ");
                parameters[2].Value = Type;
                Parcount[2].Value = Type;
            }
            if (!string.IsNullOrWhiteSpace(StartMaintenanceNo))
            {
                sql = sql + String.Format(@" and A.[MaintenanceNo]  >=  @StartMaintenanceNo ");
                parameters[3].Value = StartMaintenanceNo;
                Parcount[3].Value = StartMaintenanceNo;
            }
            if (!string.IsNullOrWhiteSpace(EndMaintenanceNo))
            {
                sql = sql + String.Format(@" and A.[MaintenanceNo]  <=  @EndMaintenanceNo ");
                parameters[4].Value = EndMaintenanceNo;
                Parcount[4].Value = EndMaintenanceNo;
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql = sql + String.Format(@" and A.[Status]  =  @Status ");
                parameters[5].Value = Status;
                Parcount[5].Value = Status;
            }

            if (!string.IsNullOrWhiteSpace(Emplno))
            {
                sql = sql + String.Format(@" and D.[Emplno]  =  @Emplno ");
                parameters[6].Value = Emplno;
                Parcount[6].Value = Emplno;
            }
            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + String.Format(@" and C.[Code]  like  @Code ");
                parameters[7].Value = Code;
                Parcount[7].Value = Code;
            }
            string orderBy = "order By A.[MaintenanceDate] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        /// 通过单号获取资料
        /// Joint 2017年8月2日09:43:02
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="MaintenanceOrderID"></param>
        /// <returns></returns>
        public static EMS_MaintenanceOrder GetByCode(string Code)
        {
            string sql = String.Format(@"select * from [EMS_MaintenanceOrder] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            sql = sql + string.Format(@" and [MaintenanceNo] = '{0}'",Code);
  
            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }
    }
}

