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
    public class SFC_FabMoRelationshipService : SuperModel<SFC_FabMoRelationship>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年7月12日10:48:33
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SFC_FabMoRelationship Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_FabMoRelationship]
                    ([FabMoRelationshipID],[FabricatedMotherID],
                    [FabMoProcessID],[PreFabMoProcessID],
                    [IfMain],[IfLastProcess],
                    [Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],
                    [Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                     (@FabMoRelationshipID,@FabricatedMotherID,
                    @FabMoProcessID,@PreFabMoProcessID,@IfMain,
                    @IfLastProcess,@Status,@Comments,
                    '{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@FabMoRelationshipID",SqlDbType.VarChar),
                    new SqlParameter("@FabricatedMotherID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@PreFabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@IfMain",SqlDbType.Bit),
                    new SqlParameter("@IfLastProcess",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.FabMoRelationshipID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.FabricatedMotherID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.FabMoProcessID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.PreFabMoProcessID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.IfMain ?? DBNull.Value;
                parameters[5].Value = (Object)Model.IfLastProcess ?? DBNull.Value;
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

        /// <summary>
        /// 更新
        /// SAM 2017年7月12日10:48:52
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SFC_FabMoRelationship Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_FabMoRelationship] set {0},
                [FabMoProcessID]=@FabMoProcessID,
                [PreFabMoProcessID]=@PreFabMoProcessID,
                [Status]=@Status,
                [IfLastProcess]=@IfLastProcess,
                [Comments]=@Comments 
                where [FabMoRelationshipID]=@FabMoRelationshipID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@FabMoRelationshipID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@PreFabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@IfLastProcess",SqlDbType.Bit),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.FabMoRelationshipID;
                parameters[1].Value = (Object)Model.FabMoProcessID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.PreFabMoProcessID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[4].Value = (Object)Model.IfLastProcess ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017年7月12日10:49:45
        /// </summary>
        /// <param name="FabMoRelationshipID"></param>
        /// <returns></returns>
        public static SFC_FabMoRelationship get(string FabMoRelationshipID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_FabMoRelationship] where [FabMoRelationshipID] = '{0}'  and [SystemID] = '{1}' ", FabMoRelationshipID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }


        /// <summary>
        /// 获取制令的制程关系列表（分页）
        /// SAM 2017年7月13日10:34:403
        /// </summary>
        /// <param name="FabricatedMotherID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00002GetFabMoRelShipList(string FabricatedMotherID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.FabMoRelationshipID,A.FabricatedMotherID,A.FabMoProcessID,A.PreFabMoProcessID,A.IfLastProcess,
            A.IfMain,A.Status,A.Comments,D.Sequence,
            (Select Code From [SYS_Parameters] where ParameterID = D.ProcessID) as ProcessCode,
            (Select Name From [SYS_Parameters] where ParameterID = D.ProcessID) as ProcessName,
            E.Sequence as PreSequence,
            (Select Code From [SYS_Parameters] where ParameterID = E.ProcessID) as PreProcessCode,
            (Select Name From [SYS_Parameters] where ParameterID = E.ProcessID) as PreProcessName,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SFC_FabMoRelationship] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SFC_FabMoProcess] D on A.[FabMoProcessID] = D.[FabMoProcessID]
            left join [SFC_FabMoProcess] E on A.[PreFabMoProcessID] = E.[FabMoProcessID]
            where A.[SystemID]='{0}' and A.Status= '{0}0201213000001' and A.[FabricatedMotherID]='{1}' ", Framework.SystemID, FabricatedMotherID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[ID] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        ///  获取制令的制程关系列表(不分页)
        ///  SAM 2017年7月13日10:50:06
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00002GetFabMoRelShipNoPage(string FabricatedMotherID)
        {
            string select = string.Format(@"select A.FabMoRelationshipID,A.FabricatedMotherID,
            A.FabMoProcessID,A.PreFabMoProcessID,A.IfLastProcess,
            A.IfMain,A.Status,A.Comments,D.Sequence,
            (Select Code From [SYS_Parameters] where ParameterID = D.ProcessID) as ProcessCode,
            (Select Name From [SYS_Parameters] where ParameterID = D.ProcessID) as ProcessName,
            E.Sequence as PreSequence,
            (Select Code From [SYS_Parameters] where ParameterID = E.ProcessID) as PreProcessCode,
            (Select Name From [SYS_Parameters] where ParameterID = E.ProcessID) as PreProcessName ");

            string sql = string.Format(@" from [SFC_FabMoRelationship] A 
           left join [SFC_FabMoProcess] D on A.[FabMoProcessID] = D.[FabMoProcessID]
            left join [SFC_FabMoProcess] E on A.[PreFabMoProcessID] = E.[FabMoProcessID]
            where A.[SystemID]='{0}' and A.Status= '{0}0201213000001' and A.[FabricatedMotherID]='{1}' ", Framework.SystemID, FabricatedMotherID);

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            return ToHashtableList(dt);
        }

        public static bool updateByFabricatedProcessID(string userId, string FabMoProcessID, string PreFabMoProcessID)
        {
            try
            {
                string sql = String.Format(
                    @"update[SFC_FabMoRelationship] 
                      set {0},
                          [PreFabMoProcessID]=@PreFabMoProcessID,
                      where [FabMoProcessID]=@FabMoProcessID",
                    UniversalService.getUpdateUTC(userId));

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@FabMoProcessID", FabMoProcessID),
                    new SqlParameter("@PreFabMoProcessID", PreFabMoProcessID),
                    };

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 根据制令制程获取实体
        /// SAM 2017年7月11日10:10:25
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static SFC_FabMoRelationship getByFMProcess(string FabMoProcessID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_FabMoRelationship] where [FabMoProcessID] = '{0}'  and [SystemID] = '{1}' and [Status]='{1}0201213000001' ", FabMoProcessID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据制令制程获取他的下一站制程
        /// SAM 2017年7月11日10:48:32
        /// </summary>
        /// <param name="PreFabMoProcessID"></param>
        /// <returns></returns>
        public static SFC_FabMoRelationship getByPreFMProcess(string PreFabMoProcessID)
        {
            string sql = string.Format(@"select Top 1 * 
            from [SFC_FabMoRelationship] where [PreFabMoProcessID] = '{0}'  
            and [SystemID] = '{1}' and [Status]='{1}0201213000001' 
            and [PreFabMoProcessID] <> [FabMoProcessID] ", PreFabMoProcessID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 判断指定制令制程是否为最终制程
        /// SAM 2017年8月29日14:23:04
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static bool CheckIfLastProcess(string FabMoProcessID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_FabMoRelationship] where [FabMoProcessID] = '{0}' and [IfLastProcess]=1  and [SystemID] = '{1}' and [Status]='{1}0201213000001' ", FabMoProcessID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据制令制程流水号获取他的所有下站制程
        /// （排除自己）
        /// SAM 2017年8月21日15:04:01
        /// </summary>
        /// <param name="PreFabMoProcessID"></param>
        /// <returns></returns>
        public static IList<Hashtable> getListByPreFMProcess(string FabMoProcessID)
        {
            string sql = string.Format(@"select A.[FabMoProcessID] as value, 
                (select Code From [SYS_Parameters] where [ParameterID] =B.[ProcessID]) as Code,
                (select Name From [SYS_Parameters] where [ParameterID] =B.[ProcessID]) as Name
                from [SFC_FabMoRelationship] A
                left join [SFC_FabMoProcess] B on  A.[FabMoProcessID] = B.[FabMoProcessID]
                where A.[PreFabMoProcessID] = '{0}'  and A.[SystemID] = '{1}' and A.[Status]='{1}0201213000001' 
               and A.[PreFabMoProcessID] <> A.[FabMoProcessID] ", FabMoProcessID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据制令工单获取他的制程关系
        /// SAM 2017年7月13日22:09:05
        /// </summary>
        /// <param name="FabricatedMotherID"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetProcessRelationshipList(string FabricatedMotherID)
        {
            string sql = string.Format(
                @"select * from [SFC_FabMoRelationship]        
                  where [SystemID] = '{0}' and [Status] = '{0}0201213000001' and [FabricatedMotherID] ='{1}'", Framework.SystemID, FabricatedMotherID);

            string orderby = " order by [FabricatedMotherID]";
            
            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据制令工单获取他的制程关系
        /// Joint
        /// </summary>
        /// <param name="FabricatedMotherID"></param>
        /// <returns></returns>
        public static List<SFC_FabMoRelationship> GetFabMoProcessRelationshipList(string FabricatedMotherID)
        {
            string sql = string.Format(
                @"select * from [SFC_FabMoRelationship]        
                  where [SystemID] = '{0}' and [Status] = '{0}0201213000001' and [FabricatedMotherID] ='{1}'", Framework.SystemID, FabricatedMotherID);

            string orderby = " order by [FabricatedMotherID]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }

        /// <summary>
        /// 设置最终制程
        /// SAM 2017年8月24日16:54:01
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="FabricatedMotherID"></param>
        public static void SetFinishProcess(string userid, string FabricatedMotherID)
        {
            try
            {
                string sql = String.Format(@"update [SFC_FabMoRelationship] set {0},
                [IfLastProcess]=0 where [FabricatedMotherID] ='{1}'", UniversalService.getUpdateUTC(userid), FabricatedMotherID, Framework.SystemID);

                SQLHelper.ExecuteNonQuery(sql, CommandType.Text);

                sql = String.Format(@"update [SFC_FabMoRelationship] set {0},
                [IfLastProcess]=1 where [FabricatedMotherID] ='{1}' and 
                [FabMoProcessID] not in (select [PreFabMoProcessID] from [SFC_FabMoRelationship] where [FabricatedMotherID] = '{1}' and [Status] = '{2}0201213000001')
                ", UniversalService.getUpdateUTC(userid), FabricatedMotherID, Framework.SystemID);

                SQLHelper.ExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
            }
        }


        /// <summary>
        /// 判断指定指令单下，是否存在没有关系的制程
        /// SAM 2017年9月10日23:24:13
        /// </summary>
        /// <param name="FabricatedMotherID"></param>
        /// <returns></returns>
        public static bool CheckProRelationShip(string FabricatedMotherID)
        {
            string sql = string.Format(@"select * from [SFC_FabMoProcess] 
            where [FabricatedMotherID] = '{0}'  and [SystemID] = '{1}' 
            and [FabMoProcessID] not in (Select [FabMoProcessID] from [SFC_FabMoRelationship] where [FabricatedMotherID] = '{0}' and [Status]='{1}0201213000001') 
            and [FabMoProcessID] not in (Select [PreFabMoProcessID] from [SFC_FabMoRelationship] where [FabricatedMotherID] = '{0}' and [Status]='{1}0201213000001') 
            ", FabricatedMotherID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断一个制令单下，是否存在多个最终制程
        /// SAM 2017年9月20日15:22:31
        /// </summary>
        /// <param name="FabricatedMotherID"></param>
        /// <returns></returns>
        public static bool CheckFinishProcess(string FabricatedMotherID)
        {
            string sql = string.Format(@"select Count(*) from [SFC_FabMoRelationship] 
            where [FabricatedMotherID] = '{0}'  and [SystemID] = '{1}'  and [IfLastProcess] = 1
            ", FabricatedMotherID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (int.Parse(dt.Rows[0][0].ToString()) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 拼接制程关系的插入语句
        /// SAM 2017年10月6日14:53:39
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static string InsertSQL(string userId, SFC_FabMoRelationship Model)
        {
            try
            {

                string sql = string.Format(
                   @"insert [SFC_FabMoRelationship]([SystemID],[FabMoRelationshipID],[FabricatedMotherID],[FabMoProcessID],[PreFabMoProcessID],
                    [IfMain],[IfLastProcess],[Status],[Comments],
                    [Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime]) values(
                    '{0}','{1}','{2}','{3}','{4}',
                    '{5}','{6}','{7}','{8}',           
                    '{9}','{10}','{11}','{9}','{10}','{11}');",
                    Framework.SystemID, Model.FabMoRelationshipID, Model.FabricatedMotherID, Model.FabMoProcessID, Model.PreFabMoProcessID,
                    Model.IfMain, Model.IfLastProcess, Model.Status, Model.Comments,
                    userId, DateTime.UtcNow, DateTime.Now);


                return sql;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                throw;
            }
        } 
        
        /// <summary>
        /// 判断指定制令制程是否为首制程
        /// SAM 2017年10月18日18:51:13
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static bool CheckFirst(string FabMoProcessID)
        {
            string sql = string.Format(@"select * from [SFC_FabMoRelationship] 
            where [FabMoProcessID] = '{0}'  and [SystemID] = '{1}'  
            and [FabMoProcessID]=[PreFabMoProcessID] and Status='{1}0201213000001'
            ", FabMoProcessID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count>0)
                return true;
            else
                return false;
        }    }
}

