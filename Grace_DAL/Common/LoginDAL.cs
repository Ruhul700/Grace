using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grace_DAL.Common
{
  public class LoginDAL: CommonDAL
    {
        public DataTable GetData(string userId, string pass)
        {
            DataTable sql = new DataTable();
            sql = Query($"SELECT * FROM T00001 WHERE T_USER_NAME ='{userId}' AND T_USER_PASS ='{pass}' AND T_ACTIVE_FLAG='1'");
            return sql;
        }
    }
}
