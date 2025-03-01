using QL_NhaHang_ADO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_NhaHang_ADO.Controllers
{
    public class MonAnController : Controller
    {
        // GET: MonAn
        public ActionResult DanhSachMonAn()
        {
            XuLyThongTinMonAn objMA = new XuLyThongTinMonAn();
            List<MonAn> listMA = objMA.LayThongTinMonAn();
            return View(listMA);
        }
        public ActionResult Menu()
        {
            return View();
        }
    }
}