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
    
    public partial class attr_types
    {
        public attr_types()
        {
            this.attr_values = new HashSet<attr_values>();
            this.changes = new HashSet<changes>();
        }
    
        public int attr_id { get; set; }
        public int type_id { get; set; }
        public string attr_name { get; set; }
        public string attr_def { get; set; }
    
        public virtual issue_types issue_types { get; set; }
        public virtual ICollection<attr_values> attr_values { get; set; }
        public virtual ICollection<changes> changes { get; set; }
    }
}
