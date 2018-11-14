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
    public class QCS_SamplingSettingService : SuperModel<QCS_SamplingSetting>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年6月11日11:30:22
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, QCS_SamplingSetting Model)
        {
            try
            {
                string sql = string.Format(@"insert[QCS_SamplingSetting]([SamplingSettingID],[CategoryID],[InspectionMethod],[Disadvantages],[AQL],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@SamplingSettingID,@CategoryID,@InspectionMethod,@Disadvantages,@AQL,
                @Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, Model.CreateTime, Model.CreateLocalTime, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@SamplingSettingID",SqlDbType.VarChar),
                    new SqlParameter("@CategoryID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionMethod",SqlDbType.VarChar),
                    new SqlParameter("@Disadvantages",SqlDbType.VarChar),
                    new SqlParameter("@AQL",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.SamplingSettingID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.CategoryID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.InspectionMethod ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Disadvantages ?? DBNull.Value;
                parameters[4].Value = (Object)Model.AQL ?? DBNull.Value;
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
        /// SAM 2017年6月11日11:30:27
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, QCS_SamplingSetting Model)
        {
            try
            {
                string sql = String.Format(@"update[QCS_SamplingSetting] set                
                [AQL]=@AQL,[Status]=@Status,[Comments]=@Comments,[Modifier]=@Modifier,
                [ModifiedTime]=@ModifiedTime,[ModifiedLocalTime]=@ModifiedLocalTime where [SamplingSettingID]=@SamplingSettingID");

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@SamplingSettingID",SqlDbType.VarChar),
                    new SqlParameter("@AQL",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@Modifier",SqlDbType.VarChar),
                    new SqlParameter("@ModifiedTime",SqlDbType.DateTime),
                    new SqlParameter("@ModifiedLocalTime",SqlDbType.DateTime),
                    };

                parameters[0].Value = Model.SamplingSettingID;
                parameters[1].Value = (Object)Model.AQL ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Modifier ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ModifiedTime ?? DBNull.Value;
                parameters[6].Value = (Object)Model.ModifiedLocalTime ?? DBNull.Value;

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
        /// SAM 2017年6月11日11:30:38
        /// </summary>
        /// <param name="SamplingSettingID"></param>
        /// <returns></returns>
        public static QCS_SamplingSetting get(string SamplingSettingID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_SamplingSetting] where [SamplingSettingID] = '{0}'  and [SystemID] = '{1}' ", SamplingSettingID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据检验类别，检验方式，缺点等级获取单一实体
        /// SAM 2017年6月11日11:31:57
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="InspectionMethod"></param>
        /// <param name="Disadvantages"></param>
        /// <returns></returns>
        public static QCS_SamplingSetting get(string CategoryID, string InspectionMethod, string Disadvantages)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_SamplingSetting] where [CategoryID] = '{0}'  and [InspectionMethod] = '{1}'  and [Disadvantages] = '{2}'  and [SystemID] = '{3}' ", CategoryID, InspectionMethod, Disadvantages, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 检验类别的抽检设定列表
        /// SAM 2017年6月11日11:39:24
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00002GetTypeDetailsList(string typeID)
        {
            string select = string.Format(@"select distinct A.CategoryID,A.InspectionMethod,A.Comments,D.Name as InspectionMethodCode,
           (Select [AQL] from [QCS_SamplingSetting] where [CategoryID]=A.[CategoryID] and [InspectionMethod] =A.[InspectionMethod] and [Disadvantages]='{1}0201213000077') as One,
           (Select [AQL] from [QCS_SamplingSetting] where [CategoryID]=A.[CategoryID] and [InspectionMethod] =A.[InspectionMethod] and [Disadvantages]='{1}0201213000078') as Two,
           (Select [AQL] from [QCS_SamplingSetting] where [CategoryID]=A.[CategoryID] and [InspectionMethod] =A.[InspectionMethod] and [Disadvantages]='{1}0201213000079') as Three,
           (Select [AQL] from [QCS_SamplingSetting] where [CategoryID]=A.[CategoryID] and [InspectionMethod] =A.[InspectionMethod] and [Disadvantages]='{1}020121300007A') as Four,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", typeID, Framework.SystemID);

            string sql = string.Format(@"  from [QCS_SamplingSetting] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[InspectionMethod] = D.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and [CategoryID] = '{1}' order by InspectionMethod ", Framework.SystemID, typeID);

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 检验类别明细的导出
        /// SAM 2017年6月11日11:49:40
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static DataTable Qcs00002TypeExport(string Code)
        {
            string select = string.Format(@"select distinct A.InspectionMethod as RowNumber,E.Code as CategoryCode,E.Name as CategoryName,E.Comments,
            (select [UserName] from [SYS_MESUsers] where MESUserID = E.[Creator]),E.CreateLocalTime as CategoryTime,
            (CASE WHEN E.CreateLocalTime=E.ModifiedLocalTime THEN NULL else (select [UserName] from [SYS_MESUsers] where MESUserID = E.[Modifier]) END),
            (CASE WHEN E.CreateLocalTime=E.ModifiedLocalTime THEN NULL else E.ModifiedLocalTime END),
            D.Name as InspectionMethodCode,
            (select [Name] from [SYS_Parameters]  where [ParameterID]=(Select [AQL] from [QCS_SamplingSetting] where [CategoryID]=A.[CategoryID] and [InspectionMethod] =A.[InspectionMethod] and [Disadvantages]='{0}0201213000077')),
            (select [Name] from [SYS_Parameters]  where [ParameterID]=(Select [AQL] from [QCS_SamplingSetting] where [CategoryID]=A.[CategoryID] and [InspectionMethod] =A.[InspectionMethod] and [Disadvantages]='{0}0201213000078')),
            (select [Name] from [SYS_Parameters]  where [ParameterID]=(Select [AQL] from [QCS_SamplingSetting] where [CategoryID]=A.[CategoryID] and [InspectionMethod] =A.[InspectionMethod] and [Disadvantages]='{0}0201213000079')),
            (select [Name] from [SYS_Parameters]  where [ParameterID]= (Select [AQL] from [QCS_SamplingSetting] where [CategoryID]=A.[CategoryID] and [InspectionMethod] =A.[InspectionMethod] and [Disadvantages]='{0}020121300007A')),
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@"  from [QCS_SamplingSetting] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[InspectionMethod] = D.[ParameterID]
            left join [SYS_Parameters] E on A.[CategoryID] = E.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001'", Framework.SystemID);

            if (!String.IsNullOrWhiteSpace(Code))
                sql += string.Format(@" and E.Code like '%{0}%'", Code);

            sql += string.Format(@" order by E.Code,RowNumber ");

            return SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);
        }

        /// <summary>
        /// 根据檢驗類別+檢驗方式+缺點等級读出AQL 
        /// SAM 2017年7月28日10:37:52
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="InspectionMethod"></param>
        /// <param name="Disadvantages"></param>
        /// <returns></returns>
        public static string getAQL(string CategoryID, string InspectionMethod, string Disadvantages)
        {
            try
            {
                string select = string.Format(@"select Top 1 A.AQL ");

                string sql = string.Format(@"  from [QCS_SamplingSetting] A 
            where A.[SystemID]='{0}' and A.[Status] ='{0}0201213000001' 
            and A.[CategoryID]='{1}' and A.[InspectionMethod] ='{2}' and A.[Disadvantages] ='{3}' 
            ", Framework.SystemID, CategoryID, InspectionMethod, Disadvantages);

                DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

                if (dt.Rows.Count == 0)
                    return null;

                return ToEntity(dt.Rows[0]).AQL;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return null;
            }
        }

        /// <summary>
        /// 根据检验类别，删除抽检设定
        /// Sam 2017年10月17日16:30:45
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public static bool Delete(string userId, string CategoryID)
        {
            try
            {
                string sql = String.Format(@"update[QCS_SamplingSetting] set 
                [Status]=@Status,[Modifier]=@Modifier,[ModifiedTime]=@ModifiedTime,
                [ModifiedLocalTime]=@ModifiedLocalTime where [CategoryID]=@CategoryID");

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CategoryID",SqlDbType.VarChar),            
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Modifier",SqlDbType.VarChar),
                    new SqlParameter("@ModifiedTime",SqlDbType.DateTime),
                    new SqlParameter("@ModifiedLocalTime",SqlDbType.DateTime),
                    };

                parameters[0].Value = CategoryID;
                parameters[1].Value = Framework.SystemID+"0201213000003";
                parameters[2].Value = userId;
                parameters[3].Value = DateTime.Now;
                parameters[4].Value = DateTime.UtcNow;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

    }
}

