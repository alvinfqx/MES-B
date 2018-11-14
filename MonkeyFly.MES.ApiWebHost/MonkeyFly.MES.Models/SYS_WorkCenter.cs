namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SYS_WorkCenter : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string WorkCenterID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CalendarID { get; set; }
        public string InoutMark { get; set; }
        public string DepartmentID { get; set; }
        public bool ResourceReport { get; set; }
        public bool IsClass { get; set; }
        public string DispatchMode { get; set; }
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
