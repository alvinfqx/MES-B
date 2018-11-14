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
    public class SFC_FabMoOperationRelationshipService : SuperModel<SFC_FabMoOperationRelationship>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年7月12日10:37:01
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SFC_FabMoOperationRelationship Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_FabMoOperationRelationship](
                [FabMoOperationRelationshipID],
                [FabricatedMotherID],[FabMoProcessID],
                [FabMoOperationID],[PreFabMoOperationID],[IsLastOperation],
                [Status],[IfMain],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],
                [Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@FabMoOperationRelationshipID,@FabricatedMotherID,@FabMoProcessID,@FabMoOperationID,
                @PreFabMoOperationID,@IsLastOperation,@Status,
                @IfMain,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@FabMoOperationRelationshipID",SqlDbType.VarChar),
                    new SqlParameter("@FabricatedMotherID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@PreFabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@IsLastOperation",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@IfMain",SqlDbType.Bit),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.FabMoOperationRelationshipID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.FabricatedMotherID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.FabMoProcessID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.FabMoOperationID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.PreFabMoOperationID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.IsLastOperation ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[7].Value = (Object)Model.IfMain ?? DBNull.Value;
                parameters[8].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017年7月12日10:38:00
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SFC_FabMoOperationRelationship Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_FabMoOperationRelationship] set {0},
                [FabMoOperationID]=@FabMoOperationID,
                [PreFabMoOperationID]=@PreFabMoOperationID,
                [IsLastOperation]=@IsLastOperation,
                [Status]=@Status,[IfMain]=@IfMain,[Comments]=@Comments
                where [FabMoOperationRelationshipID]=@FabMoOperationRelationshipID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@FabMoOperationRelationshipID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@PreFabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@IsLastOperation",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@IfMain",SqlDbType.Bit),
                    new SqlParameter("@Comments",SqlDbType.NVarChar)
                    };

                parameters[0].Value = Model.FabMoOperationRelationshipID;
                parameters[1].Value = (Object)Model.FabMoOperationID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.PreFabMoOperationID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.IsLastOperation ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[5].Value = (Object)Model.IfMain ?? DBNull.Value;
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
        /// 获取单一实体
        /// SAM 2017年7月12日10:38:52
        /// </summary>
        /// <param name="FabMoOperationRelationshipID"></param>
        /// <returns></returns>
        public static SFC_FabMoOperationRelationship get(string FabMoOperationRelationshipID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_FabMoOperationRelationship] where [FabMoOperationRelationshipID] = '{0}'  and [SystemID] = '{1}' ", FabMoOperationRelationshipID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据制令制程工序获取制令关系设定
        /// SAM 2017年7月11日10:05:14
        /// </summary>
        /// <param name="FabMoOperationRelationshipID"></param>
        /// <returns></returns>
        public static SFC_FabMoOperationRelationship getByFMOperation(string FabMoOperationRelationshipID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_FabMoOperationRelationship] 
            where [FabMoOperationID] = '{0}'  and [SystemID] = '{1}' and [Status] ='{1}0201213000001' ", FabMoOperationRelationshipID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据制令制程工序获取下一站的制令关系设定
        /// （第一站时，需要排除自己）
        /// SAM 2017年7月11日10:31:36
        /// </summary>
        /// <param name="FabMoOperationRelationshipID"></param>
        /// <returns></returns>
        public static SFC_FabMoOperationRelationship getByPreFMOperation(string FabMoOperationRelationshipID)
        {
            string sql = string.Format(@"select Top 1 * 
            from [SFC_FabMoOperationRelationship] where [PreFabMoOperationID] = '{0}'  
            and [SystemID] = '{1}' and [Status] ='{1}0201213000001' 
            and [PreFabMoOperationID] <> [FabMoOperationID] ", FabMoOperationRelationshipID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }


        /// <summary>
        /// 根据制令制程工序流水号获取他的所有下站工序
        /// (排除自己)
        /// SAM 2017年8月21日15:04:01
        /// </summary>
        /// <param name="PreFabMoProcessID"></param>
        /// <returns></returns>
        public static IList<Hashtable> getListByPreFMOperation(string PreFMOperationID)
        {
            string sql = string.Format(@"select A.[FabMoOperationID] as value, 
                (select Code From [SYS_Parameters] where [ParameterID] =B.[OperationID]) as Code,
                (select Name From [SYS_Parameters] where [ParameterID] =B.[OperationID]) as Name
                from [SFC_FabMoOperationRelationship] A
                left join [SFC_FabMoOperation] B on  A.[FabMoOperationID] = B.[FabMoOperationID]
                where A.[PreFabMoOperationID] = '{0}'  and A.[SystemID] = '{1}' 
                and A.[Status]='{1}0201213000001' and A.[PreFabMoOperationID] <> A.[FabMoOperationID] ", PreFMOperationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取制程的工序关系列表（分页）
        /// SAM 2017年7月13日17:41:53
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00002GetFabMoOperationRelShipList(string FabMoProcessID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.FabMoOperationRelationshipID,A.FabricatedMotherID,A.FabMoProcessID,
            A.FabMoOperationID,A.PreFabMoOperationID,A.IsLastOperation,A.IfMain,A.Status,A.Comments,D.Sequence,
            (Select Code From [SYS_Parameters] where ParameterID = D.OperationID) as OperationCode,
            (Select Name From [SYS_Parameters] where ParameterID = D.OperationID) as OperationName,
            E.Sequence as PreSequence,
            (Select Code From [SYS_Parameters] where ParameterID = E.OperationID) as PreOperationCode,
            (Select Name From [SYS_Parameters] where ParameterID = E.OperationID) as PreOperationName,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SFC_FabMoOperationRelationship] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SFC_FabMoOperation] D on A.[FabMoOperationID] = D.[FabMoOperationID]
            left join [SFC_FabMoOperation] E on A.[PreFabMoOperationID] = E.[FabMoOperationID]
            where A.[SystemID]='{0}' and A.Status= '{0}0201213000001' and A.[FabMoProcessID]='{1}' ", Framework.SystemID, FabMoProcessID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[ID] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        ///  获取制程的工序关系列表(不分页)
        ///  SAM 2017年7月13日17:41:58
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00002GetFabMoOperationRelShipNoPage(string FabMoProcessID)
        {
            string select = string.Format(@"select A.FabMoOperationRelationshipID,A.FabricatedMotherID,A.FabMoProcessID,
            A.FabMoOperationID,A.PreFabMoOperationID,A.IsLastOperation,A.IfMain,A.Status,A.Comments,D.Sequence,
            (Select Code From [SYS_Parameters] where ParameterID = D.OperationID) as OperationCode,
            (Select Name From [SYS_Parameters] where ParameterID = D.OperationID) as OperationName,
            E.Sequence as PreSequence,
            (Select Code From [SYS_Parameters] where ParameterID = E.OperationID) as PreOperationCode,
            (Select Name From [SYS_Parameters] where ParameterID = E.OperationID) as PreOperationNam ");

            string sql = string.Format(@" from [SFC_FabMoOperationRelationship] A 
           left join [SFC_FabMoOperation] D on A.[FabMoOperationID] = D.[FabMoOperationID]
            left join [SFC_FabMoOperation] E on A.[PreFabMoOperationID] = E.[FabMoOperationID]
            where A.[SystemID]='{0}' and A.Status= '{0}0201213000001' and A.[FabMoProcessID]='{1}' ", Framework.SystemID, FabMoProcessID);

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据制令制程获取制令制程工序关系图
        /// SAM 2017年7月13日22:07:10
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetAltRelShipList(string FabMoProcessID)
        {
            string sql = string.Format(
                @"select * from [SFC_ItemProcessRelationShip]        
                  where [SystemID] = '{0}' and [Status] = '{0}0201213000001' and [FabMoProcessID] ='{1}'", Framework.SystemID, FabMoProcessID);

            string orderby = " order by [CreateLocalTime] desc";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据制令制程获取制令制程工序关系
        /// Joint
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static List<SFC_FabMoOperationRelationship> GetFabMoOperationRelationShipList(string FabMoProcessID)
        {
            string sql = string.Format(
                @"select * from [SFC_FabMoOperationRelationship]        
                  where [SystemID] = '{0}' and [Status] = '{0}0201213000001' and [FabMoProcessID] ='{1}'", Framework.SystemID, FabMoProcessID);

            string orderby = " order by [CreateLocalTime] desc";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }


        /// <summary>
        /// 根据制程设定最终工序
        /// SAM 2017年8月22日17:24:10
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="FabMoProcessID"></param>
        public static void SetFinishOperation(string userid, string FabMoProcessID)
        {
            try
            {
                string sql = String.Format(@"update [SFC_FabMoOperationRelationship] set {0},
                [IsLastOperation]=0 where [FabMoProcessID] ='{1}'", UniversalService.getUpdateUTC(userid), FabMoProcessID, Framework.SystemID);

                SQLHelper.ExecuteNonQuery(sql, CommandType.Text);

                 sql = String.Format(@"update [SFC_FabMoOperationRelationship] set {0},
                [IsLastOperation]=1 where [FabMoProcessID] ='{1}' and 
                [FabMoOperationID] not in (select [PreFabMoOperationID] from [SFC_FabMoOperationRelationship] where [FabMoProcessID] = '{1}' and [Status] = '{2}0201213000001')
                ", UniversalService.getUpdateUTC(userid), FabMoProcessID, Framework.SystemID);

                SQLHelper.ExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
            }
        }

        /// <summary>
        /// 判断指定指令制程下，是否存在没有关系的工序
        /// SAM 2017年9月10日23:24:13
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static bool CheckOpRelationShip(string FabMoProcessID)
        {
            string sql = string.Format(@"select * from [SFC_FabMoOperation] 
            where [FabMoProcessID] = '{0}'  and [SystemID] = '{1}' 
            and [FabMoOperationID] not in (Select [FabMoOperationID] from [SFC_FabMoOperationRelationship] where [FabMoProcessID] = '{0}' and [Status]='{1}0201213000001') 
            and [FabMoOperationID] not in (Select [PreFabMoOperationID] from [SFC_FabMoOperationRelationship] where [FabMoProcessID] = '{0}' and [Status]='{1}0201213000001') 
            ", FabMoProcessID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断一个制令制程下，是否存在多个最终工序
        /// SAM 2017年9月20日15:25:49
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static bool CheckIsLastOperation(string FabMoProcessID)
        {
            string sql = string.Format(@"select Count(*) from [SFC_FabMoOperationRelationship] 
            where [FabMoProcessID] = '{0}'  and [SystemID] = '{1}'  and [IsLastOperation] = 1
            ", FabMoProcessID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (int.Parse(dt.Rows[0][0].ToString()) > 0)
                return true;
            else
                return false;
        }


        /// <summary>
        /// 判断一个制令工单下，是否存在多个最终工序
        /// SAM 2017年9月20日15:27:38
        /// </summary>
        /// <param name="FabricatedMotherID"></param>
        /// <returns></returns>
        public static bool CheckIsLastOperationByFaBMo(string FabricatedMotherID)
        {
            string sql = string.Format(@"select Count(*) from [SFC_FabMoOperationRelationship] 
            where [SystemID] = '{1}'  and [IsLastOperation] = 1  
            and [FabMoProcessID] in (select [FabMoProcessID] from [SFC_FabMoProcess] where [FabricatedMotherID]='{0}' and [Status] <> '{1}0201213000003')  
            ", FabricatedMotherID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (int.Parse(dt.Rows[0][0].ToString()) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 拼接制令制程工序关系的插入语句
        /// SAM 2017年10月6日14:57:46
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static string InsertSQL(string userId, SFC_FabMoOperationRelationship Model)
        {
            try
            {
                string sql = string.Format(
                   @"insert [SFC_FabMoOperationRelationship]([SystemID],[FabMoOperationRelationshipID],[FabricatedMotherID],[FabMoProcessID],[FabMoOperationID],[PreFabMoOperationID],
                    [IfMain],[IsLastOperation],[Status],[Comments],
                    [Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime]) values(
                    '{0}','{1}','{2}','{3}','{4}','{5}',
                    '{6}','{7}','{8}','{9}',           
                    '{10}','{11}','{12}','{10}','{11}','{12}');",
                    Framework.SystemID, Model.FabMoOperationRelationshipID, Model.FabricatedMotherID, Model.FabMoProcessID, Model.FabMoOperationID, Model.PreFabMoOperationID,
                    Model.IfMain, Model.IsLastOperation, Model.Status, Model.Comments,
                    userId, DateTime.UtcNow, DateTime.Now);


                return sql;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                throw;
            }
        }
    }
}

