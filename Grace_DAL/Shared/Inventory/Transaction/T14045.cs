namespace Grace_DAL.Shared.Inventory.Transaction
{
    public class T14045Data
    {
        public int T_SALE_ID { get; set; }
        public int? T_CUSTOMER_ID { get; set; }
        public string T_MEMO_NO { get; set; }
        public decimal T_CASH { get; set; }
        public decimal T_DUE { get; set; }
        public decimal T_PAYMENT { get; set; }
        public decimal T_TOTAL_PRICE { get; set; }
        public string T_ENTRY_USER { get; set; }
        public string T_ENTRY_DATE { get; set; }
        public decimal T_RATE { get; set; }
        public string T_VERIFY_FLG { get; set; }
    }
}
