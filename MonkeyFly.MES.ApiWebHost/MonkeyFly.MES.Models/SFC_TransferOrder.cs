namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SFC_TransferOrder : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string TransferOrderID { get; set; }
        public string TransferNo { get; set; }
        public DateTime Date { get; set; }
        public int Sequence { get; set; }
        public string Type { get; set; }
        public string CompletionOrderID { get; set; }
        public string InspectionDocumentID { get; set; }        
        public int? IPQCSequence { get; set; }
        public string TaskDispatchID { get; set; }
        public string FabricatedMotherID { get; set; }
        public string FabMoProcessID { get; set; }
        public string FabMoOperationID { get; set; }
        public string ItemID { get; set; }
        public string ProcessID { get; set; }
        public string OperationID { get; set; }
        public decimal TransferQuantity { get; set; }
        public decimal ActualTransferQuantity { get; set; }
        public string Status { get; set; }
        public string AcceptUser { get; set; }
        public string NextFabMoProcessID { get; set; }
        public string NextFabMoOperationID { get; set; }
        public string NextProcessID { get; set; }
        public string NextOperationID { get; set; }
        public string Comments { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime ModifiedLocalTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateLocalTime { get; set; }
    }
}