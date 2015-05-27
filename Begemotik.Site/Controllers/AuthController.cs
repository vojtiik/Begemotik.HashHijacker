using Begemotik.Twitter.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Begemotik.Site.Controllers
{
    public class AuthController : Controller
    {
        //
        // GET: /Auth/

        public ActionResult Authorize()
        {
            var callbackUrl = Url.Action("AuthorizeCallback", "Auth", null, "http");
            var uri = Authenticator.SignMeInUsingTwitter(callbackUrl);
            return new RedirectResult(uri.ToString(), false /*permanent*/);
        }

        // This URL is registered as the application's callback at http://dev.twitter.com
        public ActionResult AuthorizeCallback(string oauth_token, string oauth_verifier)
        {
            var username = Authenticator.SignMeInUsingTwitter_Callback(oauth_token, oauth_verifier);
            FormsAuthentication.SetAuthCookie(username, false);
            return RedirectToAction("Index", "Home");
        }
    }
}
