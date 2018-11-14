namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class QCS_InspectionDocumentDetails : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string InspectionDocumentDetailID { get; set; }
        public string InspectionDocumentID { get; set; }
        public int Sequence { get; set; }
        public string InspectionMethod { get; set; }
        public string InspectionClassID { get; set; }
        public string InspectionMethodID { get; set; }
        public string InspectionItemID { get; set; }
        public string InspectionLevelID { get; set; }
        public string InspectionStandard { get; set; }
        public string InspectionFaultID { get; set; }
        public decimal SampleQuantity { get; set; }
        public string Aql { get; set; }
        public decimal AcQuantity { get; set; }
        public decimal ReQuantity { get; set; }
        public decimal NGquantity { get; set; }
        public string Attribute { get; set; }
        public string AttributeType { get; set; }
        public string QualityControlDecision { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime ModifiedLocalTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateLocalTime { get; set; }
    }
}
