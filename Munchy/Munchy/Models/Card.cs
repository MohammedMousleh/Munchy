using System;
using System.Collections.Generic;
using System.Text;

namespace Munchy.Models
{
    public class Card
    {
        public int id { get; set; }
        public int merchant_id { get; set; }
        public bool accepted { get; set; }
        public object[] operations { get; set; }
        public CardMetaData metadata { get; set; }
        public object link { get; set; }
        public bool test_mode { get; set; }
        public object acquirer { get; set; }
        public string type { get; set; }
        public DateTime created_at { get; set; }
    }

    public class CardMetaData
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
