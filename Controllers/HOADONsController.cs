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
            return View(db.CHITIETHOADONs.ToList());
        }

        // GET: HOADONs/Edit/5?maxe=10
        public ActionResult Chitiet(int? id, int? maxe)
        {
            if (id == null || maxe == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CHITIETHOADON chiTietHoaDon = db.CHITIETHOADONs.FirstOrDefault(c => c.MaHD == id && c.MaXe == maxe);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }

            return View(chiTietHoaDon);
        }

    }
}