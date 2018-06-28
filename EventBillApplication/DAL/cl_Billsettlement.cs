using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventBillApplication.Models;
using EventBillApplication.Database;


namespace EventBillApplication.DAL
{
    public class cl_Billsettlement
    {


        public decimal getbalance(int userid,int creditbyid)
        {
            decimal balance = 0;
            using (var db = new EventDBEntities())
            {
                decimal cerditbalance=0;
                decimal debitbalance = 0;
                var havecreditamt = db.CreditAmountDetails.Where(x => x.IsActive == true && x.Creditedto == userid && x.Creditedfrom == creditbyid).Sum(x => x.Creditamount).HasValue;
                if (havecreditamt)
                    cerditbalance = db.CreditAmountDetails.Where(x => x.IsActive == true && x.Creditedto == userid && x.Creditedfrom == creditbyid).Sum(x => x.Creditamount).Value;

                var havetotalpaid = db.BillSettlementmasters.Where(x => x.IsActive == true && x.Paidby == userid && x.Creditedfrom == creditbyid).Sum(x => x.Paidamount).HasValue;
                if (havetotalpaid)
                {
                    debitbalance = db.BillSettlementmasters.Where(x => x.IsActive == true && x.Paidby == userid && x.Creditedfrom == creditbyid).Sum(x => x.Paidamount).Value;
                }
                balance = cerditbalance - debitbalance;

            }
            return balance;
        }

        public List<Billsettlemententity> Get()
        {
            List<Billsettlemententity> list = new List<Billsettlemententity>();
            using (var db = new EventDBEntities())
            {
                var records = db.BillSettlementmasters.ToList().Where(x => x.IsActive == true).ToList();
                foreach (var item in records)
                {
                    Billsettlemententity entity = new Billsettlemententity();
                    entity.Id = item.Id;
                    entity.Paidamount = item.Paidamount;
                    entity.Paidby = item.Paidby;
                    entity.Entrydate = item.Entrydate;
                    list.Add(entity);

                }

            }
            return list;
        }

        public bool Insert(Billsettlemententity entity)
        {
            bool inserted = false;
            using (var db = new EventDBEntities())
            {
                BillSettlementmaster record = new BillSettlementmaster();
                record.Entrydate = entity.Entrydate;
                record.Creditedfrom = entity.Creditedfrom;
                record.Creditamount = entity.Creditamount;
                record.Paidamount = entity.Paidamount;
                record.Paidby = entity.Paidby;
                record.IsActive = true;
                db.BillSettlementmasters.Add(record);
                db.SaveChanges();
                inserted = true;


            }
            return inserted;
        }


        public bool Delete(long id)
        {
            bool deleted = false;
            using (var db = new EventDBEntities())
            {
                var record = db.BillSettlementmasters.Find(id);
                record.IsActive = false;
                db.SaveChanges();
                deleted = true;


            }
            return deleted;
        }

        public Billsettlemententity getbyid(long id)
        {
            Billsettlemententity entity = new Billsettlemententity();
            using (var db = new EventDBEntities())
            {
                var record = db.BillSettlementmasters.Find(id);

                entity.Entrydate = record.Entrydate;
                entity.Creditedfrom = record.Creditedfrom;
                entity.Creditamount = record.Creditamount;
                entity.Paidamount = record.Paidamount;
                entity.Paidby = record.Paidby;

            }
            return entity;
        }


    }
}