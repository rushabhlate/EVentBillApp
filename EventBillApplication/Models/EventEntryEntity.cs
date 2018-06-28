using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventBillApplication.Models
{
    public class EventEntryEntity
    {
        public long Id { get; set; }
        public Nullable<long> Eventid { get; set; }
        public Nullable<System.DateTime> Eventdate { get; set; }
        public string Location { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<System.DateTime> Createddate { get; set; }
        public Nullable<int> Createdby { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public List<EventCustomerentity> customerlist { get; set; }
        public virtual EventEntity EventMaster { get; set; }
    }
}