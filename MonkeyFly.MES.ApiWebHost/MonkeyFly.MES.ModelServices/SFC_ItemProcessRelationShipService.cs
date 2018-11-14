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
    public class SFC_ItemProcessRelationShipService : SuperModel<SFC_ItemProcessRelationShip>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年6月22日23:52:31
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SFC_ItemProcessRelationShip Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_ItemProcessRelationShip]([IPRSID],[ItemProcessID],[PreItemProcessID],[FinishProcess],[IfMain],
                [ItemID],[Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@IPRSID,@ItemProcessID,@PreItemProcessID,@FinishProcess,@IfMain,
                @ItemID,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@IPRSID",SqlDbType.VarChar),
                    new SqlParameter("@ItemProcessID",SqlDbType.VarChar),
                    new SqlParameter("@PreItemProcessID",SqlDbType.VarChar),
                    new SqlParameter("@FinishProcess",SqlDbType.Bit),
                    new SqlParameter("@IfMain",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@ItemID",SqlDbType.VarChar)
                    };

                parameters[0].Value = (Object)Model.IPRSID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.ItemProcessID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.PreItemProcessID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.FinishProcess ?? DBNull.Value;
                parameters[4].Value = (Object)Model.IfMain ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[7].Value = (Object)Model.ItemID ?? DBNull.Value;

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
        /// SAM 2017年6月22日23:52:37
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SFC_ItemProcessRelationShip Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_ItemProcessRelationShip] set {0},
                [ItemProcessID]=@ItemProcessID,
                [PreItemProcessID]=@PreItemProcessID,[FinishProcess]=@FinishProcess,
                [IfMain]=@IfMain,[Status]=@Status where [IPRSID]=@IPRSID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@IPRSID",SqlDbType.VarChar),
                    new SqlParameter("@PreItemProcessID",SqlDbType.VarChar),
                    new SqlParameter("@FinishProcess",SqlDbType.Bit),
                    new SqlParameter("@IfMain",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@ItemProcessID",SqlDbType.VarChar)
                    };

                parameters[0].Value = Model.IPRSID;
                parameters[1].Value = (Object)Model.PreItemProcessID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.FinishProcess ?? DBNull.Value;
                parameters[3].Value = (Object)Model.IfMain ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ItemProcessID ?? DBNull.Value;

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
        /// SAM 2017年6月22日23:52:47
        /// </summary>
        /// <param name="IPRSID"></param>
        /// <returns></returns>
        public static SFC_ItemProcessRelationShip get(string IPRSID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_ItemProcessRelationShip] where [IPRSID] = '{0}'  and [SystemID] = '{1}' ", IPRSID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }



        /// <summary>
        /// 获取制品的制程关系列表(分页)
        /// SAM 2017年6月23日10:51:26
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetProcessRelationShipList(string ItemID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.IPRSID,A.ItemID,A.ItemProcessID,A.PreItemProcessID,A.FinishProcess,
            A.IfMain,A.Status,A.Comments,D.Sequence,
            (Select Code From [SYS_Parameters] where ParameterID = D.ProcessID) as ProcessCode,
            (Select Name From [SYS_Parameters] where ParameterID = D.ProcessID) as ProcessName,
            E.Sequence as PreSequence,
            (Select Code From [SYS_Parameters] where ParameterID = E.ProcessID) as PreProcessCode,
            (Select Name From [SYS_Parameters] where ParameterID = E.ProcessID) as PreProcessName,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SFC_ItemProcessRelationShip] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SFC_ItemProcess] D on A.ItemProcessID = D.ItemProcessID
            left join [SFC_ItemProcess] E on A.PreItemProcessID = E.ItemProcessID
            where A.[SystemID]='{0}' and A.Status= '{0}0201213000001' and A.[ItemID]='{1}' ", Framework.SystemID, ItemID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[ID] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 取制品的制程关系列表(不分页)
        /// SAM 2017年6月23日11:06:39
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetProcessRelationShipListNoPage(string ItemID)
        {
            string select = string.Format(@"select A.IPRSID,A.ItemID,A.ItemProcessID,A.PreItemProcessID,A.FinishProcess,
            A.IfMain,A.Status,A.Comments,D.Sequence,
            (Select Code From [SYS_Parameters] where ParameterID = D.ProcessID) as ProcessCode,
            (Select Name From [SYS_Parameters] where ParameterID = D.ProcessID) as ProcessName,
          E.Sequence as PreSequence,
            (Select Code From [SYS_Parameters] where ParameterID = E.ProcessID) as PreProcessCode,
            (Select Name From [SYS_Parameters] where ParameterID = E.ProcessID) as PreProcessName ");

            string sql = string.Format(@" from [SFC_ItemProcessRelationShip] A 
            left join [SFC_ItemProcess] D on A.ItemProcessID = D.ItemProcessID
            left join [SFC_ItemProcess] E on A.PreItemProcessID = E.ItemProcessID
            where A.[SystemID]='{0}' and A.Status= '{0}0201213000001' and A.[ItemID]='{1}' ", Framework.SystemID, ItemID);

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 判断一个制程是否存在多个最终制程
        /// SAM 2017年6月29日14:31:29
        /// 存在，返回true,不存在，返回false
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static bool CheckFinishProcess(string ItemID)
        {
            string sql = string.Format(@"select Count(*) from [SFC_ItemProcessRelationShip] 
            where [ItemID] = '{0}'  and [SystemID] = '{1}'  and [FinishProcess] = 1
            ", ItemID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (int.Parse(dt.Rows[0][0].ToString()) > 0)
                return true;
            else
                return false;
        }

       /// <summary>
       /// 根据制品设定最终制程
       /// SAM 2017年6月29日14:45:55
       /// </summary>
       /// <param name="userid"></param>
       /// <param name="itemID"></param>
        public static void SetFinishProcess(string userid, string itemID)
        {
            try
            {
                /*首先取消所有的最终制程关系设定*/
                string sql = String.Format(@"update [SFC_ItemProcessRelationShip] set {0},
                [FinishProcess]=0 where [ItemID] ='{1}'", UniversalService.getUpdateUTC(userid), itemID, Framework.SystemID);

                SQLHelper.ExecuteNonQuery(sql, CommandType.Text);

                /*再重新定义*/
                sql = String.Format(@"update [SFC_ItemProcessRelationShip] set {0},
                [FinishProcess]=1 where [ItemID] ='{1}' and 
                [ItemProcessID] not in (select [PreItemProcessID] from [SFC_ItemProcessRelationShip] where [ItemID] = '{1}' and [Status] = '{2}0201213000001')
                ", UniversalService.getUpdateUTC(userid), itemID, Framework.SystemID);

                SQLHelper.ExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
            }
        }

        /// <summary>
        /// 删除制程的关系
        /// SAM 2017年6月29日14:53:30
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ItemProcessID"></param>
        /// <returns></returns>
        public static bool Delete(string userId, string ItemProcessID)
        {
            try
            {
                string sql = String.Format(@"update [SFC_ItemProcessRelationShip] set {0},[Status]='{1}0201213000003'
                where [ItemProcessID]='{2}' or [PreItemProcessID]='{2}' ", UniversalService.getUpdateUTC(userId), Framework.SystemID, ItemProcessID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, null) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 根据制品获取制程关系
        /// SAM 2017年7月17日23:17:57
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static List<SFC_ItemProcessRelationShip> GetProcessShipList(string ItemID)
        {
            string sql = string.Format(
                @"select * from [SFC_ItemProcessRelationShip]        
                  where [SystemID] = '{0}' and [Status] = '{0}0201213000001' and [ItemID] ='{1}'", Framework.SystemID, ItemID);

            string orderby = " order by [CreateLocalTime] desc";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }

        /// <summary>
        /// 该制品制程是否为最终制程
        /// Mouse 2017年9月7日15:40:36
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static bool CheckLastProcess(string FabMoProcessID)
        {
            string sql = string.Format(@"select top 1 * from [SFC_ItemProcessRelationShip] where [ItemProcessID]='{0}' and [SystemID]='{1}' and [FinishProcess]='1' and [Status]='{1}0201213000001'", FabMoProcessID, Framework.SystemID);
            return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }

        /// <summary>
        /// 获取所有的有效的制程关系设定列表
        /// SAM 2017年10月6日13:56:35
        /// </summary>
        /// <returns></returns>
        public static IList<SFC_ItemProcessRelationShip> GetList()
        {
            string sql = string.Format(
                @"select * from [SFC_ItemProcessRelationShip]       
                  where [SystemID] = '{0}' and [Status] = '{0}0201213000001' ", Framework.SystemID);

            string orderby = " order by [ID]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }
    }
}

