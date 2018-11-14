namespace MonkeyFly.MES.Models
{
using MonkeyFly.Core;
using System;
public class SYS_MFCLog : IModel
{
public int ID { get; set; }
public string SystemID { get; set; }
public string UserID { get; set; }
public string UserName { get; set; }
public string Tag { get; set; }
public string Postion { get; set; }
public string Target { get; set; }
public string Type { get; set; }
public string Message { get; set; }
public DateTime CreateTime { get; set; }
}
}
