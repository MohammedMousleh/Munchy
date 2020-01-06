using System;
using System.Collections.Generic;
using System.Text;

namespace Munchy.Models
{
    public class Payment
    {
        public int id { get; set; }
        public int merchant_id { get; set; }
        public string order_id { get; set; }
        public bool accepted { get; set; }
        public string type { get; set; }
        public object text_on_statement { get; set; }
        public object branding_id { get; set; }
        public PaymentVariables variables { get; set; }
        public string currency { get; set; }
        public string state { get; set; }
        public PaymentMetaData metadata { get; set; }
        public object link { get; set; }
        public object shipping_address { get; set; }
        public object invoice_address { get; set; }
        public object[] basket { get; set; }
        public object shipping { get; set; }
        public object[] operations { get; set; }
        public bool test_mode { get; set; }
        public object acquirer { get; set; }
        public object facilitator { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public object retented_at { get; set; }
        public int balance { get; set; }
        public object fee { get; set; }
        public object deadline_at { get; set; }
    }

    public class PaymentVariables
    {
    }

    public class PaymentMetaData
    {
        public object type { get; set; }
        public object origin { get; set; }
        public object brand { get; set; }
        public object bin { get; set; }
        public object corporate { get; set; }
        public object last4 { get; set; }
        public object exp_month { get; set; }
        public object exp_year { get; set; }
        public object country { get; set; }
        public object is_3d_secure { get; set; }
        public object issued_to { get; set; }
        public object hash { get; set; }
        public object number { get; set; }
        public object customer_ip { get; set; }
        public object customer_country { get; set; }
        public bool fraud_suspected { get; set; }
        public object[] fraud_remarks { get; set; }
        public bool fraud_reported { get; set; }
        public object fraud_report_description { get; set; }
        public object fraud_reported_at { get; set; }
        public object nin_number { get; set; }
        public object nin_country_code { get; set; }
        public object nin_gender { get; set; }
        public object shopsystem_name { get; set; }
        public object shopsystem_version { get; set; }
}
}
