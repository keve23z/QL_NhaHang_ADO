using QL_NhaHang_ADO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_NhaHang_ADO.Controllers
{
    public class Admin_NguyenLieuController : Controller
    {
        // GET: Admin_NguyenLieu
        public ActionResult HienThiNguyenLieu()
        {
            XuLyNguyenLieu objHD = new XuLyNguyenLieu();
            var danhSachNguyenLieu = objHD.LayThongTinNguyenLieu();
            return View(danhSachNguyenLieu);
        }
    }
}