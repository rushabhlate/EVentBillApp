using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventBillApplication.Models;
using EventBillApplication.Database;



namespace EventBillApplication.DAL
{
    public class cl_EventEntry
    {



        public bool Insertcreditamountentry(List<CreditAmountEntity> list)
        {
            bool inserted = false;
            using (var db = new EventDBEntities())
            {
                foreach (var item in list)
                {
                    CreditAmountDetail record = new CreditAmountDetail();
                    record.Creditamount = item.Creditamount;
                    record.Creditedfrom = item.Creditedfrom;
                    record.Creditedto = item.Creditedto;
                    record.entrydate = item.entrydate;
                    record.evententryid = item.evententryid;
                    record.IsActive = true;
                    db.CreditAmountDetails.Add(record);
                    db.SaveChanges();


                }
                inserted = true;

            }
            return inserted; 
        }

        public List<CreditStatemententity> getcreditdetail(int? id,int userid)
        {
            List<CreditStatemententity> list = new List<CreditStatemententity>();

            using (var db = new EventDBEntities())
            {
                var records = db.CreditAmountDetails.Where(x => x.evententryid == id && x.Creditedto==userid).ToList();
                foreach (var item in records)
                {
                    CreditStatemententity entity = new CreditStatemententity();
                    entity.creditamount =(decimal) item.Creditamount;
                    var creditedname = db.UserMasters.Find(item.Creditedfrom).Fullname;
                    entity.creditby = creditedname;
                    list.Add(entity);
                    
                }

            }

            return list;


        }


        public List<CreditStatemententity> Getcreditstatement(int? id,int userid)
        {
            List<CreditStatemententity> list = new List<CreditStatemententity>();
            if (id != null)
            {
                using (var db = new EventDBEntities())
                {
                    var records = db.EventCustomers.Where(x => x.Evententryid == id).ToList();
                    if (records.Count > 1)
                    {
                        var noofcustomers=records.Count;
                        var totalamount = db.EventEntryMasters.Find(id).TotalAmount;
                        var userpaidamt=records.Where(x=>x.Eventcustomerid==userid).FirstOrDefault().Paidamount;


                        records = records.Where(x => x.Eventcustomerid != userid).ToList();
                        var equalamt = totalamount / noofcustomers;
                        if(userpaidamt<equalamt)//if user paid less than divided amt
                        {
                         var userremainpaidamt = equalamt - userpaidamt;
                        foreach (var item in records)
                        {
                            CreditStatemententity entity=new CreditStatemententity();

                            if (item.Paidamount > equalamt)
                            {

                                if (userremainpaidamt > 0)
                                {
                                    var creditpaidamt = item.Paidamount - equalamt;
                                    if (userremainpaidamt <= creditpaidamt)
                                    {


                                        entity.creditamount = (decimal)userremainpaidamt;
                                        entity.creditby = item.UserMaster.Fullname;

                                        list.Add(entity);
                                        userremainpaidamt = 0;
                                        break;
                                    }
                                    else
                                    {
                                        userremainpaidamt = userremainpaidamt - creditpaidamt;
                                        entity.creditamount = (decimal)creditpaidamt;
                                        entity.creditby = item.UserMaster.Fullname;
                                        list.Add(entity);
                                    }
                                }
                            }

                        }
                     }

                    }


                }


            }
            return list;
        }




        public List<EventEntryEntity> Get(int userid)
        {
            List<EventEntryEntity> list = new List<EventEntryEntity>();
            using (var db = new EventDBEntities())
            {
                var records = db.EventEntryMasters.Where(x => x.IsActive == true).ToList();
                foreach (var item in records)
                {
                    EventEntryEntity entity = new EventEntryEntity();
                    entity.Id = item.Id;
                    entity.TotalAmount = item.TotalAmount;
                    entity.Eventdate = item.Eventdate;
                    EventEntity event_ = new EventEntity();
                    if (item.EventMaster != null)
                    {
                        event_.Id = item.EventMaster.Id;
                        event_.Eventname = item.EventMaster.Eventname;
                        entity.EventMaster = event_;

                    }
                    var isexist = db.EventCustomers.Where(x => x.Eventcustomerid == userid && x.Evententryid==item.Id).FirstOrDefault();
                    if (isexist != null)
                    {
                        list.Add(entity);
                    }


                   
                }

            }

            return list;

        }


        public bool Insert(EventEntryEntity entity)
        {
            bool isinserted = false;
            using (var db = new EventDBEntities())
            {
                EventEntryMaster record = new EventEntryMaster();
                record.Eventid = entity.Eventid;
                record.Eventdate = entity.Eventdate;
                record.Createddate = entity.Createddate;
                record.Createdby = entity.Createdby;
                record.TotalAmount = entity.TotalAmount;
                record.Location = entity.Location;
                record.Id = entity.Id;
                record.IsActive = true;
                db.EventEntryMasters.Add(record);
                db.SaveChanges();
                long evenentryid = record.Id;

                if (evenentryid > 0)
                {
                    if (entity.customerlist != null)
                    {
                        foreach (var item in entity.customerlist)
                        {
                            EventCustomer eventcustrecord = new EventCustomer();
                            eventcustrecord.Eventcustomerid = item.Eventcustomerid;
                            eventcustrecord.Paidamount = item.Paidamount;
                            eventcustrecord.IsActive = true;
                            eventcustrecord.Evententryid = evenentryid;
                            db.EventCustomers.Add(eventcustrecord);
                            db.SaveChanges();

                        }

                    }

                }

                isinserted = true;


            }
            return isinserted;
        }


        public bool Update(EventEntryEntity entity)
        {
            bool isupdated = false;
             using (var db = new EventDBEntities())
            {
             }

             return isupdated;
        }



        public bool Delete(long id)
        {
            bool isdeleted = false;
            using (var db = new EventDBEntities())
            {
                var record = db.EventEntryMasters.Find(id);
                record.IsActive = false;
                db.SaveChanges();

                var eventcustrecords = db.EventCustomers.Where(x => x.Evententryid == id).ToList();
                if (eventcustrecords != null)
                {
                    foreach (var item in eventcustrecords)
                    {
                        item.IsActive = false;
                        db.SaveChanges();

                        
                    }
                }

                isdeleted = true;

            }
            return isdeleted;
        }

        public EventEntryEntity Getbyid(int id)
        {
            EventEntryEntity entity = new EventEntryEntity();
            using (var db = new EventDBEntities())
            {
                var record = db.EventEntryMasters.Find(id);
                entity.Id = record.Id;
                entity.Location = record.Location;
                entity.TotalAmount = record.TotalAmount;
                entity.Eventid =record.Eventid;
                entity.Eventdate = record.Eventdate;


                var custrecords = db.EventCustomers.Where(x => x.Evententryid == id).ToList();
                List<EventCustomerentity> eventcustlist = new List<EventCustomerentity>();
                if (custrecords != null)
                {
                    if (custrecords.Count > 0)
                    {
                        foreach (var item in custrecords)
	                     {
		 
	
                        EventCustomerentity eventcustentity = new EventCustomerentity();
                        eventcustentity.Eventcustomerid = item.Eventcustomerid;
                        UserEntity userentity = new UserEntity();
                            if(item.UserMaster!=null)
                            {
                                userentity.Id = item.UserMaster.Id;
                                userentity.Fullname = item.UserMaster.Fullname;
                                eventcustentity.UserMaster = userentity;
                            }
                        eventcustentity.Evententryid = item.Evententryid;
                        eventcustentity.Paidamount = item.Paidamount;
                        eventcustlist.Add(eventcustentity);

                        }
                    }
                    entity.customerlist = eventcustlist;

                }
                



            }
            return entity;
        }

        

    }
}