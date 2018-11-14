namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SYS_AutoNumberRecord : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string AutoNumberRecordID { get; set; }
        public string AutoNumberID { get; set; }
        public string Prevchar { get; set; }
        public int Num { get; set; }
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
