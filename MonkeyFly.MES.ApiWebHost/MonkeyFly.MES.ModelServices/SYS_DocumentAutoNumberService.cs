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
    public class SYS_DocumentAutoNumberService : SuperModel<SYS_DocumentAutoNumber>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年6月1日11:49:39
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_DocumentAutoNumber Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_DocumentAutoNumber]([DocumentAutoNumberID],[ClassID],
                [DefaultCharacter],[Num],[Attribute],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@DocumentAutoNumberID,@ClassID, @DefaultCharacter,@Num,@Attribute,@Status,
                @Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                        new SqlParameter("@DocumentAutoNumberID",SqlDbType.VarChar),
                        new SqlParameter("@ClassID",SqlDbType.VarChar),
                        new SqlParameter("@DefaultCharacter",SqlDbType.NVarChar),
                        new SqlParameter("@Num",SqlDbType.Int),
                        new SqlParameter("@Attribute",SqlDbType.Bit),
                        new SqlParameter("@Status",SqlDbType.VarChar),
                        new SqlParameter("@Comments",SqlDbType.NVarChar),
                        };

                parameters[0].Value = (Object)Model.DocumentAutoNumberID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.ClassID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.DefaultCharacter ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Num ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Attribute ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;

            //    string sql = string.Format(@"insert[SYS_DocumentAutoNumber]([DocumentAutoNumberID],[ClassID],
            //[DefaultCharacter],[Num],[Attribute],[Status],
            //[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
            // ('{4}','{5}','{6}','{7}','{8}','{9}',
            //'{10}','{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID,
            //Model.DocumentAutoNumberID,Model.ClassID,Model.DefaultCharacter,Model.Num,Model.Attribute,Model.Status,Model.Comments);
            //    DataLogerService.SAMwriteLog(sql);
                //return SQLHelper.ExecuteNonQuery(sql, CommandType.Text,parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 更新
        /// SAM 2017年6月1日11:49:33
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_DocumentAutoNumber Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_DocumentAutoNumber] set {0},
            [Num]=@Num,[Attribute]=@Attribute,[Status]=@Status 
            where [DocumentAutoNumberID]=@DocumentAutoNumberID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@DocumentAutoNumberID",SqlDbType.VarChar),
                    new SqlParameter("@Num",SqlDbType.Int),
                    new SqlParameter("@Attribute",SqlDbType.Bit),         
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.DocumentAutoNumberID;
                parameters[1].Value = (Object)Model.Num ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Attribute ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Status ?? DBNull.Value;

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
        /// SAM 2017年6月2日14:21:35
        /// </summary>
        /// <param name="DocumentAutoNumberID"></param>
        /// <returns></returns>
        public static SYS_DocumentAutoNumber get(string DocumentAutoNumberID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_DocumentAutoNumber] where [DocumentAutoNumberID] = '{0}'  and [SystemID] = '{1}' ", DocumentAutoNumberID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据类别流水号和给号字轨获取记录
        /// SAM 2017年5月24日11:32:23
        /// </summary>
        /// <param name="AutoNumberID"></param>
        /// <returns></returns>
        public static SYS_DocumentAutoNumber getByAutoNumber(string AutoNumberID, string Prevchar)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_DocumentAutoNumber] where [ClassID] = '{0}' and [DefaultCharacter]='{2}' and [SystemID] = '{1}' and [Status]='{1}0201213000001' ", AutoNumberID, Framework.SystemID, Prevchar);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据类别流水号获取单据状况
        /// SAM 2017年6月1日16:04:57
        /// </summary>
        /// <param name="DTSID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00016GetAutoNumberList(string DTSID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.DefaultCharacter,A.Num,D.Name,D.Attribute,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_DocumentAutoNumber] A
            left Join [SYS_DocumentTypeSetting] D on A.ClassID = D.DTSID
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.Status ='{0}0201213000001' and A.[ClassID]='{1}' ", Framework.SystemID, DTSID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Num] desc ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 判断单据类别是否已生成了流水号
        /// SAM 2017年6月1日16:17:13
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public static bool Check(string ClassID)
        {
            string sql = string.Format(@"select * from [SYS_DocumentAutoNumber] where  [ClassID] = '{0}' and [SystemID] = '{1}' and [Status] <> '{1}0201213000003' ", ClassID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
    }
}

