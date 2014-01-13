#define BYPASS_DEBUG

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using MigraineDiaryMVC.Models;

namespace MigraineDiaryMVC.Migrations.Accounts
{
	internal sealed class AccountMigrationConfiguration : DbMigrationsConfiguration<ApplicationDbContext>
	{
		public AccountMigrationConfiguration()
		{
			AutomaticMigrationsEnabled = true;
			//AutomaticMigrationDataLossAllowed = false;
			AutomaticMigrationDataLossAllowed = true;
		}

		protected override void Seed(ApplicationDbContext context)
		{
			CallReleaseSeed(context);

#if DEBUG || BYPASS_DEBUG
			CallDebugSeed(context);
#endif

			base.Seed(context);
		}

		private void CallReleaseSeed(ApplicationDbContext context)
		{
			
		}

		private void CallDebugSeed(ApplicationDbContext context)
		{
			
		}

		
	}
}