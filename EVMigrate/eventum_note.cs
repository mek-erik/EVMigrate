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
    
    public partial class eventum_note
    {
        public long not_id { get; set; }
        public long not_iss_id { get; set; }
        public System.DateTime not_created_date { get; set; }
        public long not_usr_id { get; set; }
        public string not_title { get; set; }
        public string not_note { get; set; }
        public string not_full_message { get; set; }
        public Nullable<long> not_parent_id { get; set; }
        public string not_unknown_user { get; set; }
        public bool not_has_attachment { get; set; }
        public string not_message_id { get; set; }
        public bool not_removed { get; set; }
        public bool not_is_blocked { get; set; }
    }
}
