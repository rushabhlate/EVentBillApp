//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventBillApplication.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class EventMaster
    {
        public EventMaster()
        {
            this.EventEntryMasters = new HashSet<EventEntryMaster>();
        }
    
        public long Id { get; set; }
        public string Eventname { get; set; }
        public Nullable<System.DateTime> Createddate { get; set; }
        public Nullable<int> Createdby { get; set; }
        public Nullable<System.DateTime> Updatedate { get; set; }
        public Nullable<int> Updatedby { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual ICollection<EventEntryMaster> EventEntryMasters { get; set; }
    }
}