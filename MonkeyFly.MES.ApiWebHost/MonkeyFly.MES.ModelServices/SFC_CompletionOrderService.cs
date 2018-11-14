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
    public class SFC_CompletionOrderService : SuperModel<SFC_CompletionOrder>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年7月23日01:52:45
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SFC_CompletionOrder Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_CompletionOrder](
                [CompletionOrderID],[CompletionNo],[Date],[Sequence],
                [Type],[TaskDispatchID],[FabricatedMotherID],[FabMoProcessID],[FabMoOperationID],
                [OriginalCompletionOrderID],[InspectionID],[ItemID],[ProcessID],[OperationID],
                [ClassID],[NextFabMoProcessID],[NextFabMoOperationID],
                [NextProcessID],[NextOperationID],[IsIF],[InspectionGroupID],
                [FinProQuantity],[ScrappedQuantity],[DifferenceQuantity],[RepairQuantity],
                [InspectionQuantity],[LaborHour],[UnLaborHour],[MachineHour],
                [UnMachineHour],[DTSID],[Status],[ReasonID],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],
                [Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (
                @CompletionOrderID,@CompletionNo,
                @Date,@Sequence,@Type,
                @TaskDispatchID,@FabricatedMotherID,@FabMoProcessID,
                @FabMoOperationID,@OriginalCompletionOrderID,@InspectionID,
                @ItemID,@ProcessID,@OperationID,
                @ClassID,@NextFabMoProcessID,@NextFabMoOperationID,
                @NextProcessID,@NextOperationID,@IsIF,
                @InspectionGroupID,@FinProQuantity,@ScrappedQuantity,
                @DifferenceQuantity,@RepairQuantity,@InspectionQuantity,
                @LaborHour,@UnLaborHour,@MachineHour,
                @UnMachineHour,@DTSID,@Status,@ReasonID,
                @Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CompletionOrderID",SqlDbType.VarChar),
                    new SqlParameter("@CompletionNo",SqlDbType.NVarChar),
                    new SqlParameter("@Date",SqlDbType.DateTime),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@TaskDispatchID",SqlDbType.VarChar),
                    new SqlParameter("@FabricatedMotherID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@OriginalCompletionOrderID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionID",SqlDbType.VarChar),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@ProcessID",SqlDbType.VarChar),
                    new SqlParameter("@OperationID",SqlDbType.VarChar),
                    new SqlParameter("@ClassID",SqlDbType.VarChar),
                    new SqlParameter("@NextFabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@NextFabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@NextProcessID",SqlDbType.VarChar),
                    new SqlParameter("@NextOperationID",SqlDbType.VarChar),
                    new SqlParameter("@IsIF",SqlDbType.Bit),
                    new SqlParameter("@InspectionGroupID",SqlDbType.VarChar),
                    new SqlParameter("@FinProQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ScrappedQuantity",SqlDbType.Decimal),
                    new SqlParameter("@DifferenceQuantity",SqlDbType.Decimal),
                    new SqlParameter("@RepairQuantity",SqlDbType.Decimal),
                    new SqlParameter("@InspectionQuantity",SqlDbType.Decimal),
                    new SqlParameter("@LaborHour",SqlDbType.BigInt),
                    new SqlParameter("@UnLaborHour",SqlDbType.BigInt),
                    new SqlParameter("@MachineHour",SqlDbType.BigInt),
                    new SqlParameter("@UnMachineHour",SqlDbType.BigInt),
                    new SqlParameter("@DTSID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@ReasonID",SqlDbType.VarChar)
                    };

                parameters[0].Value = (Object)Model.CompletionOrderID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.CompletionNo ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Date ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[5].Value = (Object)Model.TaskDispatchID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.FabricatedMotherID ?? DBNull.Value;
                parameters[7].Value = (Object)Model.FabMoProcessID ?? DBNull.Value;
                parameters[8].Value = (Object)Model.FabMoOperationID ?? DBNull.Value;
                parameters[9].Value = (Object)Model.OriginalCompletionOrderID ?? DBNull.Value;
                parameters[10].Value = (Object)Model.InspectionID ?? DBNull.Value;
                parameters[11].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[12].Value = (Object)Model.ProcessID ?? DBNull.Value;
                parameters[13].Value = (Object)Model.OperationID ?? DBNull.Value;
                parameters[14].Value = (Object)Model.ClassID ?? DBNull.Value;
                parameters[15].Value = (Object)Model.NextFabMoProcessID ?? DBNull.Value;
                parameters[16].Value = (Object)Model.NextFabMoOperationID ?? DBNull.Value;
                parameters[17].Value = (Object)Model.NextProcessID ?? DBNull.Value;
                parameters[18].Value = (Object)Model.NextOperationID ?? DBNull.Value;
                parameters[19].Value = (Object)Model.IsIF ?? DBNull.Value;
                parameters[20].Value = (Object)Model.InspectionGroupID ?? DBNull.Value;
                parameters[21].Value = (Object)Model.FinProQuantity ?? DBNull.Value;
                parameters[22].Value = (Object)Model.ScrappedQuantity ?? DBNull.Value;
                parameters[23].Value = (Object)Model.DifferenceQuantity ?? DBNull.Value;
                parameters[24].Value = (Object)Model.RepairQuantity ?? DBNull.Value;
                parameters[25].Value = (Object)Model.InspectionQuantity ?? DBNull.Value;
                parameters[26].Value = (Object)Model.LaborHour ?? DBNull.Value;
                parameters[27].Value = (Object)Model.UnLaborHour ?? DBNull.Value;
                parameters[28].Value = (Object)Model.MachineHour ?? DBNull.Value;
                parameters[29].Value = (Object)Model.UnMachineHour ?? DBNull.Value;
                parameters[30].Value = (Object)Model.DTSID ?? DBNull.Value;
                parameters[31].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[32].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[33].Value = (Object)Model.ReasonID ?? DBNull.Value;

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
        /// SAM 2017年8月28日14:37:01
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SFC_CompletionOrder Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_CompletionOrder] set {0},
                [CompletionNo]=@CompletionNo,[Date]=@Date,[Sequence]=@Sequence,[ReasonID] =@ReasonID,
                [Type]=@Type,[TaskDispatchID]=@TaskDispatchID,[FabricatedMotherID]=@FabricatedMotherID,[FabMoProcessID]=@FabMoProcessID,
                [FabMoOperationID]=@FabMoOperationID,[OriginalCompletionOrderID]=@OriginalCompletionOrderID,[InspectionID]=@InspectionID,[ItemID]=@ItemID,
                [ProcessID]=@ProcessID,[OperationID]=@OperationID,[ClassID]=@ClassID,[NextFabMoProcessID]=@NextFabMoProcessID,
                [NextFabMoOperationID]=@NextFabMoOperationID,[NextProcessID]=@NextProcessID,[NextOperationID]=@NextOperationID,[IsIF]=@IsIF,
                [InspectionGroupID]=@InspectionGroupID,[FinProQuantity]=@FinProQuantity,[ScrappedQuantity]=@ScrappedQuantity,[DifferenceQuantity]=@DifferenceQuantity,
                [RepairQuantity]=@RepairQuantity,[InspectionQuantity]=@InspectionQuantity,[LaborHour]=@LaborHour,[UnLaborHour]=@UnLaborHour,
                [MachineHour]=@MachineHour,[UnMachineHour]=@UnMachineHour,[DTSID]=@DTSID,[Status]=@Status,
                [Comments]=@Comments where [CompletionOrderID]=@CompletionOrderID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CompletionOrderID",SqlDbType.VarChar),
                    new SqlParameter("@CompletionNo",SqlDbType.NVarChar),
                    new SqlParameter("@Date",SqlDbType.DateTime),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@TaskDispatchID",SqlDbType.VarChar),
                    new SqlParameter("@FabricatedMotherID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@OriginalCompletionOrderID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionID",SqlDbType.VarChar),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@ProcessID",SqlDbType.VarChar),
                    new SqlParameter("@OperationID",SqlDbType.VarChar),
                    new SqlParameter("@ClassID",SqlDbType.VarChar),
                    new SqlParameter("@NextFabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@NextFabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@NextProcessID",SqlDbType.VarChar),
                    new SqlParameter("@NextOperationID",SqlDbType.VarChar),
                    new SqlParameter("@IsIF",SqlDbType.Bit),
                    new SqlParameter("@InspectionGroupID",SqlDbType.VarChar),
                    new SqlParameter("@FinProQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ScrappedQuantity",SqlDbType.Decimal),
                    new SqlParameter("@DifferenceQuantity",SqlDbType.Decimal),
                    new SqlParameter("@RepairQuantity",SqlDbType.Decimal),
                    new SqlParameter("@InspectionQuantity",SqlDbType.Decimal),
                    new SqlParameter("@LaborHour",SqlDbType.BigInt),
                    new SqlParameter("@UnLaborHour",SqlDbType.BigInt),
                    new SqlParameter("@MachineHour",SqlDbType.BigInt),
                    new SqlParameter("@UnMachineHour",SqlDbType.BigInt),
                    new SqlParameter("@DTSID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@ReasonID",SqlDbType.VarChar)
                    };

                parameters[0].Value = Model.CompletionOrderID;
                parameters[1].Value = (Object)Model.CompletionNo ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Date ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[5].Value = (Object)Model.TaskDispatchID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.FabricatedMotherID ?? DBNull.Value;
                parameters[7].Value = (Object)Model.FabMoProcessID ?? DBNull.Value;
                parameters[8].Value = (Object)Model.FabMoOperationID ?? DBNull.Value;
                parameters[9].Value = (Object)Model.OriginalCompletionOrderID ?? DBNull.Value;
                parameters[10].Value = (Object)Model.InspectionID ?? DBNull.Value;
                parameters[11].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[12].Value = (Object)Model.ProcessID ?? DBNull.Value;
                parameters[13].Value = (Object)Model.OperationID ?? DBNull.Value;
                parameters[14].Value = (Object)Model.ClassID ?? DBNull.Value;
                parameters[15].Value = (Object)Model.NextFabMoProcessID ?? DBNull.Value;
                parameters[16].Value = (Object)Model.NextFabMoOperationID ?? DBNull.Value;
                parameters[17].Value = (Object)Model.NextProcessID ?? DBNull.Value;
                parameters[18].Value = (Object)Model.NextOperationID ?? DBNull.Value;
                parameters[19].Value = (Object)Model.IsIF ?? DBNull.Value;
                parameters[20].Value = (Object)Model.InspectionGroupID ?? DBNull.Value;
                parameters[21].Value = (Object)Model.FinProQuantity ?? DBNull.Value;
                parameters[22].Value = (Object)Model.ScrappedQuantity ?? DBNull.Value;
                parameters[23].Value = (Object)Model.DifferenceQuantity ?? DBNull.Value;
                parameters[24].Value = (Object)Model.RepairQuantity ?? DBNull.Value;
                parameters[25].Value = (Object)Model.InspectionQuantity ?? DBNull.Value;
                parameters[26].Value = (Object)Model.LaborHour ?? DBNull.Value;
                parameters[27].Value = (Object)Model.UnLaborHour ?? DBNull.Value;
                parameters[28].Value = (Object)Model.MachineHour ?? DBNull.Value;
                parameters[29].Value = (Object)Model.UnMachineHour ?? DBNull.Value;
                parameters[30].Value = (Object)Model.DTSID ?? DBNull.Value;
                parameters[31].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[32].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[33].Value = (Object)Model.ReasonID ?? DBNull.Value;
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
        /// SAM 2017年8月1日16:21:00
        /// </summary>
        /// <param name="CompletionOrderID"></param>
        /// <returns></returns>
        public static SFC_CompletionOrder get(string CompletionOrderID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_CompletionOrder] where [CompletionOrderID] = '{0}'  and [SystemID] = '{1}' ", CompletionOrderID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据完工单号获取完工单
        /// SAM 2017年8月1日16:21:32
        /// </summary>
        /// <param name="CompletionNo"></param>
        /// <returns></returns>
        public static SFC_CompletionOrder getByCode(string CompletionNo)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_CompletionOrder] where [CompletionNo] = '{0}'  and [SystemID] = '{1}' ", CompletionNo, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 判断完工单号是否重复
        /// SAM 2017年7月20日11:20:16
        /// </summary>
        /// <param name="CompletionNo"></param>
        /// <returns></returns>
        public static bool CheckCode(string CompletionNo)
        {
            string sql = String.Format(@"select Top 1 * from [SFC_CompletionOrder] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            /*先定义Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@CompletionNo",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            /*因为Code是通过手动输入的，所以需要用参数的形式去拼SQL*/
            if (!string.IsNullOrWhiteSpace(CompletionNo))
            {
                sql = sql + string.Format(@" and [CompletionNo] =@CompletionNo ");
                parameters[0].Value = CompletionNo;
            }

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 完工回报主档读取
        /// joint
        /// </summary>
        /// <param name="CompletionNO"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetFinishList(string CompletionNo, string Code, int page, int rows, ref int count)
        {
            string select = string.Format(
              @"select A.CompletionOrderID,A.CompletionNo,A.Sequence,A.ItemID,B.Code as Code,B.Name+'/'+B.Specification as DescAndSpec,A.ProcessID,C.Code as ProcessCode,C.Name as ProcessDescription,
                A.OperationID,D.Code as OperationCode,D.Name as OperationDescription,E.MoNo+'-'+convert(varchar(20),E.SplitSequence) as MoCode,A.TaskDispatchID,F.TaskNo,A.FinProQuantity as Quantity,
                (select Name from SYS_Parameters where ParameterID=E.UnitID) as UnitDescription", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_CompletionOrder] A
                  left join SYS_Items B on A.ItemID=B.ItemID    
                  left join SYS_Parameters C on A.ProcessID=C.ParameterID
                  left join SYS_Parameters D on A.OperationID=D.ParameterID 
                  left join SFC_FabricatedMother E on A.FabricatedMotherID=E.FabricatedMotherID  
                  left join SFC_TaskDispatch F on A.TaskDispatchID=F.TaskDispatchID     
                  where A.[SystemID] = '{0}' and A.Status = '{0}020121300002A' and A.[IsIF] =1 and [FinProQuantity] > [InspectionQuantity] ", Framework.SystemID);
            //完工单的状态类型是0004，所以注销的状态应该是2A
            //bit类型的字段直接=1或者=0.不需要单引号
            // 注释，获取数据用于安卓测试
            //string sql = string.Format(@"from [SFC_CompletionOrder] A
            //      left join SYS_Items B on A.ItemID=B.ItemID    
            //      left join SYS_Parameters C on A.ProcessID=C.ParameterID
            //      left join SYS_Parameters D on A.OperationID=D.ParameterID 
            //      left join SFC_FabricatedMother E on A.FabricatedMotherID=E.FabricatedMotherID  
            //      left join SFC_TaskDispatch F on A.TaskDispatchID=F.TaskDispatchID     
            //      where A.[SystemID] = '{0}' and A.Status = '{0}020121300002A'", Framework.SystemID);
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(CompletionNo))
            {
                sql += " and A.CompletionNo collate Chinese_PRC_CI_AS like @CompletionNo";
                parameters.Add(new SqlParameter("@CompletionNo", "%" + CompletionNo + "%"));

            }
            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql += " and B.Code collate Chinese_PRC_CI_AS like @Code";
                parameters.Add(new SqlParameter("@Code", "%" + Code + "%"));

            }
            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);

            string orderby = "A.[CompletionNo]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// QCS00005完工单弹窗--需求新增
        /// Mouse 2017年11月9日14:46:26
        /// 去掉状态为开单的，
        /// 去掉状态为确认且允收的
        /// </summary>
        /// <param name="CompletionNO"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetFinishListV1(string CompletionNo, string Code, int page, int rows, ref int count)
        {
            string select = string.Format(
              @"select A.CompletionOrderID,A.CompletionNo,A.Sequence,A.ItemID,B.Code as Code,B.Name+'/'+B.Specification as DescAndSpec,A.ProcessID,C.Code as ProcessCode,C.Name as ProcessDescription,
                A.OperationID,D.Code as OperationCode,D.Name as OperationDescription,E.MoNo+'-'+convert(varchar(20),E.SplitSequence) as MoCode,A.TaskDispatchID,F.TaskNo,A.FinProQuantity as Quantity,
                (select Name from SYS_Parameters where ParameterID=E.UnitID) as UnitDescription ", Framework.SystemID);

            string sql = string.Format(
                @" from [SFC_CompletionOrder] A
                  left join SYS_Items B on A.ItemID=B.ItemID    
                  left join SYS_Parameters C on A.ProcessID=C.ParameterID
                  left join SYS_Parameters D on A.OperationID=D.ParameterID 
                  left join SFC_FabricatedMother E on A.FabricatedMotherID=E.FabricatedMotherID  
                  left join SFC_TaskDispatch F on A.TaskDispatchID=F.TaskDispatchID     
                  where A.[SystemID] = '{0}' and A.Status = '{0}020121300002A' and A.[IsIF] =1 and [FinProQuantity] > [InspectionQuantity] 
                and A.CompletionOrderID not in (select CompletionOrderID from QCS_InspectionDocument where Status = '{0}020121300008D' and InspectionMethod='10039020121300007E' ) 
                and A.CompletionOrderID not in (select CompletionOrderID from QCS_InspectionDocument where Status = '{0}020121300008E' and QualityControlDecision = '{0}0201213000091' and InspectionMethod='10039020121300007E' )
                ", Framework.SystemID);

            //完工单的状态类型是0004，所以注销的状态应该是2A
            //bit类型的字段直接=1或者=0.不需要单引号
            // 注释，获取数据用于安卓测试
            //string sql = string.Format(@"from [SFC_CompletionOrder] A
            //      left join SYS_Items B on A.ItemID=B.ItemID    
            //      left join SYS_Parameters C on A.ProcessID=C.ParameterID
            //      left join SYS_Parameters D on A.OperationID=D.ParameterID 
            //      left join SFC_FabricatedMother E on A.FabricatedMotherID=E.FabricatedMotherID  
            //      left join SFC_TaskDispatch F on A.TaskDispatchID=F.TaskDispatchID     
            //      where A.[SystemID] = '{0}' and A.Status = '{0}020121300002A'", Framework.SystemID);
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(CompletionNo))
            {
                sql += " and A.CompletionNo collate Chinese_PRC_CI_AS like @CompletionNo";
                parameters.Add(new SqlParameter("@CompletionNo", "%" + CompletionNo + "%"));

            }
            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql += " and B.Code collate Chinese_PRC_CI_AS like @Code";
                parameters.Add(new SqlParameter("@Code", "%" + Code + "%"));

            }
            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);

            string orderby = "A.[CompletionNo]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 完工單回報作業-列表
        /// SAM 2017年7月19日15:09:43
        /// </summary>
        /// <param name="FinishNo"></param>
        /// <param name="EndWorkD"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> SFC00007GetList(string TaskDispatchID, string FinishNo, string Date, string WorkCenter, string Process, string FabricatedMother, string Status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.CompletionOrderID,A.CompletionNo,A.Date,A.Sequence,A.Type,
            A.[TaskDispatchID],A.[FabricatedMotherID],D.[FabMoProcessID],D.[FabMoOperationID],A.DTSID,
            A.OriginalCompletionOrderID,A.InspectionID,A.ItemID,H.BatchNumber,
            D.TaskNo,H.MoNo+'-'+convert(varchar(20),H.SplitSequence) as OrderNum,
            E.Code as ItemCode,E.Name as ItemName,E.Specification as ItemSpecification,E.Lot,
            (Select Name from [SYS_Parameters] where H.UnitID = ParameterID) as ManufacturingUnit,
            (Select Name from [SYS_Parameters] where G.UnitID = ParameterID) as AuxiliaryUnit,G.UnitRate,
             D.DispatchQuantity-A.FinProQuantity as FinProQtyAble,                    
            A.FinProQuantity,A.ScrappedQuantity,A.DifferenceQuantity,A.RepairQuantity,A.InspectionQuantity,
            convert(varchar(20),cast(A.LaborHour/3600 as int))+':'+right('00'+convert(varchar(20),A.LaborHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.LaborHour%3600%60),2) as LaborHour,
            convert(varchar(20),cast(A.UnLaborHour/3600 as int))+':'+right('00'+convert(varchar(20),A.UnLaborHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.UnLaborHour%3600%60),2) as UnLaborHour,
            convert(varchar(20),cast(A.MachineHour/3600 as int))+':'+right('00'+convert(varchar(20),A.MachineHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.MachineHour%3600%60),2) as MachineHour,
            convert(varchar(20),cast(A.UnMachineHour/3600 as int))+':'+right('00'+convert(varchar(20),A.UnMachineHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.UnMachineHour%3600%60),2) as UnMachineHour,
            A.Status,A.Comments,
            (select [ResourceReport] from [SYS_WorkCenter] where [WorkCenterID] =G.[WorkCenterID]) as IsResourceReport,
            (CASE when (select Count(*) from SFC_FabMoRelationship where FabMoProcessID=G.FabMoProcessID and IfLastProcess=1 and Status='{0}0201213000001') >0 THEN 1 ELSE 0 END) as IfLastProcess,
            (Select Code from [SYS_Parameters] where E.LotMethod = ParameterID) as LotMethod,
            (CASE when A.[NextOperationID] is null THEN A.[NextProcessID] ELSE A.[NextOperationID] END) as NextID,
            (CASE when A.[NextOperationID] is null THEN (Select Code+'-'+Name from [SYS_Parameters] where A.[NextProcessID] = [ParameterID]) ELSE (Select Code+'-'+Name from [SYS_Parameters] where A.[NextOperationID] = [ParameterID]) END) as NextCode,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@" from [SFC_CompletionOrder] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SFC_TaskDispatch] D on A.[TaskDispatchID] = D.[TaskDispatchID]
            left join [SYS_Items] E on A.[ItemID] = E.[ItemID]
            left join [SFC_FabricatedMother] H on A.[FabricatedMotherID] = H.[FabricatedMotherID]
            left join [SFC_FabMoProcess] G on A.[FabMoProcessID] = G.[FabMoProcessID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003'  and A.[Type]= '{0}02012130000A0'", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@TaskDispatchID",SqlDbType.VarChar),
                new SqlParameter("@FinishNo",SqlDbType.VarChar),
                new SqlParameter("@Date",SqlDbType.VarChar),
                new SqlParameter("@WorkCenter",SqlDbType.VarChar),
                new SqlParameter("@Process",SqlDbType.VarChar),
                new SqlParameter("@FabricatedMother",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;
            parameters[5].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@TaskDispatchID",SqlDbType.VarChar),
                new SqlParameter("@FinishNo",SqlDbType.VarChar),
                new SqlParameter("@Date",SqlDbType.VarChar),
                new SqlParameter("@WorkCenter",SqlDbType.VarChar),
                new SqlParameter("@Process",SqlDbType.VarChar),
                new SqlParameter("@FabricatedMother",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;


            if (!string.IsNullOrWhiteSpace(TaskDispatchID))
            {
                sql = sql + String.Format(@" and A.[TaskDispatchID] = @TaskDispatchID ");
                parameters[0].Value = TaskDispatchID;
                Parcount[0].Value = TaskDispatchID;
            }

            if (!string.IsNullOrWhiteSpace(FinishNo))
            {
                FinishNo = "%" + FinishNo + "%";
                sql = sql + String.Format(@" and A.[CompletionNo] like @FinishNo ");
                parameters[1].Value = FinishNo;
                Parcount[1].Value = FinishNo;
            }

            if (!string.IsNullOrWhiteSpace(Date))
            {
                sql = sql + String.Format(@" and A.[Date] = @Date ");
                parameters[2].Value = Date;
                Parcount[2].Value = Date;
            }

            if (!string.IsNullOrWhiteSpace(WorkCenter))
            {
                WorkCenter = "%" + WorkCenter + "%";
                sql = sql + String.Format(@" and G.[WorkCenterID] in (select [WorkCenterID] from [SYS_WorkCenter] where [Code] like @WorkCenter and [Status]='{0}0201213000001' and [SystemID] ='{0}') ", Framework.SystemID);
                parameters[3].Value = WorkCenter;
                Parcount[3].Value = WorkCenter;
            }

            if (!string.IsNullOrWhiteSpace(Process))
            {
                Process = "%" + Process + "%";
                sql = sql + String.Format(@" and A.[ProcessID] in (select [ParameterID] from [SYS_Parameters] where [Code] like @Process and [IsEnable]=1 and [SystemID] ='{0}') ", Framework.SystemID);
                parameters[4].Value = Process;
                Parcount[4].Value = Process;
            }

            if (!string.IsNullOrWhiteSpace(FabricatedMother))
            {
                FabricatedMother = "%" + FabricatedMother + "%";
                sql = sql + String.Format(@" and H.[MoNo] like @FabricatedMother ");
                parameters[5].Value = FabricatedMother;
                Parcount[5].Value = FabricatedMother;
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql += string.Format(@" and A.[Status] in ('{0}')", Status.Replace(",", "','"));
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Status],A.[CompletionNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 完工单的导出
        /// SAM 2017年7月20日11:56:35
        /// </summary>
        /// <param name="FinishNo"></param>
        /// <param name="EndWorkD"></param>
        /// <returns></returns>
        public static DataTable Sfc00007Export(string FinishNo, string Date, string WorkCenterID, string ProcessID, string FabricatedMotherID, string Status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status],A.[CompletionNo]),
            A.CompletionNo,A.Date,
            D.TaskNo,D.TaskNo+'-'+convert(varchar(20),H.SplitSequence) as OrderNum,
            E.Code as ItemCode,E.Name as ItemName,E.Specification as ItemSpecification,
            (Select Name from [SYS_Parameters] where H.UnitID = ParameterID) as ManufacturingUnit,
            (Select Name from [SYS_Parameters] where G.UnitID = ParameterID) as AuxiliaryUnit,G.UnitRate,
             D.DispatchQuantity-A.FinProQuantity as FinProQtyAble,
            A.FinProQuantity,A.ScrappedQuantity,A.DifferenceQuantity,A.RepairQuantity,                 
            convert(varchar(20),cast(A.LaborHour/3600 as int))+':'+right('00'+convert(varchar(20),A.LaborHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.LaborHour%3600%60),2) as LaborHour,
            convert(varchar(20),cast(A.UnLaborHour/3600 as int))+':'+right('00'+convert(varchar(20),A.UnLaborHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.UnLaborHour%3600%60),2) as UnLaborHour,
            convert(varchar(20),cast(A.MachineHour/3600 as int))+':'+right('00'+convert(varchar(20),A.MachineHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.MachineHour%3600%60),2) as MachineHour,
            convert(varchar(20),cast(A.UnMachineHour/3600 as int))+':'+right('00'+convert(varchar(20),A.UnMachineHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.UnMachineHour%3600%60),2) as UnMachineHour,
            A.Comments,F.Name as Status,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@" from [SFC_CompletionOrder] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SFC_TaskDispatch] D on A.[TaskDispatchID] =D.[TaskDispatchID]
            left join [SYS_Items] E on A.[ItemID] =E.[ItemID]
            left join [SFC_FabricatedMother] H on D.[FabricatedMotherID] =H.[FabricatedMotherID]
            left join [SFC_FabMoProcess] G on D.[FabMoProcessID] =G.[FabMoProcessID]
            left join [SYS_Parameters] F on A.[Status] =F.[ParameterID]
            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003'  and A.Type = '{0}02012130000A0'", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@FinishNo",SqlDbType.VarChar),
                new SqlParameter("@Date",SqlDbType.VarChar),
                new SqlParameter("@WorkCenterID",SqlDbType.VarChar),
                new SqlParameter("@ProcessID",SqlDbType.VarChar),
                new SqlParameter("@FabricatedMotherID",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(FinishNo))
            {
                FinishNo = "%" + FinishNo + "%";
                sql = sql + String.Format(@" and A.[CompletionNo] like @FinishNo ");
                parameters[0].Value = FinishNo;
            }

            if (!string.IsNullOrWhiteSpace(Date))
            {
                sql = sql + String.Format(@" and A.[Date] = @Date ");
                parameters[1].Value = Date;
            }

            if (!string.IsNullOrWhiteSpace(WorkCenterID))
            {
                sql = sql + String.Format(@" and G.[WorkCenterID] = @WorkCenterID ");
                parameters[2].Value = WorkCenterID;
            }


            if (!string.IsNullOrWhiteSpace(ProcessID))
            {
                sql = sql + String.Format(@" and A.[ProcessID] = @ProcessID ");
                parameters[3].Value = ProcessID;
            }


            if (!string.IsNullOrWhiteSpace(FabricatedMotherID))
            {
                sql = sql + String.Format(@" and A.[FabricatedMotherID] = @FabricatedMotherID ");
                parameters[4].Value = FabricatedMotherID;
            }


            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql += string.Format(@" and A.[Status] in ('{0}')", Status.Replace(",", "','"));
            }

            string orderby = "order by A.[Status],A.[CompletionNo] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text, parameters);

            return dt;
        }


        /// <summary>
        /// 查询完工调整作业表
        /// SAM 2017年7月20日16:19:22
        /// </summary>
        /// <param name="AdjustCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> SFC00008GetList(string AdjustCode, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.CompletionOrderID,A.CompletionNo,A.Date,A.Sequence,A.Type,
            A.[TaskDispatchID],A.[FabricatedMotherID],D.[FabMoProcessID],D.[FabMoOperationID],A.DTSID,
            A.OriginalCompletionOrderID,A.InspectionID,A.ItemID,I.CompletionNo as OriginalCompletionNo,
            D.TaskNo,D.TaskNo+'-'+convert(varchar(20),H.SplitSequence) as OrderNum,A.ReasonID,H.BatchNumber,
            F.Code as ReasonCode,F.Name as ReasonName,
            E.Code as ItemCode,E.Name as ItemName,E.Specification as ItemSpecification,E.Lot,
            (Select Name from [SYS_Parameters] where H.UnitID = ParameterID) as ManufacturingUnit,
            (Select Name from [SYS_Parameters] where G.UnitID = ParameterID) as AuxiliaryUnit,G.UnitRate,
             D.DispatchQuantity-A.FinProQuantity as FinProQtyAble,                    
            A.FinProQuantity,A.ScrappedQuantity,A.DifferenceQuantity,A.RepairQuantity,A.InspectionQuantity,A.LaborHour,A.UnLaborHour,A.MachineHour,A.UnMachineHour,
            (select [ResourceReport] from [SYS_WorkCenter] where [WorkCenterID] =G.[WorkCenterID]) as IsResourceReport,
            (CASE when (select Count(*) from SFC_FabMoRelationship where FabMoProcessID=G.FabMoProcessID and IfLastProcess=1 and Status='{0}0201213000001') >0 THEN 1 ELSE 0 END) as IfLastProcess,
            (Select Code from [SYS_Parameters] where E.LotMethod = ParameterID) as LotMethod,       
            A.Status,A.Comments,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@" from [SFC_CompletionOrder] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SFC_TaskDispatch] D on A.[TaskDispatchID] =D.[TaskDispatchID]
            left join [SYS_Items] E on A.[ItemID] =E.[ItemID]
            left join [SFC_FabricatedMother] H on D.[FabricatedMotherID] =H.[FabricatedMotherID]
            left join [SFC_FabMoProcess] G on D.[FabMoProcessID] =G.[FabMoProcessID]
            left join [SYS_Parameters] F on A.[ReasonID] =F.[ParameterID]
            left join [SFC_CompletionOrder] I on A.[OriginalCompletionOrderID] =I.[CompletionOrderID]
            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003'  and A.Type = '{0}02012130000A1'", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@AdjustCode",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@AdjustCode",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(AdjustCode))
            {
                AdjustCode = "%" + AdjustCode.Trim() + "%";
                sql = sql + String.Format(@" and A.[CompletionNo] collate Chinese_PRC_CI_AS like @AdjustCode ");
                parameters[0].Value = AdjustCode;
                Parcount[0].Value = AdjustCode;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Status],A.[CompletionNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


        //cast(字段 as varchar)real
        /// <summary>
        /// 完工调整单的导出
        /// SAM 2017年8月1日14:55:17
        /// </summary>
        /// <param name="AdjustCode"></param>
        /// <returns></returns>
        public static DataTable Sfc00008Export(string AdjustCode)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status],A.[CompletionNo]),
            A.CompletionNo,CONVERT(varchar(10), A.Date, 20),I.CompletionNo as OriginalCompletionNo,
            D.TaskNo,H.MoNo+'-'+cast(H.SplitSequence as varchar) as OrderNum,
            E.Code as ItemCode,E.Name as ItemName,E.Specification as ItemSpecification,
            (Select Name from [SYS_Parameters] where H.UnitID = ParameterID) as ManufacturingUnit,
            (Select Name from [SYS_Parameters] where G.UnitID = ParameterID) as AuxiliaryUnit,
            cast(G.UnitRate as real),
            cast(D.DispatchQuantity-A.FinProQuantity as real),
            cast(A.FinProQuantity as real), cast(A.ScrappedQuantity as real),
            cast(A.DifferenceQuantity as real), cast(A.RepairQuantity as real),
            cast(A.LaborHour as varchar),
            cast(A.UnLaborHour as varchar),
            cast(A.MachineHour as varchar),
            cast(A.UnMachineHour as varchar),
            A.Comments,F.Name as Status,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@" from [SFC_CompletionOrder] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SFC_TaskDispatch] D on A.[TaskDispatchID] =D.[TaskDispatchID]
            left join [SYS_Items] E on A.[ItemID] =E.[ItemID]
            left join [SFC_FabricatedMother] H on D.[FabricatedMotherID] =H.[FabricatedMotherID]
            left join [SFC_FabMoProcess] G on D.[FabMoProcessID] =G.[FabMoProcessID]
            left join [SYS_Parameters] F on A.[Status] =F.[ParameterID]
            left join [SFC_CompletionOrder] I on A.[OriginalCompletionOrderID] =I.[CompletionOrderID]
            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003'  and A.Type = '{0}02012130000A1'", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@AdjustCode",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(AdjustCode))
            {
                AdjustCode = "%" + AdjustCode + "%";
                sql = sql + String.Format(@" and A.[CompletionNo] like @AdjustCode ");
                parameters[0].Value = AdjustCode;
            }

            string orderby = "order by A.[Status],A.[CompletionNo] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text, parameters);

            return dt;
        }

        /// <summary>
        /// 原完工单的开窗查询
        /// SAM 2017年7月20日16:29:42
        /// </summary>
        /// <param name="OldCompletedNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00008GetOldCompletedNo(string OldCompletedNo, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.CompletionOrderID,A.CompletionNo,A.Date,A.Sequence,A.Type,
            A.[TaskDispatchID],A.[FabricatedMotherID],D.[FabMoProcessID],D.[FabMoOperationID],
            A.OriginalCompletionOrderID,A.InspectionID,A.ItemID,I.CompletionNo as OriginalCompletionNo,
            D.TaskNo,D.TaskNo+'-'+convert(varchar(20),D.MoSequence) as OrderNum,A.ReasonID,
            F.Code as ReasonCode,F.Name as ReasonName,
            E.Code as ItemCode,E.Name as ItemName,E.Specification as ItemSpecification,E.Lot,
            (Select Name from [SYS_Parameters] where H.UnitID = ParameterID) as ManufacturingUnit,
            (Select Name from [SYS_Parameters] where G.UnitID = ParameterID) as AuxiliaryUnit,G.UnitRate,
             D.DispatchQuantity-A.FinProQuantity as FinProQtyAble,                    
            A.FinProQuantity,A.ScrappedQuantity,A.DifferenceQuantity,A.RepairQuantity,A.InspectionQuantity,          
            convert(varchar(20),cast(A.LaborHour/3600 as int))+':'+right('00'+convert(varchar(20),A.LaborHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.LaborHour%3600%60),2) as LaborHour,
            convert(varchar(20),cast(A.UnLaborHour/3600 as int))+':'+right('00'+convert(varchar(20),A.UnLaborHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.UnLaborHour%3600%60),2) as UnLaborHour,
            convert(varchar(20),cast(A.MachineHour/3600 as int))+':'+right('00'+convert(varchar(20),A.MachineHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.MachineHour%3600%60),2) as MachineHour,
            convert(varchar(20),cast(A.UnMachineHour/3600 as int))+':'+right('00'+convert(varchar(20),A.UnMachineHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.UnMachineHour%3600%60),2) as UnMachineHour,
            A.Status,A.Comments,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SFC_CompletionOrder] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SFC_TaskDispatch] D on A.[TaskDispatchID] =D.[TaskDispatchID]
            left join [SYS_Items] E on A.[ItemID] =E.[ItemID]
            left join [SFC_FabricatedMother] H on D.[FabricatedMotherID] =H.[FabricatedMotherID]
            left join [SFC_FabMoProcess] G on D.[FabMoProcessID] =G.[FabMoProcessID]
            left join [SYS_Parameters] F on A.[ReasonID] =F.[ParameterID]
            left join [SFC_CompletionOrder] I on A.[OriginalCompletionOrderID] =I.[CompletionOrderID]
            where A.[SystemID]='{0}' and A.Status = '{0}020121300002A' and A.Type = '{0}02012130000A0'", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@OldCompletedNo",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@OldCompletedNo",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(OldCompletedNo))
            {
                OldCompletedNo = "%" + OldCompletedNo + "%";
                sql = sql + String.Format(@" and A.[CompletionNo] like @OldCompletedNo ");
                parameters[0].Value = OldCompletedNo;
                Parcount[0].Value = OldCompletedNo;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Status],A.[CompletionNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 制程完工异常分析-主列表
        /// SAM 2017年7月22日22:46:16
        /// </summary>
        /// <param name="StartProcessCode"></param>
        /// <param name="EndProcessCode"></param>
        /// <param name="StartItemCode"></param>
        /// <param name="EndItemCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00013GetList(string StartProcessCode, string EndProcessCode, string StartItemCode, string EndItemCode, string StartDate, string EndDate, int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select distinct A.ItemID,A.ProcessID,
            B.Code as ItemCode,B.Name as ItemName,B.Specification as ItemSpecification,
            C.Code as ProcessCode,C.Name as ProcessName,
            (select SUM(DifferenceQuantity) from [SFC_CompletionOrder] where A.ItemID = ItemID and A.ProcessID=ProcessID and [Status] <> '{0}0201213000003')+
            (select SUM(ScrappedQuantity) from [SFC_CompletionOrder] where A.ItemID = ItemID and A.ProcessID=ProcessID and [Status] <> '{0}0201213000003')+
            (select SUM(RepairQuantity) from [SFC_CompletionOrder] where A.ItemID = ItemID and A.ProcessID=ProcessID and [Status] <> '{0}0201213000003') as Quantity
            ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_CompletionOrder] A               
                 left join [SYS_Items] B on A.[ItemID] = B.[ItemID]
                 left join [SYS_Parameters] C on A.[ProcessID] =C.[ParameterID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartProcessCode",SqlDbType.VarChar),
                new SqlParameter("@EndProcessCode",SqlDbType.VarChar),
                new SqlParameter("@StartItemCode",SqlDbType.VarChar),
                new SqlParameter("@EndItemCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;
            parameters[5].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@StartProcessCode",SqlDbType.VarChar),
                new SqlParameter("@EndProcessCode",SqlDbType.VarChar),
                new SqlParameter("@StartItemCode",SqlDbType.VarChar),
                new SqlParameter("@EndItemCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartProcessCode))
            {
                sql = sql + String.Format(@" and C.[Code] >= @StartProcessCode ");
                parameters[0].Value = StartProcessCode;
                Parcount[0].Value = StartProcessCode;
            }

            if (!string.IsNullOrWhiteSpace(EndProcessCode))
            {
                sql = sql + String.Format(@" and C.[Code] < @EndProcessCode ");
                parameters[1].Value = EndProcessCode;
                Parcount[1].Value = EndProcessCode;
            }

            if (!string.IsNullOrWhiteSpace(StartItemCode))
            {
                sql = sql + String.Format(@" and B.[Code] >= @StartItemCode ");
                parameters[2].Value = StartItemCode;
                Parcount[2].Value = StartItemCode;
            }

            if (!string.IsNullOrWhiteSpace(EndItemCode))
            {
                sql = sql + String.Format(@" and B.[Code] < @EndItemCode ");
                parameters[3].Value = EndItemCode;
                Parcount[3].Value = EndItemCode;
            }

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + String.Format(@" and A.[Date] >= @StartDate ");
                parameters[4].Value = StartDate;
                Parcount[4].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + String.Format(@" and A.[Date] <= @EndDate ");
                parameters[5].Value = EndDate;
                Parcount[5].Value = EndDate;
            }

            DataTable CountList = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text, parameters);

            count = CountList.Rows.Count;

            //string orderby = "B.[Code],C.[Code] ";

            //DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            string orderby = "[ItemCode],[ProcessCode] ";

            DataTable dt = UniversalService.GetTableDistinct(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }



        /// <summary>
        /// 根据制品和制程获取正常的完工单
        /// SAM 2017年7月22日22:52:24
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00013GetCompletionOrder(string ItemID, string ProcessID)
        {
            string sql = string.Format(
              @"select A.* from [SFC_CompletionOrder] A 
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and A.[ItemID] ='{1}' and A.[ProcessID]='{2}'", Framework.SystemID, ItemID, ProcessID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 制程工时分析-主列表
        /// SAM 2017年7月23日00:28:47
        /// </summary>
        /// <param name="StartProcessCode"></param>
        /// <param name="EndProcessCode"></param>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartItemCode"></param>
        /// <param name="EndItemCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00012GetList(string StartProcessCode, string EndProcessCode,
            string StartWorkCenterCode, string EndWorkCenterCode,
            string StartFabMoCode, string EndFabMoCode,
            string StartItemCode, string EndItemCode,
            string StartDate, string EndDate,
            int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select  A.CompletionOrderID,
            E.Code as WorkCenterCode,E.Name as WorkCenterName,
            F.Code as ProcessCode,F.Name as ProcessName,
            C.[MoNo],C.[SplitSequence],
            (select [Name] From [SYS_Parameters] where E.InoutMark = ParameterID) as InoutMark,
            (CASE WHEN E.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = E.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = E.DepartmentID) END) as DeptCode,
            (CASE WHEN E.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = E.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = E.DepartmentID) END) as DeptName,
             convert(varchar(20),cast(A.LaborHour/3600 as int))+':'+right('00'+convert(varchar(20),A.LaborHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.LaborHour%3600%60),2) as LaborHourStr,
            convert(varchar(20),cast(A.UnLaborHour/3600 as int))+':'+right('00'+convert(varchar(20),A.UnLaborHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.UnLaborHour%3600%60),2) as UnLaborHourStr,
            convert(varchar(20),cast(A.MachineHour/3600 as int))+':'+right('00'+convert(varchar(20),A.MachineHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.MachineHour%3600%60),2) as MachineHourStr,
            convert(varchar(20),cast(A.UnMachineHour/3600 as int))+':'+right('00'+convert(varchar(20),A.UnMachineHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.UnMachineHour%3600%60),2) as UnMachineHourStr,
            (select [Name] From [SYS_Parameters] where D.Status = ParameterID) as Status ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_CompletionOrder] A               
                left join [SYS_Items] B on A.[ItemID] = B.[ItemID]
                left join [SFC_FabricatedMother] C on A.[FabricatedMotherID] =C.[FabricatedMotherID]
                left join [SFC_FabMoProcess] D on A.[FabMoProcessID] =D.[FabMoProcessID]
                left join [SYS_WorkCenter] E on D.[WorkCenterID] =E.[WorkCenterID]
                left join [SYS_Parameters] F on A.[ProcessID] =F.[ParameterID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartProcessCode",SqlDbType.VarChar),
                new SqlParameter("@EndProcessCode",SqlDbType.VarChar),
                new SqlParameter("@StartItemCode",SqlDbType.VarChar),
                new SqlParameter("@EndItemCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@EndFabMoCode",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;
            parameters[5].Value = DBNull.Value;
            parameters[6].Value = DBNull.Value;
            parameters[7].Value = DBNull.Value;
            parameters[8].Value = DBNull.Value;
            parameters[9].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@StartProcessCode",SqlDbType.VarChar),
                new SqlParameter("@EndProcessCode",SqlDbType.VarChar),
                new SqlParameter("@StartItemCode",SqlDbType.VarChar),
                new SqlParameter("@EndItemCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@EndFabMoCode",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;
            Parcount[6].Value = DBNull.Value;
            Parcount[7].Value = DBNull.Value;
            Parcount[8].Value = DBNull.Value;
            Parcount[9].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartProcessCode))
            {
                sql = sql + String.Format(@" and F.[Code] >= @StartProcessCode ");
                parameters[0].Value = StartProcessCode;
                Parcount[0].Value = StartProcessCode;
            }

            if (!string.IsNullOrWhiteSpace(EndProcessCode))
            {
                sql = sql + String.Format(@" and F.[Code] < @EndProcessCode ");
                parameters[1].Value = EndProcessCode;
                Parcount[1].Value = EndProcessCode;
            }

            if (!string.IsNullOrWhiteSpace(StartItemCode))
            {
                sql = sql + String.Format(@" and B.[Code] >= @StartItemCode ");
                parameters[2].Value = StartItemCode;
                Parcount[2].Value = StartItemCode;
            }

            if (!string.IsNullOrWhiteSpace(EndItemCode))
            {
                sql = sql + String.Format(@" and B.[Code] < @EndItemCode ");
                parameters[3].Value = EndItemCode;
                Parcount[3].Value = EndItemCode;
            }

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + String.Format(@" and A.[Date] >= @StartDate ");
                parameters[4].Value = StartDate;
                Parcount[4].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + String.Format(@" and A.[Date] < @EndDate ");
                parameters[5].Value = EndDate;
                Parcount[5].Value = EndDate;
            }


            if (!string.IsNullOrWhiteSpace(StartWorkCenterCode))
            {
                sql = sql + String.Format(@" and E.[Code] >= @StartWorkCenterCode ");
                parameters[6].Value = StartWorkCenterCode;
                Parcount[6].Value = StartWorkCenterCode;
            }

            if (!string.IsNullOrWhiteSpace(EndWorkCenterCode))
            {
                sql = sql + String.Format(@" and E.[Code] < @EndWorkCenterCode ");
                parameters[7].Value = EndWorkCenterCode;
                Parcount[7].Value = EndWorkCenterCode;
            }

            if (!string.IsNullOrWhiteSpace(StartFabMoCode))
            {
                sql = sql + String.Format(@" and C.[MoNo] >= @StartFabMoCode ");
                parameters[8].Value = StartFabMoCode;
                Parcount[8].Value = StartFabMoCode;
            }

            if (!string.IsNullOrWhiteSpace(EndFabMoCode))
            {
                sql = sql + String.Format(@" and C.[MoNo] < @EndFabMoCode ");
                parameters[9].Value = EndFabMoCode;
                Parcount[9].Value = EndFabMoCode;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "E.[Code],F.[Code],C.[MoNo],C.[SplitSequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 制程工时分析-工序列表
        /// SAM 2017年7月23日01:52:26
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00012GetOperationList(string FabMoProcessID, int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select  A.CompletionOrderID,
            B.Sequence,C.Code as OperationCode,C.Name as OperationName,       
            convert(varchar(20),cast(A.LaborHour/3600 as int))+':'+right('00'+convert(varchar(20),A.LaborHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.LaborHour%3600%60),2) as LaborHourStr,
            convert(varchar(20),cast(A.UnLaborHour/3600 as int))+':'+right('00'+convert(varchar(20),A.UnLaborHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.UnLaborHour%3600%60),2) as UnLaborHourStr,
            convert(varchar(20),cast(A.MachineHour/3600 as int))+':'+right('00'+convert(varchar(20),A.MachineHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.MachineHour%3600%60),2) as MachineHourStr,
            convert(varchar(20),cast(A.UnMachineHour/3600 as int))+':'+right('00'+convert(varchar(20),A.UnMachineHour%3600/60),2)+':'+right('00'+convert(varchar(20),A.UnMachineHour%3600%60),2) as UnMachineHourStr,
            (select [Name] From [SYS_Parameters] where B.Status = ParameterID) as Status ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_CompletionOrder] A         
                left join [SFC_FabMoOperation] B on A.[FabMoOperationID] =B.[FabMoOperationID]
                left join [SYS_Parameters] C on A.[OperationID] =C.[ParameterID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and A.[FabMoProcessID]='{0}' ", Framework.SystemID, FabMoProcessID);

            count = UniversalService.getCount(sql, null);

            string orderby = "C.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据任务单流水号获取完工单
        /// Tom 2017年7月26日16点13分
        /// </summary>
        /// <param name="taskDispatchID"></param>
        /// <returns></returns>
        public static SFC_CompletionOrder GetByTaskDispatchID(string taskDispatchID)
        {
            string sql = string.Format(@"select * from SFC_CompletionOrder where TaskDispatchID = @TaskDispatchID");
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@TaskDispatchID", taskDispatchID));
            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameterList.ToArray());

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据任务单流水号获取最后一笔完工单
        /// SAM 2017年9月12日17:55:13
        /// </summary>
        /// <param name="taskDispatchID"></param>
        /// <returns></returns>
        public static SFC_CompletionOrder GetLastByTaskDispatch(string taskDispatchID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_CompletionOrder] where TaskDispatchID = @TaskDispatchID 
            and [Status] <> '{0}0201213000003' order By Date desc,CreateTime desc", Framework.SystemID);

            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@TaskDispatchID", taskDispatchID));
            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameterList.ToArray());

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据任务单判断是否存在未确认的完工单
        /// SAM 2017年9月21日17:00:15
        /// </summary>
        /// <param name="TaskDispatchID"></param>
        /// <returns></returns>
        public static bool Sfc00006CheckCompletion(string TaskDispatchID)
        {
            string sql = string.Format(@"select  * from [SFC_CompletionOrder] where [TaskDispatchID] = '{1}' 
            and ([Status] = '{0}0201213000028' or [Status]='{0}0201213000029') ", Framework.SystemID, TaskDispatchID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// SFC00023列表获取
        /// </summary>
        /// <param name="BatchNo"></param>
        /// <param name="ItemStar"></param>
        /// <param name="ItemEnd"></param>
        /// <param name="DateStar"></param>
        /// <param name="DateEnd"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> sfc00023GetList(string BatchNo, string ItemStar, string ItemEnd, string DateStar, string DateEnd, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.CompletionOrderID,A.Date,B.Code as ItemCode,B.Name as ItemName,B.Specification as ItemSpecification,
                            A.CompletionNo + '-' + cast (A.Sequence as varchar) as CompletionNo,D.MoNo + '-' + cast (D.SplitSequence as varchar) as MoNo,C.BatchNo,C.Quantity");
            string sql = string.Format(@" from SFC_CompletionOrder A
                        left join [SYS_Items] B on A.ItemID = B.ItemID
                        left join [SFC_BatchAttribute] C on C.CompletionOrderID = A.CompletionOrderID
                        left join [SFC_FabricatedMother] D on D.FabricatedMotherID = A.FabricatedMotherID
                        where A.[SystemID]='{0}' and A.[Status]='{0}020121300002A' and C.BatchAttributeID is not null", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@BatchNo",SqlDbType.VarChar),
                new SqlParameter("@ItemStar",SqlDbType.VarChar),
                new SqlParameter("@ItemEnd",SqlDbType.VarChar),
                new SqlParameter("@DateStar",SqlDbType.VarChar),
                new SqlParameter("@DateEnd",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;

            SqlParameter[] parcount = new SqlParameter[] {
                new SqlParameter("@BatchNo",SqlDbType.VarChar),
                new SqlParameter("@ItemStar",SqlDbType.VarChar),
                new SqlParameter("@ItemEnd",SqlDbType.VarChar),
                new SqlParameter("@DateStar",SqlDbType.VarChar),
                new SqlParameter("@DateEnd",SqlDbType.VarChar),
            };
            parcount[0].Value = DBNull.Value;
            parcount[1].Value = DBNull.Value;
            parcount[2].Value = DBNull.Value;
            parcount[3].Value = DBNull.Value;
            parcount[4].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(BatchNo))
            {
                BatchNo = "%" + BatchNo + "%";
                sql = sql + string.Format(@" and C.BatchNo collate Chinese_PRC_CI_AS like @BatchNo");
                parameters[0].Value = BatchNo;
                parcount[0].Value = BatchNo;
            }
            if (!string.IsNullOrWhiteSpace(ItemStar))
            {
                sql = sql + string.Format(@" and B.Code >= @ItemStar");
                parameters[1].Value = ItemStar;
                parcount[1].Value = ItemStar;
            }
            if (!string.IsNullOrWhiteSpace(ItemEnd))
            {
                sql = sql + string.Format(@" and B.Code <= @ItemEnd");
                parameters[2].Value = ItemEnd;
                parcount[2].Value = ItemEnd;
            }
            if (!string.IsNullOrWhiteSpace(DateStar))
            {
                sql = sql + string.Format(@" and A.Date >= @DateStar");
                parameters[3].Value = DateStar;
                parcount[3].Value = DateStar;
            }
            if (!string.IsNullOrWhiteSpace(DateEnd))
            {
                sql = sql + string.Format(@" and A.Date <= @DateEnd");
                parameters[4].Value = DateEnd;
                parameters[4].Value = DateEnd;
            }
            count = UniversalService.getCount(sql, parcount);

            string orderby = " A.Date,B.Code,CompletionNo,MoNo,C.BatchNo";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);
            return ToHashtableList(dt);
        }

        /// <summary>
        /// 匯總一個製令製程的完工單上的返修數量
        /// Sam 2017年9月28日15:58:48
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static decimal GetFabMoProcessRework(string FabMoProcessID)
        {
            string sql = string.Format(
              @"select ISNULL(SUM(ISNULL(RepairQuantity,0)),0) from [SFC_CompletionOrder] 
                where [SystemID] = '{0}' and [Status] <> '{0}0201213000003' 
                and [FabMoProcessID]='{1}'", Framework.SystemID, FabMoProcessID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            return decimal.Parse(dt.Rows[0][0].ToString());
        }

        /// <summary>
        /// 检测任务单有没有生成过完工单
        /// SAM 2017年10月30日16:22:49
        /// </summary>
        /// <param name="TaskDispatchID"></param>
        /// <returns></returns>
        public static bool Sfc06CheckOrder(string TaskDispatchID)
        {
            string sql = string.Format(@"select  * from [SFC_CompletionOrder] where [TaskDispatchID] = '{1}' 
            and [Status] <> '{0}020121300003' ", Framework.SystemID, TaskDispatchID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
        
        /// <summary>
         /// 制品不良统计分析-原因统计页签列表
         /// SAM 2017年10月10日16:01:35
         /// </summary>
         /// <param name="Token"></param>
         /// <param name="StartItemCode">起始料号</param>
         /// <param name="EndItemCode">结束料号</param>
         /// <param name="StartDate">起始日期</param>
         /// <param name="EndDate">结束日期</param>
         /// <param name="page"></param>
         /// <param name="rows"></param>
         /// <returns></returns>
        public static IList<Hashtable> Sfc00022GetReasonList(string StartItemCode, string EndItemCode,
            string StartDate, string EndDate, int page, int rows, ref int count)
        {
            string select = string.Format(@"select distinct A.[ItemID],B.Code,B.Name,B.Specification,B.Type,C.Name as TypeName ");

            string sql = string.Format(@" from [SFC_CompletionOrder] A
                        left join [SYS_Items] B on A.[ItemID] = B.[ItemID]
                        left join [SYS_Parameters] C on B.[Type] = C.[ParameterID]
                        where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(@"StartItemCode",SqlDbType.VarChar),
                new SqlParameter(@"EndItemCode",SqlDbType.VarChar),
                new SqlParameter(@"StartDate",SqlDbType.VarChar),
                new SqlParameter(@"EndDate",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[] {
                new SqlParameter(@"StartItemCode",SqlDbType.VarChar),
                new SqlParameter(@"EndItemCode",SqlDbType.VarChar),
                new SqlParameter(@"StartDate",SqlDbType.VarChar),
                new SqlParameter(@"EndDate",SqlDbType.VarChar),
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;


            if (!string.IsNullOrWhiteSpace(StartItemCode))
            {
                sql = sql + string.Format(@" and B.Code >= @StartItemCode");
                parameters[0].Value = StartItemCode;
                Parcount[0].Value = StartItemCode;
            }
            if (!string.IsNullOrWhiteSpace(EndItemCode))
            {
                sql = sql + string.Format(@" and B.Code <= @EndItemCode");
                parameters[1].Value = EndItemCode;
                Parcount[1].Value = EndItemCode;
            }
            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + string.Format(@" and A.[Date] >= @StartDate");
                parameters[2].Value = StartDate;
                Parcount[2].Value = StartDate;
            }
            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + string.Format(@" and A.[Date] <=@EndDate");
                parameters[3].Value = EndDate;
                Parcount[3].Value = EndDate;
            }

            string orderby = " Code ";

            DataTable countList = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text, Parcount);

            count = countList == null ? 0 : countList.Rows.Count;

            DataTable dt = UniversalService.GetTableDistinct(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }
    }
}

