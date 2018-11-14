namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SFC_FabMoRelationship : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string FabMoRelationshipID { get; set; }
        public string FabricatedMotherID { get; set; }
        public string FabMoProcessID { get; set; }
        public string PreFabMoProcessID { get; set; }
        public string Status { get; set; }
        public bool IfMain { get; set; }
        public bool IfLastProcess { get; set; }
        public string Comments { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime ModifiedLocalTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateLocalTime { get; set; }
    }
}
