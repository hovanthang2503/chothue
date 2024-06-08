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
        public ActionResult Index(int? matk)
        {
            if (!matk.HasValue || matk <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var chiTietHoaDon = db.CHITIETHOADONs
                .Include(c => c.XETHUE)
                .Include(c => c.HOADON)
                .Where(c => c.HOADON.MaTK == matk)
                .ToList();

            if (chiTietHoaDon == null || !chiTietHoaDon.Any())
            {
                return HttpNotFound();
            }

            return View(chiTietHoaDon);
        }

        // GET: HOADONs/Edit/5
        public ActionResult Chitiet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var hoadon = db.HOADONs.Include(h => h.CHITIETHOADONs)
                                    .FirstOrDefault(h => h.MaHD == id);

            if (hoadon == null)
            {
                return HttpNotFound();
            }

            return View(hoadon);
        }
    }
}