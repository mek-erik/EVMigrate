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
    
    public partial class projects
    {
        public projects()
        {
            this.alerts = new HashSet<alerts>();
            this.folders = new HashSet<folders>();
            this.rights = new HashSet<rights>();
        }
    
        public int project_id { get; set; }
        public string project_name { get; set; }
        public Nullable<int> stamp_id { get; set; }
        public Nullable<int> descr_id { get; set; }
        public Nullable<int> descr_stub_id { get; set; }
        public sbyte is_public { get; set; }
        public sbyte is_archived { get; set; }
    
        public virtual ICollection<alerts> alerts { get; set; }
        public virtual ICollection<folders> folders { get; set; }
        public virtual project_descriptions project_descriptions { get; set; }
        public virtual ICollection<rights> rights { get; set; }
    }
}
