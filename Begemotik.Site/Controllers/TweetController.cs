using Begemotik.Core;
using Begemotik.Twitter.Logic;
using Begemotik.Twitter.Logic.Exceptions;
using Begemotik.Twitter.Writter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Begemotik.Site.Controllers
{
    public class TweetController : Controller
    {
        //
        // GET: /Tweet/
        [HttpPost]
        public ActionResult PostStartTweet(FormCollection collection, string username)
        {
            try
            {
                var period = collection.Get("period");
                var status = string.Format("#{0} {1}", collection.Get("hash"), collection.Get("status"));
                HashHijack hj = new HashHijack(status, username, HashHijackPeriod.Hour);
                // pass this back so user can follow progress >> hj.HashHijackId
            }
            catch (HijackStartException hjex)
            {
                // temp re-throwe
                throw hjex;
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
