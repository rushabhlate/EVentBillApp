using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventBillApplication.Models;
using EventBillApplication.Database;

namespace EventBillApplication.DAL
{
    public class cl_UserAuth
    {
        public UserEntity Authenticateuser(LoginEntity entity)
        {
            UserEntity user = new UserEntity();
            using (var db = new EventDBEntities())
            {

                var record = db.UserMasters.Where(x => x.Username == entity.UserName && x.password == entity.Password && x.IsActive == true).FirstOrDefault();
                if (record != null)
                {

                    user = Map(record);
                }

            }

            return user;
        }


        private UserEntity Map(UserMaster records)
        {
            UserEntity model = new UserEntity();
            model.Id = records.Id;
            model.Fullname = records.Fullname;
            model.Username = records.Username;
            model.Usertypeid = records.Usertypeid;
            model.IsActive = records.IsActive;
            model.password = records.password;


            UserTypeEntity entity1 = new UserTypeEntity();

            entity1.Id = (int)records.Usertypeid;
            entity1.Usertype = records.UserTypeMaster.Usertype;
            model.UserTypeMaster = entity1;


            return model;
        }

    }
}