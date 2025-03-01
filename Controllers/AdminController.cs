using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_NhaHang_ADO.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult HienThiAdmin()
        {
            return View();
        }
        public ActionResult NhapKho()
        {
            return View();
        }
    }
}