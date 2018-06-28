using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventBillApplication.Models
{
    public class CreditAmountEntity
    {
        public long Id { get; set; }
        public Nullable<long> evententryid { get; set; }
        public Nullable<int> Creditedfrom { get; set; }
        public Nullable<int> Creditedto { get; set; }
        public Nullable<decimal> Creditamount { get; set; }
        public Nullable<System.DateTime> entrydate { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public virtual UserEntity UserMaster { get; set; }
        public virtual UserEntity UserMaster1 { get; set; }
    }
}