namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SYS_Organization : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string OrganizationID { get; set; }
        public string ParentOrganizationID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
        public string Manager { get; set; }
        public int Level { get; set; }
        public int FeeType { get; set; }
        public int HeadCount { get; set; }
        public int RealHeadCount { get; set; }
        public bool IsLegal { get; set; }
        public string PlantID { get; set; }
        public bool IfTop { get; set; }
        public int Sequence { get; set; }
        public string Comments { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime ModifiedLocalTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateLocalTime { get; set; }
    }
}
