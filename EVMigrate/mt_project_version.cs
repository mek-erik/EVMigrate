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
    
    public partial class mt_project_version
    {
        public int id { get; set; }
        public long project_id { get; set; }
        public string version { get; set; }
        public string description { get; set; }
        public sbyte released { get; set; }
        public sbyte obsolete { get; set; }
        public long date_order { get; set; }
    }
}