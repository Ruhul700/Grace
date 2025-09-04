using Grace_DAL.Shared.Inventory.Transaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grace_DAL.DAL.Inventory.Transaction
{
   public class T11019DAL:CommonDAL
    {
        public DataTable LoadData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_SITE_ID,T_SITE_CODE,T_SITE_NAME,T_SITE_IDNT_NO,T_SITE_ADDRESS, T_ENTRY_DATE from T11019  ORDER BY T_SITE_ID DESC");
            return dt;
        }
        public string SaveData(T11019Data data,string user)
        {
            string sms = "";
            var date = DateTime.Now.ToString("dd-MM-yyyy");
            if (data.T_SITE_ID == 0)
            {
                var maxCode = Query($"select MAX( cast( T_SITE_CODE as int)+10)T_SITE_CODE from T11019").Rows[0]["T_SITE_CODE"].ToString();
                var sa = Command($"INSERT INTO T11019 (T_SITE_CODE,T_SITE_NAME,T_SITE_IDNT_NO,T_SITE_ADDRESS, T_ENTRY_DATE,T_ENTRY_USER) VALUES('{maxCode}',N'{data.T_SITE_NAME}','{data.T_SITE_IDNT_NO}','{data.T_SITE_ADDRESS}','{date}','{user}')");
                if (sa == true)
                {
                    sms = "Save Successfully-1";
                }
                else
                {
                    sms = "Do not Save-0";
                }
            }
            else
            {
                var sa = Command($"UPDATE T11019 SET T_SITE_NAME=N'{data.T_SITE_NAME}',T_SITE_IDNT_NO='{data.T_SITE_IDNT_NO}', T_SITE_ADDRESS='{data.T_SITE_ADDRESS}',T_UPDATE_DATE ='{date}',T_UPDATE_USER='{user}' WHERE T_SITE_ID ='{data.T_SITE_ID}'");
                if (sa == true)
                {
                    sms = "Update Successfully-1";
                }
                else
                {
                    sms = "Do not Update-0";
                }
            }

            return sms;
        }
    }
}
