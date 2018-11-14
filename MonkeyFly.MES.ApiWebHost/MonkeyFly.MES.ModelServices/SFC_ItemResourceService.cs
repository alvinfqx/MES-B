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
    public class SFC_ItemResourceService : SuperModel<SFC_ItemResource>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年6月22日14:32:36
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SFC_ItemResource Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_ItemResource]([ItemResourceID],[ItemProcessID],[ItemOperationID],
                        [ResourceID],[IfMain],[Type],[Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                        (@ItemResourceID,@ItemProcessID,@ItemOperationID,@ResourceID,@IfMain,
                        @Type,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ItemResourceID",SqlDbType.VarChar),
                    new SqlParameter("@ItemProcessID",SqlDbType.VarChar),
                    new SqlParameter("@ItemOperationID",SqlDbType.VarChar),
                    new SqlParameter("@ResourceID",SqlDbType.VarChar),
                    new SqlParameter("@IfMain",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@Type",SqlDbType.VarChar)
                    };

                parameters[0].Value = (Object)Model.ItemResourceID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.ItemProcessID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ItemOperationID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ResourceID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.IfMain ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Type ?? DBNull.Value;

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
        /// SAM 2017年6月22日14:32:47
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SFC_ItemResource Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_ItemResource] set {0},
                [ItemOperationID]=@ItemOperationID,[ResourceID]=@ResourceID,
                [IfMain]=@IfMain,[Status]=@Status,[Comments]=@Comments where [ItemResourceID]=@ItemResourceID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ItemResourceID",SqlDbType.VarChar),
                    new SqlParameter("@ItemOperationID",SqlDbType.VarChar),
                    new SqlParameter("@ResourceID",SqlDbType.VarChar),
                    new SqlParameter("@IfMain",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.ItemResourceID;
                parameters[1].Value = (Object)Model.ItemOperationID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ResourceID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.IfMain ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
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
        /// SAM 2017年6月26日17:49:45
        /// </summary>
        /// <param name="ItemResourceID"></param>
        /// <returns></returns>
        public static SFC_ItemResource get(string ItemResourceID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_ItemResource] where [ItemResourceID] = '{0}'  and [SystemID] = '{1}' ", ItemResourceID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 制程判断是否重复
        /// SAM 2017年6月22日15:18:36
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="Type"></param>
        /// <param name="ItemResourceID"></param>
        /// <returns></returns>
        public static bool Check(string ResourceID, string ItemProcessID, string ItemOperationID, string Type, string ItemResourceID)
        {
            string sql = String.Format(@"select Top 1 * from [SFC_ItemResource] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(ResourceID))
                sql = sql + string.Format(@" and [ResourceID]= '{0}' ", ResourceID);


            if (!string.IsNullOrWhiteSpace(ItemOperationID))
                sql = sql + string.Format(@" and [ItemOperationID]= '{0}' ", ItemOperationID);


            if (!string.IsNullOrWhiteSpace(Type))
                sql = sql + string.Format(@" and [Type]= '{0}' ", Type);


            if (!string.IsNullOrWhiteSpace(ItemProcessID))
                sql = sql + string.Format(@" and [ItemProcessID]= '{0}' ", ItemProcessID);

            /*ItemResourceID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(ItemResourceID))
                sql = sql + string.Format(@" and [ItemResourceID] <> '{0}' ", ItemResourceID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 工序判断是否重复
        /// SAM 2017年6月27日18:12:39
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="ItemResourceID"></param>
        /// <returns></returns>
        public static bool CheckOperation(string ResourceID, string ItemOperationID, string ItemResourceID)
        {
            string sql = String.Format(@"select Top 1 * from [SFC_ItemResource] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(ResourceID))
                sql = sql + string.Format(@" and [ResourceID]= '{0}' ", ResourceID);

            if (!string.IsNullOrWhiteSpace(ItemOperationID))
                sql = sql + string.Format(@" and [ItemOperationID]= '{0}' ", ItemOperationID);

            /*ItemResourceID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(ItemResourceID))
                sql = sql + string.Format(@" and [ItemResourceID] <> '{0}' ", ItemResourceID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 製程資源列表
        /// SAM 2017年6月22日14:33:51
        /// </summary>
        /// <param name="ItemProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetResourceList(string ItemProcessID, string Type)
        {
            string select = string.Format(@"select A.ItemResourceID,A.ItemProcessID,A.ItemOperationID,A.ResourceID,A.IfMain,
            A.Status,A.Comments,
            D.Code,D.Description,D.Quantity,
            (Select Name From [SYS_Parameters] where ParameterID = D.GroupID) as GroupCode,
            (Select Name From [SYS_Parameters] where ParameterID = D.ClassID) as ClassName,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@" from [SFC_ItemResource] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Resources] D on A.ResourceID  =D.ResourceID
            where A.[SystemID]='{0}' and A.Status= '{0}0201213000001' and A.[ItemOperationID] is null and A.[ItemProcessID]='{1}' and A.[Type]='{0}{2}'", Framework.SystemID, ItemProcessID, Type);

            string orderby = " order by A.[ID] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 制品工序资源列表
        /// SAM 2017年6月22日18:29:11
        /// </summary>
        /// <param name="ItemOperationID"></param>
        /// <param name="Type"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetOperationResourceList(string ItemOperationID, string Type)
        {
            string select = string.Format(@"select A.ItemResourceID,A.ItemProcessID,A.ItemOperationID,A.ResourceID,A.IfMain,
            A.Status,A.Comments,
            D.Code,D.Description,D.Quantity,
            (Select Name From [SYS_Parameters] where ParameterID = D.GroupID) as GroupCode,
            (Select Name From [SYS_Parameters] where ParameterID = D.ClassID) as ClassName,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@" from [SFC_ItemResource] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Resources] D on A.ResourceID  =D.ResourceID
            where A.[SystemID]='{0}' and A.Status= '{0}0201213000001' and A.[ItemOperationID]='{1}' and A.[Type]='{0}{2}'", Framework.SystemID, ItemOperationID, Type);

            string orderby = " order by A.[ID] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据製品製程，删除数据
        /// SAM 2017年6月29日09:11:55
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ItemProcessID"></param>
        /// <returns></returns>
        public static bool Delete(string userId, string ItemProcessID)
        {
            try
            {
                string sql = String.Format(@"update[SFC_ItemResource] set {0},[Status]='{1}0201213000003'
                where [ItemProcessID]='{2}'", UniversalService.getUpdateUTC(userId), Framework.SystemID, ItemProcessID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, null) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 根据工序删除工序资源
        /// SAM 2017年6月29日15:03:14
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ItemOperationID"></param>
        /// <returns></returns>
        public static bool DeleteByOperation(string userId, string ItemOperationID)
        {
            try
            {
                string sql = String.Format(@"update[SFC_ItemResource] set {0},[Status]='{1}0201213000003'
                where [ItemOperationID]='{2}'", UniversalService.getUpdateUTC(userId), Framework.SystemID, ItemOperationID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, null) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 删除单一资源
        /// SAM 2017年7月27日12:16:02
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ItemResourceID"></param>
        /// <returns></returns>
        public static bool delete(string userId, string ItemResourceID)
        {
            try
            {
                string sql = string.Format(@"update [SFC_ItemResource] set  {0},
                [Status]='{2}0201213000003'
                where [ItemResourceID] = '{1}' ", UniversalService.getUpdateUTC(userId), ItemResourceID, Framework.SystemID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }

            catch (Exception ex)
            {

                DataLogerService.writeerrlog(ex);
                return false;
            }
        }


        /// <summary>
        /// 根据制程或者制程工序或者类型获取资源列表
        /// SAM 2017年7月13日23:12:14
        /// </summary>
        /// <param name="ItemProcessID"></param>
        /// <param name="ItemOperationID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static IList<SFC_ItemResource> GetResourceList(string ItemProcessID, string ItemOperationID, string Type)
        {
            string sql = string.Format(
                @"select * from [SFC_ItemResource]        
                  where [SystemID] = '{0}' and [Status] = '{0}0201213000001'", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(ItemProcessID))
                sql += string.Format(@" and [ItemProcessID] ='{0}'", ItemProcessID);

            if (!string.IsNullOrWhiteSpace(ItemOperationID))
                sql += string.Format(@" and [ItemOperationID] ='{0}'", ItemOperationID);

            if (!string.IsNullOrWhiteSpace(Type))
                sql += string.Format(@" and [Type]='{0}' ", Type);

            string orderby = " order by [CreateLocalTime] desc";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }


        /// <summary>
        /// 根据指令制程或者制令制程工序或者类型获取资源列表
        /// SAM 2017年7月13日22:04:06
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetResourceList(string ItemProcessID, string ItemOperationID, string Type)
        {
            string sql = string.Format(
                @"select A.ItemResourceID, A.ItemProcessID,A.ItemOperationID,A.ResourceID,A.Comments,A.IfMain,
                D.Quantity,D.Code as ResourceCode,D.Description as ResourceName,
                (select [Name] from [SYS_Parameters] where D.ClassID = ParameterID) as ClassName,
                (select [Code] from [SYS_Parameters] where D.ClassID = ParameterID) as ClassCode,
               (CASE WHEN A.[ItemProcessID] is null THEN (Select Sequence from [SFC_ItemProcess] where [ItemProcessID]=C.[ItemProcessID])
                ELSE B.Sequence END) as ProcessSequence,
                (CASE WHEN A.[ItemProcessID] is null THEN (select [Code] from [SYS_Parameters] where ParameterID = (Select Top 1 ProcessID from [SFC_ItemProcess] where [ItemProcessID]=C.[ItemProcessID]))
                ELSE(select [Code] from [SYS_Parameters] where B.ProcessID = ParameterID) END) as ProcessCode,
                (CASE WHEN A.[ItemProcessID] is null THEN (select [Name] from [SYS_Parameters] where ParameterID = (Select Top 1 ProcessID from [SFC_ItemProcess] where [ItemProcessID]=C.[ItemProcessID]))
                ELSE(select [Code] from [SYS_Parameters] where B.ProcessID = ParameterID) END) as ProcessName,
                C.Sequence as OperationSequence,
                (select [Code] from [SYS_Parameters] where C.OperationID = ParameterID) as OperationIDCode,
                (select [Name] from [SYS_Parameters] where C.OperationID = ParameterID) as OperationIDName
                from [SFC_ItemResource] A
                left join [SFC_ItemProcess] B on B.[ItemProcessID] = A.[ItemProcessID]
                left join [SFC_ItemOperation] C on C.[ItemOperationID] = A.[ItemOperationID]
                left join [SYS_Resources] D on D.[ResourceID] = A.[ResourceID]       
                  where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001'", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(ItemProcessID))
                sql += string.Format(@" and A.[ItemProcessID] ='{0}'", ItemProcessID);

            if (!string.IsNullOrWhiteSpace(ItemOperationID))
                sql += string.Format(@" and A.[ItemOperationID] ='{0}'", ItemOperationID);

            if (!string.IsNullOrWhiteSpace(Type))
                sql += string.Format(@" and A.[Type]='{0}' ", Type);

            string orderby = " order by [Type]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据制程或者制程工序流水号获取资源列表(分页)
        /// MOUSE 2017年8月1日15:09:04
        /// </summary>
        /// <param name="ItemProcessID"></param>制品制程流水号
        /// <param name="ItemOperationID"></param>制品工序流水号
        /// <param name="Type"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001BOMResourceList(string ItemProcessID, string ItemOperationID, string Type, int page, int rows, ref int count)
        {
            string select = string.Format(
                 @"select A.*, D.Code as ResourceCode,D.Description as ResourceName,
                (select [Name] from [SYS_Parameters] where D.ClassID = ParameterID) as ClassName,
                (select [Code] from [SYS_Parameters] where D.ClassID = ParameterID) as ClassCode,
               (CASE WHEN A.[ItemProcessID] is null THEN (Select Sequence from [SFC_ItemProcess] where [ItemProcessID]=C.[ItemProcessID])
                ELSE B.Sequence END) as ProcessSequence,
                (CASE WHEN A.[ItemProcessID] is null THEN (select [Code] from [SYS_Parameters] where ParameterID = (Select Top 1 ProcessID from [SFC_ItemProcess] where [ItemProcessID]=C.[ItemProcessID]))
                ELSE(select [Code] from [SYS_Parameters] where B.ProcessID = ParameterID) END) as ProcessCode,
                (CASE WHEN A.[ItemProcessID] is null THEN (select [Name] from [SYS_Parameters] where ParameterID = (Select Top 1 ProcessID from [SFC_ItemProcess] where [ItemProcessID]=C.[ItemProcessID]))
                ELSE(select [Code] from [SYS_Parameters] where B.ProcessID = ParameterID) END) as ProcessName,
                C.Sequence as OperationSequence,
                (select [Code] from [SYS_Parameters] where C.OperationID = ParameterID) as OperationIDCode,
                (select [Name] from [SYS_Parameters] where C.OperationID = ParameterID) as OperationIDName");

            string sql = string.Format(@"from [SFC_ItemResource] A
                left join [SYS_MESUsers] B on A.Creator = B.MESUserID
                left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
                left join [SFC_ItemProcess] B on B.[ItemProcessID] = A.[ItemProcessID]
                left join [SFC_ItemOperation] C on C.[ItemOperationID] = A.[ItemOperationID]
                left join [SYS_Resources] D on D.[ResourceID] = A.[ResourceID]   
                  where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001'", Framework.SystemID);


            if (!string.IsNullOrWhiteSpace(ItemProcessID))
                sql += string.Format(@" and A.[ItemProcessID] ='{0}'", ItemProcessID);

            if (!string.IsNullOrWhiteSpace(ItemOperationID))
                sql += string.Format(@" and A.[ItemOperationID] ='{0}'", ItemOperationID);

            count = UniversalService.getCount(sql, null);

            if (!string.IsNullOrWhiteSpace(Type))
                sql += string.Format(@" and A.[Type]='{0}' ", Type);

            string orderby = " order by [Type]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

       /// <summary>
       /// 判断是否存在制程/工序资源
       /// SAM 2017年8月16日15:59:37
       /// </summary>
       /// <param name="ResourceID"></param>
       /// <param name="ItemProcessID"></param>
       /// <param name="ItemOperationID"></param>
       /// <returns></returns>
        public static bool Check(string ResourceID, string ItemProcessID, string ItemOperationID)
        {
            string sql = String.Format(@"select Top 1 * from [SFC_ItemResource] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(ResourceID))
                sql = sql + String.Format(@" and [ResourceID] = '{0}' ", ResourceID);

            if (!string.IsNullOrWhiteSpace(ItemProcessID))
                sql += string.Format(@" and [ItemProcessID] ='{0}'", ItemProcessID);

            if (!string.IsNullOrWhiteSpace(ItemOperationID))
                sql += string.Format(@" and [ItemOperationID] ='{0}'", ItemOperationID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取所有的正常的制程/工序资源列表
        /// SAM 2017年10月6日13:58:26
        /// </summary>
        /// <returns></returns>
        public static IList<SFC_ItemResource> GetList()
        {
            string sql = string.Format(
                @"select * from [SFC_ItemResource]       
                  where [SystemID] = '{0}' and [Status] = '{0}0201213000001' ", Framework.SystemID);

            string orderby = " order by [ID]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }
    }
}

