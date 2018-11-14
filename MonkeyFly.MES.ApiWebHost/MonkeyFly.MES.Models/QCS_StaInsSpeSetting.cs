namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class QCS_StaInsSpeSetting : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string StaInsSpeSettingID { get; set; }
        public string SettingType { get; set; }
        public string PartID { get; set; }
        public string InspectionType { get; set; }
        public string ProcessID { get; set; }
        public string OperationID { get; set; }
        public int Sequence { get; set; }
        public string CategoryID { get; set; }
        public string InspectionMethod { get; set; }
        public int InspectionDay { get; set; }
        public string InspectionStandard { get; set; }
        public string InspectionProjectID { get; set; }
        public string Attribute { get; set; }
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