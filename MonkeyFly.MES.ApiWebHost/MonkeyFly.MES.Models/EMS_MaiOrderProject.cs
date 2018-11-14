namespace MonkeyFly.MES.Models
{
using MonkeyFly.Core;
using System;
public class EMS_MaiOrderProject : IModel
{
public int ID { get; set; }
public string SystemID { get; set; }
public string MaiOrderProjectID { get; set; }
public string MaintenanceOrderID { get; set; }
public string MaiOrderEquipmentID { get; set; }
public int Sequence { get; set; }
public string MaiProjectID { get; set; }
public string Attribute { get; set; }
public string AttributeValue { get; set; }
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
