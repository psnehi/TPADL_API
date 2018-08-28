using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSESAPI.Models
{
    public class TPADLTrascationPostModel
    {
        public string CANo { get; set; }
        public string TranId { get; set; }
        public string Status { get; set; }
        public string SmsStatus { get; set; }
        public string SmsDate { get; set; }
        public string SdFlag { get; set; }
        public string Pincode { get; set; }
        public string PayMeth { get; set; }
        public string PayDate { get; set; }
        public string PayAmt { get; set; }
        public string OredrNo { get; set; }
        public string OrderId { get; set; }
        public string Mobile { get; set; }
        public string IntrimStatus { get; set; }
        public string InsType { get; set; }
        public string GwayStatus { get; set; }
        public string Gateway { get; set; }
        public string EmailStatus { get; set; }
        public string EmailDate { get; set; }
        public string Email { get; set; }
        public string Dcode { get; set; }
        public string CustNo { get; set; }
        public string BillNo { get; set; }
        public string BankName { get; set; }
        public string IUpdate { get; set; }
        public string BillingPeriod { get; set; }
        public string BillDate { get; set; }
        public string DiscountDate { get; set; }
        public string DueDate { get; set; }
        public string BillAmt { get; set; }
        public string NetBillAmt { get; set; }
        public string PayTime { get; set; }
        public string IPortal { get; set; }
        public string BankCode { get; set; }
        public string MICRCode { get; set; }
        public string CheckNo { get; set; }
        public string ReceiptNo { get; set; }
        public string BankNameBranch { get; set; }
        public string TotChqAmt { get; set; }
        public string PayLocation { get; set; }
    }
}