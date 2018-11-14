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
    public class SFC_BatchAttributeDetailsService : SuperModel<SFC_BatchAttributeDetails>
    {

        public static bool insert(string userId, SFC_BatchAttributeDetails Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_BatchAttributeDetails]([BatchAttributeDetailID],[CompletionOrderID],
[BatchAttributeID],[Sequence],[AttributeID],
[AttributeValue],[Status],[Comments],
[Modifier],[ModifiedTime],[ModifiedLocalTime],
[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
 (@BatchAttributeDetailID,@CompletionOrderID,
@BatchAttributeID,@Sequence,@AttributeID,
@AttributeValue,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@BatchAttributeDetailID",SqlDbType.VarChar),
                    new SqlParameter("@CompletionOrderID",SqlDbType.VarChar),
                    new SqlParameter("@BatchAttributeID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@AttributeID",SqlDbType.VarChar),
                    new SqlParameter("@AttributeValue",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.BatchAttributeDetailID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.CompletionOrderID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.BatchAttributeID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[4].Value = (Object)Model.AttributeID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.AttributeValue ?? DBNull.Value;
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

        public static bool update(string userId, SFC_BatchAttributeDetails Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_BatchAttributeDetails] set {0},
[BatchAttributeID]=@BatchAttributeID,[Sequence]=@Sequence,
[AttributeID]=@AttributeID,[AttributeValue]=@AttributeValue,[Status]=@Status,[Comments]=@Comments where [BatchAttributeDetailID]=@BatchAttributeDetailID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@BatchAttributeDetailID",SqlDbType.VarChar),
                    new SqlParameter("@BatchAttributeID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@AttributeID",SqlDbType.VarChar),
                    new SqlParameter("@AttributeValue",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.BatchAttributeDetailID;
                parameters[1].Value = (Object)Model.BatchAttributeID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.AttributeID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.AttributeValue ?? DBNull.Value;
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

        public static SFC_BatchAttributeDetails get(string BatchAttributeDetailID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_BatchAttributeDetails] where [BatchAttributeDetailID] = '{0}'  and [SystemID] = '{1}' ", BatchAttributeDetailID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 完工單回報作業-批號-屬性列表
        /// SAM 2017年7月20日15:20:35
        /// </summary>
        /// <param name="BatchAttributeID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00007GetLotAttributeList(string BatchAttributeID, int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select A.BatchAttributeDetailID,A.CompletionOrderID,A.Sequence,A.BatchAttributeID,A.AttributeID,A.AttributeValue,A.Status,A.Comments,
             D.Code as AttributeCode,D.Name as AttributeDesc,E.Name as AttributeValueName,
             B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);
            string sql = string.Format(
                @"from [SFC_BatchAttributeDetails] A               
                left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                left join [SYS_Parameters] D on A.[AttributeID] =D.[ParameterID]
                left join [SYS_Parameters] E on A.[AttributeValue] =E.[ParameterID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' and A.[BatchAttributeID]='{1}'", Framework.SystemID, BatchAttributeID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 判断料品属性是否在完工屬性明細中使用
        /// SAM 2017年7月24日10:17:51
        /// </summary>
        /// <param name="AttributeID"></param>
        /// <returns></returns>
        public static bool CheckAttribute(string AttributeID)
        {
            string sql = string.Format(@"select * from [SFC_BatchAttributeDetails] where  [AttributeID] = '{0}' and [SystemID] = '{1}' and [Status] <> '{1}0201213000003' ", AttributeID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// 根据完工单号判断是否存在属性值为空的批号属性
        /// SAM 2017年7月31日09:54:55
        /// </summary>
        /// <param name="CompletionOrderID"></param>
        /// <returns></returns>
        public static bool CheckAttributeValue(string CompletionOrderID)
        {
            string sql = string.Format(@"select * from [SFC_BatchAttributeDetails] 
                where [SystemID] = '{1}' and [Status] <> '{1}0201213000003' 
                and [CompletionOrderID]='{0}' and AttributeValue is null ", CompletionOrderID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
    }
}

