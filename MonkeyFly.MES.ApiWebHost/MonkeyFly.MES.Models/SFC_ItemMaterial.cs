namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SFC_ItemMaterial : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string ItemMaterialID { get; set; }
        public string ItemProcessID { get; set; }
        public string ItemOperationID { get; set; }
        public string Sequence { get; set; }
        public string ItemID { get; set; }
        public decimal BasicQuantity { get; set; }
        public decimal AttritionRate { get; set; }
        public decimal UseQuantity { get; set; }
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
