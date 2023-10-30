using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblNganLuongInfo
    {
        public int Id { get; set; }
        public string? ResultCode { get; set; }
        public string? TimeLimit { get; set; }
        public string? MerchantSiteCode { get; set; }
        public string? TransactionId { get; set; }
        public string? Amount { get; set; }
        public string? CurrencyCode { get; set; }
        public string? TransactionType { get; set; }
        public string? TransactionStatus { get; set; }
        public string? PayerName { get; set; }
        public string? PayerEmail { get; set; }
        public string? PayerMobile { get; set; }
        public string? ReceiverName { get; set; }
        public string? ReceiverAddress { get; set; }
        public string? ReceiverMobile { get; set; }
        public string? MethodPaymentName { get; set; }
        public string? ResultDescription { get; set; }
        public string? CardInfo { get; set; }
        public string? TokenCode { get; set; }
        public DateTime? Datein { get; set; }
    }
}
