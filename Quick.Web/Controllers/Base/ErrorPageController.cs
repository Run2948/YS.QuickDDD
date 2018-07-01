using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quick.Web.Controllers
{
    public class ErrorPageController : Controller
    {
        // GET: ErrorPage
        public ActionResult NotFound()
        {
            Response.Status = "404 Not Found";
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult ServerError()
        {
            Response.Status = "500 ServerError";
            Response.StatusCode = 500;
            return View();
        }

        public ActionResult Finished(string msg)
        {
            return PartialView("Finished", msg);
        }
    }
}