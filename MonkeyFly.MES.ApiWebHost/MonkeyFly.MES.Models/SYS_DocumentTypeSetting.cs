namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SYS_DocumentTypeSetting : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string DTSID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string TypeID { get; set; }
        public bool IfDefault { get; set; }
        public string GiveWay { get; set; }
        public int YearLength { get; set; }
        public int MonthLength { get; set; }
        public int DateLength { get; set; }
        public bool Attribute { get; set; }
        public int CodeLength { get; set; }
        public int Length { get; set; }
        public string YearType { get; set; }
        public string Comments { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime ModifiedLocalTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateLocalTime { get; set; }
    }
}
