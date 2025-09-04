namespace Grace_DAL.Shared.Inventory.Transaction
{
    public class T14048Data
    {
        public int T_QUATATION_ID { get; set; }
        public string T_MEMO_NO { get; set; }
        public int T_MEMO_SQ { get; set; }
        public string T_GRAND_TOTAL { get; set; }
        public string T_LABAUR_COST { get; set; }
        public decimal T_DISCOUNT { get; set; }
        public string T_AFTER_DISCOUNT { get; set; }
        public decimal T_PAMENT { get; set; }
        public decimal T_BALANCE { get; set; }
        public decimal T_DUE { get; set; }
        public decimal T_TOTAL_VAT { get; set; }
        public decimal T_VAT_TAX { get; set; }
        public string T_SALE_DATE { get; set; }
        public string T_SALE_TOTAL { get; set; }
        public string T_ENTRY_USER { get; set; }
        public string T_ENTRY_DATE { get; set; }
        public string T_UPDATE_USER { get; set; }
        public string T_UPDATE_DATE { get; set; }
        public string T_IN_WORD { get; set; }
        public string T_TYPE_CODE { get; set; }
        public string T_TYPE_NAME { get; set; }
        public string T_VERIFY_FLG { get; set; }

        public int T_CUSTOMER_ID { get; set; }
        public string T_CUSTOMER_NAME { get; set; }
        public string T_CUSTOMER_MOBILE { get; set; }
        public string T_CUSTOMER_ADDRESS { get; set; }
    }
    public class T14049Data
    {
        public int T_QUATATION_DETAILS_ID { get; set; }
        public int T_PUR_DETL_ID { get; set; }
        public string T_MEMO_NO { get; set; }
        public string T_TYPE_CODE { get; set; }
        public string T_PRODUCT_CODE { get; set; }
        public string T_PACK_CODE { get; set; }
        public string T_SALE_QUANTITY { get; set; }
        public string T_SALE_PRICE { get; set; }
        public string T_PURCHASE_PRICE { get; set; }
        public string T_TOTAL_PRICE { get; set; }
        public string T_CCN_FINISH_FLG { get; set; }
        public string T_COOCON_TYPE_CODE { get; set; }

    }
}
