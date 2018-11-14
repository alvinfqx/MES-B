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
    public class SYS_DocumentAuthorityService : SuperModel<SYS_DocumentAuthority>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年6月1日16:27:22
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_DocumentAuthority Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_DocumentAuthority]([DocumentAuthorityID],[ClassID],[Attribute],[AuthorityID],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@DocumentAuthorityID,@ClassID,@Attribute,@AuthorityID,@Status,
                @Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@DocumentAuthorityID",SqlDbType.VarChar),
                    new SqlParameter("@ClassID",SqlDbType.VarChar),
                    new SqlParameter("@Attribute",SqlDbType.Bit),
                    new SqlParameter("@AuthorityID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.DocumentAuthorityID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.ClassID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Attribute ?? DBNull.Value;
                parameters[3].Value = (Object)Model.AuthorityID ?? DBNull.Value;
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
        /// 更新
        /// SAM 2017年6月1日16:27:28
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_DocumentAuthority Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_DocumentAuthority] set {0},
                [Attribute]=@Attribute,[AuthorityID]=@AuthorityID,
                [Status]=@Status where [DocumentAuthorityID]=@DocumentAuthorityID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@DocumentAuthorityID",SqlDbType.VarChar),
                    new SqlParameter("@Attribute",SqlDbType.Bit),
                    new SqlParameter("@AuthorityID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.DocumentAuthorityID;
                parameters[1].Value = (Object)Model.Attribute ?? DBNull.Value;
                parameters[2].Value = (Object)Model.AuthorityID ?? DBNull.Value;
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
        /// SAM 2017年6月1日16:27:381
        /// </summary>
        /// <param name="DocumentAuthorityID"></param>
        /// <returns></returns>
        public static SYS_DocumentAuthority get(string DocumentAuthorityID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_DocumentAuthority] where [DocumentAuthorityID] = '{0}'  and [SystemID] = '{1}' ", DocumentAuthorityID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据类别获取员工权限
        /// SAM 2017年6月1日16:35:13
        /// </summary>
        /// <param name="DTSID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00016GetTAuthorityList(string DTSID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.DocumentAuthorityID,A.ClassID,A.Attribute,A.AuthorityID,
            D.Emplno as AuthorityCode,D.UserName as AuthorityName,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_DocumentAuthority] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_MESUsers] D on A.AuthorityID = D.MESUserID
            where A.[SystemID]='{0}' and A.Status ='{0}0201213000001' and A.[ClassID]='{1}' ", Framework.SystemID, DTSID);

            count = UniversalService.getCount(sql, null);

            string orderby = "D.[Emplno] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据类别获取员工权限（不分页）
        /// SAM 2017年6月1日16:40:31
        /// </summary>
        /// <param name="DTSID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00016GetTAuthorityList(string DTSID)
        {
            string select = string.Format(@"select A.DocumentAuthorityID,A.ClassID,A.Attribute,A.AuthorityID,
            D.Emplno as Code,D.UserName as Name,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_DocumentAuthority] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_MESUsers] D on A.AuthorityID = D.MESUserID
            where A.[SystemID]='{0}' and A.Status ='{0}0201213000001' and A.[ClassID]='{1}' ", Framework.SystemID, DTSID);

            string orderby = " order by D.[Emplno] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据类别获取部门权限
        /// SAM 2017年6月1日16:35:31
        /// </summary>
        /// <param name="DTSID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00016GetFAuthorityList(string DTSID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.DocumentAuthorityID,A.ClassID,A.Attribute,A.AuthorityID,
            D.Code as AuthorityCode,D.Name as AuthorityName,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_DocumentAuthority] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Organization] D on A.AuthorityID = D.OrganizationID
            where A.[SystemID]='{0}' and A.Status ='{0}0201213000001' and A.[ClassID]='{1}' ", Framework.SystemID, DTSID);

            count = UniversalService.getCount(sql, null);

            string orderby = "D.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据类别获取部门权限（不分页）
        /// SAM 2017年6月1日16:41:24
        /// </summary>
        /// <param name="DTSID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00016GetFAuthorityList(string DTSID)
        {
            string select = string.Format(@"select A.DocumentAuthorityID,A.ClassID,A.Attribute,A.AuthorityID,
            D.Code,D.Name,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_DocumentAuthority] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Organization] D on A.AuthorityID = D.OrganizationID
            where A.[SystemID]='{0}' and A.Status ='{0}0201213000001' and A.[ClassID]='{1}' ", Framework.SystemID, DTSID);

            string orderby = " order by D.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 将不在范围内的数据都改成删除
        /// SAM 2017年6月1日16:57:46
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="DocumentAuthorityIDs"></param>
        /// <returns></returns>
        public static bool Delete(string userId, string DocumentAuthorityIDs, string ClassID)
        {
            try
            {
                string sql = string.Format(@"update [SYS_DocumentAuthority] set {0},
               [Status]='{1}0201213000003' where [ClassID]='{2}' ", UniversalService.getUpdateUTC(userId), Framework.SystemID, ClassID);

                if (!string.IsNullOrWhiteSpace(DocumentAuthorityIDs))
                    sql += string.Format(@" and [DocumentAuthorityID] not in ('{0}')", DocumentAuthorityIDs);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, null) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 检查是否已存在权限映射
        /// SAM 2017年6月1日17:00:59
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static bool Check(string AuthorityID, string ClassID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_DocumentAuthority] where [SystemID]='{0}' and [Status] <> '{0}0201213000003'", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(ClassID))
                sql = sql + string.Format(@" and [ClassID] = '{0}' ", ClassID);

            if (!string.IsNullOrWhiteSpace(AuthorityID))
                sql = sql + string.Format(@" and [AuthorityID] = '{0}' ", AuthorityID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        ///// <summary>
        ///// 根据用户获取单据类型列表
        ///// Tom 
        ///// </summary>
        ///// <param name="mFCUserID"></param>
        ///// <returns></returns>
        //public static object GetTypeListByUser(string mFCUserID)
        //{
        //    string select = string.Format(
        //        @"select B.[DTSID] as value, B.Code as text ");

        //    string sql = string.Format(
        //        @"from [SYS_DocumentAuthority] A 
        //          left join [SYS_DocumentTypeSetting] B on A.ClassID = B.[DTSID]
        //          where A.[SystemID]='{0}' and 
        //                A.Status ='{0}0201213000001' and 
        //                A.[AuthorityID]='{1}' and 
        //                B.TypeID = '{0}0201213000035'", Framework.SystemID, mFCUserID);

        //    string orderby = " order by B.Name ";

        //    DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

        //    return ToHashtableList(dt);
        //}


        ///// <summary>
        ///// 根据用户获取他的完工单类别设定
        ///// SAM 2017年7月28日14:52:09
        ///// </summary>
        ///// <param name="userID"></param>
        ///// <returns></returns>
        //public static object Sfc00007GetTypeList(string userID)
        //{
        //    string select = string.Format(
        //        @"select B.[DTSID] as value, B.Code as text ");

        //    string sql = string.Format(
        //        @"from [SYS_DocumentAuthority] A 
        //          left join [SYS_DocumentTypeSetting] B on A.ClassID = B.[DTSID]
        //          where A.[SystemID]='{0}' and 
        //                A.Status ='{0}0201213000001' and 
        //                A.[AuthorityID]='{1}' and 
        //                B.TypeID = '{0}0201213000036'", Framework.SystemID, userID);

        //    string orderby = " order by B.Name ";

        //    DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

        //    return ToHashtableList(dt);
        //}

        /// <summary>
        /// 判断帐号是否在单据自定编号权限设定中使用
        /// SAM 2017年7月30日23:42:58
        /// </summary>
        /// <param name="mESUserID"></param>
        /// <returns></returns>
        public static bool CheckUser(string MESUserID)
        {
            string sql = string.Format(@"select * from [SYS_DocumentAuthority] where [AuthorityID] = '{0}' and [SystemID] = '{1}' and [Status] = '{1}0201213000001'", MESUserID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
    }
}

