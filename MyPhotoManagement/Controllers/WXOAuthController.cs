using MyPhotoManagement.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPhotoManagement.Controllers
{
    public class WXOAuthController : Controller
    {
        //
        // GET: /WXOAuth/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string tar)
        {
            string appId = "wx736088cc10e7c5a2";
            string appSecret = "b739790ae7a72d7610f1e6b542a8ef90";
            var weixinOAuth = new WeiXinOAuth();

            //微信第一次握手后得到的code 和state
            string code = "";
            string state = "";
            if (code == "" || code == "authdeny")
            {
                if (code == "")
                {
                    //发起授权(第一次微信握手)
                    string authUrl = weixinOAuth.GetWeiXinCode(appId, appSecret, Server.UrlEncode(Request.Url.ToString()), true);
                    Response.Redirect(authUrl, true);
                }
                else
                {
                    // 用户取消授权
                    //Response.Redirect("~/Error.html", true);
                }
            }

            return View();
        }

    }
}
