using System.Data;

namespace Grace_DAL.Common
{
    public class T10000DAL:CommonDAL
    {
        public DataTable Parmision(string formCode, string loginCode)
        {
            DataTable sql = new DataTable();
            sql = Query($"SELECT * FROM T11000 WHERE T_ROLE = '{loginCode}' AND T_FORM_CODE = '{formCode}' ");
            return sql;
        }
    }
}
