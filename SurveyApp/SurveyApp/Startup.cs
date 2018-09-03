using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SurveyApp.Models;

[assembly: OwinStartupAttribute(typeof(SurveyApp.Startup))]
namespace SurveyApp
{
    public partial class Startup
    {
        
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }

        //Function to create roles
        public void CreateRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //Creating superadmin in the startup phase

            if (!roleManager.RoleExists("SuperAdmin"))
            {
                //First create a role and then a user in that role

                //Creating SuperAdmin role

                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "SuperAdmin";
                roleManager.Create(role);


                //Creating a user for the SuperAdmin Role

                var user1 = new ApplicationUser();
                user1.UserName = "superadmin101@gmail.com";

                string userPWd = "SuperAdmin@123";

                var checkUser = UserManager.Create(user1, userPWd);


                if (checkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user1.Id, "SuperAdmin");

                }
            }
                // creating Creating Admin role     
                if (!roleManager.RoleExists("Admin"))
                {
                    var role2 = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    role2.Name = "Admin";
                    roleManager.Create(role2);

                }

                //Creating candidate role

                if (!roleManager.RoleExists("Candidate"))
                {
                    var role3 = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    role3.Name = "Candidate";
                    roleManager.Create(role3);

                }

        }
    }
}
