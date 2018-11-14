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
    public class QCS_InspectionDocumentService : SuperModel<QCS_InspectionDocument>
    {
        /// <summary>
        /// 新增
        /// Joint
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, QCS_InspectionDocument Model)
        {
            try
            {
                string sql = string.Format(@"insert[QCS_InspectionDocument]([InspectionDocumentID],[InspectionNo],[DocumentDate],[ItemID],
                    [InspectionMethod],[CompletionOrderID],[TaskDispatchID],[InspectionDate],[InspectionUserID],[QualityControlDecision],
                    [FinQuantity],[InspectionQuantity],[ScrappedQuantity],[NGquantity],[OKQuantity],[InspectionFlag],
                    [Status],[ConfirmUserID],[ConfirmDate],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],
                    [Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                   (
                    @InspectionDocumentID,@InspectionNo,
                    @DocumentDate,@ItemID,@InspectionMethod,
                    @CompletionOrderID,@TaskDispatchID,@InspectionDate,
                    @InspectionUserID,@QualityControlDecision,@FinQuantity,
                    @InspectionQuantity,@ScrappedQuantity,@NGquantity,
                    @OKQuantity,@InspectionFlag,@Status,
                    @ConfirmUserID,@ConfirmDate,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@InspectionDocumentID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionNo",SqlDbType.NVarChar),
                    new SqlParameter("@DocumentDate",SqlDbType.DateTime),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionMethod",SqlDbType.VarChar),
                    new SqlParameter("@CompletionOrderID",SqlDbType.VarChar),
                    new SqlParameter("@TaskDispatchID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionDate",SqlDbType.DateTime),
                    new SqlParameter("@InspectionUserID",SqlDbType.VarChar),
                    new SqlParameter("@QualityControlDecision",SqlDbType.VarChar),
                    new SqlParameter("@FinQuantity",SqlDbType.Decimal),
                    new SqlParameter("@InspectionQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ScrappedQuantity",SqlDbType.Decimal),
                    new SqlParameter("@NGquantity",SqlDbType.Decimal),
                    new SqlParameter("@OKQuantity",SqlDbType.Decimal),
                    new SqlParameter("@InspectionFlag",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@ConfirmUserID",SqlDbType.VarChar),
                    new SqlParameter("@ConfirmDate",SqlDbType.DateTime),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.InspectionDocumentID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.InspectionNo ?? DBNull.Value;
                parameters[2].Value = (Object)Model.DocumentDate ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.InspectionMethod ?? DBNull.Value;
                parameters[5].Value = (Object)Model.CompletionOrderID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.TaskDispatchID ?? DBNull.Value;
                parameters[7].Value = (Object)Model.InspectionDate ?? DBNull.Value;
                parameters[8].Value = (Object)Model.InspectionUserID ?? DBNull.Value;
                parameters[9].Value = (Object)Model.QualityControlDecision ?? DBNull.Value;
                parameters[10].Value = (Object)Model.FinQuantity ?? DBNull.Value;
                parameters[11].Value = (Object)Model.InspectionQuantity ?? DBNull.Value;
                parameters[12].Value = (Object)Model.ScrappedQuantity ?? DBNull.Value;
                parameters[13].Value = (Object)Model.NGquantity ?? DBNull.Value;
                parameters[14].Value = (Object)Model.OKQuantity ?? DBNull.Value;
                parameters[15].Value = (Object)Model.InspectionFlag ?? DBNull.Value;
                parameters[16].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[17].Value = (Object)Model.ConfirmUserID ?? DBNull.Value;
                parameters[18].Value = (Object)Model.ConfirmDate ?? DBNull.Value;
                parameters[19].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// Joint
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, QCS_InspectionDocument Model)
        {
            try
            {
                string sql = String.Format(@"update[QCS_InspectionDocument] set {0},
                    [InspectionNo]=@InspectionNo,[DocumentDate]=@DocumentDate,[ItemID]=@ItemID,
                    [InspectionMethod]=@InspectionMethod,[CompletionOrderID]=@CompletionOrderID,[TaskDispatchID]=@TaskDispatchID,[InspectionDate]=@InspectionDate,
                    [InspectionUserID]=@InspectionUserID,[QualityControlDecision]=@QualityControlDecision,[FinQuantity]=@FinQuantity,[InspectionQuantity]=@InspectionQuantity,
                    [ScrappedQuantity]=@ScrappedQuantity,[NGquantity]=@NGquantity,[OKQuantity]=@OKQuantity,[InspectionFlag]=@InspectionFlag,
                    [Status]=@Status,[ConfirmUserID]=@ConfirmUserID,[ConfirmDate]=@ConfirmDate,[Comments]=@Comments
                     where [InspectionDocumentID]=@InspectionDocumentID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@InspectionDocumentID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionNo",SqlDbType.NVarChar),
                    new SqlParameter("@DocumentDate",SqlDbType.DateTime),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionMethod",SqlDbType.VarChar),
                    new SqlParameter("@CompletionOrderID",SqlDbType.VarChar),
                    new SqlParameter("@TaskDispatchID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionDate",SqlDbType.DateTime),
                    new SqlParameter("@InspectionUserID",SqlDbType.VarChar),
                    new SqlParameter("@QualityControlDecision",SqlDbType.VarChar),
                    new SqlParameter("@FinQuantity",SqlDbType.Decimal),
                    new SqlParameter("@InspectionQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ScrappedQuantity",SqlDbType.Decimal),
                    new SqlParameter("@NGquantity",SqlDbType.Decimal),
                    new SqlParameter("@OKQuantity",SqlDbType.Decimal),
                    new SqlParameter("@InspectionFlag",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@ConfirmUserID",SqlDbType.VarChar),
                    new SqlParameter("@ConfirmDate",SqlDbType.DateTime),
                    new SqlParameter("@Comments",SqlDbType.NVarChar)
                    };

                parameters[0].Value = Model.InspectionDocumentID;
                parameters[1].Value = (Object)Model.InspectionNo ?? DBNull.Value;
                parameters[2].Value = (Object)Model.DocumentDate ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.InspectionMethod ?? DBNull.Value;
                parameters[5].Value = (Object)Model.CompletionOrderID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.TaskDispatchID ?? DBNull.Value;
                parameters[7].Value = (Object)Model.InspectionDate ?? DBNull.Value;
                parameters[8].Value = (Object)Model.InspectionUserID ?? DBNull.Value;
                parameters[9].Value = (Object)Model.QualityControlDecision ?? DBNull.Value;
                parameters[10].Value = (Object)Model.FinQuantity ?? DBNull.Value;
                parameters[11].Value = (Object)Model.InspectionQuantity ?? DBNull.Value;
                parameters[12].Value = (Object)Model.ScrappedQuantity ?? DBNull.Value;
                parameters[13].Value = (Object)Model.NGquantity ?? DBNull.Value;
                parameters[14].Value = (Object)Model.OKQuantity ?? DBNull.Value;
                parameters[15].Value = (Object)Model.InspectionFlag ?? DBNull.Value;
                parameters[16].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[17].Value = (Object)Model.ConfirmUserID ?? DBNull.Value;
                parameters[18].Value = (Object)Model.ConfirmDate ?? DBNull.Value;
                parameters[19].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 单条更新 QCS00005 制程检验 
        /// Alvin 2017年9月6日15:50:53
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool updateByOne(string userId, QCS_InspectionDocument Model)
        {
            try
            {
                string sql = String.Format(@"update[QCS_InspectionDocument] set {0},
                    [InspectionNo]=@InspectionNo,[DocumentDate]=@DocumentDate,
                    [InspectionDate]=@InspectionDate,
                    [InspectionUserID]=@InspectionUserID,[QualityControlDecision]=@QualityControlDecision,
                    [InspectionQuantity]=@InspectionQuantity,
                    [ScrappedQuantity]=@ScrappedQuantity,[NGquantity]=@NGquantity,
                    [InspectionFlag]=@InspectionFlag,
                    [ConfirmUserID]=@ConfirmUserID,[ConfirmDate]=@ConfirmDate,[Comments]=@Comments
                     where [InspectionDocumentID]=@InspectionDocumentID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@InspectionDocumentID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionNo",SqlDbType.NVarChar),
                    new SqlParameter("@DocumentDate",SqlDbType.DateTime),
                    new SqlParameter("@InspectionDate",SqlDbType.DateTime),
                    new SqlParameter("@InspectionUserID",SqlDbType.VarChar),
                    new SqlParameter("@QualityControlDecision",SqlDbType.VarChar),
                    new SqlParameter("@InspectionQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ScrappedQuantity",SqlDbType.Decimal),
                    new SqlParameter("@NGquantity",SqlDbType.Decimal),
                    new SqlParameter("@InspectionFlag",SqlDbType.Bit),
                    new SqlParameter("@ConfirmUserID",SqlDbType.VarChar),
                    new SqlParameter("@ConfirmDate",SqlDbType.DateTime),
                    new SqlParameter("@Comments",SqlDbType.NVarChar)
                    };

                parameters[0].Value = Model.InspectionDocumentID;
                parameters[1].Value = (Object)Model.InspectionNo ?? DBNull.Value;
                parameters[2].Value = (Object)Model.DocumentDate ?? DBNull.Value;
                parameters[3].Value = (Object)Model.InspectionDate ?? DBNull.Value;
                parameters[4].Value = (Object)Model.InspectionUserID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.QualityControlDecision ?? DBNull.Value;
                parameters[6].Value = (Object)Model.InspectionQuantity ?? DBNull.Value;
                parameters[7].Value = (Object)Model.ScrappedQuantity ?? DBNull.Value;
                parameters[8].Value = (Object)Model.NGquantity ?? DBNull.Value;
                parameters[9].Value = (Object)Model.InspectionFlag ?? DBNull.Value;
                parameters[10].Value = (Object)Model.ConfirmUserID ?? DBNull.Value;
                parameters[11].Value = (Object)Model.ConfirmDate ?? DBNull.Value;
                parameters[12].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// </summary>
        /// <param name="InspectionDocumentID"></param>
        /// <returns></returns>
        public static QCS_InspectionDocument get(string InspectionDocumentID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_InspectionDocument] where [InspectionDocumentID] = '{0}'  and [SystemID] = '{1}' ", InspectionDocumentID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="InspectionDocumentID"></param>
        /// <returns></returns>
        public static bool delete(string InspectionDocumentID)
        {
            try
            {
                string sql = string.Format(@"delete from [QCS_InspectionDocument] where [InspectionDocumentID] = '{0}'  and [SystemID] = '{1}' ", InspectionDocumentID, Framework.SystemID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }

            catch (Exception ex)
            {

                DataLogerService.writeerrlog(ex);
                return false;
            }
        }
        /// <summary>
        /// 获取制程检验单列表
        /// Joint
        /// </summary>
        /// <param name="InspectionNo"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00005GetList(string InspectionNo,string Status, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.InspectionDocumentID,A.InspectionNo,A.DocumentDate,A.InspectionMethod,A.CompletionOrderID as FinishID,H.CompletionNo as FinishCode,H.Sequence as FinishSequence,
                    F.ItemID,F.Code as ItemCode,F.Name+'/'+F.Specification as DescAndSpec,G.TaskDispatchID ,G.TaskNo as TaskCode,A.FinQuantity,A.InspectionUserID,B.UserName as Name,
                    A.InspectionDate,A.QualityControlDecision as QcDecision,A.InspectionQuantity,A.ScrappedQuantity,A.NGquantity,A.Status,A.OKQuantity,
                    (select [MoNo] from SFC_FabricatedMother where FabricatedMotherID=G.FabricatedMotherID)+'-'+(select convert(varchar(20),[SplitSequence]) from SFC_FabricatedMother where FabricatedMotherID=G.FabricatedMotherID) as MoNoAndSequence,
                    J.Code+' '+J.Name as ProcessDesc,I.Code+' '+I.Name as OperationDesc,J.Code as ProcessCode,J.Name as ProcessName,I.Code as OperationCode,I.Name as OperationName,
                    A.InspectionFlag as Flag,(select [Name] from [SYS_Parameters] where [ParameterID]=(select [UnitID] from [SFC_FabricatedMother] where FabricatedMotherID=G.FabricatedMotherID and SplitSequence=G.MoSequence)) as UnitDesc,A.Comments,          
                    (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName as Creator,A.CreateLocalTime as CreateTime,
                    (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN D.Emplno is null or D.Emplno = '' THEN D.Account else D.Emplno END)+'-'+D.UserName END) as Modifier,
                    (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [QCS_InspectionDocument] A                
                  left join SYS_MESUsers B on A.InspectionUserID = B.MESUserID
                  left join SYS_MESUsers C on A.Modifier = C.MESUserID
                  left join SYS_MESUsers D on A.Creator = D.MESUserID
                  left join SYS_Items F on A.ItemID=F.ItemID
                  left join SFC_TaskDispatch G on A.TaskDispatchID=G.TaskDispatchID
                  left join SFC_CompletionOrder H on A.CompletionOrderID=H.CompletionOrderID
                  left join [SYS_Parameters] J on J.[ParameterID]= G.[ProcessID]
                  left join [SYS_Parameters] I on I.[ParameterID]= G.[OperationID]
                  where A.[SystemID] = '{0}' and A.Status <> '{0}0201213000003' and A.InspectionMethod='{0}020121300007E'", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@InspectionNo",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@InspectionNo",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(InspectionNo))
            {
                InspectionNo = "%" + InspectionNo + "%";
                sql = sql + string.Format(@" and A.[InspectionNo] like @InspectionNo ");
                parameters[0].Value = InspectionNo;
                Parcount[0].Value = InspectionNo;
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql = sql + string.Format(@" and A.[Status] = @Status ");
                parameters[1].Value = Status;
                Parcount[1].Value = Status;
            }

            count = UniversalService.getCount(sql, Parcount);

            String orderby = "A.[Status],A.[DocumentDate] Desc,A.[InspectionNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取制程检验单列表
        /// Joint
        /// </summary>
        /// <param name="InspectionNo"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00007GetList(string InspectionNo, string Status, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.InspectionDocumentID,A.InspectionNo,A.DocumentDate,A.InspectionMethod,
                    F.ItemID,F.Code as ItemCode,F.Name+'/'+F.Specification as DescAndSpec,G.TaskDispatchID ,G.TaskNo as TaskCode,A.FinQuantity,A.InspectionUserID,B.UserName as Name,
                    A.InspectionDate,A.QualityControlDecision as QcDecision,A.InspectionQuantity,A.ScrappedQuantity,A.NGquantity,A.Status,A.OKQuantity,
                    (select [MoNo] from SFC_FabricatedMother where FabricatedMotherID=G.FabricatedMotherID)+'-'+(select convert(varchar(20),[SplitSequence]) from SFC_FabricatedMother where FabricatedMotherID=G.FabricatedMotherID) as MoNoAndSequence,
                    H.Code+' '+H.Name as ProcessDesc,I.Code+' '+I.Name as OperationDesc,H.Code as ProcessCode,H.Name as ProcessName,I.Code as OperationCode,I.Name as OperationName,
                    A.InspectionFlag as Flag,(select [Name] from [SYS_Parameters] where [ParameterID]=(select [UnitID] from [SFC_FabricatedMother] where FabricatedMotherID=G.FabricatedMotherID and SplitSequence=G.MoSequence)) as UnitDesc,A.Comments,          
                    (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName as Creator,A.CreateLocalTime as CreateTime,
                    (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN D.Emplno is null or D.Emplno = '' THEN D.Account else D.Emplno END)+'-'+D.UserName END) as Modifier,
                    (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [QCS_InspectionDocument] A                
                  left join SYS_MESUsers B on A.InspectionUserID = B.MESUserID
                  left join SYS_MESUsers C on A.Modifier = C.MESUserID
                  left join SYS_MESUsers D on A.Creator = D.MESUserID
                  left join SYS_Items F on A.ItemID=F.ItemID
                  left join SFC_TaskDispatch G on A.TaskDispatchID=G.TaskDispatchID
                  left join [SYS_Parameters] H on H.[ParameterID]= G.[ProcessID]
                  left join [SYS_Parameters] I on I.[ParameterID]= G.[OperationID]
                  where A.[SystemID] = '{0}' and A.Status <> '{0}0201213000003' and A.InspectionMethod='{0}0201213000080'", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@InspectionNo",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@InspectionNo",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(InspectionNo))
            {
                InspectionNo = "%" + InspectionNo + "%";
                sql = sql + string.Format(@" and A.[InspectionNo] like @InspectionNo ");
                parameters[0].Value = InspectionNo;
                Parcount[0].Value = InspectionNo;
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql = sql + string.Format(@" and A.[Status] = @Status ");
                parameters[1].Value = Status;
                Parcount[1].Value = Status;
            }

            count = UniversalService.getCount(sql, Parcount);

            String orderby = "A.[Status],A.[DocumentDate] Desc,A.[InspectionNo]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 获取制程巡检验单列表
        /// Joint
        /// </summary>
        /// <param name="InspectionNo"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00008GetList(string InspectionNo, string Status, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.InspectionDocumentID,A.InspectionNo,A.DocumentDate,A.InspectionMethod,
                    F.ItemID,F.Code as ItemCode,F.Name+'/'+F.Specification as DescAndSpec,G.TaskDispatchID ,G.TaskNo as TaskCode,A.FinQuantity,A.InspectionUserID,B.UserName as Name,
                    A.InspectionDate,A.QualityControlDecision as QcDecision,A.InspectionQuantity,A.ScrappedQuantity,A.NGquantity,A.Status,A.OKQuantity,
                    (select [MoNo] from SFC_FabricatedMother where FabricatedMotherID=G.FabricatedMotherID)+'-'+(select convert(varchar(20),[SplitSequence]) from SFC_FabricatedMother where FabricatedMotherID=G.FabricatedMotherID) as MoNoAndSequence,
                    H.Code+' '+H.Name as ProcessDesc,I.Code+' '+I.Name as OperationDesc,H.Code as ProcessCode,H.Name as ProcessName,I.Code as OperationCode,I.Name as OperationName,
                    A.InspectionFlag as Flag,(select [Name] from [SYS_Parameters] where [ParameterID]=(select [UnitID] from [SFC_FabricatedMother] where FabricatedMotherID=G.FabricatedMotherID and SplitSequence=G.MoSequence)) as UnitDesc,A.Comments,          
                    (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName as Creator,A.CreateLocalTime as CreateTime,
                    (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN D.Emplno is null or D.Emplno = '' THEN D.Account else D.Emplno END)+'-'+D.UserName END) as Modifier,
                    (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [QCS_InspectionDocument] A                
                  left join SYS_MESUsers B on A.InspectionUserID = B.MESUserID
                  left join SYS_MESUsers C on A.Modifier = C.MESUserID
                  left join SYS_MESUsers D on A.Creator = D.MESUserID
                  left join SYS_Items F on A.ItemID=F.ItemID
                  left join SFC_TaskDispatch G on A.TaskDispatchID=G.TaskDispatchID
                  left join [SYS_Parameters] H on H.[ParameterID]= G.[ProcessID]
                  left join [SYS_Parameters] I on I.[ParameterID]= G.[OperationID]
                  where A.[SystemID] = '{0}' and A.Status <> '{0}0201213000003' and A.InspectionMethod='{0}0201213000081'", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@InspectionNo",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@InspectionNo",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(InspectionNo))
            {
                InspectionNo = "%" + InspectionNo + "%";
                sql = sql + string.Format(@" and A.[InspectionNo] like @InspectionNo ");
                parameters[0].Value = InspectionNo;
                Parcount[0].Value = InspectionNo;
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql = sql + string.Format(@" and A.[Status] = @Status ");
                parameters[1].Value = Status;
                Parcount[1].Value = Status;
            }

            count = UniversalService.getCount(sql, Parcount);

            String orderby = " A.[Status],A.[DocumentDate] Desc,A.[InspectionNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        ///  制程检验确认-主查詢
        ///  SAM 2017年7月9日20:30:31
        /// </summary>
        /// <param name="inspectionType"></param>
        /// <param name="userID"></param>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <param name="startInspectionNo"></param>
        /// <param name="endInspectionNo"></param>
        /// <param name="startRCNo"></param>
        /// <param name="endRCNo"></param>
        /// <param name="startPart"></param>
        /// <param name="endPart"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00006GetList(string inspectionType, string userID, string sDate, string eDate, string startInspectionNo, string endInspectionNo, string startRCNo, string endRCNo, string startPart, string endPart, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.InspectionDocumentID,A.InspectionNo,A.DocumentDate,A.ItemID,
            A.InspectionMethod,A.CompletionOrderID,A.TaskDispatchID,A.InspectionDate,
            A.InspectionUserID,A.QualityControlDecision,A.FinQuantity,A.InspectionQuantity,A.ScrappedQuantity,A.NGquantity,A.OKQuantity,A.InspectionFlag,
            A.Status,A.ConfirmUserID,A.ConfirmDate,A.Comments,
            E.CompletionNo,E.Sequence as CompletionSeq,
            F.Code as ItemCode,F.Name+'/'+F.Specification as DescriptionSpec,
            D.TaskNo,(Select Code from [SYS_Parameters] where F.Unit=ParameterID) as Unit,
            G.UserName as InspectionUserName,H.Name as StatusName,
            (Select [MoNo] from [SFC_FabricatedMother] where E.FabricatedMotherID =FabricatedMotherID)+'-'+(Select convert(varchar(20),[SplitSequence]) from [SFC_FabricatedMother] where E.FabricatedMotherID =FabricatedMotherID) as MoNo,
            (Select [Code] from [SYS_Parameters] where E.ProcessID =ParameterID)+' '+(Select [Name] from [SYS_Parameters] where E.ProcessID =ParameterID) as Process,
            (Select [Code] from [SYS_Parameters] where E.ProcessID =ParameterID) as ProcessCode,
            (Select [Name] from [SYS_Parameters] where E.ProcessID =ParameterID) as ProcessName,
            (Select [Code] from [SYS_Parameters] where E.OperationID =ParameterID)+' '+(Select [Name] from [SYS_Parameters] where E.OperationID =ParameterID) as Operation,
             (Select [Code] from [SYS_Parameters] where E.OperationID =ParameterID) as OperationCode,
             (Select [Name] from [SYS_Parameters] where E.OperationID =ParameterID) as OperationName,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@"  from [QCS_InspectionDocument] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SFC_TaskDispatch] D on A.[TaskDispatchID] = D.[TaskDispatchID]
            left join [SFC_CompletionOrder] E on A.[CompletionOrderID] = E.[CompletionOrderID]
            left join [SYS_Items] F on A.[ItemID] = F.[ItemID]
            left join [SYS_MESUsers] G on A.[InspectionUserID] = G.[MESUserID]
            left join [SYS_Parameters] H on A.[Status] = H.[ParameterID]
            left join [SYS_Parameters] I on A.[InspectionMethod] = I.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}020121300008D' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@InspectionMethod",SqlDbType.VarChar),
                new SqlParameter("@userID",SqlDbType.VarChar),
                new SqlParameter("@sDate",SqlDbType.VarChar),
                new SqlParameter("@eDate",SqlDbType.VarChar),
                new SqlParameter("@startInspectionNo",SqlDbType.VarChar),
                new SqlParameter("@endInspectionNo",SqlDbType.VarChar),
                new SqlParameter("@startRCNo",SqlDbType.VarChar),
                new SqlParameter("@endRCNo",SqlDbType.VarChar),
                new SqlParameter("@startPart",SqlDbType.VarChar),
                new SqlParameter("@endPart",SqlDbType.VarChar)
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
                new SqlParameter("@InspectionMethod",SqlDbType.VarChar),
                new SqlParameter("@userID",SqlDbType.VarChar),
                new SqlParameter("@sDate",SqlDbType.VarChar),
                new SqlParameter("@eDate",SqlDbType.VarChar),
                new SqlParameter("@startInspectionNo",SqlDbType.VarChar),
                new SqlParameter("@endInspectionNo",SqlDbType.VarChar),
                new SqlParameter("@startRCNo",SqlDbType.VarChar),
                new SqlParameter("@endRCNo",SqlDbType.VarChar),
                new SqlParameter("@startPart",SqlDbType.VarChar),
                new SqlParameter("@endPart",SqlDbType.VarChar)
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

            if (!string.IsNullOrWhiteSpace(inspectionType))
            {
                inspectionType = "%" + inspectionType.Trim() + "%";
                sql = sql + string.Format(@" and I.[Code] like @InspectionMethod ");
                parameters[0].Value = inspectionType;
                Parcount[0].Value = inspectionType;
            }

            if (!string.IsNullOrWhiteSpace(userID))
            {
                sql = sql + string.Format(@" and A.[InspectionUserID] = @userID ");
                parameters[1].Value = userID;
                Parcount[1].Value = userID;
            }

            if (!string.IsNullOrWhiteSpace(sDate))
            {
                sql = sql + string.Format(@" and A.[InspectionDate] >= @sDate ");
                parameters[2].Value = sDate;
                Parcount[2].Value = sDate;
            }

            if (!string.IsNullOrWhiteSpace(eDate))
            {
                sql = sql + string.Format(@" and A.[InspectionDate] <= @eDate ");
                parameters[3].Value = eDate;
                Parcount[3].Value = eDate;
            }

            if (!string.IsNullOrWhiteSpace(startInspectionNo))
            {
                sql = sql + string.Format(@" and A.[InspectionNo] >= @startInspectionNo ");
                parameters[4].Value = startInspectionNo;
                Parcount[4].Value = startInspectionNo;
            }

            if (!string.IsNullOrWhiteSpace(endInspectionNo))
            {
                sql = sql + string.Format(@" and A.[InspectionNo] <= @endInspectionNo ");
                parameters[5].Value = endInspectionNo;
                Parcount[5].Value = endInspectionNo;
            }

            if (!string.IsNullOrWhiteSpace(startRCNo))
            {
                sql = sql + string.Format(@" and D.[TaskNo] >= @startRCNo ");
                parameters[6].Value = startRCNo;
                Parcount[6].Value = startRCNo;
            }


            if (!string.IsNullOrWhiteSpace(endRCNo))
            {
                sql = sql + string.Format(@" and D.[TaskNo] <= @endRCNo ");
                parameters[7].Value = endRCNo;
                Parcount[7].Value = endRCNo;
            }


            if (!string.IsNullOrWhiteSpace(startPart))
            {
                sql = sql + string.Format(@" and F.[Code] >= @startPart ");
                parameters[8].Value = startPart;
                Parcount[8].Value = startPart;
            }

            if (!string.IsNullOrWhiteSpace(endPart))
            {
                sql = sql + string.Format(@" and F.[Code] <= @endPart ");
                parameters[9].Value = endPart;
                Parcount[9].Value = endPart;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[Status],A.[DocumentDate] desc ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 检验单的开窗
        /// SAM 2017年7月9日20:51:201
        /// </summary>
        /// <param name="code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> QCSInspectionDocumentList(string code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.InspectionDocumentID,A.InspectionNo,A.DocumentDate,
            D.Emplno as InspectionUserCode,B.UserName as InspectionUserName,E.Name as Status,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@"  from [QCS_InspectionDocument] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_MESUsers] D on A.[InspectionUserID] = D.[MESUserID]
            left join [SYS_Parameters] E on A.[Status] = E.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}020121300008D' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@code",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(code))
            {
                code = "%" + code.Trim() + "%";
                sql = sql + string.Format(@" and A.[InspectionNo] collate Chinese_PRC_CI_AS like @code ");
                parameters[0].Value = code;
                Parcount[0].Value = code;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[InspectionNo] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 制程检验维护导出
        /// Joint
        /// </summary>
        /// <param name="InspectionNo"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static DataTable Qcs00005Export(string InspectionNo, string Status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status],A.[DocumentDate] Desc,A.[InspectionNo]),A.InspectionNo,
             Convert (varchar(10),A.DocumentDate,120) as ADocumentDate,
             (select [Name] from [SYS_Parameters] where [ParameterID] = A.InspectionMethod) as InspectionMethod,
             C.CompletionNo,C.Sequence,D.Code,D.Name+'/'+D.Specification,E.TaskNo,
             cast (A.FinQuantity as decimal(18,0)) as AFinQuantity,
             (CASE WHEN A.InspectionFlag='1' THEN '是' else '否' END) as AInspectionFlag,
            (select [Name] from [SYS_Parameters] where [ParameterID]=(select Top 1 UnitID from SFC_FabricatedMother where FabricatedMotherID=E.FabricatedMotherID and SplitSequence=E.MoSequence)) as  UnitDesc,
             J.UserName,A.InspectionDate,
            (select [Name] from [SYS_Parameters] where [ParameterID] = A.QualityControlDecision)as QualityControlDecision,
             cast (A.InspectionQuantity as decimal(18,0)) as AInspectionQuantity,
             cast (A.ScrappedQuantity as decimal(18,0)) as AScrappedQuantity,
             cast (A.NGquantity as decimal(18,0)) as ANGquantity,
             (select [Name] from [SYS_Parameters] where [ParameterID] = A.Status) as AStatus,
             cast (A.OKQuantity as decimal(18,0)) as AOKQuantity,
            (select [MoNo] from [SFC_FabricatedMother] where [FabricatedMotherID]=E.[FabricatedMotherID])+'-'+(select convert(varchar(20),[SplitSequence]) from [SFC_FabricatedMother] where [FabricatedMotherID]=E.[FabricatedMotherID]) as MoNoAndSequence,       
             H.Code+' '+H.Name as ProcessDesc,I.Code+' '+I.Name as OperationDesc,
             A.Comments,F.Emplno+'-'+F.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else F.Emplno+'-'+F.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime,
             ROW_NUMBER() OVER (ORDER BY A.[DocumentDate] Desc,A.[InspectionNo]),B.Sequence,
            (select Top 1 Code from [QCS_InspectionProject] where InspectionProjectID=B.InspectionItemID) as ProcessCode,
            (select Top 1 Name from [QCS_InspectionProject] where InspectionProjectID=B.InspectionItemID) as ProcessName,
             B.InspectionStandard,
             (select [Name] from [SYS_Parameters] where [ParameterID] = B.InspectionFaultID) as InspectionFaultID,
             cast (B.SampleQuantity as decimal(18,0)) as BSampleQuantity,
             (select [Name] from [SYS_Parameters] where [ParameterID] = B.Aql) as Aql,
             cast (B.AcQuantity as decimal(18,0)) as BAcQuantity,
             cast (B.ReQuantity as decimal(18,0)) as BReQuantity,
             cast (B.NGquantity as decimal(18,0)) as BNGquantity,
             B.Attribute,
             (select [Name] from [SYS_Parameters] where [ParameterID] = B.QualityControlDecision)as QualityControlDecision ,B.Comments,
             G.Emplno+'-'+G.UserName as GCreator,A.CreateLocalTime as GCreateTime,
            (CASE WHEN B.CreateLocalTime=B.ModifiedLocalTime THEN NULL else G.Emplno+'-'+G.UserName END) as GModifier,
            (CASE WHEN B.CreateLocalTime=B.ModifiedLocalTime THEN NULL else B.ModifiedLocalTime END) as GModifiedTime", Framework.SystemID);

            string sql = string.Format(@" from [QCS_InspectionDocument] A 
            left join [QCS_InspectionDocumentDetails] B on A.InspectionDocumentID=B.InspectionDocumentID
            left join [SFC_CompletionOrder] C on A.CompletionOrderID=C.CompletionOrderID
            left join [SYS_Items] D on A.ItemID=D.ItemID
            left join [SYS_MESUsers] F on A.Creator= F.MESUserID
            left join [SYS_MESUsers] G on B.Creator= G.MESUserID
            left join [SFC_TaskDispatch] E on A.TaskDispatchID=E.TaskDispatchID
            left join [SYS_Parameters] H on H.[ParameterID]= E.[ProcessID]
            left join [SYS_Parameters] I on I.[ParameterID]= E.[OperationID]
            left join [SYS_MESUsers] J on J.[MESUserID] = A.[InspectionUserID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' and A.[InspectionMethod]='{0}020121300007E' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@InspectionNo",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@InspectionNo",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(InspectionNo))
            {
                InspectionNo = "%" + InspectionNo + "%";
                sql = sql + string.Format(@" and A.[InspectionNo] like @InspectionNo ");
                parameters[0].Value = InspectionNo;
                Parcount[0].Value = InspectionNo;
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql = sql + string.Format(@" and A.[Status] = @Status ");
                parameters[1].Value = Status;
                Parcount[1].Value = Status;
            }

            string orderby = " order by A.[Status],A.[DocumentDate] Desc,A.[InspectionNo]";

            return SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text, parameters);
        }

        /// <summary>
        /// 制程首件检验维护导出
        /// Joint
        /// </summary>
        /// <param name="InspectionNo"></param>
        /// <returns></returns>
        public static DataTable Qcs00007Export(string InspectionNo)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status],A.[DocumentDate] Desc,A.[InspectionNo]),A.InspectionNo,
             Convert (varchar(10),A.DocumentDate,120) as ADocumentDate,
             (select [Name] from [SYS_Parameters] where [ParameterID] = A.InspectionMethod) as InspectionMethod,
             E.TaskNo,D.Code,D.Name+'/'+D.Specification,
             cast (A.FinQuantity as decimal(18,0)) as AFinQuantity,
             (CASE WHEN A.InspectionFlag='1' THEN '是' else '否' END) as AInspectionFlag,
             (select [Name] from [SYS_Parameters] where [ParameterID]=(select Top 1 UnitID from SFC_FabricatedMother where FabricatedMotherID=E.FabricatedMotherID and SplitSequence=E.MoSequence)) as  UnitDesc,
             J.UserName,A.InspectionDate,
             (select [Name] from [SYS_Parameters] where [ParameterID] = A.QualityControlDecision)as QualityControlDecision,
             cast (A.InspectionQuantity as decimal(18,0)) as AInspectionQuantity,
             cast (A.ScrappedQuantity as decimal(18,0)) as AScrappedQuantity,
             cast (A.NGquantity as decimal(18,0)) as ANGquantity,
             cast (A.OKQuantity as decimal(18,0)) as AOKQuantity,
             (select [Name] from [SYS_Parameters] where [ParameterID] = A.Status) as AStatus,
             (select [MoNo] from [SFC_FabricatedMother] where [FabricatedMotherID]=E.[FabricatedMotherID])+'-'+(select convert(varchar(20),[SplitSequence]) from [SFC_FabricatedMother] where [FabricatedMotherID]=E.[FabricatedMotherID]) as MoNoAndSequence,
             H.Code+' '+H.Name as ProcessDesc,I.Code+' '+I.Name as OperationDesc,
             A.Comments,F.Emplno+'-'+F.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else F.Emplno+'-'+F.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime,
             ROW_NUMBER() OVER (ORDER BY A.[DocumentDate] Desc,A.[InspectionNo]),B.Sequence,
            (select Top 1 Code from [QCS_InspectionProject] where InspectionProjectID=B.InspectionItemID) as ProcessCode,
            (select Top 1 Name from [QCS_InspectionProject] where InspectionProjectID=B.InspectionItemID) as ProcessName,
             B.InspectionStandard,
            (select [Name] from [SYS_Parameters] where [ParameterID] = B.InspectionFaultID) as InspectionFaultID,
             cast (B.SampleQuantity as decimal(18,0)) as BSampleQuantity,
             (select [Name] from [SYS_Parameters] where [ParameterID] = B.Aql) as Aql,
             cast (B.AcQuantity as decimal(18,0)) as BAcQuantity,
             cast (B.ReQuantity as decimal(18,0)) as BReQuantity,
             cast (B.NGquantity as decimal(18,0)) as BNGquantity,
             B.Attribute,(select [Name] from [SYS_Parameters] where [ParameterID] = B.QualityControlDecision)as BQualityControlDecision,B.Comments,
             G.Emplno+'-'+G.UserName as GCreator,A.CreateLocalTime as GCreateTime,
            (CASE WHEN B.CreateLocalTime=B.ModifiedLocalTime THEN NULL else G.Emplno+'-'+G.UserName END) as GModifier,
            (CASE WHEN B.CreateLocalTime=B.ModifiedLocalTime THEN NULL else B.ModifiedLocalTime END) as GModifiedTime", Framework.SystemID);

            string sql = string.Format(@" from [QCS_InspectionDocument] A 
            left join [QCS_InspectionDocumentDetails] B on A.InspectionDocumentID=B.InspectionDocumentID
            left join [SFC_CompletionOrder] C on A.CompletionOrderID=C.CompletionOrderID
            left join [SYS_Items] D on A.ItemID=D.ItemID
            left join [SFC_TaskDispatch] E on A.TaskDispatchID=E.TaskDispatchID
            left join [SYS_MESUsers] F on A.Creator= F.MESUserID
            left join [SYS_MESUsers] G on B.Creator= G.MESUserID
            left join [SYS_Parameters] H on H.[ParameterID]= E.[ProcessID]
            left join [SYS_Parameters] I on I.[ParameterID]= E.[OperationID]
            left join [SYS_MESUsers] J on J.[MESUserID] = A.[InspectionUserID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' and A.[InspectionMethod]='{0}0201213000080' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@InspectionNo",SqlDbType.VarChar),

            };
            parameters[0].Value = DBNull.Value;


            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@InspectionNo",SqlDbType.VarChar),

            };
            Parcount[0].Value = DBNull.Value;


            if (!string.IsNullOrWhiteSpace(InspectionNo))
            {
                InspectionNo = "%" + InspectionNo + "%";
                sql = sql + String.Format(@" and A.[InspectionNo] like @InspectionNo ");
                parameters[0].Value = InspectionNo;
                Parcount[0].Value = InspectionNo;
            }



            string orderBy = "order By A.[Status],A.[DocumentDate] Desc,A.[InspectionNo] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }


        /// <summary>
        /// 制程巡检检验维护导出
        /// Joint
        /// </summary>
        /// <param name="InspectionNo"></param>
        /// <returns></returns>
        public static DataTable Qcs00008Export(string InspectionNo)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status],A.[DocumentDate] Desc,A.[InspectionNo]),A.InspectionNo,
             Convert (varchar(10),A.DocumentDate,120) as ADocumentDate,
             (select [Name] from [SYS_Parameters] where [ParameterID] = A.InspectionMethod) as InspectionMethod,
             E.TaskNo,D.Code,D.Name+'/'+D.Specification,
             cast (A.FinQuantity as decimal(18,0)) as AFinQuantity,
             (CASE WHEN A.InspectionFlag='1' THEN '是' else '否' END) as AInspectionFlag,
             (select [Name] from [SYS_Parameters] where [ParameterID]=(select Top 1 UnitID from SFC_FabricatedMother where FabricatedMotherID=E.FabricatedMotherID and SplitSequence=E.MoSequence)) as  UnitDesc,
             J.UserName,A.InspectionDate,
             (select [Name] from [SYS_Parameters] where [ParameterID] = A.QualityControlDecision)as QualityControlDecision,
             cast (A.InspectionQuantity as decimal(18,0)) as AInspectionQuantity,
             cast (A.ScrappedQuantity as decimal(18,0)) as AScrappedQuantity,
             cast (A.NGquantity as decimal(18,0)) as ANGquantity,
             cast (A.OKQuantity as decimal(18,0)) as AOKQuantity,
             (select [Name] from [SYS_Parameters] where [ParameterID] = A.Status) as AStatus,
             (select [MoNo] from [SFC_FabricatedMother] where [FabricatedMotherID]=E.[FabricatedMotherID])+'-'+(select convert(varchar(20),[SplitSequence]) from [SFC_FabricatedMother] where [FabricatedMotherID]=E.[FabricatedMotherID]) as MoNoAndSequence,
             H.Code+' '+H.Name as ProcessDesc,I.Code+' '+I.Name as OperationDesc,
             A.Comments,F.Emplno+'-'+F.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else F.Emplno+'-'+F.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime,
             ROW_NUMBER() OVER (ORDER BY A.[DocumentDate] Desc,A.[InspectionNo]),B.Sequence,
            (select Top 1 Code from [QCS_InspectionProject] where InspectionProjectID=B.InspectionItemID) as ProcessCode,
            (select Top 1 Name from [QCS_InspectionProject] where InspectionProjectID=B.InspectionItemID) as ProcessName,
             B.InspectionStandard,
            (select [Name] from [SYS_Parameters] where [ParameterID] = B.InspectionFaultID) as InspectionFaultID,
             cast (B.SampleQuantity as decimal(18,0)) as BSampleQuantity,
             (select [Name] from [SYS_Parameters] where [ParameterID] = B.Aql) as Aql,
             cast (B.AcQuantity as decimal(18,0)) as BAcQuantity,
             cast (B.ReQuantity as decimal(18,0)) as BReQuantity,
             cast (B.NGquantity as decimal(18,0)) as BNGquantity,
             B.Attribute,(select [Name] from [SYS_Parameters] where [ParameterID] = B.QualityControlDecision)as BQualityControlDecision,B.Comments,
             G.Emplno+'-'+G.UserName as GCreator,A.CreateLocalTime as GCreateTime,
            (CASE WHEN B.CreateLocalTime=B.ModifiedLocalTime THEN NULL else G.Emplno+'-'+G.UserName END) as GModifier,
            (CASE WHEN B.CreateLocalTime=B.ModifiedLocalTime THEN NULL else B.ModifiedLocalTime END) as GModifiedTime", Framework.SystemID);

            string sql = string.Format(@" from [QCS_InspectionDocument] A 
            left join [QCS_InspectionDocumentDetails] B on A.InspectionDocumentID=B.InspectionDocumentID
            left join [SFC_CompletionOrder] C on A.CompletionOrderID=C.CompletionOrderID
            left join [SYS_Items] D on A.ItemID=D.ItemID
            left join [SFC_TaskDispatch] E on A.TaskDispatchID=E.TaskDispatchID
            left join [SYS_MESUsers] F on A.Creator= F.MESUserID
            left join [SYS_MESUsers] G on B.Creator= G.MESUserID
            left join [SYS_Parameters] H on H.[ParameterID]= E.[ProcessID]
            left join [SYS_Parameters] I on I.[ParameterID]= E.[OperationID]
            left join [SYS_MESUsers] J on J.[MESUserID] = A.[InspectionUserID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' and A.[InspectionMethod]='{0}0201213000081' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@InspectionNo",SqlDbType.VarChar),

            };
            parameters[0].Value = DBNull.Value;


            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@InspectionNo",SqlDbType.VarChar),

            };
            Parcount[0].Value = DBNull.Value;


            if (!string.IsNullOrWhiteSpace(InspectionNo))
            {
                InspectionNo = "%" + InspectionNo + "%";
                sql = sql + String.Format(@" and A.[InspectionNo] like @InspectionNo ");
                parameters[0].Value = InspectionNo;
                Parcount[0].Value = InspectionNo;
            }



            string orderBy = "order By A.[Status],A.[DocumentDate] Desc,A.[InspectionNo] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        /// QCS00007开窗修改---移动开发
        /// Alvin 2017年9月11日14:24:58
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Qcs00007UpdateByOne(string userid, QCS_InspectionDocument Model)
        {
            try
            {
                string sql = String.Format(@"update[QCS_InspectionDocument] set {0},
                    [DocumentDate]=@DocumentDate,
                    [TaskDispatchID]=@TaskDispatchID,[InspectionDate]=@InspectionDate,
                    [InspectionUserID]=@InspectionUserID,[QualityControlDecision]=@QualityControlDecision,
                    [InspectionQuantity]=@InspectionQuantity,
                    [ScrappedQuantity]=@ScrappedQuantity,[NGquantity]=@NGquantity,[InspectionFlag]=@InspectionFlag,
                    [ConfirmUserID]=@ConfirmUserID,[ConfirmDate]=@ConfirmDate,[Comments]=@Comments
                     where [InspectionDocumentID]=@InspectionDocumentID", UniversalService.getUpdateUTC(userid));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@InspectionDocumentID",SqlDbType.VarChar),
                    new SqlParameter("@DocumentDate",SqlDbType.DateTime),
                    new SqlParameter("@TaskDispatchID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionDate",SqlDbType.DateTime),
                    new SqlParameter("@InspectionUserID",SqlDbType.VarChar),
                    new SqlParameter("@QualityControlDecision",SqlDbType.VarChar),
                    new SqlParameter("@InspectionQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ScrappedQuantity",SqlDbType.Decimal),
                    new SqlParameter("@NGquantity",SqlDbType.Decimal),
                    new SqlParameter("@InspectionFlag",SqlDbType.Bit),
                    new SqlParameter("@ConfirmUserID",SqlDbType.VarChar),
                    new SqlParameter("@ConfirmDate",SqlDbType.DateTime),
                    new SqlParameter("@Comments",SqlDbType.NVarChar)
                    };

                parameters[0].Value = Model.InspectionDocumentID;
                parameters[1].Value = (Object)Model.DocumentDate ?? DBNull.Value;
                parameters[2].Value = (Object)Model.TaskDispatchID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.InspectionDate ?? DBNull.Value;
                parameters[4].Value = (Object)Model.InspectionUserID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.QualityControlDecision ?? DBNull.Value;
                parameters[6].Value = (Object)Model.InspectionQuantity ?? DBNull.Value;
                parameters[7].Value = (Object)Model.ScrappedQuantity ?? DBNull.Value;
                parameters[8].Value = (Object)Model.NGquantity ?? DBNull.Value;
                parameters[9].Value = (Object)Model.InspectionFlag ?? DBNull.Value;
                parameters[10].Value = (Object)Model.ConfirmUserID ?? DBNull.Value;
                parameters[11].Value = (Object)Model.ConfirmDate ?? DBNull.Value;
                parameters[12].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }
        /// <summary>
        /// 检查检验单号是否已存在
        /// Mouse 2017年9月12日15:02:13
        /// </summary>
        /// <param name="InspectionNo"></param>
        /// <returns></returns>
        public static bool CheckInspectionNo(string InspectionNo)
        {
            try
            {
                string sql = string.Format(@"select * from [QCS_InspectionDocument] where [InspectionNo]='{1}' and [SystemID]='{0}' and [Status] <> '{0}0201213000003'", Framework.SystemID, InspectionNo);
                DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }catch(Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 制程巡检检验维护表头修改（移动端）
        /// Alvin 2017年9月12日16:07:38
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool Qcs00008UpdateByOne(string userid, QCS_InspectionDocument Model)
        {
            try
            {
                string sql = String.Format(@"update[QCS_InspectionDocument] set {0},
                    [DocumentDate]=@DocumentDate,
                    [QualityControlDecision]=@QualityControlDecision,
                    [ScrappedQuantity]=@ScrappedQuantity,
                    [NGquantity]=@NGquantity,
                    [ConfirmUserID]=@ConfirmUserID,[ConfirmDate]=@ConfirmDate,
                    [Comments]=@Comments
                     where [InspectionDocumentID]=@InspectionDocumentID", UniversalService.getUpdateUTC(userid));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@InspectionDocumentID",SqlDbType.VarChar),
                    new SqlParameter("@DocumentDate",SqlDbType.DateTime),
                    new SqlParameter("@QualityControlDecision",SqlDbType.VarChar),
                    new SqlParameter("@ScrappedQuantity",SqlDbType.Decimal),
                    new SqlParameter("@NGquantity",SqlDbType.Decimal),
                    new SqlParameter("@ConfirmUserID",SqlDbType.VarChar),
                    new SqlParameter("@ConfirmDate",SqlDbType.DateTime),
                    new SqlParameter("@Comments",SqlDbType.NVarChar)
                    };

                parameters[0].Value = Model.InspectionDocumentID;
                parameters[1].Value = (Object)Model.DocumentDate ?? DBNull.Value;
                parameters[2].Value = (Object)Model.QualityControlDecision ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ScrappedQuantity ?? DBNull.Value;
                parameters[4].Value = (Object)Model.NGquantity ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ConfirmUserID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.ConfirmDate ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Comments ?? DBNull.Value;

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

