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
    public class SFC_FabMoResourceService : SuperModel<SFC_FabMoResource>
    {
        public static bool insert(string userId, SFC_FabMoResource Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_FabMoResource]([FabMoResourceID],[FabMoProcessID],
                [FabMoOperationID],[Status],[ResourceID],[Quantity],[Type],[IfMain],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@FabMoResourceID,@FabMoProcessID,
                @FabMoOperationID,@Status,@ResourceID,
                @Quantity,@Type,@IfMain,
                @Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@FabMoResourceID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@ResourceID",SqlDbType.VarChar),
                    new SqlParameter("@Quantity",SqlDbType.Int),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@IfMain",SqlDbType.Bit),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.FabMoResourceID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.FabMoProcessID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.FabMoOperationID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[4].Value = (Object)Model.ResourceID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Quantity ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Type ?? DBNull.Value;
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
        /// SAM 2017年7月17日09:31:09
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SFC_FabMoResource Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_FabMoResource] set {0},
                [ResourceID]=@ResourceID,[IfMain]=@IfMain,[Comments]=@Comments 
                where [FabMoResourceID]=@FabMoResourceID", UniversalService.getUpdateUTC(userId));

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@FabMoResourceID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@ResourceID",SqlDbType.VarChar),
                    new SqlParameter("@IfMain",SqlDbType.Bit),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.FabMoResourceID;
                parameters[1].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ResourceID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.IfMain ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }


        public static SFC_FabMoResource get(string FabMoResourceID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_FabMoResource] where [FabMoResourceID] = '{0}'  and [SystemID] = '{1}' ", FabMoResourceID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 删除（实际上是将状态改成删除）
        /// SAM 2017年7月17日09:40:26
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="FabMoResourceID"></param>
        /// <returns></returns>
        public static bool delete(string userId,string FabMoResourceID)
        {
            try
            {
                string sql = string.Format(@"update [SFC_FabMoResource] set  {0},
                [Status]='{2}0201213000003'
                where [FabMoResourceID] = '{1}' ", UniversalService.getUpdateUTC(userId), FabMoResourceID, Framework.SystemID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }

            catch (Exception ex)
            {

                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static IList<Hashtable> GetEquipmentList(string FabMoProcessID, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.FabMoResourceID, A.FabMoProcessID, B.Code as ResourceCode, 
                         B.Description as ResourceName, B.Quantity as ResourceQuantity, A.IfMain, A.ResourceID,
                         A.Comments, F.UserName as Modifier, G.UserName as Creator, A.Status,
                         A.CreateLocalTime as CreateTime, A.ModifiedLocalTime as ModifiedTime", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_FabMoResource] A
                  left join SYS_Resources B on A.ResourceID = B.ResourceID 
                  left join SYS_MESUsers F on A.Modifier = F.MESUserID
                  left join SYS_MESUsers G on A.Creator = G.MESUserID                  
                  where A.[SystemID] = '{0}' and  A.FabMoProcessID = @FabMoProcessID and
                        A.Status <> '{0}0201213000003' and A.Type = '{0}0201213000084'", Framework.SystemID);

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FabMoProcessID", FabMoProcessID));

            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);

            string orderby = "A.[CreateLocalTime]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);

            return ToHashtableList(dt);
        }

        public static IList<Hashtable> GetUserList(string FabMoProcessID, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.FabMoResourceID, A.FabMoProcessID, B.Code as ResourceCode, 
                         B.Description as ResourceName, B.Quantity as ResourceQuantity, A.IfMain, A.Comments,
                         F.UserName as Modifier, G.UserName as Creator, A.Status, A.ResourceID,
                         A.CreateLocalTime as CreateTime, A.ModifiedLocalTime as ModifiedTime", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_FabMoResource] A
                  left join SYS_Resources B on A.ResourceID = B.ResourceID 
                  left join SYS_MESUsers F on A.Modifier = F.MESUserID
                  left join SYS_MESUsers G on A.Creator = G.MESUserID                  
                  where A.[SystemID] = '{0}' and  A.FabMoProcessID = @FabMoProcessID and
                        A.Status <> '{0}0201213000003' and A.Type = '{0}0201213000085'", Framework.SystemID);

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FabMoProcessID", FabMoProcessID));

            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);

            string orderby = "A.[CreateLocalTime]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);

            return ToHashtableList(dt);
        }

        public static IList<Hashtable> GetOtherList(string FabMoProcessID, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.FabMoResourceID, A.FabMoProcessID, B.Code as ResourceCode, 
                         B.Description as ResourceName, B.Quantity as ResourceQuantity, A.IfMain, A.Status,
                         A.Comments, F.UserName as Modifier, G.UserName as Creator, A.ResourceID, C.Name as ClassName,
                         A.CreateLocalTime as CreateTime, A.ModifiedLocalTime as ModifiedTime", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_FabMoResource] A
                  left join SYS_Resources B on A.ResourceID = B.ResourceID 
                  left join [SYS_Parameters] C on B.ClassID = C.ParameterID
                  left join SYS_MESUsers F on A.Modifier = F.MESUserID
                  left join SYS_MESUsers G on A.Creator = G.MESUserID                  
                  where A.[SystemID] = '{0}' and  A.FabMoProcessID = @FabMoProcessID and
                        A.Status <> '{0}0201213000003' and A.Type = '{0}0201213000086'", Framework.SystemID);

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FabMoProcessID", FabMoProcessID));

            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);

            string orderby = "A.[CreateLocalTime]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);

            return ToHashtableList(dt);
        }

        public static bool CheckInsertArgs(SFC_FabMoResource model)
        {
            string sql = string.Format(
                @"from SFC_FabMoResource
                  where ResourceID = '{0}' and Status <> '{1}0201213000003'",
                model.ResourceID, Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(model.FabMoProcessID))
                sql += string.Format(@" and FabMoProcessID = '{0}'", model.FabMoProcessID);

            if (!string.IsNullOrWhiteSpace(model.FabMoOperationID))
                sql += string.Format(@" and FabMoOperationID = '{0}'", model.FabMoOperationID);

            return UniversalService.getCount(sql, null) <= 0;
        }

        public static bool CheckUpdateArgs(SFC_FabMoResource model)
        {
            string sql = string.Format(
                @"from SFC_FabMoResource
                  where  ResourceID = '{1}' and
                        FabMoResourceID <> '{2}' and Status <> '{3}0201213000003'",
                model.FabMoProcessID, model.ResourceID, model.FabMoResourceID, 
                Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(model.FabMoProcessID))
                sql += string.Format(@" and FabMoProcessID = '{0}'", model.FabMoProcessID);

            if (!string.IsNullOrWhiteSpace(model.FabMoOperationID))
                sql += string.Format(@" and FabMoOperationID = '{0}'", model.FabMoOperationID);


            return UniversalService.getCount(sql, null) <= 0;
        }

        /// <summary>
        /// 删除资源
        /// SAM 2017年7月13日15:43:52
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool delete(string uid, SFC_FabMoResource model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_FabMoResource] set {0},
                [Status]=@Status where [FabMoResourceID]=@FabMoResourceID", UniversalService.getUpdateUTC(uid));

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@FabMoResourceID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar)
                    };

                parameters[0].Value = model.FabMoResourceID;
                parameters[1].Value = model.Status;
       
                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }


        /// <summary>
        /// 制令制程工序资源
        /// SAM 2017年7月13日22:00:31
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="ItemOperationID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00002GetFabricatedOperationResource(string Type, string FabMoOperationID,  int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.FabMoResourceID,A.FabMoOperationID,A.ResourceID,A.IfMain,
            A.Status,A.Comments,
            D.Code,D.Description,D.Quantity,
            (Select Name From [SYS_Parameters] where ParameterID = D.GroupID) as GroupCode,
            (Select Name From [SYS_Parameters] where ParameterID = D.ClassID) as ClassName,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@" from [SFC_FabMoResource] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Resources] D on A.ResourceID  =D.ResourceID
            where A.[SystemID]='{0}' and A.Status= '{0}0201213000001' and A.[FabMoOperationID]='{1}' and A.[Type]='{2}'", Framework.SystemID, FabMoOperationID, Type);


            count = UniversalService.getCount(sql, null);

            string orderby = "D.[Code]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据指令制程或者制令制程工序或者类型获取资源列表
        /// SAM 2017年7月13日22:04:06
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00002GetResourceList(string FabMoProcessID, string FabMoOperationID, string Type)
        {
            string sql = string.Format(
                @"select A.FabMoResourceID,A.FabMoProcessID,A.FabMoOperationID,A.Status,A.ResourceID,A.IfMain,A.Comments,
                D.Quantity,D.Code as ResourceCode,D.Description as ResourceName,
                (select [Name] from [SYS_Parameters] where D.ClassID = ParameterID) as ClassName,
                (select [Code] from [SYS_Parameters] where D.ClassID = ParameterID) as ClassCode,
               (CASE WHEN A.[FabMoProcessID] is null THEN (Select Sequence from [SFC_FabMoProcess] where [FabMoProcessID]=C.[FabMoProcessID])
                ELSE B.Sequence END) as ProcessSequence,
                (CASE WHEN A.[FabMoProcessID] is null THEN (select [Code] from [SYS_Parameters] where ParameterID = (Select Top 1 ProcessID from [SFC_FabMoProcess] where [FabMoProcessID]=C.[FabMoProcessID]))
                ELSE(select [Code] from [SYS_Parameters] where ProcessID = ParameterID) END) as ProcessCode,
                (CASE WHEN A.[FabMoProcessID] is null THEN (select [Name] from [SYS_Parameters] where ParameterID = (Select Top 1 ProcessID from [SFC_FabMoProcess] where [FabMoProcessID]=C.[FabMoProcessID]))
                ELSE(select [Code] from [SYS_Parameters] where ProcessID = ParameterID) END) as ProcessName,
                C.Sequence as OperationSequence,
                (select [Code] from [SYS_Parameters] where C.OperationID = ParameterID) as OperationIDCode,
                (select [Name] from [SYS_Parameters] where C.OperationID = ParameterID) as OperationIDName
                from [SFC_FabMoResource] A
                left join [SFC_FabMoProcess] B on B.[FabMoProcessID] = A.[FabMoProcessID]
                left join [SFC_FabMoOperation] C on C.[FabMoOperationID] = A.[FabMoOperationID]
                left join [SYS_Resources] D on D.[ResourceID] = A.[ResourceID]       
                  where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001'", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(FabMoProcessID))
                sql += string.Format(@" and A.[FabMoProcessID] ='{0}'", FabMoProcessID);

            if (!string.IsNullOrWhiteSpace(FabMoOperationID))
                sql += string.Format(@" and A.[FabMoOperationID] ='{0}'", FabMoOperationID);

            if (!string.IsNullOrWhiteSpace(Type))
                sql += string.Format(@" and A.[Type]='{0}' ",Type);

            string orderby = " order by [Type]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据指令制程或者制令制程工序或者类型获取资源列表
        /// Joint
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static List<SFC_FabMoResource> GetFabMoResourceList(string FPID, string FOID)
        {
            string sql = string.Format(
                @"select * from [SFC_FabMoResource]        
                  where [SystemID] = '{0}' and [Status] = '{0}0201213000001'", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(FPID))
                sql += string.Format(@" and [FabMoProcessID] in ('{0}')", FPID);

            if (!string.IsNullOrWhiteSpace(FOID))
                sql += string.Format(@" and [FabMoOperationID] in ('{0}')", FOID);

            string orderby = " order by [CreateLocalTime] desc";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }

        /// <summary>
        /// 判断资源是否存在制程资源表中
        /// MOUSE 2017年7月28日15:57:55
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <returns></returns>
        public static bool CheckResource(string ResourceID)
        {
            string sql = string.Format(@"select * from [SFC_FabMoResource] where [ResourceID] = '{0}' and [SystemID] = '{1}' and [Status] = '{1}0201213000001'", ResourceID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断制令制程资源或者制令制程工序资源是否已存在
        /// SAM 2017年8月31日11:21:58
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <param name="Type"></param>
        /// <param name="FabMoResourceID"></param>
        /// <returns></returns>
        public static bool CheckResource(string ResourceID, string FabMoProcessID, string FabMoOperationID, string Type, string FabMoResourceID)
        {
            string sql = String.Format(@"select Top 1 * from [SFC_FabMoResource] where [SystemID]='{0}' 
            and [Status] <> '{0}0201213000003' and [Type] = '{1}'", Framework.SystemID, Type);

            /*先定义Name和Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ResourceID",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            /*因为ResourceID是通过手动输入的，所以需要用参数的形式去拼SQL*/
            if (!string.IsNullOrWhiteSpace(ResourceID))
            {
                sql = sql + String.Format(@" and [ResourceID] =@ResourceID ");
                parameters[0].Value = ResourceID;
            }

            if (!string.IsNullOrWhiteSpace(FabMoProcessID))
                sql = sql + String.Format(@" and [FabMoProcessID] = '{0}' ", FabMoProcessID);

            if (!string.IsNullOrWhiteSpace(FabMoOperationID))
                sql = sql + String.Format(@" and [FabMoOperationID] = '{0}' ", FabMoOperationID);

            /*FabMoResourceID（流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(FabMoResourceID))
                sql = sql + String.Format(@" and [FabMoResourceID] <> '{0}' ", FabMoResourceID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取设备的资源明细ID后连接制令资源找到制令制程，与制令制程工序
        /// Mouse2017年9月13日15:25:56
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        public static SFC_FabMoResource Iot00003GetFabMoResource(string EquipmentID)
        {
            string sql = string.Format(@"select *
                        from [SFC_FabMoResource] A
                        left join [SYS_ResourceDetails] B on '{0}'=B.[DetailID]
                        where A.[ResourceID]=B.[ResourceID] and A.[SystemID]='{1}'",EquipmentID,Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return ToEntity(dt.Rows[0]);
            }
        }

        /// <summary>
        /// 拼接制令制程/工序资源的SQL语句
        /// SAM 2017年10月6日14:30:07
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static string InsertSQL(string userId, SFC_FabMoResource Model)
        {
            try
            {

                string sql = string.Format(
                   @"insert [SFC_FabMoResource]([SystemID],[FabMoResourceID],[FabMoProcessID],[FabMoOperationID],[ResourceID],
                    [Type],[IfMain],[Status],[Comments],
                    [Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime]) values(
                    '{0}','{1}',{2},{3},'{4}',
                    '{5}','{6}','{7}','{8}',           
                    '{9}','{10}','{11}','{9}','{10}','{11}');",
                    Framework.SystemID, Model.FabMoResourceID, UniversalService.checkNullForSQL(Model.FabMoProcessID),
                    UniversalService.checkNullForSQL(Model.FabMoOperationID), Model.ResourceID,
                    Model.Type, Model.IfMain, Model.Status, Model.Comments,
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

