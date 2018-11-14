namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class QCS_SamplingSetting : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string SamplingSettingID { get; set; }
        public string CategoryID { get; set; }
        public string InspectionMethod { get; set; }
        public string Disadvantages { get; set; }
        public string AQL { get; set; }
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
