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
    public class SFC_FabMoProcessService : SuperModel<SFC_FabMoProcess>
    {
        /// <summary>
        /// ����
        /// SAM 2017��7��13��23:51:18
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SFC_FabMoProcess Model)
        {
            try
            {
                SYS_WorkCenter wc = SYS_WorkCenterService.get(Model.WorkCenterID);
                if (wc.InoutMark == Framework.SystemID + "020121300002E")
                {
                    SYS_Parameters process = SYS_ParameterService.get(Model.ProcessID);
                    Model.IsEnableOperation = process.IsDefault;
                }
                else
                {
                    Model.IsEnableOperation = false;
                }
                string sql = string.Format(
                    @"insert[SFC_FabMoProcess]([FabMoProcessID],[FabricatedMotherID],[ProcessID],[Sequence],
                    [WorkCenterID],[StartDate],[FinishDate],[SeparateQuantity],[OrderQuantity],
                    [FinProQuantity],[OutProQuantity],[ScrappedQuantity],[DifferenceQuantity],
                    [PreProQuantity],[AssignQuantity],[UnitID],[UnitRate],[StandardTime],
                    [PrepareTime],[Status],[BeginDate],[EndDate],[TaskNo],[OriginalFabMoProcessID],
                    [Comments],[IsEnableOperation],[Price],[SourceID],[Quantity],
                    [Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values(
                    @FabMoProcessID,@FabricatedMotherID,
                    @ProcessID,@Sequence,@WorkCenterID,
                    @StartDate,@FinishDate,
                    @SeparateQuantity,@OrderQuantity,@FinProQuantity,
                    @OutProQuantity,@ScrappedQuantity,@DifferenceQuantity,
                    @PreProQuantity,@AssignQuantity,@UnitID,
                    @UnitRate,@StandardTime,@PrepareTime,
                    @Status,@BeginDate,@EndDate,
                    @TaskNo,@OriginalFabMoProcessID,@Comments,@IsEnableOperation,@Price,@SourceID,@Quantity,
                    '{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@FabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@FabricatedMotherID",SqlDbType.VarChar),
                    new SqlParameter("@ProcessID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.VarChar),
                    new SqlParameter("@WorkCenterID",SqlDbType.VarChar),
                    new SqlParameter("@StartDate",SqlDbType.DateTime),
                    new SqlParameter("@FinishDate",SqlDbType.DateTime),
                    new SqlParameter("@SeparateQuantity",SqlDbType.Decimal),
                    new SqlParameter("@OrderQuantity",SqlDbType.Decimal),
                    new SqlParameter("@FinProQuantity",SqlDbType.Decimal),
                    new SqlParameter("@OutProQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ScrappedQuantity",SqlDbType.Decimal),
                    new SqlParameter("@DifferenceQuantity",SqlDbType.Decimal),
                    new SqlParameter("@PreProQuantity",SqlDbType.Decimal),
                    new SqlParameter("@AssignQuantity",SqlDbType.Decimal),
                    new SqlParameter("@UnitID",SqlDbType.VarChar),
                    new SqlParameter("@UnitRate",SqlDbType.Decimal),
                    new SqlParameter("@StandardTime",SqlDbType.Int),
                    new SqlParameter("@PrepareTime",SqlDbType.Int),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@BeginDate",SqlDbType.DateTime),
                    new SqlParameter("@EndDate",SqlDbType.DateTime),
                    new SqlParameter("@TaskNo",SqlDbType.VarChar),
                    new SqlParameter("@OriginalFabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@IsEnableOperation", SqlDbType.Bit),
                    new SqlParameter("@Price", SqlDbType.Decimal),
                    new SqlParameter("@SourceID", SqlDbType.VarChar),
                    new SqlParameter("@Quantity", SqlDbType.Decimal),
                    };
                parameters[0].Value = (Object)Model.FabMoProcessID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.FabricatedMotherID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ProcessID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[4].Value = (Object)Model.WorkCenterID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.StartDate ?? DBNull.Value;
                parameters[6].Value = (Object)Model.FinishDate ?? DBNull.Value;
                parameters[7].Value = (Object)Model.SeparateQuantity ?? 0;
                parameters[8].Value = (Object)Model.OrderQuantity ?? 0;
                parameters[9].Value = (Object)Model.FinProQuantity ?? 0;
                parameters[10].Value = (Object)Model.OutProQuantity ?? 0;
                parameters[11].Value = (Object)Model.ScrappedQuantity ?? 0;
                parameters[12].Value = (Object)Model.DifferenceQuantity ?? 0;
                parameters[13].Value = (Object)Model.PreProQuantity ?? 0;
                parameters[14].Value = (Object)Model.AssignQuantity ?? 0;
                parameters[15].Value = (Object)Model.UnitID ?? DBNull.Value;
                parameters[16].Value = (Object)Model.UnitRate ?? 0;
                parameters[17].Value = (Object)Model.StandardTime ?? DBNull.Value;
                parameters[18].Value = (Object)Model.PrepareTime ?? DBNull.Value;
                parameters[19].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[20].Value = (Object)Model.BeginDate ?? DBNull.Value;
                parameters[21].Value = (Object)Model.EndDate ?? DBNull.Value;
                parameters[22].Value = (Object)Model.TaskNo ?? DBNull.Value;
                parameters[23].Value = (Object)Model.OriginalFabMoProcessID ?? DBNull.Value;
                parameters[24].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[25].Value = (Object)Model.IsEnableOperation ?? false;
                parameters[26].Value = (Object)Model.Price ?? DBNull.Value;
                parameters[27].Value = (Object)Model.SourceID ?? DBNull.Value;
                parameters[28].Value = (Object)Model.Quantity ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// ����
        /// SAM 2017��7��13��11:42:30
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SFC_FabMoProcess Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_FabMoProcess] set {0},
                [ProcessID]=@ProcessID,[Sequence]=@Sequence,[Quantity]=@Quantity,
                [WorkCenterID]=@WorkCenterID,[StartDate]=@StartDate,[FinishDate]=@FinishDate,[Price]=@Price,
                [SeparateQuantity]=@SeparateQuantity,[OrderQuantity]=@OrderQuantity,[FinProQuantity]=@FinProQuantity,
                [OutProQuantity]=@OutProQuantity,[ScrappedQuantity]=@ScrappedQuantity,
                [DifferenceQuantity]=@DifferenceQuantity,[PreProQuantity]=@PreProQuantity,[AssignQuantity]=@AssignQuantity,
                [UnitID]=@UnitID,[UnitRate]=@UnitRate,[StandardTime]=@StandardTime,[PrepareTime]=@PrepareTime,
                [Status]=@Status,[BeginDate]=@BeginDate,[EndDate]=@EndDate,[TaskNo]=@TaskNo,
                [OriginalFabMoProcessID]=@OriginalFabMoProcessID,[Comments]=@Comments,[RepairQuantity]=@RepairQuantity
                where [FabMoProcessID]=@FabMoProcessID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@FabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@ProcessID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.VarChar),
                    new SqlParameter("@WorkCenterID",SqlDbType.VarChar),
                    new SqlParameter("@StartDate",SqlDbType.DateTime),
                    new SqlParameter("@FinishDate",SqlDbType.DateTime),
                    new SqlParameter("@SeparateQuantity",SqlDbType.Decimal),
                    new SqlParameter("@OrderQuantity",SqlDbType.Decimal),
                    new SqlParameter("@FinProQuantity",SqlDbType.Decimal),
                    new SqlParameter("@OutProQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ScrappedQuantity",SqlDbType.Decimal),
                    new SqlParameter("@DifferenceQuantity",SqlDbType.Decimal),
                    new SqlParameter("@PreProQuantity",SqlDbType.Decimal),
                    new SqlParameter("@AssignQuantity",SqlDbType.Decimal),
                    new SqlParameter("@UnitID",SqlDbType.VarChar),
                    new SqlParameter("@UnitRate",SqlDbType.Decimal),
                    new SqlParameter("@StandardTime",SqlDbType.Int),
                    new SqlParameter("@PrepareTime",SqlDbType.Int),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@BeginDate",SqlDbType.DateTime),
                    new SqlParameter("@EndDate",SqlDbType.DateTime),
                    new SqlParameter("@TaskNo",SqlDbType.VarChar),
                    new SqlParameter("@OriginalFabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@Price",SqlDbType.NVarChar),
                    new SqlParameter("@Quantity",SqlDbType.Decimal),
                    new SqlParameter("@RepairQuantity",SqlDbType.Decimal)
                    };

                parameters[0].Value = Model.FabMoProcessID;
                parameters[1].Value = (Object)Model.ProcessID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.WorkCenterID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.StartDate ?? DBNull.Value;
                parameters[5].Value = (Object)Model.FinishDate ?? DBNull.Value;
                parameters[6].Value = (Object)Model.SeparateQuantity ?? DBNull.Value;
                parameters[7].Value = (Object)Model.OrderQuantity ?? DBNull.Value;
                parameters[8].Value = (Object)Model.FinProQuantity ?? DBNull.Value;
                parameters[9].Value = (Object)Model.OutProQuantity ?? DBNull.Value;
                parameters[10].Value = (Object)Model.ScrappedQuantity ?? DBNull.Value;
                parameters[11].Value = (Object)Model.DifferenceQuantity ?? DBNull.Value;
                parameters[12].Value = (Object)Model.PreProQuantity ?? DBNull.Value;
                parameters[13].Value = (Object)Model.AssignQuantity ?? DBNull.Value;
                parameters[14].Value = (Object)Model.UnitID ?? DBNull.Value;
                parameters[15].Value = (Object)Model.UnitRate ?? DBNull.Value;
                parameters[16].Value = (Object)Model.StandardTime ?? DBNull.Value;
                parameters[17].Value = (Object)Model.PrepareTime ?? DBNull.Value;
                parameters[18].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[19].Value = (Object)Model.BeginDate ?? DBNull.Value;
                parameters[20].Value = (Object)Model.EndDate ?? DBNull.Value;
                parameters[21].Value = (Object)Model.TaskNo ?? DBNull.Value;
                parameters[22].Value = (Object)Model.OriginalFabMoProcessID ?? DBNull.Value;
                parameters[23].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[24].Value = (Object)Model.Price ?? DBNull.Value;
                parameters[25].Value = (Object)Model.Quantity ?? DBNull.Value;
                parameters[26].Value = (Object)Model.RepairQuantity ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// ��������޸����������е������Ƴ�״̬
        /// SAM 2017��7��13��10:02:20
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="FabricatedMotherID"></param>
        /// <param name="Status"></param>
        public static bool updateByFabMo(string userId, string FabricatedMotherID, string Status)
        {
            try
            {
                string sql = String.Format(@"update [SFC_FabMoProcess] set {0},
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


        public static SFC_FabMoProcess get(string FabMoProcessID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_FabMoProcess] where [FabMoProcessID] = '{0}'  and [SystemID] = '{1}' ", FabMoProcessID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        public static bool delete(string FabMoProcessID)
        {
            try
            {
                string sql = string.Format(@"delete from [SFC_FabMoProcess] where [FabMoProcessID] = '{0}'  and [SystemID] = '{1}' ", FabMoProcessID, Framework.SystemID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }

            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static bool delete(string uid, SFC_FabMoProcess m)
        {
            try
            {
                string orgID = m.FabMoProcessID;
                m = get(orgID);
                if (m != null)
                {
                    m.Status = Framework.SystemID + "0201213000003";
                    return update(uid, m);
                }
                return true;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static bool CheckInsertArgs(SFC_FabMoProcess model)
        {
            string sql = string.Format(
                @"from SFC_FabMoProcess
                  where FabricatedMotherID = '{0}' and ProcessID = '{1}' and Status <> '{2}0201213000003'",
                model.FabricatedMotherID, model.ProcessID, Framework.SystemID);

            return UniversalService.getCount(sql, null) <= 0;
        }

        public static bool CheckUpdateArgs(SFC_FabMoProcess model)
        {
            string sql = string.Format(
                @"from SFC_FabMoProcess
                  where FabricatedMotherID = '{0}' and ProcessID = '{1}' and
                        FabMoProcessID <> '{2}' and Status <> '{3}0201213000003'",
                model.FabricatedMotherID, model.ProcessID, model.FabMoProcessID, Framework.SystemID);

            return UniversalService.getCount(sql, null) <= 0;
        }

        /// <summary>
        /// ��ȡ�����Ƴ��б�
        /// Tom 
        /// </summary>
        /// <param name="fabricatedMotherID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00002FabricatedProcessList(string fabricatedMotherID, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.FabMoProcessID, A.FabricatedMotherID, A.PreProQuantity,
                         B.MoNo, H.Code as ItemCode, H.Name as ItemName, H.Specification,
                         B.Quantity, A.UnitID, I.Name as Unit, A.Sequence, C.Code as ProcessCode, C.Name as ProcessName, A.ProcessID,
                         A.WorkCenterID, D.Code as WorkCenterCode, D.Name as WorkCenterName, D.InoutMark, 
                        (CASE WHEN D.InoutMark='{0}020121300002E' THEN (Select [Name] from [SYS_Organization] where [OrganizationID] = D.DepartmentID) ELSE (Select [Name] from [SYS_Manufacturers] where [ManufacturerID] = D.DepartmentID) END) as OrganizationName,                      
                         A.StartDate, A.FinishDate, D.ResourceReport,A.Quantity as AfterSeparateQuantity,
                         A.FinProQuantity, A.OutProQuantity, A.DifferenceQuantity, A.ScrappedQuantity, A.UnitRate,A.OriginalFabMoProcessID,
                         A.StandardTime, A.PrepareTime, A.Status, convert(varchar(8),dateadd(ss,A.StandardTime,108),108) as StandardTimeStr,
                         convert(varchar(8),dateadd(ss,A.PrepareTime,108),108) as PrepareTimeStr, A.Comments, A.IsEnableOperation,
                         F.UserName as Modifier, G.UserName as Creator, 
                         A.CreateLocalTime as CreateTime, A.ModifiedLocalTime as ModifiedTime", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_FabMoProcess] A
                  left join [SFC_FabricatedMother] B on A.FabricatedMotherID = B.FabricatedMotherID
                  left join [SYS_Parameters] C on A.ProcessID = C.ParameterID
                  left join [SYS_WorkCenter] D on A.WorkCenterID = D.WorkCenterID
                  left join [SYS_Organization] E on B.OrganizationID = E.OrganizationID
                  left join [SYS_MESUsers] F on A.Modifier = F.MESUserID
                  left join [SYS_MESUsers] G on A.Creator = G.MESUserID
                  left join [SYS_Items] H on B.ItemID = H.ItemID
                  left join [SYS_Parameters] I on A.UnitID = I.ParameterID
                  where A.[SystemID] = '{0}' and  A.FabricatedMotherID = @FabricatedMotherID and
                        A.Status <> '{0}0201213000003'", Framework.SystemID);

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FabricatedMotherID", fabricatedMotherID));

            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);

            string orderby = "A.[CreateLocalTime]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// �����-��ȡ����µ��Ƴ̵���
        /// SAM 2017��7��14��15:54:22
        /// </summary>
        /// <param name="fabricatedMotherID"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> SfcGetFabMoProcessList(string fabricatedMotherID, string Code, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.FabMoProcessID, A.FabricatedMotherID, B.MoNo, H.Code as ItemCode, H.Name as ItemName, H.Specification,
                         B.Quantity, A.UnitID, I.Name as Unit, A.Sequence, C.Code as ProcessCode, C.Name as ProcessName, A.ProcessID,
                         A.WorkCenterID, D.Code as WorkCenterCode, D.Name as WorkCenterName, D.InoutMark, E.Name as OrganizationName,
                         A.StartDate, A.FinishDate, D.ResourceReport,A.Quantity as AfterSeparateQuantity,
                         A.FinProQuantity, A.OutProQuantity, A.DifferenceQuantity, A.ScrappedQuantity, A.UnitRate,
                         A.StandardTime, A.PrepareTime, A.Status, convert(varchar(8),dateadd(ss,A.StandardTime,108),108) as StandardTimeStr,
                         convert(varchar(8),dateadd(ss,A.PrepareTime,108),108) as PrepareTimeStr, A.Comments, A.IsEnableOperation,
                         F.UserName as Modifier, G.UserName as Creator, 
                         A.CreateLocalTime as CreateTime, A.ModifiedLocalTime as ModifiedTime", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_FabMoProcess] A
                  left join [SFC_FabricatedMother] B on A.FabricatedMotherID = B.FabricatedMotherID
                  left join [SYS_Parameters] C on A.ProcessID = C.ParameterID
                  left join [SYS_WorkCenter] D on A.WorkCenterID = D.WorkCenterID
                  left join [SYS_Organization] E on B.OrganizationID = E.OrganizationID
                  left join [SYS_MESUsers] F on A.Modifier = F.MESUserID
                  left join [SYS_MESUsers] G on A.Creator = G.MESUserID
                  left join [SYS_Items] H on B.ItemID = H.ItemID
                  left join [SYS_Parameters] I on A.UnitID = I.ParameterID
                  where A.[SystemID] = '{0}' and  A.FabricatedMotherID = @FabricatedMotherID and
                        A.Status <> '{0}0201213000003'", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(Code))
                sql += string.Format("C.Code like '%{0}%'", Code);


            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FabricatedMotherID", fabricatedMotherID));

            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);

            string orderby = "A.[CreateLocalTime]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ��ȡ�������б�
        /// Tom
        /// </summary>
        /// <param name="OriginalFabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetSplitList(string OriginalFabMoProcessID, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.FabMoProcessID, A.FabricatedMotherID, A.Sequence, A.ProcessID,
                        C.Code as ProcessCode, C.Name as ProcessName, 
                        A.WorkCenterID, D.Code as WorkCenterCode, D.Name as WorkCenterName, 
                        (Select Name from [SYS_Parameters] where ParameterID = D.InoutMark) as InoutMark, 
                        (CASE WHEN D.InoutMark='{0}020121300002F' THEN 'False' ELSE C.IsDefault END) as IsOperation,
                        (CASE WHEN D.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = D.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = D.DepartmentID) END) as DeptCode,
                        (CASE WHEN D.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = D.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = D.DepartmentID) END) as DeptName,            
                        A.Price, A.SeparateQuantity, A.UnitID, I.Name as Unit, A.UnitRate, A.StartDate, A.FinishDate,
                        A.StandardTime, A.PrepareTime,A.Quantity,
                        convert(varchar(8),dateadd(ss,A.StandardTime,108),108) as StandardTimeStr,
                        convert(varchar(8),dateadd(ss,A.PrepareTime,108),108) as PrepareTimeStr,
                        F.UserName as Modifier, G.UserName as Creator, 
                        A.CreateLocalTime as CreateTime, A.ModifiedLocalTime as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_FabMoProcess] A
                  left join [SFC_FabricatedMother] B on A.FabricatedMotherID = B.FabricatedMotherID
                  left join [SYS_Parameters] C on A.ProcessID = C.ParameterID
                  left join [SYS_WorkCenter] D on A.WorkCenterID = D.WorkCenterID
                  left join [SYS_MESUsers] F on A.Modifier = F.MESUserID
                  left join [SYS_MESUsers] G on A.Creator = G.MESUserID
                  left join [SYS_Parameters] I on A.UnitID = I.ParameterID
                  where A.[SystemID] = '{0}' and  A.OriginalFabMoProcessID = '{1}' and
                        A.Status <> '{0}0201213000003'", Framework.SystemID, OriginalFabMoProcessID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[CreateLocalTime]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        public static bool CheckSplitUpdateArgs(SFC_FabMoProcess model)
        {
            string sql = string.Format(
                @"from SFC_FabMoRelationship
                  where FabMoProcessID = '{0}' and OriginalFabMoProcessID = '{1}' and Status <> '{2}0201213000003'",
                model.FabMoProcessID, model.OriginalFabMoProcessID, Framework.SystemID);

            return UniversalService.getCount(sql, null) <= 0;
        }

        public static IList<Hashtable> GetUnSpiltList(int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.FabMoProcessID, A.FabricatedMotherID, A.Sequence, C.Code as ProcessCode, C.Name as ProcessName, 
                         A.WorkCenterID, D.Code as WorkCenterCode, D.Name as WorkCenterName, D.InoutMark, E.Name as OrganizationName,
                         A.Price, A.SeparateQuantity, A.UnitID, I.Name as Unit, A.UnitRate, A.StartDate, A.FinishDate, A.StandardTime, 
                         A.PrepareTime, 
                         F.UserName as Modifier, G.UserName as Creator, 
                        A.CreateLocalTime as CreateTime, A.ModifiedLocalTime as ModifiedTime",
                Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_FabMoProcess] A
                  left join SFC_FabricatedMother B on A.FabricatedMotherID = B.FabricatedMotherID
                  left join [SYS_Parameters] C on A.ProcessID = C.ParameterID
                  left join SYS_WorkCenter D on A.WorkCenterID = D.WorkCenterID
                  left join SYS_Organization E on B.OrganizationID = E.OrganizationID
                  left join SYS_MESUsers F on A.Modifier = F.MESUserID
                  left join SYS_MESUsers G on A.Creator = G.MESUserID
                  left join SFC_FabMoRelationship H on A.FabMoProcessID = H.FabMoProcessID and H.Stauts = '{0}0201213000001'
                  where A.[SystemID] = '{0}' and  A.OriginalFabMoProcessID = null and
                        A.Status = '{0}0201213000001'", Framework.SystemID);

            string orderby = "A.[CreateLocalTime]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// �����������ȡ�����Ƴ��б�
        /// SAM 2017��7��13��22:07:10
        /// </summary>
        /// <param name="FabricatedMotherID"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetBomList(string FabricatedMotherID)
        {
            string sql = string.Format(
                @"select FabMoProcessID,null as Parenter,null as FabMoOperationID,A.Sequence,A.WorkCenterID,
                A.Sequence+' '+(select [Code] from [SYS_Parameters] where A.[ProcessID] = [ParameterID])+' '+(select [Name] from [SYS_Parameters] where A.[ProcessID] = [ParameterID])+' '+
                (select [Name] from [SYS_WorkCenter] where A.[WorkCenterID] = [WorkCenterID]) as Value
                  from [SFC_FabMoProcess] A       
                  where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and A.[FabricatedMotherID] ='{1}'", Framework.SystemID, FabricatedMotherID);

            string orderby = " order by A.[Sequence]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }
        /// <summary>
        /// ���������ˮ�Ż�ȡ�Ƴ���С(����Ϊ1)����Ʒ�Ƴ�
        /// Mouse 2017��9��8��16:53:36
        /// </summary>
        /// <param name="FabricatedMotherID"></param>
        /// <returns></returns>
        public static SFC_FabMoProcess GetMinProcess(string FabricatedMotherID)
        {
            string sql = string.Format(@"select * from [SFC_FabMoProcess] where [SystemID]='{0}' and [FabricatedMotherID]='{1}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID, FabricatedMotherID);

            string orderby = " order by A.[Sequence] ";
            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);


            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return ToEntity(dt.Rows[0]);
            }
        }

        /// <summary>
        /// �����������ȡ�����Ƴ��б�
        /// Joint
        /// </summary>
        /// <param name="FabricatedMotherID"></param>
        /// <returns></returns>
        public static List<SFC_FabMoProcess> GetFabMoProcessList(string FabricatedMotherID)
        {
            string sql = string.Format(
                @"select * from [SFC_FabMoProcess]        
                  where [SystemID] = '{0}' and [Status] <> '{0}0201213000003' and [FabricatedMotherID] ='{1}'", Framework.SystemID, FabricatedMotherID);

            string orderby = " order by [Sequence]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }

        /// <summary>
        /// ���������Ƴ���ˮ�Ż�ȡ�����Ƴ���Ϣ
        /// SAM 2017��7��17��17:05:54
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static Hashtable Sfc00002GetFabMoProcess(string FabMoProcessID)
        {
            string select = string.Format(
                 @"select Top 1 A.FabMoProcessID, A.FabricatedMotherID, B.MoNo, null as OperationID,
                         H.Code as ItemCode, H.Name as ItemName, H.Specification,
                         B.Quantity, A.UnitID, I.Name as Unit, A.Sequence, B.OrderQuantity,A.PreProQuantity,
                         C.Code as ProcessCode, C.Name as ProcessName, A.ProcessID,
                         A.WorkCenterID, D.Code as WorkCenterCode, D.Name as WorkCenterName, 
                         D.InoutMark, E.Name as OrganizationName,
                         A.StartDate, A.FinishDate, D.ResourceReport, 
                         A.Quantity as AfterSeparateQuantity, 
                         (A.Quantity - A.AssignQuantity) as NotAssignQuantity, 
                         A.FinProQuantity, A.OutProQuantity, A.DifferenceQuantity, A.ScrappedQuantity, A.UnitRate,H.ItemID,A.AssignQuantity,
                        (Select ISnull(SUM(DispatchQuantity),0) from [SFC_TaskDispatch] where [FabMoProcessID] =A.[FabMoProcessID] and [Status]='{0}0201213000087') as NAAssignQuantity,
                         A.StandardTime, A.PrepareTime, A.Status, convert(varchar(8),dateadd(ss,A.StandardTime,108),108) as StandardTimeStr,
                         convert(varchar(8),dateadd(ss,A.PrepareTime,108),108) as PrepareTimeStr, A.Comments, A.IsEnableOperation,
                         F.UserName as Modifier, G.UserName as Creator, 
                         A.CreateLocalTime as CreateTime, A.ModifiedLocalTime as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @" from [SFC_FabMoProcess] A
                  left join [SFC_FabricatedMother] B on A.FabricatedMotherID = B.FabricatedMotherID
                  left join [SYS_Parameters] C on A.ProcessID = C.ParameterID
                  left join [SYS_WorkCenter] D on A.WorkCenterID = D.WorkCenterID
                  left join [SYS_Organization] E on B.OrganizationID = E.OrganizationID
                  left join [SYS_MESUsers] F on A.Modifier = F.MESUserID
                  left join [SYS_MESUsers] G on A.Creator = G.MESUserID
                  left join [SYS_Items] H on B.ItemID = H.ItemID
                  left join [SYS_Parameters] I on A.UnitID = I.ParameterID
                  where A.[SystemID] = '{0}' and  A.FabMoProcessID = '{1}' and
                        A.Status <> '{0}0201213000003'", Framework.SystemID, FabMoProcessID);

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }


        /// <summary>
        /// �����������Դ����ȡ�����Ƴ�ʵ��
        /// SAM 2017��7��17��23:20:29
        /// </summary>
        /// <param name="FabricatedMotherID"></param>
        /// <param name="SourceID"></param>
        /// <returns></returns>
        public static SFC_FabMoProcess get(string FabricatedMotherID, string SourceID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_FabMoProcess] where [FabricatedMotherID] = '{0}'  and [SourceID] ='{1}' and [Status] <> '{2}0201213000003'  and [SystemID] = '{2}' ", FabricatedMotherID, SourceID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }



        /// <summary>
        /// �Ƴ��깤״������-���б�
        /// SAM 2017��7��23��00:46:30
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
        public static IList<Hashtable> Sfc00011GetList(string StartWorkCenterCode, string EndWorkCenterCode,
           string StartFabMoCode, string EndFabMoCode, string StartDate, string EndDate,
           int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select  A.FabMoProcessID,
            E.Code as WorkCenterCode,E.Name as WorkCenterName,
            B.[MoNo],B.[SplitSequence],C.Code as ItemCode,
            A.Sequence,D.Code as ProcessCode,D.Name as ProcessName,       
            (select [Name] From [SYS_Parameters] where E.InoutMark = ParameterID) as InoutMark,
            (CASE WHEN E.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = E.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = E.DepartmentID) END) as DeptCode,
            (CASE WHEN E.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = E.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = E.DepartmentID) END) as DeptName,
            A.StartDate,A.FinishDate,A.Quantity,A.FinProQuantity,
            A.Quantity-A.FinProQuantity  as BalanceQuantity,
            A.ScrappedQuantity,A.DifferenceQuantity,
            F.Code as UnitCode,F.Name as UnitName,A.UnitRate,
            G.Name as Status,A.Comments ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_FabMoProcess] A         
                left join [SFC_FabricatedMother] B on A.[FabricatedMotherID] =B.[FabricatedMotherID]      
                left join [SYS_Items] C on B.[ItemID] = C.[ItemID]
                left join [SYS_Parameters] D on A.[ProcessID] =D.[ParameterID]
                left join [SYS_WorkCenter] E on A.[WorkCenterID] =E.[WorkCenterID]
                left join [SYS_Parameters] F on A.[UnitID] =F.[ParameterID]
                left join [SYS_Parameters] G on A.[Status] =G.[ParameterID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@EndFabMoCode",SqlDbType.VarChar),
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
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@EndFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartWorkCenterCode))
            {
                sql = sql + String.Format(@" and E.[Code] >= @StartWorkCenterCode ");
                parameters[0].Value = StartWorkCenterCode;
                Parcount[0].Value = StartWorkCenterCode;
            }

            if (!string.IsNullOrWhiteSpace(EndWorkCenterCode))
            {
                sql = sql + String.Format(@" and E.[Code] <= @EndWorkCenterCode ");
                parameters[1].Value = EndWorkCenterCode;
                Parcount[1].Value = EndWorkCenterCode;
            }


            if (!string.IsNullOrWhiteSpace(StartFabMoCode))
            {
                sql = sql + String.Format(@" and B.[MoNo] >= @StartFabMoCode ");
                parameters[2].Value = StartFabMoCode;
                Parcount[2].Value = StartFabMoCode;
            }

            if (!string.IsNullOrWhiteSpace(EndFabMoCode))
            {
                sql = sql + String.Format(@" and B.[MoNo] <= @EndFabMoCode ");
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

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "E.[Code],B.[MoNo],B.[SplitSequence],A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// �Ƴ��깤״������-���б�
        /// SAM 2017��7��23��00:46:30
        /// ���������������ֶ�
        /// Mouse 2017��11��15��18:00:39
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
        public static IList<Hashtable> Sfc00011GetListV1(string StartWorkCenterCode, string EndWorkCenterCode,
           string StartFabMoCode, string EndFabMoCode, string StartDate, string EndDate,
           int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select  A.FabMoProcessID,
            E.Code as WorkCenterCode,E.Name as WorkCenterName,
            B.[MoNo],B.[SplitSequence],C.Code as ItemCode,C.Name as ItemName,C.Specification as ItemSpecification,
            A.Sequence,D.Code as ProcessCode,D.Name as ProcessName,       
            (select [Name] From [SYS_Parameters] where E.InoutMark = ParameterID) as InoutMark,
            (CASE WHEN E.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = E.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = E.DepartmentID) END) as DeptCode,
            (CASE WHEN E.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = E.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = E.DepartmentID) END) as DeptName,
            A.StartDate,A.FinishDate,A.Quantity,A.FinProQuantity,
            A.Quantity-A.FinProQuantity  as BalanceQuantity,
            A.ScrappedQuantity,A.DifferenceQuantity,
            F.Code as UnitCode,F.Name as UnitName,A.UnitRate,
            G.Name as Status,A.Comments ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_FabMoProcess] A         
                left join [SFC_FabricatedMother] B on A.[FabricatedMotherID] =B.[FabricatedMotherID]      
                left join [SYS_Items] C on B.[ItemID] = C.[ItemID]
                left join [SYS_Parameters] D on A.[ProcessID] =D.[ParameterID]
                left join [SYS_WorkCenter] E on A.[WorkCenterID] =E.[WorkCenterID]
                left join [SYS_Parameters] F on A.[UnitID] =F.[ParameterID]
                left join [SYS_Parameters] G on A.[Status] =G.[ParameterID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@EndFabMoCode",SqlDbType.VarChar),
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
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@StartFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@EndFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartWorkCenterCode))
            {
                sql = sql + String.Format(@" and E.[Code] >= @StartWorkCenterCode ");
                parameters[0].Value = StartWorkCenterCode;
                Parcount[0].Value = StartWorkCenterCode;
            }

            if (!string.IsNullOrWhiteSpace(EndWorkCenterCode))
            {
                sql = sql + String.Format(@" and E.[Code] <= @EndWorkCenterCode ");
                parameters[1].Value = EndWorkCenterCode;
                Parcount[1].Value = EndWorkCenterCode;
            }


            if (!string.IsNullOrWhiteSpace(StartFabMoCode))
            {
                sql = sql + String.Format(@" and B.[MoNo] >= @StartFabMoCode ");
                parameters[2].Value = StartFabMoCode;
                Parcount[2].Value = StartFabMoCode;
            }

            if (!string.IsNullOrWhiteSpace(EndFabMoCode))
            {
                sql = sql + String.Format(@" and B.[MoNo] <= @EndFabMoCode ");
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

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "E.[Code],B.[MoNo],B.[SplitSequence],A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        ///  �Ƴ��깤״�����������-�Ƴ����б�
        ///  SAM 2017��7��23��01:27:58
        /// </summary>
        /// <param name="FabricatedMotherID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00010GetProcessList(string FabricatedMotherID, int page, int rows, ref int count)
        {
            string select = string.Format(
             @"select  A.FabMoProcessID,
            A.Sequence,B.Code as ProcessCode,B.Name as ProcessName,       
            E.Code as WorkCenterCode,E.Name as WorkCenterName,      
            (select [Name] From [SYS_Parameters] where E.InoutMark = ParameterID) as InoutMark,
            (CASE WHEN E.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = E.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = E.DepartmentID) END) as DeptCode,
            (CASE WHEN E.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = E.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = E.DepartmentID) END) as DeptName,
   
             A.StartDate,A.FinishDate,A.Quantity,A.FinProQuantity,
            A.Quantity-A.FinProQuantity as BalanceQuantity,
            A.ScrappedQuantity,A.DifferenceQuantity,
            C.Code as UnitCode,C.Name as UnitName,A.UnitRate,
            D.Name as Status,A.Comments ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_FabMoProcess] A         
                left join [SYS_Parameters] B on A.[ProcessID] =B.[ParameterID]
                left join [SYS_Parameters] C on A.[UnitID] =C.[ParameterID]
                left join [SYS_Parameters] D on A.[Status] =D.[ParameterID]
                left join [SYS_WorkCenter] E on A.[WorkCenterID] =E.[WorkCenterID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and A.[FabricatedMotherID] = '{1}'", Framework.SystemID, FabricatedMotherID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ����Ƴ��Ƿ�ʹ��
        /// Joint 2017��7��27��11:53:11
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static bool CheckProcess(string ProcessID)
        {
            string sql = string.Format(@"select * from [SFC_FabMoProcess]
            where [ProcessID]='{1}' and [SystemID]='{0}'", Framework.SystemID, ProcessID);
            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ��鹤�������Ƿ�ʹ��
        /// Joint 2017��7��28��14:41:35
        /// </summary>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public static bool CheckWorkCenter(string WorkCenterID)
        {
            string sql = string.Format(@"select * from [SFC_FabMoProcess]
            where [WorkCenterID]='{1}' and [SystemID]='{0}'", Framework.SystemID, WorkCenterID);
            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }



        /// <summary>
        /// �ж�һ���Ƴ�����У��Ƿ������ͬ���Ƴ̺͹�������
        /// SAM 2017��9��1��14:24:59
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <param name="WorkCenterID"></param>
        /// <param name="OriginalFabMoProcessID"></param>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static bool CheckSplitProcessID(string ProcessID, string WorkCenterID, string OriginalFabMoProcessID, string FabMoProcessID)
        {
            string sql = String.Format(@"select Top 1 * from [SFC_FabMoProcess] where [SystemID]='{0}' and [Status] <> '{0}0201213000003'", Framework.SystemID);

            /*�ȶ���Name��Code��Ĭ�ϸ�DbNull,��������Ҳ���*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ProcessID",SqlDbType.VarChar),
                new SqlParameter("@WorkCenterID",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            /*��ΪName��Code��ͨ���ֶ�����ģ�������Ҫ�ò�������ʽȥƴSQL*/
            if (!string.IsNullOrWhiteSpace(ProcessID))
            {
                sql = sql + String.Format(@" and [ProcessID] =@ProcessID ");
                parameters[0].Value = ProcessID;
            }

            if (!string.IsNullOrWhiteSpace(WorkCenterID))
            {
                sql = sql + String.Format(@" and [WorkCenterID] =@WorkCenterID ");
                parameters[1].Value = WorkCenterID;
            }

            if (!string.IsNullOrWhiteSpace(OriginalFabMoProcessID))
                sql = sql + String.Format(@" and [OriginalFabMoProcessID] = '{0}' ", OriginalFabMoProcessID);

            if (!string.IsNullOrWhiteSpace(FabMoProcessID))
                sql = sql + String.Format(@" and [FabMoProcessID] <> '{0}' ", FabMoProcessID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// ����ֱͨ�ʷ���-�Ƴ���ϸ�б�
        /// SAM 2017��9��3��21:26:49
        /// </summary>
        /// <param name="FabricatedMotherID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00017GetDetailList(string FabricatedMotherID, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.FabMoProcessID, A.FabricatedMotherID, A.ProcessID, A.Sequence,A.AssignQuantity,
                B.Code as ProcessCode,B.Name as ProcessName,cast(A.PreProQuantity as real) as PreProQuantity,
                cast(A.ScrappedQuantity as real) as ScrappedQuantity ");

            string sql = string.Format(
                @"from [SFC_FabMoProcess] A
                  left join [SYS_Parameters] B on A.ProcessID = B.ParameterID  where A.[SystemID] = '{0}' 
                and  A.[FabricatedMotherID] = @FabricatedMotherID 
                and  A.[Status] = '{0}020121300002A'", Framework.SystemID);

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FabricatedMotherID", FabricatedMotherID));

            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);

            string orderby = "A.[CreateLocalTime]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        ///  �uƷ���a���r����-��������ҳǩ
        ///  SAM 2017��9��4��00:08:03
        /// </summary>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
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
        public static IList<Hashtable> Sfc00019WorkCenterGetList(
            string StartWorkCenterCode, string EndWorkCenterCode, string StartItemCode, string EndItemCode,
         string StartFabMoCode, string EndFabMoCode, string StartDate, string EndDate,
             int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.FabMoProcessID,C.Code as WorkCenterCode,C.Name as WorkCenterName,
                D.Code as ProcessCode,D.Name as ProcessName,
                E.Code as ItemCode,E.Name as ItemName,E.Specification as ItemSpecification,
                B.MoNo+'-'+convert(varchar(20),B.SplitSequence) as MoNo,
                ISNULL(A.StandardTime,0) as StandardTime,
                null ActualHour,null as DifferenceHour,A.StandardTime as StandardHour ");

            string sql = string.Format(
                @"from [SFC_FabMoProcess] A
                   left join [SFC_FabricatedMother] B on A.[FabricatedMotherID] = B.[FabricatedMotherID]
                   left join [SYS_WorkCenter] C on A.[WorkCenterID] = C.[WorkCenterID]
                   left join [SYS_Parameters] D on A.[ProcessID] = D.[ParameterID]
                   left join [SYS_Items] E on B.[ItemID] = E.[ItemID]

                  where A.[SystemID] = '{0}' and A.[Status] = '{0}020121300002A' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@EndFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@StartItemCode",SqlDbType.VarChar),
                new SqlParameter("@EndItemCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar)
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
                new SqlParameter("@StartFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@EndFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@StartItemCode",SqlDbType.VarChar),
                new SqlParameter("@EndItemCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartWorkCenterCode",SqlDbType.VarChar),
                new SqlParameter("@EndWorkCenterCode",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;
            Parcount[6].Value = DBNull.Value;
            Parcount[7].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartFabMoCode))
            {
                sql = sql + String.Format(@" and B.[MoNo] >= @StartFabMoCode ");
                parameters[0].Value = StartFabMoCode;
                Parcount[0].Value = StartFabMoCode;
            }

            if (!string.IsNullOrWhiteSpace(EndFabMoCode))
            {
                sql = sql + String.Format(@" and B.[MoNo] <= @EndFabMoCode ");
                parameters[1].Value = EndFabMoCode;
                Parcount[1].Value = EndFabMoCode;
            }

            if (!string.IsNullOrWhiteSpace(StartItemCode))
            {
                sql = sql + String.Format(@" and E.[Code] >= @StartItemCode ");
                parameters[2].Value = StartItemCode;
                Parcount[2].Value = StartItemCode;
            }

            if (!string.IsNullOrWhiteSpace(EndItemCode))
            {
                sql = sql + String.Format(@" and E.[Code] <= @EndItemCode ");
                parameters[3].Value = EndItemCode;
                Parcount[3].Value = EndItemCode;
            }

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + String.Format(@" and B.[Date] >= @StartDate ");
                parameters[4].Value = StartDate;
                Parcount[4].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndFabMoCode))
            {
                sql = sql + String.Format(@" and B.[Date] <= @EndDate ");
                parameters[5].Value = EndDate;
                Parcount[5].Value = EndDate;
            }

            if (!string.IsNullOrWhiteSpace(StartWorkCenterCode))
            {
                sql = sql + String.Format(@" and C.[Code] >= @StartWorkCenterCode ");
                parameters[6].Value = StartWorkCenterCode;
                Parcount[6].Value = StartWorkCenterCode;
            }

            if (!string.IsNullOrWhiteSpace(EndWorkCenterCode))
            {
                sql = sql + String.Format(@" and C.[Code] <= @EndWorkCenterCode ");
                parameters[7].Value = EndWorkCenterCode;
                Parcount[7].Value = EndWorkCenterCode;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "C.[Code] desc,A.[Sequence] desc,E.[Code] desc,B.[MoNo] desc ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// Iot00003��ȡ��������棬�޹����ѷ���
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static Hashtable Iot00003GetMomProcessNoOperation(string FabMoProcessID)
        {
            string sql = string.Format(@"select E.Code as ItemCode,B.MoNo,B.SplitSequence,A.Sequence as ProcessSequence,C.Code as ProcessCode,C.Name as ProcessName,
                    D.Code as CenterCode,D.Name as CenterName,B.StartDate,B.FinishDate,B.Status 
                    from [SFC_FabMoProcess] A 
                    left join [SFC_FabricatedMother] B on A.[FabricatedMotherID]= B.[FabricatedMotherID]
                    left join [SYS_Parameters] C on A.[ProcessID]= C.[ParameterID]
                    left join [SYS_WorkCenter] D on A.[WorkCenterID]= D.[WorkCenterID]
                    left join [SYS_Items] E on E.[ItemID] = B.[ItemID]
                    where A.[SystemID]='{0}' and A.[FabMoProcessID]='{1}'
                    ", Framework.SystemID, FabMoProcessID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count != 0)
            {
                return ToHashtableList(dt)[0];//�ж�������ʱҲֻ���ص�һ������
            }
            else
            {
                return null;//��ѯ����Ϊ��
            }
        }

        /// <summary>
        /// SFC20�б�
        /// Mouse 2017-9-26 15:22:54
        /// </summary>
        /// <param name="FabricatedMotherIDStar"></param>
        /// <param name="FabricatedMotherIDEnd"></param>
        /// <param name="StartDate"></param>
        /// <param name="FinishDate"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> sfc00020GetList(string MoNoStar, string MoNoEnd, string StartDate, string FinishDate, string Status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.FabMoProcessID,B.MoNo + '-' + cast (B.SplitSequence as varchar) as MoNo,C.Code as ItemCode,C.Name as ItemName,C.Specification as ItemSpecification,
                        A.Sequence as ProcessSequence,D.Code as ProcessCode,D.Name as ProcessName,A.StartDate,A.FinishDate,A.Quantity,A.FinProQuantity,cast ( ( cast ((CASE WHEN A.FinProQuantity=0 THEN 0 else (A.FinProQuantity/A.Quantity) END) as decimal(18,2))*100) as decimal)  as Rate, A.Status");
            string sql = string.Format(@" from SFC_FabMoProcess A
                        left join [SFC_FabricatedMother] B on A.[FabricatedMotherID]=B.[FabricatedMotherID]
                        left join [SYS_Items] C on B.[ItemID]=C.[ItemID]
                        left join [SYS_Parameters] D on A.[ProcessID] = D.[ParameterID]
                        where A.[SystemID]='{0}' and  B.[Status] <> '{0}020121300002B' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(@"MoNoStar",SqlDbType.VarChar),
                new SqlParameter(@"MoNoEnd",SqlDbType.VarChar),
                new SqlParameter(@"StartDate",SqlDbType.VarChar),
                new SqlParameter(@"FinishDate",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[] {
                new SqlParameter(@"MoNoStar",SqlDbType.VarChar),
                new SqlParameter(@"MoNoEnd",SqlDbType.VarChar),
                new SqlParameter(@"StartDate",SqlDbType.VarChar),
                new SqlParameter(@"FinishDate",SqlDbType.VarChar),
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;


            if (!string.IsNullOrWhiteSpace(MoNoStar))
            {
                sql = sql + string.Format(@" and B.MoNo >= @MoNoStar");
                parameters[0].Value = MoNoStar;
                Parcount[0].Value = MoNoStar;
            }
            if (!string.IsNullOrWhiteSpace(MoNoEnd))
            {
                sql = sql + string.Format(@" and B.MoNo <= @MoNoEnd");
                parameters[1].Value = MoNoEnd;
                Parcount[1].Value = MoNoEnd;
            }
            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + string.Format(@" and A.StartDate >= @StartDate");
                parameters[2].Value = StartDate;
                Parcount[2].Value = StartDate;
            }
            if (!string.IsNullOrWhiteSpace(FinishDate))
            {
                sql = sql + string.Format(@" and A.StartDate <=@FinishDate");
                parameters[3].Value = FinishDate;
                Parcount[3].Value = FinishDate;
            }
            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql = sql + string.Format(@" and A.Status in ('{0}')", Status.Replace(",", "','"));
            }

            string orderby = " B.MoNo,B.SplitSequence,A.Sequence";

            count = UniversalService.getCount(sql, Parcount);
            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);
            return ToHashtableList(dt);
        }


        /// <summary>
        /// �u�����aӋ���-�б�
        /// Sam 2017��9��28��10:13:45
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartProcessCode">��ʼ�u�̴�̖</param>
        /// <param name="EndProcessCode">�Y���u�̴�̖</param>
        /// <param name="StartDate">��ʼ�u���u���_����</param>
        /// <param name="EndDate">�Y���u���u���_����</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00021GetList(string StartProcessCode, string EndProcessCode,
            string StartDate, string EndDate, int page, int rows, ref int count)
        {
            string select = string.Format(@"select distinct A.[ProcessID],D.Code as ProcessCode,D.Name as ProcessName,
            (select cast(ISNULL(SUM(ISNULL(PreProQuantity,0)),0) as real) from [SFC_FabMoProcess] where [ProcessID]=A.[ProcessID] and ([Status]='{0}0201213000029' or [Status]='{0}020121300002A')) as InputNum,
            (select cast(ISNULL(SUM(ISNULL(FinProQuantity,0)),0)as real) from [SFC_FabMoProcess] where [ProcessID]=A.[ProcessID] and ([Status]='{0}0201213000029' or [Status]='{0}020121300002A')) as OutputNum,
            (select cast(ISNULL(SUM(ISNULL(Quantity,0)),0)as real) from [SFC_FabMoProcess] where [ProcessID]=A.[ProcessID] and ([Status]='{0}0201213000029' or [Status]='{0}020121300002A')) as PlanNum ", Framework.SystemID);

            string sql = string.Format(@" from [SFC_FabMoProcess] A
                        left join [SYS_Parameters] D on A.[ProcessID] = D.[ParameterID]
                        where A.[SystemID]='{0}' and (A.[Status]='{0}0201213000029' or A.[Status]='{0}020121300002A')", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(@"StartProcessCode",SqlDbType.VarChar),
                new SqlParameter(@"EndProcessCode",SqlDbType.VarChar),
                new SqlParameter(@"StartDate",SqlDbType.VarChar),
                new SqlParameter(@"EndDate",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[] {
                new SqlParameter(@"StartProcessCode",SqlDbType.VarChar),
                new SqlParameter(@"EndProcessCode",SqlDbType.VarChar),
                new SqlParameter(@"StartDate",SqlDbType.VarChar),
                new SqlParameter(@"EndDate",SqlDbType.VarChar),
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;


            if (!string.IsNullOrWhiteSpace(StartProcessCode))
            {
                sql = sql + string.Format(@" and D.Code >= @StartProcessCode");
                parameters[0].Value = StartProcessCode;
                Parcount[0].Value = StartProcessCode;
            }
            if (!string.IsNullOrWhiteSpace(EndProcessCode))
            {
                sql = sql + string.Format(@" and D.Code <= @EndProcessCode");
                parameters[1].Value = EndProcessCode;
                Parcount[1].Value = EndProcessCode;
            }
            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + string.Format(@" and A.[StartDate] >= @StartDate");
                parameters[2].Value = StartDate;
                Parcount[2].Value = StartDate;
            }
            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + string.Format(@" and A.[StartDate] <=@EndDate");
                parameters[3].Value = EndDate;
                Parcount[3].Value = EndDate;
            }

            string orderby = " ProcessCode ";

            count = UniversalService.getCount(sql, Parcount);

            DataTable dt = UniversalService.GetTableDistinct(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ƴ�������Ƴ̵�SQL���
        /// SAM 2017��10��4��00:42:03
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static String InsertSQL(string userId, SFC_FabMoProcess Model)
        {
            try
            {
                string sql = string.Format(
                    @"insert [SFC_FabMoProcess]([SystemID],[FabMoProcessID],[FabricatedMotherID],[ProcessID],[Sequence],
                    [WorkCenterID],[StartDate],[FinishDate],[Quantity],[OrderQuantity],              
                    [UnitID],[UnitRate],[StandardTime],[PrepareTime],[Status],
                    [IsEnableOperation],[SourceID],[Comments],
                    [Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime]) values(
                    '{0}','{1}','{2}','{3}','{4}',
                     {5},{6},{7},{8},{9},
                     {10},{11},{12},{13},'{14}',
                     '{15}','{16}','{17}',
                    '{18}','{19}','{20}','{18}','{19}','{20}');",
                     Framework.SystemID, Model.FabMoProcessID, Model.FabricatedMotherID, Model.ProcessID, Model.Sequence,
                     UniversalService.checkNullForSQL(Model.WorkCenterID),
                     Model.StartDate == null ? "NULL" : "'" + Model.StartDate.ToString() + "'",
                     Model.FinishDate == null ? "NULL" : "'" + Model.FinishDate.ToString() + "'",
                     Model.Quantity, Model.OrderQuantity,
                     UniversalService.checkNullForSQL(Model.UnitID),
                     Model.UnitRate, Model.StandardTime == null ? "NULL" : "'" + Model.StandardTime.ToString() + "'",
                     Model.PrepareTime == null ? "NULL" : "'" + Model.PrepareTime.ToString() + "'",
                     Model.Status, Model.IsEnableOperation, Model.SourceID, Model.Comments,
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
        /// �����������ȡ���������������Ƴ��б�
        /// SAM 2017��10��6��15:17:58
        /// </summary>
        /// <returns></returns>
        public static IList<SFC_FabMoProcess> GetList(string FabricatedMotherID)
        {
            string sql = string.Format(
                @"select * from [SFC_FabMoProcess]       
                  where [SystemID] = '{0}' and [Status] <> '{0}0201213000003' and [FabricatedMotherID] ='{1}' ", Framework.SystemID, FabricatedMotherID);

            string orderby = " order by [Sequence]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }
        
        /// <summary>
         /// ���������Ƴ���ˮ�Ż�ȡ�����Ƴ���Ϣ
         /// SAM 2017��10��24��11:33:04
         /// SFC04��ͷר�ã��ѷ�����=���������Ƴ̵ģ�-�ѷ����������з�ɾ���������ϵ����񵥣�+�����������з�ɾ���������ϵ����񵥣�
         /// </summary>
         /// <param name="FabMoProcessID"></param>
         /// <returns></returns>
        public static Hashtable Sfc00004GetFabMoProcess(string FabMoProcessID)
        {
            string select = string.Format(
                 @"select Top 1 A.FabMoProcessID, A.FabricatedMotherID, B.MoNo, null as OperationID, A.ProcessID,
                         D.Code as ItemCode, D.Name as ItemName, D.Specification, A.Sequence,
                         C.Code as ProcessCode, C.Name as ProcessName,B.OrderQuantity,
                         A.FinProQuantity,A.AssignQuantity,A.Quantity,A.PreProQuantity,
                        (Select ISnull(SUM(DiffQuantity),0) from [SFC_TaskDispatch] where [FabMoProcessID] = A.[FabMoProcessID] and [Status] <> '{0}020121300008C' and [Status] <> '{0}0201213000003') as TaskDiffQuantity,
                        (Select ISnull(SUM(DispatchQuantity),0) from [SFC_TaskDispatch] where [FabMoProcessID] = A.[FabMoProcessID] and [Status] <> '{0}020121300008C' and [Status] <> '{0}0201213000003') as TaskAssignQuantity,
                         A.Status, A.Comments, A.IsEnableOperation ", Framework.SystemID);

            string sql = string.Format(
                @" from [SFC_FabMoProcess] A
                  left join [SFC_FabricatedMother] B on A.FabricatedMotherID = B.FabricatedMotherID
                  left join [SYS_Parameters] C on A.ProcessID = C.ParameterID
                  left join [SYS_Items] D on B.ItemID = D.ItemID
                  where A.[SystemID] = '{0}' and  A.FabMoProcessID = '{1}' and A.Status <> '{0}0201213000003'", Framework.SystemID, FabMoProcessID);

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }
    }
}

