using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVMigrate
{
    internal class EventumRepo
    {
        private u49299178_Entities2 eventumContext;
        public EventumRepo()
        {
            eventumContext = new u49299178_Entities2();

        }
        public string GetUserName(long id)
        {
            return eventumContext.eventum_user.First(users => users.usr_id == id).usr_full_name;
        }
        /// <summary>
        /// Gets the user id of the last assigned user to an issue.
        /// </summary>
        /// <param name="issueId"></param>
        /// <returns>0 if no user was assigned</returns>
        public long GetAssignedUserId(long issueId)
        {
            
            var rows = eventumContext.eventum_issue_user.Where(isu => isu.isu_iss_id == issueId).OrderByDescending(isu => isu.isu_assigned_date);
            if (rows.Count() > 0)
            {
                return rows.First().isu_usr_id;
            }
            else
            {
                return 0;
            }
        }
        public string GetPriorityRank(long issueId)
        {
            long prjId = GetProject(issueId);
            long prioId = GetPrioId(issueId);
            string rank = "Medium";
            if (prioId > -1)
            {
                
            try
            {

            rank = eventumContext.eventum_project_priority.Find(prioId).pri_title;
            }
            catch (Exception)
            {

                rank = "Medium";
            }
            }
            return rank;
        }
        private long GetProject(long issueId)
        {
            return eventumContext.eventum_issue.Find(issueId).iss_prj_id;
        }
        private long GetPrioId(long issueId)
        {
            return  eventumContext.eventum_issue.Find(issueId).iss_pri_id;
        }
        public List<string> GetCustomFieldValues(long issue_id, string field_name)
        {
            eventum_issue issue = eventumContext.eventum_issue.Find(issue_id);
            //List<eventum_project_custom_field> fields =  eventumContext.eventum_issue_custom_field.Where(icf => icf.icf_iss_id == issue_id) 
            //    eventumContext.eventum_project_custom_field
            //    .Where(pf => pf.pcf_prj_id == issue.iss_prj_id).Select(pf=> pf.pcf_fld_id);

                //.Select(pf => eventumContext.eventum_custom_field.Find(pf.pcf_fld_id)).ToList();
            
                
            //eventum_issue_custom_field queryField = eventumContext.eventum_custom_field.Where(customField => customField.fld_title == field_name );
            var fieldRows = eventumContext.eventum_custom_field.Where(field => field.fld_title == field_name);

            if (fieldRows == null) return null;

            List<string> unParsedValues = eventumContext.eventum_issue_custom_field.Where(icf => icf.icf_iss_id == issue.iss_id && fieldRows.Select(flH => flH.fld_id).Contains(icf.icf_fld_id)).Select(icf => icf.icf_value).ToList();
            long parsedId = 0;
            if (unParsedValues.Count == 0) return new List<string>();
            if (long.TryParse(unParsedValues.First(), out parsedId))
            {
                List<long> parsedValues = unParsedValues.Select(unparsed => long.Parse(unparsed)).ToList();
                return eventumContext.eventum_custom_field_option.Where(cfo => parsedValues.Contains(cfo.cfo_id)).Select(cfo => cfo.cfo_value).ToList();
                //return eventumContext.eventum_custom_field_option.Find(parsedId).cfo_value;
            }
            else
            {
                return unParsedValues;
            }
            
        }
        public long GetStatusId(long issue_id)
        {
            return eventumContext.eventum_issue.Find(issue_id).iss_sta_id;
        }
        public long GetResolutionId(long issue_id)
        {
            long id = 5;
            try
            {
                id = eventumContext.eventum_issue.Find(issue_id).iss_res_id.Value;
            }
            catch (Exception)
            {

                
            }
            
            return id;
        }
        public long GetCategoryId(long issue_id)
        {
            return eventumContext.eventum_issue.Find(issue_id).iss_prc_id;
        }
        public List<eventum_note> GetComments(long issue_id)
        {
            return eventumContext.eventum_note.Where(note => note.not_iss_id == issue_id && note.not_usr_id != 3).ToList();
        }
    }
}
