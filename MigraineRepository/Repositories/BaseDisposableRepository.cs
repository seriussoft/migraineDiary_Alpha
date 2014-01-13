using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriusSoft.MigraineDiaryMVC.MigraineRepository.Repositories
{
	public abstract class BaseDisposableRepository : IDisposable
	{
		protected MigraineDiaryMVC_DBContext Context { get; set; }
		public bool SaveOnChange { get; set; }

		protected BaseDisposableRepository(MigraineDiaryMVC_DBContext context, bool saveOnChange = true)
		{
			this.Context = context;
			this.SaveOnChange = saveOnChange;
		}

		public void Save()
		{
			this.Context.SaveChanges();
		}

		protected void Save(bool shouldSave)
		{
			if (shouldSave)
			{
				this.Save();
			}
		}

		#region IDisposable Members

		private bool Disposed = false;

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!this.Disposed)
			{
				if (disposing)
				{
					this.Context.Dispose();
				}
			}

			this.Disposed = true;
		}

		#endregion
	}
}
