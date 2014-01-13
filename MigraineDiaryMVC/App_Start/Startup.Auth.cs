using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Web.WebPages.OAuth;
using System.Threading.Tasks;

namespace MigraineDiaryMVC
{
	public partial class Startup
	{
		public void ConfigureAuth(IAppBuilder app)
		{
			app.UseCookieAuthentication(new CookieAuthenticationOptions()
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,//.ExternalCookie,
				LoginPath = new PathString(string.Format("/{0}/Login", Controllers.AccountController.CONTROLLER_NAME))
				//LoginPath = new PathString("/Account/Login")
			});

			app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

			//app.UseMicrosoftAccountAuthentication(clientId: "", clientSecret: "");
			//app.UseTwitterAuthentication(consumerKey: "", consumerSecret: "");
			//app.UseFacebookAuthentication(appId: "212156475635263", appSecret: "16c521c509a36c97f76fc962bc419376");
			//app.UseFacebookAuthentication(appId: "212156475635263", appSecret: "d1f20a62001e1bc70237cce5e6589e65");
			//app.UseFacebookAuthentication(options: new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions() { AppId = "180809595462326", AppSecret = "f19431784131585eb6abeddd1e32d681" });

			FacebookAuthentication.AddFacebookAuthentication(app);

			app.UseGoogleAuthentication();
		}

		

		protected class Constants
		{
			public const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";
		}
	}

	public class FacebookAuthentication
	{
		public static void AddFacebookAuthentication(IAppBuilder app)
		{
			const string appID = "180809595462326";
			const string appSecret = "f19431784131585eb6abeddd1e32d681";

			//AddFacebookOAuth2(app, appID, appSecret);
			//AddFacebookOAuth(app, appID, appSecret);
			//AddFacebookOWIN(app, appID, appSecret);
			AddFacebookOWIN_Quick(app, appID, appSecret);
		}

		private static void AddFacebookOWIN_Quick(IAppBuilder app, string appID, string appSecret)
		{
			app.UseFacebookAuthentication(appId: appID, appSecret: appSecret);
		}

		//private static void AddFacebookOWIN(IAppBuilder app, string appID, string appSecret)
		//{
		//	var facebookOptions = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions()
		//	{
		//		AppId = appID,
		//		AppSecret = appSecret,
		//		Provider = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationProvider()
		//		{
		//			OnAuthenticated = (context) =>
		//			{
		//				context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:access_token", context.AccessToken, Constants.XmlSchemaString, "Facebook"));
		//				foreach (var x in context.User)
		//				{
		//					var claimType = string.Format("urn:facebook:{0}", x.Key);
		//					var claimValue = x.Value.ToString();
		//					if (!context.Identity.HasClaim(claimType, claimValue))
		//						context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, Constants.XmlSchemaString, "Facebook"));
		//				}
		//				return Task.FromResult(0);
		//			}
		//		}
		//	};

		//	facebookOptions.Scope.Add("email");
		//	app.UseFacebookAuthentication(facebookOptions);
		//}

		private static void AddFacebookOAuth2(IAppBuilder app, string appID, string appSecret)
		{
			//app.UseFacebookAuthentication(appId: appID, appSecret: appSecret);
			var fb = new FacebookAuthenticationOptions();
			fb.Scope.Add("email");
			fb.AppId = appID;
			fb.AppSecret = appSecret;

			fb.Provider = new FacebookAuthenticationProvider()
			{
				OnAuthenticated = async fbContext => fbContext.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", fbContext.AccessToken))
			};

			fb.SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie;

			app.UseFacebookAuthentication(fb);
		}

		private static void AddFacebookOAuth(IAppBuilder app, string appID, string appSecret)
		{
			OAuthWebSecurity.RegisterFacebookClient(appId: appID, appSecret: appSecret);
		}

		//private void AddLinkedInOAuth(IAppBuilder app, string consumerKey, string consumerSecret)
		//{
		//	OAuthWebSecurity.RegisterLinkedInClient(consumerKey, consumerSecret);

		//	app.UseOAuthBearerAuthentication(new Microsoft.Owin.Security.OAuth.OAuthBearerAuthenticationOptions() { 
		//}
	}
}