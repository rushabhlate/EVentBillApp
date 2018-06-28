using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventBillApplication.Models
{
    public class CreditStatemententity
    {
        public string Eventname { get; set; }
        public string creditby { get; set; }
        public decimal creditamount { get; set; }
    }
}