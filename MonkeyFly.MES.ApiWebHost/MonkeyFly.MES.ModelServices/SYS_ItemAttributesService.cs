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
    public class SYS_ItemAttributesService : SuperModel<SYS_ItemAttributes>
    {
        /// <summary>
        /// ����
        /// SAM 2017��5��16��14:43:47
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_ItemAttributes Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_ItemAttributes]([ItemAttributeID],[ItemID],[AttributeID],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                (@ItemAttributeID,@ItemID,@AttributeID,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ItemAttributeID",SqlDbType.VarChar),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@AttributeID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    };

                parameters[0].Value = (Object)Model.ItemAttributeID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.AttributeID ?? DBNull.Value;
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
        /// ����
        /// SAM 2017��5��16��14:44:00
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_ItemAttributes Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_ItemAttributes] set {0},
                [AttributeID]=@AttributeID,[Status]=@Status,[Comments]=@Comments 
                where [ItemAttributeID]=@ItemAttributeID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ItemAttributeID",SqlDbType.VarChar),
                    new SqlParameter("@AttributeID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.ItemAttributeID;
                parameters[1].Value = (Object)Model.AttributeID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017��5��16��14:44:13
        /// </summary>
        /// <param name="ItemAttributeID"></param>
        /// <returns></returns>
        public static SYS_ItemAttributes get(string ItemAttributeID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_ItemAttributes] where [ItemAttributeID] = '{0}'  and [SystemID] = '{1}' ", ItemAttributeID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// �ж���ϸ�Ƿ��ظ���
        /// SAM 2017��5��16��16:37:21
        /// </summary>
        /// <param name="AttributeID"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static bool Check(string AttributeID, string ItemID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_ItemAttributes] where [SystemID]='{0}' and Status <> '{0}0201213000003'", Framework.SystemID);

            /*�ȶ���Code��Ĭ�ϸ�DbNull,��������Ҳ���*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@AttributeID",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(AttributeID))
            {
                sql = sql + String.Format(@" and [AttributeID] =@AttributeID ");
                parameters[0].Value = AttributeID;
            }

            if (!string.IsNullOrWhiteSpace(ItemID))
                sql = sql + String.Format(@" and [ItemID] = '{0}' ", ItemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ��ȡ��Ʒ�������б�
        /// SAM 2017��5��16��16:33:17
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="page">ҳ��</param>
        /// <param name="rows">����</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00010AttributeGetList(string ItemID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ItemAttributeID,A.ItemID,A.AttributeID,A.Status,
            D.Code as AttributeCode,D.Name as AttributeName,A.Comments,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_ItemAttributes] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.AttributeID = D.ParameterID
            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003' and A.ItemID='{1}' ", Framework.SystemID, ItemID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Status],D.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ������Ʒ��ȡ��Ʒ�����б�
        /// SAM 2017��7��20��15:16:30
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetAttributeGetList(string ItemID)
        {
            string select = string.Format(@"select A.ItemAttributeID,A.ItemID,A.AttributeID,A.Comments ");

            string sql = string.Format(@" from [SYS_ItemAttributes] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.Status = '{0}0201213000001' and A.ItemID='{1}' ", Framework.SystemID, ItemID);

            string orderby = " order by A.[ID] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select+sql+orderby, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt);
        }


        /// <summary>
        /// �ж���Ʒ�����Ƿ�����Ʒ������ʹ��
        /// SAM 2017��7��24��10:17:51
        /// </summary>
        /// <param name="AttributeID"></param>
        /// <returns></returns>
        public static bool CheckAttribute(string AttributeID)
        {
            string sql = string.Format(@"select * from [SYS_ItemAttributes] where  [AttributeID] = '{0}' and [SystemID] = '{1}' and [Status] <> '{1}0201213000003' ", AttributeID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
    }
}

