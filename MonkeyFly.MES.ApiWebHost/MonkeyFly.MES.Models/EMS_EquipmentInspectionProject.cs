namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class EMS_EquipmentInspectionProject : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string EIProjectID { get; set; }
        public string EquipmentID { get; set; }
        public string EquipmentProjectID { get; set; }
        public int Sequence { get; set; }
        public string Status { get; set; }
        public string ProjectID { get; set; }
        public string Comments { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime ModifiedLocalTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateLocalTime { get; set; }
    }
}
