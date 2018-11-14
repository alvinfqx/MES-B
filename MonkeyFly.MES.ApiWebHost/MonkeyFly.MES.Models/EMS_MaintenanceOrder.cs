namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class EMS_MaintenanceOrder : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string MaintenanceOrderID { get; set; }
        public string MaintenanceNo { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? MaintenanceDate { get; set; }
        public string Type { get; set; }
        public string EquipmentMaintenanceListID { get; set; }
        public string OrganizationID { get; set; }
        public string ManufacturerID { get; set; }
        public string MESUserID { get; set; }
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
