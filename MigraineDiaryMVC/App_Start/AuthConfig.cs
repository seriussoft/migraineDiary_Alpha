using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using MigraineDiaryMVC.Models;

namespace MigraineDiaryMVC
{
	public static class AuthConfig
	{
		public static void RegisterAuth()
		{
			// To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
			// you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

			//OAuthWebSecurity.RegisterMicrosoftClient(
			//    clientId: "",
			//    clientSecret: "");

			//OAuthWebSecurity.RegisterTwitterClient(
			//    consumerKey: "",
			//    consumerSecret: "");

			//OAuthWebSecurity.RegisterFacebookClient(
			//		appId: "212156475635263",
			//		appSecret: "16c521c509a36c97f76fc962bc419376");

			//OAuthWebSecurity.RegisterGoogleClient();
		}
	}
}
