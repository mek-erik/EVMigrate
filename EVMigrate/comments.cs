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
    
    public partial class comments
    {
        public int comment_id { get; set; }
        public string comment_text { get; set; }
        public sbyte comment_format { get; set; }
    
        public virtual changes changes { get; set; }
    }
}