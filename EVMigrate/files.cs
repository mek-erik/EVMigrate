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
    
    public partial class files
    {
        public int file_id { get; set; }
        public string file_name { get; set; }
        public int file_size { get; set; }
        public byte[] file_data { get; set; }
        public string file_descr { get; set; }
        public sbyte file_storage { get; set; }
    
        public virtual changes changes { get; set; }
    }
}
