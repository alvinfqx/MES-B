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
    public class SFC_FabricatedMotherService : SuperModel<SFC_FabricatedMother>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年9月3日21:16:07
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SFC_FabricatedMother Model)
        {
            try
            {
                string sql = string.Format(
                  @"insert[SFC_FabricatedMother]([FabricatedMotherID],[MoNo],[Date],
                    [Version],[SplitSequence],[BatchNumber],[ItemID],[Quantity],
                    [UnitID],[StartDate],[FinishDate],[OrderNo],[OrderQuantity],
                    [CustomerID],[MESUserID],[ShipmentDate],[OrganizationID],[Status],
                    [StorageQuantity],[SeparateQuantity],[OverRate],[Source],
                    [OriginalFabricatedMotherID],[ControlUserID],[Comments],[Modifier],[ModifiedTime],
                    [ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values(
                    @FabricatedMotherID,@MoNo,@Date,
                    @Version,@SplitSequence,@BatchNumber,
                    @ItemID,@Quantity,@UnitID,
                    @StartDate,@FinishDate,@OrderNo,
                    @OrderQuantity,@CustomerID,@MESUserID,
                    @ShipmentDate,@OrganizationID,@Status,
                    @StorageQuantity,@SeparateQuantity,@OverRate,
                    @Source,@OriginalFabricatedMotherID,@ControlUserID,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')",
                    userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@FabricatedMotherID",SqlDbType.VarChar),
                    new SqlParameter("@MoNo",SqlDbType.VarChar),
                    new SqlParameter("@Version",SqlDbType.VarChar),
                    new SqlParameter("@SplitSequence",SqlDbType.Int),
                    new SqlParameter("@BatchNumber",SqlDbType.VarChar),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@Quantity",SqlDbType.Decimal),
                    new SqlParameter("@UnitID",SqlDbType.VarChar),
                    new SqlParameter("@StartDate",SqlDbType.DateTime),
                    new SqlParameter("@FinishDate",SqlDbType.DateTime),
                    new SqlParameter("@OrderNo",SqlDbType.VarChar),
                    new SqlParameter("@OrderQuantity",SqlDbType.Decimal),
                    new SqlParameter("@CustomerID",SqlDbType.VarChar),
                    new SqlParameter("@MESUserID",SqlDbType.VarChar),
                    new SqlParameter("@ShipmentDate",SqlDbType.DateTime),
                    new SqlParameter("@OrganizationID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@StorageQuantity",SqlDbType.Decimal),
                    new SqlParameter("@SeparateQuantity",SqlDbType.Decimal),
                    new SqlParameter("@OverRate",SqlDbType.Decimal),
                    new SqlParameter("@Source",SqlDbType.VarChar),
                    new SqlParameter("@OriginalFabricatedMotherID",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@Date",SqlDbType.DateTime),
                    new SqlParameter("@ControlUserID",SqlDbType.NVarChar)
                    };

                parameters[0].Value = (Object)Model.FabricatedMotherID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.MoNo ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Version ?? DBNull.Value;
                parameters[3].Value = (Object)Model.SplitSequence ?? DBNull.Value;
                parameters[4].Value = (Object)Model.BatchNumber ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Quantity ?? 0;
                parameters[7].Value = (Object)Model.UnitID ?? DBNull.Value;
                parameters[8].Value = (Object)Model.StartDate ?? DBNull.Value;
                parameters[9].Value = (Object)Model.FinishDate ?? DBNull.Value;
                parameters[10].Value = (Object)Model.OrderNo ?? DBNull.Value;
                parameters[11].Value = (Object)Model.OrderQuantity ?? 0;
                parameters[12].Value = (Object)Model.CustomerID ?? DBNull.Value;
                parameters[13].Value = (Object)Model.MESUserID ?? DBNull.Value;
                parameters[14].Value = (Object)Model.ShipmentDate ?? DBNull.Value;
                parameters[15].Value = (Object)Model.OrganizationID ?? DBNull.Value;
                parameters[16].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[17].Value = (Object)Model.StorageQuantity ?? 0;
                parameters[18].Value = (Object)Model.SeparateQuantity ?? 0;
                parameters[19].Value = (Object)Model.OverRate ?? 0;
                parameters[20].Value = (Object)Model.Source ?? DBNull.Value;
                parameters[21].Value = (Object)Model.OriginalFabricatedMotherID ?? DBNull.Value;
                parameters[22].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[23].Value = (Object)Model.Date ?? DBNull.Value;
                parameters[24].Value = (Object)Model.ControlUserID ?? DBNull.Value;

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
        /// SAM 2017年7月13日23:45:19
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SFC_FabricatedMother Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_FabricatedMother] set {0},
                [Version]=@Version,[SplitSequence]=@SplitSequence,[Date] = @Date,
                [BatchNumber]=@BatchNumber,[ItemID]=@ItemID,[Quantity]=@Quantity,[UnitID]=@UnitID,
                [StartDate]=@StartDate,[FinishDate]=@FinishDate,[OrderNo]=@OrderNo,[OrderQuantity]=@OrderQuantity,
                [CustomerID]=@CustomerID,[MESUserID]=@MESUserID,[ShipmentDate]=@ShipmentDate,[OrganizationID]=@OrganizationID,
                [Status]=@Status,[StorageQuantity]=@StorageQuantity,[SeparateQuantity]=@SeparateQuantity,[OverRate]=@OverRate,
                [Source]=@Source,[OriginalFabricatedMotherID]=@OriginalFabricatedMotherID,[Comments]=@Comments,
                [ApproveUserID]=@ApproveUserID,[ApproveDate]=@ApproveDate
                where [FabricatedMotherID]=@FabricatedMotherID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@FabricatedMotherID",SqlDbType.VarChar),
                    new SqlParameter("@Version",SqlDbType.VarChar),
                    new SqlParameter("@SplitSequence",SqlDbType.Int),
                    new SqlParameter("@BatchNumber",SqlDbType.VarChar),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@Quantity",SqlDbType.Decimal),
                    new SqlParameter("@UnitID",SqlDbType.VarChar),
                    new SqlParameter("@StartDate",SqlDbType.DateTime),
                    new SqlParameter("@FinishDate",SqlDbType.DateTime),
                    new SqlParameter("@OrderNo",SqlDbType.VarChar),
                    new SqlParameter("@OrderQuantity",SqlDbType.Decimal),
                    new SqlParameter("@CustomerID",SqlDbType.VarChar),
                    new SqlParameter("@MESUserID",SqlDbType.VarChar),
                    new SqlParameter("@ShipmentDate",SqlDbType.DateTime),
                    new SqlParameter("@OrganizationID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@StorageQuantity",SqlDbType.Decimal),
                    new SqlParameter("@SeparateQuantity",SqlDbType.Decimal),
                    new SqlParameter("@OverRate",SqlDbType.Decimal),
                    new SqlParameter("@Source",SqlDbType.VarChar),
                    new SqlParameter("@OriginalFabricatedMotherID",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@Date",SqlDbType.DateTime),
                    new SqlParameter("@ApproveUserID",SqlDbType.NVarChar),
                    new SqlParameter("@ApproveDate",SqlDbType.DateTime)
                    };

                parameters[0].Value = Model.FabricatedMotherID;
                parameters[1].Value = (Object)Model.Version ?? DBNull.Value;
                parameters[2].Value = (Object)Model.SplitSequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.BatchNumber ?? DBNull.Value;
                parameters[4].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Quantity ?? DBNull.Value;
                parameters[6].Value = (Object)Model.UnitID ?? DBNull.Value;
                parameters[7].Value = (Object)Model.StartDate ?? DBNull.Value;
                parameters[8].Value = (Object)Model.FinishDate ?? DBNull.Value;
                parameters[9].Value = (Object)Model.OrderNo ?? DBNull.Value;
                parameters[10].Value = (Object)Model.OrderQuantity ?? DBNull.Value;
                parameters[11].Value = (Object)Model.CustomerID ?? DBNull.Value;
                parameters[12].Value = (Object)Model.MESUserID ?? DBNull.Value;
                parameters[13].Value = (Object)Model.ShipmentDate ?? DBNull.Value;
                parameters[14].Value = (Object)Model.OrganizationID ?? DBNull.Value;
                parameters[15].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[16].Value = (Object)Model.StorageQuantity ?? DBNull.Value;
                parameters[17].Value = (Object)Model.SeparateQuantity ?? DBNull.Value;
                parameters[18].Value = (Object)Model.OverRate ?? DBNull.Value;
                parameters[19].Value = (Object)Model.Source ?? DBNull.Value;
                parameters[20].Value = (Object)Model.OriginalFabricatedMotherID ?? DBNull.Value;
                parameters[21].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[22].Value = (Object)Model.Date ?? DBNull.Value;
                parameters[23].Value = (Object)Model.ApproveUserID ?? DBNull.Value;
                parameters[24].Value = (Object)Model.ApproveDate ?? DBNull.Value;

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
        /// SAM 2017年7月13日23:44:57
        /// </summary>
        /// <param name="FabricatedMotherID"></param>
        /// <returns></returns>
        public static SFC_FabricatedMother get(string FabricatedMotherID)
        {
            string sql = string.Format(
                @"select Top 1 * 
                  from [SFC_FabricatedMother] 
                  where [FabricatedMotherID] = '{0}'  and [SystemID] = '{1}' ", FabricatedMotherID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 判断制令单号是否重复
        /// SAM 2017年9月6日17:27:31
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static bool Check(string Code)
        {
            string sql = String.Format(@"select Top 1 * from [SFC_FabricatedMother] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(Code))
                sql = sql + string.Format(@" and [MoNo]= '{0}' ", Code);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断批号是否重复
        /// SAM 2017年9月19日16:20:18
        /// </summary>
        /// <param name="BatchNumber"></param>
        /// <returns></returns>
        public static bool CheckLot(string BatchNumber)
        {
            string sql = String.Format(@"select Top 1 * from [SFC_FabricatedMother] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(BatchNumber))
                sql = sql + string.Format(@" and [BatchNumber]= '{0}' ", BatchNumber);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }



        /// <summary>
        /// 開窗讀取製令母件表
        /// Joint 2017年6月26日09:48:07
        /// </summary>
        ///<param name="MoNo">制令单号</param>
        ///<param name="page">页码</param>
        ///<param name"rows">行数</param>
        ///<param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00003GetFabricatedMother(string MoNo, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.MoNo,convert(varchar(10),A.Date,120) as Date,A.OrderNo", Framework.SystemID);
            string sql = string.Format(@"from [SFC_FabricatedMother] A where A.[SystemID]='{0}' and A.Status= '{0}0201213000028' and A.SplitSequence= 0", Framework.SystemID);
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(MoNo))
            {
                sql += " and A.MoNo collate Chinese_PRC_CI_AS like @MoNo";
                parameters.Add(new SqlParameter("@MoNo", "%" + MoNo + "%"));

            }

            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);
            string orderby = "A.[MoNo]";
            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);
            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取制令单列表
        /// Tom
        /// </summary>
        /// <param name="moNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetList(string MoNo,
            string StartItemCode, string EndItemCode,string StartFabMoCode, string EndFabMoCode,
            string CustCode, string MESUserCode,string ControlUser, string Status,
            int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.FabricatedMotherID, A.MoNo, A.Version, A.SplitSequence,A.Date,
                         A.BatchNumber, A.ItemID, A.Quantity, A.UnitID, A.StartDate,
                         A.FinishDate, A.OrderNo, A.OrderQuantity, A.CustomerID, 
                        (A.Quantity+A.SeparateQuantity) as BeforeQuantity,
                         A.MESUserID, A.ShipmentDate, A.OrganizationID, A.Status,
                         A.StorageQuantity, A.SeparateQuantity, A.OverRate, A.Source,
                         H.MoNo as OriginalMoNo, A.Comments,
                         B.Code as ItemCode, B.Name as ItemName, B.[Specification],
                         C.Code as CustomerCode, C.Name as CustomerName, D.Emplno, D.UserName as EmpName,
                         E.Name as OrganizationName, A.ControlUserID,
                         I.Emplno as ControlUserCode,I.UserName as ControlUserName,
                         G.Emplno+'-'+G.UserName as Creator, A.CreateLocalTime as CreateTime, 
                        (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else F.Emplno + '-' + F.UserName END) as Modifier,
                        (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(
                @"from [SFC_FabricatedMother] A
                  left join SYS_Items B on A.ItemID = B.ItemID 
                  left join SYS_Customers C on A.CustomerID = C.CustomerID
                  left join SYS_MESUsers D on A.MESUserID = D.MESUserID
                  left join SYS_Organization E on A.OrganizationID = E.OrganizationID
                  left join SYS_MESUsers F on A.Modifier = F.MESUserID
                  left join SYS_MESUsers G on A.Creator = G.MESUserID
                  left join SYS_MESUsers I on A.ControlUserID = I.MESUserID
                  left join SFC_FabricatedMother H on  A.OriginalFabricatedMotherID = H.FabricatedMotherID
                  where A.[SystemID] = '{0}' and A.Status <> '{0}0201213000003'", Framework.SystemID);

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(StartItemCode))
            {
                sql += " and B.Code >= @StartItemCode";
                parameters.Add(new SqlParameter("@StartItemCode",StartItemCode.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(EndItemCode))
            {
                sql += " and B.Code <= @EndItemCode";
                parameters.Add(new SqlParameter("@EndItemCode", EndItemCode.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(StartFabMoCode))
            {
                sql += " and A.MoNo >= @StartFabMoCode";
                parameters.Add(new SqlParameter("@StartFabMoCode", StartFabMoCode.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(EndFabMoCode))
            {
                sql += " and A.MoNo <= @EndFabMoCode";
                parameters.Add(new SqlParameter("@EndFabMoCode", EndFabMoCode.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(CustCode))
            {
                sql += " and C.Code  collate Chinese_PRC_CI_AS  like @CustCode";
                parameters.Add(new SqlParameter("@CustCode", "%"+ CustCode.Trim() + "%"));
            }

            if (!string.IsNullOrWhiteSpace(MESUserCode))
            {
                sql += " and D.Emplno  collate Chinese_PRC_CI_AS  like @MESUserCode";
                parameters.Add(new SqlParameter("@MESUserCode", "%" + MESUserCode.Trim() + "%" ));
            }

            if (!string.IsNullOrWhiteSpace(ControlUser))
            {
                sql += " and I.Emplno  collate Chinese_PRC_CI_AS  like @ControlUser";
                parameters.Add(new SqlParameter("@ControlUser", "%" + ControlUser.Trim() + "%"));
            }

            if (!string.IsNullOrWhiteSpace(MoNo))
            {
                sql += " and A.MoNo  collate Chinese_PRC_CI_AS  like @MoNo";
                parameters.Add(new SqlParameter("@MoNo", "%" + MoNo.Trim() + "%"));
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql +=string.Format(@" and A.[Status] in ('{0}')",Status.Replace(",","','"));
            }

            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);

            string orderby = "A.[MoNo]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取列表
        /// Joint
        /// </summary>
        /// <param name="MoNo"></param>
        /// <param name="StartItemCode"></param>
        /// <param name="EndItemCode"></param>
        /// <param name="StartMoNo"></param>
        /// <param name="EndMoNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00003GetList( string StartItemCode, string EndItemCode, string StartMoNo, string EndMoNo, int page, int rows,ref int count)
        {

            string select = string.Format(
                @"select A.FabricatedMotherID, A.MoNo, A.Version, A.SplitSequence,
                         A.BatchNumber, A.ItemID, A.Quantity, A.UnitID, A.StartDate,
                         A.FinishDate, A.OrderNo, A.OrderQuantity, A.CustomerID, 
                         A.MESUserID, A.ShipmentDate, A.OrganizationID, A.Status,
                         A.StorageQuantity, A.SeparateQuantity, A.OverRate, A.Source,
                         H.MoNo as OriginalMoNo, A.Comments,(A.Quantity+A.SeparateQuantity) as BeforeQuantity,
                         B.Code as ItemCode, B.Name as ItemName, B.[Specification],
                         C.Code as CustomerCode, C.Name as CustomerName, D.Emplno, D.UserName as EmpName,
                         E.Name as OrganizationName, F.UserName as Modifier, G.UserName as Creator, 
                         A.CreateLocalTime as CreateTime, A.ModifiedLocalTime as ModifiedTime", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_FabricatedMother] A
                  left join SYS_Items B on A.ItemID = B.ItemID 
                  left join SYS_Customers C on A.CustomerID = C.CustomerID
                  left join SYS_MESUsers D on A.MESUserID = D.MESUserID
                  left join SYS_Organization E on A.OrganizationID = E.OrganizationID
                  left join SYS_MESUsers F on A.Modifier = F.MESUserID
                  left join SYS_MESUsers G on A.Creator = G.MESUserID
                  left join SFC_FabricatedMother H on H.FabricatedMotherID = A.OriginalFabricatedMotherID
                  where A.[SystemID] = '{0}' and A.Status = '{0}0201213000028' and A.SplitSequence= 0 ", Framework.SystemID);

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(StartItemCode))
            {
                sql += " and B.Code >= @StartItemCode";
                parameters.Add(new SqlParameter("@StartItemCode", StartItemCode));
            }
            if (!string.IsNullOrWhiteSpace(EndItemCode))
            {
                sql += " and B.Code <= @EndItemCode";
                parameters.Add(new SqlParameter("@EndItemCode", EndItemCode));
            }
            if (!string.IsNullOrWhiteSpace(StartMoNo))
            {
                sql += " and A.[MoNo] >= @StartMoNo";
                parameters.Add(new SqlParameter("@StartMoNo",StartMoNo));
            }
            if (!string.IsNullOrWhiteSpace(EndMoNo))
            {
                sql += " and A.[MoNo] <= @EndMoNo";
                parameters.Add(new SqlParameter("@EndMoNo", EndMoNo));
            }

            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);

            string orderby = " A.[MoNo],B.[Code]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// RC分派与维护主列表
        /// SAM 2017年6月29日15:11:32
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00004GetList(int page, string StartWorkCenterCode, string EndWorkCenterCode,
           string StartProcessCode, string EndProcessCode,
           string StartFabMoCode, string EndFabMoCode,
           string StartDate, string EndDate, int rows, ref int count)
        {
            string select = string.Format(
                @"select DISTINCT A.FabricatedMotherID,C.FabMoOperationID,B.FabMoProcessID,
                A.MoNo, A.Version, A.SplitSequence,A.BatchNumber,B.ProcessID,C.OperationID,A.ItemID,
                B.Sequence as ProcessSequence,D.Code as ProcessCode,D.Name as ProcessName,
                C.Sequence as OperationSequence,E.Code as OperationCode,E.Name as OperationName,
                F.Code as WorkCenterCode,F.Name as WorkCenterName,
                (Select Name from [SYS_Parameters] where ParameterID = F.InoutMark) as InoutMark,
                D.IsDefault as IsOperation,
                (CASE WHEN F.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = F.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = F.DepartmentID) END) as DeptCode,
                (CASE WHEN F.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = F.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = F.DepartmentID) END) as DeptName,       
                G.Code as ItemCode,B.Quantity,B.Comments,B.AssignQuantity,
                (CASE When C.FabMoOperationID is null THEN B.StartDate ELSE C.StartDate END) as StartDate,
                (CASE When C.FabMoOperationID is null THEN B.FinishDate ELSE C.FinishDate END) as FinishDate,
                (select Name from SYS_Parameters where ParameterID = (CASE When C.FabMoOperationID is null THEN B.Status ELSE C.Status END)) as Status,
                Z.Emplno+'-'+Z.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else X.Emplno+'-'+X.UserName END) as Modifier,
               (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_FabricatedMother] A
                 left join [SFC_FabMoProcess] B on A.[FabricatedMotherID] = B.[FabricatedMotherID] and (B.[Status] = '{0}0201213000029' or B.[Status] = '{0}0201213000028')
                 left join [SFC_FabMoOperation] C on C.[FabMoProcessID] = B.[FabMoProcessID] and (C.[Status] = '{0}0201213000029' or C.[Status] = '{0}0201213000028')
                 left join [SYS_Parameters] D on B.[ProcessID] = D.ParameterID 
                 left join [SYS_Parameters] E on C.OperationID = E.ParameterID
                 left join [SYS_WorkCenter] F on B.WorkCenterID = F.WorkCenterID
                 left join [SYS_Items] G on A.[ItemID] = G.[ItemID]
                 left join SYS_MESUsers X on A.Modifier = X.MESUserID
                 left join SYS_MESUsers Z on A.Creator = Z .MESUserID
                  where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000029' ", Framework.SystemID);


            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@EndFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartProcessCode",SqlDbType.VarChar),
                new SqlParameter("@EndProcessCode",SqlDbType.VarChar),

            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;
            parameters[5].Value = DBNull.Value;
            parameters[6].Value = DBNull.Value;
            parameters[7].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@EndFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartProcessCode",SqlDbType.VarChar),
                new SqlParameter("@EndProcessCode",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;
            Parcount[6].Value = DBNull.Value;
            Parcount[7].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartWorkCenterCode))
            {
                sql = sql + String.Format(@" and F.[Code] >= @StartWorkCenterCode");
                parameters[0].Value = StartWorkCenterCode;
                Parcount[0].Value = StartWorkCenterCode;
            }

            if (!string.IsNullOrWhiteSpace(EndWorkCenterCode))
            {
                sql = sql + String.Format(@" and F.[Code] <= @EndWorkCenterCode");
                parameters[1].Value = EndWorkCenterCode;
                Parcount[1].Value = EndWorkCenterCode;
            }


            if (!string.IsNullOrWhiteSpace(StartFabMoCode))
            {
                sql = sql + String.Format(@" and A.[MoNo] >= @StartFabMoCode ");
                parameters[2].Value = StartFabMoCode;
                Parcount[2].Value = StartFabMoCode;
            }

            if (!string.IsNullOrWhiteSpace(EndFabMoCode))
            {
                sql = sql + String.Format(@" and A.[MoNo] <= @EndFabMoCode ");
                parameters[3].Value = EndFabMoCode;
                Parcount[3].Value = EndFabMoCode;
            }

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + String.Format(@" and (CASE When C.FabMoOperationID is null THEN B.StartDate ELSE C.StartDate END) >= @StartDate ");
                parameters[4].Value = StartDate;
                Parcount[4].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + String.Format(@" and (CASE When C.FabMoOperationID is null THEN B.StartDate ELSE C.StartDate END) <= @EndDate ");
                parameters[5].Value = EndDate;
                Parcount[5].Value = EndDate;
            }

            if (!string.IsNullOrWhiteSpace(StartProcessCode))
            {
                sql = sql + String.Format(@" and D.[Code] >= @StartProcessCode ");
                parameters[6].Value = StartProcessCode;
                Parcount[6].Value = StartProcessCode;
            }

            if (!string.IsNullOrWhiteSpace(EndProcessCode))
            {
                sql = sql + String.Format(@" and D.[Code] <= @EndProcessCode ");
                parameters[7].Value = EndProcessCode;
                Parcount[7].Value = EndProcessCode;
            }


            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[MoNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


        public static bool Check(string OriginalFabricatedMotherID, string SplitSequence)
        {
            string sql = string.Format(
               @"select Top 1 * from [SFC_FabricatedMother] where  [SystemID] = '{1}' ",  Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(OriginalFabricatedMotherID))
                sql = sql + string.Format(@" and [OriginalFabricatedMotherID] = '{0}' ", OriginalFabricatedMotherID);

            if (!string.IsNullOrWhiteSpace(SplitSequence))
                sql = sql + string.Format(@" and [SplitSequence]= '{0}' ", SplitSequence);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }



        /// <summary>
        /// 求拆单序号不为0的制令单制造数量和
        /// Joint
        /// </summary>
        /// <param name="FabricatedMotherID"></param>
        /// <returns></returns>
        public static SFC_FabricatedMother GetSumQuantity(string FabricatedMotherID)
        {
            string sql = string.Format(
                @"select SUM(Quantity) from [SFC_FabricatedMother] where OriginalFabricatedMotherID='{1}' and [SystemID] = '{0}'", Framework.SystemID,FabricatedMotherID);
            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);
            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);

        }
       /// <summary>
       /// 获取拆单序号最大值
       /// Joint
       /// </summary>
       /// <param name="FabricatedMotherID"></param>
       /// <returns></returns>
        public static SFC_FabricatedMother GetMaxSplitSequence(string MoNo)
        {
            string sql = string.Format(
                @"select Top 1 * from [SFC_FabricatedMother] where MoNo='{0}' and [SystemID] = '{1}' ORDER BY SplitSequence DESC", MoNo,Framework.SystemID);
            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);
            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }



        /// <summary>
        /// 制程完工状况分析（制令）-主列表
        /// SAM 2017年7月23日01:07:56
        /// </summary>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartCustCode"></param>
        /// <param name="EndCustCode"></param>
        /// <param name="StartMESUserCode"></param>
        /// <param name="EndMESUserCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00010GetList(
        string StartWorkCenterCode, string EndWorkCenterCode,
        string StartFabMoCode, string EndFabMoCode,
        string StartDate, string EndDate,
        string StartCustCode, string EndCustCode,
        string StartMESUserCode, string EndMESUserCode,
        int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select  A.FabricatedMotherID,
            A.MoNo,A.SplitSequence,A.Date,C.Name as Status,B.Code as ItemCode,B.Name as ItemName,B.Specification as ItemSpecification,
            D.Code as UnitCode,D.Name as UnitName,A.Quantity,A.StartDate,A.FinishDate,
            A.StorageQuantity,A.OrderNo,
            F.UserName,E.Name as CustName ");

            string sql = string.Format(
                @"from [SFC_FabricatedMother] A               
                left join [SYS_Items] B on A.[ItemID] = B.[ItemID]
                left join [SYS_Parameters] C on A.[Status] =C.[ParameterID]
                left join [SYS_Parameters] D on A.[UnitID] =D.[ParameterID]
                left join [SYS_Customers] E on A.[CustomerID] =E.[CustomerID]
                left join [SYS_MESUsers] F on A.[MESUserID] =F.[MESUserID]
                where A.[SystemID] = '{0}' and (A.[Status] = '{0}0201213000029' or A.[Status] = '{0}020121300002A') ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{           
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@EndFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartCustCode",SqlDbType.VarChar),
                new SqlParameter("@EndCustCode",SqlDbType.VarChar),
                new SqlParameter("@StartMESUserCode",SqlDbType.VarChar),
                new SqlParameter("@EndMESUserCode",SqlDbType.VarChar)

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
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@EndFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartCustCode",SqlDbType.VarChar),
                new SqlParameter("@EndCustCode",SqlDbType.VarChar),
                new SqlParameter("@StartMESUserCode",SqlDbType.VarChar),
                new SqlParameter("@EndMESUserCode",SqlDbType.VarChar)
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

            if (!string.IsNullOrWhiteSpace(StartWorkCenterCode))
            {
                sql = sql + String.Format(@" and [FabricatedMotherID] 
                in (select  [FabricatedMotherID] from [SFC_FabMoProcess] 
                where [WorkCenterID] 
                in (select [WorkCenterID] from [SYS_WorkCenter] 
                where Code >= @StartWorkCenterCode and [Status] ='{0}0201213000001' and [SystemID]='{0}'))",Framework.SystemID);
                parameters[0].Value = StartWorkCenterCode;
                Parcount[0].Value = StartWorkCenterCode;
            }

            if (!string.IsNullOrWhiteSpace(EndWorkCenterCode))
            {
                sql = sql + String.Format(@" and [FabricatedMotherID] 
                in (select  [FabricatedMotherID] from [SFC_FabMoProcess] 
                where [WorkCenterID] 
                in (select [WorkCenterID] from [SYS_WorkCenter] 
                where Code <= @EndWorkCenterCode and [Status] ='{0}0201213000001' and [SystemID]='{0}'))", Framework.SystemID);
                parameters[1].Value = EndWorkCenterCode;
                Parcount[1].Value = EndWorkCenterCode;
            }


            if (!string.IsNullOrWhiteSpace(StartFabMoCode))
            {
                sql = sql + String.Format(@" and A.[MoNo] >= @StartFabMoCode ");
                parameters[2].Value = StartFabMoCode;
                Parcount[2].Value = StartFabMoCode;
            }

            if (!string.IsNullOrWhiteSpace(EndFabMoCode))
            {
                sql = sql + String.Format(@" and A.[MoNo] <= @EndFabMoCode ");
                parameters[3].Value = EndFabMoCode;
                Parcount[3].Value = EndFabMoCode;
            }

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + String.Format(@" and A.[FinishDate] >= @StartDate ");
                parameters[4].Value = StartDate;
                Parcount[4].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + String.Format(@" and A.[FinishDate] <= @EndDate ");
                parameters[5].Value = EndDate;
                Parcount[5].Value = EndDate;
            }


            if (!string.IsNullOrWhiteSpace(StartCustCode))
            {
                sql = sql + String.Format(@" and E.[Code] >= @StartCustCode ");
                parameters[6].Value = StartCustCode;
                Parcount[6].Value = StartCustCode;
            }

            if (!string.IsNullOrWhiteSpace(EndCustCode))
            {
                sql = sql + String.Format(@" and E.[Code] <= @EndCustCode ");
                parameters[7].Value = EndCustCode;
                Parcount[7].Value = EndCustCode;
            }

            if (!string.IsNullOrWhiteSpace(StartMESUserCode))
            {
                sql = sql + String.Format(@" and F.[Emplno] >= @StartMESUserCode ");
                parameters[8].Value = StartMESUserCode;
                Parcount[8].Value = StartMESUserCode;
            }

            if (!string.IsNullOrWhiteSpace(EndMESUserCode))
            {
                sql = sql + String.Format(@" and F.[Emplno] <= @EndMESUserCode ");
                parameters[9].Value = EndMESUserCode;
                Parcount[9].Value = EndMESUserCode;
            }


            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[MoNo],A.[SplitSequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

    
        /// <summary>
        /// 制令单的弹窗
        /// SAM 2017年7月25日14:05:51
        /// </summary>
        /// <param name="MoNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00010GetFabricatedMother(string MoNo, int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select  A.FabricatedMotherID,
            A.MoNo,A.SplitSequence,A.Date,C.Name as Status,B.Code as ItemCode,B.Name as ItemName,B.Specification as ItemSpecification,
            D.Code as UnitCode,D.Name as UnitName,A.Quantity,A.StartDate,A.FinishDate,
            A.StorageQuantity,A.OrderNo,
            F.UserName,E.Name as CustName");

            string sql = string.Format(
                @"from [SFC_FabricatedMother] A               
                left join [SYS_Items] B on A.[ItemID] = B.[ItemID]
                left join [SYS_Parameters] C on A.[Status] =C.[ParameterID]
                left join [SYS_Parameters] D on A.[UnitID] =D.[ParameterID]
                left join [SYS_Customers] E on A.[CustomerID] =E.[CustomerID]
                left join [SYS_MESUsers] F on A.[MESUserID] =F.[MESUserID]
                where A.[SystemID] = '{0}' and (A.[Status] = '{0}0201213000029' or A.[Status] = '{0}020121300002A') ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@MoNo",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@MoNo",SqlDbType.VarChar),
            };
            Parcount[0].Value = DBNull.Value;


            if (!string.IsNullOrWhiteSpace(MoNo))
            {
                MoNo = "%" + MoNo.Trim() + "%";
                sql = sql + string.Format(@" and A.[MoNo] collate Chinese_PRC_CI_AS like @MoNo ");
                parameters[0].Value = MoNo;
                Parcount[0].Value = MoNo;
            }


            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[MoNo],A.[SplitSequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// Sfc02的制令单开窗
        /// SAM 2017年8月30日17:00:15
        /// </summary>
        /// <param name="MoNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00002GetFabricatedMother(string MoNo,string ItemName, int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select  A.FabricatedMotherID,
            A.MoNo,A.SplitSequence,A.Date,C.Name as Status,B.Code as ItemCode,B.Name as ItemName,B.Specification as ItemSpecification,
            D.Code as UnitCode,D.Name as UnitName,A.Quantity,A.StartDate,A.FinishDate,
            A.StorageQuantity,A.OrderNo,
            F.UserName,E.Name as CustName");

            string sql = string.Format(
                @"from [SFC_FabricatedMother] A               
                left join [SYS_Items] B on A.[ItemID] = B.[ItemID]
                left join [SYS_Parameters] C on A.[Status] =C.[ParameterID]
                left join [SYS_Parameters] D on A.[UnitID] =D.[ParameterID]
                left join [SYS_Customers] E on A.[CustomerID] =E.[CustomerID]
                left join [SYS_MESUsers] F on A.[MESUserID] =F.[MESUserID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003'", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@MoNo",SqlDbType.VarChar),
                new SqlParameter("@ItemName",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@MoNo",SqlDbType.VarChar),
                new SqlParameter("@ItemName",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(MoNo))
            {
                MoNo = "%" + MoNo.Trim() + "%";
                sql = sql + string.Format(@" and A.[MoNo] collate Chinese_PRC_CI_AS like @MoNo ");
                parameters[0].Value = MoNo;
                Parcount[0].Value = MoNo;
            }

            if (!string.IsNullOrWhiteSpace(ItemName))
            {
                ItemName = "%" + ItemName.Trim() + "%";
                sql = sql + string.Format(@" and B.[Name] collate Chinese_PRC_CI_AS like @ItemName ");
                parameters[1].Value = ItemName;
                Parcount[1].Value = ItemName;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[MoNo],A.[SplitSequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }
        
        /// <summary>
        /// Sfc20的制令单开窗
        /// Mouse 2017年9月26日14:48:32
        /// </summary>
        /// <param name="MoNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00020GetFabricatedMother(string MoNo,string ItemName, int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select  A.FabricatedMotherID,
            A.MoNo,A.SplitSequence,B.Code as ItemCode,B.Name as ItemName,B.Specification as ItemSpecification,A.Status");

            string sql = string.Format(
                @"from [SFC_FabricatedMother] A               
                left join [SYS_Items] B on A.[ItemID] = B.[ItemID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}020121300002B' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@MoNo",SqlDbType.VarChar),
                new SqlParameter("@ItemName",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@MoNo",SqlDbType.VarChar),
                new SqlParameter("@ItemName",SqlDbType.VarChar),
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(MoNo))
            {
                MoNo = "%" + MoNo + "%";
                sql = sql + string.Format(@" and A.[MoNo] collate Chinese_PRC_CI_AS like @MoNo ");
                parameters[0].Value = MoNo;
                Parcount[0].Value = MoNo;
            }
            if (!string.IsNullOrWhiteSpace(ItemName))
            {
                ItemName = "%" + ItemName + "%";
                sql = sql + string.Format(@"and B.[Name] collate Chinese_PRC_CI_AS like @ItemName");
                parameters[1].Value = ItemName;
                Parcount[1].Value = ItemName;
            }
            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[MoNo],A.[SplitSequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }
        /// <summary>
        /// 判断指定客户是否在指令单使用
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool CheckCust(string customerID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_FabricatedMother] 
            where  [SystemID] = '{0}' and [Status] <> '{0}0201213000003' and [CustomerID] = '{1}'"
            , Framework.SystemID, customerID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// 製令直通率分析
        /// SAM 2017年9月3日21:22:38
        /// 获取已结案的制令单
        /// </summary>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartItemCode"></param>
        /// <param name="EndItemCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00017GetList(string StartFabMoCode, string EndFabMoCode,
         string StartItemCode, string EndItemCode, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.FabricatedMotherID,A.MoNo+'-'+convert(varchar(20),A.SplitSequence) as MoNo,
                A.Date,B.Code,B.Name,B.Specification ");

            string sql = string.Format(
                @"from [SFC_FabricatedMother] A
                   left join [SYS_Items] B on A.[ItemID] = B.[ItemID]
                  where A.[SystemID] = '{0}' and A.[Status] = '{0}020121300002A' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@EndFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@StartItemCode",SqlDbType.VarChar),
                new SqlParameter("@EndItemCode",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@StartFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@EndFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@StartItemCode",SqlDbType.VarChar),
                new SqlParameter("@EndItemCode",SqlDbType.VarChar),           
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartFabMoCode))
            {
                sql = sql + String.Format(@" and A.[MoNo] >= @StartFabMoCode ");
                parameters[0].Value = StartFabMoCode;
                Parcount[0].Value = StartFabMoCode;
            }

            if (!string.IsNullOrWhiteSpace(EndFabMoCode))
            {
                sql = sql + String.Format(@" and A.[MoNo] <= @EndFabMoCode ");
                parameters[1].Value = EndFabMoCode;
                Parcount[1].Value = EndFabMoCode;
            }

            if (!string.IsNullOrWhiteSpace(StartItemCode))
            {
                sql = sql + String.Format(@" and B.[Code] >= @StartItemCode ");
                parameters[2].Value = StartItemCode;
                Parcount[2].Value = StartItemCode;
            }

            if (!string.IsNullOrWhiteSpace(EndItemCode))
            {
                sql = sql + String.Format(@" and B.[Code] <= @EndItemCode ");
                parameters[3].Value = EndItemCode;
                Parcount[3].Value = EndItemCode;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[MoNo],A.[SplitSequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 製品生產工時分析-制品页签
        /// SAM 2017年9月3日23:41:50
        /// </summary>
        /// <param name="StartItemCode"></param>
        /// <param name="EndItemCode"></param>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00019ItemGetList(string StartItemCode, string EndItemCode, 
            string StartFabMoCode, string EndFabMoCode,
             string StartDate, string EndDate,
                int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.FabricatedMotherID,A.Date,C.FabMoProcessID, A.MoNo+'-'+cast(A.SplitSequence as varchar) as MoNo,
                B.Code as ItemCode,B.Name as ItemName,B.Specification as ItemSpecification,ISNULL(C.StandardTime,0) as StandardTime,
                C.Sequence,D.Code as ProcessCode,D.Name as ProcessName,null ActualHour,null as DifferenceHour,C.StandardTime/3600 as StandardHour ");

            string sql = string.Format(
                @"from [SFC_FabricatedMother] A
                   left join [SYS_Items] B on A.[ItemID] = B.[ItemID]
                   left join [SFC_FabMoProcess] C on A.[FabricatedMotherID] = C.[FabricatedMotherID] 
                   left join [SYS_Parameters] D on C.[ProcessID] = D.[ParameterID]
                  where A.[SystemID] = '{0}' and A.[Status] = '{0}020121300002A' and C.[Status] = '{0}020121300002A'", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@EndFabMoCode",SqlDbType.VarChar),
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
                new SqlParameter("@StartFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@EndFabMoCode",SqlDbType.VarChar),
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

            if (!string.IsNullOrWhiteSpace(StartFabMoCode))
            {
                sql = sql + String.Format(@" and A.[MoNo] >= @StartFabMoCode ");
                parameters[0].Value = StartFabMoCode;
                Parcount[0].Value = StartFabMoCode;
            }

            if (!string.IsNullOrWhiteSpace(EndFabMoCode))
            {
                sql = sql + String.Format(@" and A.[MoNo] <= @EndFabMoCode ");
                parameters[1].Value = EndFabMoCode;
                Parcount[1].Value = EndFabMoCode;
            }

            if (!string.IsNullOrWhiteSpace(StartItemCode))
            {
                sql = sql + String.Format(@" and B.[Code] >= @StartItemCode ");
                parameters[2].Value = StartItemCode;
                Parcount[2].Value = StartItemCode;
            }

            if (!string.IsNullOrWhiteSpace(EndItemCode))
            {
                sql = sql + String.Format(@" and B.[Code] <= @EndItemCode ");
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


            count = UniversalService.getCount(sql, Parcount);

            string orderby = "B.[Code],A.[MoNo],A.[SplitSequence],C.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// sfc00017的制令单号弹窗
        /// SAM 2017年10月11日17:40:28
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="MoNo"></param>
        /// <param name="ItemName"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00017GetFabMoList(string MoNo, string ItemName, int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select  A.FabricatedMotherID,
            A.MoNo,A.SplitSequence,A.Date,C.Name as Status,B.Code as ItemCode,B.Name as ItemName,B.Specification as ItemSpecification,
            D.Code as UnitCode,D.Name as UnitName,A.Quantity,A.StartDate,A.FinishDate,
            A.StorageQuantity,A.OrderNo,
            F.UserName,E.Name as CustName");

            string sql = string.Format(
                @"from [SFC_FabricatedMother] A               
                left join [SYS_Items] B on A.[ItemID] = B.[ItemID]
                left join [SYS_Parameters] C on A.[Status] =C.[ParameterID]
                left join [SYS_Parameters] D on A.[UnitID] =D.[ParameterID]
                left join [SYS_Customers] E on A.[CustomerID] =E.[CustomerID]
                left join [SYS_MESUsers] F on A.[MESUserID] =F.[MESUserID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003'", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@MoNo",SqlDbType.VarChar),
                new SqlParameter("@ItemName",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@MoNo",SqlDbType.VarChar),
                new SqlParameter("@ItemName",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(MoNo))
            {
                sql = sql + string.Format(@" and A.[MoNo] collate Chinese_PRC_CI_AS > @MoNo ");
                parameters[0].Value = MoNo.Trim();
                Parcount[0].Value = MoNo.Trim();
            }

            if (!string.IsNullOrWhiteSpace(ItemName))
            {
                ItemName = "%" + ItemName.Trim() + "%";
                sql = sql + string.Format(@" and B.[Name] collate Chinese_PRC_CI_AS like @ItemName ");
                parameters[1].Value = ItemName;
                Parcount[1].Value = ItemName;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[MoNo],A.[SplitSequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取制令单列表
        /// SAM 2017年10月20日08:45:51 
        /// 状态为ca/作废 的数据不抓取
        /// MoNo为单个其实区间查询
        /// </summary>
        /// <param name="MoNo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> SfcGetFabMoList(string MoNo,string ItemName, int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select  A.FabricatedMotherID,A.MoNo,A.SplitSequence,A.Date,C.Name as Status,
             A.MoNo+'-'+cast(A.SplitSequence as varchar) as MoCode,
            B.Code as ItemCode,B.Name as ItemName,B.Specification as ItemSpecification,
            D.Code as UnitCode,D.Name as UnitName,A.Quantity,A.StartDate,A.FinishDate,
            A.StorageQuantity,A.OrderNo,
            F.UserName,E.Name as CustName");

            string sql = string.Format(
                @"from [SFC_FabricatedMother] A               
                left join [SYS_Items] B on A.[ItemID] = B.[ItemID]
                left join [SYS_Parameters] C on A.[Status] =C.[ParameterID]
                left join [SYS_Parameters] D on A.[UnitID] =D.[ParameterID]
                left join [SYS_Customers] E on A.[CustomerID] =E.[CustomerID]
                left join [SYS_MESUsers] F on A.[MESUserID] =F.[MESUserID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003'  and A.[Status] <> '{0}020121300002B'", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@MoNo",SqlDbType.VarChar),
                new SqlParameter("@ItemName",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@MoNo",SqlDbType.VarChar),
                 new SqlParameter("@ItemName",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(MoNo))
            {
                sql = sql + string.Format(@" and A.[MoNo] collate Chinese_PRC_CI_AS > @MoNo ");
                parameters[0].Value = MoNo.Trim();
                Parcount[0].Value = MoNo.Trim();
            }

            if (!string.IsNullOrWhiteSpace(ItemName))
            {
                sql = sql + string.Format(@" and B.[Name] collate Chinese_PRC_CI_AS > @ItemName ");
                parameters[1].Value = ItemName.Trim();
                Parcount[1].Value = ItemName.Trim();
            }
            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[MoNo],A.[SplitSequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取指定状态的制令单列表
        /// SAM 2017年10月20日15:40:03
        /// </summary>
        /// <param name="Status">指定状态</param>
        /// <param name="MoNo">MoNo为单个起始区间查询，不包括本身</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> SfcStatusGetFabMoList(string Status,string MoNo, int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select  A.FabricatedMotherID,A.MoNo,A.SplitSequence,A.Date,C.Name as Status,
             A.MoNo+'-'+cast(A.SplitSequence as varchar) as MoCode,
            B.Code as ItemCode,B.Name as ItemName,B.Specification as ItemSpecification,
            D.Code as UnitCode,D.Name as UnitName,A.Quantity,A.StartDate,A.FinishDate,
            A.StorageQuantity,A.OrderNo,
            F.UserName,E.Name as CustName");

            string sql = string.Format(
                @"from [SFC_FabricatedMother] A               
                left join [SYS_Items] B on A.[ItemID] = B.[ItemID]
                left join [SYS_Parameters] C on A.[Status] =C.[ParameterID]
                left join [SYS_Parameters] D on A.[UnitID] =D.[ParameterID]
                left join [SYS_Customers] E on A.[CustomerID] =E.[CustomerID]
                left join [SYS_MESUsers] F on A.[MESUserID] =F.[MESUserID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003'  and A.[Status] = '{1}'", Framework.SystemID, Status);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@MoNo",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@MoNo",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(MoNo))
            {
                sql = sql + string.Format(@" and A.[MoNo] collate Chinese_PRC_CI_AS > @MoNo ");
                parameters[0].Value = MoNo.Trim();
                Parcount[0].Value = MoNo.Trim();
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[MoNo],A.[SplitSequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

    }
}

