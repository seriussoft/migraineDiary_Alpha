using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MigraineDiaryMVC.Helpers;
using System.Diagnostics;

namespace MigraineDiaryMVC.Models
{
	//public class RoleModel
	//{
		

		
	//}

	public sealed class RoleTranslator
	{
		private EnumDescription<Roles> RolesToDescriptionManager { get; set; }
		public static Lazy<RoleTranslator> Instance { get; private set; } //do not ever update this guy!!!

		private RoleTranslator()
		{
			this.RolesToDescriptionManager = new EnumDescription<Roles>();
		}

		public string GetDescription(Roles roleValue)
		{
			var dictionary = this.RolesToDescriptionManager.EnumToDescription;

			Debug.Assert(dictionary.ContainsKey(roleValue));

			return dictionary[roleValue];
		}

		public Roles GetRole(string roleDescription)
		{
			var dictionary = this.RolesToDescriptionManager.DescriptionToEnum;

			Debug.Assert(dictionary.ContainsKey(roleDescription));

			return dictionary[roleDescription];
		}
	}

	public enum Roles
	{
		[Description(RoleConstants.Admin)] Admin,
		[Description(RoleConstants.SuperUser)] SuperUser,
		[Description(RoleConstants.Tester)] Tester,
		[Description(RoleConstants.Customer)] Customer,
		[Description(RoleConstants.ProspectiveCustomer)] ProspectiveCustomer,
		[Description(RoleConstants.BannedUser)] BannedUser,
	}

	public class RoleConstants
	{
		public const string Admin = "Admin";
		public const string SuperUser = "SuperUser";
		public const string Tester = "Tester";
		public const string Customer = "Customer";
		public const string ProspectiveCustomer = "ProspectiveCustomer";
		public const string BannedUser = "BannedUser";
	}
}