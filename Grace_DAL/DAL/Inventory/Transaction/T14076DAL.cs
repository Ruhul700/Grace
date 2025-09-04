using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grace_DAL.DAL.Inventory.Transaction
{
   public class T14076DAL:CommonDAL
    {
        public DataTable GetParty()
        {
            DataTable sql = new DataTable();
            sql = Query($"select T_PARTY_CODE, T_PARTY_NAME  from T14070 ORDER BY T_PARTY_CODE ASC");
            return sql;
        }

        public DataTable GetProject()
        {
            DataTable sql = new DataTable();
            sql = Query($"SELECT T_PROJECT_CODE,T_PROJECT_NAME FROM T14072");
            return sql;
        }
        public DataTable GetHeaderData()
        {
            DataTable sql = new DataTable();
            sql = Query($"select T_HEAD_CODE, T_HEAD_NAME  from T14071 ORDER BY T_HEAD_CODE ASC");
            return sql;
        }
    }
}
