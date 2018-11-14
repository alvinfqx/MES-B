namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SFC_ItemOperation : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string ItemOperationID { get; set; }
        public string ItemProcessID { get; set; }
        public string Sequence { get; set; }
        public string OperationID { get; set; }
        public string ProcessID { get; set; }
        public string WorkCenterID { get; set; }
        public string Unit { get; set; }
        public decimal UnitRatio { get; set; }
        public int StandardTime { get; set; }
        public int PrepareTime { get; set; }
        public bool IsIP { get; set; }
        public bool IsFPI { get; set; }
        public bool IsOSI { get; set; }
        public string InspectionGroupID { get; set; }
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
