using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quick.Web.Controllers
{
    public class UserBaseController : Controller
    {
        // GET: UserBase
        public ActionResult Index()
        {
            return View();
        }
    }
}