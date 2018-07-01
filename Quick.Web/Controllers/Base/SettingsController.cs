using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick.Data.Entities;
using Quick.Web.Filters;

namespace Quick.Web.Controllers.Base
{
    public class SettingsController : UserBaseController
    {
        // GET: Settings
        public ActionResult Index()
        {
            var model = ConfigService.loadConfig();
            return View(model);
        }

        [HttpPost]
        [ValidateModelState]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SiteConfig model)
        {
            if (ModelState.IsValid)
            {
                ConfigService.saveConifg(model);
                return FinishResult("系统参数修改成功！");
            }
            return View(model);
        }
    }
}