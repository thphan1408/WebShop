using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;
using WebShop.Common;
using PagedList;

namespace WebShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var dao = new AccountDAO();
            var model = dao.ListAllPaging(page, pageSize);
            return View(model);
        }

        // Thêm mới user

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Account account)
        {
            if (ModelState.IsValid)
            {
                var dao = new AccountDAO();
                
                //Mã hóa password được thêm
                var encryptedMD5Pas = Encryptor.MD5Hash(account.Password);
                account.Password = encryptedMD5Pas;

                long id = dao.Insert(account);
                if (id > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user thành công.");
                }
            }
            return View("Index");
        }

        // Cập nhật mới user

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var account = new AccountDAO().ViewDetail(id);
            return View(account);
        }

        [HttpPost]
        public ActionResult Edit(Account account)
        {
            if (ModelState.IsValid)
            {
                var dao = new AccountDAO();

                if(!string.IsNullOrEmpty(account.Password))
                {
                    //Mã hóa password được thêm
                    var encryptedMD5Pas = Encryptor.MD5Hash(account.Password);
                    account.Password = encryptedMD5Pas;
                }

                var result = dao.Update(account);
                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật user thành công.");
                }
            }
            return View("Index");
        }

        //Xóa user

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new AccountDAO().Delete(id);
            return RedirectToAction("Index");
        }

    }
}