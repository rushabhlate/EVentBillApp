using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventBillApplication.Models
{
    public class UserTypeEntity
    {
        public int Id { get; set; }
        public string Usertype { get; set; }

        public virtual ICollection<UserEntity> UserMasters { get; set; }
    }
}