namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SYS_Customers : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string CustomerID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Contacts { get; set; }
        public string Email { get; set; }
        public string MESUserID { get; set; }
        public string ClassOne { get; set; }
        public string ClassTwo { get; set; }
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
