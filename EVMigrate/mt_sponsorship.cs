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
    
    public partial class mt_sponsorship
    {
        public int id { get; set; }
        public int bug_id { get; set; }
        public int user_id { get; set; }
        public int amount { get; set; }
        public string logo { get; set; }
        public string url { get; set; }
        public sbyte paid { get; set; }
        public long date_submitted { get; set; }
        public long last_updated { get; set; }
    }
}
