using Microsoft.AspNetCore.Identity;

namespace Project_ZF.Models
{
    public class ApplicationRole : IdentityRole <int>
    {

        public string Description { get; set; }
    }
}
