namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class EMS_EquipmentMaintenanceListDetails : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string EquipmentMaintenanceListDetailID { get; set; }
        public string EquipmentMaintenanceListID { get; set; }
        public int Sequence { get; set; }
        public int Type { get; set; }
        public string DetailID { get; set; }
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
