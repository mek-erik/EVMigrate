using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.IO;

namespace EVMigrate
{
    class Program
    {
        static void Main(string[] args)
        {
            int stamp;
            MantisRepo mantisRepo = new MantisRepo();
            EventumRepo repo = new EventumRepo();
            Console.WriteLine(string.Join(",", repo.GetCustomFieldValues(180, "Objective") ));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WindowWidth = 100;
            using (var eventumContext = new u49299178_Entities2())
            {
                using (var mantisContext = new bugtrackerEntities())
                {


                    using (var webIssuesContext = new webissuesEntities())
                    {
                        //webIssuesContext.Database.ExecuteSqlCommand("DELETE  FROM attr_values");
                        //webIssuesContext.Database.ExecuteSqlCommand("DELETE  FROM issues");
                        //webIssuesContext.Database.ExecuteSqlCommand("DELETE  FROM stamps");
                        //webIssuesContext.Database.ExecuteSqlCommand("DELETE  FROM issue_states");
                        //webIssuesContext.Database.ExecuteSqlCommand("ALTER TABLE attr_values AUTO_INCREMENT = 1");
                        //webIssuesContext.Database.ExecuteSqlCommand("ALTER TABLE issues AUTO_INCREMENT = 1");
                        //webIssuesContext.Database.ExecuteSqlCommand("ALTER TABLE stamps AUTO_INCREMENT = 1");
                        //webIssuesContext.Database.ExecuteSqlCommand("ALTER TABLE issue_states AUTO_INCREMENT = 1");
                        //webIssuesContext.SaveChanges();

                        


                        //WebIssueRepo repo = new WebIssueRepo(webIssuesContext);
                        //repo.AddIssue(1, "Generated Test Issue");
                        //repo.AddDescription("Description");
                        List<eventum_issue> issues = eventumContext.eventum_issue.Where(issue => issue.iss_prj_id == 2).ToList();
                        List<eventum_custom_field> custFields = eventumContext.eventum_custom_field.ToList();
                        List<eventum_custom_field_option> custOptions = eventumContext.eventum_custom_field_option.ToList();
                        List<eventum_note> comments = eventumContext.eventum_note.ToList();
                        List<eventum_custom_field_option> issueTypes = eventumContext.eventum_custom_field_option.Where(field => field.cfo_fld_id == 1).ToList();
                        List<eventum_custom_field_option> salesEffects = eventumContext.eventum_custom_field_option.Where(field => field.cfo_fld_id == 2).ToList();
                        List<eventum_issue_custom_field> eventum_Issue_Custom_Fields = eventumContext.eventum_issue_custom_field.ToList();
                        List<eventum_issue_attachment_file> eventum_files = eventumContext.eventum_issue_attachment_file.ToList();
                        List<eventum_issue_attachment> eventum_issue_attachments = eventumContext.eventum_issue_attachment.ToList();
                        List<eventum_issue_user> eventum_issue_user = eventumContext.eventum_issue_user.ToList();
                        List<eventum_status> eventum_Issue_Statuses = eventumContext.eventum_status.ToList();
                        List<eventum_user> eventum_Users = eventumContext.eventum_user.ToList();
                        List<long> eventum_allstats = issues.Select(iss => iss.iss_sta_id).Distinct().ToList();

                        var evusers = eventumContext.eventum_user.ToList();
                        int key = 0;
                        for (int i = 1; i < issues.Last().iss_id; i++)
                        {
                            eventum_issue issue = issues.Find(iss => iss.iss_id == i);
                            if (issue != null)
                            {

                                mantisRepo.AddBug(issue);






                                //
                                List<eventum_issue_attachment> issue_Attachments = eventum_issue_attachments.Where(atch => atch.iat_iss_id == issue.iss_id).ToList();
                                foreach (var issue_attachment in issue_Attachments)
                                {
                                    string fileUuid = Guid.NewGuid().ToString("N");
                                    string usernameAttachment = eventum_Users.First(users => users.usr_id == issue_attachment.iat_usr_id).usr_full_name;
                                    long filerId = mantisContext.mt_user.Where(mtu => mtu.realname == usernameAttachment).First().id;
                                    List<eventum_issue_attachment_file> attachment_Files;

                                    

                                    mt_bugnote_text commentText = new mt_bugnote_text()
                                    {
                                        note = issue_attachment.iat_description

                                    };
                                    mantisContext.mt_bugnote_text.Add(commentText);
                                    mantisContext.SaveChanges();
                                    mt_bugnote fileNote = new mt_bugnote()
                                    {
                                        bug_id = issue_attachment.iat_iss_id,
                                        reporter_id = filerId,
                                        note_type = 0,
                                        view_state = 10,
                                        last_modified = ((DateTimeOffset)issue_attachment.iat_created_date).ToUnixTimeSeconds(),
                                        date_submitted = ((DateTimeOffset)issue_attachment.iat_created_date).ToUnixTimeSeconds(),
                                        bugnote_text_id = commentText.id
                                    };
                                    mantisContext.mt_bugnote.Add(fileNote);

                                    // Add custom fields
                                    var attributeGroup = eventum_Issue_Custom_Fields.Where(item => item.icf_iss_id == issue.iss_id && item.icf_value != null && item.icf_value != "").GroupBy(item => item.icf_fld_id);


                                    mantisContext.SaveChanges();
                                    attachment_Files = eventum_files.Where(f => f.iaf_iat_id == issue_attachment.iat_id).ToList();
                                    foreach (eventum_issue_attachment_file attachment_File in attachment_Files)
                                    {
                                        mt_bug_file file = new mt_bug_file()
                                        {
                                            diskfile = fileUuid,
                                            bug_id = issue_attachment.iat_iss_id,
                                            folder = @"\data",
                                            filename = attachment_File.iaf_filename,
                                            date_added = ((DateTimeOffset)issue_attachment.iat_created_date).ToUnixTimeSeconds(),
                                            filesize = Convert.ToInt32(attachment_File.iaf_filesize),
                                            file_type = attachment_File.iaf_filetype,
                                            title = "",
                                            description = "",
                                            bugnote_id = fileNote.id
                                            
                                        };
                                        
                                        mantisContext.mt_bug_file.Add(file);
                                        // Save File Locally
                                        File.WriteAllBytes(@".\data\" + fileUuid,attachment_File.iaf_file);
                                    }

                                    mantisContext.SaveChanges();
                                }


                                //mantisContext.mt_bug_history.Add(bug_History_Status);
                                //mantisContext.mt_bug_history.Add(bug_History_Handler);
                                mantisContext.SaveChanges();
                                Console.WriteLine("--------------------------------------------------------");
                                Console.WriteLine(issue.iss_id + " " + issue.iss_summary);
                            }

                            //mt_bugn
                            

                        
                            else
                            {
                                mantisRepo.MakeEmptyBug();
                                
                            }
                            Console.Title = ((int)((double)i / issues.Count() * 100)).ToString();


                        }

                        //foreach (eventum_issue issue in issues.Where(iss => iss.iss_id))
                        //{
                        //    key++;
                        // Mantis Stuff








                        //    // WebIssues stuff
                        //    WebIssueRepo repo = new WebIssueRepo(webIssuesContext);

                        //    repo.AddIssue(EventumConverter.Category(Convert.ToInt32(issue.iss_prc_id)), issue.iss_summary, eventumContext.eventum_user.First(users => users.usr_id == issue.iss_usr_id).usr_full_name, issue.iss_created_date);
                        //    repo.AddDescription(issue.iss_description);
                        //    repo.AddAttribute("Status", eventum_Issue_Statuses.Find(stat => stat.sta_id == issue.iss_sta_id).sta_title);
                        //    Console.Clear();
                        //    Console.Write("\r" + issue.iss_id);
                        //    Console.Write("\r" + issue.iss_summary);
                        //    //Get status
                        //    string status = eventum_Issue_Statuses.Find(st => st.sta_id == issue.iss_sta_id).sta_title;

                        //    var attributeGroup = eventum_Issue_Custom_Fields.Where(item => item.icf_iss_id == issue.iss_id && item.icf_value != null && item.icf_value != "").GroupBy(item => item.icf_fld_id);
                        //    foreach (var group in attributeGroup)
                        //    {
                        //        var item = group.FirstOrDefault();
                        //        //    eventum_custom_field_option test = custOptions.Find(option => option.cfo_id.ToString() == item.icf_value);
                        //        Console.Write("\r" + custFields.Find(field => field.fld_id == item.icf_fld_id).fld_title); //+ " - " + custOptions.Find(option => option.cfo_id.ToString() == item.icf_value).cfo_value);
                        //        string value;
                        //        if (custFields.Find(field => field.fld_id == item.icf_fld_id).fld_type != "text")
                        //        {
                        //            value = custOptions.Find(option => option.cfo_id.ToString() == group.First().icf_value).cfo_value;
                        //        }
                        //        else
                        //        {

                        //            value = group.Select(i => i.icf_value).First();
                        //            //value = string.Join(", ", group.SelectMany(i => i.icf_value).ToList());
                        //        }
                        //        repo.AddAttribute(custFields.Find(field => field.fld_id == item.icf_fld_id).fld_title, value);

                        //    }
                        //    foreach (var item in eventumContext.eventum_note.Where(note => note.not_iss_id == issue.iss_id))
                        //    {
                        //        if (item.not_usr_id > 3)
                        //        {
                        //            string username = evusers.First(user => item.not_usr_id == user.usr_id).usr_full_name;
                        //            repo.AddComment(item.not_note, username, item.not_created_date);
                        //        }

                        //    }
                        //    List<eventum_issue_user> assignedUsers = eventum_issue_user.Where(user => user.isu_iss_id == issue.iss_id).ToList();

                        //    // Add Priority
                        //    string priority = "Medium";
                        //    if (issue.iss_pri_id > 0) priority = eventumContext.eventum_project_priority.FirstOrDefault(pri => pri.pri_id == issue.iss_pri_id && issue.iss_prj_id == pri.pri_prj_id).pri_title;

                        //    repo.AddAttribute("Priority", priority);
                        //    Console.Write("\r" + new string('_', 50) + " " + (int)((double)key / issues.Count() * 100));

                        //    Console.Clear();
                        //}
                        Console.Write("\r" + issues.Count);

                    }
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
            if (webIssuesContext.users.Count(user => user.user_name == username) > 0  )
            {
                int unixStamp = Convert.ToInt32(new DateTimeOffset(time).ToUnixTimeSeconds());
                int userId = 4;


                userId = webIssuesContext.users.First(user => user.user_name == username).user_id;
                
                int id = AddStamp(unixStamp, username);

                webIssuesContext.changes.Add(new changes() { issue_id = issue.issue_id, change_type = 3, stamp_id = id, change_id = id });
                webIssuesContext.SaveChanges();

                comments comm = new comments() { comment_text = comment, comment_id = id, comment_format = 1 };
                webIssuesContext.comments.Add(comm);
                webIssuesContext.SaveChanges();


            }
            //issue.issue_states.Add(new issue_states() { read_id = id, user_id = userId });
            
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
