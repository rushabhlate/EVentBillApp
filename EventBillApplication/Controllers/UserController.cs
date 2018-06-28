using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventBillApplication.DAL;
using EventBillApplication.Models;
using EventBillApplication.Helper;
namespace EventBillApplication.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        cl_User obj = new cl_User();

        public ActionResult Index()
        {
            var list = obj.Get();
            return View(list);
        }
        
        public ActionResult Create()
        {
            List<UserTypeEntity> usertypelist = new List<UserTypeEntity>()
            {
            new UserTypeEntity(){
            Id=1,
            Usertype="Admin"
            },
             new UserTypeEntity(){
            Id=2,
            Usertype="User"
            }
            };

            ViewBag.usertypelist = new SelectList(usertypelist, "Id", "Usertype", 0);
            return View();
        }
         [HttpPost]
        public ActionResult Create(UserEntity entity)
        {
            entity.Createddate = DateTime.Now;
           // entity.Createdby = Helper.Helper.CurrentLoggedUser.Id;
            entity.password = Helper.Helper.Encrypt(entity.password);

            var result = obj.Insert(entity);
            if (result)
                return RedirectToAction("Index");
            return View();
        }


        public ActionResult Edit(int id)
        {
            var record = obj.Getbyid(id);
            record.password = Helper.Helper.Decrypt(record.password);
            List<UserTypeEntity> usertypelist = new List<UserTypeEntity>()
            {
            new UserTypeEntity(){
            Id=1,
            Usertype="Admin"
            },
             new UserTypeEntity(){
            Id=2,
            Usertype="User"
            }
            };
            ViewBag.usertypelist = new SelectList(usertypelist, "Id", "Usertype", record.Usertypeid);



            return View(record);
        }
        [HttpPost]
        public ActionResult Edit(UserEntity entity)
        {
            entity.Updateddate = DateTime.Now;
            entity.Updatedby = Helper.Helper.CurrentLoggedUser.Id;
            entity.password = Helper.Helper.Encrypt(entity.password);
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
