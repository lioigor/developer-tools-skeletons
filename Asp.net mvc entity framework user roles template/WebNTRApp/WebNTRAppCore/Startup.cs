using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using WebNTRAppCore.Models;
using Owin;
using System.Security.Claims;

[assembly: OwinStartupAttribute(typeof(WebNTRAppCore.Startup))]
namespace WebNTRAppCore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
			createRolesandUsers();
		}


		// In this method we will create default User roles and Admin user for login
		private void createRolesandUsers()
		{
			ApplicationDbContext context = new ApplicationDbContext();

			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
			var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


			// In Startup iam creating first Admin Role and creating a default Admin User 
			if (!roleManager.RoleExists("Admin"))
			{

				// first we create Admin role
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Admin";
				roleManager.Create(role);

				//Here we create a Admin super user who will maintain the website				

				var user = new ApplicationUser();
				user.UserName = "superuser";
				user.Email = "sysadmin@gmail.com";

				string userPWD = "Q@22kateS";

				var chkUser = UserManager.Create(user, userPWD);

				//Add default User to Role Admin
				if (chkUser.Succeeded)
				{
					var result1 = UserManager.AddToRole(user.Id, "Admin");

				}
			}

			// creating maintaner role 
			if (!roleManager.RoleExists("Maintaner"))
			{
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Maintaner";
				roleManager.Create(role);

			}

			// creating translator role 
			if (!roleManager.RoleExists("Translator"))
			{
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Translator";
				roleManager.Create(role);

			}

            // creating developer role 
            if (!roleManager.RoleExists("Developer"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Developer";
                roleManager.Create(role);

            }
		}
	}
}
