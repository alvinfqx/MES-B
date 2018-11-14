namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class EMS_CalledRepairOrder : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string CalledRepairOrderID { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public string EquipmentID { get; set; }
        public string Status { get; set; }
        public string CallMESUserID { get; set; }
        public string CallOrganizationID { get; set; }
        public string MESUserID { get; set; }
        public string InOutRepair { get; set; }
        public string ManufacturerID { get; set; }
        public string CloseMESUserID { get; set; }
        public DateTime CloseDate { get; set; }
        public string Comments { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime ModifiedLocalTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateLocalTime { get; set; }
    }
}
