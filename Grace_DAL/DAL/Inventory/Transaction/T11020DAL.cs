using Grace_DAL.Shared.Inventory.Transaction;
using System;
using System.Data;

namespace Grace_DAL.DAL.Inventory.Transaction
{
    public class T11020DAL:CommonDAL
    {
        public DataTable LoadData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_USER_ID,T_USER_CODE,T_USER_NAME,T_USER_MOBILE,T_USER_ADDRESS, t11.T_GENDER_CODE, t01.T_GENDER_NAME, t11.T_RELIGION_CODE, t02.T_RELIGION_NAME, t11.T_DESIGNATION_CODE, t03.T_DESIGNATION_NAME, t11.T_ENTRY_DATE,t19.T_SITE_CODE ,t19.T_SITE_NAME from T11020 t11 JOIN T11101 t01 ON t11.T_GENDER_CODE = t01.T_GENDER_CODE JOIN T11102 t02 ON t11.T_RELIGION_CODE= t02.T_RELIGION_CODE JOIN T11103 t03 ON t11.T_DESIGNATION_CODE =t03.T_DESIGNATION_CODE JOIN T11019 t19 ON t11.T_SITE_CODE =t19.T_SITE_CODE  ORDER BY T_USER_ID DESC");
            return dt;
        }
        public DataTable GenderData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_GENDER_CODE, T_GENDER_NAME  from T11101 ");
            return dt;
        }
        public DataTable ReligionData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_RELIGION_CODE, T_RELIGION_NAME  from T11102 ");
            return dt;

        }
        public DataTable DesignationData()
        {
            DataTable dt = new DataTable();
            dt = Query("select T_DESIGNATION_CODE, T_DESIGNATION_NAME  from T11103  ");
            return dt;
        }
        public DataTable SiteData(string T_ROLL,string T_SITE_CODE)
        {
            DataTable dt = new DataTable();
            if (T_ROLL=="100") {
                dt = Query("select T_SITE_CODE, T_SITE_NAME  from T11019");
            } else {
                dt = Query($"select T_SITE_CODE, T_SITE_NAME  from T11019 WHERE T_SITE_CODE ='{T_SITE_CODE}'");
            }
           
            return dt;
        }
        public string SaveData(T11020Data t11020)
        {
            string sms = "";
            var date = DateTime.Now.ToString("dd-MM-yyyy");
            if (t11020.T_USER_ID == 0)
            {
                var maxCode = Query($"select MAX( cast( T_USER_CODE as int)+1)T_USER_CODE from T11020").Rows[0]["T_USER_CODE"].ToString();
                var sa = Command($"INSERT INTO T11020 (T_USER_CODE,T_USER_NAME,T_USER_MOBILE,T_USER_ADDRESS,T_GENDER_CODE, T_RELIGION_CODE,T_DESIGNATION_CODE,T_SITE_CODE,T_ENTRY_DATE) VALUES('{maxCode}','{t11020.T_USER_NAME}','{t11020.T_USER_MOBILE}','{t11020.T_USER_ADDRESS}','{t11020.T_GENDER_CODE}','{t11020.T_RELIGION_CODE}','{t11020.T_DESIGNATION_CODE}','{t11020.T_SITE_CODE}','{date}')");
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
                var sa = Command($"UPDATE T11020 SET T_USER_NAME='{t11020.T_USER_NAME}',T_USER_MOBILE='{t11020.T_USER_MOBILE}',T_USER_ADDRESS='{t11020.T_USER_ADDRESS}',T_GENDER_CODE='{t11020.T_GENDER_CODE}', T_RELIGION_CODE='{t11020.T_RELIGION_CODE}',T_DESIGNATION_CODE='{t11020.T_DESIGNATION_CODE}',T_SITE_CODE='{t11020.T_SITE_CODE}',T_UPDATE_DATE ='{date}' WHERE T_USER_ID ='{t11020.T_USER_ID}'");
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
