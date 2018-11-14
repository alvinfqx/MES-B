namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class QCS_Complaint : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string ComplaintID { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Complaintor { get; set; }
        public string ApplicantID { get; set; }
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
