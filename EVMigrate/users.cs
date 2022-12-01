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
    
    public partial class users
    {
        public users()
        {
            this.alerts = new HashSet<alerts>();
            this.email_inboxes = new HashSet<email_inboxes>();
            this.issue_states = new HashSet<issue_states>();
            this.log_events = new HashSet<log_events>();
            this.preferences = new HashSet<preferences>();
            this.rights = new HashSet<rights>();
            this.sessions = new HashSet<sessions>();
            this.stamps = new HashSet<stamps>();
            this.subscriptions = new HashSet<subscriptions>();
            this.view_preferences = new HashSet<view_preferences>();
            this.views = new HashSet<views>();
        }
    
        public int user_id { get; set; }
        public string user_login { get; set; }
        public string user_name { get; set; }
        public string user_passwd { get; set; }
        public sbyte user_access { get; set; }
        public sbyte passwd_temp { get; set; }
        public string reset_key { get; set; }
        public Nullable<int> reset_time { get; set; }
        public string user_email { get; set; }
        public string user_language { get; set; }
    
        public virtual ICollection<alerts> alerts { get; set; }
        public virtual ICollection<email_inboxes> email_inboxes { get; set; }
        public virtual ICollection<issue_states> issue_states { get; set; }
        public virtual ICollection<log_events> log_events { get; set; }
        public virtual ICollection<preferences> preferences { get; set; }
        public virtual ICollection<rights> rights { get; set; }
        public virtual ICollection<sessions> sessions { get; set; }
        public virtual ICollection<stamps> stamps { get; set; }
        public virtual ICollection<subscriptions> subscriptions { get; set; }
        public virtual ICollection<view_preferences> view_preferences { get; set; }
        public virtual ICollection<views> views { get; set; }
    }
}
