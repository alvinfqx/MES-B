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
    public class QCS_ComplaintDetailsService : SuperModel<QCS_ComplaintDetails>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年6月14日17:49:26
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, QCS_ComplaintDetails Model)
        {
            try
            {
                string sql = string.Format(@"insert[QCS_ComplaintDetails]([ComplaintDetailID],[ComplaintID],[Sequence],
                [ItemID],[BatchNumber],[ShipperNo],[OrderNo],[Quantity],[Description],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@ComplaintDetailID,@ComplaintID,@Sequence,@ItemID,@BatchNumber,@ShipperNo,@OrderNo,@Quantity,@Description,
                @Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ComplaintDetailID",SqlDbType.VarChar),
                    new SqlParameter("@ComplaintID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@BatchNumber",SqlDbType.NVarChar),
                    new SqlParameter("@ShipperNo",SqlDbType.VarChar),
                    new SqlParameter("@Quantity",SqlDbType.Decimal),
                    new SqlParameter("@Description",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@OrderNo",SqlDbType.VarChar)
                    };

                parameters[0].Value = (Object)Model.ComplaintDetailID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.ComplaintID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.BatchNumber ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ShipperNo ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Quantity ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Description ?? DBNull.Value;
                parameters[8].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[9].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[10].Value = (Object)Model.OrderNo ?? DBNull.Value;

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
        /// SAM 2017年6月14日17:49:33
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, QCS_ComplaintDetails Model)
        {
            try
            {
                string sql = String.Format(@"update[QCS_ComplaintDetails] set {0},
                [Sequence]=@Sequence,[ItemID]=@ItemID,[OrderNo]=@OrderNo,
                [BatchNumber]=@BatchNumber,[ShipperNo]=@ShipperNo,[Quantity]=@Quantity,[Description]=@Description,
                [Status]=@Status,[Comments]=@Comments where [ComplaintDetailID]=@ComplaintDetailID", UniversalService.getUpdateUTC(userId));

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ComplaintDetailID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@BatchNumber",SqlDbType.NVarChar),
                    new SqlParameter("@ShipperNo",SqlDbType.VarChar),
                    new SqlParameter("@Quantity",SqlDbType.Decimal),
                    new SqlParameter("@Description",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@OrderNo",SqlDbType.VarChar)
                    };

                parameters[0].Value = Model.ComplaintDetailID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.BatchNumber ?? DBNull.Value;
                parameters[4].Value = (Object)Model.ShipperNo ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Quantity ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Description ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[8].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[9].Value = (Object)Model.OrderNo ?? DBNull.Value;

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
        /// SAM 2017年6月14日17:49:50
        /// </summary>
        /// <param name="ComplaintDetailID"></param>
        /// <returns></returns>
        public static QCS_ComplaintDetails get(string ComplaintDetailID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_ComplaintDetails] where [ComplaintDetailID] = '{0}'  and [SystemID] = '{1}' ", ComplaintDetailID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据客诉表头更新表头明细
        /// SAM 2017年6月15日09:37:12
        /// </summary>
        /// <param name="ComplaintID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static bool updateStatus(string ComplaintID, string Status)
        {
            try
            {
                string sql = String.Format(@"update [QCS_ComplaintDetails] 
                set [Status]=@Status where [ComplaintID]=@ComplaintID");

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ComplaintID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar)
                    };

                parameters[0].Value = ComplaintID;
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
        /// 判断指定的客诉单表头对应的明细中是否存在不处于指定状态的情况
        /// SAM 2017年6月15日16:06:34
        /// 存在则返回true,不存在返回fasle
        /// </summary>
        /// <param name="complaintID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static bool CheckStatus(string complaintID,string Status)
        {
            string sql = string.Format(@"select * from [QCS_ComplaintDetails] where [ComplaintID] = '{0}'  and [SystemID] = '{1}'  and [Status] <> '{2}'", complaintID, Framework.SystemID, Status);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 客诉单的明细列表
        /// SAM 2017年6月14日17:51:34
        /// </summary>
        /// <param name="ComplaintID"></param>
        /// <param name="StartOrderCode"></param>
        /// <param name="EndOrderCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00009GetDetailsList(string ComplaintID, string StartOrderCode, string EndOrderCode, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ComplaintDetailID,A.ComplaintID,A.Sequence,A.ItemID,A.BatchNumber,A.ShipperNo,A.Quantity,
            A.Description,A.Status,A.Comments,A.OrderNo,D.Name as ItemName,D.Code as ItemCode,D.Specification,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [QCS_ComplaintDetails] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Items] D on A.[ItemID] = D.[ItemID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' and A.[ComplaintID] = '{1}' ", Framework.SystemID, ComplaintID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartOrderCode",SqlDbType.VarChar),
                new SqlParameter("@EndOrderCode",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
               new SqlParameter("@StartOrderCode",SqlDbType.VarChar),
                new SqlParameter("@EndOrderCode",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
     
            if (!string.IsNullOrWhiteSpace(StartOrderCode))
            {
                sql = sql + string.Format(@" and A.[OrderNo] >= @StartOrderCode ");
                parameters[0].Value = StartOrderCode;
                Parcount[0].Value = StartOrderCode;
            }

            if (!string.IsNullOrWhiteSpace(EndOrderCode))
            {
                sql = sql + string.Format(@" and A.[OrderNo] <= @EndOrderCode ");
                parameters[1].Value = EndOrderCode;
                Parcount[1].Value = EndOrderCode;
            }

            count = UniversalService.getCount(sql, Parcount);

            String orderby = " A.[Status],A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

    }
}

