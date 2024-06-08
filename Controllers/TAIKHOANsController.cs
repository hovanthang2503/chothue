using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using chothuexe1.Models;

namespace chothuexe1.Controllers
{
    public class TAIKHOANsController : Controller
    {
        private DoAnPhanMemEntities db = new DoAnPhanMemEntities();

        // GET: TAIKHOANs
        public ActionResult Index()
        {
            return View(db.TAIKHOANs.ToList());
        }


        //Dang ky
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(TAIKHOAN taikhoan)
        {
            if (ModelState.IsValid)
            {
                if (db.TAIKHOANs.Any(x => x.TenDangNhap == taikhoan.TenDangNhap))
                {
                    ModelState.AddModelError("TenDangNhap", "Tên đăng nhập đã tồn tại.");
                }
                else
                {
                    db.TAIKHOANs.Add(taikhoan);
                    db.SaveChanges();

                    Session["tendangnhap"] = taikhoan.TenDangNhap.ToString();
                    return RedirectToAction("Index", "Home");
                }
            }

            // Nếu ModelState không hợp lệ, trả về view với dữ liệu và lỗi.
            return View(taikhoan);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TAIKHOAN taikhoan)
        {
            var user = db.TAIKHOANs.Where(x => x.SDT.Equals(taikhoan.SDT) && x.MatKhau.Equals(taikhoan.MatKhau)).FirstOrDefault();
            if (user != null)
            {
                // Lưu họ và tên vào Session
                Session["HoTen"] = user.HoVaTen;
                Session["MaTK"] = user.MaTK;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notification = "Sai số điện thoại hoặc mật khẩu";
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

