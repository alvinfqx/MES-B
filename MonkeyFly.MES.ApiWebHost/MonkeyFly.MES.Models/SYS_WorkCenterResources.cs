namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SYS_WorkCenterResources : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string WorkCenterResourcesID { get; set; }
        public string WorkCenterID { get; set; }
        public string ProcessID { get; set; }
        public string OperationID { get; set; }
        public string ResourceID { get; set; }
        public bool IfMain { get; set; }
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
