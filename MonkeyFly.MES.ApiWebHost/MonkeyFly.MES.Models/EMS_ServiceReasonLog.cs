namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class EMS_ServiceReasonLog : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string ServiceReasonLogID { get; set; }
        public string CalledRepairOrderID { get; set; }
        public string ReasonID { get; set; }
        public string ReasonDescription { get; set; }
        public string ReasonGroupID { get; set; }
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
