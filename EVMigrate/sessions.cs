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
    
    public partial class sessions
    {
        public string session_id { get; set; }
        public int user_id { get; set; }
        public string session_data { get; set; }
        public int last_access { get; set; }
    
        public virtual users users { get; set; }
    }
}