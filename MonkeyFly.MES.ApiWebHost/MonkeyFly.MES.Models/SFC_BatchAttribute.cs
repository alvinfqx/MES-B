namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SFC_BatchAttribute : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string BatchAttributeID { get; set; }
        public string CompletionOrderID { get; set; }
        public int Sequence { get; set; }
        public string BatchNo { get; set; }
        public DateTime EffectDate { get; set; }
        public decimal Quantity { get; set; }
        public string AutoNumberRecordID { get; set; }
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
