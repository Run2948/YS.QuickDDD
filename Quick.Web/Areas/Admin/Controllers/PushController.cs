using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Quick.Web.Hubs;

namespace Quick.Web.Areas.Admin.Controllers
{
    public class PushController : Controller
    {
        // GET: Admin/Push
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Push(string msg)
        {
            IHubContext chat = GlobalHost.ConnectionManager.GetHubContext<PushHub>();
            chat.Clients.All.notice("系统通知", msg);
            return Json(new { status = 1, msg = "发送成功！" });
        }
    }
}