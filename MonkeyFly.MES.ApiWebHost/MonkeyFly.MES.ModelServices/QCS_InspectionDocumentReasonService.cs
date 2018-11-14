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
    public class QCS_InspectionDocumentReasonService : SuperModel<QCS_InspectionDocumentReason>
    {
        /// <summary>
        /// 新增
        /// Joint
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, QCS_InspectionDocumentReason Model)
        {
            try
            {
                string sql = string.Format(@"insert[QCS_InspectionDocumentReason]([InspectionDocumentReasonID],[InspectionDocumentID],[InspectionDocumentDetailID],[Sequence],[ReasonID],[Status],
                    [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (
                    @InspectionDocumentReasonID, @InspectionDocumentID,@InspectionDocumentDetailID,@Sequence,
                    @ReasonID,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@InspectionDocumentReasonID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionDocumentID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@ReasonID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@InspectionDocumentDetailID",SqlDbType.VarChar),
                    };

                parameters[0].Value = (Object)Model.InspectionDocumentReasonID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.InspectionDocumentID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ReasonID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[6].Value = (Object)Model.InspectionDocumentDetailID ?? DBNull.Value;

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
        /// Joint
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, QCS_InspectionDocumentReason Model)
        {
            try
            {
                string sql = String.Format(@"update[QCS_InspectionDocumentReason] set {0},
                    [Sequence]=@Sequence,[ReasonID]=@ReasonID,[Status]=@Status,
                    [Comments]=@Comments where [InspectionDocumentReasonID]=@InspectionDocumentReasonID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@InspectionDocumentReasonID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@ReasonID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.InspectionDocumentReasonID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ReasonID ?? DBNull.Value;
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
        /// 获取单一实体
        /// Joint
        /// </summary>
        /// <param name="InspectionDocumentReasonID"></param>
        /// <returns></returns>
        public static QCS_InspectionDocumentReason get(string InspectionDocumentReasonID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_InspectionDocumentReason] where [InspectionDocumentReasonID] = '{0}'  and [SystemID] = '{1}' ", InspectionDocumentReasonID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }
        /// <summary>
        /// 删除
        /// Joint
        /// </summary>
        /// <param name="InspectionDocumentReasonID"></param>
        /// <returns></returns>
        public static bool delete(string InspectionDocumentReasonID)
        {
            try
            {
                string sql = string.Format(@"delete from [QCS_InspectionDocumentReason] where [InspectionDocumentReasonID] = '{0}'  and [SystemID] = '{1}' ", InspectionDocumentReasonID, Framework.SystemID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }

            catch (Exception ex)
            {

                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 原因码列表
        /// Joint
        /// 2017年8月14日09:16:02
        /// </summary>
        /// <param name="InspectionDocumentID"></param>
        /// <param name="InspectionDocumentDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00005GetReasonList(string InspectionDocumentID,string InspectionDocumentDetailID,int page, int rows, ref int count)
        {
            string select = string.Format(
              @"select A.InspectionDocumentReasonID,A.Sequence,A.ReasonID,D.Code as ReasonCode,D.Name as ReasonName,A.Comments,A.InspectionDocumentID,A.InspectionDocumentDetailID", Framework.SystemID);

            string sql = string.Format(
                @"from [QCS_InspectionDocumentReason] A
                  left join [QCS_InspectionDocumentDetails] B on B.[InspectionDocumentDetailID]=A.[InspectionDocumentDetailID]
                  left join [QCS_InspectionDocument] C on C.[InspectionDocumentID]=A.[InspectionDocumentID]
                  left join [SYS_Parameters] D on D.[ParameterID]=A.[ReasonID]                            
                  where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and A.[InspectionDocumentID]='{1}' and A.[InspectionDocumentDetailID]='{2}'", Framework.SystemID,  InspectionDocumentID, InspectionDocumentDetailID);

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(InspectionDocumentID))
            {
                sql += " and A.InspectionDocumentID like @InspectionDocumentID";
                parameters.Add(new SqlParameter("@InspectionDocumentID", "%" + InspectionDocumentID + "%"));

            }

            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);

            string orderby = "A.[InspectionDocumentID]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 檢驗明細-不良原因
        /// SAM 2017年7月9日21:42:30
        /// </summary>
        /// <param name="InspectionDocumentDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> QCS00006DetailReason(string InspectionDocumentDetailID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.InspectionDocumentReasonID,
            E.Code as Reason,E.Name as ReasonDesc,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@"  from [QCS_InspectionDocumentReason] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] E on A.[ReasonID] = E.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' and A.[InspectionDocumentDetailID] ='{1}'", Framework.SystemID, InspectionDocumentDetailID);

            count = UniversalService.getCount(sql, null);

            string orderby = " A.[Status],A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }
    }
}

