using System.Data;

namespace Grace_DAL.DAL.Transaction
{
    public class T18021DAL:CommonDAL
    {
        public DataTable GetChatSummeryData()
        {
            DataTable sql = new DataTable();
            sql = Query($"select T18020.T_CHAT_CLIENT_ID, T_CHAT_CLIENT_NAME, T_CHAT_CLIENT_MOBILE, T_CHAT_CLIENT_ADDRESS, T18021.T_ENTRY_USER, T18020.T_ENTRY_DATE, T18021.T_CHAT_TEXT LAST_CHAT, T_CHAT_DATE, T_CHAT_TIME from T18020 JOIN T18021 ON T18020.T_CHAT_CLIENT_ID=T18021.T_CHAT_CLIENT_ID AND T18021.T_CHAT_ID=(select max(t21.T_CHAT_ID)from T18021 t21 WHERE t21.T_CHAT_CLIENT_ID=T18020.T_CHAT_CLIENT_ID)");
            return sql;
        }
        public DataTable GetChatListByMobile(string mobile)
        {
            DataTable sql = new DataTable();
            sql = Query($"select T18020.T_CHAT_CLIENT_ID, T_CHAT_CLIENT_NAME, T_CHAT_CLIENT_MOBILE, T_CHAT_CLIENT_ADDRESS, T18021.T_ENTRY_USER, T18020.T_ENTRY_DATE, T18021.T_CHAT_TEXT LAST_CHAT, T_CHAT_DATE, T_CHAT_TIME from T18020 JOIN T18021 ON T18020.T_CHAT_CLIENT_ID=T18021.T_CHAT_CLIENT_ID WHERE T_CHAT_CLIENT_MOBILE='{mobile}' ORDER BY T18021.T_CHAT_ID desc");
            return sql;
        }
    }
}
