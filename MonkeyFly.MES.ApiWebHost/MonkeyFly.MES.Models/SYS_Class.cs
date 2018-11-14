namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SYS_Class : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string ClassID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public bool CrossDay { get; set; }
        public string OnTime { get; set; }
        public string OffTime { get; set; }
        public decimal OffHour { get; set; }
        public decimal WorkHour { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime ModifiedLocalTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateLocalTime { get; set; }
    }
}