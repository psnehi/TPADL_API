using BSESAPI.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;

namespace BSESAPI.Controllers
{
    public class TPADLController : ApiController
    {
        private static readonly ILog _Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //variables for TATA Power Ajmer
        string baseurl_TPADL = ConfigurationManager.AppSettings["TPADL_Bill_Fetch"];
        string username_TPADL = ConfigurationManager.AppSettings["TPADL_UserName"];
        string password_TPADL = ConfigurationManager.AppSettings["TPADL_Password"];
        
        public IHttpActionResult GetBill(string canumber)
        {
            TPADLAPIGetResponse getResponse = new TPADLAPIGetResponse();
            //  string url = @"/sap/opu/odata/sap/ZTPADL_CURRENTBILL_SRV/CurrentBillSet(ICaNumber='" + canumber + "')/?$expand=Bills,Sec,ConName";

            string url = @"sap/opu/odata/sap/ZTPADL_CURRENTBILL_SRV/CurrentBillSet(ICaNumber='" + canumber + "')/?$expand=Bills,Sec,ConName";
            string strResponse = string.Empty;

            var byteArray = Encoding.ASCII.GetBytes($"{username_TPADL}:{password_TPADL}");
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl_TPADL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    //client.Timeout = new TimeSpan(30000);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    //client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Basic dWNlc19zc29fZWFkOnRhdGFANzg5");
                    //HTTP GET
                    
                    var responseTask = client.GetAsync(url);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    _Log.Info("GetBillApi Response:-" + result);
                    if (result.IsSuccessStatusCode)
                    {

                        //var readTask = result.Content.ReadAsAsync<TPADLAPIResponse>();
                        var tt = result.Content.ReadAsStringAsync().Result;

                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(tt);

                        XmlNodeList nodelist = doc.GetElementsByTagName("m:properties");

                        foreach (XmlNode node in nodelist)
                        {

                            if (node.FirstChild == node["d:Paid"])
                            {
                                getResponse.Paid = node["d:Paid"].InnerText;
                                getResponse.CANumber = node["d:CaNumber"].InnerText;
                                getResponse.BillNumber = node["d:BillNumber"].InnerText;
                                getResponse.BillDate = node["d:BillDate"].InnerText;
                                getResponse.BillingPeriod = node["d:BillingPeriod"].InnerText;
                                getResponse.NetBillAmount = node["d:NetBillAmt"].InnerText;
                                getResponse.DiscountDate = node["d:DiscountDate"].InnerText.Substring(0, 10);
                                getResponse.BillAmount = node["d:BillAmnt"].InnerText;
                                getResponse.DueDate = node["d:DueDate"].InnerText.Substring(0, 10);
                            }
                            if (node.FirstChild == node["d:BlockFlag"])
                            {

                                getResponse.KNumber = node["d:LegacyKNo"].InnerText;
                            }

                            if (node.FirstChild == node["d:ICaNumber"])
                            {
                                getResponse.CustomerName = node["d:CName"].InnerText;
                               
                            }
                           




                        }

                    }
                }

            }
            catch (Exception ex)
            {
                _Log.Info("Message Error --> " + ex.Message);
                _Log.Info("-------------------------------------------------------------------------------");
                _Log.Info("InnerException Error --> " + ex.InnerException);
                _Log.Info("-------------------------------------------------------------------------------");
                _Log.Info("StackTrace Error --> " + ex.StackTrace);
                _Log.Info("-------------------------------------------------------------------------------");

            }
            //if (getResponse.Count == 0)
            //{
            //    return NotFound();
            //}

            return Ok(getResponse);
        }


        public string GetPostBill(string canumber)
        {
            string token;
            string url = @"sap/opu/odata/sap/ZTPADL_CAPAYPORTAL_SRV/capayportalSet(IVkont='" + canumber + "')";
            var byteArray = Encoding.ASCII.GetBytes($"{username_TPADL}:{password_TPADL}");

            string strResponse = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseurl_TPADL);
                    client.DefaultRequestHeaders.Accept.Clear();
                   // client.Timeout = new TimeSpan(30000);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/xml");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("X-CSRF-Token", "Fetch");
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    var responseTask = client.GetAsync(url);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    _Log.Info("GetPostBillApi Response:-" + result);
                    if (result.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(result.ReasonPhrase);
                    }
                    HttpHeaders headers = result.Headers;
                    IEnumerable<string> values;
                    if (headers.TryGetValues("X-CSRF-Token", out values))
                    {
                        return token = values.First();
                    }
                    else
                    {
                        return "Error";
                    }
                }
            }
            catch (Exception ex)
            {
                _Log.Info("Message Error --> " + ex.Message);
                _Log.Info("------------------------------------------------------------------------------");
                _Log.Info("InnerException Error --> " + ex.InnerException);
                _Log.Info("------------------------------------------------------------------------------");
                _Log.Info("StackTrace Error --> " + ex.StackTrace);
                _Log.Info("------------------------------------------------------------------------------");
                return "Error";
            }



        }




       /*    public IHttpActionResult PutPostBill(TPADLTrascationPostModel model)
           {
           string url = @"sap/opu/odata/sap/ZTPADL_CAPAYPORTAL_SRV/capayportalSet(IVkont='" + model.CANo + "')";
            /*  string requestXML = $@"<?xml version=""1.0"" encoding=""utf - 8""?>
  <entry xml:base=""http://tpcead.tpc.co.in:8000/sap/opu/odata/sap/ZTPADL_CAPAYPORTAL_SRV/"" xmlns=""http://www.w3.org/2005/Atom"" xmlns:m =""http://schemas.microsoft.com/ado/2007/08/dataservices/metadata"" xmlns:d =""http://schemas.microsoft.com/ado/2007/08/dataservices"" >
  <id>http://tpcead.tpc.co.in:8000/sap/opu/odata/sap/ZTPADL_CAPAYPORTAL_SRV/capayportalSet('{model.CANo}')</id>
   <title type =""text"">capayportalSet('{model.CANo}') </title>
   <updated>2018-01-11T11:15:11Z</updated>
   <category term =""ZTPADL_CAPAYPORTAL_SRV.capayportal"" scheme=""http://schemas.microsoft.com/ado/2007/08/dataservices/scheme""/>
   <link href=""capayportalSet('{model.CANo}')"" rel =""self"" title =""capayportal""/>
   <content type =""application/xml"">
       <m:properties>
         <d:IVkont>{model.CANo}</d:IVkont>
         <d:TranId/>
         <d:Status/>
         <d:SmsStatus/>
         <d:SmsDate m:null""true""/>
         <d:SdFlag/>
         <d:Pincode/>
         <d:PayMeth/>
         <d:PayDate m:null=""true""/>
         <d:PayAmt>0.00</d:PayAmt>
         <d:OredrNo/>
         <d:OrderId/>
         <d:Mobile/>
         <d:IntrimStatus/>
         <d:InsType/>
         <d:GwayStatus/>
         <d:Gateway/>
         <d:EmailStatus/>
         <d:EmailDate m:null=""true""/>
         <d:Email/>
         <d:Dcode/>
         <d:CustNo/>
         <d:BillNo/>
         <d:BankName/>
         <d:IUpdate/>
         <d:BillingPeriod/>
         <d:BillDate m:null=""true""/>
         <d:DiscountDate m:null =""true""/>
         <d:DueDate m:null=""true""/>
         <d:BillAmt>0.00</d:BillAmt>
         <d:NetBillAmt>0.00</d:NetBillAmt>
         <d:PayTime>PT00H00M00S</d:PayTime>
         <d:IPortal/>
         <d:BankCode/>
         <d:Exist>tru</d:Exist>
         <d:MICRCode/>
         <d:CheckNo/>
         <d:ReceiptNo/>
         <d:BankNameBranch/>
         <d:TotChqAmt>0.00</d:TotChqAmt>
         <d:PayLocation/>
         </m:properties>
         </content>
         </entry> ";*/
/*
            string requestxml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
  < entry xml:base=""http://tpcead.tpc.co.in:8000/sap/opu/odata/sap/ZTPADL_CAPAYPORTAL_SRV/"" xmlns=""http://www.w3.org/2005/Atom"" xmlns:m=""http://schemas.microsoft.com/ado/2007/08/dataservices/metadata"" xmlns:d=""http://schemas.microsoft.com/ado/2007/08/dataservices"" >
            
             <id> http://tpcead.tpc.co.in:8000/sap/opu/odata/sap/ZTPADL_CAPAYPORTAL_SRV/capayportalSet('{model.CANo}')</id>
 <title type=""text"" > capayportalSet('{model.CANo}')</title>
 <updated>2018-01-11T11:15:11Z</updated>
         
          <category term =""ZTPADL_CAPAYPORTAL_SRV.capayportal"" scheme=""http://schemas.microsoft.com/ado/2007/08/dataservices/scheme""/>
            
             <link href=""capayportalSet('{model.CANo}')"" rel=""self"" title= ""capayportal""/>
                 
                  <content type=""application/xml"">
                  
                    <m:properties>
                   
                     <d:IVkont>{model.CANo}</d:IVkont>
                       
                          <d:TranId>1000000</d:TranId>
                            
                               <d:Status>S</d:Status>
                                 
                                    <d:SmsStatus/>
                                  
                                     <d:SmsDate m:null=""true""/>
                                   
                                      <d:SdFlag/>
                                    
                                     
                                        < d:PayMeth > Cash </ d:PayMeth >
                                          
                                             < d:PayDate > 2018 - 08 - 27T00: 00:00 </ d:PayDate >
                                                    
                                                       < d:PayAmt > 3267.00 </ d:PayAmt >
                                                         
                                                            < d:OredrNo />
                                                          
                                                             < d:OrderId />
                                                           
                                                              < d:Mobile />
                                                            
                                                               < d:IntrimStatus />
                                                             
                                                                < d:InsType > W </ d:InsType >
                                                                  
                                                                     < d:GwayStatus />
                                                                   
                                                                      < d:Gateway > T </ d:Gateway >
                                                                        
                                                                           < d:EmailStatus />
                                                                         
                                                                            < d:EmailDate m:null = "true" />
                                                                          
                                                                             < d:Email />
                                                                           
                                                                              < d:Dcode />
                                                                            
                                                                               < d:CustNo > 800000249303 </ d:CustNo >
                                                                                 
                                                                                    < d:BillNo > 83000000221 </ d:BillNo >
                                                                                      
                                                                                         < d:BankName />
                                                                                       
                                                                                          < d:IUpdate />
                                                                                        
                                                                                           < d:BillingPeriod > 2018 / 03 </ d:BillingPeriod >
                                                                                               
                                                                                                  < d:BillDate > 2018 - 04 - 08T00: 00:00 </ d:BillDate >
                                                                                                         
                                                                                                            < d:DiscountDate > 2018 - 04 - 25T00: 00:00 </ d:DiscountDate >
                                                                                                                   
                                                                                                                      < d:DueDate > 2018 - 04 - 25T00: 00:00 </ d:DueDate >
                                                                                                                             
                                                                                                                                < d:BillAmt > 3267.00 </ d:BillAmt >
                                                                                                                                  
                                                                                                                                     < d:NetBillAmt > 3267.00 </ d:NetBillAmt >
                                                                                                                                       
                                                                                                                                          < d:PayTime > PT00H00M00S </ d:PayTime >
                                                                                                                                            
                                                                                                                                               < d:IPortal />
                                                                                                                                             
                                                                                                                                                < d:BankCode />
                                                                                                                                              
                                                                                                                                                 < d:Exist > true </ d:Exist >
                                                                                                                                                   
                                                                                                                                                      < d:MICRCode />
                                                                                                                                                    
                                                                                                                                                       < d:CheckNo />
                                                                                                                                                     
                                                                                                                                                        < d:ReceiptNo > 12345 </ d:ReceiptNo >
                                                                                                                                                          
                                                                                                                                                             < d:BankNameBranch />
                                                                                                                                                           
                                                                                                                                                              < d:TotChqAmt > 0.00 </ d:TotChqAmt >
                                                                                                                                                                
                                                                                                                                                                   < d:PayLocation > Vaishali Nagar </ d:PayLocation >
                                                                                                                                                                     
                                                                                                                                                                       </ m:properties >
                                                                                                                                                                      
                                                                                                                                                                      </ content >
                                                                                                                                                                      </ entry > ";
               var byteArray = Encoding.ASCII.GetBytes($"{username_TPADL}:{password_TPADL}");

               string strResponse = string.Empty;

               using (var client = new HttpClient())
               {
                   client.BaseAddress = new Uri(baseurl_TPADL);
                   client.DefaultRequestHeaders.Accept.Clear();
                   //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));
                   client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/xml");
                   client.DefaultRequestHeaders.TryAddWithoutValidation("X-CSRF-Token", token);
                   client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                   var responseTask = client.PutAsXmlAsync(url, requestXML);
                   responseTask.Wait();

                   var result = responseTask.Result;
                   if (result.IsSuccessStatusCode)
                   {




                   }
                   return Ok();
               }
           }
        */
           
    }
}

