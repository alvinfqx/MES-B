//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MonkeyFly.MES.Models
{
    using Core;
    using System;

    public class SYS_LanguageLib : IModel
    {
        public int ID { get; set; }
        public string SystemID { get; set; }
        public string LanguageLibID { get; set; }
        public string TableID { get; set; }
        public string RowID { get; set; }
        public string Field { get; set; }
        public string OriginalLanguage { get; set; }
        public string OriginalContent { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageContentOne { get; set; }
        public string LanguageContentTwo { get; set; }
        public string Tag { get; set; }
        public Boolean IsDefault { get; set; }
        public string Comments { get; set; }
        public string Modifier { get; set; }
        public System.DateTime ModifiedTime { get; set; }
        public string Creator { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}