namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class EMS_EquipmentProject : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string EquipmentProjectID { get; set; }
        public string EquipmentID { get; set; }
        public string ProjectID { get; set; }
        public bool IfCollection { get; set; }
        public string CollectionWay { get; set; }
        public string SensorID { get; set; }
        public string StandardValue { get; set; }
        public string MaxValue { get; set; }
        public string MinValue { get; set; }
        public int? MaxAlarmTime { get; set; }
        public int? MinAlarmTime { get; set; }
        public string MaxAlarmValue { get; set; }
        public string MinAlarmValue { get; set; }
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
