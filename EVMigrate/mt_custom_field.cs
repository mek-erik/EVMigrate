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
    
    public partial class mt_custom_field
    {
        public int id { get; set; }
        public string name { get; set; }
        public short type { get; set; }
        public string possible_values { get; set; }
        public string default_value { get; set; }
        public string valid_regexp { get; set; }
        public short access_level_r { get; set; }
        public short access_level_rw { get; set; }
        public int length_min { get; set; }
        public int length_max { get; set; }
        public sbyte require_report { get; set; }
        public sbyte require_update { get; set; }
        public sbyte display_report { get; set; }
        public sbyte display_update { get; set; }
        public sbyte require_resolved { get; set; }
        public sbyte display_resolved { get; set; }
        public sbyte display_closed { get; set; }
        public sbyte require_closed { get; set; }
        public sbyte filter_by { get; set; }
    }
}
