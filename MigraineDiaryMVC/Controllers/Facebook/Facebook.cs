using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MigraineDiaryMVC.Controllers.Facebook
{
	public class FacebookHandler
	{
		public string Facebook_GraphAPI_Token = "https://graph.facebook.com/oauth/access_token?";
		public string Facebook_GraphAPI_Me = "https://graph.facebook.com/me?";
		public string AppID { get; set; }
		public string AppSecret { get; set; }

		public FacebookHandler(string appID, string appSecret)
		{
			this.AppID = appID;
			this.AppSecret = appSecret;
		}

		public IDictionary<string, string> GetUserData(string accessCode, string redirectURI)
		{
			var tokenUrl = String.Format("{0}client_id={1}&redirect_uri={2}%3F__provider__%3Dfacebook&client_secret={3}&code={4}", this.Facebook_GraphAPI_Token, this.AppID, HttpUtility.HtmlEncode(redirectURI), this.AppSecret, accessCode);
			string token = WebMethods.GetHTML(tokenUrl);
			if (String.IsNullOrEmpty(token))
			{
				return null;
			}

			var dataUrl = String.Format("{0}fields=id,name,email,username,gender,link&access_token={1}", this.Facebook_GraphAPI_Me, token.Substring("access_token=", "&"));
			string data = WebMethods.GetHTML(dataUrl);

			// this dictionary must contains
			var userData = new Dictionary<string, string>();
			userData.Add("id", data.Substring("\"id\":\"", "\""));
			userData.Add("username", data.Substring("username\":\"", "\""));
			userData.Add("name", data.Substring("name\":\"", "\""));
			userData.Add("link", data.Substring("link\":\"", "\"").Replace("\\/", "/"));
			userData.Add("gender", data.Substring("gender\":\"", "\""));
			userData.Add("email", data.Substring("email\":\"", "\"").Replace("\\u0040", "@"));
			userData.Add("accesstoken", token.Substring("access_token=", "&"));
			return userData;
		}

	}

	public static class WebMethods
	{
		public static string GetHTML(string url)
		{
			string connectionString = url;

			try
			{
				var myRequest = (HttpWebRequest)WebRequest.Create(connectionString);
				myRequest.Credentials = CredentialCache.DefaultCredentials;

				var webResponse = myRequest.GetResponse();
				string pageContent = null;

				using (var respStream = webResponse.GetResponseStream())
				{
					using (var ioStream = new StreamReader(respStream))
					{
						pageContent = ioStream.ReadToEnd();
					}
				}

				return pageContent;
			}
			catch
			{
				//why does he just absorb exceptions????
				//throw;
			}

			return null;
		}
	}

	public static class StringExtensions
	{
		public static string Substring(this string originalString, string startString, string endString)
		{
			if (originalString.Contains(startString))
			{
				int iStart = originalString.IndexOf(startString) + startString.Length;
				int iEnd = originalString.IndexOf(endString, iStart);
				return originalString.Substring(iStart, (iEnd - iStart));
			}
			return null;
		}
	}
}

namespace MigraineDiaryMVC.Controllers.FacebookInfo
{
	using MigraineDiaryMVC.Controllers.Facebook;

	//these guys will go in your normal controller
	public class FacebookController
	{

		//[HttpPost]
		//[AllowAnonymous]
		//[ValidateAntiForgeryToken]
		//public ActionResult ExternalLogin(string provider, string returnUrl)
		//{
		//	returnUrl = Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl });
		//	var facebookHandler = CreateFacebookHandler();
		//	var accountPage = "UserProfile"; //Account
		//	return Redirect("https://www.facebook.com/dialog/oauth?client_id=" + facebookHandler.AppID + "&redirect_uri=" + HttpUtility.HtmlEncode("h" + System.Web.HttpContext.Current.Request.Url.ToString().Substring("h", "/" + accountPage) + returnUrl) + "%3F__provider__%3Dfacebook&scope=email");


		//	//return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
		//}

		//[AllowAnonymous]
		//public ActionResult ExternalLoginCallback(string returnUrl)
		//{
		//	//AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
		//	string code = Request.QueryString["code"];
		//	string returnUrl1 = Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl });

		//	var facebookHandler = CreateFacebookHandler();
		//	var accountPage = "/UserProfile"; //"/Account"
		//	IDictionary<string, string> userData = facebookHandler.GetUserData(code, HttpUtility.HtmlEncode("h" + System.Web.HttpContext.Current.Request.Url.ToString().Substring("h", accountPage) + returnUrl1));
		//	var result = new DotNetOpenAuth.AspNet.AuthenticationResult(isSuccessful: true, provider: "facebook", providerUserId: userData["id"], userName: userData["username"], extraData: userData);

		//	if (!result.IsSuccessful)
		//	{
		//		return RedirectToAction("ExternalLoginFailure");
		//	}
		//	else
		//	{
		//		//new EMail().SendMail("Registration", userData["email"], new System.String[] { userData["name"], userData["id"] });
		//	}


		//	if (Microsoft.Web.WebPages.OAuth.OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
		//	{
		//		return RedirectToLocal(returnUrl);
		//	}

		//	if (User.Identity.IsAuthenticated)
		//	{
		//		// If the current user is logged in add the new account
		//		Microsoft.Web.WebPages.OAuth.OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
		//		return RedirectToLocal(returnUrl);
		//	}
		//	else
		//	{
		//		// User is new, ask for their desired membership name
		//		string loginData = Microsoft.Web.WebPages.OAuth.OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
		//		ViewBag.ProviderDisplayName = Microsoft.Web.WebPages.OAuth.OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
		//		ViewBag.ReturnUrl = returnUrl;
		//		return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
		//	}
		//}

		private Facebook.FacebookHandler CreateFacebookHandler()
		{
			const string appID = "180809595462326";
			const string appSecret = "f19431784131585eb6abeddd1e32d681";

			var handler = new Facebook.FacebookHandler(appID, appSecret);
			return handler;
		}

	}
}