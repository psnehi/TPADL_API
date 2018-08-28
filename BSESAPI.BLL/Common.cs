using BSES.Models;
using BSESAPI.BLL.Interfaces;
using BSESAPI.DataLayer.Models;
using BSESAPI.Dto;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO.Compression;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;

namespace BSESAPI.BLL
{
    public class Common : ICommon
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string URL = "http://bulksmsindia.mobi/sendurlcomma.aspx?";
        BSESEntities db = new BSESEntities();
        public async Task<string> BatchCloseUpdate(List<batch_closing_master> batchCloseModel)
        {

            BatchCloseDto responseMessage = new BatchCloseDto();
            string result = string.Empty;
            List<BatchCloseResponseModel> updateDataList = new List<BatchCloseResponseModel>();
            List<batch_closing_master> insertDataList = new List<batch_closing_master>();
            try
            {


                foreach (var item in batchCloseModel)
                {
                    batch_closing_master data = db.batch_closing_master.FirstOrDefault(f => f.batchclosedatetime == item.batchclosedatetime && f.kiosk_id == item.kiosk_id);
                    BatchCloseResponseModel updateData = new BatchCloseResponseModel();
                    if (data == null)
                    {
                        batch_closing_master insertData = new batch_closing_master();
                        string token = Guid.NewGuid().ToString();

                        insertData.kiosk_id = item.kiosk_id;
                        insertData.reportbatchno = item.reportbatchno;
                        insertData.start_rcptno = item.start_rcptno;
                        insertData.end_rcptno = item.end_rcptno;
                        insertData.card_amnt = item.card_amnt;
                        insertData.card_rcpts = item.card_rcpts;
                        insertData.cash_amnt = item.cash_amnt;
                        insertData.cash_rcpts = item.cash_rcpts;
                        insertData.chq_amnt = item.chq_amnt;
                        insertData.chq_rcpts = item.chq_rcpts;
                        insertData.token_id = token;
                        insertData.ttl_amnt = item.ttl_amnt;
                        insertData.ttl_rcpts = item.ttl_rcpts;
                        insertData.batchclosedatetime = item.batchclosedatetime;
                        insertDataList.Add(insertData);
                        updateData.BatchCloseDateTime = item.batchclosedatetime;
                        updateData.KioskId = item.kiosk_id;
                        updateData.ReportBatchNo = item.reportbatchno;
                        updateData.Status = UpdateStatus.SUCCESS.ToString();
                        updateData.TokenId = token;
                    }
                    else
                    {
                        updateData.BatchCloseDateTime = item.batchclosedatetime;
                        updateData.KioskId = item.kiosk_id;
                        updateData.ReportBatchNo = item.reportbatchno;
                        updateData.Status = UpdateStatus.SUCCESS.ToString();
                        updateData.TokenId = data.token_id;
                    }

                    updateDataList.Add(updateData);
                }

                db.batch_closing_master.AddRange(insertDataList);
                await db.SaveChangesAsync();
                responseMessage.IsSuccess = true;
                responseMessage.Message = "Update Successful";
                responseMessage.UpdatedBatchCloseResponse = updateDataList;
                result = JsonConvert.SerializeObject(responseMessage);
            }
            catch (Exception ex)
            {
                responseMessage.IsSuccess = false;
                responseMessage.Message = "Update not Successful";
                responseMessage.UpdatedBatchCloseResponse = null;
                result = JsonConvert.SerializeObject(responseMessage);
                _log.Info("Batch Close Update Error Message --> " + ex.Message);
                _log.Info("Batch Close Update Inner Exception  Error Message --> " + ex.InnerException);
                _log.Info("Batch Close Update Error StackTrace --> " + ex.StackTrace);
            }
            _log.Info("Transaction Data Result --> " + result);
            return result;

        }

        public async Task<string> MakeKioskAttendance(List<kiosk_attendance> kioskAttendanceModel)
        {

            string result = string.Empty;
            KioskAttendanceDto kAttendnceResponseMsg = new KioskAttendanceDto();
            List<KioskAttendanceResponseModel> updateDataList = new List<KioskAttendanceResponseModel>();

            //List<kiosk_attendance> insertDataList = new List<kiosk_attendance>();
            try
            {
                foreach (var item in kioskAttendanceModel)
                {

                    kiosk_attendance data = db.kiosk_attendance.FirstOrDefault(s => s.kiosk_id == item.kiosk_id && s.atpm_mchn_ip == item.atpm_mchn_ip && s.login_date_time == item.login_date_time && s.log_out_time == item.log_out_time);



                    KioskAttendanceResponseModel updateData = new KioskAttendanceResponseModel();
                    if (data == null)
                    {

                        try
                        {
                            kiosk_attendance insertData = new kiosk_attendance();
                            string token = Guid.NewGuid().ToString();
                            insertData.inserted_by = item.inserted_by;
                            insertData.atpm_mchn_ip = item.atpm_mchn_ip;
                            insertData.kiosk_id = item.kiosk_id;
                            insertData.inserted_date = item.inserted_date;
                            insertData.login_date_time = item.login_date_time;
                            insertData.log_out_time = item.log_out_time;
                            insertData.status = UpdateStatus.SUCCESS.ToString();
                            insertData.tokenId = token;
                            insertData.UpdatedOn = DateTime.Now;
                            //insertDataList.Add(insertData);
                            updateData.AtpmMchnIp = item.atpm_mchn_ip;
                            updateData.KioskId = item.kiosk_id;
                            updateData.LoginDateTime = item.login_date_time;
                            updateData.LogOutTime = item.log_out_time;
                            updateData.Status = UpdateStatus.SUCCESS.ToString();
                            updateData.TokenId = token;
                            db.kiosk_attendance.Add(insertData);
                            await db.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            _log.Info("KioskAttendence Update Error --> " + ex.Message);
                            _log.Info("KioskAttendence Update Error --> " + ex.InnerException);
                            _log.Info("KioskAttendence Update Error --> " + ex.StackTrace);
                        }
                    }
                    else
                    {
                        updateData.AtpmMchnIp = item.atpm_mchn_ip;
                        updateData.KioskId = item.kiosk_id;
                        updateData.LoginDateTime = item.login_date_time;
                        updateData.LogOutTime = item.log_out_time;
                        updateData.Status = UpdateStatus.SUCCESS.ToString();
                        updateData.TokenId = data.tokenId;
                    }
                    updateDataList.Add(updateData);
                }

                //db.kiosk_attendance.AddRange(insertDataList);
                //db.SaveChanges();
                kAttendnceResponseMsg.IsSuccess = true;
                kAttendnceResponseMsg.Message = "Update Successful";
                kAttendnceResponseMsg.UpdatedKioskAttendanceResponse = updateDataList;
                result = JsonConvert.SerializeObject(kAttendnceResponseMsg);

            }
            catch (Exception ex)
            {
                kAttendnceResponseMsg.IsSuccess = false;
                kAttendnceResponseMsg.Message = "Update not Successful";
                kAttendnceResponseMsg.UpdatedKioskAttendanceResponse = null;
                result = JsonConvert.SerializeObject(kAttendnceResponseMsg);
                _log.Info("Make Kiosk Attendance Update Error Message --> " + ex.Message);
                _log.Info("Make Kiosk Attendance Update Inner Exception Error --> " + ex.InnerException);
                _log.Info("Make Kiosk Attendance Update Error StackTrace --> " + ex.StackTrace);
            }
            return result;


        }

        public async Task<string> SendEmail(SendEmailModel sendEmailModel)
        {

            _log.Info("Email Data Received --> " + JsonConvert.SerializeObject(sendEmailModel));
            Random rnd = new Random();
            string filePath = string.Empty;
            ResponseMessage responseObj = new ResponseMessage();
            MailMessage message = null;
            Attachment attachment = null;
            string[] toAddressList = sendEmailModel.ToAddress.Split(';');
            string result = string.Empty;
            try
            {
                if (sendEmailModel.AttachmentFile != null)
                {
                    filePath = HttpContext.Current.Server.MapPath("~") + "SendEmailFolder";
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    if (sendEmailModel.AttachFileName.Count > 1)
                    {
                        char[] delimiterChars = { '-', ':', '.' };
                        string fileName = Convert.ToString(DateTime.Now);
                        string name = string.Empty;
                        string[] splitWord = fileName.Split(delimiterChars);
                        foreach (string Name in splitWord)
                        {
                            name += Name;
                        }
                        filePath = filePath + "\\Mulit_File_Folder" + name; // Creating Folder
                        Directory.CreateDirectory(filePath);

                        for (int i = 0; i < sendEmailModel.AttachmentFile.Count; i++)
                        {
                            string newFileName = filePath + "\\File_" + rnd.Next(999) + "_" + sendEmailModel.AttachFileName[i];
                            File.WriteAllBytes(newFileName, sendEmailModel.AttachmentFile[i]);
                        }

                        string zipPath = filePath + ".zip";
                        ZipFile.CreateFromDirectory(filePath, zipPath);
                        DeleteFolder(filePath);
                        filePath = filePath + ".zip";
                    }
                    else
                    {
                        for (int i = 0; i < sendEmailModel.AttachmentFile.Count; i++)
                        {
                            filePath = filePath + "\\Receipt_" + rnd.Next(999) + sendEmailModel.AttachFileName[i];
                            File.WriteAllBytes(filePath, sendEmailModel.AttachmentFile[i]);
                        }
                    }

                }

                string fromAddress = ConfigurationManager.AppSettings["FromAddress_Kiosk"];
                string fromPassword = ConfigurationManager.AppSettings["FromPassword_Kiosk"];
                int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTP_Port_Kiosk"]);
                MailAddress fromEmail = new MailAddress(fromAddress, "BBPS System");

                string subject = sendEmailModel.EmailSubject;

                string body = sendEmailModel.MessageBody;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = smtpPort,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail.Address, fromPassword)

                };

                foreach (var toEmailAddress in toAddressList)
                {
                    message = new MailMessage(fromEmail.Address, toEmailAddress.Trim())
                    {
                        Subject = subject,
                        // Body = body,
                        IsBodyHtml = true,
                        Priority = MailPriority.Normal,
                        BodyEncoding = System.Text.Encoding.GetEncoding("utf-8")

                    };

                    System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(System.Text.RegularExpressions.Regex.Replace(body, @"< (.|\n) *?>", string.Empty), null, "text/plain");
                    System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(body, null, "text/html");

                    message.AlternateViews.Add(plainView);
                    message.AlternateViews.Add(htmlView);


                    if (filePath.Length > 0)
                    {
                        attachment = new Attachment(filePath, MediaTypeNames.Application.Octet);
                        ContentDisposition disposition = attachment.ContentDisposition;
                        disposition.CreationDate = File.GetCreationTime(filePath);
                        disposition.ModificationDate = File.GetLastWriteTime(filePath);
                        disposition.ReadDate = File.GetLastAccessTime(filePath);
                        disposition.FileName = Path.GetFileName(filePath);
                        disposition.Size = new FileInfo(filePath).Length;
                        disposition.DispositionType = DispositionTypeNames.Attachment;
                        message.Attachments.Add(attachment);
                    }

                    smtp.Send(message);
                }

                //File.Delete(filePath);

                responseObj.IsSuccess = true;
                responseObj.Message = "Email Sent Successfully !!!";
                responseObj.Response = "Email Sent Successfully !!!";
                result = JsonConvert.SerializeObject(responseObj);

            }
            catch (Exception ex)
            {
                responseObj.IsSuccess = false;
                responseObj.Message = "Email not Send !!!";
                responseObj.Response = ex.Message;
                _log.Info("Email Error --> " + ex.Message);
                _log.Info("Email Error --> " + ex.InnerException);
                _log.Info("Email Error --> " + ex.StackTrace);
                result = JsonConvert.SerializeObject(responseObj);
            }
            finally
            {
                result = JsonConvert.SerializeObject(responseObj);
            }

            _log.Info("Email Data Response --> " + result);
            return await Task.FromResult<string>(result);


        }
        private void DeleteFolder(string path)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(path);

                foreach (FileInfo f in di.GetFiles())
                {
                    f.Delete();
                }
                Directory.Delete(path);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<string> SendSMS(SMSModel smsModel)
        {
            
                _log.Info("SMS Data Received --> " + JsonConvert.SerializeObject(smsModel));
                string result = string.Empty;
                string response = string.Empty;
                ResponseMessage responseObj = new ResponseMessage();
                try
                {
                    string profileId = ConfigurationManager.AppSettings["ProfileID_Kiosk"];
                    string password = ConfigurationManager.AppSettings["Password_Kiosk"];
                    string senderId = ConfigurationManager.AppSettings["SenderID_Kiosk"];
                    string mobileNumber = string.Empty;

                    foreach (var number in smsModel.MobileNumber)
                    {
                        mobileNumber += number.Trim() + ",";
                    }

                    mobileNumber = mobileNumber.Remove(mobileNumber.Length - 1);
                    URL += "user=" + profileId + "&pwd=" + password + "&senderid=" + senderId + "&mobileno=" + mobileNumber + "&msgtext=" + smsModel.MessageBody + "&smstype=0";
                    response = SendRequest(URL);
                    SMSSendResponse sms_Response = JsonConvert.DeserializeObject<SMSSendResponse>(response);
                    if (sms_Response.MessageSendSuccess)
                    {
                        responseObj.IsSuccess = true;
                        responseObj.Message = "SMS Send";
                        responseObj.Response = "SMS Send Successfully";
                    }
                    else
                    {
                        responseObj.IsSuccess = false;
                        responseObj.Message = "SMS not Send !!!";
                        responseObj.Response = "SMS not Send !!!";
                    }

                    result = JsonConvert.SerializeObject(responseObj);
                }

                catch (Exception ex)
                {
                    responseObj.IsSuccess = false;
                    responseObj.Message = "SMS not Send !!!";
                    responseObj.Response = ex.Message;
                    result = JsonConvert.SerializeObject(responseObj);
                    _log.Info("SMS Error --> " + ex.Message);
                    _log.Info("SMS Error --> " + ex.InnerException);
                    _log.Info("SMS Error --> " + ex.StackTrace);
                }
                finally
                {
                    result = JsonConvert.SerializeObject(responseObj);
                }
                _log.Info("SMS Data Response --> " + result);
                return await Task.FromResult<string>(result);
            
        }
        public static void EnableTrustedHosts()
        {
            //Trust all certificates
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
        }

        public static string SendRequest(string url)
        {

            EnableTrustedHosts();
            string result = string.Empty;
            string sms_API_Response = string.Empty;
            SMSSendResponse res = new SMSSendResponse();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                httpWebRequest.Timeout = 10000;
                var httpresponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpresponse.GetResponseStream()))
                {
                    var output = streamReader.ReadToEnd();
                    sms_API_Response = output;
                }

                res.MessageSendSuccess = true;
                res.Message = sms_API_Response;
                res.Response = sms_API_Response;
                result = JsonConvert.SerializeObject(res);
                return result;
            }
            catch (Exception ex)
            {
                res.MessageSendSuccess = false;
                res.Message = "Exception: " + ex.Message; ;
                res.Response = "Exception: " + ex.Message; ;
                result = JsonConvert.SerializeObject(res);
                return result;
            }
        }
        public async Task<string> TransactionUpdate(TransactionsDB transactionsDbModel)
        {

            _log.Info("Transaction Data Received --> " + JsonConvert.SerializeObject(transactionsDbModel));

            TransactionDto responseMessage = new TransactionDto();
            string result = string.Empty;
            try
            {
                TransactionsDB data = db.TransactionsDBs.FirstOrDefault(w => w.TerminalID == transactionsDbModel.TerminalID.Trim() && w.RCPTNO == transactionsDbModel.RCPTNO && w.PAYMENTDATE == transactionsDbModel.PAYMENTDATE);
                TransactionBYPLResponseModel updateData = new TransactionBYPLResponseModel();
                string updateStatus = transactionsDbModel.UPDATIONSTATUS == UpdateStatus.PENDING.ToString() ? UpdateStatus.SUCCESS.ToString() : UpdateStatus.FAILURE.ToString();

                string token = Guid.NewGuid().ToString();

                transactionsDbModel.UPDATIONSTATUS = updateStatus;
                transactionsDbModel.update_token = token;
                if (data == null)
                {
                    db.TransactionsDBs.Add(transactionsDbModel);
                    await db.SaveChangesAsync();

                    updateData.CANo = transactionsDbModel.CANo;
                    updateData.PAYMENTDATE = transactionsDbModel.PAYMENTDATE;
                    updateData.RCPTNO = transactionsDbModel.RCPTNO;
                    updateData.TerminalID = transactionsDbModel.TerminalID;
                    updateData.TRANSACTION_ID = transactionsDbModel.TRANSACTION_ID;
                    updateData.UpdateToken = token;
                    updateData.UpdateStatus = updateStatus;
                }
                else
                {

                    updateData.CANo = data.CANo;
                    updateData.PAYMENTDATE = data.PAYMENTDATE;
                    updateData.RCPTNO = data.RCPTNO;
                    updateData.TerminalID = data.TerminalID;
                    updateData.TRANSACTION_ID = data.TRANSACTION_ID;
                    updateData.UpdateToken = data.update_token;
                    updateData.UpdateStatus = updateStatus;
                }

                responseMessage.IsSuccess = true;
                responseMessage.Message = "Update Successful";
                responseMessage.UpdatedTransactionList = updateData;
                result = JsonConvert.SerializeObject(responseMessage);
            }
            catch (Exception ex)
            {
                responseMessage.IsSuccess = false;
                responseMessage.Message = "Update not Successful";
                responseMessage.UpdatedTransactionList = null;
                result = JsonConvert.SerializeObject(responseMessage);
                _log.Info("Transaction Update Error Message:- --> " + ex.Message);
                _log.Info("Transaction Update Inner Exception Error Message:- --> " + ex.InnerException);
                _log.Info("Transaction Update Error Stack Trace:- --> " + ex.StackTrace);
            }
            _log.Info("Transaction Data Result --> " + result);
            return result;


        }
    }
}
