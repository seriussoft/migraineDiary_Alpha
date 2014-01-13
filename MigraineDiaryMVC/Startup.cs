using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MigraineDiaryMVC.Startup))]
namespace MigraineDiaryMVC
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}