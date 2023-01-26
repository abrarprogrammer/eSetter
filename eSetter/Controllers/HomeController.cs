using eSetter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace eSetter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            ViewBag.requestBase = HttpContext.Request.Headers["Referer"];

            return View();
        }

        public ActionResult privacy()
        {
            return View();
        }

        public ActionResult privacyProtection()
        {
            return View();
        }

        public ActionResult settingmail()
        {
            return View();
        }

        public ActionResult faq()
        {
            return View();
        }

        public ActionResult errors()
        {
            return View();

        }

        public ActionResult Connect(Subscriber sub)
        {


            string auth = "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes("admin070:Bblk21_2"));
            string result = string.Empty;

            string str = string.Empty;
            string goods = string.Empty;
            string sessions = string.Empty;
            string pmo = string.Empty;
            try
            {
                using (var db = new SetterDbContext())
                {
                    var pm = db.Subscriber.Where(x => x.username == sub.username && x.prefix == sub.prefix && x.password == sub.password).FirstOrDefault();
                    if (pm == null)
                    {
                        result = "Погрешно корисничко име или лозинка";
                        return Content(result);
                    }
                    if (!pm.consent)
                    {
                        result = "Потребна е согласност";
                        return Content(result);
                    }
                }

                var user = string.Format("{0}@{1}.mk", sub.username, sub.prefix);
                var pass = sub.password;
                result = HTTPHandler.Connect(string.Format("https://webmail.{0}.mk", sub.prefix), "admin", "Bblk21_2", "eSetter", "user=" + user + "&pass=" + pass, 20000);
                

            }

            catch (Exception e)
            {
                result = "настана грешка";
                return Content(result);
            }

            return Content(result);
        }
    }
}