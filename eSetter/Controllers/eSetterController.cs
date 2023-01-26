using eSetter.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using RestSharp;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Helpers;
using System.Collections.Generic;

namespace eSetter.Controllers
{
    [RoutePrefix("api/esetter")]
    public class eSetterController : ApiController
    {
        //POST: api/esetter
        [HttpPost]
        [Route("")]
        public IHttpActionResult createAccount([FromBody] webContent webcontent)
        {

            //string messageId, string businessNumber, string time, string sessionId, string msisdn, string operatorCode, string keyword, string command, string content
            //var filesReadToProvider = Request.Content.ReadAsMultipartAsync().Result;

            ////We will use two content part one is used to store the json another is used to store the image binary.
            //var messageId = filesReadToProvider.Contents[0].ReadAsStringAsync().Result;
            //var businessNumber = filesReadToProvider.Contents[1].ReadAsStringAsync().Result;
            //var time = filesReadToProvider.Contents[2].ReadAsStringAsync().Result;
            //var sessionId = filesReadToProvider.Contents[3].ReadAsStringAsync().Result;
            //var msisdn = filesReadToProvider.Contents[4].ReadAsStringAsync().Result;
            //var operatorCode = filesReadToProvider.Contents[5].ReadAsStringAsync().Result;
            //var keyword = filesReadToProvider.Contents[6].ReadAsStringAsync().Result;
            //var command = filesReadToProvider.Contents[7].ReadAsStringAsync().Result;
            //var content = filesReadToProvider.Contents[8].ReadAsStringAsync().Result;
            //string msisdn = "0038970300973"; 13

            string input = "messageId: " + webcontent.messageId + "; bussinessNumber: " + webcontent.businessNumber + "; time: " + webcontent.time + "; sessionId: " + webcontent.sessionId + "; msisdn: " + webcontent.msisdn + "; operatorCode: " + webcontent.operatorCode + "; keyword: " + webcontent.keyword + "; command: " + webcontent.command + "; content: " + webcontent.content;
            string type = webcontent.keyword == "START" ? "CreateAccount" : "DeleteAccount";
            string content = string.Empty;
            try
            {
                using (var db = new SetterDbContext())
                {
                    string domain = '0' + webcontent.msisdn.Substring(webcontent.msisdn.Length - 8, 2);
                    string phoneNumber = webcontent.msisdn.Substring(webcontent.msisdn.Length - 6, 6);
                    var ima = db.Subscriber.Where(x => x.prefix == domain && x.username == phoneNumber).FirstOrDefault();
                    if (ima != null)
                    {
                        bool dl = deleteAcount(ima.username, ima.prefix);
                    }

                    if (webcontent.keyword.Equals("STOP"))
                    {
                        return Ok();
                    }
                    string password = string.Empty;
                    string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
                    Random random = new Random();
                    char[] chars = new char[12];
                    for (int i = 0; i < 12; i++)
                    {
                        chars[i] = validChars[random.Next(0, validChars.Length)];
                    }
                    password = new string(chars);


                    Subscriber sb = new Subscriber();
                    sb.createdDate = DateTime.Now;
                    sb.prefix = domain;
                    sb.username = phoneNumber;
                    sb.password = password;
                    sb.consent = false;
                    db.Subscriber.Add(sb);
                    content = "Uspesno vi e kreiran mail akaunt. Za da pristapite do nego najavete se na e-setter.mk i vnesete ja privremenata lozinka: " + password;
                    db.SaveChanges();

                    logData(webcontent.msisdn, string.Empty, input, type, "Successfully Created Account for " + webcontent.msisdn);
                    /* string returnMsg = string.Format("http://mobilegate54.nth.ch:9099/?command=submitMessage&username=MKtest&password=KvABwheg&price=0&content={0}&businessNumber={1}&msisdn={2}&sessionId={3}&operatorCode={4}", content, webcontent.businessNumber, webcontent.msisdn, webcontent.sessionId, webcontent.operatorCode);

                     ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                     HttpWebRequest reqreturn = HttpWebRequest.Create(returnMsg) as HttpWebRequest;
                     ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };

                     reqreturn.UserAgent = "eSetter";
                     WebResponse respreturn = reqreturn.GetResponse();
                     Stream receiveStreamreturn = respreturn.GetResponseStream();
                     using (StreamReader readerReturn = new StreamReader(receiveStreamreturn, Encoding.UTF8))
                     {
                         string contentAppReturn = readerReturn.ReadToEnd();
                     } */


                }
            }
            catch (Exception ex)
            {
                logData(webcontent.msisdn, string.Empty, input, type, ex.Message);
                HttpResponseMessage rese = Request.CreateResponse(HttpStatusCode.OK, ex.Data);
                return Ok();
            }
            return Ok();
        }

        //POST: api/eSetter/notifyme

        [HttpPost, Route("notifyme")]
        public IHttpActionResult notifyme([FromBody] notification notification)
        {
            /*var filesReadToProvider = Request.Content.ReadAsMultipartAsync().Result;

            //We will use two content part one is used to store the json another is used to store the image binary.
            var messageStatus = filesReadToProvider.Contents[0].ReadAsStringAsync().Result;
            var messageStatusText = filesReadToProvider.Contents[1].ReadAsStringAsync().Result;
            var messageId = filesReadToProvider.Contents[2].ReadAsStringAsync().Result;
            var messageRef = filesReadToProvider.Contents[3].ReadAsStringAsync().Result;
            var businessNumber = filesReadToProvider.Contents[4].ReadAsStringAsync().Result;
            var time = filesReadToProvider.Contents[5].ReadAsStringAsync().Result;
            var sessionId = filesReadToProvider.Contents[6].ReadAsStringAsync().Result;
            var msisdn = filesReadToProvider.Contents[7].ReadAsStringAsync().Result;
            var command = filesReadToProvider.Contents[8].ReadAsStringAsync().Result;*/
            string input = "messageStatus: " + notification.messageStatus + "; messageStatusText: " + notification.messageStatusText + "; messageId: " + notification.messageId + "; messageRef: " + notification.messageRef + "; businessNumber: " + notification.businessNumber + "; time: " + notification.time + "; sessionId: " + notification.sessionId + "; msisdn: " + notification.msisdn + "; command: " + notification.command;
            string type = "Notification";
            try
            {
                logData(notification.msisdn, string.Empty, input, type, "Successfully Sent notification to " + notification.msisdn);
            }
            catch (Exception ex)
            {
                logData(notification.msisdn, string.Empty, input, type, ex.InnerException.Message);
                HttpResponseMessage rese = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Data);
                return Ok();
            }
            HttpResponseMessage res = Request.CreateResponse(HttpStatusCode.OK, "Успешно");
            return Ok();
        }

        public Boolean deleteAcount(string mail, string domain)
        {
            dynamic contentApp;
            List<string> lst = new List<string>();
            try
            {
                lst.Add("--update");
                lst.Add(mail + "@" + domain + ".mk");
                lst.Add("-status");
                lst.Add("disabled");
                contentApp = HTTPHandler.CreateRequest(lst);

                if (contentApp.code != 0)
                    throw new Exception(contentApp.stdout);

                using (var db = new SetterDbContext())
                {
                    Subscriber sb = db.Subscriber.Where(x => x.username == mail && x.prefix == domain).FirstOrDefault();
                    db.Subscriber.Remove(sb);
                    db.SaveChanges();
                }

                    logData(mail, domain, String.Empty, "DeleteAccountFunc", "Successfully Deleted Account " + mail + "@" + domain + ".mk");
                return true;
            }
            catch(Exception e)
            {
                logData(mail, domain, String.Empty, "DeleteAccountFunc", e.InnerException.Message);
                return false;
            }
        }

        //POST api/esetter/sendMail
        [ActionName("sendMail")]
        [Route("sendMail")]
        [HttpPost]
        public IHttpActionResult sendMail([FromBody] MailModel mailModel) { 
        
            try
            {
                MimeMessage message = new MimeMessage();
                message.To.Add(MailboxAddress.Parse("info@codepro.mk"));
                message.From.Add(new MailboxAddress(mailModel.name,mailModel.to));
                message.Subject = mailModel.title; 
                message.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                {
                    Text = mailModel.message
                }; 
                
                using (var client = new SmtpClient())
                {
                    client.CheckCertificateRevocation = false;
                    client.Connect("mail.codepro.mk", 587, SecureSocketOptions.None);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate("info@codepro.mk", "Damjanovski88"); 

                    client.Send(message);
                    client.Disconnect(true);
                }
                
                HttpResponseMessage res = Request.CreateResponse(HttpStatusCode.OK, "Успешно пратен меил");
                return ResponseMessage(res);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                HttpResponseMessage resp = Request.CreateResponse(HttpStatusCode.BadRequest, "Настана грешка. Обидете се повторно!");
                return ResponseMessage(resp);
            }
        }

        //POST: api/esetter/usersignin
        [ActionName("checkAccount")]
        [Route("checkAccount")]
        [HttpPost]
        public IHttpActionResult checkAccount([FromBody] Subscriber sub)
        {
            DataToReturn data = new DataToReturn();
            Subscriber pm;
            try
            {
                using (var db = new SetterDbContext())
                {
                    pm = db.Subscriber.Where(x => x.username == sub.username && x.prefix == sub.prefix && x.password == sub.password).FirstOrDefault();
                    if (pm == null)
                    {
                        HttpResponseMessage respo = Request.CreateResponse(HttpStatusCode.NotFound, "Погрешно корисничко име или лозинка");
                        return ResponseMessage(respo);
                    }
                    if (!pm.consent)
                    {
                        HttpResponseMessage res = Request.CreateResponse(HttpStatusCode.NotAcceptable, "Потребна е согласност");
                        return ResponseMessage(res);
                    }

                    pm.lastLoginDate = DateTime.Now;
                    db.Entry(pm).State = EntityState.Modified;
                    db.SaveChanges();
                }


                string token = HTTPHandler.GetToken();
                //var client = new RestClient("https://" + sub.prefix + ".mk:2096/login/");
                var client = new RestClient("https://070.mk:2096/login/");
                client.CookieContainer = new System.Net.CookieContainer();
                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("Keep-Alive", "false");
                request.AddHeader("X-API-Key", token);
                client.UserAgent = "no-reply@e-setter.mk";
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("user", sub.username + "@" + sub.prefix + ".mk");
                request.AddParameter("pass", sub.password);
                IRestResponse response = client.Execute(request);


                if (response.StatusCode == HttpStatusCode.OK && response.ResponseUri != null)
                {
                    data.data = InitForm(sub.username + "@" + sub.prefix + ".mk", sub.password, sub.prefix);
                    data.status = true;
                    
                    return Ok(data);
                }
                else
                {
                    logData(sub.username, sub.prefix, sub.ToString(), "LoginFunc", response.ErrorException.InnerException.Message);
                    HttpResponseMessage resp = Request.CreateResponse(HttpStatusCode.BadRequest, "Настана грешка. Обидете се повторно!");
                    return ResponseMessage(resp);
                }

            }
            catch (Exception e)
            {
                logData(sub.username, sub.prefix, sub.ToString(), "LoginFunc", e.InnerException.Message);
                HttpResponseMessage resp = Request.CreateResponse(HttpStatusCode.BadRequest, "Настана грешка. Обидете се повторно!");
                return ResponseMessage(resp);
            }
        }

        private string InitForm(string username, string password, string prefix)
        {
            try
            {
                return @$"<div style='display: none;' id='content-wrap'><form name='redirectForm' action='https://070.mk:2096/login/' method='post'> <input name='login_theme' type='hidden' value='cpanel' />
                                        < table class='login' style='width: 200px;' border='0' cellspacing='0' cellpadding='0'>
                                        <tbody style='text-align: left;'>
                                        <tr style='text-align: left;'>
                                        <td style='text-align: left;'><strong>Login</strong></td>
                                        <td style='text-align: left;'>&nbsp;</td>
                                        </tr>
                                        <tr style='text-align: left;'>
                                        <td class='login_lines' style='text-align: left;'>Email:</td>
                                        <td class='login_lines' style='text-align: left;'><input id='user' value={username} name='user' size='16' type='text' tabindex='1' /></td>
                                        </tr>
                                        <tr class='row2' style='text-align: left;'>
                                        <td class='login_lines' style='text-align: left;'>Password:</td>
                                        <td class='login_lines' style='text-align: left;'><input id='pass' name='pass' value={password} size='16' type='password' tabindex='2' /></td>
                                        </tr>
                                        <tr style='text-align: left;'>
                                        <td style='text-align: left;' colspan='2'><input id='login' class='input-button' type='submit' value='Login' tabindex='3' /></td>
                                        </tr>
                                        </tbody>
                                        </table>
                                        <input name='goto_uri' type='hidden' value='/horde/' /> </form> <br /> <br />
                    
                                  </div>";
            }
            catch
            {
                return null;
            }
        }

        //PUT: api/eSetter/updateUser
        [ActionName("updateUser")]
        [Route("updateUser")]
        [HttpPost]
        public IHttpActionResult updateUser([FromBody] Subscriber sub)
        {
            string str = string.Empty;
            dynamic contentApp;
            DataToReturn data = new DataToReturn();
            List<string> lst = new List<string>();
            try
            {
                using (var db = new SetterDbContext())
                {
                    var usr = db.Subscriber.Where(x => x.prefix == sub.prefix && x.username == sub.username && x.password == sub.password).FirstOrDefault();
                    if (usr == null)
                    {
                        HttpResponseMessage respo = Request.CreateResponse(HttpStatusCode.NotFound, "Погрешни податоци!");
                        return ResponseMessage(respo);
                    }

                    lst.Add("--create");
                    lst.Add(sub.username + "@" + sub.prefix + ".mk");
                    lst.Add("-passwd");
                    lst.Add(sub.password);
                    lst.Add("-mailbox");
                    lst.Add("true");
                    lst.Add("-mbox_quota");
                    lst.Add("125000");
                    contentApp = HTTPHandler.CreateRequest(lst);

                    if (contentApp.code != 0)
                        throw new Exception(contentApp.stdout);

                    usr.sex = sub.sex == "0" ? null : sub.sex;
                    usr.name = sub.name;
                    usr.lastName = sub.lastName;
                    usr.birthDate = sub.birthDate;
                    usr.consent = sub.consent;
                    usr.lastLoginDate = DateTime.Now;

                    db.Entry(usr).State = EntityState.Modified;
                    db.SaveChanges();
                    // var client = new RestClient("https://" + sub.prefix + ".mk:2096/login/");
                    var client = new RestClient("https://070.mk:2096/login/");
                    client.CookieContainer = new System.Net.CookieContainer();
                    RestRequest request = new RestRequest(Method.POST);
                    request.AddHeader("Keep-Alive", "false");
                    request.AddHeader("X-API-Key", null);
                    client.UserAgent = "no-reply@e-setter.mk";
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddParameter("user", sub.username + "@" + sub.prefix + ".mk");
                    request.AddParameter("pass", sub.password);
                    IRestResponse response = client.Execute(request);


                    if (response.StatusCode == HttpStatusCode.OK && response.ResponseUri != null)
                    {
                        data.data = InitForm(sub.username + "@" + sub.prefix + ".mk", sub.password, sub.prefix);
                        data.status = true;
                        return Ok(data);
                    }

                    HttpResponseMessage respons = Request.CreateResponse(response.StatusCode);
                    return ResponseMessage(respons);
                }
            }
            catch (Exception ex)
            {
                logData(sub.username, sub.prefix, sub.ToString(), "CreateAccountFunc", ex.InnerException.Message);
                HttpResponseMessage rese = Request.CreateResponse(HttpStatusCode.BadRequest, "Настана грешка. Обидете се повторно!");
                return ResponseMessage(rese);
            }
        }

        [ActionName("suspendUser")]
        [Route("suspendUser")]
        [HttpGet]
        public IHttpActionResult suspendAccount(String accountName,String prefix)
        {
            dynamic contentApp;
            List<string> lst = new List<string>();
            try
            {
                lst.Add("--update");
                lst.Add(prefix + "@" + accountName + ".mk");
                lst.Add("-status");
                lst.Add("disabled");
                contentApp = HTTPHandler.CreateRequest(lst);

                if (contentApp.code != 0)
                    throw new Exception(contentApp.stdout);

                logData(accountName, prefix, string.Empty, "suspendAccountFunc", "Successfully Suspended Account " + accountName + "@" + prefix + ".mk");

                HttpResponseMessage rese = Request.CreateResponse(HttpStatusCode.OK, true);
                return ResponseMessage(rese);
            }
            catch (Exception ex)
            {
                logData(accountName, prefix, string.Empty, "suspendAccountFunc", ex.InnerException.Message);
                HttpResponseMessage rese = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Data);
                return ResponseMessage(rese);
            }
        }

        [ActionName("unsuspendUser")]
        [Route("unsuspendUser")]
        [HttpGet]
        public IHttpActionResult unSuspendAccount(String accountName, String prefix)
        {
            dynamic contentApp;
            List<string> lst = new List<string>();
            try
            {
                lst.Add("--update");
                lst.Add(prefix + "@" + accountName + ".mk");
                lst.Add("-status");
                lst.Add("enabled");
                contentApp = HTTPHandler.CreateRequest(lst);

                if (contentApp.code != 0)
                    throw new Exception(contentApp.stdout);

                logData(accountName, prefix, string.Empty, "unsuspendAccountFunc", "Successfully Unsuspended Account " + accountName + "@" + prefix + ".mk");

                HttpResponseMessage rese = Request.CreateResponse(HttpStatusCode.OK, true);
                return ResponseMessage(rese);
            }
            catch (Exception ex)
            {
                logData(accountName, prefix, string.Empty, "unsuspendAccountFunc", ex.InnerException.Message);
                HttpResponseMessage rese = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Data);
                return ResponseMessage(rese);
            }
        }

        private bool logData(string msisdn, string prefix, string text, string type, string message)
        {
            using (var db = new SetterDbContext())
            {
                log lg = new log();
                lg.date = DateTime.Now;
                lg.msisdn = prefix != string.Empty  ? "003897" + (prefix + msisdn).Substring(1) : msisdn;
                lg.inputText = text;
                lg.type = type;
                lg.errorTxt = message;
                db.log.Add(lg);
                db.SaveChanges();
            }
            return true;
        }

        //PUT: api/eSetter/resetPassword
        [ActionName("resetPassword")]
        [Route("resetPassword")]
        [HttpPost]
        public IHttpActionResult resetPassword([FromBody] Subscriber sub)
        {
            List<string> lst = new List<string>();
            dynamic contentApp;
            string password = string.Empty;
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            try
            {
                Random random = new Random();
                Subscriber exists = new Subscriber();
                char[] chars = new char[12];
                string str = string.Empty;
                for (int i = 0; i < 12; i++)
                {
                    chars[i] = validChars[random.Next(0, validChars.Length)];
                }
                password = sub.password ?? new string(chars);

                using (var db = new SetterDbContext())
                {
                    if (sub.password == null)
                    {
                        exists = db.Subscriber.Where(x => x.birthDate == sub.birthDate && x.username == sub.username && x.prefix == sub.prefix).FirstOrDefault();
                        if (exists == null)
                        {
                            HttpResponseMessage responseReq = Request.CreateResponse(HttpStatusCode.NotFound, "Погрешни податоци");
                            return ResponseMessage(responseReq);
                        }
                        str = "Новата лозинка ви е пратена по SMS.";
                    }
                    else
                    {
                        exists = db.Subscriber.Where(x => x.username == sub.username && x.prefix == sub.prefix && x.password == sub.oldPass).FirstOrDefault();
                        if (exists == null)
                        {
                            HttpResponseMessage responseReq = Request.CreateResponse(HttpStatusCode.NotFound, "Погрешни податоци");
                            return ResponseMessage(responseReq);
                        }
                        str = "Променета ви е лозинката.";
                    }

                    lst.Add("--update");
                    lst.Add(exists.username + "@" + exists.prefix + ".mk");
                    lst.Add("-passwd");
                    lst.Add(password);
                    contentApp = HTTPHandler.CreateRequest(lst);

                    if (contentApp.code != 0)
                        throw new Exception(contentApp.stdout);

                    exists.password = password;
                    db.Entry(exists).State = EntityState.Modified;
                    db.SaveChanges();
                }
                HttpResponseMessage res = Request.CreateResponse(HttpStatusCode.OK, str);
                return ResponseMessage(res);
            }
            catch(Exception e)
            {
                logData(sub.username, sub.prefix, sub.ToString(), "passwordResetFunc", e.InnerException.Message);
                HttpResponseMessage res = Request.CreateResponse(HttpStatusCode.BadRequest, "Има проблем со промената на лозинката. Обидете се повторно!");
                return ResponseMessage(res);
            }
        }
    }

}