using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventBillApplication.Models
{
    public class EventCustomerentity
    {
        public long Id { get; set; }
        public Nullable<long> Evententryid { get; set; }
        public Nullable<int> Eventcustomerid { get; set; }
        public Nullable<decimal> Paidamount { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public virtual UserEntity UserMaster { get; set; }
    }
}