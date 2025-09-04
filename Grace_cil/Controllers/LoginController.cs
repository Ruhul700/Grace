using Grace_DAL.Common;
using System;
using System.Data;
using System.Diagnostics;
using System.Web.Mvc;

namespace Grace_cil.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        LoginDAL loginDAL = new LoginDAL();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserLogin(string userId, string pass)
        {
            try
            {
                var sms = ""; //cmd: wmic bios get serialnumber
                              // var savetest = testDal.SaveData(); sr == "G9N0CV01J96835A"
                              //var sr = sirealNumber();
                var sr = "G9N0CV01J96835A";
                if (sr == "G9N0CV01J96835A")
                {
                    var data = loginDAL.GetData(userId, pass);
                    if (data.Rows.Count > 0)
                    {
                        foreach (DataRow i in data.Rows)
                        {
                            Session["T_EMP_ID"] = i["T_EMP_ID"].ToString();
                            Session["T_SITE_CODE"] = i["T_SITE_CODE"].ToString();
                            //  Session["LOGIN_PASS"] = i["LOGIN_PASS"].ToString();
                            //  Session["LOGIN_CODE"] = i["LOGIN_CODE"].ToString();
                            Session["T_ROLE"] = i["T_ROLE"].ToString();
                            sms = "1" + "-" + i["T_ROLE"].ToString();
                            // var myStr = Session["someKey1"] as String;
                        }
                    }
                    else
                    {
                        sms = "2";
                    }
                }
                else
                {
                    sms = "3";
                }

                return Json(sms, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(exc.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public string sirealNumber()
        {
            // var output = "";
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = "/C wmic bios get serialnumber";
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            process.WaitForExit();
            string output = process.StandardOutput.ReadToEnd();
            var dk = output.Substring(12);
            var result = dk.Trim();
            return result;
        }
    }
}