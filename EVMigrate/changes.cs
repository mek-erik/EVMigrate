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
    
    public partial class changes
    {
        public int change_id { get; set; }
        public int issue_id { get; set; }
        public sbyte change_type { get; set; }
        public int stamp_id { get; set; }
        public Nullable<int> attr_id { get; set; }
        public string value_old { get; set; }
        public string value_new { get; set; }
        public Nullable<int> from_folder_id { get; set; }
        public Nullable<int> to_folder_id { get; set; }
        public Nullable<int> subscription_id { get; set; }
    
        public virtual attr_types attr_types { get; set; }
        public virtual stamps stamps { get; set; }
        public virtual folders folders { get; set; }
        public virtual issues issues { get; set; }
        public virtual folders folders1 { get; set; }
        public virtual comments comments { get; set; }
        public virtual files files { get; set; }
    }
}
