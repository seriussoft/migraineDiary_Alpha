using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriusSoft.MigraineDiaryMVC.MigraineRepository.Repositories
{
	public class TriggerRepository : BaseDisposableRepository, Interfaces.ITriggerRepository
	{
		public TriggerRepository(MigraineDiaryMVC_DBContext context, bool saveOnChange = true) : base(context, saveOnChange) { }

		#region ITriggerRepository Members

		#region Get

		public IEnumerable<MigraineModels.TriggerModel> GetTriggers()
		{
			return this.Context.Triggers.Include(t => t.TriggerType);
		}

		public IEnumerable<MigraineModels.TriggerModel> GetTriggersMinimal()
		{
			return this.Context.Triggers.ToList();
		}

		public MigraineModels.TriggerModel GetTriggerByID(int id)
		{
			return this.Context.Triggers.Find(id);
		}

		public MigraineModels.TriggerModel GetTriggerMinimalByID(int id)
		{
			return this.Context.Triggers.Find(id);
		} 

		#endregion	Get

		#region Create

		public MigraineModels.TriggerModel CreateTrigger(MigraineModels.TriggerModel trigger)
		{
			var returnTrigger = this.Context.Triggers.Add(trigger);
			this.Save(this.SaveOnChange);

			return returnTrigger;
		}

		public bool CreateTriggerIfValid(MigraineModels.TriggerModel trigger, bool isValid)
		{
			if (isValid)
				this.CreateTrigger(trigger);

			return isValid;
		}

		#endregion	Create

		#region Update

		public void UpdateTrigger(MigraineModels.TriggerModel trigger)
		{
			this.Context.Entry(trigger).State = EntityState.Modified;
			this.Save(this.SaveOnChange);
		}

		public bool UpdateTriggerIfValid(MigraineModels.TriggerModel trigger, bool isValid)
		{
			if (isValid)
				this.UpdateTrigger(trigger);

			return isValid;
		}

		#endregion	Update

		#region Delete

		public void DeleteTrigger(int id)
		{
			var trigger = this.Context.Triggers.Find(id);
			this.DeleteTrigger(trigger);
		}

		public void DeleteTrigger(MigraineModels.TriggerModel Trigger)
		{
			this.Context.Triggers.Remove(Trigger);
			this.Save(this.SaveOnChange);
		}

		#endregion	Delete

		#endregion	ITriggerRepository Members
	}
}
