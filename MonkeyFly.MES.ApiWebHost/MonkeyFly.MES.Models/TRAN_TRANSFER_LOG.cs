namespace MonkeyFly.MES.Models
{
    using MonkeyFly.Core;
    using System;
    public class TRAN_TRANSFER_LOG : IModel
    {
        public string Data { get; set; }
        public string KeyColum { get; set; }
        public DateTime? TransferDate { get; set; }
        public string Log { get; set; }
    }
}
