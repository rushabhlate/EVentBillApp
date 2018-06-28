using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EventBillApplication.DAL;
using EventBillApplication.Models;
using EventBillApplication.Helper;
using System.Globalization;

namespace EventBillApplication.Controllers
{
    public class BillSettlementController : SuperController
    {
        //
        // GET: /BillSettlement/
        cl_Billsettlement obj = new cl_Billsettlement();

        public JsonResult getBalance(int creditbyid)
        {
            var currentuser=Helper.Helper.CurrentLoggedUser.Id;
            var balance = obj.getbalance(currentuser, creditbyid);
            return Json(balance);

        }

        public ActionResult Index()
        {
            var list = obj.Get().Where(x => x.Paidby == Helper.Helper.CurrentLoggedUser.Id).ToList();

            return View(list);
        }
        public ActionResult Create()
        {
            var currentuser = Helper.Helper.CurrentLoggedUser.Id;
            var userlist = new cl_User().Get().Where(x=>x.Id!=currentuser).ToList();
            ViewBag.userlist = new SelectList(userlist, "Id", "Fullname", 0);
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection frm,Billsettlemententity entity)
        {
            string eventdatestr = frm["entrydate"].ToString();
            entity.Paidby = Helper.Helper.CurrentLoggedUser.Id;
            if (eventdatestr != "")
            {
                DateTime evdate = DateTime.ParseExact(eventdatestr, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                entity.Entrydate = evdate;
             }
            var inserted = obj.Insert(entity);
            if (inserted)
                return RedirectToAction("Index");
            return View();
        }



    }
}
