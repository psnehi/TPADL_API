//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BSESAPI.DataLayer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransactionsDB
    {
        public int ID { get; set; }
        public string Mode { get; set; }
        public string TerminalID { get; set; }
        public string USP { get; set; }
        public string CANo { get; set; }
        public Nullable<decimal> RCPTNO { get; set; }
        public string RCPTREF { get; set; }
        public Nullable<decimal> BATCHNO { get; set; }
        public string PAYMENTMODE { get; set; }
        public Nullable<System.DateTime> PAYMENTDATE { get; set; }
        public Nullable<decimal> BILLAMOUNT { get; set; }
        public Nullable<decimal> COLLECTIONAMT { get; set; }
        public Nullable<decimal> BATCHPAYMODEAMT { get; set; }
        public string BILLER_CATEGORY { get; set; }
        public string BILLER_ID { get; set; }
        public string BILLER_NAME { get; set; }
        public string CUST_PARAMA_DETAILS { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string BILL_DATE { get; set; }
        public string BILL_PERIOD { get; set; }
        public string BILL_NUMBER { get; set; }
        public string CCF { get; set; }
        public string INITIATING_CHANNEL { get; set; }
        public string TRANSACTION_ID { get; set; }
        public string REFERENCE1 { get; set; }
        public string REFERENCE2 { get; set; }
        public string REFERENCE3 { get; set; }
        public string REFERENCE4 { get; set; }
        public string REFERENCE5 { get; set; }
        public string REFERENCE6 { get; set; }
        public string REFERENCE7 { get; set; }
        public string REFERENCE8 { get; set; }
        public string REFERENCE9 { get; set; }
        public string REFERENCE10 { get; set; }
        public string REFERENCE11 { get; set; }
        public string REFERENCE12 { get; set; }
        public string REFERENCE13 { get; set; }
        public string REFERENCE14 { get; set; }
        public string REFERENCE15 { get; set; }
        public string REFERENCE16 { get; set; }
        public string REFERENCE17 { get; set; }
        public string REFERENCE18 { get; set; }
        public string REFERENCE19 { get; set; }
        public string REFERENCE20 { get; set; }
        public string REFERENCE21 { get; set; }
        public string REFERENCE22 { get; set; }
        public string REFERENCE23 { get; set; }
        public string REFERENCE24 { get; set; }
        public string REFERENCE25 { get; set; }
        public string REFERENCE26 { get; set; }
        public string REFERENCE27 { get; set; }
        public string REFERENCE28 { get; set; }
        public string REFERENCE29 { get; set; }
        public string REFERENCE30 { get; set; }
        public string REFERENCE31 { get; set; }
        public string REFERENCE32 { get; set; }
        public string REFERENCE33 { get; set; }
        public string REFERENCE34 { get; set; }
        public string REFERENCE35 { get; set; }
        public string REFERENCE36 { get; set; }
        public string REFERENCE37 { get; set; }
        public string REFERENCE38 { get; set; }
        public string REFERENCE39 { get; set; }
        public string REFERENCE40 { get; set; }
        public string REFERENCE41 { get; set; }
        public string REFERENCE42 { get; set; }
        public string REFERENCE43 { get; set; }
        public string REFERENCE44 { get; set; }
        public string REFERENCE45 { get; set; }
        public string REFERENCE46 { get; set; }
        public Nullable<System.DateTime> REFERENCEDATE1 { get; set; }
        public Nullable<System.DateTime> REFERENCEDATE2 { get; set; }
        public Nullable<long> REFERENCEINT1 { get; set; }
        public Nullable<long> REFERENCEINT2 { get; set; }
        public Nullable<decimal> REFERENCEDECIMAL1 { get; set; }
        public Nullable<decimal> REFERENCEDECIMAL2 { get; set; }
        public Nullable<decimal> Denom1 { get; set; }
        public Nullable<decimal> Denom2 { get; set; }
        public Nullable<decimal> Denom5 { get; set; }
        public Nullable<decimal> Denom10 { get; set; }
        public Nullable<decimal> Denom20 { get; set; }
        public Nullable<decimal> Denom50 { get; set; }
        public Nullable<decimal> Denom100 { get; set; }
        public Nullable<decimal> Denom500 { get; set; }
        public Nullable<decimal> Denom1000 { get; set; }
        public Nullable<decimal> Denom2000 { get; set; }
        public Nullable<decimal> Coin1 { get; set; }
        public Nullable<decimal> Coin2 { get; set; }
        public Nullable<decimal> Coin5 { get; set; }
        public Nullable<decimal> Coin10 { get; set; }
        public string CHEQUENO { get; set; }
        public string CHEQUEDT { get; set; }
        public string MICR { get; set; }
        public string BANKCODE { get; set; }
        public string TrType { get; set; }
        public string BR_CODE { get; set; }
        public string BankShortName { get; set; }
        public string BankName { get; set; }
        public string ACTYPE { get; set; }
        public Nullable<bool> PostDated { get; set; }
        public Nullable<bool> ChequeReturned { get; set; }
        public byte[] FrontImage { get; set; }
        public string ImageName { get; set; }
        public Nullable<decimal> ImageSize { get; set; }
        public string ReportStatus { get; set; }
        public string UPDATIONSTATUS { get; set; }
        public string UpdationMessage { get; set; }
        public Nullable<long> ReportBatchNo { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string ENACCNO { get; set; }
        public string ENCOLLAMT { get; set; }
        public string update_token { get; set; }
        public string Ed { get; set; }
        public string Idf { get; set; }
        public string BillCyc { get; set; }
        public string BillGrp { get; set; }
        public string ApprovalCode { get; set; }
        public string CardNo { get; set; }
        public string ExpDate { get; set; }
        public string CardType { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<long> mobileno { get; set; }
        public string mailid { get; set; }
        public string flag { get; set; }
        public string BRPL_BYPL_FLAG { get; set; }
        public string Bill_Due_Date { get; set; }
    }
}
