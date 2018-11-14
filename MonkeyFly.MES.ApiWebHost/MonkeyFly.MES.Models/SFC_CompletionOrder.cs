namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SFC_CompletionOrder : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string CompletionOrderID { get; set; }
        public string CompletionNo { get; set; }
        public DateTime? Date { get; set; }
        public int Sequence { get; set; }
        public string Type { get; set; }
        public string TaskDispatchID { get; set; }
        public string FabricatedMotherID { get; set; }
        public string FabMoProcessID { get; set; }
        public string FabMoOperationID { get; set; }
        public string OriginalCompletionOrderID { get; set; }
        public string InspectionID { get; set; }
        public string ItemID { get; set; }
        public string ProcessID { get; set; }
        public string OperationID { get; set; }
        public string ClassID { get; set; }
        public string NextFabMoProcessID { get; set; }
        public string NextFabMoOperationID { get; set; }
        public string NextProcessID { get; set; }
        public string NextOperationID { get; set; }
        public bool IsIF { get; set; }
        public string InspectionGroupID { get; set; }
        public decimal FinProQuantity { get; set; }
        public decimal ScrappedQuantity { get; set; }
        public decimal DifferenceQuantity { get; set; }
        public decimal RepairQuantity { get; set; }
        public decimal InspectionQuantity { get; set; }
        public int? LaborHour { get; set; }
        public int? UnLaborHour { get; set; }
        public int? MachineHour { get; set; }
        public int? UnMachineHour { get; set; }
        public string DTSID { get; set; }
        public string Status { get; set; }
        public string ReasonID { get; set; }
        public string Comments { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime ModifiedLocalTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateLocalTime { get; set; }
    }
}
