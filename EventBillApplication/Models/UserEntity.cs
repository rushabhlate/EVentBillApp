using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace EventBillApplication.Models
{
    public class UserEntity
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string Fullname { get; set; }
        public Nullable<System.DateTime> Createddate { get; set; }
        public Nullable<int> Createdby { get; set; }
        public Nullable<System.DateTime> Updateddate { get; set; }
        public Nullable<int> Updatedby { get; set; }
        [Required]
        public Nullable<int> Usertypeid { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public virtual UserTypeEntity UserTypeMaster { get; set; }
    }
}