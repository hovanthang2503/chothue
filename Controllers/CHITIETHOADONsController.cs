using chothuexe1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace chothuexe1.Controllers
{
    public class CHITIETHOADONsController : Controller
    {
        private DoAnPhanMemEntities db = new DoAnPhanMemEntities();
        // GET: CHITIETHOADON
        public ActionResult Index()
        {
            return View(db.CHITIETHOADONs.ToList());
        }
    }
}