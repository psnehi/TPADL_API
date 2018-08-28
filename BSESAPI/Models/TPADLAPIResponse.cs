using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSESAPI.Models
{
    public class TPADLAPIGetResponse
    {
        public string Paid { get; set; }
        public string CustomerName { get; set; }
        public string CANumber { get; set; }
        public string KNumber { get; set; }
        public string BillNumber { get; set; }
        public string BillDate { get; set; }
        public string BillingPeriod { get; set; }
        public string DiscountDate { get; set; }
        public string NetBillAmount { get; set; }
        public string BillAmount { get; set; }
        public string DueDate { get; set; }

    }

    //public class Entry
    //{
    //    public Content content { get; set; }

    //}

    //public class Content
    //{
    //    [JsonProperty(PropertyName = "m:properties")]
    //    public MProperties m_Properties { get; set; }
    //}

    //public class MProperties
    //{
    //    [JsonProperty(PropertyName = "d:ICaNumber")]
    //    public string ICaNumber { get; set; }
    //    [JsonProperty(PropertyName = "d:CaNumber")]
    //    public string CaNumber { get; set; }
    //    [JsonProperty(PropertyName = "d:BillNumber")]
    //    public string BillNumber { get; set; }
    //    [JsonProperty(PropertyName = "d:BillDate")]
    //    public BillDateC BillDate { get; set; }

    //    [JsonProperty(PropertyName = "d:BillingPeriod")]
    //    public string BillingPeriod { get; set; }
    //    [JsonProperty(PropertyName = "d:DiscountDate")]
    //    public DiscountDateC DiscountDate { get; set; }
    //    [JsonProperty(PropertyName = "d:NetBillAmt")]
    //    public string NetBillAmt { get; set; }
    //    [JsonProperty(PropertyName = "d:BillAmnt")]
    //    public string BillAmnt { get; set; }

    //    [JsonProperty(PropertyName = "d:CName")]
    //    public string CName { get; set; }
    //    [JsonProperty(PropertyName = "d:DueDate")]
    //    public DueDateC DueDate { get; set; }

    //}

    //public class DueDateC
    //{
    //    [JsonProperty(PropertyName = "@m:null")]
    //    public string m_Null { get; set; }
    //}
    //public class BillDateC
    //{
    //    [JsonProperty(PropertyName = "@m:null")]
    //    public string m_Null { get; set; }
    //}
    //public class DiscountDateC
    //{
    //    [JsonProperty(PropertyName = "@m:null")]
    //    public string m_Null { get; set; }
    //}

    //public class Model
    //{
    //    public Entry entry { get; set; }
    //}




}