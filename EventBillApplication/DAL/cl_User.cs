using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventBillApplication.Models;
using EventBillApplication.Database;
namespace EventBillApplication.DAL
{
    public class cl_User
    {

        public List<UserEntity> Get()
        {
            List<UserEntity> list = new List<UserEntity>();
            using (var db = new EventDBEntities())
            {
                var records = db.UserMasters.Where(x => x.IsActive == true).ToList();
                foreach (var item in records)
                {
                    UserEntity userentity = new UserEntity();
                    userentity.Id = item.Id;
                    userentity.Fullname = item.Fullname;
                    UserTypeEntity usertypeentity = new UserTypeEntity();
                    if (item.UserTypeMaster != null)
                    {
                        usertypeentity.Id = item.UserTypeMaster.Id;
                        usertypeentity.Usertype = item.UserTypeMaster.Usertype;
                        userentity.UserTypeMaster = usertypeentity;
                    }

                    list.Add(userentity);
                }

            }

            return list;

        }



        public bool Insert(UserEntity user)
        {
            bool isinserted = false;
            using (var db = new EventDBEntities())
            {
                UserMaster record = new UserMaster();
                record.Fullname = user.Fullname;
                record.Createddate = user.Createddate;
                record.Createdby = user.Createdby;
                record.IsActive = true;
                record.password = user.password;
                record.Updatedby = user.Updatedby;
                record.Updateddate = user.Updateddate;
                record.Username = user.Username;
                record.Usertypeid = user.Usertypeid;
                db.UserMasters.Add(record);
                db.SaveChanges();
                isinserted = true;

            }

            return isinserted;

        }

        public bool Update(UserEntity user)
        {
            bool isupdated = false;
            using (var db = new EventDBEntities())
            {
                UserMaster record = db.UserMasters.Find(user.Id);
                record.Fullname = user.Fullname;
               // record.Createddate = user.Createddate;
               // record.Createdby = user.Createdby;
                record.IsActive = true;
                record.password = user.password;
                record.Updatedby = user.Updatedby;
                record.Updateddate = user.Updateddate;
                record.Username = user.Username;
                record.Usertypeid = user.Usertypeid;
                db.SaveChanges();
                isupdated = true;

            }
            return isupdated;
        }


        public bool Delete(int id)
        {
            bool isdeleted = false;
            using (var db = new EventDBEntities())
            {
                var record = db.UserMasters.Find(id);
                record.IsActive = false;
                db.SaveChanges();
                isdeleted = true;

            }
            return isdeleted;

        }

        public UserEntity Getbyid(int id)
        {
            UserEntity entity = new UserEntity();
            using (var db = new EventDBEntities())
            {
                var record = db.UserMasters.Find(id);
                entity.Fullname = record.Fullname;
                // record.Createddate = user.Createddate;
                // record.Createdby = user.Createdby;
               // entity.IsActive = true;
                entity.password = record.password;
                entity.Updatedby = record.Updatedby;
                entity.Updateddate = record.Updateddate;
                entity.Username = record.Username;
                entity.Usertypeid = record.Usertypeid;

            }
            return entity;

        }


    }
}