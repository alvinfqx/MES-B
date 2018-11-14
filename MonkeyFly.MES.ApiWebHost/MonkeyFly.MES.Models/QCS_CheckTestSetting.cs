namespace MonkeyFly.MES.Models
{
using MonkeyFly.Core;
using System;
public class QCS_CheckTestSetting : IModel
{
public int ID { get; set; }
public string SystemID { get; set; }
public string CheckTestSettingID { get; set; }
public string InspectionStandard { get; set; }
public string InspectionLevel { get; set; }
public string InspectionMethod { get; set; }
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
