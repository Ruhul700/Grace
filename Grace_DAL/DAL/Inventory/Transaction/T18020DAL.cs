using Grace_DAL.Shared.Transaction;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Grace_DAL.DAL.Transaction
{
    public class T18020DAL : CommonDAL
    {
        public DataTable GetClientData(string mobile)
        {
            DataTable dt = new DataTable();
            dt = Query($"select T18020.T_CHAT_CLIENT_ID, T_CHAT_CLIENT_NAME, T_CHAT_CLIENT_MOBILE, T_CHAT_CLIENT_ADDRESS, T18021.T_ENTRY_USER, T18020.T_ENTRY_DATE, T18021.T_CHAT_TEXT LAST_CHAT, T_CHAT_DATE, T_CHAT_TIME,CONCAT(T11020.T_USER_NAME,';',T_CHAT_DATE,';', T_CHAT_TIME) T_USER from T18020 JOIN T18021 ON T18021.T_CHAT_ID=(select max(t21.T_CHAT_ID)from T18021 t21 where T18020.T_CHAT_CLIENT_ID=t21.T_CHAT_CLIENT_ID ) JOIN T11020 ON T18021.T_ENTRY_USER=T11020.T_USER_CODE  WHERE T_CHAT_CLIENT_MOBILE='{mobile}' ");
            return dt;
        }
        public string SaveData(T18020Data t20, string user)
        {
            string sms = "";
            var clientId = "";
            SqlTransaction objTrans = null;
            var date = DateTime.Now.ToString("dd-MM-yyyy");
            var time = DateTime.Now.ToString("HH:mm");

            using (SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString))
            {
                objConn.Open();
                objTrans = objConn.BeginTransaction();
                try
                {
                    var isExt = Query($"select count(*) T_COUNT from T18020 WHERE T_CHAT_CLIENT_MOBILE='{t20.T_CHAT_CLIENT_MOBILE}'").Rows[0]["T_COUNT"].ToString();
                    //--------------
                    if (isExt == "0")
                    {
                        var maxId = Query($"select CASE WHEN count(*)>0 then (select MAX( cast( T_CHAT_CLIENT_ID as int)+1)T_CHAT_CLIENT_ID from t18020) ELSE '1' end T_CHAT_CLIENT_ID  from T18020").Rows[0]["T_CHAT_CLIENT_ID"].ToString();
                        //---------------
                        var query20 = $"INSERT INTO T18020 (T_CHAT_CLIENT_ID,T_CHAT_CLIENT_NAME, T_CHAT_CLIENT_MOBILE,T_CHAT_CLIENT_ADDRESS, T_ENTRY_DATE,T_ENTRY_USER) VALUES('{maxId}', '{t20.T_CHAT_CLIENT_NAME}','{t20.T_CHAT_CLIENT_MOBILE}','{t20.T_CHAT_CLIENT_ADDRESS}','{date}','{t20.T_ENTRY_USER}')";
                        command_2(query20, objConn, objTrans);
                        t20.T_CHAT_CLIENT_ID = maxId;
                    }
                    //--------------------
                    var max21 = Query_2($@"select CASE WHEN count(*)>0 then (select MAX( cast( T_CHAT_ID as int)+1)T_CHAT_ID from t18021) ELSE '1' end T_CHAT_ID  from T18021", objConn, objTrans).Rows[0]["T_CHAT_ID"].ToString();

                    var query21 = $"INSERT INTO T18021 (T_CHAT_ID,T_CHAT_CLIENT_ID,T_CHAT_TEXT, T_CHAT_DATE,T_CHAT_TIME,T_ENTRY_USER) VALUES('{max21}','{t20.T_CHAT_CLIENT_ID}','{t20.T_CHAT_TEXT}','{date}','{time}','{user}')";
                    var sav21 = command_2(query21, objConn, objTrans);
                    if (sav21)
                    {
                        sms = "Save Successfully-1";
                        objTrans.Commit();
                    }
                    else
                    {
                        sms = "Do not Save-0";
                        objTrans.Rollback();
                    }

                }
                catch (Exception ex)
                {
                    var kk = ex.Message;
                    sms = " Do not Save-0";
                    objTrans.Rollback();
                }
                finally
                {
                    objConn.Close();
                }
            }
            return sms;
        }
    }
}
