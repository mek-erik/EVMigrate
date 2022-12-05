using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace EVMigrate
{
    class Program
    {
        static void Main(string[] args)
        {
            int stamp;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WindowWidth = 100;
            using (var eventumContext = new u49299178_Entities1())
            {
                using (var webIssuesContext = new webissuesEntities())
                {
                    webIssuesContext.Database.ExecuteSqlCommand("DELETE  FROM attr_values");
                    webIssuesContext.Database.ExecuteSqlCommand("DELETE  FROM issues");
                    webIssuesContext.Database.ExecuteSqlCommand("DELETE  FROM stamps");
                    webIssuesContext.Database.ExecuteSqlCommand("DELETE  FROM issue_states");
                    webIssuesContext.Database.ExecuteSqlCommand("ALTER TABLE attr_values AUTO_INCREMENT = 1");
                    webIssuesContext.Database.ExecuteSqlCommand("ALTER TABLE issues AUTO_INCREMENT = 1");
                    webIssuesContext.Database.ExecuteSqlCommand("ALTER TABLE stamps AUTO_INCREMENT = 1");
                    webIssuesContext.Database.ExecuteSqlCommand("ALTER TABLE issue_states AUTO_INCREMENT = 1");
                    webIssuesContext.SaveChanges();



                    //WebIssueRepo repo = new WebIssueRepo(webIssuesContext);
                    //repo.AddIssue(1, "Generated Test Issue");
                    //repo.AddDescription("Description");
                    List<eventum_issue> issues = eventumContext.eventum_issue.Where(issue => issue.iss_prj_id == 2).ToList();
                    List<eventum_custom_field> custFields = eventumContext.eventum_custom_field.ToList();
                    List<eventum_custom_field_option> custOptions = eventumContext.eventum_custom_field_option.ToList();

                    List<eventum_custom_field_option> issueTypes = eventumContext.eventum_custom_field_option.Where(field => field.cfo_fld_id == 1).ToList();
                    List<eventum_custom_field_option> salesEffects = eventumContext.eventum_custom_field_option.Where(field => field.cfo_fld_id == 2).ToList();
                    List<eventum_issue_custom_field> eventum_Issue_Custom_Fields = eventumContext.eventum_issue_custom_field.ToList();
                    int key = 0;
                    foreach (eventum_issue issue in issues)
                    { 
                        key++;
                        Console.WriteLine(new String('-', 100));
                        WebIssueRepo repo = new WebIssueRepo(webIssuesContext);

                        repo.AddIssue(EventumConverter.Category(Convert.ToInt32(issue.iss_prc_id)), issue.iss_summary, eventumContext.eventum_user.First(users => users.usr_id == issue.iss_usr_id).usr_full_name, issue.iss_created_date);
                        repo.AddDescription(issue.iss_description);
                        
                        Console.WriteLine(issue.iss_id);
                        Console.WriteLine(issue.iss_summary);

                        foreach (var item in eventum_Issue_Custom_Fields.Where(item => item.icf_iss_id == issue.iss_id && item.icf_value != null && item.icf_value != ""))
                        {
                            //    eventum_custom_field_option test = custOptions.Find(option => option.cfo_id.ToString() == item.icf_value);
                            Console.WriteLine(custFields.Find(field => field.fld_id == item.icf_fld_id).fld_title); //+ " - " + custOptions.Find(option => option.cfo_id.ToString() == item.icf_value).cfo_value);
                            string value;
                            if (custFields.Find(field => field.fld_id == item.icf_fld_id).fld_type != "text"  )
                            {
                                value = custOptions.Find(option => option.cfo_id.ToString() == item.icf_value).cfo_value;
                            }
                            else 
                            {
                                value = item.icf_value;
                            }
                            repo.AddAttribute(custFields.Find(field => field.fld_id == item.icf_fld_id).fld_title, value);

                        }
                        foreach (var item in eventumContext.eventum_note.Where(note => note.not_iss_id == issue.iss_id))
                        {
                            repo.AddComment(item.not_full_message, "Erik van Reusel" , item.not_created_date);
                        }
                        // Add Priority
                        string priority = "Medium";
                        if (issue.iss_pri_id > 0) priority = eventumContext.eventum_project_priority.FirstOrDefault(pri => pri.pri_id == issue.iss_pri_id && issue.iss_prj_id == pri.pri_prj_id).pri_title;

                        repo.AddAttribute("Priority", priority);
                        Console.WriteLine(new string('_', 50) + " " + (int)((double)key / issues.Count() * 100));
                        Console.Title =  ((int)((double)key / issues.Count() * 100)).ToString();
                        Console.Clear();
                    }
                    Console.WriteLine(issues.Count);
                    
                }
                
            }
            Console.ReadLine();

        }


    }
    class EventumConverter
    {
        public static int Category(int evCategory)
            {
            switch (evCategory)
            {
                case 7:
                    return 1;
               case 13:
                    return 2;
               case 8:
                    return 3;
               case 9:
                    return 4;

                default:
                    return 1;
            }
        }
    }
  class WebIssueRepo
    {
        private issues issue = new issues();
        private webissuesEntities webIssuesContext;
        int unixStamp;
        protected string createdBy;

        public WebIssueRepo(webissuesEntities webIssuesContext) 
        {
        this.webIssuesContext = webIssuesContext;

        }
        public void AddComment(string comment, string username, DateTime time)
        {
            int unixStamp = Convert.ToInt32(new DateTimeOffset(time).ToUnixTimeSeconds());
            int userId = webIssuesContext.users.First(user => user.user_name == username).user_id;
           
            int id = AddStamp(unixStamp, username);
            comments comm = new comments() { comment_text = comment, comment_id = id, comment_format = 1 };
            issue.issue_states.Add(new issue_states() { read_id = id, user_id = userId });
            webIssuesContext.SaveChanges();
        }
        private int AddStamp(int timeStamp, string username)
        {
            stamps stamp = new stamps();

            var webIssueUser = webIssuesContext.users.ToList();
            if (webIssuesContext.users.Count(user => user.user_name == username) == 0)
            {
                if (username == "EventumMailer")
                {
                    stamp.user_id = 2;
                }
               
            }
            else
            {
                stamp.user_id = webIssuesContext.users.First(user => user.user_name == username).user_id;
            }
            
            stamp.stamp_time = timeStamp;
            webIssuesContext.stamps.Add(stamp);
            webIssuesContext.SaveChanges();
            return stamp.stamp_id;
        }
        public void AddIssue(int folderId, string title, string createdBy, DateTime dtCreated)
        {
            unixStamp = Convert.ToInt32(new DateTimeOffset(dtCreated).ToUnixTimeSeconds());
            int id = AddStamp(unixStamp, createdBy);
            this.createdBy = createdBy;
            issue = new issues();
            issue.issue_id = id;
            issue.folder_id = folderId;
            issue.issue_name = title;
            issue.stamp_id = id;
            webIssuesContext.issues.Add(issue);
            webIssuesContext.SaveChanges();
        }

        public void AddDescription(string text, int format = 1)
        {

            int id = AddStamp(unixStamp, createdBy);


            issue_descriptions description = new issue_descriptions();
            //description.issue_id = issue.issue_id;
            description.descr_text = text;
            description.descr_format = 1;
            issue.issue_descriptions = description;
            issue.descr_id= id;
            webIssuesContext.SaveChanges();

        }
        public void AddAttribute(string name, string value)
        {
            attr_types attribute = webIssuesContext.attr_types.First(att => att.attr_name.ToLower() == name.ToLower() && att.type_id == issue.folder_id);
            if (name == "Priority")
            {
                int id = AddStamp(unixStamp, createdBy);
                attr_values issueAttribute = new attr_values();
                issueAttribute.attr_id = attribute.attr_id;
                issueAttribute.attr_value = value;
                issue.attr_values.Add(issueAttribute);
                webIssuesContext.SaveChanges();
            }
            else
            {
                if (issue.attr_values.Count(att => att.attr_id == attribute.attr_id) == 0)
                {

                    int id = AddStamp(unixStamp, createdBy);
                    attr_values issueAttribute = new attr_values();
                    issueAttribute.attr_id = attribute.attr_id;
                    issueAttribute.attr_value = value;
                    issue.attr_values.Add(issueAttribute);
                    webIssuesContext.SaveChanges();

                }
                else
                {
                    Console.WriteLine("More than one attribute, skipping.");
                }
            }


        }
    }
}
