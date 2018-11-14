namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class EMS_ServiceReasonLogDetails : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string SRLDID { get; set; }
        public string ServiceReasonLogID { get; set; }
        public int Sequence { get; set; }
        public string MESUserID { get; set; }
        public string OrganizationID { get; set; }
        public string ManufacturerID { get; set; }
        public bool IsFee { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal Hour { get; set; }
        public string Description { get; set; }
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
