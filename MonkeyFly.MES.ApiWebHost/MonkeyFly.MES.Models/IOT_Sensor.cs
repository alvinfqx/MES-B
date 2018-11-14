namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class IOT_Sensor : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string SensorID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime? EnabledDate { get; set; }
        public DateTime? FailureDate { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public string ManufacturerID { get; set; }
        public bool IsWarning { get; set; }
        public decimal? MaxValue { get; set; }
        public decimal? MinValue { get; set; }
        public int? MaxAlarmTime { get; set; }
        public int? MinAlarmTime { get; set; }
        public string Comments { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime ModifiedLocalTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateLocalTime { get; set; }
    }
}
