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
    public class SFC_FabMoOperationService : SuperModel<SFC_FabMoOperation>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年7月13日23:55:56
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SFC_FabMoOperation Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_FabMoOperation]([FabMoOperationID],[FabricatedMotherID],
                [FabMoProcessID],[OperationID],[Sequence],[UnitID],
                [UnitRate],[StartDate],[FinishDate],
                [Quantity],[FinProQuantity],[OutProQuantity],
                [ScrappedQuantity],[DifferenceQuantity],[PreProQuantity],
                [AssignQuantity],[ResourceReport],[StandardTime],[PrepareTime],
                [TaskNo],[Status],[SourceID],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@FabMoOperationID,@FabricatedMotherID,
                @FabMoProcessID,@OperationID,@Sequence,
                @UnitID,@UnitRate,@StartDate,
                @FinishDate,@Quantity,@FinProQuantity,
                @OutProQuantity,@ScrappedQuantity,@DifferenceQuantity,
                @PreProQuantity,@AssignQuantity,@ResourceReport,
                @StandardTime,@PrepareTime,@TaskNo,
                @Status,@SourceID,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@FabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@FabricatedMotherID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@OperationID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.VarChar),
                    new SqlParameter("@UnitID",SqlDbType.VarChar),
                    new SqlParameter("@UnitRate",SqlDbType.Decimal),
                    new SqlParameter("@StartDate",SqlDbType.DateTime),
                    new SqlParameter("@FinishDate",SqlDbType.DateTime),
                    new SqlParameter("@Quantity",SqlDbType.Decimal),
                    new SqlParameter("@FinProQuantity",SqlDbType.Decimal),
                    new SqlParameter("@OutProQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ScrappedQuantity",SqlDbType.Decimal),
                    new SqlParameter("@DifferenceQuantity",SqlDbType.Decimal),
                    new SqlParameter("@PreProQuantity",SqlDbType.Decimal),
                    new SqlParameter("@AssignQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ResourceReport",SqlDbType.Bit),
                    new SqlParameter("@StandardTime",SqlDbType.Int),
                    new SqlParameter("@PrepareTime",SqlDbType.Int),
                    new SqlParameter("@TaskNo",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@SourceID",SqlDbType.VarChar)
                    };

                parameters[0].Value = (Object)Model.FabMoOperationID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.FabricatedMotherID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.FabMoProcessID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.OperationID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[5].Value = (Object)Model.UnitID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.UnitRate ?? DBNull.Value;
                parameters[7].Value = (Object)Model.StartDate ?? DBNull.Value;
                parameters[8].Value = (Object)Model.FinishDate ?? DBNull.Value;
                parameters[9].Value = (Object)Model.Quantity ?? DBNull.Value;
                parameters[10].Value = (Object)Model.FinProQuantity ?? DBNull.Value;
                parameters[11].Value = (Object)Model.OutProQuantity ?? DBNull.Value;
                parameters[12].Value = (Object)Model.ScrappedQuantity ?? DBNull.Value;
                parameters[13].Value = (Object)Model.DifferenceQuantity ?? DBNull.Value;
                parameters[14].Value = (Object)Model.PreProQuantity ?? DBNull.Value;
                parameters[15].Value = (Object)Model.AssignQuantity ?? DBNull.Value;
                parameters[16].Value = (Object)Model.ResourceReport ?? DBNull.Value;
                parameters[17].Value = (Object)Model.StandardTime ?? DBNull.Value;
                parameters[18].Value = (Object)Model.PrepareTime ?? DBNull.Value;
                parameters[19].Value = (Object)Model.TaskNo ?? DBNull.Value;
                parameters[20].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[21].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[22].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017年7月13日23:55:50
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SFC_FabMoOperation Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_FabMoOperation] set {0},
                [OperationID]=@OperationID,[BeginDate]=@BeginDate,[EndDate]=@EndDate,
                [Sequence]=@Sequence,[UnitID]=@UnitID,[UnitRate]=@UnitRate,[StartDate]=@StartDate,
                [FinishDate]=@FinishDate,[Quantity]=@Quantity,[FinProQuantity]=@FinProQuantity,[OutProQuantity]=@OutProQuantity,
                [ScrappedQuantity]=@ScrappedQuantity,[DifferenceQuantity]=@DifferenceQuantity,[PreProQuantity]=@PreProQuantity,[AssignQuantity]=@AssignQuantity,
                [ResourceReport]=@ResourceReport,[StandardTime]=@StandardTime,[PrepareTime]=@PrepareTime,[TaskNo]=@TaskNo,
                [Status]=@Status,[Comments]=@Comments,[RepairQuantity]=@RepairQuantity
                where [FabMoOperationID]=@FabMoOperationID", UniversalService.getUpdateUTC(userId));

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@FabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@OperationID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.VarChar),
                    new SqlParameter("@UnitID",SqlDbType.VarChar),
                    new SqlParameter("@UnitRate",SqlDbType.Decimal),
                    new SqlParameter("@StartDate",SqlDbType.DateTime),
                    new SqlParameter("@FinishDate",SqlDbType.DateTime),
                    new SqlParameter("@Quantity",SqlDbType.Decimal),
                    new SqlParameter("@FinProQuantity",SqlDbType.Decimal),
                    new SqlParameter("@OutProQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ScrappedQuantity",SqlDbType.Decimal),
                    new SqlParameter("@DifferenceQuantity",SqlDbType.Decimal),
                    new SqlParameter("@PreProQuantity",SqlDbType.Decimal),
                    new SqlParameter("@AssignQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ResourceReport",SqlDbType.Bit),
                    new SqlParameter("@StandardTime",SqlDbType.Int),
                    new SqlParameter("@PrepareTime",SqlDbType.Int),
                    new SqlParameter("@TaskNo",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@BeginDate",SqlDbType.DateTime),
                    new SqlParameter("@EndDate",SqlDbType.DateTime),
                    new SqlParameter("@RepairQuantity",SqlDbType.Decimal)
                    };

                parameters[0].Value = Model.FabMoOperationID;
                parameters[1].Value = (Object)Model.OperationID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.UnitID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.UnitRate ?? DBNull.Value;
                parameters[5].Value = (Object)Model.StartDate ?? DBNull.Value;
                parameters[6].Value = (Object)Model.FinishDate ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Quantity ?? DBNull.Value;
                parameters[8].Value = (Object)Model.FinProQuantity ?? DBNull.Value;
                parameters[9].Value = (Object)Model.OutProQuantity ?? DBNull.Value;
                parameters[10].Value = (Object)Model.ScrappedQuantity ?? DBNull.Value;
                parameters[11].Value = (Object)Model.DifferenceQuantity ?? DBNull.Value;
                parameters[12].Value = (Object)Model.PreProQuantity ?? DBNull.Value;
                parameters[13].Value = (Object)Model.AssignQuantity ?? DBNull.Value;
                parameters[14].Value = (Object)Model.ResourceReport ?? DBNull.Value;
                parameters[15].Value = (Object)Model.StandardTime ?? DBNull.Value;
                parameters[16].Value = (Object)Model.PrepareTime ?? DBNull.Value;
                parameters[17].Value = (Object)Model.TaskNo ?? DBNull.Value;
                parameters[18].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[19].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[20].Value = (Object)Model.BeginDate ?? DBNull.Value;
                parameters[21].Value = (Object)Model.EndDate ?? DBNull.Value;
                parameters[22].Value = (Object)Model.RepairQuantity ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 根据制令制程和来源，获取制令制程工序实体
        /// SAM 2017年7月17日23:09:06
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <param name="SourceID"></param>
        /// <returns></returns>
        public static SFC_FabMoOperation get(string FabMoProcessID, string SourceID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_FabMoOperation] where [FabMoProcessID] = '{0}'  and [SourceID] ='{1}' and [Status] <> '{2}0201213000003'  and [SystemID] = '{2}' ", FabMoProcessID, SourceID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 判断制令制程下是否已存在相同的工序
        /// SAM 2017年9月1日10:51:43
        /// </summary>
        /// <param name="OperationID"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <returns></returns>
        public static bool CheckFabMoOperation(string OperationID, string FabMoProcessID, string FabMoOperationID)
        {
            string sql = String.Format(@"select Top 1 * from [SFC_FabMoOperation] where [SystemID]='{0}' and [Status] <> '{0}0201213000003'", Framework.SystemID);

            /*先定义Name和Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@OperationID",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            /*因为Name和Code是通过手动输入的，所以需要用参数的形式去拼SQL*/
            if (!string.IsNullOrWhiteSpace(OperationID))
            {
                sql = sql + String.Format(@" and [OperationID] =@OperationID ");
                parameters[0].Value = OperationID;
            }

            if (!string.IsNullOrWhiteSpace(FabMoProcessID))
                sql = sql + String.Format(@" and [FabMoProcessID] = '{0}' ", FabMoProcessID);

            /*FabMoOperationID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(FabMoOperationID))
                sql = sql + String.Format(@" and [FabMoOperationID] <> '{0}' ", FabMoOperationID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取单一实体
        /// SAM 2017年8月22日09:47:32
        /// </summary>
        /// <param name="FabMoOperationID"></param>
        /// <returns></returns>
        public static SFC_FabMoOperation get(string FabMoOperationID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_FabMoOperation] where [FabMoOperationID] = '{0}'  and [SystemID] = '{1}' ", FabMoOperationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 检测工序是否被使用
        /// Joint 2017年7月26日11:20:22
        /// </summary>
        /// <param name="ParameterID"></param>
        /// <returns></returns>
        public static SFC_FabMoOperation checkOperation(string ParameterID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_FabMoOperation] where [OperationID] = '{0}'  and [SystemID] = '{1}' ", ParameterID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据制令单修改他下面所有的制令制程工序状态
        /// SAM 2017年7月13日10:03:14
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static bool updateByFabMo(string userId, string FabricatedMotherID, string Status)
        {
            try
            {
                string sql = String.Format(@"update[SFC_FabMoOperation] set {0},
               [Status]=@Status where [FabricatedMotherID]=@FabricatedMotherID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@FabricatedMotherID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar)
                    };

                parameters[0].Value = FabricatedMotherID;
                parameters[1].Value = Status;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 根据制令制程获取他下面的制令制程工序列表
        /// SAM 2017年7月13日16:23:41
        /// </summary>
        /// <param name="fabricatedMotherID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00002FabMoOperationList(string FabMoProcessID, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.FabMoOperationID,A.FabMoProcessID, A.FabricatedMotherID, A.OperationID,A.Sequence,
                D.Code as OperationCode, D.Name as OperationName,A.UnitID,A.UnitRate,
                A.StandardTime,A.PrepareTime,A.Status,A.PreProQuantity,
                 convert(varchar(8),dateadd(ss,A.StandardTime,108),108) as StandardHour,
                 convert(varchar(8),dateadd(ss,A.PrepareTime,108),108) as PrepareHour,
                A.Quantity,FinProQuantity,A.OutProQuantity,A.ScrappedQuantity,A.DifferenceQuantity,
                E.Code as UnitCode,E.Name as UnitName,A.ResourceReport,A.Comments,
                B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(
                @"from [SFC_FabMoOperation] A
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                 left join [SYS_Parameters] D on A.[OperationID] = D.[ParameterID]
                 left join [SYS_Parameters] E on A.[UnitID] = E.[ParameterID]
                  where A.[SystemID] = '{0}' and  A.FabMoProcessID = @FabMoProcessID and
                        A.Status <> '{0}0201213000003'", Framework.SystemID);

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FabMoProcessID", FabMoProcessID));

            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 制令工单-根据制令制程获取下属工序弹窗
        /// SAM 2017年7月14日11:51:05
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> SfcGetFabMoOperationList(string FabMoProcessID, string Code, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.FabMoOperationID,A.FabMoProcessID, A.FabricatedMotherID, A.OperationID,A.Sequence,
                D.Code as OperationCode, D.Name as OperationName,A.UnitID,A.UnitRate,
                A.StandardTime,null as StandardHour,A.PrepareTime,null as PrepareHour,
                A.Quantity,FinProQuantity,A.OutProQuantity,A.ScrappedQuantity,A.DifferenceQuantity,
                E.Code as UnitCode,E.Name as UnitName,A.ResourceReport,A.Comments,
                B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(
                @"from [SFC_FabMoOperation] A
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                 left join [SYS_Parameters] D on A.[OperationID] = D.[ParameterID]
                 left join [SYS_Parameters] E on A.[UnitID] = E.[ParameterID]
                  where A.[SystemID] = '{0}' and  A.FabMoProcessID = @FabMoProcessID and
                        A.Status <> '{0}0201213000003'", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(Code))
                sql += string.Format(@" and D.Code like '%{0}%'", Code);

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FabMoProcessID", FabMoProcessID));


            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);

            return ToHashtableList(dt);
        }



        /// <summary>
        /// 根据制令制程获取制令制程工序列表
        /// SAN 2017年7月13日21:59:21
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetBomList(string FabMoProcessID)
        {
            string sql = string.Format(
                @"select A.FabMoOperationID,A.FabMoProcessID as Parenter,A.FabMoProcessID+A.FabMoOperationID as FabMoProcessID,A.Sequence,B.WorkCenterID,
                A.Sequence+' '+(select [Code] from [SYS_Parameters] where A.[OperationID] = [ParameterID])+' '+(select [Name] from [SYS_Parameters] where A.[OperationID] = [ParameterID]) as Value
                  from [SFC_FabMoOperation] A
                  left Join SFC_FabMoProcess B on A.FabMoProcessID = B.FabMoProcessID
                  where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and A.[FabMoProcessID] ='{1}'", Framework.SystemID, FabMoProcessID);

            string orderby = " order by [Sequence]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据制令制程获取制令制程工序列表
        /// Joint
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static List<SFC_FabMoOperation> GetFabMoOperationList(string FabMoProcessID)
        {
            string sql = string.Format(
                @"select * from [SFC_FabMoOperation]        
                  where [SystemID] = '{0}' and [Status] <> '{0}0201213000003' and [FabMoProcessID] ='{1}'", Framework.SystemID, FabMoProcessID);

            string orderby = " order by [CreateLocalTime] desc";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }

        /// <summary>
        /// 根据制令制程工序流水号获取制令制程工序信息
        /// SAM 2017年7月17日18:28:14
        /// </summary>
        /// <param name="fabMoOperationID"></param>
        /// <returns></returns>
        public static Hashtable Sfc00002GetFabMoOperation(string FabMoOperationID)
        {
            string select = string.Format(
                @"select Top 1 A.FabMoOperationID,A.FabMoProcessID, A.FabricatedMotherID, A.OperationID,A.Sequence,
                G.Quantity,D.Code as OperationCode, D.Name as OperationName,A.AssignQuantity,F.ProcessID,G.MoNo,
                (Select ItemID from SFC_FabricatedMother where F.FabricatedMotherID = FabricatedMotherID) as ItemID,
                (Select Code from [SYS_Items] where ItemID = (Select ItemID from SFC_FabricatedMother where F.FabricatedMotherID = FabricatedMotherID)) as ItemCode,
                (select [Code] from [SYS_Parameters] where [ParameterID] =F.[ProcessID]) as ProcessCode,
                (select [Name] from [SYS_Parameters] where [ParameterID] =F.[ProcessID]) as ProcessName,
                A.UnitID,A.UnitRate,A.PreProQuantity,G.OrderQuantity,
                A.StandardTime,null as StandardHour,A.PrepareTime,null as PrepareHour,
                A.Quantity as AfterSeparateQuantity,A.FinProQuantity,A.OutProQuantity,A.ScrappedQuantity,A.DifferenceQuantity,
                E.Code as UnitCode,E.Name as UnitName,A.ResourceReport,A.Comments,
                (Select ISnull(SUM(DispatchQuantity),0) from [SFC_TaskDispatch] where [FabMoOperationID] =A.[FabMoOperationID] and [Status]='{0}0201213000087') as NAAssignQuantity,
                B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_FabMoOperation] A
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                 left join [SYS_Parameters] D on A.[OperationID] = D.[ParameterID]
                 left join [SYS_Parameters] E on A.[UnitID] = E.[ParameterID]
                 left join [SFC_FabMoProcess] F on A.[FabMoProcessID] = F.[FabMoProcessID]
                 left join [SFC_FabricatedMother] G on A.FabricatedMotherID = G.FabricatedMotherID
                  where A.[SystemID] = '{0}' and  A.FabMoOperationID = '{1}' and
                        A.Status <> '{0}0201213000003'", Framework.SystemID, FabMoOperationID);

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 制程完工状况分析-工序列表
        /// SAM 2017年7月23日00:59:49
        /// </summary>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00011GetOperationList(string FabMoProcessID, int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select  A.FabMoOperationID,
            A.Sequence,B.Code as OperationCode,B.Name as OperationName,       
             A.StartDate,A.FinishDate,A.Quantity,A.FinProQuantity,
            A.Quantity-A.FinProQuantity as BalanceQuantity,
            A.ScrappedQuantity,A.DifferenceQuantity,
            C.Code as UnitCode,C.Name as UnitName,A.UnitRate,
            D.Name as Status,A.Comments ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_FabMoOperation] A         
                left join [SYS_Parameters] B on A.[OperationID] =B.[ParameterID]
                left join [SYS_Parameters] C on A.[UnitID] =C.[ParameterID]
                left join [SYS_Parameters] D on A.[Status] =D.[ParameterID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and A.[FabMoProcessID]='{1}' ", Framework.SystemID, FabMoProcessID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 根据制令制程流水号获取他的第一道工序
        /// 可能存在多个第一道工序的情况
        /// SAM 2017年8月22日09:48:54
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetFirstOperationList(string FabMoProcessID)
        {
            string sql = string.Format(
                @"select *
                  from [SFC_FabMoOperation] A
                  where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and A.[FabMoProcessID] ='{1}'
                and [FabMoOperationID] in (select [FabMoOperationID] from [SFC_FabMoOperationRelationship] where [FabMoProcessID]='{1}' and [FabMoOperationID]= [PreFabMoOperationID] and [Status]='{0}1213000001')", Framework.SystemID, FabMoProcessID);

            string orderby = " order by [Sequence]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据制令制程流水号获取他的第一道工序
        /// 存在多个第一道工序是默认拿第一道
        /// SAM 2017年8月29日14:29:41
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static SFC_FabMoOperation GetFirstOperation(string FabMoProcessID)
        {
            string sql = string.Format(
                @"select Top 1 *
                  from [SFC_FabMoOperation] A
                  where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and A.[FabMoProcessID] ='{1}'
                and [FabMoOperationID] in (select [FabMoOperationID] from [SFC_FabMoOperationRelationship] where [FabMoProcessID]='{1}' and [FabMoOperationID]= [PreFabMoOperationID] and [Status]='{0}0201213000001')", Framework.SystemID, FabMoProcessID);

            string orderby = " order by [Sequence]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToEntity(dt.Rows[0]);
        }


        /// <summary>
        /// 汇总一个制令制程的工序标准工时和
        /// SAM 2017年9月7日10:47:18
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static int GetStandardTimeSum(string FabMoProcessID)
        {
            string sql = string.Format(@"select SUM(ISNULL(StandardTime,0)) from [SFC_FabMoOperation] where [FabMoProcessID] = '{0}'  and [SystemID] = '{1}' and [Status] <> '{1}0201213000003' ", FabMoProcessID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return 0;
            else
                return int.Parse(dt.Rows[0][0].ToString());
        }


        /// <summary>
        /// 汇总一个制令制程的工序整备工时和
        /// SAM 2017年9月7日10:47:18
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static int GetPrepareTimeSum(string FabMoProcessID)
        {
            string sql = string.Format(@"select SUM(ISNULL(PrepareTime,0)) from [SFC_FabMoOperation] where [FabMoProcessID] = '{0}'  and [SystemID] = '{1}' and [Status] <> '{1}0201213000003' ", FabMoProcessID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return 0;
            else
                return int.Parse(dt.Rows[0][0].ToString());
        }

        /// <summary>
        /// Iot00003获取制令工单画面，有工序已发派
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static Hashtable Iot00003GetMomProcessHasOperation(string FabMoOperationID)
        {
            string sql = string.Format(@"select E.Code as ItemCode,B.MoNo,B.SplitSequence,F.Sequence as ProcessSequence,G.Code as ProcessCode,G.Name as ProcessName,
                        A.Sequence as OperationSequence,C.Code as OperationCode,C.Name as OperationName,D.Code as CenterCode,D.Name as CenterName,B.StartDate,B.FinishDate,B.Status 
                        from [SFC_FabMoOperation] A 
						left join [SFC_FabricatedMother] B on A.[FabricatedMotherID]= B.[FabricatedMotherID]
						left join [SYS_Parameters] C on A.[OperationID]= C.[ParameterID]
						left join [SYS_Items] E on E.[ItemID] = B.[ItemID]
						left join [SFC_FabMoProcess] F on A.[FabMoProcessID] =F.[FabMoProcessID]
						left join [SYS_WorkCenter] D on F.[WorkCenterID]= D.[WorkCenterID]
						left join [SYS_Parameters] G on F.[ProcessID]=G.[ParameterID]
                        where A.[SystemID]='{0}' and A.[Status]<>'{0}020121300002B' and A.[FabMoOperationID]='{1}'", Framework.SystemID, FabMoOperationID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count != 0)
            {
                return ToHashtableList(dt)[0];//有多条数据时也只返回第一条数据
            }
            else
            {
                return null;//查询数据为空
            }
        }

        /// <summary>
        /// 拼接制令制程工序的插入语句
        /// SAM 2017年10月6日14:37:36
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static String InsertSQL(string userId, SFC_FabMoOperation Model)
        {
            try
            {
                string sql = string.Format(
                    @"insert [SFC_FabMoOperation]([SystemID],[FabMoOperationID],[FabricatedMotherID],[FabMoProcessID],[OperationID],
                    [Sequence],[UnitID],[UnitRate],[Quantity],[StandardTime],[PrepareTime],              
                    [StartDate],[FinishDate],[Status],[SourceID],[Comments],
                    [Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime]) values(
                    '{0}','{1}','{2}','{3}','{4}',
                    '{5}',{6},{7},{8},{9},{10},
                     {11},{12},'{13}','{14}','{15}',
                    '{16}','{17}','{18}','{16}','{17}','{18}');",
                     Framework.SystemID, Model.FabMoOperationID, Model.FabricatedMotherID, Model.FabMoProcessID, Model.OperationID,
                     Model.Sequence, UniversalService.checkNullForSQL(Model.UnitID), Model.UnitRate, Model.Quantity,
                     Model.StandardTime, Model.PrepareTime,
                     Model.StartDate == null ? "NULL" : "'" + Model.StartDate.ToString() + "'",
                     Model.FinishDate == null ? "NULL" : "'" + Model.FinishDate.ToString() + "'",
                     Model.Status, Model.SourceID, Model.Comments,
                     userId, DateTime.UtcNow, DateTime.Now);

                return sql;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                throw;
            }
        }


        /// <summary>
        /// 根据指令工单获取所有正常的制令制程工序列表
        /// SAM 2017年10月6日15:18:22
        /// </summary>
        /// <returns></returns>
        public static IList<SFC_FabMoOperation> GetList(string FabricatedMotherID)
        {
            string sql = string.Format(
                @"select * from [SFC_FabMoOperation]       
                  where [SystemID] = '{0}' and [Status] <> '{0}0201213000003'  and [FabricatedMotherID] ='{1}'", Framework.SystemID, FabricatedMotherID);

            string orderby = " order by [Sequence]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }
        
        /// <summary>
          /// 根据制令制程工序流水号获取制令制程工序信息
          /// SAM 2017年10月24日11:33:04
          /// SFC04表头专用，已分派量=制造量（工序）-已分派量（所有非删除，非作废的任务单）+差异量（所有非删除，非作废的任务单）
          /// </summary>
          /// <param name="FabMoOperationID"></param>
          /// <returns></returns>
        public static Hashtable Sfc00004GetFabMoOperation(string FabMoOperationID)
        {
            string select = string.Format(
                @"select Top 1 A.FabMoOperationID,A.FabMoProcessID, A.FabricatedMotherID, A.OperationID,A.Sequence,F.[ProcessID],
                D.Code as OperationCode, D.Name as OperationName,A.AssignQuantity,F.ProcessID,G.MoNo,
                (select [Code] from [SYS_Parameters] where [ParameterID] =F.[ProcessID]) as ProcessCode,
                (select [Name] from [SYS_Parameters] where [ParameterID] =F.[ProcessID]) as ProcessName,
                (select [Code] from [SYS_Items] where [ItemID] =G.[ItemID]) as ItemCode,
                A.UnitID,A.UnitRate,A.PreProQuantity,G.OrderQuantity,
                A.Quantity,A.FinProQuantity,A.OutProQuantity,A.ScrappedQuantity,A.DifferenceQuantity,A.ResourceReport,A.Comments,
                (Select ISnull(SUM(DiffQuantity),0) from [SFC_TaskDispatch] where [FabMoOperationID] = A.[FabMoOperationID] and [Status] <> '{0}020121300008C' and [Status] <> '{0}0201213000003') as TaskDiffQuantity,
                (Select ISnull(SUM(DispatchQuantity),0) from [SFC_TaskDispatch] where [FabMoOperationID] = A.[FabMoOperationID] and [Status] <> '{0}020121300008C' and [Status] <> '{0}0201213000003') as TaskAssignQuantity ", Framework.SystemID);

            string sql = string.Format(
                @" from [SFC_FabMoOperation] A
                 left join [SYS_Parameters] D on A.[OperationID] = D.[ParameterID]
                 left join [SFC_FabMoProcess] F on A.[FabMoProcessID] = F.[FabMoProcessID]
                 left join [SFC_FabricatedMother] G on A.[FabricatedMotherID] = G.[FabricatedMotherID]
                 where A.[SystemID] = '{0}' and  A.FabMoOperationID = '{1}' and A.Status <> '{0}0201213000003'", Framework.SystemID, FabMoOperationID);

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }
    }
}

