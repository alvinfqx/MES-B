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
    public class QCS_InspectionProjectService : SuperModel<QCS_InspectionProject>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年6月9日11:18:34
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, QCS_InspectionProject Model)
        {
            try
            {
                string sql = string.Format(@"insert[QCS_InspectionProject]([InspectionProjectID],[Code],[Name],
                [InspectionStandard],[InspectionLevel],[Disadvantages],[Attribute],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@InspectionProjectID,@Code,@Name,@InspectionStandard,@InspectionLevel,
                @Disadvantages,@Attribute,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@InspectionProjectID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.NVarChar),
                    new SqlParameter("@Name",SqlDbType.NVarChar),
                    new SqlParameter("@InspectionStandard",SqlDbType.NVarChar),
                    new SqlParameter("@InspectionLevel",SqlDbType.VarChar),
                    new SqlParameter("@Disadvantages",SqlDbType.VarChar),
                    new SqlParameter("@Attribute",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.InspectionProjectID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[3].Value = (Object)Model.InspectionStandard ?? DBNull.Value;
                parameters[4].Value = (Object)Model.InspectionLevel ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Disadvantages ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Attribute ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Status ?? DBNull.Value;
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
        /// SAM 2017年6月9日11:18:42
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, QCS_InspectionProject Model)
        {
            try
            {
                string sql = String.Format(@"update[QCS_InspectionProject] set {0},
                [InspectionStandard]=@InspectionStandard,
                [InspectionLevel]=@InspectionLevel,[Disadvantages]=@Disadvantages,[Attribute]=@Attribute,[Status]=@Status,
                [Comments]=@Comments where [InspectionProjectID]=@InspectionProjectID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@InspectionProjectID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionStandard",SqlDbType.NVarChar),
                    new SqlParameter("@InspectionLevel",SqlDbType.VarChar),
                    new SqlParameter("@Disadvantages",SqlDbType.VarChar),
                    new SqlParameter("@Attribute",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.InspectionProjectID;
                parameters[1].Value = (Object)Model.InspectionStandard ?? DBNull.Value;
                parameters[2].Value = (Object)Model.InspectionLevel ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Disadvantages ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Attribute ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017年10月17日16:05:07
        /// 新增检验项目名的修改
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool UpdateV2(string userId, QCS_InspectionProject Model)
        {
            try
            {
                string sql = String.Format(@"update[QCS_InspectionProject] set {0},
                [InspectionStandard]=@InspectionStandard,[Name]=@Name,[InspectionLevel]=@InspectionLevel,
                [Disadvantages]=@Disadvantages,[Attribute]=@Attribute,[Status]=@Status,
                [Comments]=@Comments where [InspectionProjectID]=@InspectionProjectID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@InspectionProjectID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionStandard",SqlDbType.NVarChar),
                    new SqlParameter("@InspectionLevel",SqlDbType.VarChar),
                    new SqlParameter("@Disadvantages",SqlDbType.VarChar),
                    new SqlParameter("@Attribute",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@Name",SqlDbType.NVarChar)
                    };

                parameters[0].Value = Model.InspectionProjectID;
                parameters[1].Value = (Object)Model.InspectionStandard ?? DBNull.Value;
                parameters[2].Value = (Object)Model.InspectionLevel ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Disadvantages ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Attribute ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Name ?? DBNull.Value;

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
        /// SAM 2017年6月9日11:18:57
        /// </summary>
        /// <param name="InspectionProjectID"></param>
        /// <returns></returns>
        public static QCS_InspectionProject get(string InspectionProjectID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_InspectionProject] where [InspectionProjectID] = '{0}'  and [SystemID] = '{1}' ", InspectionProjectID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据检验项目代号Code获取单一实体
        /// Alvin 2017年9月5日17:16:38
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static QCS_InspectionProject getByCode(string Code)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_InspectionProject] where [Code] = '{0}'  and [SystemID] = '{1}' ", Code, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 判断代号是否重复
        /// SAM 2017年6月16日16:20:58
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="InspectionProjectID"></param>
        /// <returns></returns>
        public static bool Check(string Code, string InspectionProjectID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_InspectionProject] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            /*先定义Name和Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            /*因为Code是通过手动输入的，所以需要用参数的形式去拼SQL*/
            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql = sql + String.Format(@" and [Code] =@Code ");
                parameters[0].Value = Code;
            }

            /*InspectionProjectID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(InspectionProjectID))
                sql = sql + String.Format(@" and [InspectionProjectID] <> '{0}' ", InspectionProjectID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// 获取检验项目列表
        /// SAM 2017年6月9日11:20:05
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00002GetProjectList(string Code, string Status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.InspectionProjectID,A.Code,A.Name,A.InspectionStandard,A.InspectionLevel,
            A.Status,A.Disadvantages,A.Attribute,A.Comments,          
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [QCS_InspectionProject] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]      
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql = sql + string.Format(@" and A.[Status] = @Status ");
                parameters[1].Value = Status;
                Parcount[1].Value = Status;
            }

            count = UniversalService.getCount(sql, Parcount);

            String orderby = " A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 检验项目的导出
        /// SAM 2017年6月11日13:14:00
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static DataTable Qcs00002ProjectExport(string Code, string Status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status],A.[Code]),
            A.Code,A.Name,D.Name as Attribute,A.InspectionStandard,G.Name as InspectionLevel,H.Name as Disadvantages,
            A.Comments,J.Name as Status,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [QCS_InspectionProject] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[Attribute] = D.[ParameterID]
            left join [SYS_Parameters] G on A.[InspectionLevel] = G.[ParameterID]
            left join [SYS_Parameters] H on A.[Disadvantages] = H.[ParameterID]
            left join [SYS_Parameters] J on A.[Status] = J.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql = sql + string.Format(@" and A.[Status] = @Status ");
                parameters[1].Value = Status;
                Parcount[1].Value = Status;
            }

            string orderBy = "order By A.[Status],A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        ///  QCS00004获取检验项目的下拉框
        ///  SAM 2017年7月6日15:13:56
        /// </summary>
        /// <returns></returns>
        public static IList<Hashtable> QCS00004GetInspectionProjectList()
        {
            string select = string.Format(@"select A.InspectionProjectID,
            A.Code,A.Name,A.InspectionStandard,A.InspectionLevel,A.Disadvantages,A.Attribute,
            G.Name as InspectionLevelName,H.Name as DisadvantagesName,J.Name as Status,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [QCS_InspectionProject] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] G on A.[InspectionLevel] = G.[ParameterID]
            left join [SYS_Parameters] H on A.[Disadvantages] = H.[ParameterID]
            left join [SYS_Parameters] J on A.[Status] = J.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID);

            string orderBy = "order By A.[Status],A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToHashtableList(dt);
        }


        /// <summary>
        /// 获取所有正常的检验项目
        /// SAM 2017年10月19日10:51:18
        /// </summary>
        /// <returns></returns>
        public static IList<QCS_InspectionProject> GetList()
        {
            string sql = string.Format(@"select * from [QCS_InspectionProject] 
            where [SystemID]='{0}' and [Status] ='{0}0201213000001'",Framework.SystemID);

            DataTable result = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            return ToList(result);
        }
    }
}

