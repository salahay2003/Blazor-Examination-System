using Microsoft.AspNetCore.Identity;
using Blazor_Examination_System.Models;

namespace Blazor_Examination_System.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public ICollection<StudentExam> StudentExams { get; set; } = new List<StudentExam>();
    }

}
