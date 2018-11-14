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
    public class SYS_WorkCenterService : SuperModel<SYS_WorkCenter>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年5月24日17:53:00
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_WorkCenter Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_WorkCenter]([WorkCenterID],[Code],[Name],[CalendarID],[InoutMark],[DepartmentID],[ResourceReport],[IsClass],[DispatchMode],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                (@WorkCenterID,@Code,@Name,@CalendarID,@InoutMark,@DepartmentID,@ResourceReport,@IsClass,@DispatchMode,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@WorkCenterID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.NVarChar),
                    new SqlParameter("@Name",SqlDbType.NVarChar),
                    new SqlParameter("@CalendarID",SqlDbType.VarChar),
                    new SqlParameter("@InoutMark",SqlDbType.VarChar),
                    new SqlParameter("@DepartmentID",SqlDbType.VarChar),
                    new SqlParameter("@ResourceReport",SqlDbType.VarChar),
                    new SqlParameter("@IsClass",SqlDbType.VarChar),
                    new SqlParameter("@DispatchMode",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.WorkCenterID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[3].Value = (Object)Model.CalendarID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.InoutMark ?? DBNull.Value;
                parameters[5].Value = (Object)Model.DepartmentID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.ResourceReport ?? DBNull.Value;
                parameters[7].Value = (Object)Model.IsClass ?? DBNull.Value;
                parameters[8].Value = (Object)Model.DispatchMode ?? DBNull.Value;
                parameters[9].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[10].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAm 2017年5月24日17:53:10
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_WorkCenter Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_WorkCenter] set {0},
            [Name]=@Name,[CalendarID]=@CalendarID,
            [InoutMark]=@InoutMark,[DepartmentID]=@DepartmentID,[ResourceReport]=@ResourceReport,[IsClass]=@IsClass,[DispatchMode]=@DispatchMode,[Status]=@Status,[Comments]=@Comments where [WorkCenterID]=@WorkCenterID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@WorkCenterID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.NVarChar),
                    new SqlParameter("@Name",SqlDbType.NVarChar),
                    new SqlParameter("@CalendarID",SqlDbType.VarChar),
                    new SqlParameter("@InoutMark",SqlDbType.VarChar),
                    new SqlParameter("@DepartmentID",SqlDbType.VarChar),
                    new SqlParameter("@ResourceReport",SqlDbType.VarChar),
                    new SqlParameter("@IsClass",SqlDbType.VarChar),
                    new SqlParameter("@DispatchMode",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.WorkCenterID;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[3].Value = (Object)Model.CalendarID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.InoutMark ?? DBNull.Value;
                parameters[5].Value = (Object)Model.DepartmentID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.ResourceReport ?? DBNull.Value;
                parameters[7].Value = (Object)Model.IsClass ?? DBNull.Value;
                parameters[8].Value = (Object)Model.DispatchMode ?? DBNull.Value;
                parameters[9].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[10].Value = (Object)Model.Comments ?? DBNull.Value;
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
        /// SAm 2017年5月24日17:53:28
        /// </summary>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public static SYS_WorkCenter get(string WorkCenterID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_WorkCenter] where [WorkCenterID] = '{0}'  and [SystemID] = '{1}' ", WorkCenterID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据代号获取单一实体
        /// SAM 2017年9月5日10:10:24
        /// </summary>
        /// <param name="WorkCenterCode"></param>
        /// <returns></returns>
        public static SYS_WorkCenter getByCode(string WorkCenterCode)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_WorkCenter] where [Code] = '{0}'  and [SystemID] = '{1}' and [Status] = '{0}0201213000001' ", WorkCenterCode, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// 判断代号是否重复
        /// SAM 2017年4月27日10:23:52
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static bool CheckCode(string Code, string WorkCenterID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_WorkCenter] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

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

            /*WorkCenterID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(WorkCenterID))
                sql = sql + String.Format(@" and [WorkCenterID] <> '{0}' ", WorkCenterID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }



        /// <summary>
        /// 获取工作中心的列表
        /// SAM 2017年5月24日17:54:15
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00018GetWorkCenterList(string Code, string status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.WorkCenterID,A.Code,A.Name,A.CalendarID,A.InoutMark,A.ResourceReport,A.IsClass as EnableShift,A.DispatchMode,
            A.DepartmentID,A.Status,A.Comments,D.Code as CalendarCode,
            (CASE WHEN A.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = A.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = A.DepartmentID) END) as DeptCode,
            (CASE WHEN A.InoutMark='{0}020121300002E' THEN (Select [Name] from [SYS_Organization] where [OrganizationID] = A.DepartmentID) ELSE (Select [Name] from [SYS_Manufacturers] where [ManufacturerID] = A.DepartmentID) END) as DeptName,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@" from [SYS_WorkCenter] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Calendar] D on A.CalendarID = D.CalendarID
            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003' ", Framework.SystemID);

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
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql = sql + String.Format(@" and A.[Status] = @Status ");
                parameters[1].Value = status;
                Parcount[1].Value = status;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 工作中心的导出
        /// SAM 2017年5月25日11:11:22
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static DataTable Inf00018OWorkCenterExport(string Code, string Status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status],A.[Code]),A.Code,A.Name,E.Name as InoutMark,
            (CASE WHEN A.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = A.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = A.DepartmentID) END) as DeptCode,
            (CASE WHEN A.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = A.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = A.DepartmentID) END) as DeptName,
            D.Code as CalendarCode,
            (CASE WHEN A.IsClass='0' THEN '不啟用' ELSE '啟用' END)  as EnableShift,(CASE WHEN A.ResourceReport='0' THEN 'N' ELSE 'Y' END) as ResourceReport,
            G.Name,F.Name as Status,A.Comments,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@" from [SYS_WorkCenter] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Calendar] D on A.CalendarID = D.CalendarID
            left join [SYS_Parameters] E on A.InoutMark = E.ParameterID
            left join [SYS_Parameters] F on A.Status = F.ParameterID
            left join [SYS_Parameters] G on A.DispatchMode = G.ParameterID
            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003' ", Framework.SystemID);

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
        /// 工作中心的弹窗
        /// SAM 2017年6月20日14:53:59
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetWorkCenterList(string ProcessID, string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.WorkCenterID,A.Code,A.Name,A.InoutMark,
            A.DepartmentID,A.Status,A.Comments,A.ResourceReport,D.Name as StatusName,
            (CASE WHEN A.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = A.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = A.DepartmentID) END) as DeptCode,
            (CASE WHEN A.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = A.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = A.DepartmentID) END) as DeptName,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@" from [SYS_WorkCenter] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.Status = D.ParameterID
            where A.[SystemID]='{0}' and A.Status = '{0}0201213000001' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(ProcessID))
                sql += string.Format(@" and A.[WorkCenterID] in (select [WorkCenterID] from [SYS_WorkCenterProcess] where [ProcessID] = '{0}') ", ProcessID);

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
                Code = "%" + Code.Trim() + "%";
                sql = sql + string.Format(@" and A.[Code] collate Chinese_PRC_CI_AS like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 无效工时原因分析
        /// SAM 2017年7月22日18:56:57
        /// </summary>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00016GetList(string StartWorkCenterCode, string EndWorkCenterCode, string StartDate, string EndDate, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.WorkCenterID,A.Code,A.Name,A.CalendarID,D.Name as InoutMark,A.DepartmentID,A.Status,A.Comments,
            (CASE WHEN A.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = A.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = A.DepartmentID) END) as DeptCode,
            (CASE WHEN A.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = A.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = A.DepartmentID) END) as DeptName,
            convert(varchar(8),dateadd(ss,(Select SUM(UnLaborHour) from [SFC_CompletionOrder] where FabMoProcessID in (select [FabMoProcessID] from [SFC_FabMoProcess] where [WorkCenterID] =A.[WorkCenterID]) and [Status]='{0}02012130000A'),108),108) as UnLaborHour,
            convert(varchar(8),dateadd(ss,(Select SUM(UnMachineHour) from [SFC_CompletionOrder] where FabMoProcessID in (select [FabMoProcessID] from [SFC_FabMoProcess] where [WorkCenterID] =A.[WorkCenterID]) and [Status]='{0}02012130000A'),108),108) as UnMachineHour ", Framework.SystemID);

            string sql = string.Format(@" from [SYS_WorkCenter] A 
            left join [SYS_Parameters] D on A.InoutMark = D.ParameterID
            where A.[SystemID]='{0}' and A.Status = '{0}0201213000001' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartWorkCenterCode))
            {
                sql = sql + String.Format(@" and A.[Code] >= @StartWorkCenterCode ");
                parameters[0].Value = StartWorkCenterCode;
                Parcount[0].Value = StartWorkCenterCode;
            }

            if (!string.IsNullOrWhiteSpace(EndWorkCenterCode))
            {
                sql = sql + String.Format(@" and A.[Code] <= @EndWorkCenterCode ");
                parameters[1].Value = EndWorkCenterCode;
                Parcount[1].Value = EndWorkCenterCode;
            }


            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql += string.Format(@" and A.[WorkCenterID] in (select [WorkCenterID] from [SFC_FabMoProcess] where [FabMoProcessID] in (select [FabMoProcessID] from [SFC_CompletionOrder] where [Date] >= @StartDate and [Status]<> '{0}0201213000003') and [Status]<> '{0}0201213000003') ", Framework.SystemID);
                parameters[2].Value = StartDate;
                Parcount[2].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql += string.Format(@" and A.[WorkCenterID] in (select [WorkCenterID] from [SFC_FabMoProcess] where [FabMoProcessID] in (select [FabMoProcessID] from [SFC_CompletionOrder] where [Date] <= @EndDate and [Status]<> '{0}0201213000003') and [Status]<> '{0}0201213000003') ", Framework.SystemID);
                parameters[3].Value = EndDate;
                Parcount[3].Value = EndDate;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 判断部门是否再工作中心中使用
        /// SAM 2017年7月25日11:15:39
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool CheckOrganization(string OrganizationID)
        {
            string sql = string.Format(@"select * from [SYS_WorkCenter] where [DepartmentID]='{0}' and [SystemID] = '{1}' and [Status] <> '{1}0201213000003' ", OrganizationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据制程获取不属于他的工作中心列表(不分页)
        /// Joint 2017年7月27日16:26:20
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static object Inf00018GetNoWorkCenterProcessList(string ProcessID)
        {
            string select = string.Format(@"select null as WorkCenterProcessID,A.WorkCenterID,
            A.Code as WorkCenterNo,A.Name as WorkCenterDescription,
            A.InoutMark,(select [Name] from [SYS_Parameters] where A.[Status] = [ParameterID]) as Status ");

            string sql = string.Format(@"  from [SYS_WorkCenter] A 
            where [WorkCenterID] not in (select [WorkCenterID] from [SYS_WorkCenterProcess] where [ProcessID] ='{1}' and [Status] = '{0}0201213000001')
            and A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID, ProcessID);

            string orderBy = "order By A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 判断行事历是否在工作中心使用
        /// SAM 2017年8月1日21:52:55
        /// </summary>
        /// <param name="CalendarID"></param>
        /// <returns></returns>
        public static bool inf00014Check(string CalendarID)
        {
            string sql = string.Format(@"select * from [SYS_WorkCenter] where [CalendarID] = '{0}' and [SystemID] = '{1}' and [Status] <> '{1}0201213000003' ", CalendarID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取所有正常的工作中心实体集合
        /// SAM 2017年10月26日14:49:16
        /// </summary>
        /// <returns></returns>
        public static IList<SYS_WorkCenter> GetList()
        {
            string sql = string.Format(@"select * from [SYS_WorkCenter] where [SystemID] = '{0}'  and [Status]='{0}0201213000001' ", Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToList(dt);
        }

        /// <summary>
        /// 获取工作中心列表
        /// SAM 2017年10月20日15:38:10
        /// Code为单个起始区间查询，不包括本身
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> SfcWorkCenterList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.WorkCenterID,A.Code,A.Name,A.InoutMark,E.Name as InoutMarkName,
            A.DepartmentID,A.Status,A.Comments,A.ResourceReport,D.Name as StatusName,
            (CASE WHEN A.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = A.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = A.DepartmentID) END) as DeptCode,
            (CASE WHEN A.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = A.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = A.DepartmentID) END) as DeptName,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@" from [SYS_WorkCenter] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.Status = D.ParameterID
            left join [SYS_Parameters] E on A.InoutMark = E.ParameterID
            where A.[SystemID]='{0}' and A.Status = '{0}0201213000001' ", Framework.SystemID);

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
                sql = sql + string.Format(@" and A.[Code] collate Chinese_PRC_CI_AS > @Code ");
                parameters[0].Value = Code.Trim();
                Parcount[0].Value = Code.Trim();
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }
    }
}

