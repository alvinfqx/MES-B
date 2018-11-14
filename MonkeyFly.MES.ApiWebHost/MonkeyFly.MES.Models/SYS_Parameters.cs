namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SYS_Parameters : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string ParameterID { get; set; }
        public string ParentParameterID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string DescriptionOne { get; set; }
        public string ParameterTypeID { get; set; }
        public int IsEnable { get; set; }
        public bool IsDefault { get; set; }
        public int UsingType { get; set; }
        public bool IsSystem { get; set; }
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
