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
    
    public partial class mt_news
    {
        public long id { get; set; }
        public long project_id { get; set; }
        public long poster_id { get; set; }
        public short view_state { get; set; }
        public sbyte announcement { get; set; }
        public string headline { get; set; }
        public string body { get; set; }
        public long last_modified { get; set; }
        public long date_posted { get; set; }
    }
}