using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using chothuexe1.Models;

namespace chothuexe1.Controllers
{
    public class HOADONsController : Controller
    {
        private DoAnPhanMemEntities db = new DoAnPhanMemEntities();
        // GET: HOADONs
        public ActionResult Index()
        {
            int? maTK = Session["MaTK"] as int?;

            if (maTK == null)
            {
                return RedirectToAction("Login", "TAIKHOANs");
            }

            // Truy vấn để lấy các hóa đơn và chi tiết hóa đơn liên quan
            var hoaDons = db.HOADONs
                           .Where(h => h.MaTK == maTK)
                           .Include(h => h.CHITIETHOADONs.Select(c => c.XETHUE))
                           .ToList();

            // Truyền danh sách hóa đơn và chi tiết hóa đơn đến view
            return View(hoaDons);
        }
        // GET: HOADONs/Chitiet/id
        public ActionResult Chitiet(int id)
        {
            // Fetch the invoice based on MaHD
            var hoaDon = db.HOADONs.Include("CHITIETHOADONs").FirstOrDefault(h => h.MaHD == id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }

            return View(hoaDon);
        }

        // POST: HOADONs/GiaHanThueXe
        [HttpPost]
        public ActionResult GiaHanThueXe(FormCollection form)
        {
            int id = int.Parse(form["MaHD"]);
            int extendDays = int.Parse(form["extend-days"]);
            string notes = form["notes"];

            var hoaDon = db.HOADONs.Include("CHITIETHOADONs").FirstOrDefault(h => h.MaHD == id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }

            // Cập nhật ngày bắt đầu thuê trong bảng CHITIETHOADON
            foreach (var chiTiet in hoaDon.CHITIETHOADONs)
            {
                chiTiet.ThoiGianThue += extendDays;
                db.Entry(chiTiet).State = EntityState.Modified;
            }

           

            db.SaveChanges();
            return RedirectToAction("Chitiet", new { id = id });
        }
    }
}