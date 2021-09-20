using Model.EF;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using PagedList;
using System;

namespace Model.DAO
{
    public class AccountDAO
    {
        WebShopDbContext db = null;

        public AccountDAO()
        {
            db = new WebShopDbContext();
        }

        public long Insert(Account entity)
        {
            db.Account.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Account entity)
        {
            try
            {
                var account = db.Account.Find(entity.ID);
                account.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    account.Password = entity.Password;
                }
                account.Address = entity.Address;
                account.Email = entity.Email;
                account.Phone = entity.Phone;
                account.ModifiedBy = entity.ModifiedBy;
                account.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch
            { 
                //logging
                return false; 
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var account = db.Account.Find(id);
                db.Account.Remove(account);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Account> ListAllPaging(int page, int pageSize)
        {

            return db.Account.OrderByDescending(x=>x.CreatedDate).ToPagedList(page, pageSize);
        }

        public Account GetByID(string userName)
        {
            return db.Account.SingleOrDefault(x => x.UserName == userName);
        }

        public Account ViewDetail(int id)
        {
            return db.Account.Find(id);
        }

        public int Login(string userName, string passWord)
        {
            var result = db.Account.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == passWord)
                        return 1;
                    else
                        return -2;
                }
            }
        }
    }
}
