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
    
    public partial class issue_states
    {
        public int state_id { get; set; }
        public int user_id { get; set; }
        public int issue_id { get; set; }
        public Nullable<int> read_id { get; set; }
        public Nullable<int> subscription_id { get; set; }
    
        public virtual issues issues { get; set; }
        public virtual users users { get; set; }
    }
}
