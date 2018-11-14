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
    public class SFC_ItemMaterialService : SuperModel<SFC_ItemMaterial>
    {
        /// <summary>
        /// ����
        /// SAM 2017��6��22��11:16:47
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SFC_ItemMaterial Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_ItemMaterial]([ItemMaterialID],[ItemProcessID],[ItemOperationID],
                [Sequence],[ItemID],[BasicQuantity],[AttritionRate],[UseQuantity],
                [Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@ItemMaterialID,@ItemProcessID,@ItemOperationID,@Sequence,@ItemID,
                @BasicQuantity,@AttritionRate,@UseQuantity,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ItemMaterialID",SqlDbType.VarChar),
                    new SqlParameter("@ItemProcessID",SqlDbType.VarChar),
                    new SqlParameter("@ItemOperationID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.VarChar),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@BasicQuantity",SqlDbType.Decimal),
                    new SqlParameter("@AttritionRate",SqlDbType.Decimal),
                    new SqlParameter("@UseQuantity",SqlDbType.Decimal),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.ItemMaterialID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.ItemProcessID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ItemOperationID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[4].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.BasicQuantity ?? DBNull.Value;
                parameters[6].Value = (Object)Model.AttritionRate ?? DBNull.Value;
                parameters[7].Value = (Object)Model.UseQuantity ?? DBNull.Value;
                parameters[8].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[9].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017��6��23��17:51:54
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SFC_ItemMaterial Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_ItemMaterial] set {0},
            [Sequence]=@Sequence,[ItemID]=@ItemID,[BasicQuantity]=@BasicQuantity,[AttritionRate]=@AttritionRate,[UseQuantity]=@UseQuantity,
            [Status]=@Status,[Comments]=@Comments where [ItemMaterialID]=@ItemMaterialID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ItemMaterialID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.VarChar),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@BasicQuantity",SqlDbType.Decimal),
                    new SqlParameter("@AttritionRate",SqlDbType.Decimal),
                    new SqlParameter("@UseQuantity",SqlDbType.Decimal),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.ItemMaterialID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.BasicQuantity ?? DBNull.Value;
                parameters[4].Value = (Object)Model.AttritionRate ?? DBNull.Value;
                parameters[5].Value = (Object)Model.UseQuantity ?? DBNull.Value;
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
        /// ��ȡ��һʵ��
        /// SAM 2017��6��29��09:02:27
        /// </summary>
        /// <param name="ItemMaterialID"></param>
        /// <returns></returns>
        public static SFC_ItemMaterial get(string ItemMaterialID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_ItemMaterial] where [ItemMaterialID] = '{0}'  and [SystemID] = '{1}' ", ItemMaterialID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// �ж��Ϻ��Ƿ��ظ�
        /// SAM 2017��6��22��11:47:03
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="ItemMaterialID"></param>
        /// <returns></returns>
        public static bool Check(string ItemID, string ItemProcessID, string ItemOperationID, string ItemMaterialID)
        {
            string sql = String.Format(@"select Top 1 * from [SFC_ItemMaterial] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);


            if (string.IsNullOrWhiteSpace(ItemOperationID))
            {
                if (!string.IsNullOrWhiteSpace(ItemProcessID))
                    sql = sql + string.Format(@" and [ItemProcessID]= '{0}' ", ItemProcessID);
            }
            else
               sql = sql + string.Format(@" and [ItemOperationID]= '{0}' ", ItemOperationID);

            if (!string.IsNullOrWhiteSpace(ItemID))
                sql = sql + string.Format(@" and [ItemID]= '{0}' ", ItemID);

            /*ItemMaterialID��������ˮ�ţ������ڸ���ʱ���ų����Լ�*/
            if (!string.IsNullOrWhiteSpace(ItemMaterialID))
                sql = sql + string.Format(@" and [ItemMaterialID] <> '{0}' ", ItemMaterialID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ����ģ�����ж��Ϻ��Ƿ��ظ�
        /// SAM 2017��6��22��18:20:59
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="ItemOperationID"></param>
        /// <param name="ItemMaterialID"></param>
        /// <returns></returns>
        public static bool OperationCheck(string ItemID, string ItemOperationID, string ItemMaterialID)
        {
            string sql = String.Format(@"select Top 1 * from [SFC_ItemMaterial] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(ItemOperationID))
                sql = sql + string.Format(@" and [ItemOperationID]= '{0}' ", ItemOperationID);

            if (!string.IsNullOrWhiteSpace(ItemID))
                sql = sql + string.Format(@" and [ItemID]= '{0}' ", ItemID);

            /*ItemMaterialID��������ˮ�ţ������ڸ���ʱ���ų����Լ�*/
            if (!string.IsNullOrWhiteSpace(ItemMaterialID))
                sql = sql + string.Format(@" and [ItemMaterialID] <> '{0}' ", ItemMaterialID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
        /// <summary>
        /// �uƷ�u�������б�
        /// SAM 2017��6��22��11:43:56
        /// </summary>
        /// <param name="ItemProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetMaterialList(string ItemProcessID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ItemMaterialID,A.ItemProcessID,A.ItemOperationID,A.Sequence,A.ItemID,
            A.BasicQuantity,A.AttritionRate,A.UseQuantity,A.Comments,H.Drawing,H.Code as ItemCode,
           H.Name+(CASE When H.Specification is null or H.Specification ='' THEN '' ELSE '/'+H.Specification END) as NameSpecification,
            (Select Name from [SYS_Parameters] where [ParameterID] = H.[PartSource]) as PartSource,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@" from [SFC_ItemMaterial] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Items] H on A.ItemID = H.ItemID
            where A.[SystemID]='{0}' and A.Status= '{0}0201213000001' and A.[ItemProcessID]='{1}' ", Framework.SystemID, ItemProcessID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// �uƷ���������б�
        /// SAM 2017��6��22��18:18:22
        /// </summary>
        /// <param name="ItemOperationID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetOperationMaterialList(string ItemOperationID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ItemMaterialID,A.ItemProcessID,A.ItemOperationID,A.Sequence,A.ItemID,
            A.BasicQuantity,A.AttritionRate,A.UseQuantity,A.Comments,H.Drawing,H.Code as ItemCode,
           H.Name+(CASE When H.Specification is null or H.Specification ='' THEN '' ELSE '/'+H.Specification END) as NameSpecification,
            (Select Name from [SYS_Parameters] where [ParameterID] = H.[PartSource]) as PartSource,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@" from [SFC_ItemMaterial] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
       
            left join [SYS_Items] H on A.ItemID = H.ItemID
            where A.[SystemID]='{0}' and A.Status= '{0}0201213000001' and A.[ItemOperationID]='{1}' ", Framework.SystemID, ItemOperationID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// �����Ƴ̻����Ƴ̹�����ˮ�Ż�ȡ�����б�(��ҳ)
        /// MOUSE 2017��8��1��11:50:06
        /// </summary>
        /// <param name="ItemProcessID"></param>��Ʒ�Ƴ���ˮ��
        /// <param name="ItemOperationID"></param>��Ʒ������ˮ��
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001BomList(string ItemProcessID, string ItemOperationID,int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.*,D.Code,D.Name+'/'+D.Specification as NameSpecification,A.ItemID,
               (CASE WHEN A.[ItemProcessID] is null THEN (Select Sequence from [SFC_ItemProcess] where [ItemProcessID]=C.[ItemProcessID])
                ELSE B.Sequence END) as ProcessSequence,
                (CASE WHEN A.[ItemProcessID] is null THEN (select [Code] from [SYS_Parameters] where ParameterID = (Select Top 1 ProcessID from [SFC_ItemProcess] where [ItemProcessID]=C.[ItemProcessID]))
                ELSE(select [Code] from [SYS_Parameters] where B.ProcessID = ParameterID) END) as ProcessCode,
                (CASE WHEN A.[ItemProcessID] is null THEN (select [Name] from [SYS_Parameters] where ParameterID = (Select Top 1 ProcessID from [SFC_ItemProcess] where [ItemProcessID]=C.[ItemProcessID]))
                ELSE(select [Code] from [SYS_Parameters] where B.ProcessID = ParameterID) END) as ProcessName,
                C.Sequence as OperationSequence,
                (select [Code] from [SYS_Parameters] where C.OperationID = ParameterID) as OperationIDCode,
                (select [Name] from [SYS_Parameters] where C.OperationID = ParameterID) as OperationIDName");

            string sql = string.Format(@"from [SFC_ItemMaterial] A
                left join [SYS_MESUsers] B on A.Creator = B.MESUserID
                left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
                left join [SFC_ItemProcess] B on B.[ItemProcessID] = A.[ItemProcessID]
                left join [SFC_ItemOperation] C on C.[ItemOperationID] = A.[ItemOperationID]
                left join [SYS_Items] D on D.[ItemID] = A.[ItemID]
                  where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001'", Framework.SystemID);


            if (!string.IsNullOrWhiteSpace(ItemProcessID))
                sql += string.Format(@" and A.[ItemProcessID] ='{0}'", ItemProcessID);

            if (!string.IsNullOrWhiteSpace(ItemOperationID))
                sql += string.Format(@" and A.[ItemOperationID] ='{0}'", ItemOperationID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// �����uƷ�u�̣�ɾ������
        /// SAM 2017��6��29��09:08:09
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool Delete(string userId,string ItemProcessID)
        {
            try
            {
                string sql = String.Format(@"update[SFC_ItemMaterial] set {0},[Status]='{1}0201213000003'
                where [ItemProcessID]='{2}'", UniversalService.getUpdateUTC(userId),Framework.SystemID, ItemProcessID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, null) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// ���ݹ���ɾ���Ƴ̹�������
        /// SAM 2017��6��29��15:02:40
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ItemOperationID"></param>
        /// <returns></returns>
        public static bool DeleteByOperation(string userId, string ItemOperationID)
        {
            try
            {
                string sql = String.Format(@"update[SFC_ItemMaterial] set {0},[Status]='{1}0201213000003'
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
        /// �����Ƴ̻����Ƴ̹����ȡ����
        /// SAM 2017��7��13��23:06:41
        /// </summary>
        /// <param name="ItemProcessID"></param>
        /// <param name="ItemOperationID"></param>
        /// <returns></returns>
        public static IList<SFC_ItemMaterial> GetMaterialList(string ItemProcessID,string ItemOperationID)
        {
            string sql = string.Format(
                @"select * from [SFC_ItemMaterial]        
                  where [SystemID] = '{0}' and [Status] = '{0}0201213000001'", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(ItemProcessID))
                sql += string.Format(@" and [ItemProcessID] ='{0}'", ItemProcessID);

            if (!string.IsNullOrWhiteSpace(ItemOperationID))
                sql += string.Format(@" and [ItemOperationID] ='{0}'", ItemOperationID);

            string orderby = " order by [Sequence]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }

        /// <summary>
        /// �����Ƴ̻����Ƴ̹�����ˮ�Ż�ȡ�����б�
        /// SAM 2017��7��27��11:56:43
        /// </summary>
        /// <param name="ItemProcessID"></param>
        /// <param name="ItemOperationID"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetItemList(string ItemProcessID, string ItemOperationID)
        {
            string sql = string.Format(
                @"select A.*,D.Code,D.Name+'/'+D.Specification as NameSpecification,A.ItemID,
               (CASE WHEN A.[ItemProcessID] is null THEN (Select Sequence from [SFC_ItemProcess] where [ItemProcessID]=C.[ItemProcessID])
                ELSE B.Sequence END) as ProcessSequence,
                (CASE WHEN A.[ItemProcessID] is null THEN (select [Code] from [SYS_Parameters] where ParameterID = (Select Top 1 ProcessID from [SFC_ItemProcess] where [ItemProcessID]=C.[ItemProcessID]))
                ELSE(select [Code] from [SYS_Parameters] where B.ProcessID = ParameterID) END) as ProcessCode,
                (CASE WHEN A.[ItemProcessID] is null THEN (select [Name] from [SYS_Parameters] where ParameterID = (Select Top 1 ProcessID from [SFC_ItemProcess] where [ItemProcessID]=C.[ItemProcessID]))
                ELSE(select [Code] from [SYS_Parameters] where B.ProcessID = ParameterID) END) as ProcessName,
                C.Sequence as OperationSequence,
                (select [Code] from [SYS_Parameters] where C.OperationID = ParameterID) as OperationIDCode,
                (select [Name] from [SYS_Parameters] where C.OperationID = ParameterID) as OperationIDName
                from [SFC_ItemMaterial] A
                left join [SFC_ItemProcess] B on B.[ItemProcessID] = A.[ItemProcessID]
                left join [SFC_ItemOperation] C on C.[ItemOperationID] = A.[ItemOperationID]
                left join [SYS_Items] D on D.[ItemID] = A.[ItemID]
                  where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001'", Framework.SystemID);

            string orderby = " order by [Sequence]";

            if (!string.IsNullOrWhiteSpace(ItemProcessID))
                sql += string.Format(@" and A.[ItemProcessID] ='{0}'", ItemProcessID);

            if (!string.IsNullOrWhiteSpace(ItemOperationID))
                sql += string.Format(@" and A.[ItemOperationID] ='{0}'", ItemOperationID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ��ȡ���е��������Ƴ�/���������б�
        /// SAM 2017��10��6��13:58:26
        /// </summary>
        /// <returns></returns>
        public static IList<SFC_ItemMaterial> GetList()
        {
            string sql = string.Format(
                @"select * from [SFC_ItemMaterial]       
                  where [SystemID] = '{0}' and [Status] = '{0}0201213000001' ", Framework.SystemID);

            string orderby = " order by [Sequence]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }
    }
}

