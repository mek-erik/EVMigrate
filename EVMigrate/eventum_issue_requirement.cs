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
    
    public partial class eventum_issue_requirement
    {
        public long isr_id { get; set; }
        public long isr_iss_id { get; set; }
        public long isr_usr_id { get; set; }
        public Nullable<long> isr_updated_usr_id { get; set; }
        public System.DateTime isr_created_date { get; set; }
        public Nullable<System.DateTime> isr_updated_date { get; set; }
        public string isr_requirement { get; set; }
        public Nullable<float> isr_dev_time { get; set; }
        public string isr_impact_analysis { get; set; }
    }
}