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
    public class EMS_MaiOrderEquipmentService : SuperModel<EMS_MaiOrderEquipment>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年7月9日15:43:09
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, EMS_MaiOrderEquipment Model)
        {
            try
            {
                string sql = string.Format(@"insert[EMS_MaiOrderEquipment]([MaiOrderEquipmentID],[MaintenanceOrderID],[Sequence],[EquipmentID],
            [StartDate],[EndDate],[Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
             (@MaiOrderEquipmentID,@MaintenanceOrderID,
            @Sequence,@EquipmentID,@StartDate,
            @EndDate,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@MaiOrderEquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@MaintenanceOrderID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@EquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@StartDate",SqlDbType.DateTime),
                    new SqlParameter("@EndDate",SqlDbType.DateTime),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.MaiOrderEquipmentID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.MaintenanceOrderID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.EquipmentID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.StartDate ?? DBNull.Value;
                parameters[5].Value = (Object)Model.EndDate ?? DBNull.Value;
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
        /// SAM 2017年7月9日16:35:49
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, EMS_MaiOrderEquipment Model)
        {
            try
            {
                string sql = String.Format(@"update[EMS_MaiOrderEquipment] set {0},
                [Sequence]=@Sequence,[EquipmentID]=@EquipmentID,
                [StartDate]=@StartDate,[EndDate]=@EndDate,[Status]=@Status,[Comments]=@Comments 
                where [MaiOrderEquipmentID]=@MaiOrderEquipmentID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@MaiOrderEquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@EquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@StartDate",SqlDbType.DateTime),
                    new SqlParameter("@EndDate",SqlDbType.DateTime),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.MaiOrderEquipmentID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.EquipmentID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.StartDate ?? DBNull.Value;
                parameters[4].Value = (Object)Model.EndDate ?? DBNull.Value;
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
        /// SAM 2017年7月9日15:43:31
        /// </summary>
        /// <param name="MaiOrderEquipmentID"></param>
        /// <returns></returns>
        public static EMS_MaiOrderEquipment get(string MaiOrderEquipmentID)
        {
            string sql = string.Format(@"select Top 1 * from [EMS_MaiOrderEquipment] where [MaiOrderEquipmentID] = '{0}'  and [SystemID] = '{1}' ", MaiOrderEquipmentID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据工单批量更新设备明细状态
        /// SAM 2017年7月9日16:37:16
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="MaintenanceOrderID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static bool updateStatus(string userId, string MaintenanceOrderID,string Status)
        {
            try
            {
                string sql = String.Format(@"update[EMS_MaiOrderEquipment] set {0},
                [Status]=@Status
                where [MaintenanceOrderID]=@MaintenanceOrderID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@MaintenanceOrderID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar)
                    };

                parameters[0].Value = MaintenanceOrderID;
                parameters[1].Value = Status;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 根据保养工单获取他的设备保养明细列表
        /// SAM 2017年7月9日15:48:55
        /// </summary>
        /// <param name="MaintenanceOrderID"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00009GetDetailList(string MaintenanceOrderID, string EquipmentID,int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.MaiOrderEquipmentID,A.MaintenanceOrderID,A.Sequence,A.EquipmentID,
            A.StartDate,A.EndDate,A.Status,A.Comments,D.OrganizationID,
            D.Code as EquipmentCode,D.Name as EquipmentName,
            (Select Code from [SYS_Organization] where D.OrganizationID=OrganizationID) as EquOrganizationCode,
            (Select Name from [SYS_Organization] where D.OrganizationID=OrganizationID) as EquOrganizationName,
            E.Name as StatusName,D.Model,D.ExpireDate,F.Status as HeaderStatus,
            (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_MaiOrderEquipment] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [EMS_Equipment] D on A.[EquipmentID] = D.[EquipmentID]
            left join [SYS_Parameters] E on A.[Status] = E.[ParameterID]
            left join [EMS_MaintenanceOrder] F on F.[MaintenanceOrderID] = A.[MaintenanceOrderID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' 
            and A.[MaintenanceOrderID] = '{1}'", Framework.SystemID, MaintenanceOrderID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@EquipmentID",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@EquipmentID",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(EquipmentID))
            {
                sql = sql + string.Format(@" and A.[EquipmentID] = @EquipmentID ");
                parameters[0].Value = EquipmentID;
                Parcount[0].Value = EquipmentID;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[Status],A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }



        /// <summary>
        /// 判断指定工单下的设备明细是否全部都变成了指定状态了
        /// SAM 2017年7月9日17:49:58 
        /// 是返回true  不是返回false
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="MaintenanceOrderID"></param>
        /// <returns></returns>
        public static bool CheckStatus(string Status, string MaintenanceOrderID)
        {
            string sql = String.Format(@"select * from [EMS_MaiOrderEquipment] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            sql = sql + string.Format(@" and [Status] <> '{0}' ", Status);

            sql = sql + string.Format(@" and [MaintenanceOrderID] = '{0}' ", MaintenanceOrderID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 将指定工单设备设定中，不在范围内的设备明细都删除了
        /// SAM 2017年7月31日23:33:37
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="MaintenanceOrderID"></param>
        /// <param name="EquipmentIDs"></param>
        /// <returns></returns>
        public static bool DeleteByOrder(string userid, string MaintenanceOrderID, string EquipmentIDs)
        {
            try
            {
                string sql = string.Format(@"update[EMS_MaiOrderEquipment] set {0},
               [Status]='{1}0201213000003' where [MaintenanceOrderID]='{2}' ", UniversalService.getUpdateUTC(userid), Framework.SystemID, MaintenanceOrderID);

                if (!string.IsNullOrWhiteSpace(EquipmentIDs))//过滤不用删除的
                    sql += string.Format(@" and [EquipmentID] not in ('{0}')", EquipmentIDs);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 判断工单是否已存在指定设备的设定了
        /// SAM 2017年7月31日23:34:48
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <param name="MaintenanceOrderID"></param>
        /// <returns></returns>
        public static bool CheckEquipment(string EquipmentID, string MaintenanceOrderID)
        {
            string sql = String.Format(@"select Top 1 * from [EMS_MaiOrderEquipment] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(EquipmentID))
                sql = sql + String.Format(@" and [EquipmentID] = '{0}' ", EquipmentID);

            if (!string.IsNullOrWhiteSpace(MaintenanceOrderID))
                sql = sql + String.Format(@" and [MaintenanceOrderID] = '{0}' ", MaintenanceOrderID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }



        /// <summary>
        /// 根据保养单流水号和设备流水号找出保養單设备流水号
        /// Joint 2017年8月2日15:12:25
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <param name="MaintenanceOrderID"></param>
        /// <returns></returns>
        public static EMS_MaiOrderEquipment GetEquipment(string EquipmentID, string MaintenanceOrderID)
        {
            string sql = String.Format(@"select Top 1 * from [EMS_MaiOrderEquipment] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(EquipmentID))
                sql = sql + String.Format(@" and [EquipmentID] = '{0}' ", EquipmentID);

            if (!string.IsNullOrWhiteSpace(MaintenanceOrderID))
                sql = sql + String.Format(@" and [MaintenanceOrderID] = '{0}' ", MaintenanceOrderID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }
    }
}

