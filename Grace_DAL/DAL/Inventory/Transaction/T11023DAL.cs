using Grace_DAL.Shared.Inventory.Transaction;
using System;
using System.Collections.Generic;
using System.Data;

namespace Grace_DAL.DAL.Inventory.Transaction
{
    public class T11023DAL : CommonDAL
    {
        public DataTable LoadData(string type, string roll)
        {
            DataTable dt = new DataTable();
            dt = Query($@"select t11.T_FORM_CODE,t11.T_LINK_NAME,t11.T_FORM_TYPE,t10.T_INSERT,t10.T_UPDATE,t10.T_DELETE,'0' T_CHK  from T11000 t11 left JOIN T10000 t10 on t11.T_FORM_CODE =t10.T_FORM_CODE and t10.T_ROLE ='{roll}' where t11.T_LINK_SEPARATION ='{type}' and t11.T_ROLE ='100'");
            return dt;
        }
        public DataTable RollData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_ROLL_CODE, T_ROLL_NAME  from T11104 ");
            return dt;

        }
        public string SaveData(string roll, List<T11023Data> t23List)
        {
            string sms = "";
            var date = DateTime.Now.ToString("dd-MM-yyyy");
            try
            {
                foreach (var i in t23List)
                {
                    var count = Query($"select count(*) T_COUNT from T10000 where T_ROLE ='{roll}' AND T_FORM_CODE='{i.T_FORM_CODE}'").Rows[0]["T_COUNT"].ToString();
                    if (Convert.ToInt32(count) > 0)
                    {
                        var update = Command($"UPDATE T10000 SET T_INSERT='{i.T_INSERT}',T_UPDATE='{i.T_UPDATE}',T_DELETE='{i.T_DELETE}' WHERE T_FORM_CODE ='{i.T_FORM_CODE}' AND T_ROLE='{roll}'");
                        sms = "Save Successfully-1";
                    }
                    else
                    {
                        var sa = Command($"INSERT INTO T10000 (T_FORM_CODE,T_ROLE,T_INSERT,T_UPDATE,T_DELETE ) VALUES('{i.T_FORM_CODE}','{roll}','{i.T_INSERT}','{i.T_UPDATE}','{i.T_DELETE}')");
                        sms = "Save Successfully-1";
                    }
                }
            }
            catch (Exception ex)
            {
                sms = "Do not Save-0";
            }

            return sms;
        }
    }
}
