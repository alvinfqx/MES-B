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
    public class SYS_OrganizationService : SuperModel<SYS_Organization>
    {
        /// <summary>
        /// 添加
        /// SAM 2017年4月26日15:10:07
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_Organization Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_Organization]([OrganizationID],[ParentOrganizationID],[Name],[Code],[Type],[Status],[Sequence],[PlantID],[IfTop],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@OrganizationID,@ParentOrganizationID,@Name,@Code,@Type,@Status,@Sequence,@PlantID,@IfTop,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')"
                , userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@OrganizationID",SqlDbType.VarChar),
                    new SqlParameter("@ParentOrganizationID",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.VarChar),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.TinyInt),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    new SqlParameter("@PlantID",SqlDbType.VarChar),
                    new SqlParameter("@IfTop",SqlDbType.Bit)
                    };

                parameters[0].Value = (object)Model.OrganizationID ?? DBNull.Value;
                parameters[1].Value = (object)Model.ParentOrganizationID ?? DBNull.Value;
                parameters[2].Value = (object)Model.Name ?? DBNull.Value;
                parameters[3].Value = (object)Model.Code ?? DBNull.Value;
                parameters[4].Value = (object)Model.Type ?? DBNull.Value;
                parameters[5].Value = (object)Model.Status ?? DBNull.Value;
                parameters[6].Value = (object)Model.Sequence ?? DBNull.Value;
                parameters[7].Value = (object)Model.Comments ?? DBNull.Value;
                parameters[8].Value = (object)Model.PlantID ?? DBNull.Value;
                parameters[9].Value = (object)Model.IfTop ?? DBNull.Value;

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
        /// SAM 2017年4月26日15:10:23
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_Organization Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_Organization] set {0},
                [ParentOrganizationID]=@ParentOrganizationID,[Name]=@Name,[Code]=@Code,[Status]=@Status,[Sequence]=@Sequence,[Comments]=@Comments,
                [PlantID]=@PlantID,[IfTop]=@IfTop
                where [OrganizationID]=@OrganizationID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@OrganizationID",SqlDbType.VarChar),
                    new SqlParameter("@ParentOrganizationID",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.TinyInt),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    new SqlParameter("@PlantID",SqlDbType.VarChar),
                    new SqlParameter("@IfTop",SqlDbType.Bit)
                    };

                parameters[0].Value = Model.OrganizationID;
                parameters[1].Value = (Object)Model.ParentOrganizationID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[7].Value = (object)Model.PlantID ?? DBNull.Value;
                parameters[8].Value = (object)Model.IfTop ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 获取仓库弹窗列表
        /// Tom 2017年6月29日01点23分
        /// </summary>
        /// <param name="code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetWarehouseList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.OrganizationID,A.Code,A.Name,A.ParentOrganizationID,A.Status,A.Comments,A.Sequence,
             B.UserName as Creator,A.CreateLocalTime as CreateTime,D.Code as PlantCode,D.Name as PlantName,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Organization] D on A.[ParentOrganizationID] = D.[OrganizationID]
            where A.[SystemID]='{0}' and A.[Status] = 1 and A.[Type]='{0}020121300001F'  ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;


            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            count = UniversalService.getCount(sql, Parcount);

            String orderby = "A.[Code] asc";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取单一实体
        /// SAM 2017年4月26日15:10:55
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static SYS_Organization get(string OrganizationID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Organization] where [OrganizationID] = '{0}'  and [SystemID] = '{1}' ", OrganizationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 判断代号是否重复
        /// SAM 2017年4月26日14:51:26
        /// 重复返回true，不重复返回false
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool CheckCode(string Code, string type, string OrganizationID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_Organization] where [SystemID]='{0}' and [Status] <> 2  and [Type]= '{1}' ", Framework.SystemID, type);

            /*先定义Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            /*因为Code是通过手动输入的，所以需要用参数的形式去拼SQL*/
            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql = sql + String.Format(@" and [Code] =@Code ");
                parameters[0].Value = Code;
            }

            /*MaterialStructureID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(OrganizationID))
                sql = sql + String.Format(@" and [OrganizationID] <> '{0}' ", OrganizationID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据代号获取厂别
        /// SAM 2017年4月26日16:40:14
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static SYS_Organization getByCode(string Code, string Tpye)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Organization] where [Code] = '{0}'  and [SystemID] = '{1}'  and Status <> 2 and Type = '{2}'", Code, Framework.SystemID, Tpye);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 厂别的弹窗
        /// SAM 2017年5月4日11:36:29
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetPlantList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.OrganizationID,A.Code,A.Name,A.Comments,A.Status,A.Type,
             A.Code+'-'+A.Name as NewName,A.PlantID,
             B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join SYS_MESUsers B on A.Creator = B.MESUserID
            left join SYS_MESUsers C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.[Status]=1 and A.[Type]='{0}020121300001E' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Status] desc,A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 部门的弹窗
        /// SAM 2017年5月25日23:07:50
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="IsPlant"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetDeptList(string Code, string Plant, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.OrganizationID,A.Code,A.Name,A.Comments,A.Status,A.IfTop,A.Type,
             A.Code+'-'+A.Name as NewName,A.PlantID,
             B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join SYS_MESUsers B on A.Creator = B.MESUserID
            left join SYS_MESUsers C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.[Status]=1 and A.[Type]='{0}020121300001D' ", Framework.SystemID);

            if (!String.IsNullOrWhiteSpace(Plant))
                sql += string.Format(@" and A.[PlantID] = '{0}' ", Plant);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取厂别的列表
        /// SAM 2017年4月26日15:20:03
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00001getPlantList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.OrganizationID,A.Code,A.Name,A.Status,A.Comments,A.Sequence,
             B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join SYS_MESUsers B on A.Creator = B.MESUserID
            left join SYS_MESUsers C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.[Status] <> 2 and A.[Type]='{0}020121300001E' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + String.Format(@" and A.[Code] collate Chinese_PRC_CI_AS like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            count = UniversalService.getCount(sql, Parcount);

            String orderby = "A.[Status] desc,A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取厂别的导出信息
        /// SAM 2017年4月26日16:47:03
        /// </summary>
        /// <param name="organizationID"></param>
        /// <param name="yearMonth"></param>
        /// <returns></returns>
        public static DataTable GetExportList(string Code)
        {
            string select = string.Format(@"select A.Code,A.Name,A.Comments,
            (CASE WHEN A.Status=1 THEN '正常' ELSE '作废' END),
             B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join SYS_MESUsers B on A.Creator = B.MESUserID
            left join SYS_MESUsers C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.[Status] <> 2 and A.[Type]='{0}020121300001E' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
            }

            string orderBy = "order By A.[Status] desc,A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        /// 获取仓库的列表
        /// SAM 2017年5月4日11:15:25
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00012GetList(string Code, string Status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.OrganizationID,A.Code,A.Name,A.ParentOrganizationID,A.Status,A.Comments,A.Sequence,
             B.UserName as Creator,A.CreateLocalTime as CreateTime,D.Code as PlantCode,D.Name as PlantName,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Organization] D on A.[ParentOrganizationID] = D.[OrganizationID]
            where A.[SystemID]='{0}' and A.[Status] <> 2 and A.[Type]='{0}020121300001F'  ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;


            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(Status))
                sql = sql + string.Format(@" and A.[Status] ={0} ", Status);

            count = UniversalService.getCount(sql, Parcount);

            String orderby = "A.[Status] desc,A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取仓库的导出信息
        /// SAM 2017年5月4日11:42:36
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static DataTable Inf00012GetExportList(string Code, string Status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status] desc,A.[Code]),A.Code,A.Name,D.Code as PlantCode,A.Comments,
            (CASE WHEN A.Status=1 THEN '正常' ELSE '作废' END),
             B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join SYS_MESUsers B on A.Creator = B.MESUserID
            left join SYS_MESUsers C on A.Modifier = C.MESUserID
            left join [SYS_Organization] D on A.[ParentOrganizationID] = D.[OrganizationID]
            where A.[SystemID]='{0}' and A.[Status] <> 2 and A.[Type]='{0}020121300001F' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(Status))
                sql = sql + string.Format(@" and A.[Status] ={0} ", Status);

            string orderBy = "order By A.[Status] desc,A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        /// 判断厂别是否在部门中使用
        /// SAM 2017年5月18日23:04:10
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool Inf00001Check(string OrganizationID)
        {
            string sql = string.Format(@"select * from [SYS_Organization] where [PlantID] = '{0}' and [SystemID] = '{1}' and [Status] <> 2 and [Type]='{1}020121300001D' ", OrganizationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断厂别是否在仓库中使用
        /// SAM 2017年7月19日22:25:47
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool Inf00012Check(string OrganizationID)
        {
            string sql = string.Format(@"select * from [SYS_Organization] where [ParentOrganizationID] = '{0}' and [SystemID] = '{1}' and [Status] <> 2 and [Type]='{1}020121300001F' ", OrganizationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 部门主档列表
        /// SAM 2017年5月18日23:44:24
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00005GetList(string Code, string Status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.OrganizationID,A.Code,A.Name,A.ParentOrganizationID,A.Status,A.Comments,A.Sequence,A.PlantID,A.IfTop,
                D.Code as ParentCode,D.Name as ParentName,A.Type,
                (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Organization] D on A.[ParentOrganizationID] = D.[OrganizationID]
            where A.[SystemID]='{0}' and A.[Status] <> 2 and A.[Type] = '{0}020121300001D' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(Status))
                sql = sql + string.Format(@" and A.[Status] ={0} ", Status);

            count = UniversalService.getCount(sql, Parcount);

            String orderby = "A.[Status] desc,A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 部门的导出
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static DataTable Inf00005Export(string Code, string Status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status] desc,A.[Code]),
            A.Code,A.Name,A.Comments,F.Code+'-'+F.Name as Plant,(CASE WHEN A.IfTop=1 THEN 'Y' ELSE 'N' END),D.Code as ParentCode,
           (CASE WHEN A.Status=1 THEN '正常' ELSE '作废' END),
           (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Organization] D on A.[ParentOrganizationID] = D.[OrganizationID]
            left join [SYS_Parameters] E on A.[Type] = E.[ParameterID]
            left join [SYS_Organization] F on A.[PlantID] = F.[OrganizationID]
            where A.[SystemID]='{0}' and A.[Status] <> 2 and A.[Type] = '{0}020121300001D'", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;


            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(Status))
                sql = sql + string.Format(@" and A.[Status] ={0} ", Status);

            string orderBy = "order By A.[Status] desc,A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        /// 判断指定厂别是否已经存在正常的最上级部门
        /// SAM 2017年5月19日09:31:18
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool CheckIfTop(string PlantID, string OrganizationID)
        {
            string sql = string.Format(@"select * from [SYS_Organization] where [IfTop]=1 and [PlantID] = '{0}' and [SystemID] = '{1}' and [Status] <> 2 and [Type]='{1}020121300001D' and [OrganizationID] <> '{2}'", PlantID, Framework.SystemID, OrganizationID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断部门是否已经是其他部门的上层部门
        /// SAM 2017年5月19日10:22:43
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool CheckParent(string OrganizationID)
        {
            string sql = string.Format(@"select * from [SYS_Organization] where [ParentOrganizationID]='{0}' and [OrganizationID] <> '{0}' and [SystemID] = '{1}' and [Status] <> 2 and [Type]='{1}020121300001D' ", OrganizationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断部门是否映射了用户
        /// SAM 2017年5月19日10:25:29
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool CheckUserMapping(string OrganizationID)
        {
            string sql = string.Format(@"select * from [SYS_UserOrganizationMapping] where  [OrganizationID] = '{0}' and [SystemID] = '{1}' ", OrganizationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断部门是否映射了角色
        /// SAM 2017年5月19日10:27:18
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool CheckRoleMapping(string OrganizationID)
        {
            string sql = string.Format(@"select * from [SYS_OrganizationRoleMapping] where [OrganizationID] = '{0}' and [SystemID] = '{1}' ", OrganizationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据厂别获取他下面所有正常的部门
        /// SAM 2017年5月19日10:53:55
        /// </summary>
        /// <param name="plantID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00005GetDeptList(string PlantCode)
        {
            string select = string.Format(@"select A.OrganizationID,A.Code,A.Name,A.Code+'-'+A.Name as NewName,
            D.Code as ParentCode,D.Name as ParentName,
            (CASE WHEN  A.OrganizationID=A.ParentOrganizationID THEN '0' ELSE A.ParentOrganizationID END) as ParentOrganizationID ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join [SYS_Organization] D on A.[PlantID] = D.[OrganizationID]
            where A.[SystemID]='{0}' and A.[Status] =1 and A.[Type]='{0}020121300001D' and D.[Code]='{1}'", Framework.SystemID, PlantCode);

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text, null);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 根据用户获取他的组织结构编号
        /// SAM 2017年5月25日16:45:56
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string getUserOrg(string userID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_UserOrganizationMapping] where [UserID] = '{0}' and [SystemID] = '{1}' ", userID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]).OrganizationID;
        }

        /// <summary>
        /// 根据类别代号获取不归属于他的数据
        /// SAM 2017年6月1日16:52:041
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static object Inf00016GetNotAuthorityList(string ClassID, string Code)
        {
            string select = string.Format(@"select null as DocumentAuthorityID,A.OrganizationID as AuthorityID,A.Code,A.Name ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            where [OrganizationID] not in (select [AuthorityID] from [SYS_DocumentAuthority] where [ClassID] ='{1}' and [Status] = '{0}0201213000001' and [Attribute]=0)
            and A.[SystemID]='{0}' and A.[Status] =1 ", Framework.SystemID, ClassID);

            if (!string.IsNullOrWhiteSpace(Code))
                sql += string.Format(@"and A.Code like '%{0}%' ", Code);

            string orderBy = "order By A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, null);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 账号管理-获取所有有厂别的部门
        /// SAM 2017年7月4日11:15:34
        /// </summary>
        /// <returns></returns>
        public static IList<Hashtable> Inf00003GetOrganization()
        {
            string select = string.Format(@"select A.OrganizationID,A.Code,A.Name,A.Code+'-'+A.Name as NewName ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            where A.[SystemID]='{0}' and A.[Status] =1 and A.[Type]='{0}020121300001D' and A.[PlantID] is not null ", Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取所有的正常的部门的实体集
        /// Sam 2017年10月17日14:58:32
        /// </summary>
        /// <returns></returns>
        public static IList<SYS_Organization> GetList()
        {
            string sql = string.Format(@"select * from [SYS_Organization] where [SystemID] = '{0}' and [Type]='{0}020121300001D'  and [Status]=1 ", Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToList(dt);
        }
    }
}

