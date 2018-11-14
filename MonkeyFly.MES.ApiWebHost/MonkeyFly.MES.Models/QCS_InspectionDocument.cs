namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class QCS_InspectionDocument : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string InspectionDocumentID { get; set; }
        public string InspectionNo { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string ItemID { get; set; }
        public string InspectionMethod { get; set; }
        public string CompletionOrderID { get; set; }
        public string TaskDispatchID { get; set; }
        public DateTime? InspectionDate { get; set; }
        public string InspectionUserID { get; set; }
        public string QualityControlDecision { get; set; }
        public decimal FinQuantity { get; set; }
        public decimal InspectionQuantity { get; set; }
        public decimal ScrappedQuantity { get; set; }
        public decimal NGquantity { get; set; }
        public decimal OKQuantity { get; set; }
        public bool InspectionFlag { get; set; }
        public string Status { get; set; }
        public string ConfirmUserID { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public string Comments { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime ModifiedLocalTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateLocalTime { get; set; }
    }
}
