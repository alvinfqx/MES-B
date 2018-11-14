namespace MonkeyFly.MES.Models
{
using MonkeyFly.Core;
using System;
public class QCS_CheckTestSettingDetails : IModel
{
public int ID { get; set; }
public string SystemID { get; set; }
public string CTSDID { get; set; }
public string CheckTestSettingID { get; set; }
public int Sequence { get; set; }
public decimal StartBatch { get; set; }
public decimal EndBatch { get; set; }
public decimal SamplingQuantity { get; set; }
public decimal AcQuantity { get; set; }
public decimal ReQuantity { get; set; }
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
