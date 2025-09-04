namespace Grace_DAL.Common
{
    public class H00001DAL:CommonDAL
    {
        public string PanelPermission(string roll, string module)
        {
            var sql = Query($"select COUNT(*) COUNT from T00002 where T_ROLL_CODE ='{roll}' AND T_SITE_CODE='{module}'").Rows[0]["COUNT"].ToString();

            //var sql = Query($"select COUNT(*) COUNT,T00000.T_ROLE from T00001 JOIN T00000 ON T00001.T_ROLE =T00000.T_ROLE where T00001.T_TYPE ='{type}' AND T00000.T_USER_ID ='{userId}'AND T00000.T_PASSWORD ='{password}' GROUP BY T00000.T_ROLE"); //.Rows[0]["COUNT"].ToString()
            return sql;
        }
    }
}
