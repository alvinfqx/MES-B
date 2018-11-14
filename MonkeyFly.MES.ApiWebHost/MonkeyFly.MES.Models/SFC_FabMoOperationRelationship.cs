namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SFC_FabMoOperationRelationship : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string FabMoOperationRelationshipID { get; set; }
        public string FabricatedMotherID { get; set; }
        public string FabMoProcessID { get; set; }
        public string FabMoOperationID { get; set; }
        public string PreFabMoOperationID { get; set; }
        public bool IsLastOperation { get; set; }
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
