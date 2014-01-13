using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MigraineDiaryMVC.Models
{
	public class MigraineDiaryMVC_DBContext : DbContext
	{
		public DbSet<MigraineModel> Migraines { get; set; }
		public DbSet<MedicationModel> Medications { get; set; }
		public DbSet<IngredientModel> Ingredients { get; set; }
		public DbSet<DosageModel> Dosage { get; set; }
		public DbSet<UnitOfMeasurementModel> UnitsOfMeasurement { get; set; }
		public DbSet<SymptomModel> Symptoms { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

			modelBuilder.Entity<MedicationModel>()
				.HasMany(m => m.Ingredients)
				.WithMany(i => i.Medications)
				.Map(m =>
				{
					m.ToTable("MedicationModelsToIngredientModels");
					m.MapLeftKey("MedicationID");
					m.MapRightKey("IngredientID");
				});

			

			base.OnModelCreating(modelBuilder);
		}
	}
}