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
    public class SFC_ProcessOperationRelationShipService : SuperModel<SFC_ProcessOperationRelationShip>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年6月22日23:51:29
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SFC_ProcessOperationRelationShip Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_ProcessOperationRelationShip]([PORSID],[ItemOperationID],[PreItemOperationID],[FinishOperation],
                [IfMain],[ItemProcessID],[Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@PORSID,@ItemOperationID,@PreItemOperationID,@FinishOperation,@IfMain,
                @ItemProcessID,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@PORSID",SqlDbType.VarChar),
                    new SqlParameter("@ItemOperationID",SqlDbType.VarChar),
                    new SqlParameter("@PreItemOperationID",SqlDbType.VarChar),
                    new SqlParameter("@FinishOperation",SqlDbType.Bit),
                    new SqlParameter("@IfMain",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@ItemProcessID",SqlDbType.VarChar)
                    };

                parameters[0].Value = (Object)Model.PORSID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.ItemOperationID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.PreItemOperationID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.FinishOperation ?? DBNull.Value;
                parameters[4].Value = (Object)Model.IfMain ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[7].Value = (Object)Model.ItemProcessID ?? DBNull.Value;

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
        /// SAM 2017年6月22日23:51:35
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SFC_ProcessOperationRelationShip Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_ProcessOperationRelationShip] set {0},
                [ItemOperationID]=@ItemOperationID,
                [PreItemOperationID]=@PreItemOperationID,[FinishOperation]=@FinishOperation,
                [IfMain]=@IfMain,[Status]=@Status where [PORSID]=@PORSID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@PORSID",SqlDbType.VarChar),
                    new SqlParameter("@PreItemOperationID",SqlDbType.VarChar),
                    new SqlParameter("@FinishOperation",SqlDbType.Bit),
                    new SqlParameter("@IfMain",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@ItemOperationID",SqlDbType.VarChar)
                    };

                parameters[0].Value = Model.PORSID;
                parameters[1].Value = (Object)Model.PreItemOperationID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.FinishOperation ?? DBNull.Value;
                parameters[3].Value = (Object)Model.IfMain ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ItemOperationID ?? DBNull.Value;

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
        /// SAM 2017年6月22日23:51:41
        /// </summary>
        /// <param name="PORSID"></param>
        /// <returns></returns>
        public static SFC_ProcessOperationRelationShip get(string PORSID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_ProcessOperationRelationShip] where [PORSID] = '{0}'  and [SystemID] = '{1}' ", PORSID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 获取制程的工序关系列表(分页)
        /// SAM 2017年6月23日10:51:26
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetOperationRelationShipList(string ItemProcessID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.PORSID,A.ItemProcessID,A.ItemOperationID,A.PreItemOperationID,A.FinishOperation,
            A.IfMain,A.Status,A.Comments,D.Sequence,
            (Select Code From [SYS_Parameters] where ParameterID = D.OperationID) as OperationCode,
            (Select Name From [SYS_Parameters] where ParameterID = D.OperationID) as OperationName,
           E.Sequence as PreSequence,
            (Select Code From [SYS_Parameters] where ParameterID = E.OperationID) as PreOperationCode,
            (Select Name From [SYS_Parameters] where ParameterID = E.OperationID) as PreOperationName, 
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SFC_ProcessOperationRelationShip] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SFC_ItemOperation] D on A.ItemOperationID = D.ItemOperationID
            left join [SFC_ItemOperation] E on A.PreItemOperationID = E.ItemOperationID
            where A.[SystemID]='{0}' and A.Status= '{0}0201213000001' and A.[ItemProcessID]='{1}' ", Framework.SystemID, ItemProcessID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[ID] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 取制程的工序关系列表(不分页)
        /// SAM 2017年6月23日11:06:39
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetOperationRelationShipListNoPage(string ItemProcessID)
        {
            string select = string.Format(@"select A.PORSID,A.ItemProcessID,A.ItemOperationID,A.PreItemOperationID,A.FinishOperation,
            A.IfMain,A.Status,A.Comments,D.Sequence,
            (Select Code From [SYS_Parameters] where ParameterID = D.OperationID) as OperationCode,
            (Select Name From [SYS_Parameters] where ParameterID = D.OperationID) as OperationName,
          E.Sequence as PreSequence,
            (Select Code From [SYS_Parameters] where ParameterID = E.OperationID) as PreOperationCode,
            (Select Name From [SYS_Parameters] where ParameterID = E.OperationID) as PreOperationName ");

            string sql = string.Format(@" from [SFC_ProcessOperationRelationShip] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
              left join [SFC_ItemOperation] D on A.ItemOperationID = D.ItemOperationID
            left join [SFC_ItemOperation] E on A.PreItemOperationID = E.ItemOperationID
            where A.[SystemID]='{0}' and A.Status= '{0}0201213000001' and A.[ItemProcessID]='{1}' ", Framework.SystemID, ItemProcessID);

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 判断一个製品製程是否存在多个最终工序
        /// SAM 2017年6月29日14:31:29
        /// 存在，返回true,不存在，返回false
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static bool CheckFinishOperation(string ItemProcessID)
        {
            string sql = string.Format(@"select Count(*) from [SFC_ProcessOperationRelationShip] 
            where [ItemProcessID] = '{0}'  and [SystemID] = '{1}'  and [FinishOperation] = 1
            ", ItemProcessID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (int.Parse(dt.Rows[0][0].ToString()) > 0)
                return true;
            else
                return false;
        }


        /// <summary>
        /// 根据製品製程设定最终工序
        /// SAM 2017年6月29日14:45:55
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="ItemProcessID"></param>
        public static void SetFinishOperation(string userid, string ItemProcessID)
        {
            try
            {
                /*首先取消所有的最终制程工序关系设定*/
                string sql = String.Format(@"update [SFC_ProcessOperationRelationShip] set {0},
                [FinishOperation]=0 where [ItemProcessID] ='{1}'", UniversalService.getUpdateUTC(userid), ItemProcessID, Framework.SystemID);

                SQLHelper.ExecuteNonQuery(sql, CommandType.Text);

                /*再重新设定*/
                sql = String.Format(@"update [SFC_ProcessOperationRelationShip] set {0},
                [FinishOperation]=1 where [ItemProcessID] ='{1}' and 
                [ItemOperationID] not in (select [PreItemOperationID] from [SFC_ProcessOperationRelationShip] where [ItemProcessID] = '{1}' and [Status] = '{2}0201213000001')
                ", UniversalService.getUpdateUTC(userid), ItemProcessID, Framework.SystemID);

                SQLHelper.ExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
            }
        }

       /// <summary>
       /// 取消指定制程的最终工序
       /// SAM 2017年8月14日17:42:09
       /// </summary>
       /// <param name="userid"></param>
       /// <param name="ItemProcessID"></param>
        public static void CancelFinishOperation(string userid, string ItemProcessID)
        {
            try
            {
                string sql = String.Format(@"update [SFC_ProcessOperationRelationShip] set {0},
                [FinishOperation]=0 where [ItemProcessID] ='{1}'
                ", UniversalService.getUpdateUTC(userid), ItemProcessID, Framework.SystemID);

                SQLHelper.ExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
            }
        }

        /// <summary>
        /// 删除指定制程的工序关系
        /// SAM 2017年6月29日14:56:41
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ItemProcessID"></param>
        /// <returns></returns>
        public static bool DeleteByProcess(string userId, string ItemProcessID)
        {
            try
            {
                string sql = String.Format(@"update[SFC_ProcessOperationRelationShip] set {0},[Status]='{1}0201213000003'
                where [ItemProcessID]='{2}' ", UniversalService.getUpdateUTC(userId), Framework.SystemID, ItemProcessID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, null) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 删除指定工序的工序关系
        /// SAM 2017年6月29日15:05:23
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ItemOperationID"></param>
        /// <returns></returns>
        public static bool Delete(string userId, string ItemOperationID)
        {
            try
            {
                string sql = String.Format(@"update[SFC_ProcessOperationRelationShip] set {0},[Status]='{1}0201213000003'
                where [ItemOperationID]='{2}' or [PreItemOperationID] ='{2}' ", UniversalService.getUpdateUTC(userId), Framework.SystemID, ItemOperationID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, null) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        ///根据制品制程获取工序关系
        ///SAM 2017年7月17日23:04:18
        /// </summary>
        /// <param name="ItemProcessID"></param>
        /// <returns></returns>
        public static List<SFC_ProcessOperationRelationShip> GetOperationShipList(string ItemProcessID)
        {
            string sql = string.Format(
                @"select * from [SFC_ProcessOperationRelationShip]        
                  where [SystemID] = '{0}' and [Status] = '{0}0201213000001' and [ItemProcessID] ='{1}'", Framework.SystemID, ItemProcessID);

            string orderby = " order by [CreateLocalTime] desc";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }


        /// <summary>
        /// 获取所有的正常的工序关系列表
        /// SAM 2017年10月6日13:58:26
        /// </summary>
        /// <returns></returns>
        public static IList<SFC_ProcessOperationRelationShip> GetList()
        {
            string sql = string.Format(
                @"select * from [SFC_ProcessOperationRelationShip]       
                  where [SystemID] = '{0}' and [Status] = '{0}0201213000001' ", Framework.SystemID);

            string orderby = " order by [ID]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }
    }
}

