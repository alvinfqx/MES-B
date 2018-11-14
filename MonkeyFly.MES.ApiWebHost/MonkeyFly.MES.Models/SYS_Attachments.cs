namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class SYS_Attachments : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string AttachmentID { get; set; }
        public string ObjectID { get; set; }
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string Path { get; set; }
        public DateTime UploadTime { get; set; }
        public bool IsNotInit { get; set; }
        public bool Default { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int Sequence { get; set; }
        public string Comments { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
