﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class bugtrackerEntities : DbContext
    {
        public bugtrackerEntities()
            : base("name=bugtrackerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<mt_api_token> mt_api_token { get; set; }
        public DbSet<mt_bug> mt_bug { get; set; }
        public DbSet<mt_bug_file> mt_bug_file { get; set; }
        public DbSet<mt_bug_history> mt_bug_history { get; set; }
        public DbSet<mt_bug_monitor> mt_bug_monitor { get; set; }
        public DbSet<mt_bug_relationship> mt_bug_relationship { get; set; }
        public DbSet<mt_bug_revision> mt_bug_revision { get; set; }
        public DbSet<mt_bug_tag> mt_bug_tag { get; set; }
        public DbSet<mt_bug_text> mt_bug_text { get; set; }
        public DbSet<mt_bugnote> mt_bugnote { get; set; }
        public DbSet<mt_bugnote_text> mt_bugnote_text { get; set; }
        public DbSet<mt_category> mt_category { get; set; }
        public DbSet<mt_config> mt_config { get; set; }
        public DbSet<mt_custom_field> mt_custom_field { get; set; }
        public DbSet<mt_custom_field_project> mt_custom_field_project { get; set; }
        public DbSet<mt_custom_field_string> mt_custom_field_string { get; set; }
        public DbSet<mt_email> mt_email { get; set; }
        public DbSet<mt_filters> mt_filters { get; set; }
        public DbSet<mt_news> mt_news { get; set; }
        public DbSet<mt_plugin> mt_plugin { get; set; }
        public DbSet<mt_project> mt_project { get; set; }
        public DbSet<mt_project_file> mt_project_file { get; set; }
        public DbSet<mt_project_user_list> mt_project_user_list { get; set; }
        public DbSet<mt_project_version> mt_project_version { get; set; }
        public DbSet<mt_sponsorship> mt_sponsorship { get; set; }
        public DbSet<mt_tag> mt_tag { get; set; }
        public DbSet<mt_tokens> mt_tokens { get; set; }
        public DbSet<mt_user> mt_user { get; set; }
        public DbSet<mt_user_pref> mt_user_pref { get; set; }
        public DbSet<mt_user_print_pref> mt_user_print_pref { get; set; }
        public DbSet<mt_user_profile> mt_user_profile { get; set; }
        public DbSet<mt_project_hierarchy> mt_project_hierarchy { get; set; }
    }
}