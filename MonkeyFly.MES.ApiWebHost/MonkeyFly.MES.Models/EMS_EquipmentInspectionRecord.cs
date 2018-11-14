namespace MonkeyFly.MES.Models
{
using MonkeyFly.Core;
using System;
public class EMS_EquipmentInspectionRecord : IModel
{
public int ID { get; set; }
public string SystemID { get; set; }
public string EquipmentInspectionRecordID { get; set; }
public string Code { get; set; }
public DateTime Date { get; set; }
public string EquipmentID { get; set; }
public string ClassID { get; set; }
public string MESUserID { get; set; }
public string TaskID { get; set; }
public string ItemID { get; set; }
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
