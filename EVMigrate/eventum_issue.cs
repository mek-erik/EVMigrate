//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EVMigrate
{
    using System;
    using System.Collections.Generic;
    
    public partial class eventum_issue
    {
        public long iss_id { get; set; }
        public Nullable<long> iss_customer_id { get; set; }
        public Nullable<long> iss_customer_contact_id { get; set; }
        public string iss_customer_contract_id { get; set; }
        public long iss_usr_id { get; set; }
        public Nullable<long> iss_grp_id { get; set; }
        public long iss_prj_id { get; set; }
        public long iss_prc_id { get; set; }
        public long iss_pre_id { get; set; }
        public short iss_pri_id { get; set; }
        public bool iss_sta_id { get; set; }
        public Nullable<long> iss_res_id { get; set; }
        public Nullable<long> iss_duplicated_iss_id { get; set; }
        public System.DateTime iss_created_date { get; set; }
        public Nullable<System.DateTime> iss_updated_date { get; set; }
        public Nullable<System.DateTime> iss_last_response_date { get; set; }
        public Nullable<System.DateTime> iss_first_response_date { get; set; }
        public Nullable<System.DateTime> iss_closed_date { get; set; }
        public Nullable<System.DateTime> iss_last_customer_action_date { get; set; }
        public Nullable<System.DateTime> iss_expected_resolution_date { get; set; }
        public string iss_summary { get; set; }
        public string iss_description { get; set; }
        public Nullable<float> iss_dev_time { get; set; }
        public Nullable<float> iss_developer_est_time { get; set; }
        public string iss_impact_analysis { get; set; }
        public string iss_contact_person_lname { get; set; }
        public string iss_contact_person_fname { get; set; }
        public string iss_contact_email { get; set; }
        public string iss_contact_phone { get; set; }
        public string iss_contact_timezone { get; set; }
        public Nullable<bool> iss_trigger_reminders { get; set; }
        public Nullable<System.DateTime> iss_last_public_action_date { get; set; }
        public string iss_last_public_action_type { get; set; }
        public Nullable<System.DateTime> iss_last_internal_action_date { get; set; }
        public string iss_last_internal_action_type { get; set; }
        public bool iss_private { get; set; }
        public Nullable<byte> iss_percent_complete { get; set; }
        public string iss_root_message_id { get; set; }
    }
}
