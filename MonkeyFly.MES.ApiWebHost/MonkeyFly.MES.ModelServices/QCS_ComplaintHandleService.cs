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
    public class QCS_ComplaintHandleService : SuperModel<QCS_ComplaintHandle>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年6月15日14:16:23
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, QCS_ComplaintHandle Model)
        {
            try
            {
                string sql = string.Format(@"insert[QCS_ComplaintHandle]([ComplaintHandleID],[ComplaintID],[ComplaintDetailID],
                [Sequence],[Method],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@ComplaintHandleID,@ComplaintID,@ComplaintDetailID,@Sequence,@Method,
                @Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ComplaintHandleID",SqlDbType.VarChar),
                    new SqlParameter("@ComplaintID",SqlDbType.VarChar),
                    new SqlParameter("@ComplaintDetailID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Method",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.ComplaintHandleID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.ComplaintID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ComplaintDetailID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Method ?? DBNull.Value;
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
        /// SAM 2017年6月15日14:16:16
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, QCS_ComplaintHandle Model)
        {
            try
            {
                string sql = String.Format(@"update[QCS_ComplaintHandle] set {0},
                [Sequence]=@Sequence,[Method]=@Method,[Status]=@Status,[Comments]=@Comments 
                where [ComplaintHandleID]=@ComplaintHandleID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ComplaintHandleID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Method",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.ComplaintHandleID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Method ?? DBNull.Value;
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
        /// SAM 2017年6月15日14:16:31
        /// </summary>
        /// <param name="ComplaintHandleID"></param>
        /// <returns></returns>
        public static QCS_ComplaintHandle get(string ComplaintHandleID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_ComplaintHandle] where [ComplaintHandleID] = '{0}'  and [SystemID] = '{1}' ", ComplaintHandleID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 客訴單处理对策列表
        /// SAM 2017年6月15日14:17:28
        /// </summary>
        /// <param name="ComplaintDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00010GetHandleList(string ComplaintDetailID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ComplaintHandleID,A.ComplaintDetailID,A.ComplaintID,A.Sequence,
            A.Method,A.Status,A.Comments,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [QCS_ComplaintHandle] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and A.[ComplaintDetailID] ='{1}'", Framework.SystemID, ComplaintDetailID);

            count = UniversalService.getCount(sql, null);

            string orderby = " A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }



    }
}

