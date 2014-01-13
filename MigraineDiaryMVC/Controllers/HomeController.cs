using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MigraineDiaryMVC.Controllers
{
	public class HomeController : Controller
	{
		[Route("Home/")]
		[Route("~/")]
		public ActionResult Index()
		{
			ViewBag.Message = "Helping you regain some control over your life in your fight against migraines.";

			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your app description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}
