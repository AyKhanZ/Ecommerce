using Microsoft.AspNetCore.Identity;

namespace ASP_Project.Areas.Identity.Data.Models
{
    public static class RoleStorage
    {
        public static List<IdentityRole> Roles { get; private set; }

        static RoleStorage()
        {
            Roles = new List<IdentityRole>()
        {
            new IdentityRole("Admin"),
            new IdentityRole("User")
        };
        }
    }
}
