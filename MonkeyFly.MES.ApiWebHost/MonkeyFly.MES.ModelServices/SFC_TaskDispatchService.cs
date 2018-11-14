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
    public class SFC_TaskDispatchService : SuperModel<SFC_TaskDispatch>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年7月3日11:20:16
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SFC_TaskDispatch Model)
        {
            try
            {
                string sql = string.Format(@"insert [SFC_TaskDispatch]([TaskDispatchID],[TaskNo],[Sequence],
                [FabricatedMotherID],[FabMoProcessID],[FabMoOperationID],
                [MoSequence],[ItemID],[ProcessID],[OperationID],[StartDate],[FinishDate],
                [IsDispatch],[MESUserID],[DispatchDate],[DispatchQuantity],[ClassID],
                [InDateTime],[OutDateTime],[IsIP],[IsFPI],[IsOSI],[InspectionGroupID],[FPIPass],
                [FinishQuantity],[ScrapQuantity],[DiffQuantity],[RepairQuantity],[LaborHour],[UnLaborHour],
                [MachineHour],[UnMachineHour],
                [Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@TaskDispatchID,@TaskNo,
                @Sequence,@FabricatedMotherID,@FabMoProcessID,
                @FabMoOperationID,@MoSequence,@ItemID,
                @ProcessID,@OperationID,@StartDate,
                @FinishDate,@IsDispatch,@MESUserID,
                @DispatchDate,@DispatchQuantity,@ClassID,
                @InDateTime,@OutDateTime,@IsIP,
                @IsFPI,@IsOSI,@InspectionGroupID,
                @FPIPass,@FinishQuantity,@ScrapQuantity,
                @DiffQuantity,@RepairQuantity,@LaborHour,
                @UnLaborHour,@MachineHour,@UnMachineHour,
                @Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TaskDispatchID",SqlDbType.VarChar),
                    new SqlParameter("@TaskNo",SqlDbType.NVarChar),
                    new SqlParameter("@Sequence",SqlDbType.VarChar),
                    new SqlParameter("@FabricatedMotherID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@MoSequence",SqlDbType.Int),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@ProcessID",SqlDbType.VarChar),
                    new SqlParameter("@OperationID",SqlDbType.VarChar),
                    new SqlParameter("@StartDate",SqlDbType.DateTime),
                    new SqlParameter("@FinishDate",SqlDbType.DateTime),
                    new SqlParameter("@IsDispatch",SqlDbType.Bit),
                    new SqlParameter("@MESUserID",SqlDbType.VarChar),
                    new SqlParameter("@DispatchDate",SqlDbType.DateTime),
                    new SqlParameter("@DispatchQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ClassID",SqlDbType.VarChar),
                    new SqlParameter("@InDateTime",SqlDbType.DateTime),
                    new SqlParameter("@OutDateTime",SqlDbType.DateTime),
                    new SqlParameter("@IsIP",SqlDbType.Bit),
                    new SqlParameter("@IsFPI",SqlDbType.Bit),
                    new SqlParameter("@IsOSI",SqlDbType.Bit),
                    new SqlParameter("@InspectionGroupID",SqlDbType.VarChar),
                    new SqlParameter("@FPIPass",SqlDbType.Bit),
                    new SqlParameter("@FinishQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ScrapQuantity",SqlDbType.Decimal),
                    new SqlParameter("@DiffQuantity",SqlDbType.Decimal),
                    new SqlParameter("@RepairQuantity",SqlDbType.Decimal),
                    new SqlParameter("@LaborHour",SqlDbType.Decimal),
                    new SqlParameter("@UnLaborHour",SqlDbType.Decimal),
                    new SqlParameter("@MachineHour",SqlDbType.Decimal),
                    new SqlParameter("@UnMachineHour",SqlDbType.Decimal),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.TaskDispatchID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.TaskNo ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.FabricatedMotherID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.FabMoProcessID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.FabMoOperationID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.MoSequence ?? DBNull.Value;
                parameters[7].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[8].Value = (Object)Model.ProcessID ?? DBNull.Value;
                parameters[9].Value = (Object)Model.OperationID ?? DBNull.Value;
                parameters[10].Value = (Object)Model.StartDate ?? DBNull.Value;
                parameters[11].Value = (Object)Model.FinishDate ?? DBNull.Value;
                parameters[12].Value = (Object)Model.IsDispatch ?? DBNull.Value;
                parameters[13].Value = (Object)Model.MESUserID ?? DBNull.Value;
                parameters[14].Value = (Object)Model.DispatchDate ?? DBNull.Value;
                parameters[15].Value = (Object)Model.DispatchQuantity ?? DBNull.Value;
                parameters[16].Value = (Object)Model.ClassID ?? DBNull.Value;
                parameters[17].Value = (Object)Model.InDateTime ?? DBNull.Value;
                parameters[18].Value = (Object)Model.OutDateTime ?? DBNull.Value;
                parameters[19].Value = (Object)Model.IsIP ?? DBNull.Value;
                parameters[20].Value = (Object)Model.IsFPI ?? DBNull.Value;
                parameters[21].Value = (Object)Model.IsOSI ?? DBNull.Value;
                parameters[22].Value = (Object)Model.InspectionGroupID ?? DBNull.Value;
                parameters[23].Value = (Object)Model.FPIPass ?? DBNull.Value;
                parameters[24].Value = (Object)Model.FinishQuantity ?? DBNull.Value;
                parameters[25].Value = (Object)Model.ScrapQuantity ?? DBNull.Value;
                parameters[26].Value = (Object)Model.DiffQuantity ?? DBNull.Value;
                parameters[27].Value = (Object)Model.RepairQuantity ?? DBNull.Value;
                parameters[28].Value = (Object)Model.LaborHour ?? DBNull.Value;
                parameters[29].Value = (Object)Model.UnLaborHour ?? DBNull.Value;
                parameters[30].Value = (Object)Model.MachineHour ?? DBNull.Value;
                parameters[31].Value = (Object)Model.UnMachineHour ?? DBNull.Value;
                parameters[32].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[33].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017年7月3日11:20:51
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SFC_TaskDispatch Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_TaskDispatch] set {0},
                [FabMoProcessID]=@FabMoProcessID,[FabMoOperationID]=@FabMoOperationID,[MoSequence]=@MoSequence,[ItemID]=@ItemID,
                [ProcessID]=@ProcessID,[OperationID]=@OperationID,[StartDate]=@StartDate,[FinishDate]=@FinishDate,
                [IsDispatch]=@IsDispatch,[MESUserID]=@MESUserID,[DispatchDate]=@DispatchDate,[DispatchQuantity]=@DispatchQuantity,
                [ClassID]=@ClassID,[InDateTime]=@InDateTime,[OutDateTime]=@OutDateTime,[IsIP]=@IsIP,
                [IsFPI]=@IsFPI,[IsOSI]=@IsOSI,[InspectionGroupID]=@InspectionGroupID,[FPIPass]=@FPIPass,
                [FinishQuantity]=@FinishQuantity,[ScrapQuantity]=@ScrapQuantity,[DiffQuantity]=@DiffQuantity,[RepairQuantity]=@RepairQuantity,
                [LaborHour]=@LaborHour,[UnLaborHour]=@UnLaborHour,[MachineHour]=@MachineHour,[UnMachineHour]=@UnMachineHour,
                [Status]=@Status,[Comments]=@Comments 
                where [TaskDispatchID]=@TaskDispatchID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TaskDispatchID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@MoSequence",SqlDbType.Int),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@ProcessID",SqlDbType.VarChar),
                    new SqlParameter("@OperationID",SqlDbType.VarChar),
                    new SqlParameter("@StartDate",SqlDbType.DateTime),
                    new SqlParameter("@FinishDate",SqlDbType.DateTime),
                    new SqlParameter("@IsDispatch",SqlDbType.Bit),
                    new SqlParameter("@MESUserID",SqlDbType.VarChar),
                    new SqlParameter("@DispatchDate",SqlDbType.DateTime),
                    new SqlParameter("@DispatchQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ClassID",SqlDbType.VarChar),
                    new SqlParameter("@InDateTime",SqlDbType.DateTime),
                    new SqlParameter("@OutDateTime",SqlDbType.DateTime),
                    new SqlParameter("@IsIP",SqlDbType.Bit),
                    new SqlParameter("@IsFPI",SqlDbType.Bit),
                    new SqlParameter("@IsOSI",SqlDbType.Bit),
                    new SqlParameter("@InspectionGroupID",SqlDbType.VarChar),
                    new SqlParameter("@FPIPass",SqlDbType.Bit),
                    new SqlParameter("@FinishQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ScrapQuantity",SqlDbType.Decimal),
                    new SqlParameter("@DiffQuantity",SqlDbType.Decimal),
                    new SqlParameter("@RepairQuantity",SqlDbType.Decimal),
                    new SqlParameter("@LaborHour",SqlDbType.Decimal),
                    new SqlParameter("@UnLaborHour",SqlDbType.Decimal),
                    new SqlParameter("@MachineHour",SqlDbType.Decimal),
                    new SqlParameter("@UnMachineHour",SqlDbType.Decimal),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.TaskDispatchID;
                parameters[1].Value = (Object)Model.FabMoProcessID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.FabMoOperationID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.MoSequence ?? DBNull.Value;
                parameters[4].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ProcessID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.OperationID ?? DBNull.Value;
                parameters[7].Value = (Object)Model.StartDate ?? DBNull.Value;
                parameters[8].Value = (Object)Model.FinishDate ?? DBNull.Value;
                parameters[9].Value = (Object)Model.IsDispatch ?? DBNull.Value;
                parameters[10].Value = (Object)Model.MESUserID ?? DBNull.Value;
                parameters[11].Value = (Object)Model.DispatchDate ?? DBNull.Value;
                parameters[12].Value = (Object)Model.DispatchQuantity ?? DBNull.Value;
                parameters[13].Value = (Object)Model.ClassID ?? DBNull.Value;
                parameters[14].Value = (Object)Model.InDateTime ?? DBNull.Value;
                parameters[15].Value = (Object)Model.OutDateTime ?? DBNull.Value;
                parameters[16].Value = (Object)Model.IsIP ?? DBNull.Value;
                parameters[17].Value = (Object)Model.IsFPI ?? DBNull.Value;
                parameters[18].Value = (Object)Model.IsOSI ?? DBNull.Value;
                parameters[19].Value = (Object)Model.InspectionGroupID ?? DBNull.Value;
                parameters[20].Value = (Object)Model.FPIPass ?? DBNull.Value;
                parameters[21].Value = (Object)Model.FinishQuantity ?? DBNull.Value;
                parameters[22].Value = (Object)Model.ScrapQuantity ?? DBNull.Value;
                parameters[23].Value = (Object)Model.DiffQuantity ?? DBNull.Value;
                parameters[24].Value = (Object)Model.RepairQuantity ?? DBNull.Value;
                parameters[25].Value = (Object)Model.LaborHour ?? DBNull.Value;
                parameters[26].Value = (Object)Model.UnLaborHour ?? DBNull.Value;
                parameters[27].Value = (Object)Model.MachineHour ?? DBNull.Value;
                parameters[28].Value = (Object)Model.UnMachineHour ?? DBNull.Value;
                parameters[29].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[30].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 更新（状态，进站人，进站时间，出站人，出站时间）
        /// SAM 2017年10月26日21:43:56
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool UpdateV1(string userId, SFC_TaskDispatch Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_TaskDispatch] set {0},
                [InMESUserID]=@InMESUserID,[OutMESUserID]=@OutMESUserID,
                [InDateTime]=@InDateTime,[OutDateTime]=@OutDateTime,[Status]=@Status
                where [TaskDispatchID]=@TaskDispatchID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TaskDispatchID",SqlDbType.VarChar),
                    new SqlParameter("@InMESUserID",SqlDbType.VarChar),
                    new SqlParameter("@OutMESUserID",SqlDbType.VarChar),
                    new SqlParameter("@InDateTime",SqlDbType.DateTime),
                    new SqlParameter("@OutDateTime",SqlDbType.DateTime),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.TaskDispatchID;
                parameters[1].Value = (Object)Model.InMESUserID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.OutMESUserID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.InDateTime ?? DBNull.Value;
                parameters[4].Value = (Object)Model.OutDateTime ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Status ?? DBNull.Value;

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
        /// SAM 2017年7月3日11:21:05
        /// </summary>
        /// <param name="TaskDispatchID"></param>
        /// <returns></returns>
        public static SFC_TaskDispatch get(string TaskDispatchID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_TaskDispatch] where [TaskDispatchID] = '{0}'  and [SystemID] = '{1}' ", TaskDispatchID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据任务卡号获取实体
        /// SAM 2017年8月1日15:22:16
        /// </summary>
        /// <param name="TaskNo"></param>
        /// <returns></returns>
        public static SFC_TaskDispatch getByCode(string TaskNo)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_TaskDispatch] where [TaskNo] = '{0}'  and [SystemID] = '{1}' ", TaskNo, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 任务单分派列表(制令制程工序)
        /// SAM 2017年6月29日17:44:28
        /// </summary>
        /// <param name="FabMoOperationID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00004GetODispatchList(string FabMoOperationID, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.TaskDispatchID,A.TaskNo,A.Sequence,
                A.FabricatedMotherID,A.FabMoProcessID,A.FabMoOperationID,A.MoSequence,A.ItemID,A.ProcessID,A.OperationID,
                A.StartDate,A.FinishDate,A.IsDispatch,A.DispatchQuantity,A.ClassID,A.Comments,A.Status,
                D.Code as ClassCode,D.Name as ClassName,A.FinishQuantity,
                (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN B.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_TaskDispatch] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                 left join [SYS_Class] D on A.[ClassID] = D.[ClassID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and A.[FabMoOperationID]='{1}'", Framework.SystemID, FabMoOperationID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Status],A.[TaskNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 任务单分派列表(制令制程)
        /// SAM 2017年6月29日17:57:25
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00004GetDispatchList(string FabMoProcessID, int page, int rows, ref int count)
        {
            string select = string.Format(
              @"select A.TaskDispatchID,A.TaskNo,A.Sequence,
                A.FabricatedMotherID,A.FabMoProcessID,A.FabMoOperationID,A.MoSequence,A.ItemID,A.ProcessID,A.OperationID,
                A.StartDate,A.FinishDate,A.IsDispatch,A.DispatchQuantity,A.ClassID,A.Comments,A.Status,
                D.Code as ClassCode,D.Name as ClassName,A.FinishQuantity,
                (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN B.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_TaskDispatch] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                 left join [SYS_Class] D on A.[ClassID] = D.[ClassID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and A.[FabMoProcessID]='{1}'", Framework.SystemID, FabMoProcessID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Status],A.[TaskNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据任务单号获取他的信息
        /// SAM 2017年7月6日16:30:55
        /// </summary>
        /// <param name="taskDispatchID"></param>
        /// <returns></returns>
        public static Hashtable Sfc00004GetTask(string TaskDispatchID)
        {
            string sql = string.Format(@"select Top 1 A.TaskNo,A.StartDate,A.FinishDate,A.FabricatedMotherID,
            A.FabMoProcessID,A.FabMoOperationID,
            B.MoNo,A.DispatchQuantity,B.OrderNo,A.Comments,A.TaskDispatchID,
            (Select [Code] from [SYS_Items] where [ItemID] = B.[ItemID]) as ItemCode, 
            (Select [Name] from [SYS_Items] where [ItemID] = B.[ItemID]) as ItemName, 
            (Select [Code] from [SYS_WorkCenter] where [WorkCenterID] = C.[WorkCenterID]) as WorkCenterCode, 
            (Select [Name] from [SYS_WorkCenter] where [WorkCenterID] = C.[WorkCenterID]) as WorkCenterName, 
            (Select [Code] from [SYS_Parameters] where [ParameterID] = C.[ProcessID]) as ProcessCode, 
            (Select [Name] from [SYS_Parameters] where [ParameterID] = C.[ProcessID]) as ProcessName, 
            (Select [Code] from [SYS_Parameters] where [ParameterID] = D.[OperationID]) as OPerationCode, 
            (Select [Name] from [SYS_Parameters] where [ParameterID] = D.[OperationID]) as OPerationName, 
            (Select [Name] from [SYS_Customers] where [CustomerID] = B.[CustomerID]) as CustomerName
            from [SFC_TaskDispatch] A
            left join [SFC_FabricatedMother] B on A.[FabricatedMotherID] = B.[FabricatedMotherID]
            left join [SFC_FabMoProcess] C on A.[FabMoProcessID] =C.[FabMoProcessID]
            left join [SFC_FabMoOperation] D on A.[FabMoOperationID] =D.[FabMoOperationID]
            where A.[TaskDispatchID] = '{0}' and A.[SystemID] = '{1}' ", TaskDispatchID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 任务单的开窗
        /// SAM 2017年9月6日17:15:40
        /// </summary>
        /// <param name="TaskNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> TaskDispatchList(string TaskNo, int page, int rows, ref int count)
        {
            string select = string.Format(
              @"select A.TaskDispatchID,A.TaskNo,A.ItemID,
                D.Code as ItemCode,D.Name+'/'+D.Specification as DescriptionSpec,
                E.Code as ProcessCode,E.Name as ProcessName,E.Description as ProcessDescription,
                F.Code as OperationCode,F.Name as OperationDescName,F.Description as OperationDescription,
                H.MoNo+'-'+cast (H.SplitSequence as varchar) as MoCode,
                (select Name from SYS_Parameters where ParameterID=H.UnitID) as UnitDescription,
                B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
               (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(
                @"from [SFC_TaskDispatch] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                 left join [SYS_Items] D on A.[ItemID] = D.[ItemID]
                 left join [SYS_Parameters] E on A.[ProcessID] = E.[ParameterID]
                 left join [SYS_Parameters] F on A.[OperationID] = F.[ParameterID]
                 left join [SFC_FabricatedMother] H on A.[FabricatedMotherID]=H.[FabricatedMotherID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@TaskNo",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@TaskNo",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(TaskNo))
            {
                TaskNo = "%" + TaskNo.Trim() + "%";
                sql = sql + string.Format(@" and A.[TaskNo] collate Chinese_PRC_CI_AS like @TaskNo ");
                parameters[0].Value = TaskNo;
                Parcount[0].Value = TaskNo;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[TaskNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }



        /// <summary>
        /// 任务单的开窗
        /// SAM 2017年7月9日21:06:13
        /// Mouse 2017年9月4日17:53:16 修改
        /// </summary>
        /// <param name="TaskNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00007TaskDispatchList(string TaskNo, int page, int rows, ref int count)
        {
            //string select = string.Format(
            //  @"select A.TaskDispatchID,A.TaskNo,A.ItemID,
            //    D.Code as ItemCode,D.Name+'/'+D.Specification as DescriptionSpec,
            //    E.Code as ProcessCode,E.Name as ProcessName,E.Description as ProcessDescription,
            //    F.Code as OperationCode,F.Name as OperationDescName,F.Description as OperationDescription,
            //    H.MoNo+'-'+H.SplitSequence as MoCode,
            //    (select Name from SYS_Parameters where ParameterID=H.UnitID) as UnitDescription,
            //    B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            //    (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            //   (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            //string sql = string.Format(
            //    @"from [SFC_TaskDispatch] A               
            //     left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            //     left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
            //     left join [SYS_Items] D on A.[ItemID] = D.[ItemID]
            //     left join [SYS_Parameters] E on A.[ProcessID] = E.[ParameterID]
            //     left join [SFC_FabricatedMother] H on A.[FabricatedMotherID]=H.[FabricatedMotherID]
            //    where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

            string select = string.Format(@"select A.TaskDispatchID,A.TaskNo,A.ItemID,
                D.Code as ItemCode,D.Name+'/'+D.Specification as DescriptionSpec,
                E.Code as ProcessCode,E.Name as ProcessName,E.Description as ProcessDescription,
                F.Code as OperationCode,F.Name as OperationDescName,F.Description as OperationDescription,
                A.DispatchQuantity as DocumentQuantity,
                H.MoNo+'-'+cast (H.SplitSequence as varchar) as MoCode,
                (select Name from SYS_Parameters where ParameterID=H.UnitID) as UnitDescription,
                B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
               (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_TaskDispatch] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                 left join [SYS_Items] D on A.[ItemID] = D.[ItemID]
                 left join [SYS_Parameters] E on A.[ProcessID] = E.[ParameterID]
                 left join [SYS_Parameters] F on A.[OperationID] = F.[ParameterID]
                 left join [SFC_FabricatedMother] H on A.[FabricatedMotherID]=H.[FabricatedMotherID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000089' and A.[IsFPI]= '1' and  A.[FPIPass]='0'", Framework.SystemID);
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@TaskNo",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@TaskNo",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(TaskNo))
            {
                TaskNo = "%" + TaskNo + "%";
                sql = sql + string.Format(@" and A.[TaskNo] like @TaskNo ");
                parameters[0].Value = TaskNo;
                Parcount[0].Value = TaskNo;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[TaskNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }



        /// <summary>
        /// 任务单的开窗
        /// Mouse 2017年11月1日14:22:38
        /// </summary>
        /// <param name="TaskNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00007TaskDispatchListV1(string TaskNo, string ProcessCode, string ProcessName, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.TaskDispatchID,A.TaskNo,A.ItemID,
                D.Code as ItemCode,D.Name+'/'+D.Specification as DescriptionSpec,
                E.Code as ProcessCode,E.Name as ProcessName,E.Description as ProcessDescription,
                F.Code as OperationCode,F.Name as OperationDescName,F.Description as OperationDescription,
                A.DispatchQuantity as DocumentQuantity,
                H.MoNo+'-'+cast (H.SplitSequence as varchar) as MoCode,
                (select Name from SYS_Parameters where ParameterID=H.UnitID) as UnitDescription,
                B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
               (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_TaskDispatch] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                 left join [SYS_Items] D on A.[ItemID] = D.[ItemID]
                 left join [SYS_Parameters] E on A.[ProcessID] = E.[ParameterID]
                 left join [SYS_Parameters] F on A.[OperationID] = F.[ParameterID]
                 left join [SFC_FabricatedMother] H on A.[FabricatedMotherID]=H.[FabricatedMotherID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000089' and A.[IsFPI]= 1 and  A.[FPIPass]=0 
                and A.TaskDispatchID not in (select TaskDispatchID from QCS_InspectionDocument where Status = '{0}020121300008D' and InspectionMethod='{0}0201213000080') 
                and A.TaskDispatchID not in (select TaskDispatchID from QCS_InspectionDocument where Status = '{0}020121300008E' and QualityControlDecision = '{0}0201213000091' and InspectionMethod='{0}0201213000080')", Framework.SystemID);
            //排除检验种类为Q7状态不为作废的任务单
            //排除检验种类为Q7状态不为确认不为拒收的任务单
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@TaskNo",SqlDbType.VarChar),
                new SqlParameter("@ProcessCode",SqlDbType.VarChar),
                new SqlParameter("@ProcessName",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@TaskNo",SqlDbType.VarChar),
                new SqlParameter("@ProcessCode",SqlDbType.VarChar),
                new SqlParameter("@ProcessName",SqlDbType.VarChar),
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            if (!string.IsNullOrWhiteSpace(TaskNo))
            {
                TaskNo = "%" + TaskNo + "%";
                sql = sql + string.Format(@" and A.[TaskNo] collate Chinese_Prc_CI_AS like @TaskNo ");
                parameters[0].Value = TaskNo;
                Parcount[0].Value = TaskNo;
            }

            if (!string.IsNullOrWhiteSpace(ProcessCode))
            {
                ProcessCode = "%" + ProcessCode + "%";
                sql = sql + string.Format(@" and E.[Code] collate Chinese_Prc_CI_AS like  @ProcessCode ");
                parameters[1].Value = ProcessCode;
                Parcount[1].Value = ProcessCode;
            }

            if (!string.IsNullOrWhiteSpace(ProcessName))
            {
                ProcessName = "%" + ProcessName + "%";
                sql = sql + string.Format(@" and E.[Name] collate Chinese_Prc_CI_AS like  @ProcessName ");
                parameters[2].Value = ProcessName;
                Parcount[2].Value = ProcessName;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[TaskNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 新的任务单的开窗
        /// Mouse 2017年8月18日11:06:59
        /// </summary>
        /// <param name="TaskNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00008TaskDispatchList(string TaskNo, int page, int rows, ref int count)
        {
            string select = string.Format(
              @"select A.TaskDispatchID,A.TaskNo,A.ItemID,
                D.Code as ItemCode,D.Name+'/'+D.Specification as DescriptionSpec,
                E.Code as ProcessCode,E.Name as ProcessName,E.Description as ProcessDescription,
                F.Code as OperationCode,F.Name as OperationDescName,F.Description as OperationDescription,
                A.DispatchQuantity as DocumentQuantity,
                H.MoNo+'-'+cast (H.SplitSequence as varchar) as MoCode,
                (select Name from SYS_Parameters where ParameterID=H.UnitID) as UnitDescription,
                B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
               (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_TaskDispatch] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                 left join [SYS_Items] D on A.[ItemID] = D.[ItemID]
                 left join [SYS_Parameters] E on A.[ProcessID] = E.[ParameterID]
                 left join [SYS_Parameters] F on A.[OperationID] = F.[ParameterID]
                 left join [SFC_FabricatedMother] H on A.[FabricatedMotherID]=H.[FabricatedMotherID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000089' and A.[IsOSI]='1' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@TaskNo",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@TaskNo",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(TaskNo))
            {
                TaskNo = "%" + TaskNo + "%";
                sql = sql + string.Format(@" and A.[TaskNo] like @TaskNo ");
                parameters[0].Value = TaskNo;
                Parcount[0].Value = TaskNo;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[TaskNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }
        /// <summary>
        /// 获取工作站列表
        /// Tom 2017年7月17日16点48分
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00006GetList(string TaskNo, string MoNo, string WorkcenterCode, string ProcessCode,
            string OperationCode, string ClassCode, string StartDate, string EndDate, string Status, int page, int rows, ref int count)
        {
            //       string select = string.Format(
            //         @"select A.TaskDispatchID, H.MoNo, A.TaskNo, A.ItemID, D.Code as ItemCode, D.Name as ItemName, 
            //                  D.Specification,A.DispatchDate, A.FabricatedMotherID, A.FabMoProcessID,
            //                  A.FabMoOperationID, A.ProcessID, A.OperationID, T.Code as WorkCenterCode, 
            //                  T.Name as WorkCenterName,
            //                  A.FinishQuantity, A.ScrapQuantity, A.DiffQuantity, A.RepairQuantity, A.DispatchQuantity as AssignQty,
            //                  A.StartDate, A.FinishDate, A.Comments, 
            //                  K.Sequence as NextProcessSequence, L.Code as NextProcessCode, L.Name as NextProcessName,
            //                  N.Sequence as NextOperationSequence, O.Code as NextOperationCode, O.Name as NextOperationName, 
            //                  R.Name as StatusDes, A.Status,
            //                  E.Code as ProcessCode, E.Name as ProcessName, S.Code as OperationCode, S.Name as OperationName,
            //                  B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            //                  (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            //                  (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime  ", Framework.SystemID);

            //       string sql = string.Format(
            //          @"from [SFC_TaskDispatch] A               
            //            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            //            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            //            left join [SYS_Items] D on A.[ItemID] = D.[ItemID]
            //            left join [SYS_Parameters] E on A.[ProcessID] = E.[ParameterID]
            //            left join [SYS_Parameters] F on A.[OperationID] = F.[ParameterID]
            //            left join [SFC_FabricatedMother] H on A.[FabricatedMotherID]=H.[FabricatedMotherID]
            //            left join [SFC_FabMoOperation] I on A.[FabMoOperationID]=I.[FabMoOperationID]               
            //            left join [SYS_Parameters] R on A.[Status] = R.[ParameterID]
            //            left join [SYS_Parameters] S on A.[OperationID] = S.[ParameterID]                 
            //left join [SFC_FabMoProcess] U on A.[FabMoProcessID] = U.FabMoProcessID
            //            left join [SYS_WorkCenter] T on U.WorkCenterID = T.WorkCenterID

            //            left join [SFC_FabMoRelationship] J on A.[FabMoProcessID] =J.[PreFabMoProcessID] and J.[FabMoProcessID] <>  J.[PreFabMoProcessID]

            //            left join [SFC_FabMoProcess] K on K.FabMoProcessID = J.FabMoProcessID
            //            left join [SYS_Parameters] L on K.[ProcessID] = L.[ParameterID]
            //            left join [SFC_FabMoOperationRelationship] M on  A.FabMoOperationID = M.PreFabMoOperationID 
            //            left join [SFC_FabMoOperation] N on N.FabMoOperationID = M.FabMoOperationID
            //            left join [SYS_Parameters] O on N.[OperationID] = O.[ParameterID]              			


            //           where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and A.[IsDispatch]=1 ", Framework.SystemID);

            string select = string.Format(
          @"select A.TaskDispatchID, H.MoNo, A.TaskNo, A.ItemID, D.Code as ItemCode, D.Name as ItemName, 
                       D.Specification,A.DispatchDate, A.FabricatedMotherID, A.FabMoProcessID,
                       A.FabMoOperationID, A.ProcessID, A.OperationID,
                       A.FinishQuantity, A.ScrapQuantity, A.DiffQuantity, A.RepairQuantity, A.DispatchQuantity as AssignQty,
                       A.StartDate, A.FinishDate, A.Comments,                        
                       R.Name as StatusDes, A.Status,
                       E.Code as ProcessCode, E.Name as ProcessName, F.Code as OperationCode, F.Name as OperationName,
                       (select [Code] from [SYS_WorkCenter] where U.[WorkCenterID] = WorkCenterID) as WorkCenterCode,
                       B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,null as Next,
                       (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
                       (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime  ", Framework.SystemID);

            string sql = string.Format(
               @"from [SFC_TaskDispatch] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
                 left join [SYS_Items] D on A.[ItemID] = D.[ItemID]
                 left join [SYS_Parameters] E on A.[ProcessID] = E.[ParameterID]
                 left join [SYS_Parameters] F on A.[OperationID] = F.[ParameterID]
                 left join [SFC_FabricatedMother] H on A.[FabricatedMotherID]=H.[FabricatedMotherID]
                 left join [SFC_FabMoOperation] I on A.[FabMoOperationID]=I.[FabMoOperationID]               
                 left join [SYS_Parameters] R on A.[Status] = R.[ParameterID]             
				 left join [SFC_FabMoProcess] U on A.[FabMoProcessID] = U.[FabMoProcessID]
                 left join [SYS_Class] G on G.[ClassID] = A.[ClassID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and A.[IsDispatch]=1 ", Framework.SystemID);



            List<SqlParameter> parameterList = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(TaskNo))
            {
                sql += string.Format(" and A.TaskNo like @TaskNo");
                parameterList.Add(new SqlParameter("@TaskNo", "%" + TaskNo + "%"));
            }

            if (!string.IsNullOrWhiteSpace(MoNo))
            {
                sql += string.Format(" and H.MoNo like @MoNo");
                parameterList.Add(new SqlParameter("@MoNo", "%" + MoNo + "%"));
            }

            if (!string.IsNullOrWhiteSpace(WorkcenterCode))
            {
                sql += string.Format(" and (Select Top 1 Code from [SYS_WorkCenter] where U.[WorkCenterID] = [WorkCenterID]) like @WorkcenterCode");
                parameterList.Add(new SqlParameter("@WorkcenterCode", "%" + WorkcenterCode + "%"));
            }

            if (!string.IsNullOrWhiteSpace(ProcessCode))
            {
                sql += string.Format(" and E.Code like @ProcessCode");
                parameterList.Add(new SqlParameter("@ProcessCode", "%" + ProcessCode + "%"));
            }

            if (!string.IsNullOrWhiteSpace(OperationCode))
            {
                sql += string.Format(" and F.Code like @OperationCode");
                parameterList.Add(new SqlParameter("@OperationCode", "%" + OperationCode + "%"));
            }

            if (!string.IsNullOrWhiteSpace(ClassCode))
            {
                sql += string.Format(" and G.Code like @ClassCode");
                parameterList.Add(new SqlParameter("@ClassCode", "%" + ClassCode + "%"));
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql += string.Format(" and A.Status = @Status");
                parameterList.Add(new SqlParameter("@Status", Status));
            }

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql += string.Format(" and A.StartDate >= @StartDate");
                parameterList.Add(new SqlParameter("@StartDate", StartDate));
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql += string.Format(" and A.FinishDate <= @FinishDate");
                parameterList.Add(new SqlParameter("@FinishDate", EndDate));
            }

            SqlParameter[] parameters = parameterList.ToArray();
            count = UniversalService.getCount(sql, parameters);

            string orderby = " A.[TaskNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 获取工作站作业列表
        /// SAM 2017年10月24日09:27:32 
        /// 在原来的基础上，加多了班别字段。代号-名称
        /// </summary>
        /// <param name="TaskNo"></param>
        /// <param name="MoNo"></param>
        /// <param name="WorkcenterCode"></param>
        /// <param name="ProcessCode"></param>
        /// <param name="OperationCode"></param>
        /// <param name="ClassCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00006GetListV1(string TaskNo, string MoNo, string WorkcenterCode, string ProcessCode,
          string OperationCode, string ClassCode, string StartDate, string EndDate, string Status, int page, int rows, ref int count)
        {
            string select = string.Format(
          @"select A.TaskDispatchID, H.MoNo, A.TaskNo, A.ItemID, D.Code as ItemCode, D.Name as ItemName, 
                       D.Specification,A.DispatchDate, A.FabricatedMotherID, A.FabMoProcessID,
                       A.FabMoOperationID, A.ProcessID, A.OperationID,
                       A.FinishQuantity, A.ScrapQuantity, A.DiffQuantity, A.RepairQuantity, A.DispatchQuantity as AssignQty,
                       A.StartDate, A.FinishDate, A.Comments,                        
                       R.Name as StatusDes, A.Status,G.Code+'-'+G.Name as ClassCodeName,
                       E.Code as ProcessCode, E.Name as ProcessName, F.Code as OperationCode, F.Name as OperationName,
                       (select [Code] from [SYS_WorkCenter] where U.[WorkCenterID] = WorkCenterID) as WorkCenterCode,
                       B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,null as Next,
                       (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
                       (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime  ", Framework.SystemID);

            string sql = string.Format(
               @"from [SFC_TaskDispatch] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
                 left join [SYS_Items] D on A.[ItemID] = D.[ItemID]
                 left join [SYS_Parameters] E on A.[ProcessID] = E.[ParameterID]
                 left join [SYS_Parameters] F on A.[OperationID] = F.[ParameterID]
                 left join [SFC_FabricatedMother] H on A.[FabricatedMotherID]=H.[FabricatedMotherID]
                 left join [SFC_FabMoOperation] I on A.[FabMoOperationID]=I.[FabMoOperationID]               
                 left join [SYS_Parameters] R on A.[Status] = R.[ParameterID]             
				 left join [SFC_FabMoProcess] U on A.[FabMoProcessID] = U.[FabMoProcessID]
                 left join [SYS_Class] G on G.[ClassID] = A.[ClassID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and A.[IsDispatch]=1 ", Framework.SystemID);

            List<SqlParameter> parameterList = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(TaskNo))
            {
                sql += string.Format(" and A.TaskNo like @TaskNo");
                parameterList.Add(new SqlParameter("@TaskNo", "%" + TaskNo + "%"));
            }

            if (!string.IsNullOrWhiteSpace(MoNo))
            {
                sql += string.Format(" and H.MoNo like @MoNo");
                parameterList.Add(new SqlParameter("@MoNo", "%" + MoNo + "%"));
            }

            if (!string.IsNullOrWhiteSpace(WorkcenterCode))
            {
                sql += string.Format(" and (Select Top 1 Code from [SYS_WorkCenter] where U.[WorkCenterID] = [WorkCenterID]) like @WorkcenterCode");
                parameterList.Add(new SqlParameter("@WorkcenterCode", "%" + WorkcenterCode + "%"));
            }

            if (!string.IsNullOrWhiteSpace(ProcessCode))
            {
                sql += string.Format(" and E.Code like @ProcessCode");
                parameterList.Add(new SqlParameter("@ProcessCode", "%" + ProcessCode + "%"));
            }

            if (!string.IsNullOrWhiteSpace(OperationCode))
            {
                sql += string.Format(" and F.Code like @OperationCode");
                parameterList.Add(new SqlParameter("@OperationCode", "%" + OperationCode + "%"));
            }

            if (!string.IsNullOrWhiteSpace(ClassCode))
            {
                sql += string.Format(" and G.Code like @ClassCode");
                parameterList.Add(new SqlParameter("@ClassCode", "%" + ClassCode + "%"));
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql += string.Format(" and A.Status = @Status");
                parameterList.Add(new SqlParameter("@Status", Status));
            }

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql += string.Format(" and A.StartDate >= @StartDate");
                parameterList.Add(new SqlParameter("@StartDate", StartDate));
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql += string.Format(" and A.FinishDate <= @FinishDate");
                parameterList.Add(new SqlParameter("@FinishDate", EndDate));
            }

            SqlParameter[] parameters = parameterList.ToArray();
            count = UniversalService.getCount(sql, parameters);

            string orderby = " A.[TaskNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取工作站作业列表
        /// SAM 2017年10月26日19:15:04
        /// 在V1的基础上，加多了进站人和出站人的显示
        /// </summary>
        /// <param name="TaskNo"></param>
        /// <param name="MoNo"></param>
        /// <param name="WorkcenterCode"></param>
        /// <param name="ProcessCode"></param>
        /// <param name="OperationCode"></param>
        /// <param name="ClassCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00006GetListV2(string TaskNo, string MoNo, string WorkcenterCode, string ProcessCode,
         string OperationCode, string ClassCode, string StartDate, string EndDate, string Status, int page, int rows, ref int count)
        {
            string select = string.Format(
          @"select A.TaskDispatchID, H.MoNo, A.TaskNo, A.ItemID, D.Code as ItemCode, D.Name as ItemName, 
                       D.Specification,A.DispatchDate, A.FabricatedMotherID, A.FabMoProcessID,
                       A.FabMoOperationID, A.ProcessID, A.OperationID,
                       A.FinishQuantity, A.ScrapQuantity, A.DiffQuantity, A.RepairQuantity, A.DispatchQuantity as AssignQty,
                       A.StartDate, A.FinishDate, A.Comments,A.InDateTime,A.OutDateTime,                       
                       R.Name as StatusDes, A.Status,G.Code+'-'+G.Name as ClassCodeName,
                       E.Code as ProcessCode, E.Name as ProcessName, F.Code as OperationCode, F.Name as OperationName,
                       (select [Code] from [SYS_WorkCenter] where U.[WorkCenterID] = WorkCenterID) as WorkCenterCode,null as Next,
                        (CASE WHEN BB.Emplno is null or BB.Emplno = '' THEN BB.Account else BB.Emplno END)+'-'+BB.UserName as InMESUser,
                        (CASE WHEN CC.Emplno is null or CC.Emplno = '' THEN CC.Account else CC.Emplno END)+'-'+CC.UserName as OutMESUser,
                        (CASE WHEN B.Emplno is null or B.Emplno = ''  or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                        (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN B.Emplno is null or C.Emplno = '' or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
                        (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
               @"from [SFC_TaskDispatch] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
                 left join [SYS_Items] D on A.[ItemID] = D.[ItemID]
                 left join [SYS_Parameters] E on A.[ProcessID] = E.[ParameterID]
                 left join [SYS_Parameters] F on A.[OperationID] = F.[ParameterID]
                 left join [SFC_FabricatedMother] H on A.[FabricatedMotherID]=H.[FabricatedMotherID]
                 left join [SFC_FabMoOperation] I on A.[FabMoOperationID]=I.[FabMoOperationID]               
                 left join [SYS_Parameters] R on A.[Status] = R.[ParameterID]             
				 left join [SFC_FabMoProcess] U on A.[FabMoProcessID] = U.[FabMoProcessID]
                 left join [SYS_Class] G on G.[ClassID] = A.[ClassID]
                 left join [SYS_MESUsers] BB on A.[InMESUserID] = BB.[MESUserID]
                 left join [SYS_MESUsers] CC on A.[OutMESUserID] = CC.[MESUserID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and A.[IsDispatch]=1 ", Framework.SystemID);

            List<SqlParameter> parameterList = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(TaskNo))
            {
                sql += string.Format(" and A.TaskNo like @TaskNo");
                parameterList.Add(new SqlParameter("@TaskNo", "%" + TaskNo + "%"));
            }

            if (!string.IsNullOrWhiteSpace(MoNo))
            {
                sql += string.Format(" and H.MoNo like @MoNo");
                parameterList.Add(new SqlParameter("@MoNo", "%" + MoNo + "%"));
            }

            if (!string.IsNullOrWhiteSpace(WorkcenterCode))
            {
                sql += string.Format(" and (Select Top 1 Code from [SYS_WorkCenter] where U.[WorkCenterID] = [WorkCenterID]) like @WorkcenterCode");
                parameterList.Add(new SqlParameter("@WorkcenterCode", "%" + WorkcenterCode + "%"));
            }

            if (!string.IsNullOrWhiteSpace(ProcessCode))
            {
                sql += string.Format(" and E.Code like @ProcessCode");
                parameterList.Add(new SqlParameter("@ProcessCode", "%" + ProcessCode + "%"));
            }

            if (!string.IsNullOrWhiteSpace(OperationCode))
            {
                sql += string.Format(" and F.Code like @OperationCode");
                parameterList.Add(new SqlParameter("@OperationCode", "%" + OperationCode + "%"));
            }

            if (!string.IsNullOrWhiteSpace(ClassCode))
            {
                sql += string.Format(" and G.Code like @ClassCode");
                parameterList.Add(new SqlParameter("@ClassCode", "%" + ClassCode + "%"));
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql += string.Format(" and A.Status = @Status");
                parameterList.Add(new SqlParameter("@Status", Status));
            }

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql += string.Format(" and A.StartDate >= @StartDate");
                parameterList.Add(new SqlParameter("@StartDate", StartDate));
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql += string.Format(" and A.FinishDate <= @FinishDate");
                parameterList.Add(new SqlParameter("@FinishDate", EndDate));
            }

            SqlParameter[] parameters = parameterList.ToArray();
            count = UniversalService.getCount(sql, parameters);

            string orderby = " A.[TaskNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 设备巡检维护任务单弹窗
        /// Tom 2017年7月28日17:40:38
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems0003GetTaskDispatchList(string EquipmentID, int page, int rows, ref int count)
        {
            string select = string.Format(
              @"select A.TaskDispatchID, A.TaskNo, H.MoNo, R.Name as StatusDes, 
                       A.Status, A.ItemID, D.Code as ItemCode, D.Name as ItemName,
                       (select [Code] from [SYS_WorkCenter] where WorkCenterID= U.WorkCenterID) as WorkCenterCode,
                       (select [Name] from [SYS_WorkCenter] where WorkCenterID= U.WorkCenterID) as WorkCenterName ",
              Framework.SystemID);

            string sql = string.Format(
               @"from [SFC_TaskDispatch] A               
                 left join [SYS_Items] D on A.[ItemID] = D.[ItemID]                
                 left join [SFC_FabricatedMother] H on A.[FabricatedMotherID]=H.[FabricatedMotherID]    
				 left join [SFC_FabMoProcess] U on A.FabMoProcessID = U.FabMoProcessID             
                 left join [SYS_Parameters] R on A.Status = R.[ParameterID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000089' and [IsDispatch]=1 ", Framework.SystemID);

            sql += string.Format(@" and [TaskDispatchID] in (select [TaskDispatchID] from [SFC_TaskDispatchResource] where [EquipmentID]='{0}' and [Status]='{1}0201213000001')", EquipmentID, Framework.SystemID);

            count = UniversalService.getCount(sql, null);

            string orderby = " A.[TaskNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 工作站导出
        /// Tom 2017年8月21日09:26:16
        /// </summary>
        /// <param name="TaskNo"></param>
        /// <param name="MoNo"></param>
        /// <param name="WorkcenterID"></param>
        /// <param name="ProcessID"></param>
        /// <param name="OperationID"></param>
        /// <param name="ClassID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static DataTable Sfc00006GetExprotList(string TaskNo, string MoNo, string WorkcenterID, string ProcessID,
            string OperationID, string ClassID, string StartDate, string EndDate, string Status)
        {
            string sql = string.Format(
              @"select A.TaskNo, H.MoNo, D.Code as ItemCode, D.Name as ItemName, D.Specification,
                       A.DispatchQuantity as AssignQty, A.FinishQuantity, A.ScrapQuantity, A.DiffQuantity, A.RepairQuantity, 
                       A.StartDate, A.FinishDate, A.Comments, K.Sequence as NextProcessSequence, L.Code as NextProcessCode, L.Name as NextProcessName,
                       N.Sequence as NextOperationSequence, O.Code as NextOperationCode, O.Name as NextOperationName, R.Name as StatusDes,
                       B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                       (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
                       (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime  
                from [SFC_TaskDispatch] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
                 left join [SYS_Items] D on A.[ItemID] = D.[ItemID]
                 left join [SYS_Parameters] E on A.[ProcessID] = E.[ParameterID]
                 left join [SYS_Parameters] F on A.[OperationID] = F.[ParameterID]
                 left join [SFC_FabricatedMother] H on A.[FabricatedMotherID]=H.[FabricatedMotherID]
                 left join [SFC_FabMoOperation] I on I.FabMoOperationID = A.FabMoOperationID
                 left join [SFC_FabMoRelationship] J on J.PreFabMoProcessID = A.FabMoProcessID
                 left join [SFC_FabMoProcess] K on K.FabMoProcessID = J.FabMoProcessID
                 left join [SYS_Parameters] L on K.[ProcessID] = L.[ParameterID]
                 left join [SFC_FabMoOperationRelationship] M on M.PreFabMoOperationID = A.FabMoOperationID
                 left join [SFC_FabMoOperation] N on N.FabMoOperationID = M.FabMoOperationID
                 left join [SYS_Parameters] O on N.[OperationID] = O.[ParameterID]
                 left join [SYS_Parameters] R on A.Status = R.[ParameterID]
				 left join [SYS_Parameters] S on A.OperationID = S.[ParameterID]
                 left join [SYS_WorkCenter] T on K.WorkCenterID = T.WorkCenterID
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

            List<SqlParameter> parameterList = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(TaskNo))
            {
                sql += string.Format(" and A.TaskNo like @TaskNo");
                parameterList.Add(new SqlParameter("@TaskNo", "%" + TaskNo + "%"));
            }

            if (!string.IsNullOrWhiteSpace(MoNo))
            {
                sql += string.Format(" and H.MoNo like @MoNo");
                parameterList.Add(new SqlParameter("@MoNo", "%" + MoNo + "%"));
            }

            if (!string.IsNullOrWhiteSpace(WorkcenterID))
            {
                sql += string.Format(" and K.WorkcenterID = @WorkcenterID");
                parameterList.Add(new SqlParameter("@WorkcenterID", WorkcenterID));
            }

            if (!string.IsNullOrWhiteSpace(WorkcenterID))
            {
                sql += string.Format(" and K.ProcessID = @ProcessID");
                parameterList.Add(new SqlParameter("@ProcessID", ProcessID));
            }

            if (!string.IsNullOrWhiteSpace(OperationID))
            {
                sql += string.Format(" and I.OperationID = @OperationID");
                parameterList.Add(new SqlParameter("@OperationID", ProcessID));
            }

            if (!string.IsNullOrWhiteSpace(ClassID))
            {
                sql += string.Format(" and A.ClassID = @ClassID");
                parameterList.Add(new SqlParameter("@ClassID", ClassID));
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql += string.Format(" and A.Status = @Status");
                parameterList.Add(new SqlParameter("@Status", Status));
            }

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql += string.Format(" and A.StartDate >= @StartDate");
                parameterList.Add(new SqlParameter("@StartDate", StartDate));
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql += string.Format(" and A.EndDate <= @EndDate");
                parameterList.Add(new SqlParameter("@EndDate", EndDate));
            }

            SqlParameter[] parameters = parameterList.ToArray();
            sql += " order by A.[TaskNo] ";

            return SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);
        }

        /// <summary>
        /// 完工單回報作業-任務單號開窗（状态不为CL和CA）
        /// SAM 2017年7月19日15:48:34
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00007GetTaskList(string TaskCode, string ItemCode, int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select A.TaskDispatchID,A.TaskNo,A.ItemID,H.BatchNumber,
                H.MoNo+'-'+convert(varchar(20),H.SplitSequence) as OrderNum,I.Name as Status,
                D.Code as ItemCode,D.Name as ItemName,D.Specification as ItemSpecification,
                (Select Name from [SYS_Parameters] where H.UnitID = ParameterID) as ManufacturingUnit,
                (Select Name from [SYS_Parameters] where G.UnitID = ParameterID) as AuxiliaryUnit,G.UnitRate,
                (Select Code from [SYS_Parameters] where D.LotMethod = ParameterID) as LotMethod,
                   A.DispatchQuantity-A.FinishQuantity as FinProQtyAble,                    
                B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
               (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_TaskDispatch] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                 left join [SYS_Items] D on A.[ItemID] = D.[ItemID]
                 left join [SFC_FabricatedMother] H on A.[FabricatedMotherID]=H.[FabricatedMotherID]
                 left join [SFC_FabMoProcess] G on A.[FabMoProcessID]=G.[FabMoProcessID]
                 left join [SYS_Parameters] I on A.[Status]=I.[ParameterID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' 
                and A.[Status] <> '{0}020121300008B' and A.[Status] <> '{0}020121300008C' 
                and A.[IsDispatch]=1 ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(TaskCode))
                sql += String.Format(@" and A.[TaskNo] like '%{0}%'", TaskCode);

            if (!string.IsNullOrWhiteSpace(ItemCode))
                sql += String.Format(@" and D.[Code] like '%{0}%'", ItemCode);

            count = UniversalService.getCount(sql, null);

            string orderby = " A.[Status],A.[TaskNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 检测班别是否存在资料
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static SFC_TaskDispatch Check(string Code)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_TaskDispatch] where [ClassID] = '{0}'  and [SystemID] = '{1}' ", Code, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }


        /// <summary>
        /// 获取指定制程或者工序的立单状态的分派量
        /// SAM 2017年8月22日14:56:12
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <returns></returns>
        public static decimal GetNAAssignQuantity(string FabMoProcessID, string FabMoOperationID)
        {
            string sql = string.Format(@"select ISnull(SUM(DispatchQuantity),0) from [SFC_TaskDispatch] where [SystemID] = '{0}' and [Status]='{0}0201213000087'", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(FabMoProcessID))
                sql += string.Format(@" and [FabMoProcessID] ='{0}'", FabMoProcessID);

            if (!string.IsNullOrWhiteSpace(FabMoOperationID))
                sql += string.Format(@" and [FabMoOperationID] ='{0}'", FabMoOperationID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return 0;
            else
                return decimal.Parse(dt.Rows[0][0].ToString());
        }


        /// <summary>
        /// 判断制令单是否存在立單NA,核發OP,進站IN的任务单
        /// SAM 2017年8月22日16:20:06
        /// </summary>
        /// <param name="FabricatedMotherID"></param>
        /// <returns></returns>
        public static bool CheckByFabMo(string FabricatedMotherID)
        {
            string sql = string.Format(@"select * from [SFC_TaskDispatch] 
            where [Status] in ('{1}0201213000087','{1}0201213000088','{1}0201213000089')
            and [FabricatedMotherID]='{0}' and [SystemID] = '{1}' ", FabricatedMotherID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// 根据设备ID获取状态为OP或IN的任务卡
        /// Mouse 2017年9月13日11:25:46
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        public static List<SFC_TaskDispatch> GetIot00003EquipmentTask(string EquipmentID)
        {
            string sql = string.Format(@"select distinct B.* from SFC_TaskDispatchResource A 
                        left join [SFC_TaskDispatch] B on A.TaskDispatchID = B.TaskDispatchID
                        where A.Type='{0}' and A.Status='{1}0201213000001' and  B.Status='{1}0201213000088' or B.Status='{1}0201213000089'", EquipmentID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            return ToList(dt);

        }


        /// <summary>
        /// 判断任务单号是否重复
        /// Sam 2017年9月13日10:43:38
        /// </summary>
        /// <param name="TaskNo"></param>
        /// <param name="TaskDispatchID"></param>
        /// <returns></returns>
        public static bool Check(string TaskNo, string TaskDispatchID)
        {
            string sql = String.Format(@"select Top 1 * from [SFC_TaskDispatch] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@TaskNo",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(TaskNo))
            {
                sql = sql + String.Format(@" and [TaskNo] =@TaskNo ");
                parameters[0].Value = TaskNo;
            }

            /*TaskDispatchID（流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(TaskDispatchID))
                sql = sql + String.Format(@" and [TaskDispatchID] <> '{0}' ", TaskDispatchID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据制令制程获取总实际工时
        /// SAM 2017年9月27日15:48:32
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static int Sfc00019GetHour(string FabMoProcessID)
        {
            string sql = String.Format(@"select ISNULL(SUM(datediff(SECOND,InDateTime,OutDateTime)),0) from [SFC_TaskDispatch] 
            where [SystemID]='{0}' and [Status] <> '{0}0201213000003'
            and [FabMoProcessID] ='{1}' and [FabMoOperationID] is null and OutDateTime is not null ", Framework.SystemID, FabMoProcessID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return 0;
            else
                return int.Parse(dt.Rows[0][0].ToString());
        }

        /// <summary>
        /// 完工單回報作業-任務單號開窗（状态不为CL和CA）
        /// SAM 2017年10月24日11:20:11
        /// V1版本，加多班别字段显示，然后因为界面并无查询条件，移除查询条件
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00007GetTaskListV1(int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select A.TaskDispatchID,A.TaskNo,A.ItemID,H.BatchNumber,
                H.MoNo+'-'+convert(varchar(20),H.SplitSequence) as OrderNum,I.Name as Status,
                D.Code as ItemCode,D.Name as ItemName,D.Specification as ItemSpecification,
                (Select Name from [SYS_Parameters] where H.UnitID = ParameterID) as ManufacturingUnit,
                (Select Name from [SYS_Parameters] where G.UnitID = ParameterID) as AuxiliaryUnit,G.UnitRate,
                (Select Code from [SYS_Parameters] where D.LotMethod = ParameterID) as LotMethod,
                  A.DispatchQuantity-A.FinishQuantity as FinProQtyAble,E.Code+'-'+E.Name as ClassCodeName,               
                B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
               (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_TaskDispatch] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                 left join [SYS_Items] D on A.[ItemID] = D.[ItemID]
                 left join [SFC_FabricatedMother] H on A.[FabricatedMotherID]=H.[FabricatedMotherID]
                 left join [SFC_FabMoProcess] G on A.[FabMoProcessID]=G.[FabMoProcessID]
                 left join [SYS_Parameters] I on A.[Status]=I.[ParameterID]
                 left join [SYS_Class] E on A.[ClassID]=E.[ClassID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' 
                and A.[Status] <> '{0}020121300008B' and A.[Status] <> '{0}020121300008C' and A.[IsDispatch]=1 ", Framework.SystemID);

            count = UniversalService.getCount(sql, null);

            string orderby = " A.[Status],A.[TaskNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取制程/工序对应的任务单的总分派量（所有非删除，非作废的任务单）
        /// SAM 2017年10月24日11:59:26
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <returns></returns>
        public static decimal GetTaskAssignQuantity(string FabMoProcessID, string FabMoOperationID)
        {
            string sql = string.Format(@"select ISnull(SUM(DispatchQuantity),0) from [SFC_TaskDispatch] where [SystemID] = '{0}' and [Status] <> '{0}020121300008C' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(FabMoProcessID))
                sql += string.Format(@" and [FabMoProcessID] ='{0}'", FabMoProcessID);

            if (!string.IsNullOrWhiteSpace(FabMoOperationID))
                sql += string.Format(@" and [FabMoOperationID] ='{0}'", FabMoOperationID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return 0;
            else
                return decimal.Parse(dt.Rows[0][0].ToString());
        }

        /// <summary>
        /// 获取制程/工序对应的任务单的总差异量（所有非删除，非作废的任务单）
        /// SAM 2017年10月24日11:59:26
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <returns></returns>
        public static decimal GetTaskDiffQuantity(string FabMoProcessID, string FabMoOperationID)
        {
            string sql = string.Format(@"select ISnull(SUM(DiffQuantity),0) from [SFC_TaskDispatch] where [SystemID] = '{0}' and [Status] <> '{0}020121300008C' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(FabMoProcessID))
                sql += string.Format(@" and [FabMoProcessID] ='{0}'", FabMoProcessID);

            if (!string.IsNullOrWhiteSpace(FabMoOperationID))
                sql += string.Format(@" and [FabMoOperationID] ='{0}'", FabMoOperationID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return 0;
            else
                return decimal.Parse(dt.Rows[0][0].ToString());
        }
    }
}

