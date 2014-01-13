//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using IdentityRoleManager = Microsoft.AspNet.Identity.RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>;
using IdentityRoleStore = Microsoft.AspNet.Identity.EntityFramework.RoleStore<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace MigraineDiaryMVC.Models
{
	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	[Table("ApplicationUser", Schema = "nvanbkirk_migrainediary_alpha")]
	public class ApplicationUser : IdentityUser
	{
		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email address")]
		public string EmailAddress { get; set; }

		//public string RemoveMeLater { get; set; }
	}

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("DefaultConnection")
		{
		}
	}

	public interface IIdentityManager<TUserType> where TUserType : IdentityUser, IUser
	{
		bool RolesExist(string roleName);
		bool CreateRole(string roleName);
		bool AddUserToRole(string userID, string roleName);
		void ClearUserRoles(string userID);
	}

	public class IdentityManager<TUserType> : IIdentityManager<TUserType> where TUserType : IdentityUser, IUser
	{
		private IdentityRoleManager RoleManager { get; set; }
		private UserManager<TUserType> UserManager { get; set; }

		public IdentityManager(UserManager<TUserType> userManager = null, IdentityRoleManager roleManager = null)
		{
			this.UserManager = userManager ?? CreateUserManager();
			this.RoleManager = roleManager ?? CreateRoleManager();
		}

		public bool RolesExist(string roleName)
		{
			return this.RoleManager.RoleExists(roleName);
		}

		public bool CreateRole(string roleName)
		{
			var addResult = this.RoleManager.Create(new IdentityRole(roleName));
			return addResult.Succeeded;
		}

		public bool AddUserToRole(string userID, string roleName)
		{
			if (!RolesExist(roleName))
				CreateRole(roleName);

			var addResult = this.UserManager.AddToRole(userID, roleName);
			return addResult.Succeeded;
			//can add checks later for logging/displaying errors if they occurr
		}

		public void ClearUserRoles(string userID)
		{
			var user = this.UserManager.FindById(userID);
			var currentRoles = new List<IdentityUserRole>(user.Roles);

			foreach (var role in currentRoles)
				this.UserManager.RemoveFromRole(userID, role.Role.Name);
		}

		private IdentityRoleManager CreateRoleManager()
		{
			var roleManager = new IdentityRoleManager(new IdentityRoleStore(new ApplicationDbContext()));
			return roleManager;
		}

		private UserManager<TUserType> CreateUserManager()
		{
			return new UserManager<TUserType>(new UserStore<TUserType>(new ApplicationDbContext()));
		}
	}
}