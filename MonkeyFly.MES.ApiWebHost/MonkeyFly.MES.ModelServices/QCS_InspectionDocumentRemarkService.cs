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
    public class QCS_InspectionDocumentRemarkService : SuperModel<QCS_InspectionDocumentRemark>
    {
        /// <summary>
        /// 新增
        /// Joint
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>

        public static bool insert(string userId, QCS_InspectionDocumentRemark Model)
        {
            try
            {
                string sql = string.Format(@"insert[QCS_InspectionDocumentRemark]([InspectionDocumentRemarkID],[InspectionDocumentID],[InspectionDocumentDetailID],
            [Sequence],[Remark],[Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
             (@InspectionDocumentRemarkID,@InspectionDocumentID,@InspectionDocumentDetailID,
            @Sequence,@Remark,@Status,
            @Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@InspectionDocumentRemarkID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionDocumentID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Remark",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@InspectionDocumentDetailID",SqlDbType.VarChar)
                    };

                parameters[0].Value = (Object)Model.InspectionDocumentRemarkID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.InspectionDocumentID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Remark ?? DBNull.Value;
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
        /// SAM 2017年7月9日23:02:17
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, QCS_InspectionDocumentRemark Model)
        {
            try
            {
                string sql = String.Format(@"update[QCS_InspectionDocumentRemark] set {0},
            [Sequence]=@Sequence,[Remark]=@Remark,[Status]=@Status,[Comments]=@Comments 
            where [InspectionDocumentRemarkID]=@InspectionDocumentRemarkID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@InspectionDocumentRemarkID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Remark",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.InspectionDocumentRemarkID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Remark ?? DBNull.Value;
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
        /// SAM 2017年7月9日23:02:32
        /// </summary>
        /// <param name="InspectionDocumentRemarkID"></param>
        /// <returns></returns>
        public static QCS_InspectionDocumentRemark get(string InspectionDocumentRemarkID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_InspectionDocumentRemark] where [InspectionDocumentRemarkID] = '{0}'  and [SystemID] = '{1}' ", InspectionDocumentRemarkID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 制程检验明细-检验结果说明
        /// </summary>
        /// <param name="InspectionDocumentID"></param>
        /// <param name="InspectionDocumentDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00005GetRemarkList(string InspectionDocumentID, string InspectionDocumentDetailID, int page, int rows, ref int count)
        {
            string select = string.Format(
              @"select A.[InspectionDocumentRemarkID],A.[InspectionDocumentID],A.[InspectionDocumentDetailID],A.[Sequence],A.[Remark],A.[Status]", Framework.SystemID);

            string sql = string.Format(
                @"from [QCS_InspectionDocumentRemark] A 
                  left join [QCS_InspectionDocumentDetails] B on B.[InspectionDocumentDetailID]=A.[InspectionDocumentDetailID]
                  left join [QCS_InspectionDocument] C on C.[InspectionDocumentID]=A.[InspectionDocumentID]
                  where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and A.[InspectionDocumentDetailID]='{1}' 
                  and A.[InspectionDocumentID]='{2}'", Framework.SystemID, InspectionDocumentDetailID, InspectionDocumentID);

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(InspectionDocumentID))
            {
                sql += " and A.[InspectionDocumentID] like @InspectionDocumentID";
                parameters.Add(new SqlParameter("@InspectionDocumentID", "%" + InspectionDocumentID + "%"));

            }

            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);

            string orderby = "A.[InspectionDocumentID]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 檢驗明細-檢驗結果
        /// SAM 2017年7月9日21:44:18
        /// </summary>
        /// <param name="InspectionDocumentDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> QCS00006DetailResult(string InspectionDocumentDetailID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.InspectionDocumentRemarkID,A.Sequence, A.Remark,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@"  from [QCS_InspectionDocumentRemark] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and A.[InspectionDocumentDetailID] ='{1}'", Framework.SystemID, InspectionDocumentDetailID);

            count = UniversalService.getCount(sql, null);

            string orderby = " A.[Status],A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }
    }
}

