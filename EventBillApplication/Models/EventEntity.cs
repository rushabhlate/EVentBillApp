using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace EventBillApplication.Models
{
    public class EventEntity
    {
        public long Id { get; set; }
        [Required]
        public string Eventname { get; set; }
        public Nullable<System.DateTime> Createddate { get; set; }
        public Nullable<int> Createdby { get; set; }
        public Nullable<System.DateTime> Updatedate { get; set; }
        public Nullable<int> Updatedby { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}