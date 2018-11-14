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
    public class EMS_EquipmentInspectionRecordDetailsService : SuperModel<EMS_EquipmentInspectionRecordDetails>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年6月8日15:31:14
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, EMS_EquipmentInspectionRecordDetails Model)
        {
            try
            {
                string sql = string.Format(@"insert[EMS_EquipmentInspectionRecordDetails]([EIRDID],[EquipmentInspectionRecordID],[Sequence],[EIProjectID],[EquipmentProjectID],
                [ProjectID],[Value],[IsHand],[Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@EIRDID,@EquipmentInspectionRecordID,@Sequence,@EIProjectID,@EquipmentProjectID,@ProjectID,@Value,@IsHand,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@EIRDID",SqlDbType.VarChar),
                    new SqlParameter("@EquipmentInspectionRecordID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@EIProjectID",SqlDbType.VarChar),
                    new SqlParameter("@ProjectID",SqlDbType.VarChar),
                    new SqlParameter("@Value",SqlDbType.VarChar),
                    new SqlParameter("@IsHand",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@EquipmentProjectID",SqlDbType.NVarChar)
                    };

                parameters[0].Value = (Object)Model.EIRDID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.EquipmentInspectionRecordID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.EquipmentProjectID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.ProjectID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Value ?? DBNull.Value;
                parameters[6].Value = (Object)Model.IsHand ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[8].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[9].Value = (Object)Model.EquipmentProjectID ?? DBNull.Value;

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
        /// SAM 2017年6月8日15:31:19
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, EMS_EquipmentInspectionRecordDetails Model)
        {
            try
            {
                string sql = String.Format(@"update [EMS_EquipmentInspectionRecordDetails] set {0},
                [Sequence]=@Sequence,[EquipmentProjectID]=@EquipmentProjectID,[EIProjectID]=@EIProjectID,
                [ProjectID]=@ProjectID,[Value]=@Value,[IsHand]=@IsHand,[Status]=@Status,[Comments]=@Comments
                where [EIRDID]=@EIRDID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@EIRDID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@EIProjectID",SqlDbType.VarChar),
                    new SqlParameter("@ProjectID",SqlDbType.VarChar),
                    new SqlParameter("@Value",SqlDbType.VarChar),
                    new SqlParameter("@IsHand",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@EquipmentProjectID",SqlDbType.NVarChar)
                    };

                parameters[0].Value = Model.EIRDID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.EquipmentProjectID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ProjectID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Value ?? DBNull.Value;
                parameters[5].Value = (Object)Model.IsHand ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[8].Value = (Object)Model.EquipmentProjectID ?? DBNull.Value;

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
        /// SAM 2017年6月8日15:31:26
        /// </summary>
        /// <param name="EIRDID"></param>
        /// <returns></returns>
        public static EMS_EquipmentInspectionRecordDetails get(string EIRDID)
        {
            string sql = string.Format(@"select Top 1 * from [EMS_EquipmentInspectionRecordDetails] where [EIRDID] = '{0}'  and [SystemID] = '{1}' ", EIRDID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据表头删除他的所有明细
        /// SAM 2017年6月8日15:29:34
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="EquipmentInspectionRecordID"></param>
        /// <returns></returns>
        public static bool delete(string userId, string EquipmentInspectionRecordID)
        {
            try
            {
                string sql = String.Format(@"update[EMS_EquipmentInspectionRecordDetails] set {0},
               [Status]='{1}0201213000003' where [EquipmentInspectionRecordID]='{2}'", UniversalService.getUpdateUTC(userId),Framework.SystemID, EquipmentInspectionRecordID);
          
                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 根据表头获取他的明细（不分页）
        /// SAM 2017年6月8日15:31:49
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static IList<Hashtable> EMS00003GetDetailList(string EquipmentInspectionRecordID)
        {
            string select = string.Format(@"select A.EIRDID,A.EquipmentInspectionRecordID,A.EIProjectID,A.EquipmentProjectID,A.ProjectID,A.Value,
            F.StandardValue,F.MaxValue,F.MinValue,A.IsHand,A.Status,A.Comments,A.Sequence,          
            E.Code as ProjectCode,E.Description as ProjectName,E.Attribute,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_EquipmentInspectionRecordDetails] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Projects] E on A.[ProjectID] = E.[ProjectID]   
            left join [EMS_EquipmentProject] F on A.[EquipmentProjectID] = F.[EquipmentProjectID]   
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and A.[EquipmentInspectionRecordID] ='{1}' ", Framework.SystemID, EquipmentInspectionRecordID);

            string orderby = "order By A.[Sequence] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }
    }
}

