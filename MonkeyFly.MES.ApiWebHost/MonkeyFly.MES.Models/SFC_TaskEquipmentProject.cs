namespace MonkeyFly.MES.Models
{
using MonkeyFly.Core;
using System;
public class SFC_TaskEquipmentProject : IModel
{
public int ID { get; set; }
public string SystemID { get; set; }
public string TaskEquipmentProjectID { get; set; }
public string FabricatedProcessID { get; set; }
public string FabricatedOperationID { get; set; }
public string TaskDispatchID { get; set; }
public string EquipmentID { get; set; }
public string ProjectID { get; set; }
public bool IfCollection { get; set; }
public string CollectionWay { get; set; }
public string StandardValue { get; set; }
public string MaxValue { get; set; }
public string MinValue { get; set; }
public string RecordValue { get; set; }
public bool IfEntryRecord { get; set; }
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
