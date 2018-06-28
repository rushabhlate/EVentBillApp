using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventBillApplication.DAL;
using EventBillApplication.Models;
namespace EventBillApplication.Controllers
{
    public class EventController : SuperController    {
        //
        // GET: /Event/

        cl_Event obj = new cl_Event();

        public ActionResult Index()
        {
            var list = obj.Get();
            return View(list);
        }

        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(EventEntity entity)
        {
            entity.Createddate = DateTime.Now;
            entity.Createdby = Helper.Helper.CurrentLoggedUser.Id;
            var result=obj.Insert(entity);

            if (result)
                return RedirectToAction("Index");
            return View();
        }


        public ActionResult Edit(int id)
        {
            var record = obj.Getbyid(id);
            return View(record);
        }

         [HttpPost]
        public ActionResult Edit(EventEntity entity)
        {
            entity.Updatedate = DateTime.Now;
            entity.Updatedby = Helper.Helper.CurrentLoggedUser.Id;

            var result = obj.Update(entity);
            if (result)
                return RedirectToAction("Index");
            return View();
        }


        public JsonResult Delete(int id)
        {

            var result = obj.Delete(id);
            return Json(result);
        }


    }
}
