namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class QCS_ComplaintReason : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string ComplaintReasonID { get; set; }
        public string ComplaintID { get; set; }
        public string ComplaintDetailID { get; set; }
        public int Sequence { get; set; }
        public string ReasonGroupID { get; set; }
        public string ReasonID { get; set; }
        public string Status { get; set; }
        public decimal Quantity { get; set; }
        public string Comments { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime ModifiedLocalTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateLocalTime { get; set; }
    }
}
