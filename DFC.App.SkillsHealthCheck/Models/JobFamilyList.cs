using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DFC.App.SkillsHealthCheck.Models
{
    public class JobFamilyList
    {
        // This property contains the available options
        public List<JobFamily> JobFamilies { get; set; }

        // This property contains the excluded job families
        [MaxLength(3, ErrorMessage = "Please select a maximum of 3 jobs")]
        public IEnumerable<string> SelectedJobs { get; set; }

        public JobFamilyList()
        {
            JobFamilies = new List<JobFamily>
            {
                new JobFamily { Id = "F10001", Name = "Administrative and Clerical" },
                new JobFamily { Id = "F10002", Name = "Alternative Therapies" },
                new JobFamily { Id = "F10003", Name = "Animals,Plants and Land" },
                new JobFamily { Id = "F10004", Name = "Arts, Crafts and Design" },
                new JobFamily { Id = "F10005", Name = "Catering Services" },
                new JobFamily { Id = "F10006", Name = "Construction" },
                new JobFamily { Id = "F10007", Name = "Education and Training" },
                new JobFamily { Id = "F10008", Name = "Environmental Sciences" },
                new JobFamily { Id = "F10009", Name = "Financial Services" },
                new JobFamily { Id = "F10010", Name = "General and Personal Services" },
                new JobFamily { Id = "F10011", Name = "Information Technology and Information Management" },
                new JobFamily { Id = "F10012", Name = "Legal Services" },
                new JobFamily { Id = "F10013", Name = "Maintenance, Service and Repair" },
                new JobFamily { Id = "F10014", Name = "Management and Planning" },
                new JobFamily { Id = "F10015", Name = "Manufacturing and Engineering" },
                new JobFamily { Id = "F10016", Name = "Marketing, Selling and Advertising" },
                new JobFamily { Id = "F10017", Name = "Medical Technology" },
                new JobFamily { Id = "F10018", Name = "Medicine and Nursing" },
                new JobFamily { Id = "F10019", Name = "Performing Arts, Broadcast and Media" },
                new JobFamily { Id = "F10020", Name = "Publishing and Journalism" },
                new JobFamily { Id = "F10021", Name = "Retail Sales and Customer Service" },
                new JobFamily { Id = "F10022", Name = "Science and Research" },
                new JobFamily { Id = "F10023", Name = "Security and Uniformed Services" },
                new JobFamily { Id = "F10024", Name = "Social Services" },
                new JobFamily { Id = "F10025", Name = "Sport, Leisure and Tourism" },
                new JobFamily { Id = "F10026", Name = "Storage, Dispatching and Delivery" },
                new JobFamily { Id = "F10027", Name = "Transport" },
            };

            SelectedJobs = new List<string>();
        }
    }
}
