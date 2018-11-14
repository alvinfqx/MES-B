namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SYS_PlantArea : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string PlantAreaID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string PlantID { get; set; }
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
