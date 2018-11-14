namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class IOT_DAQ_EVENT_VALUE : IModel
    {
        public decimal RecNo { get; set; }
        public string Sensor { get; set; }
        public decimal Measurement { get; set; }
        public string SensorDesc { get; set; }
    }
}
