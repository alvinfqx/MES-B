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
    public class QCS_StaInsSpeSettingService : SuperModel<QCS_StaInsSpeSetting>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年6月16日14:42:13
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, QCS_StaInsSpeSetting Model)
        {
            try
            {
                string sql = string.Format(@"insert[QCS_StaInsSpeSetting]([StaInsSpeSettingID],[SettingType],
            [PartID],[InspectionType],[ProcessID],[OperationID],
            [Sequence],[CategoryID],[InspectionMethod],[InspectionDay],[InspectionStandard],
            [InspectionProjectID],[Attribute],[Status],[AQL],
            [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
             (@StaInsSpeSettingID,@SettingType,
            @PartID,@InspectionType,@ProcessID,
            @OperationID,@Sequence,@CategoryID,
            @InspectionMethod,@InspectionDay,@InspectionStandard,
            @InspectionProjectID,@Attribute,@Status,@AQL,
            @Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@StaInsSpeSettingID",SqlDbType.VarChar),
                    new SqlParameter("@SettingType",SqlDbType.VarChar),
                    new SqlParameter("@PartID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionType",SqlDbType.VarChar),
                    new SqlParameter("@ProcessID",SqlDbType.VarChar),
                    new SqlParameter("@OperationID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@CategoryID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionMethod",SqlDbType.VarChar),
                    new SqlParameter("@InspectionDay",SqlDbType.Int),
                    new SqlParameter("@InspectionStandard",SqlDbType.NVarChar),
                    new SqlParameter("@InspectionProjectID",SqlDbType.VarChar),
                    new SqlParameter("@Attribute",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@AQL",SqlDbType.NVarChar)
                    };

                parameters[0].Value = (Object)Model.StaInsSpeSettingID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.SettingType ?? DBNull.Value;
                parameters[2].Value = (Object)Model.PartID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.InspectionType ?? DBNull.Value;
                parameters[4].Value = (Object)Model.ProcessID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.OperationID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[7].Value = (Object)Model.CategoryID ?? DBNull.Value;
                parameters[8].Value = (Object)Model.InspectionMethod ?? DBNull.Value;
                parameters[9].Value = (Object)Model.InspectionDay ?? DBNull.Value;
                parameters[10].Value = (Object)Model.InspectionStandard ?? DBNull.Value;
                parameters[11].Value = (Object)Model.InspectionProjectID ?? DBNull.Value;
                parameters[12].Value = (Object)Model.Attribute ?? DBNull.Value;
                parameters[13].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[14].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[15].Value = (Object)Model.AQL ?? DBNull.Value;
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
        /// SAM 2017年6月16日14:42:19
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, QCS_StaInsSpeSetting Model)
        {
            try
            {
                string sql = String.Format(@"update[QCS_StaInsSpeSetting] set {0},
                [Sequence]=@Sequence,[CategoryID]=@CategoryID,
                [InspectionMethod]=@InspectionMethod,[InspectionDay]=@InspectionDay,
                [InspectionStandard]=@InspectionStandard,[InspectionProjectID]=@InspectionProjectID,
                [Attribute]=@Attribute,[Status]=@Status,[Comments]=@Comments 
                where [StaInsSpeSettingID]=@StaInsSpeSettingID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@StaInsSpeSettingID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@CategoryID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionMethod",SqlDbType.VarChar),
                    new SqlParameter("@InspectionDay",SqlDbType.Int),
                    new SqlParameter("@InspectionStandard",SqlDbType.NVarChar),
                    new SqlParameter("@InspectionProjectID",SqlDbType.VarChar),
                    new SqlParameter("@Attribute",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.StaInsSpeSettingID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.CategoryID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.InspectionMethod ?? DBNull.Value;
                parameters[4].Value = (Object)Model.InspectionDay ?? DBNull.Value;
                parameters[5].Value = (Object)Model.InspectionStandard ?? DBNull.Value;
                parameters[6].Value = (Object)Model.InspectionProjectID ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Attribute ?? DBNull.Value;
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
        /// 获取单一实体
        /// SAM 2017年6月16日14:44:28
        /// </summary>
        /// <param name="StaInsSpeSettingID"></param>
        /// <returns></returns>
        public static QCS_StaInsSpeSetting get(string StaInsSpeSettingID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_StaInsSpeSetting] where [StaInsSpeSettingID] = '{0}'  and [SystemID] = '{1}' ", StaInsSpeSettingID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 判断重复
        /// SAM 2017年6月18日23:49:19
        /// </summary>
        /// <param name="PartID"></param>
        /// <param name="SettingType"></param>
        /// <param name="StaInsSpeSettingID"></param>
        /// <returns></returns>
        public static bool Check(string PartID, string SettingType, string StaInsSpeSettingID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_InspectionProject] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            /*先定义Name和Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            /*因为PartID是通过手动输入的，所以需要用参数的形式去拼SQL*/
            if (!string.IsNullOrWhiteSpace(PartID))
            {
                sql = sql + String.Format(@" and [PartID] =@PartID ");
                parameters[0].Value = PartID;
            }

            if (!string.IsNullOrWhiteSpace(SettingType))
                sql = sql + String.Format(@" and [SettingType] = '{0}' ", SettingType);

            /*StaInsSpeSettingID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(StaInsSpeSettingID))
                sql = sql + String.Format(@" and [StaInsSpeSettingID] <> '{0}' ", StaInsSpeSettingID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 标准检验规范设定-料品页签列表
        /// SAM 2017年6月16日14:47:05
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00004GetItemList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.StaInsSpeSettingID,A.PartID,
            D.Code,D.Name,D.Specification,D.Comments,A.InspectionType,
            (Select [Name] from [SYS_Parameters] where ParameterID = D.Type) as Type,
            (Select [Name] from [SYS_Parameters] where ParameterID = D.Unit) as Unit,
            (Select [Name] from [SYS_Parameters] where ParameterID = D.Status) as Status,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [QCS_StaInsSpeSetting] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]      
            left join [SYS_Items] D on A.[PartID] = D.[ItemID]      
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and D.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }
            count = UniversalService.getCount(sql, Parcount);

            string orderby = " D.[Status],D.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 标准检验规范设定-检验群码页签列表
        /// SAM 2017年6月16日15:37:46
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00004GetGroupList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.StaInsSpeSettingID,A.PartID,
            D.Code,D.Name,D.Comments,A.InspectionType,        
            (Select [Name] from [SYS_Parameters] where ParameterID = D.Status) as Status,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [QCS_StaInsSpeSetting] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]      
            left join [SYS_Parameters] D on A.[PartID] = D.[ParameterID]      
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and D.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }
            count = UniversalService.getCount(sql, Parcount);

            string orderby = " D.[Status],D.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取标准检验规范设定-表头列表
        /// SAM 2017年7月6日09:52:52
        /// </summary>
        /// <param name="staInsSpeSettingID"></param>
        /// <param name="inspectionType"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00004GetHeaderList(string ItemID, string GroupID, string InspectionType, string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select DISTINCT A.ItemProcessID,D.ItemOperationID,A.ItemID,A.ProcessID,D.OperationID,
             '{0}' as InspectionType,'{1}' as GroupID,
            (Select [Code] from [SYS_Parameters] where A.[ProcessID] = [ParameterID]) as ProcessCode,
            (Select [Name] from [SYS_Parameters] where A.[ProcessID] = [ParameterID]) as ProcessName,
            (CASE WHEN (Select [IsDefault] from [SYS_Parameters] where A.[ProcessID] = [ParameterID]) =1  THEN 
            (Select [Code] from [SYS_Parameters] where D.[OperationID] = [ParameterID])  ELSE '' END) as OperationCode,
            (CASE WHEN (Select [IsDefault] from [SYS_Parameters] where A.[ProcessID] = [ParameterID]) =1  THEN 
            (Select [Name] from [SYS_Parameters] where D.[OperationID] = [ParameterID])  ELSE '' END) as OperationName,
            (CASE WHEN (Select [IsDefault] from [SYS_Parameters] where A.[ProcessID] = [ParameterID]) =1  THEN 
            A.Comments ELSE D.Comments END) as Comments,
            (CASE WHEN D.ItemOperationID is null  THEN A.IsIP ELSE D.IsIP END) as IsIP,
            (CASE WHEN D.ItemOperationID is null  THEN A.IsFPI ELSE D.IsFPI END) as IsFPI,
            (CASE WHEN D.ItemOperationID is null  THEN A.IsOSI ELSE D.IsOSI END) as IsOSI,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else  C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", InspectionType, GroupID);

            string sql = string.Format(@"  from [SFC_ItemProcess] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]      
            left join [SFC_ItemOperation] D on A.[ItemProcessID] = D.[ItemProcessID]      
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001'", Framework.SystemID, GroupID);

            if (!string.IsNullOrWhiteSpace(ItemID))
                sql += string.Format(@"  and A.[ItemID] = '{0}' ", ItemID);
            else if (!string.IsNullOrWhiteSpace(GroupID))
                sql += string.Format(@"  and A.[ItemID] in (Select [ItemID] from [QCS_GroupItem] where [GroupID] ='{0}' and [Status] = '{1}0201213000001') ", GroupID, Framework.SystemID);
            else
                sql += string.Format(@"  and 0=1 ");

            count = UniversalService.getCount(sql, null);

            string orderby = " A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// QCS弹窗明细列表
        /// SAM 2017年7月6日11:15:36
        /// </summary>
        /// <param name="SettingType"></param>
        /// <param name="PartID"></param>
        /// <param name="InspectionType"></param>
        /// <param name="ProcessID"></param>
        /// <param name="OperationID"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00004GetDetailsList(string SettingType, string PartID, string InspectionType, string ProcessID, string OperationID, string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.StaInsSpeSettingID,A.PartID,A.SettingType,A.InspectionType,A.ProcessID,A.OperationID,
            A.Sequence,A.CategoryID,A.InspectionMethod,A.InspectionDay,A.InspectionStandard,A.InspectionProjectID,A.Attribute,A.Status,
            D.Name as CategoryName,A.Comments,A.AQL,E.InspectionLevel,
            E.Code as InspectionProjectCode,E.Name as InspectionProjectName,E.Disadvantages,
            (Select [Name] from [SYS_Parameters] where [ParameterID] = E.[InspectionLevel]) as InspectionLevelName,
            (Select [Name] from [SYS_Parameters] where [ParameterID] = E.[Disadvantages]) as DisadvantagesName,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@"  from [QCS_StaInsSpeSetting] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]      
            left join [SYS_Parameters] D on A.[CategoryID] = D.[ParameterID]   
            left join [QCS_InspectionProject] E on A.[InspectionProjectID] = E.[InspectionProjectID]    
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' 
            and A.[SettingType]='{1}' and A.[PartID]='{2}' and A.[InspectionType]='{3}' and A.[ProcessID]='{4}'
            ", Framework.SystemID, SettingType, PartID, InspectionType, ProcessID);

            if (!string.IsNullOrWhiteSpace(OperationID))
                sql += string.Format(@" and A.[OperationID]='{0}' ", OperationID);


            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and D.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }
            count = UniversalService.getCount(sql, Parcount);

            string orderby = " D.[Code],A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据料品流水号获取检验标准
        /// Joint 2017年8月7日15:18:34
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static QCS_StaInsSpeSetting GetByItemID(string ItemID, string InspectionMethod)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_StaInsSpeSetting]
            where [SystemID]='{0}' and [Status] <> '{0}0201213000003' and [PartID]='{1}'", Framework.SystemID, ItemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据料品流水号与检验种类获取列表
        /// Mouse
        /// 2017年8月11日19:11:25
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static List<QCS_StaInsSpeSetting> GetListByItemID(string ItemID, string InspectionMethod, string ProcessID, string OperationID)
        {
            string sql = string.Format(
                @"select * from [QCS_StaInsSpeSetting] A 
                where A.[PartID] = '{0}'and A.[InspectionType]='{1}' and [SystemID]='{2}' and [Status]='{2}0201213000001' and [ProcessID]='{3}'", ItemID, InspectionMethod, Framework.SystemID, ProcessID);

            if (!string.IsNullOrWhiteSpace(OperationID))
            {
                sql = sql + string.Format(" and [OperationID]='{0}'", OperationID);
            }
            else
            {
                sql = sql + string.Format(" and [OperationID] is null ");
            }


            string orderby = " order by [Sequence]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }

        //删除检验标准规范设定制品与制程对应明细
        public static bool DeleteByProcesssAndItem(string userid, string ProcessID, string ItemID)
        {
            try
            {
                string sql = string.Format(@"update [QCS_StaInsSpeSetting] set '{0}',[Status]='{1}0201213000003' where [ProcessID]='{2}' and [ItemID]='{3}' ", UniversalService.getUpdateUTC(userid), Framework.SystemID, ProcessID, ItemID);

                return SQLHelper.ExecuteNonQuery(sql) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 判断指定检验项目是否被使用中
        /// SAM 2017年10月9日14:26:14
        /// </summary>
        /// <param name="InspectionProjectID"></param>
        /// <returns></returns>
        public static bool CheckQcs00002Project(string InspectionProjectID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_StaInsSpeSetting] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            /*先定义Name和Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@InspectionProjectID",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(InspectionProjectID))
            {
                sql = sql + String.Format(@" and [InspectionProjectID] =@InspectionProjectID ");
                parameters[0].Value = InspectionProjectID;
            }

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断检验类别是否被Q4使用
        /// Sam 2017年10月17日16:24:04
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public static bool CheckQcs00002Type(string CategoryID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_StaInsSpeSetting] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            /*先定义Name和Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@CategoryID",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(CategoryID))
            {
                sql = sql + String.Format(@" and [CategoryID] =@CategoryID ");
                parameters[0].Value = CategoryID;
            }

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 检验群码专属表头列表
        /// SAM 2017年10月19日13:42:16
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="InspectionType"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00004GroupGetHeaderList(string GroupID, string InspectionType, int page, int rows, ref int count)
        {
            string select = string.Format(@"select DISTINCT A.ProcessID,D.OperationID,'{0}' as InspectionType,'{1}' as GroupID,
            (Select [Code] from [SYS_Parameters] where A.[ProcessID] = [ParameterID]) as ProcessCode,
            (Select [Name] from [SYS_Parameters] where A.[ProcessID] = [ParameterID]) as ProcessName,
            (CASE WHEN (Select [IsDefault] from [SYS_Parameters] where A.[ProcessID] = [ParameterID]) =1  THEN 
            (Select [Code] from [SYS_Parameters] where D.[OperationID] = [ParameterID])  ELSE '' END) as OperationCode,
            (CASE WHEN (Select [IsDefault] from [SYS_Parameters] where A.[ProcessID] = [ParameterID]) =1  THEN 
            (Select [Name] from [SYS_Parameters] where D.[OperationID] = [ParameterID])  ELSE '' END) as OperationName,
            (CASE WHEN (Select [IsDefault] from [SYS_Parameters] where A.[ProcessID] = [ParameterID]) =1  THEN 
            A.Comments ELSE D.Comments END) as Comments ", InspectionType, GroupID);

            string sql = string.Format(@"  from [SFC_ItemProcess] A 
            left join [SFC_ItemOperation] D on A.[ItemProcessID] = D.[ItemProcessID]      
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001'", Framework.SystemID);

            sql += string.Format(@"  and A.[ItemID] in (Select [ItemID] from [SYS_Items] where [GroupID] ='{0}' and [Status] = '{1}0201213000001') ", GroupID, Framework.SystemID);

            DataTable CountList = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            count = CountList.Rows.Count;

            string orderby = " [ProcessCode],[OperationCode] ";

            DataTable dt = UniversalService.GetTableDistinct(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取最早创建的明细
        /// SAM 2017年10月19日14:41:48
        /// </summary>
        /// <param name="PartID"></param>
        /// <param name="InspectionType"></param>
        /// <param name="ProcessID"></param>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        public static Hashtable Qcs00004GetCreate(string PartID, string InspectionType, string ProcessID, string OperationID)
        {
            string select = string.Format(@"select A.StaInsSpeSettingID,A.PartID,A.SettingType,A.InspectionType,A.ProcessID,A.OperationID,
            A.Sequence,A.CategoryID,A.InspectionMethod,A.InspectionDay,A.InspectionStandard,A.InspectionProjectID,A.Attribute,A.Status,
            A.Comments,A.AQL,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@"  from [QCS_StaInsSpeSetting] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]      
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' 
            and  A.[PartID]='{1}' and A.[InspectionType]='{2}' and A.[ProcessID]='{3}'
            ", Framework.SystemID, PartID, InspectionType, ProcessID);

            if (!string.IsNullOrWhiteSpace(OperationID))
                sql += string.Format(@" and A.[OperationID]='{0}' ", OperationID);


            string orderby = "order by A.CreateTime ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 获取最近更新的明细
        /// SAM 2017年10月19日14:42:06
        /// </summary>
        /// <param name="PartID"></param>
        /// <param name="InspectionType"></param>
        /// <param name="ProcessID"></param>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        public static Hashtable Qcs00004GetModified(string PartID, string InspectionType, string ProcessID, string OperationID)
        {
            string select = string.Format(@"select A.StaInsSpeSettingID,A.PartID,A.SettingType,A.InspectionType,A.ProcessID,A.OperationID,
            A.Sequence,A.CategoryID,A.InspectionMethod,A.InspectionDay,A.InspectionStandard,A.InspectionProjectID,A.Attribute,A.Status,
            A.Comments,A.AQL,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@"  from [QCS_StaInsSpeSetting] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]      
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' 
            and  A.[PartID]='{1}' and A.[InspectionType]='{2}' and A.[ProcessID]='{3}'
            ", Framework.SystemID, PartID, InspectionType, ProcessID);

            if (!string.IsNullOrWhiteSpace(OperationID))
                sql += string.Format(@" and A.[OperationID]='{0}' ", OperationID);

            string orderby = "order by A.CreateTime ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 判断唯一性
        /// Sam 2017年10月20日15:25:36
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Check(QCS_StaInsSpeSetting model)
        {
            string sql = String.Format(@"select Top 1 * from [QCS_StaInsSpeSetting] where [SystemID]='{0}' and [Status] <> '{0}0201213000003'", Framework.SystemID);

            sql += string.Format(@"and [SettingType]='{0}' and [PartID]='{1}'
                and [InspectionType]='{2}' and [ProcessID]='{3}'
                and [CategoryID]='{4}' and [InspectionMethod]='{5}'
                and [InspectionProjectID]='{6}' and [Attribute]='{7}' ",
                model.SettingType, model.PartID, model.InspectionType,
                model.ProcessID, model.CategoryID, model.InspectionMethod,
                model.InspectionProjectID, model.Attribute);

            if (!string.IsNullOrWhiteSpace(model.OperationID))
                sql = sql + String.Format(@" and [OperationID] = '{0}' ", model.OperationID);

            /*StaInsSpeSettingID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(model.StaInsSpeSettingID))
                sql = sql + String.Format(@" and [StaInsSpeSettingID] <> '{0}' ", model.StaInsSpeSettingID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// 根据檢驗設定型態+料號/檢驗群組+制程获取明细
        /// SAM 2017年10月23日12:41:58
        /// </summary>
        /// <param name="PartID"></param>
        /// <param name="InspectionMethod"></param>
        /// <param name="SettingType"></param>
        /// <param name="ProcessID"></param>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        public static List<QCS_StaInsSpeSetting> GetListByPartID(string PartID, string InspectionMethod, string SettingType, string ProcessID, string OperationID)
        {
            string sql = string.Format(
                @"select * from [QCS_StaInsSpeSetting]
                where [PartID] = '{0}'and [InspectionType]='{1}'  and [SettingType] ='{4}'
                and [SystemID]='{2}' and [Status]='{2}0201213000001' and [ProcessID]='{3}'", PartID, InspectionMethod, Framework.SystemID, ProcessID, SettingType);

            if (!string.IsNullOrWhiteSpace(OperationID))
                sql += string.Format(" and [OperationID]='{0}'", OperationID);
            else
                sql += string.Format(" and [OperationID] is null ");

            string orderby = " order by [Sequence]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToList(dt);
        }
    }
}

