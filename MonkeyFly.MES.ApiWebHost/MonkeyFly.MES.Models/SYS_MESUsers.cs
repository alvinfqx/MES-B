namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SYS_MESUsers : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string MESUserID { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
        public int Status { get; set; }
        public string UserName { get; set; }
        public string EnglishName { get; set; }
        public string Emplno { get; set; }
        public string CardCode { get; set; }
        public string JobTitle { get; set; }
        public bool Sex { get; set; }
        public string Email { get; set; }
        public string Brith { get; set; }
        public string IDcard { get; set; }
        public string InTime { get; set; }
        public string Type { get; set; }
        public string Mobile { get; set; }
        public int LoginCount { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string ConfigJSON { get; set; }
        public int Sequence { get; set; }
        public int Language { get; set; }       
        public string Comments { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime ModifiedLocalTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateLocalTime { get; set; }
        public string FunctionType { get; set; }
    }
}
