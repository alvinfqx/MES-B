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
    public class EMS_ServiceReasonLogDetailsService : SuperModel<EMS_ServiceReasonLogDetails>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年6月2日11:30:59
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, EMS_ServiceReasonLogDetails Model)
        {
            try
            {
                string sql = string.Format(@"insert[EMS_ServiceReasonLogDetails]([SRLDID],[ServiceReasonLogID],[Sequence],[MESUserID],
                [OrganizationID],[ManufacturerID],[IsFee],[Hour],[Description],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@SRLDID,@ServiceReasonLogID,@Sequence,@MESUserID,@OrganizationID,@ManufacturerID,@IsFee,@Hour,@Description,
                @Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@SRLDID",SqlDbType.VarChar),
                    new SqlParameter("@ServiceReasonLogID",SqlDbType.NVarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@MESUserID",SqlDbType.VarChar),
                    new SqlParameter("@OrganizationID",SqlDbType.VarChar),
                    new SqlParameter("@ManufacturerID",SqlDbType.VarChar),
                    new SqlParameter("@IsFee",SqlDbType.Bit),
                    new SqlParameter("@Hour",SqlDbType.Decimal),
                    new SqlParameter("@Description",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.SRLDID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.ServiceReasonLogID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.MESUserID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.OrganizationID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ManufacturerID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.IsFee ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Hour ?? DBNull.Value;
                parameters[8].Value = (Object)Model.Description ?? DBNull.Value;
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

        /// <summary>
        /// 更新
        /// SAM 2017年6月2日11:30:33
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, EMS_ServiceReasonLogDetails Model)
        {
            try
            {
                string sql = String.Format(@"update[EMS_ServiceReasonLogDetails] set {0},
                [StartTime] = @StartTime,[EndTime] = @EndTime,[Hour]=@Hour,
                [Sequence]=@Sequence,[MESUserID]=@MESUserID,[OrganizationID]=@OrganizationID,
                [ManufacturerID]=@ManufacturerID,[IsFee]=@IsFee,[Description]=@Description,[Status]=@Status,
                [Comments]=@Comments where [SRLDID]=@SRLDID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@SRLDID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@MESUserID",SqlDbType.VarChar),
                    new SqlParameter("@OrganizationID",SqlDbType.VarChar),
                    new SqlParameter("@ManufacturerID",SqlDbType.VarChar),
                    new SqlParameter("@IsFee",SqlDbType.Bit),
                    new SqlParameter("@Description",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@StartTime",SqlDbType.DateTime),
                    new SqlParameter("@EndTime",SqlDbType.DateTime),
                    new SqlParameter("@Hour",SqlDbType.Decimal)
                    };

                parameters[0].Value = Model.SRLDID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.MESUserID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.OrganizationID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.ManufacturerID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.IsFee ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Description ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[8].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[9].Value = (Object)Model.StartTime ?? DBNull.Value;
                parameters[10].Value = (Object)Model.EndTime ?? DBNull.Value;
                parameters[11].Value = (Object)Model.Hour ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 更新状态
        /// SAM 2017年6月16日10:46:06
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool updateStatus(string userId, EMS_ServiceReasonLogDetails Model)
        {
            try
            {
                string sql = String.Format(@"update[EMS_ServiceReasonLogDetails] set {0},
                [Status]=@Status where [SRLDID]=@SRLDID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@SRLDID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar)
                    };

                parameters[0].Value = Model.SRLDID;
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
        /// 更新开始时间
        /// SAM 2017年6月14日17:43:31
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool updateStartTime(string userId, EMS_ServiceReasonLogDetails Model)
        {
            try
            {
                string sql = String.Format(@"update[EMS_ServiceReasonLogDetails] set {0},
                [StartTime]=@StartTime where [SRLDID]=@SRLDID", UniversalService.getUpdateUTC(userId));

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@SRLDID",SqlDbType.VarChar),
                    new SqlParameter("@StartTime",SqlDbType.DateTime)
                    };

                parameters[0].Value = Model.SRLDID;
                parameters[1].Value = (Object)Model.StartTime ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 更新结束时间
        /// SAM 2017年6月14日17:43:18
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool updateEndTime(string userId, EMS_ServiceReasonLogDetails Model)
        {
            try
            {
                string sql = String.Format(@"update[EMS_ServiceReasonLogDetails] set {0},[EndTime]=@EndTime,
                    [Hour]=@Hour where [SRLDID]=@SRLDID", UniversalService.getUpdateUTC(userId));

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@SRLDID",SqlDbType.VarChar),
                    new SqlParameter("@EndTime",SqlDbType.DateTime),
                    new SqlParameter("@Hour",SqlDbType.Decimal)
                    };

                parameters[0].Value = Model.SRLDID;
                parameters[1].Value = (Object)Model.EndTime ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Hour ?? DBNull.Value;


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
        /// SAM 2017年6月2日11:30:20
        /// </summary>
        /// <param name="SRLDID"></param>
        /// <returns></returns>
        public static EMS_ServiceReasonLogDetails get(string SRLDID)
        {
            string sql = string.Format(@"select Top 1 * from [EMS_ServiceReasonLogDetails] where [SRLDID] = '{0}'  and [SystemID] = '{1}' ", SRLDID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 维修记录明细的列表
        /// SAM 2017年6月2日11:30:14
        /// </summary>
        /// <param name="ServiceReasonLogID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00005GetServiceDetailsList(string ServiceReasonLogID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.SRLDID,A.ServiceReasonLogID,A.Sequence,A.IsFee,A.StartTime,A.EndTime,A.Hour,
            A.Status,A.Description,A.MESUserID,A.Comments,A.OrganizationID,A.ManufacturerID,
            D.Emplno as MESUserCode,D.UserName as MESUserName,
            E.Code as OrganizationCode,E.Name as OrganizationName,
            H.Code as ManufacturerCode,H.Name as ManufacturerName,
            (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_ServiceReasonLogDetails] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_MESUsers] D on A.[MESUserID] = D.[MESUserID]
            left join [SYS_Organization] E on A.[OrganizationID] = E.[OrganizationID]
            left join [SYS_Manufacturers] H on A.[ManufacturerID] = H.[ManufacturerID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' and A.[ServiceReasonLogID]='{1}' ", Framework.SystemID, ServiceReasonLogID);       

            count = UniversalService.getCount(sql, null);

            string orderby = " A.[Status] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 判断指定叫修单是否存在没有结束的明细
        /// SAM 2017年11月1日15:21:37
        /// </summary>
        /// <param name="CalledRepairOrderID"></param>
        /// <returns></returns>
        public static bool Check(string CalledRepairOrderID)
        {
            string sql = string.Format(@"select * from [EMS_ServiceReasonLogDetails] 
            where [SystemID] = '{1}' and [Status]<>'{1}0201213000003'
            and [EndTime] is null
            and [ServiceReasonLogID] in (select [ServiceReasonLogID] from [EMS_ServiceReasonLog] where [CalledRepairOrderID]='{0}' and [Status]<>'{1}0201213000003') ", CalledRepairOrderID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
    }
}

