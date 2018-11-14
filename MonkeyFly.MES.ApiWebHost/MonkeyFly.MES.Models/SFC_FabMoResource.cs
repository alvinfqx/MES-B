namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SFC_FabMoResource : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string FabMoResourceID { get; set; }
        public string FabMoProcessID { get; set; }
        public string FabMoOperationID { get; set; }
        public string Status { get; set; }
        public string ResourceID { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }
        public bool IfMain { get; set; }
        public string Comments { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime ModifiedLocalTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateLocalTime { get; set; }
    }
}
