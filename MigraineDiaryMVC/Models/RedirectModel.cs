using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineDiaryMVC.Models
{
	public class RedirectModel
	{
		public string Action { get; set; }
		public string ReturnUrl { get; set; }
	}
}