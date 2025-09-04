namespace Grace_DAL.Shared.Inventory.Transaction
{
    public class T14076Data
    {
        public int T_VOUCHER_ID { get; set; }
        public string T_VOUCHER_CODE { get; set; }
        public string T_PROJECT_CODE { get; set; }
        public string T_PARTY_CODE { get; set; }
        public decimal T_TOTAL_TAKA { get; set; }         
 
    }
    public class T14077Data
    {
        public int T_VOUCHER_DETL_ID { get; set; }
        public string T_VOUCHER_CODE { get; set; }
        public string T_HEAD_CODE { get; set; }
        public string T_DESCRIPTION { get; set; }
        public decimal T_TOTAL_TAKA { get; set; }

    }
}
