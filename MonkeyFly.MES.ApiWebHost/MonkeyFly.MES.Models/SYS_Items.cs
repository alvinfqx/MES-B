namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SYS_Items : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string ItemID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Specification { get; set; }
        public string Status { get; set; }
        public string Unit { get; set; }
        public string ClassOne { get; set; }
        public string ClassTwo { get; set; }
        public string ClassThree { get; set; }
        public string ClassFour { get; set; }
        public string ClassFive { get; set; }
        public string AuxUnit { get; set; }
        public decimal? AuxUnitRatio { get; set; }
        public bool IsCutMantissa { get; set; }
        public string CutMantissa { get; set; }
        public string Type { get; set; }
        public string Drawing { get; set; }
        public string PartSource { get; set; }
        public string BarCord { get; set; }
        public string GroupID { get; set; }
        public bool Lot { get; set; }
        public decimal OverRate { get; set; }
        public string Comments { get; set; }
        public bool SerialPart { get; set; }
        public bool KeyPart { get; set; }
        public string LotMethod { get; set; }
        public string LotClassID { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime ModifiedLocalTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateLocalTime { get; set; }
    }
}
