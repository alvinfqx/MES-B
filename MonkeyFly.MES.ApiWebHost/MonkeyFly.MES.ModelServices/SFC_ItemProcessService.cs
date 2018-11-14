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
    public class SFC_ItemProcessService : SuperModel<SFC_ItemProcess>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年6月20日14:12:15
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SFC_ItemProcess Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_ItemProcess]([ItemProcessID],[ItemID],
                [Sequence],[ProcessID],[WorkCenterID],[AuxUnit],[AuxUnitRatio],[Price],
                [ResourceReport],[StandardTime],[PrepareTime],[IsIP],[IsFPI],
                [IsOSI],[InspectionGroupID],[Status],[IfRC],[RoutID],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@ItemProcessID,@ItemID,@Sequence,@ProcessID,@WorkCenterID,
                @AuxUnit,@AuxUnitRatio,@Price,@ResourceReport,@StandardTime,@PrepareTime,
                @IsIP,@IsFPI,@IsOSI,@InspectionGroupID,@Status,@IfRC,@RoutID,
                @Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ItemProcessID",SqlDbType.VarChar),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.VarChar),
                    new SqlParameter("@ProcessID",SqlDbType.VarChar),
                    new SqlParameter("@WorkCenterID",SqlDbType.VarChar),
                    new SqlParameter("@AuxUnit",SqlDbType.VarChar),
                    new SqlParameter("@AuxUnitRatio",SqlDbType.Decimal),
                    new SqlParameter("@Price",SqlDbType.Decimal),
                    new SqlParameter("@ResourceReport",SqlDbType.Bit),
                    new SqlParameter("@StandardTime",SqlDbType.Int),
                    new SqlParameter("@PrepareTime",SqlDbType.Int),
                    new SqlParameter("@IsIP",SqlDbType.Bit),
                    new SqlParameter("@IsFPI",SqlDbType.Bit),
                    new SqlParameter("@IsOSI",SqlDbType.Bit),
                    new SqlParameter("@InspectionGroupID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@IfRC",SqlDbType.Bit),
                    new SqlParameter("@RoutID",SqlDbType.VarChar)
                    };

                parameters[0].Value = (Object)Model.ItemProcessID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ProcessID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.WorkCenterID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.AuxUnit ?? DBNull.Value;
                parameters[6].Value = (Object)Model.AuxUnitRatio ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Price ?? DBNull.Value;
                parameters[8].Value = (Object)Model.ResourceReport ?? DBNull.Value;
                parameters[9].Value = (Object)Model.StandardTime ?? DBNull.Value;
                parameters[10].Value = (Object)Model.PrepareTime ?? DBNull.Value;
                parameters[11].Value = (Object)Model.IsIP ?? DBNull.Value;
                parameters[12].Value = (Object)Model.IsFPI ?? DBNull.Value;
                parameters[13].Value = (Object)Model.IsOSI ?? DBNull.Value;
                parameters[14].Value = (Object)Model.InspectionGroupID ?? DBNull.Value;
                parameters[15].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[16].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[17].Value = (Object)Model.IfRC ?? DBNull.Value;
                parameters[18].Value = (Object)Model.RoutID ?? DBNull.Value;

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
        /// SAM 2017年6月20日14:12:32
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SFC_ItemProcess Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_ItemProcess] set {0},
                [Sequence]=@Sequence,[ProcessID]=@ProcessID,[IfRC]=@IfRC,[RoutID]=@RoutID,
                [WorkCenterID]=@WorkCenterID,[AuxUnit]=@AuxUnit,[AuxUnitRatio]=@AuxUnitRatio,[Price]=@Price,
                [ResourceReport]=@ResourceReport,[StandardTime]=@StandardTime,[PrepareTime]=@PrepareTime,[IsIP]=@IsIP,
                [IsFPI]=@IsFPI,[IsOSI]=@IsOSI,[InspectionGroupID]=@InspectionGroupID,
                [Status]=@Status,[Comments]=@Comments where [ItemProcessID]=@ItemProcessID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ItemProcessID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.VarChar),
                    new SqlParameter("@ProcessID",SqlDbType.VarChar),
                    new SqlParameter("@WorkCenterID",SqlDbType.VarChar),
                    new SqlParameter("@AuxUnit",SqlDbType.VarChar),
                    new SqlParameter("@AuxUnitRatio",SqlDbType.Decimal),
                    new SqlParameter("@Price",SqlDbType.Decimal),
                    new SqlParameter("@ResourceReport",SqlDbType.Bit),
                    new SqlParameter("@StandardTime",SqlDbType.Int),
                    new SqlParameter("@PrepareTime",SqlDbType.Int),
                    new SqlParameter("@IsIP",SqlDbType.Bit),
                    new SqlParameter("@IsFPI",SqlDbType.Bit),
                    new SqlParameter("@IsOSI",SqlDbType.Bit),
                    new SqlParameter("@InspectionGroupID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@IfRC",SqlDbType.Bit),
                    new SqlParameter("@RoutID",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.ItemProcessID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ProcessID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.WorkCenterID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.AuxUnit ?? DBNull.Value;
                parameters[5].Value = (Object)Model.AuxUnitRatio ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Price ?? DBNull.Value;
                parameters[7].Value = (Object)Model.ResourceReport ?? DBNull.Value;
                parameters[8].Value = (Object)Model.StandardTime ?? DBNull.Value;
                parameters[9].Value = (Object)Model.PrepareTime ?? DBNull.Value;
                parameters[10].Value = (Object)Model.IsIP ?? DBNull.Value;
                parameters[11].Value = (Object)Model.IsFPI ?? DBNull.Value;
                parameters[12].Value = (Object)Model.IsOSI ?? DBNull.Value;
                parameters[13].Value = (Object)Model.InspectionGroupID ?? DBNull.Value;
                parameters[14].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[15].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[16].Value = (Object)Model.IfRC ?? DBNull.Value;
                parameters[17].Value = (Object)Model.RoutID ?? DBNull.Value;

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
        /// SAM 2017年6月20日14:12:46
        /// </summary>
        /// <param name="ItemProcessID"></param>
        /// <returns></returns>
        public static SFC_ItemProcess get(string ItemProcessID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_ItemProcess] where [ItemProcessID] = '{0}'  and [SystemID] = '{1}' ", ItemProcessID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据制品流水号和制程流水号获取制品制程
        /// SAM 2017年7月17日22:47:323
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static SFC_ItemProcess getByItemProcess(string ItemID, string ProcessID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_ItemProcess] where [ItemID] = '{0}' and  [ProcessID]='{1}' and [SystemID] = '{2}' and [Status] ='{2}0201213000001' ", ItemID, ProcessID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }



        /// <summary>
        /// 判断重复
        /// SAM 2017年6月20日14:22:45
        /// </summary>
        /// <param name="SensorID"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="ProjectID"></param>
        /// <param name="EquipmentProjectID"></param>
        /// <returns></returns>
        public static bool Check(string ProcessID, string ItemID, string ItemProcessID)
        {
            string sql = String.Format(@"select Top 1 * from [SFC_ItemProcess] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(ProcessID))
                sql = sql + string.Format(@" and [ProcessID]= '{0}' ", ProcessID);

            if (!string.IsNullOrWhiteSpace(ItemID))
                sql = sql + string.Format(@" and [ItemID]= '{0}' ", ItemID);

            /*ItemProcessID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(ItemProcessID))
                sql = sql + string.Format(@" and [ItemProcessID] <> '{0}' ", ItemProcessID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// 制程列表
        /// SAM 2017年6月20日14:18:39
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetDetailList(string ItemID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ItemProcessID,A.Sequence,A.ProcessID,A.WorkCenterID,A.ItemID,
            A.AuxUnit,A.AuxUnitRatio,A.Price,A.StandardTime,A.PrepareTime,A.IsIP,A.IsFPI,A.IsOSI,A.InspectionGroupID,
            A.Status,A.Comments,D.Code as ProcessCode,D.Name as ProcessName,H.Code as ItemCode,H.Name as ItemName,H.Specification,
            A.StandardTime as StandardHour,A.PrepareTime as PrepareHour,
            E.Code as WorkCenterCode,E.Name as WorkCenterName,
            (Select Name from [SYS_Parameters] where ParameterID = E.InoutMark) as InoutMark,
            E.ResourceReport,
            (CASE WHEN E.InoutMark='{0}020121300002F' THEN 'False' ELSE D.IsDefault END) as IsOperation,
            (CASE WHEN E.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = E.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = E.DepartmentID) END) as DeptCode,
            (CASE WHEN E.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = E.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = E.DepartmentID) END) as DeptName,            
            F.Code as AuxUnitCode,D.IsDefault,G.Code as GroupCode,G.Name as GroupName,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@" from [SFC_ItemProcess] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.ProcessID = D.ParameterID
            left join [SYS_WorkCenter] E on A.WorkCenterID = E.WorkCenterID
            left join [SYS_Parameters] F on A.AuxUnit = F.ParameterID
            left join [SYS_Parameters] G on A.InspectionGroupID = G.ParameterID
            left join [SYS_Items] H on A.ItemID = H.ItemID
            where A.[SystemID]='{0}' and A.Status= '{0}0201213000001' and A.[ItemID]='{1}' ", Framework.SystemID, ItemID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 根据制品制程流水号获取相关信息
        /// SAM 2017-6-23 10:31:30
        /// </summary>
        /// <param name="ItemProcessID"></param>
        /// <returns></returns>
        public static Hashtable GetItemProcess(string ItemProcessID)
        {
            string select = string.Format(@"select Top 1 A.ItemProcessID,A.ProcessID,A.WorkCenterID,A.ItemID,
            D.Code as ProcessCode,D.Name as ProcessName,
            H.Code as ItemCode,H.Name as ItemName,H.Specification,        
            E.Code as WorkCenterCode,E.Name as WorkCenterName,
            (Select Name from [SYS_Parameters] where ParameterID = E.InoutMark) as InoutMark,E.ResourceReport,
            (CASE WHEN E.InoutMark='{0}020121300002E' THEN 'False' ELSE D.IsDefault END) as IsOperation ");

            string sql = string.Format(@" from [SFC_ItemProcess] A 
            left join [SYS_Parameters] D on A.ProcessID = D.ParameterID
            left join [SYS_WorkCenter] E on A.WorkCenterID = E.WorkCenterID
            left join [SYS_Items] H on A.ItemID = H.ItemID
            where A.[SystemID]='{0}' and A.Status= '{0}0201213000001' and A.[ItemProcessID]='{1}' ", Framework.SystemID, ItemProcessID);

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToHashtableList(dt)[0];
        }

        ///<summary>
        ///開窗讀取制品制程表专属
        ///Joint 2017年6月27日14:38:20
        ///</summary>
        ///<param name="Token">授权码</param>
        ///<param name="ItemID">料品流水号</param>
        ///<param name="page">页码</param>
        ///<param name="rows">行数</param>
        ///<returns></returns>

        public static IList<Hashtable> Sfc00003GetItemProcessList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select DISTINCT A.Code,A.Name,A.Specification ", Framework.SystemID);
            string sql = string.Format(@"from [SYS_Items] A
            where A.ItemID in (select ItemID from SFC_ItemProcess where Status <> '{0}0201213000003') and A.[SystemID]='{0}' and A.Status= '{0}0201213000001'", Framework.SystemID);
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql += " and A.Code collate Chinese_PRC_CI_AS like @Code";
                parameters.Add(new SqlParameter("@Code", "%" + Code + "%"));

            }
            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);
            string orderby = "A.[Code]";
            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);
            return ToHashtableList(dt);
        }


        /// <summary>
        /// 制品制程关系专属弹窗
        /// SAM 2017年6月27日22:14:06
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetItemProcessList(string ItemID, string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ItemProcessID,A.Sequence,A.ProcessID,A.WorkCenterID,A.ItemID,
            A.AuxUnit,A.AuxUnitRatio,A.Price,A.StandardTime,A.PrepareTime,A.IsIP,A.IsFPI,A.IsOSI,A.InspectionGroupID,
            I.Name as Status,A.Comments,D.Code as ProcessCode,D.Name as ProcessName,H.Code as ItemCode,H.Name as ItemName,H.Specification,
            A.StandardTime as StandardHour,A.PrepareTime as PrepareHour,
            E.Code as WorkCenterCode,E.Name as WorkCenterName,
            (Select Name from [SYS_Parameters] where ParameterID = E.InoutMark) as InoutMark,
            E.ResourceReport,
            (CASE WHEN E.InoutMark='{0}020121300002F' THEN 'False' ELSE D.IsDefault END) as IsOperation,
            (CASE WHEN E.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = E.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = E.DepartmentID) END) as DeptCode,
            (CASE WHEN E.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = E.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = E.DepartmentID) END) as DeptName,            
            F.Code as AuxUnitCode,D.IsDefault,G.Code as GroupCode,G.Name as GroupName,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@" from [SFC_ItemProcess] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.ProcessID = D.ParameterID
            left join [SYS_WorkCenter] E on A.WorkCenterID = E.WorkCenterID
            left join [SYS_Parameters] F on A.AuxUnit = F.ParameterID
            left join [SYS_Parameters] G on A.InspectionGroupID = G.ParameterID
            left join [SYS_Items] H on A.ItemID = H.ItemID
            left join [SYS_Parameters] I on A.Status = I.ParameterID
            where A.[SystemID]='{0}' and A.Status= '{0}0201213000001' and A.[ItemID]='{1}' ", Framework.SystemID, ItemID);

            if (!string.IsNullOrWhiteSpace(Code))
                sql += string.Format(@"  and D.Code like '%{0}%' ", Code);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据料品流水号获取列表
        /// Tom 2017年6月28日15点07分
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static List<SFC_ItemProcess> GetListByItemID(string ItemID)
        {
            string sql = string.Format(
                @"select * from [SFC_ItemProcess] where [ItemID] = '{0}' and [Status] = '{1}0201213000001'", ItemID, Framework.SystemID);

            DataTable dt = MESSQLHelper.getDataTable(sql);
            if (dt == null)
            {
                return new List<SFC_ItemProcess>();
            }

            return ToList(dt);
        }


        /// <summary>
        /// 判断指定制品下，是否存在没有关系的制程
        /// SAM 2017年6月29日14:24:29
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static bool CheckItemProcessRelationShip(string ItemID)
        {
            string sql = string.Format(@"select * from [SFC_ItemProcess] 
            where [ItemID] = '{0}'  and [SystemID] = '{1}' 
            and [ItemProcessID] not in (Select [ItemProcessID] from [SFC_ItemProcessRelationShip] where [ItemID] = '{0}' and [Status]='{1}0201213000001') 
            and [ItemProcessID] not in (Select [PreItemProcessID] from [SFC_ItemProcessRelationShip] where [ItemID] = '{0}' and [Status]='{1}0201213000001') 
            ", ItemID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据制品获取制程列表
        /// SAM 2017年7月27日11:26:46
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetBomList(string ItemID)
        {
            string sql = string.Format(
                @"select ItemProcessID,null as Parenter,null as ItemOperationID,A.Sequence,A.WorkCenterID,
                convert(varchar(20),A.Sequence)+' '+(select [Code] from [SYS_Parameters] where A.[ProcessID] = [ParameterID])+' '+(select [Name] from [SYS_Parameters] where A.[ProcessID] = [ParameterID])+' '+
                (select [Name] from [SYS_WorkCenter] where A.[WorkCenterID] = [WorkCenterID]) as Value
                  from [SFC_ItemProcess] A       
                  where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and A.[ItemID] ='{1}'", Framework.SystemID, ItemID);

            string orderby = " order by A.[Sequence]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// 检查制程是否被使用
        /// Joint 2017年7月27日11:50:54
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static bool CheckProcess(string ProcessID)
        {
            string sql = string.Format(@"select * from [SFC_ItemProcess]
            where [ProcessID]='{1}' and [SystemID]='{0}'", Framework.SystemID, ProcessID);
            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// 检查工作中心是否被使用
        /// Joint 2017年7月28日14:26:06
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static bool CheckWorkCenter(string WorkCenterID)
        {
            string sql = string.Format(@"select * from [SFC_ItemProcess]
            where [WorkCenterID]='{1}' and [SystemID]='{0}'", Framework.SystemID, WorkCenterID);
            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据制程+群码，更新群码下料品对应的制程的检验注记
        /// Sam 2017年10月19日11:05:12
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ProcessID"></param>
        /// <param name="GroupID"></param>
        /// <param name="FieldName"></param>
        /// <param name="IsInspection"></param>
        /// <returns></returns>
        public static bool Qcs04Update(string userId, string ProcessID, string GroupID, string FieldName, bool IsInspection)
        {
            string sql = null;
            try
            {
                sql = String.Format(@"update [SFC_ItemProcess] set {0},[InspectionGroupID]='{4}',
                [{1}]='{2}' where [ProcessID]='{3}' and [ItemID] in (select [ItemID] from [SYS_Items] where [GroupID]='{4}')", UniversalService.getUpdateUTC(userId), FieldName, IsInspection, ProcessID, GroupID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(new Exception(sql));
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 更新指定制品制程的指定注记
        /// SAM 2017年10月19日20:49:30
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="FieldName"></param>
        /// <param name="IsInspection"></param>
        /// <returns></returns>
        public static bool Qcs04ItemUpdate(string userId, string ItemProcessID, string FieldName, bool IsInspection)
        {
            string sql = null;
            try
            {
                sql = String.Format(@"update [SFC_ItemProcess] set {0},[{1}]='{2}' 
            where [ItemProcess]='{3}'", UniversalService.getUpdateUTC(userId), FieldName, IsInspection, ItemProcessID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(new Exception(sql));
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 判断指定检验群码+制程下,制品制程的指定注记是否Y或者N
        /// SAM 2017年10月19日17:25:05
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <param name="GroupID"></param>
        /// <param name="FieldName"></param>
        /// <param name="IsInspection">1:Y  0:N</param>
        /// <returns></returns>
        public static bool Qcs04CheckYorN(string ProcessID, string GroupID, string FieldName, int IsInspection)
        {
            string sql = String.Format(@" select * from [SFC_ItemProcess] where [ProcessID]='{0}' and [{2}]={3}
            and [ItemID] in (select [ItemID] from [SYS_Items] where [GroupID]='{1}')", ProcessID, GroupID, FieldName, IsInspection);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;

            return true;
        }

        /// <summary>
        /// 判断对应检验群码下是否存在对应制程的制品制程
        /// SAM 2017年10月20日15:13:34
        /// false不存在，true存在
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static bool Qcs04GroupCheck(string GroupID, string ProcessID)
        {
            string sql = String.Format(@" select * from [SFC_ItemProcess] where [ProcessID]='{0}' and [Status]='{2}0201213000001' and [SystemID]='{2}'
            and [ItemID] in (select [ItemID] from [SYS_Items] where [GroupID]='{1}' and [Status]='{2}0201213000001'  and [SystemID]='{2}')", ProcessID, GroupID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取所有的有效的制品制程设定列表
        /// SAM 2017年10月6日13:54:59
        /// </summary>
        /// <returns></returns>
        public static IList<SFC_ItemProcess> GetList()
        {
            string sql = string.Format(
                @"select A.ItemProcessID,A.Sequence,A.ProcessID,A.WorkCenterID,A.StandardTime,A.PrepareTime,A.ItemID,
                    (CASE when B.InoutMark ='{0}020121300002E' THEN C.IsDefault ELSE 0 END) as IsEnableOperation
                  from [SFC_ItemProcess] A
                    left join [SYS_WorkCenter] B on A.WorkCenterID=B.WorkCenterID
                    left join [SYS_Parameters] C on A.ProcessID=C.ParameterID
                  where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID);

            string orderby = " order by [Sequence]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }
    }
}

