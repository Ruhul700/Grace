using Grace_DAL.Shared.Inventory.Transaction;
using System;
using System.Collections.Generic;
using System.Data;

namespace Grace_DAL.DAL.Inventory.Transaction
{
    public class T11022DAL : CommonDAL
    {
        public DataTable LoadData(string type, string roll)
        {
            DataTable dt = new DataTable();
            dt = Query($@"select t_1.T_FORM_TYPE_ID,t_1.T_SL,
t_1.T_FORM_TYPE, t_1.T_FORM_CODE, t_1.T_LINK_NAME,
t_1.T_LINK, t_1.T_LINK_SEPARATION, t_1.T_ROLE, t_1.T_TYPE,
t_1.T_TYPE_NAME, t_1.T_MODULE,(case when t_2.T_ACTIVE_FLAG ='1' then '1'else '0' end)T_ACTIVE_FLAG ,'0' T_CHK from (
select * from T11000 where T_ROLE ='100' AND T_LINK_SEPARATION ='{type}'
)t_1
left JOIN (select T_FORM_TYPE_ID,T_SL,
T_FORM_TYPE, T_FORM_CODE, T_LINK_NAME,
T_LINK, T_LINK_SEPARATION,T_ROLE, T_TYPE,
T_TYPE_NAME, T_MODULE, T_ACTIVE_FLAG,'0' T_CHK 
from T11000 where T_ROLE ='{roll}' AND T_LINK_SEPARATION ='{type}')t_2 ON t_1.T_FORM_CODE = t_2.T_FORM_CODE");
            return dt;
        }
        public DataTable RollData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_ROLL_CODE, T_ROLL_NAME  from T11104 ");
            return dt;

        }
        public string SaveData(string roll, List<T11022Data> t22List)
        {
            string sms = "";
            var date = DateTime.Now.ToString("dd-MM-yyyy");
            try
            {
                foreach (var i in t22List)
                {
                    var count = Query($"select count(*) T_COUNT from T11000 where T_ROLE ='{roll}' AND T_FORM_CODE='{i.T_FORM_CODE}'").Rows[0]["T_COUNT"].ToString();
                    if (Convert.ToInt32(count) > 0)
                    {
                        var update = Command($"UPDATE T11000 SET T_ACTIVE_FLAG='{i.T_ACTIVE_FLAG}' WHERE T_FORM_CODE ='{i.T_FORM_CODE}' AND T_ROLE='{roll}'");
                        sms = "Save Successfully-1";
                    }
                    else
                    {
                        var sa = Command($"INSERT INTO T11000 (T_SL,T_FORM_TYPE, T_FORM_CODE, T_LINK_NAME, T_LINK, T_LINK_SEPARATION, T_ROLE,T_TYPE, T_TYPE_NAME, T_MODULE,T_ACTIVE_FLAG) VALUES('{i.T_SL}','{i.T_FORM_TYPE}','{i.T_FORM_CODE}','{i.T_LINK_NAME}','{i.T_LINK}','{i.T_LINK_SEPARATION}','{roll}','{i.T_TYPE}','{i.T_TYPE_NAME}','{i.T_MODULE}','{i.T_ACTIVE_FLAG}')");
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
