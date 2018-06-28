using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventBillApplication.DAL;
using EventBillApplication.Models;
using System.Globalization;
namespace EventBillApplication.Controllers
{
    public class EventEntryController : SuperController
    {
        //
        // GET: /EventEntry/
        cl_EventEntry obj = new cl_EventEntry();
        public ActionResult Index()
        {
            var curuser = Helper.Helper.CurrentLoggedUser.Id;
            var list = obj.Get(curuser);
            return View(list);
        }

        public ActionResult CreditstatementReport(int? id,int userid)
        {
            //var records = obj.Getcreditstatement(id, userid);
            var records = obj.getcreditdetail(id,userid);
            return View(records);
        }

        public ActionResult InsertCreditAmountentry(long id)
        {

            ViewBag.evententryid = id;
            var curuuserid=Helper.Helper.CurrentLoggedUser.Id;
            var listofuserid = obj.Getbyid((int)id);
            List<UserEntity> eventuserlist = new List<UserEntity>();
            foreach (var cust in listofuserid.customerlist)
            {
                eventuserlist.Add(cust.UserMaster);
            }
            
          //  var userlist = new cl_User().Get().Where(x=>x.Id!=curuuserid).ToList();
            eventuserlist = eventuserlist.Where(x => x.Id != curuuserid).ToList();




            ViewBag.userlist = new SelectList(eventuserlist, "Id", "Fullname", 0);
            return View();
        }

        [HttpPost]
        public ActionResult InsertCreditAmountentry(FormCollection frm)
        {
            var currentuserid = Helper.Helper.CurrentLoggedUser.Id;
            long evenentryid = Convert.ToInt64(frm["eventryid"]);
            List<CreditAmountEntity> creditlist = new List<CreditAmountEntity>();
            var customerlist = frm["rowstr"].ToString();
            if (customerlist != null)
            {
                string[] custstr = customerlist.Split('#');

                foreach (var cust in custstr)
                {
                    CreditAmountEntity entity = new CreditAmountEntity();
                    var custarr = cust.Split('~');
                   // EventCustomerentity eventcustentity = new EventCustomerentity();
                    entity.Creditedfrom =Convert.ToInt32(custarr[0]);
                    entity.Creditamount = Convert.ToDecimal(custarr[1]);
                    entity.Creditedto = currentuserid;
                    entity.evententryid = evenentryid;
                    entity.IsActive = true;
                    creditlist.Add(entity);

                }
            }

            var inserted = obj.Insertcreditamountentry(creditlist);
            if (inserted)
                return RedirectToAction("Index");

            return View();
        }


        public ActionResult Create()
        {

            var userlist = new cl_User().Get();
            var eventlist = new cl_Event().Get();
            ViewBag.eventlist = new SelectList(eventlist, "Id", "Eventname", 0);
            ViewBag.userlist = new SelectList(userlist, "Id", "Fullname", 0);
            return View();
        }


        [HttpPost]
        public ActionResult Create(FormCollection frm)
        {

            EventEntryEntity entity = new EventEntryEntity();


            entity.Createddate = DateTime.Now;
            entity.Createdby = Helper.Helper.CurrentLoggedUser.Id;
            entity.Eventid=Convert.ToInt64(frm["eventid"]);
            entity.Location=frm["Location"].ToString();
            entity.TotalAmount =Convert.ToDecimal(frm["TotalAmount"]);
            entity.IsActive=true;

            string eventdatestr = frm["entrydate"].ToString();

            if (eventdatestr != "")
            {
                DateTime evdate = DateTime.ParseExact(eventdatestr, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                entity.Eventdate = evdate;


            }
            List<EventCustomerentity> eventcustlist = new List<EventCustomerentity>();
            var customerlist = frm["rowstr"].ToString();
            if (customerlist != null)
            {
                string[] custstr = customerlist.Split('#');

                foreach (var cust in custstr)
                {
                    var custarr = cust.Split('~');
                    EventCustomerentity eventcustentity = new EventCustomerentity();
                    eventcustentity.Eventcustomerid = Convert.ToInt32(custarr[0]);
                    eventcustentity.Paidamount = Convert.ToDecimal(custarr[1]);
                    eventcustlist.Add(eventcustentity);

                }
            }

            entity.customerlist = eventcustlist;
            var inserted = obj.Insert(entity);
            if (inserted)
            {
                return RedirectToAction("Index");
            }


           return View();
        }


        public ActionResult Edit(int id)
        {

            var record = obj.Getbyid(id);
            var userlist = new cl_User().Get();
            var eventlist = new cl_Event().Get();
            ViewBag.eventlist = new SelectList(eventlist, "Id", "Eventname", record.Eventid);
            ViewBag.userlist = new SelectList(userlist, "Id", "Fullname", 0);
            return View(record);
        }



        public JsonResult Delete(long id)
        {

            var result = obj.Delete(id);
            return Json(result);
        }


    }
}
