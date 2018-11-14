namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class IOT_DAQ_EVENT : IModel
    {
        public decimal RecNo { get; set; }
        public string RecType { get; set; }
        public string PlantID { get; set; }
        public string CtrlID { get; set; }
        public string EventID { get; set; }
        public string MachID { get; set; }
        public string DeviceID { get; set; }
        public DateTime MeasureTime { get; set; }
        public string CtrlDesc { get; set; }
        public string EventDesc { get; set; }
        public string DeviceDesc { get; set; }
    }
}
