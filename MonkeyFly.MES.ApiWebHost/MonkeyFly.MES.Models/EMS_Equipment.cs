namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class EMS_Equipment : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string EquipmentID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ResourceCategory { get; set; }
        public string PlantAreaID { get; set; }
        public string PlantID { get; set; }
        public string FixedAssets { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string ManufacturerID { get; set; }
        public string Model { get; set; }
        public string MachineNo { get; set; }
        public string ClassOne { get; set; }
        public string ClassTwo { get; set; }
        public string OrganizationID { get; set; }
        public string Status { get; set; }
        public string Condition { get; set; }
        public DateTime? ExpireDate { get; set; }
        public decimal? StdCapacity { get; set; }
        public decimal MaintenanceTime { get; set; }
        public decimal MaintenanceNum { get; set; }
        public decimal TotalOutput { get; set; }
        public decimal UsedTime { get; set; }
        public decimal UsableTime { get; set; }
        public decimal CavityMold { get; set; }
        public decimal UsedTimes { get; set; }
        public decimal UsableTimes { get; set; }
        public bool StatisticsFlag { get; set; }
        public string DescriptionOne { get; set; }
        public string DescriptionTwo { get; set; }
        public DateTime? DateOne { get; set; }
        public DateTime? DateTwo { get; set; }
        public decimal? NumOne { get; set; }
        public decimal? NumTwo { get; set; }
        public bool AbnormalStatus { get; set; }
        public string AttachmentID { get; set; }
        public string Comments { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime ModifiedLocalTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateLocalTime { get; set; }
        public string DAQMachID { get; set; }

    }
}
