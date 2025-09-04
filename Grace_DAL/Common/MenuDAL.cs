using System.Data;

namespace Grace_DAL.Common
{
    public  class MenuDAL: CommonDAL
    {
        public DataTable GetAllLinkData(string role, string module)
        {
            DataTable sql = new DataTable();
            sql = Query($"select T_FORM_TYPE,T_FORM_CODE,T_LINK_NAME,T_LINK,T_LINK_SEPARATION from T11000 WHERE T_ROLE='{role}' AND T_MODULE ='{module}' AND T_ACTIVE_FLAG='1' ORDER BY T_SL ASC");
            // var data=  ConvertToIenumerable(sql);
            return sql;
        }
    }
}
