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
        /// ����
        /// SAM 2017��6��22��14:32:36
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
        /// ����
        /// SAM 2017��6��22��14:32:47
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
        /// ��ȡ��һʵ��
        /// SAM 2017��6��26��17:49:45
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
        /// �Ƴ��ж��Ƿ��ظ�
        /// SAM 2017��6��22��15:18:36
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

            /*ItemResourceID��������ˮ�ţ������ڸ���ʱ���ų����Լ�*/
            if (!string.IsNullOrWhiteSpace(ItemResourceID))
                sql = sql + string.Format(@" and [ItemResourceID] <> '{0}' ", ItemResourceID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// �����ж��Ƿ��ظ�
        /// SAM 2017��6��27��18:12:39
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

            /*ItemResourceID��������ˮ�ţ������ڸ���ʱ���ų����Լ�*/
            if (!string.IsNullOrWhiteSpace(ItemResourceID))
                sql = sql + string.Format(@" and [ItemResourceID] <> '{0}' ", ItemResourceID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// �u���YԴ�б�
        /// SAM 2017��6��22��14:33:51
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
        /// ��Ʒ������Դ�б�
        /// SAM 2017��6��22��18:29:11
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
        /// �����uƷ�u�̣�ɾ������
        /// SAM 2017��6��29��09:11:55
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
        /// ���ݹ���ɾ��������Դ
        /// SAM 2017��6��29��15:03:14
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
        /// ɾ����һ��Դ
        /// SAM 2017��7��27��12:16:02
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
        /// �����Ƴ̻����Ƴ̹���������ͻ�ȡ��Դ�б�
        /// SAM 2017��7��13��23:12:14
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
        /// ����ָ���Ƴ̻��������Ƴ̹���������ͻ�ȡ��Դ�б�
        /// SAM 2017��7��13��22:04:06
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
        /// �����Ƴ̻����Ƴ̹�����ˮ�Ż�ȡ��Դ�б�(��ҳ)
        /// MOUSE 2017��8��1��15:09:04
        /// </summary>
        /// <param name="ItemProcessID"></param>��Ʒ�Ƴ���ˮ��
        /// <param name="ItemOperationID"></param>��Ʒ������ˮ��
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
       /// �ж��Ƿ�����Ƴ�/������Դ
       /// SAM 2017��8��16��15:59:37
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
        /// ��ȡ���е��������Ƴ�/������Դ�б�
        /// SAM 2017��10��6��13:58:26
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

