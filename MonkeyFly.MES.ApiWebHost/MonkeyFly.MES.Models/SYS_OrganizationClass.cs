namespace MonkeyFly.MES.Models
{
using MonkeyFly.Core;
using System;
public class SYS_OrganizationClass : IModel
{
public int ID { get; set; }
public string SystemID { get; set; }
public string OrganizationClassID { get; set; }
public string OrganizationID { get; set; }
public string ClassID { get; set; }
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
