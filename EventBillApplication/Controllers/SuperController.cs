using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventBillApplication.Controllers
{
    public class SuperController : Controller
    {
        //
        // GET: /Super/

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (Session["LoggedUser"] != null)
                    base.OnActionExecuting(filterContext);
                else
                    filterContext.Result = new RedirectResult("~/Public/Index");
            }
            catch (Exception)
            {
                TempData["Message"] = "Something went worng... Please try again..!!!";
                TempData["MessageClass"] = "alert-danger";

                filterContext.Result = new RedirectResult("~/Public/Index");
            }
        }
    }
}
