using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventBillApplication.Models
{
    public class Billsettlemententity
    {

        public long Id { get; set; }
        public Nullable<System.DateTime> Entrydate { get; set; }
        public Nullable<decimal> Creditamount { get; set; }
        public Nullable<decimal> Paidamount { get; set; }
        public Nullable<int> Paidby { get; set; }
        public Nullable<int> Creditedfrom { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}