namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SFC_ItemProcessRelationShip : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string IPRSID { get; set; }
        public string ItemID { get; set; }
        public string ItemProcessID { get; set; }
        public string PreItemProcessID { get; set; }
        public bool FinishProcess { get; set; }
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
