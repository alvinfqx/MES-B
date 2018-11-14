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
    public class SYS_PlantAreaService : SuperModel<SYS_PlantArea>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年4月26日14:36:03
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_PlantArea Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_PlantArea]([PlantAreaID],[Code],[Name],[PlantID],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@PlantAreaID,@Code,@Name,@PlantID,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", 
                 userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@PlantAreaID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.VarChar),
                    new SqlParameter("@PlantID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    };

                parameters[0].Value = (Object)Model.PlantAreaID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[3].Value = (Object)Model.PlantID ?? DBNull.Value;
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
        /// SAM 2017年4月26日14:36:17
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_PlantArea Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_PlantArea] set {0},
                [Name]=@Name,[PlantID]=@PlantID,[Status]=@Status,[Comments]=@Comments 
                where [PlantAreaID]=@PlantAreaID", UniversalService.getUpdateUTC(userId));

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@PlantAreaID",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.VarChar),
                    new SqlParameter("@PlantID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.PlantAreaID;
                parameters[1].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[2].Value = (Object)Model.PlantID ?? DBNull.Value;
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
        /// 获取单一实体
        /// SAM 2017年4月26日14:36:27
        /// </summary>
        /// <param name="PlantAreaID"></param>
        /// <returns></returns>
        public static SYS_PlantArea get(string PlantAreaID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_PlantArea] where [PlantAreaID] = '{0}'  and [SystemID] = '{1}' ", PlantAreaID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 删除
        /// SAM 2017年4月26日14:36:35
        /// </summary>
        /// <param name="PlantAreaID"></param>
        /// <returns></returns>
        public static bool delete(string PlantAreaID)
        {
            try
            {
                string sql = string.Format(@"delete from [SYS_PlantArea] where [PlantAreaID] = '{0}'  and [SystemID] = '{1}' ", PlantAreaID, Framework.SystemID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }

            catch (Exception ex)
            {

                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 判断代号是否重复
        /// SAM 2017年4月26日14:51:26
        /// 重复返回true，不重复返回false
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="PlantAreaID"></param>
        /// <returns></returns>
        public static bool CheckCode(string Code, string PlantAreaID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_PlantArea] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

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
            if (!string.IsNullOrWhiteSpace(PlantAreaID))
                sql = sql + String.Format(@" and [PlantAreaID] <> '{0}' ", PlantAreaID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// 获取厂区列表
        /// SAM 2017年4月26日15:37:35
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00001getPlantAreaList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.PlantAreaID,A.Code,A.Name,A.Status,A.Comments,
            B.Code as PlantCode,B.Name as PlantName,A.PlantID,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_PlantArea] A 
            left join [SYS_Organization] B on A.PlantID= B.OrganizationID
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003'", Framework.SystemID);

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

            String orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 厂区要导出的数据
        /// SAM 2017年4月26日16:57:24
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static DataTable GetExportList(string Code)
        {
            string select = string.Format(@"select A.Code,A.Name,
            B.Code as PlantCode,B.Code as PlantName,
            A.Comments,C.Name as Status,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_PlantArea] A 
            left join [SYS_Organization] B on A.PlantID= B.OrganizationID
            left join [SYS_Parameters] C on A.Status=C.ParameterID
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003'", Framework.SystemID);

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

            string orderBy = "order By A.Code ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy,CommandType.Text, parameters);
        }

        /// <summary>
        /// 判断指定厂别是否存在厂区中
        /// SAM  2017年4月28日17:32:33
        /// true 表示存在，FALSE表示不存在
        /// </summary>
        /// <param name="PlantID"></param>
        /// <returns></returns>
        public static bool CheckPlant(string PlantID)
        {
            string sql = string.Format(@"select * from [SYS_PlantArea] where [PlantID] = '{0}' and [SystemID] = '{1}' and [Status] <> '{1}0201213000003'", PlantID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据厂区代号获取厂区
        /// SAM 2017年5月22日17:53:20
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static SYS_PlantArea getByCode(string Code)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_PlantArea] where [Code] = '{0}'  and [SystemID] = '{1}' and [Status] <> '{1}0201213000003'", Code, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }


        /// <summary>
        /// 厂区的弹窗
        /// SAM 2017年5月24日14:59:41
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetPlantAreaList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.PlantAreaID,A.Code,A.Name,A.Comments,C.Name as Status,
            B.Code as PlantCode,B.Name as PlantName,A.PlantID,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_PlantArea] A 
            left join [SYS_Organization] B on A.PlantID= B.OrganizationID
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            left join [SYS_Parameters] C on A.[Status] = C.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001'", Framework.SystemID);

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
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            count = UniversalService.getCount(sql, Parcount);

            String orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }
    }
}

