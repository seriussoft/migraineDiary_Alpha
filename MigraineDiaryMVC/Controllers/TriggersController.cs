using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SeriusSoft.MigraineDiaryMVC.MigraineModels;
using SeriusSoft.MigraineDiaryMVC.MigraineRepository;

namespace MigraineDiaryMVC.Controllers
{
	public class TriggersController : Controller
	{
		private MigraineDiaryMVC_DBContext db = new MigraineDiaryMVC_DBContext();

		[Route("~/Triggers/Index")]
		[Route("~/Triggers/")]
		public async Task<ActionResult> Index()
		{
			var triggers = db.Triggers.Include(t => t.TriggerType);
			return View(await triggers.ToListAsync());
		}

		// GET: /Triggers/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			TriggerModel triggermodel = await db.Triggers.FindAsync(id);
			if (triggermodel == null)
			{
				return HttpNotFound();
			}
			return View(triggermodel);
		}

		// GET: /Triggers/Create
		public ActionResult Create()
		{
			ViewBag.TriggerTypeID = new SelectList(db.TriggerTypes, "TriggerTypeID", "Name");
			return View();
		}

		// POST: /Triggers/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "TriggerID,TriggerTypeID,Name,Info")] TriggerModel triggermodel)
		{
			if (ModelState.IsValid)
			{
				db.Triggers.Add(triggermodel);
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			ViewBag.TriggerTypeID = new SelectList(db.TriggerTypes, "TriggerTypeID", "Name", triggermodel.TriggerTypeID);
			return View(triggermodel);
		}

		// GET: /Triggers/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			TriggerModel triggermodel = await db.Triggers.FindAsync(id);
			if (triggermodel == null)
			{
				return HttpNotFound();
			}
			ViewBag.TriggerTypeID = new SelectList(db.TriggerTypes, "TriggerTypeID", "Name", triggermodel.TriggerTypeID);
			return View(triggermodel);
		}

        /// <summary>
        /// This is just for loading up the partial editor (must be non-async because of how mvc works with actions calling actions
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
    public ActionResult EditNonAsync(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      TriggerModel triggermodel = db.Triggers.Find(id);
      if (triggermodel == null)
      {
        return HttpNotFound();
      }
      ViewBag.TriggerTypeID = new SelectList(db.TriggerTypes, "TriggerTypeID", "Name", triggermodel.TriggerTypeID);
      //return View("Edit", triggermodel);
      //return View("EditSansLayoutPartial", triggermodel);
      return View("EditDialogPartial", triggermodel);
    }

		// POST: /Triggers/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "TriggerID,TriggerTypeID,Name,Info")] TriggerModel triggermodel)
		{
			if (ModelState.IsValid)
			{
				db.Entry(triggermodel).State = EntityState.Modified;
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewBag.TriggerTypeID = new SelectList(db.TriggerTypes, "TriggerTypeID", "Name", triggermodel.TriggerTypeID);
			return View(triggermodel);
		}

        /// <summary>
        /// This is just for loading up the partial editor (must be non-async because of how mvc works with actions calling actions
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNonAsync([Bind(Include = "TriggerID,TriggerTypeID,Name,Info")] TriggerModel triggerModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(triggerModel).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.TriggerTypeID = new SelectList(db.TriggerTypes, "TriggerTypeID", "Name", triggerModel.TriggerTypeID);
                return View("EditDialogPartial", triggerModel);//RedirectToAction("Index");
            }

            ViewBag.TriggerTypeID = new SelectList(db.TriggerTypes, "TriggerTypeID", "Name", triggerModel.TriggerTypeID);
            //return View("Edit", triggerModel);
            return View("EditDialogPartial", triggerModel);
        }

		// GET: /Triggers/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			TriggerModel triggermodel = await db.Triggers.FindAsync(id);
			if (triggermodel == null)
			{
				return HttpNotFound();
			}
			return View(triggermodel);
		}

		// POST: /Triggers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			TriggerModel triggermodel = await db.Triggers.FindAsync(id);
			db.Triggers.Remove(triggermodel);
			await db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
