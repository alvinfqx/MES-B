namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SFC_FabMoOperation : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string FabMoOperationID { get; set; }
        public string FabricatedMotherID { get; set; }
        public string FabMoProcessID { get; set; }
        public string OperationID { get; set; }
        public string Sequence { get; set; }
        public string UnitID { get; set; }
        public decimal UnitRate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal FinProQuantity { get; set; }
        public decimal OutProQuantity { get; set; }
        public decimal ScrappedQuantity { get; set; }
        public decimal DifferenceQuantity { get; set; }
        public decimal RepairQuantity { get; set; }
        public decimal PreProQuantity { get; set; }
        public decimal AssignQuantity { get; set; }
        public bool ResourceReport { get; set; }
        public int StandardTime { get; set; }
        public int PrepareTime { get; set; }
        public string TaskNo { get; set; }
        public string Status { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Comments { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime ModifiedLocalTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateLocalTime { get; set; }
        public string SourceID { get; set; }
    }
}
