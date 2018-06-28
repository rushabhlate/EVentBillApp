using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventBillApplication.Models;
using EventBillApplication.Database;
namespace EventBillApplication.DAL
{
    public class cl_Event
    {

        public List<EventEntity> Get()
        {
            List<EventEntity> list = new List<EventEntity>();
            using (var db = new EventDBEntities())
            {
                var records = db.EventMasters.Where(x=>x.IsActive==true).ToList();
                foreach (var item in records)
                {
                    EventEntity entity = new EventEntity();
                    entity.Id = item.Id;
                    entity.Eventname = item.Eventname;
                   


                    list.Add(entity);

                }


            }
            return list;
        }

        public bool Insert(EventEntity entity)
        {
            bool isinserted = false;
            using (var db = new EventDBEntities())
            {
                EventMaster record = new EventMaster();
                record.Id = entity.Id;
                record.Eventname = entity.Eventname;
                record.Createddate = entity.Createddate;
                record.IsActive = true;
                record.Createdby = entity.Createdby;
                db.EventMasters.Add(record);
                db.SaveChanges();
                isinserted = true;


            }

            return isinserted;

        }

        public bool Update(EventEntity entity)
        {
            bool isupdated = false;
            using (var db = new EventDBEntities())
            {
                var record = db.EventMasters.Find(entity.Id);
                record.Eventname = entity.Eventname;
                record.Updatedate = entity.Updatedate;
                record.Updatedby = entity.Updatedby;
                db.SaveChanges();
                isupdated = true;


            }
            return isupdated;
        }


        public bool Delete(int id)
        {
            bool isdeleted = false;
            using (var db = new EventDBEntities())
            {
                var record = db.EventMasters.Find(id);
                record.IsActive = false;
                db.SaveChanges();
                isdeleted = true;


            }
            return isdeleted;

        }

        public EventEntity Getbyid(int id)
        {
            EventEntity entity = new EventEntity();
            using (var db = new EventDBEntities())
            {
                var record = db.EventMasters.Find(id);
                entity.Eventname = record.Eventname;
               

            }
            return entity;

        }

    }
}