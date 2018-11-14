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
    public class SFC_TransferOrderService : SuperModel<SFC_TransferOrder>
    {

        public static bool insert(string userId, SFC_TransferOrder Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_TransferOrder]([TransferOrderID],[TransferNo],
                [Date],[Sequence],[Type],
                [CompletionOrderID],[InspectionDocumentID],[IPQCSequence],
                [TaskDispatchID],[FabricatedMotherID],[FabMoProcessID],[FabMoOperationID],
                [ItemID],[ProcessID],[OperationID],
                [TransferQuantity],[ActualTransferQuantity],[Status],
                [AcceptUser],[NextFabMoProcessID],[NextFabMoOperationID],
                [NextProcessID],[NextOperationID],[Comments],
                [Modifier],[ModifiedTime],[ModifiedLocalTime],
                [Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@TransferOrderID,@TransferNo,
                @Date,@Sequence,@Type,
                @CompletionOrderID,@InspectionDocumentID,@IPQCSequence,@TaskDispatchID,
                @FabricatedMotherID,@FabMoProcessID,@FabMoOperationID,
                @ItemID,@ProcessID,@OperationID,
                @TransferQuantity,@ActualTransferQuantity,@Status,
                @AcceptUser,@NextFabMoProcessID,@NextFabMoOperationID,
                @NextProcessID,@NextOperationID,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.UtcNow, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TransferOrderID",SqlDbType.VarChar),
                    new SqlParameter("@TransferNo",SqlDbType.NVarChar),
                    new SqlParameter("@Date",SqlDbType.DateTime),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@CompletionOrderID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionDocumentID",SqlDbType.VarChar),
                    new SqlParameter("@TaskDispatchID",SqlDbType.VarChar),
                    new SqlParameter("@FabricatedMotherID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@ProcessID",SqlDbType.VarChar),
                    new SqlParameter("@OperationID",SqlDbType.VarChar),
                    new SqlParameter("@TransferQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ActualTransferQuantity",SqlDbType.Decimal),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@AcceptUser",SqlDbType.VarChar),
                    new SqlParameter("@NextFabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@NextFabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@NextProcessID",SqlDbType.VarChar),
                    new SqlParameter("@NextOperationID",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@IPQCSequence",SqlDbType.Int)
                    };

                parameters[0].Value = (Object)Model.TransferOrderID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.TransferNo ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Date ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[5].Value = (Object)Model.CompletionOrderID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.InspectionDocumentID ?? DBNull.Value;
                parameters[7].Value = (Object)Model.TaskDispatchID ?? DBNull.Value;
                parameters[8].Value = (Object)Model.FabricatedMotherID ?? DBNull.Value;
                parameters[9].Value = (Object)Model.FabMoProcessID ?? DBNull.Value;
                parameters[10].Value = (Object)Model.FabMoOperationID ?? DBNull.Value;
                parameters[11].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[12].Value = (Object)Model.ProcessID ?? DBNull.Value;
                parameters[13].Value = (Object)Model.OperationID ?? DBNull.Value;
                parameters[14].Value = (Object)Model.TransferQuantity ?? DBNull.Value;
                parameters[15].Value = (Object)Model.ActualTransferQuantity ?? DBNull.Value;
                parameters[16].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[17].Value = (Object)Model.AcceptUser ?? DBNull.Value;
                parameters[18].Value = (Object)Model.NextFabMoProcessID ?? DBNull.Value;
                parameters[19].Value = (Object)Model.NextFabMoOperationID ?? DBNull.Value;
                parameters[20].Value = (Object)Model.NextProcessID ?? DBNull.Value;
                parameters[21].Value = (Object)Model.NextOperationID ?? DBNull.Value;
                parameters[22].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[23].Value = (Object)Model.IPQCSequence ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static bool update(string userId, SFC_TransferOrder Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_TransferOrder] set {0},
                    [Sequence]=@Sequence,[IPQCSequence]=@IPQCSequence,
                    [Type]=@Type,[CompletionOrderID]=@CompletionOrderID,
                    [InspectionDocumentID]=@InspectionDocumentID,[TaskDispatchID]=@TaskDispatchID,
                    [FabricatedMotherID]=@FabricatedMotherID,[FabMoProcessID]=@FabMoProcessID,
                    [FabMoOperationID]=@FabMoOperationID,[ItemID]=@ItemID,
                    [ProcessID]=@ProcessID,[OperationID]=@OperationID,
                    [TransferQuantity]=@TransferQuantity,[ActualTransferQuantity]=@ActualTransferQuantity,
                    [Status]=@Status,[AcceptUser]=@AcceptUser,
                    [NextFabMoProcessID]=@NextFabMoProcessID,[NextFabMoOperationID]=@NextFabMoOperationID,
                    [NextProcessID]=@NextProcessID,[NextOperationID]=@NextOperationID,[Comments]=@Comments 
                    where [TransferOrderID]=@TransferOrderID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TransferOrderID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@CompletionOrderID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionDocumentID",SqlDbType.VarChar),
                    new SqlParameter("@TaskDispatchID",SqlDbType.VarChar),
                    new SqlParameter("@FabricatedMotherID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@ProcessID",SqlDbType.VarChar),
                    new SqlParameter("@OperationID",SqlDbType.VarChar),
                    new SqlParameter("@TransferQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ActualTransferQuantity",SqlDbType.Decimal),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@AcceptUser",SqlDbType.VarChar),
                    new SqlParameter("@NextFabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@NextFabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@NextProcessID",SqlDbType.VarChar),
                    new SqlParameter("@NextOperationID",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@IPQCSequence",SqlDbType.Int)
                    };

                parameters[0].Value = Model.TransferOrderID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[3].Value = (Object)Model.CompletionOrderID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.InspectionDocumentID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.TaskDispatchID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.FabricatedMotherID ?? DBNull.Value;
                parameters[7].Value = (Object)Model.FabMoProcessID ?? DBNull.Value;
                parameters[8].Value = (Object)Model.FabMoOperationID ?? DBNull.Value;
                parameters[9].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[10].Value = (Object)Model.ProcessID ?? DBNull.Value;
                parameters[11].Value = (Object)Model.OperationID ?? DBNull.Value;
                parameters[12].Value = (Object)Model.TransferQuantity ?? DBNull.Value;
                parameters[13].Value = (Object)Model.ActualTransferQuantity ?? DBNull.Value;
                parameters[14].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[15].Value = (Object)Model.AcceptUser ?? DBNull.Value;
                parameters[16].Value = (Object)Model.NextFabMoProcessID ?? DBNull.Value;
                parameters[17].Value = (Object)Model.NextFabMoOperationID ?? DBNull.Value;
                parameters[18].Value = (Object)Model.NextProcessID ?? DBNull.Value;
                parameters[19].Value = (Object)Model.NextOperationID ?? DBNull.Value;
                parameters[20].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[21].Value = (Object)Model.IPQCSequence ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static SFC_TransferOrder get(string TransferOrderID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_TransferOrder] where [TransferOrderID] = '{0}'  and [SystemID] = '{1}' ", TransferOrderID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        public static bool delete(string TransferOrderID)
        {
            try
            {
                string sql = string.Format(@"delete from [SFC_TransferOrder] where [TransferOrderID] = '{0}'  and [SystemID] = '{1}' ", TransferOrderID, Framework.SystemID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }

            catch (Exception ex)
            {

                DataLogerService.writeerrlog(ex);
                return false;
            }
        }
    }
}
