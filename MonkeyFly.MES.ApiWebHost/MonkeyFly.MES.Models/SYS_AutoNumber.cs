namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SYS_AutoNumber : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string AutoNumberID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string DefaultCharacter { get; set; }
        public int YearLength { get; set; }
        public int MonthLength { get; set; }
        public int DateLength { get; set; }
        public int NumLength { get; set; }
        public int Length { get; set; }
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
