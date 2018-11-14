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
    public class QCS_ComplaintReasonService : SuperModel<QCS_ComplaintReason>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年6月15日11:35:22
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, QCS_ComplaintReason Model)
        {
            try
            {
                string sql = string.Format(@"insert[QCS_ComplaintReason]([ComplaintReasonID],[ComplaintID],[ComplaintDetailID],
            [Sequence],[ReasonGroupID],[ReasonID],[Quantity],[Status],
            [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
             (@ComplaintReasonID,@ComplaintID,@ComplaintDetailID,@Sequence,@ReasonGroupID,
            @ReasonID,@Quantity,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ComplaintReasonID",SqlDbType.VarChar),
                    new SqlParameter("@ComplaintID",SqlDbType.VarChar),
                    new SqlParameter("@ComplaintDetailID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@ReasonGroupID",SqlDbType.VarChar),
                    new SqlParameter("@ReasonID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@Quantity",SqlDbType.Decimal)
                    };

                parameters[0].Value = (Object)Model.ComplaintReasonID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.ComplaintID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ComplaintDetailID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[4].Value = (Object)Model.ReasonGroupID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ReasonID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[8].Value = (Object)Model.Quantity ?? DBNull.Value;

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
        /// SAM 2017年6月15日11:35:07
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, QCS_ComplaintReason Model)
        {
            try
            {
                string sql = String.Format(@"update[QCS_ComplaintReason] set {0},[Quantity]=@Quantity,
                [Sequence]=@Sequence,[ReasonGroupID]=@ReasonGroupID,[ReasonID]=@ReasonID,[Status]=@Status,[Comments]=@Comments 
                where [ComplaintReasonID]=@ComplaintReasonID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ComplaintReasonID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@ReasonGroupID",SqlDbType.VarChar),
                    new SqlParameter("@ReasonID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@Quantity",SqlDbType.NVarChar)
                    };

                parameters[0].Value = Model.ComplaintReasonID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ReasonGroupID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ReasonID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Quantity ?? DBNull.Value;

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
        /// SAM 2017年6月15日11:34:52
        /// </summary>
        /// <param name="ComplaintReasonID"></param>
        /// <returns></returns>
        public static QCS_ComplaintReason get(string ComplaintReasonID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_ComplaintReason] where [ComplaintReasonID] = '{0}'  and [SystemID] = '{1}' ", ComplaintReasonID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 获取客诉原因列表
        /// SAM 2017年6月15日11:43:14
        /// </summary>
        /// <param name="ComplaintDetailID"></param>
        /// <param name="GroupCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00010GetReasonList(string ComplaintDetailID, string GroupCode, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ComplaintReasonID,A.ComplaintDetailID,A.ComplaintID,A.Sequence,
            A.ReasonGroupID,A.ReasonID,A.Status,A.Comments,A.Quantity,
            D.Code as ReasonGroupCode,D.Name as ReasonGroupName,
            E.Code as ReasonCode,E.Name as ReasonName,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [QCS_ComplaintReason] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[ReasonGroupID] = D.[ParameterID]
            left join [SYS_Parameters] E on A.[ReasonID] = E.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' and A.[ComplaintDetailID] ='{1}'", Framework.SystemID, ComplaintDetailID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@GroupCode",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@GroupCode",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(GroupCode))
            {
                GroupCode = "%" + GroupCode + "%";
                sql = sql + string.Format(@" and D.[Code] like @GroupCode ");
                parameters[0].Value = GroupCode;
                Parcount[0].Value = GroupCode;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 客诉单原因的导出
        /// SAM 2017年6月15日14:30:59
        /// </summary>
        /// <param name="ComplaintDetailID"></param>
        /// <param name="GroupCode"></param>
        /// <returns></returns>
        public static DataTable Qcs00010ReasonExport(string ComplaintDetailID, string GroupCode)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Sequence]),
            F.Code, D.Code as ReasonGroupCode,
            E.Code as ReasonCode,E.Name as ReasonName,A.Quantity ");

            string sql = string.Format(@" from [QCS_ComplaintReason] A 
            left join [SYS_Parameters] D on A.[ReasonGroupID] = D.[ParameterID]
            left join [SYS_Parameters] E on A.[ReasonID] = E.[ParameterID]
            left join [QCS_Complaint] F on A.[ComplaintID] =F.[ComplaintID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and A.[ComplaintDetailID] ='{1}' ", Framework.SystemID, ComplaintDetailID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@GroupCode",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@GroupCode",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(GroupCode))
            {
                GroupCode = "%" + GroupCode + "%";
                sql = sql + string.Format(@" and D.[Code] like @GroupCode ");
                parameters[0].Value = GroupCode;
                Parcount[0].Value = GroupCode;
            }

            string orderBy = "order By A.[Sequence] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }


    }
}

