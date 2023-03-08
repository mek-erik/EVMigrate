using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EVMigrate
{
    internal class MantisRepo
    {
        
        private bugtrackerEntities mantisContext;
        private EventumRepo eventumRepo = new EventumRepo();
        public MantisRepo() 
        
        {
            mantisContext = new bugtrackerEntities();
            ClearData();
        }
        private void ClearData()
        {
            mantisContext.Database.ExecuteSqlCommand("DELETE  FROM mt_bug_text");
            mantisContext.Database.ExecuteSqlCommand("DELETE  FROM mt_bug");
            mantisContext.Database.ExecuteSqlCommand("DELETE  FROM mt_bug_history");
            mantisContext.Database.ExecuteSqlCommand("DELETE  FROM mt_bug_monitor");


            //mantisContext.Database.ExecuteSqlCommand("DELETE  FROM mt_bug_monitor");
            mantisContext.Database.ExecuteSqlCommand("DELETE  FROM mt_bugnote_text");
            mantisContext.Database.ExecuteSqlCommand("DELETE  FROM mt_bugnote");
            mantisContext.Database.ExecuteSqlCommand("DELETE  FROM mt_bug_file");
            mantisContext.Database.ExecuteSqlCommand("ALTER TABLE mt_bug_text AUTO_INCREMENT = 1");
            mantisContext.Database.ExecuteSqlCommand("ALTER TABLE mt_bug AUTO_INCREMENT = 1");
            mantisContext.Database.ExecuteSqlCommand("ALTER TABLE mt_bug_history AUTO_INCREMENT = 1");
            mantisContext.Database.ExecuteSqlCommand("ALTER TABLE mt_bug_monitor AUTO_INCREMENT = 1");
            mantisContext.Database.ExecuteSqlCommand("ALTER TABLE mt_bugnote_text AUTO_INCREMENT = 1");
            mantisContext.Database.ExecuteSqlCommand("ALTER TABLE mt_bugnote AUTO_INCREMENT = 1");
            mantisContext.Database.ExecuteSqlCommand("ALTER TABLE mt_bug_file AUTO_INCREMENT = 1");
            mantisContext.SaveChanges();
        }
        private long GetUserId(string userName)
        {
           return mantisContext.mt_user.Where(mtu => mtu.realname == userName).First().id;
        }
        private short GetPriority(string title)
        {
            switch (title)
            {
                case "Critical":
                    return 50;
                 case "High":
                    return 40;
                 case "Medium":
                    return 30;
                 case "Low":
                    return 20;
                 case "Not Prioritized":
                    return 10;
                default:
                    return 30;
            }
        }
        private short GetSeverity(string priority )
        {
            //switch (priority)
            //{
            //    case "Critical":
            //        return 50;
            //    case "High":
            //        return 40;
            //    case "Medium":
            //        return 30;
            //    case "Low":
            //        return 20;
            //    case "Not Prioritized":
            //        return 10;
            //    default:
            //        return 50;
            //}
            return 50;
        }
        private long GetProjectId(long issueId)
        {
            string projectName = "22X";
            List<string> applications = eventumRepo.GetCustomFieldValues(issueId, "Application");
            if (applications.Count == 1)
            {
                projectName = applications[0];
            }
            else
            {
                if (applications.Contains("22X SpiderEye"))
                {
                    projectName = "22X SpiderEye";
                }
                else if (applications.Contains("DB Manager"))
                {
                    projectName = "DB Manager";
                }
                else if (applications.Contains("DB22X"))
                {
                    projectName = "DB22X";
                }
                else if (applications.Contains("FileConverter"))
                {
                    projectName = "22X";
                }
                else if (applications.Contains("CsCenter") || applications.Contains("CsAnalyzer") || applications.Contains("CsWatch") || applications.Contains("CsRepair"))
                {
                    projectName = "Catch";
                }
                else
                {
                    projectName = "22X";
                }

                
            }
            long projectId = 1;
            if (projectName != "")
            {
                try
                {
                    projectId = mantisContext.mt_project.Where(prj => prj.name == projectName).FirstOrDefault().id;
                }
                catch (Exception)
                {
                    Console.WriteLine("JE OMA");

                }
            }
            
            
            return projectId;
        }
        private long GetStatusId()
        {
            return 0;
        }
        private short GetResolutionId(long issueId)
        {
            long eventumResId = eventumRepo.GetResolutionId(issueId);

            switch (eventumResId)
            {
                case 1: return 60;
                case 2: return 20;
                case 3: return 70;
                case 4: return 50;
                case 5: return 10;
                case 6: return 30;
                case 7: return 80;
                case 8: return 40;
                case 9: return 90;
                default:
                    return 10;
                    break;
            }
        }
        private short GetCategoryId(long issueId)
        {
            long eventumCategoryId = eventumRepo.GetCategoryId(issueId);
            switch (eventumCategoryId)
            {
                case 7: // Bug
                    return 4;
                case 8: // Feature request
                    return 5;
                case 9: // Tech sup
                    return 7;
                case 13: // Feature change
                    return 6;
                default:
                    return 1;
                    
            }
        }
        private short GetStatusId(long issueId)
        {
            long statusId = eventumRepo.GetStatusId(issueId);
            switch (statusId)
            {
                case 1:
                return 10;
                case 2:
                return 30;
                case 3:
                return 50;
                case 4:
                return 20;
                case 5:
                return 90;
                case 6:
                    return 90;
                default:
                    return 10;
            }
        }
        public void AddBug(eventum_issue issue)
        {
            
            mt_bug bug = new mt_bug()
            {
                project_id = GetProjectId(issue.iss_id),
                //id = issue.iss_id,
                reporter_id = GetUserId(eventumRepo.GetUserName(issue.iss_usr_id)),
                
                duplicate_id = 0,
                priority = GetPriority(eventumRepo.GetPriorityRank(issue.iss_id)),
                severity = GetSeverity(eventumRepo.GetPriorityRank(issue.iss_id)),
                reproducibility = 10, // TODO: Default value?
                projection = 50, // Not needed!
                category_id = GetCategoryId(issue.iss_id),
                resolution = GetResolutionId(issue.iss_id),
                status = GetStatusId(issue.iss_id), // TODO
                summary = issue.iss_summary,
                date_submitted = ((DateTimeOffset)issue.iss_created_date).ToUnixTimeSeconds(),
                last_updated = ((DateTimeOffset)issue.iss_updated_date).ToUnixTimeSeconds(),
                eta = 10,
                os = "",
                os_build = "",
                platform = "",
                fixed_in_version = "",
                build = "",
                target_version = "",
                version = "",
                view_state = 10,
                sponsorship_total = 0,
                bug_text_id = issue.iss_id,
                due_date = 1

            };
            try
            {
                bug.handler_id = GetUserId(eventumRepo.GetUserName(eventumRepo.GetAssignedUserId(issue.iss_id)));
            }
            catch (Exception)
            {
                //bug.handler_id = null;
            }
           
            mt_bug_text bugtext = new mt_bug_text()
            {
                //id = issue.iss_id,
                description = MarkDownText(issue.iss_description),
                steps_to_reproduce = "",
                additional_information = "Migrated from eventum #" + issue.iss_id
            };
            mantisContext.mt_bug_text.Add(bugtext);
            mantisContext.SaveChanges();
            bug.bug_text_id = bugtext.id;
            mantisContext.mt_bug.Add(bug);
            mantisContext.SaveChanges();
            mt_bug_history bug_History_Status = new mt_bug_history()
            {
                user_id = 1,
                bug_id = bug.id,
                date_modified = ((DateTimeOffset)issue.iss_created_date).ToUnixTimeSeconds(), // TODO: Change to update date
                field_name = "status",
                old_value = "10",
                new_value = "50",
                type = 0

            };
            mt_bug_history bug_History_Handler = new mt_bug_history()
            {
                user_id = 1,
                bug_id = bug.id,
                date_modified = ((DateTimeOffset)issue.iss_created_date).ToUnixTimeSeconds(), // TODO: Change to update date
                field_name = "handler_id",
                old_value = "0",
                new_value = "1",
                type = 0

            };
            AddComments(issue.iss_id);
        }

        private void AddComments(long issueId)
        {
            var evcomments = eventumRepo.GetComments(issueId);
            foreach (var item in evcomments)
            {

                mt_bugnote_text commentText = new mt_bugnote_text()
                {
                    note = item.not_note
                };
                mantisContext.mt_bugnote_text.Add(commentText);
                mantisContext.SaveChanges();
                mt_bugnote comment = new mt_bugnote()
                {
                    bug_id = item.not_iss_id,
                    reporter_id = GetUserId(eventumRepo.GetUserName(item.not_usr_id)),
                    note_type = 0,
                    view_state = 10,
                    last_modified = ((DateTimeOffset)item.not_created_date).ToUnixTimeSeconds(),
                    date_submitted = ((DateTimeOffset)item.not_created_date).ToUnixTimeSeconds(),
                    bugnote_text_id = commentText.id
                };
                mantisContext.mt_bugnote.Add(comment);
                mantisContext.SaveChanges();
                //        if (item.not_usr_id > 3)
                //        {
                //            string username = evusers.First(user => item.not_usr_id == user.usr_id).usr_full_name;
                //            repo.AddComment(item.not_note, username, item.not_created_date);
                //        }
                //Console.WriteLine("Writing Files...");

            }
        }

        private static string MarkDownText(string text)
        {
            string result = "";
            text.Split(new string[] { Environment.NewLine },
    StringSplitOptions.None).ToList().ForEach(line =>
    {
        if (line.StartsWith("Problem:") || line.StartsWith("Cause:") || line.StartsWith("Solution:") /*&& (!line.Contains(" ") && line.Trim().EndsWith(":")) && line.Length < 15*/)
        {
            line = "## " + line;
        }

        result += line + Environment.NewLine;

    });

            return result;
        }
        public void MakeEmptyBug()
        {
            Console.WriteLine("Empty");
            mt_bug_text bugtext = new mt_bug_text()
            {
                //id = issue.iss_id,
                description = "DELETE ME",
                steps_to_reproduce = "",
                additional_information = "DELETE ME"
            };

            mt_bug bug = new mt_bug()
            {
                project_id = 1,
                //id = issue.iss_id,
                reporter_id = 1,
                handler_id = 1,
                duplicate_id = 0,
                priority = 30,
                severity = 70,
                reproducibility = 70,
                projection = 50,
                category_id = 1,

                status = 50,
                summary = "DELETE ME",
                date_submitted = 0,
                last_updated = 0,
                eta = 10,
                os = "",
                os_build = "",
                platform = "",
                fixed_in_version = "",
                build = "",
                target_version = "",
                version = "",
                view_state = 10,
                sponsorship_total = 0,
                due_date = 1

            };
            mantisContext.mt_bug_text.Add(bugtext);
            mantisContext.SaveChanges();
            bug.bug_text_id = bugtext.id;
            mantisContext.mt_bug.Add(bug);
            mantisContext.SaveChanges();
        }
        
    }
}
