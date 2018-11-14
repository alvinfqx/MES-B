namespace MonkeyFly.MES.Models
{
    using Core;
    using System;
    public class SFC_FabricatedMother : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string FabricatedMotherID { get; set; }
        public string MoNo { get; set; }
        public DateTime? Date { get; set; }
        public string Version { get; set; }
        public int SplitSequence { get; set; }
        public string BatchNumber { get; set; }
        public string ItemID { get; set; }
        public decimal Quantity { get; set; }
        public string UnitID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string OrderNo { get; set; }
        public decimal OrderQuantity { get; set; }
        public string CustomerID { get; set; }
        public string MESUserID { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public string OrganizationID { get; set; }
        public string Status { get; set; }
        public decimal StorageQuantity { get; set; }
        public decimal SeparateQuantity { get; set; }
        public decimal OverRate { get; set; }
        public string Source { get; set; }
        public string OriginalFabricatedMotherID { get; set; }
        public string ApproveUserID { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string RoutID { get; set; }
        public string Comments { get; set; }
        public string ControlUserID { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime ModifiedLocalTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateLocalTime { get; set; }


        
    }
}
