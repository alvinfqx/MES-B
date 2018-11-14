namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SFC_TaskDispatch : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string TaskDispatchID { get; set; }
        public string TaskNo { get; set; }
        public string Sequence { get; set; }
        public string FabricatedMotherID { get; set; }
        public string FabMoProcessID { get; set; }
        public string FabMoOperationID { get; set; }
        public int MoSequence { get; set; }
        public string ItemID { get; set; }
        public string ProcessID { get; set; }
        public string OperationID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool IsDispatch { get; set; }
        public string MESUserID { get; set; }
        public DateTime? DispatchDate { get; set; }
        public decimal DispatchQuantity { get; set; }
        public string ClassID { get; set; }
        public string InMESUserID { get; set; }
        public DateTime? InDateTime { get; set; }
        public string OutMESUserID { get; set; }
        public DateTime? OutDateTime { get; set; }
        public bool IsIP { get; set; }
        public bool IsFPI { get; set; }
        public bool IsOSI { get; set; }
        public string InspectionGroupID { get; set; }
        public bool FPIPass { get; set; }
        public decimal FinishQuantity { get; set; }
        public decimal ScrapQuantity { get; set; }
        public decimal DiffQuantity { get; set; }
        public decimal RepairQuantity { get; set; }
        public decimal LaborHour { get; set; }
        public decimal UnLaborHour { get; set; }
        public decimal MachineHour { get; set; }
        public decimal UnMachineHour { get; set; }
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
