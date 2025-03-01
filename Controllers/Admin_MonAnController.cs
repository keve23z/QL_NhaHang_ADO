using QL_NhaHang_ADO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_NhaHang_ADO.Controllers
{
    public class Admin_MonAnController : Controller
    {
        // GET: Admin_MonAn
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DanhSachMonAn(string TenMonCanTim = null)
        {
            XuLyThongTinMonAn objMA = new XuLyThongTinMonAn();
            List<MonAn> listMA = objMA.LayThongTinMonAn();
            if (!String.IsNullOrEmpty(TenMonCanTim))
            {
                listMA = listMA.Where(s => s.TenMon.ToLower().Contains(TenMonCanTim.ToLower())).ToList();

            }
            return View(listMA);
        }


        //        @model IEnumerable<QL_NhaHang_ADO.Models.MonAn>

        //<p>
        //    @Html.ActionLink("Create New", "Create")
        //</p>

        // gọi hàm thêm món ăn từ XuLyThongTinMonAn
        [HttpPost]
        public ActionResult XuLyForm()
        {
            MonAn monAn = new MonAn();
            monAn.MaMonAn = Request.Form["MaMonAn"];
            monAn.TenMon = Request.Form["TenMon"];
            monAn.LoaiMon = Request.Form["LoaiMon"];
            monAn.Gia = float.Parse(Request.Form["Gia"]);
            monAn.AnhMon = Request.Form["TenAnh"];
            monAn.MoTa = Request.Form["MoTa"];
            string action = Request.Form["action"];
            XuLyThongTinMonAn objMA = new XuLyThongTinMonAn();
            switch (action)
            {
                case "Them":
                    // Gọi logic để thêm món ăn
                    objMA.themMonAn(monAn);
                    return RedirectToAction("DanhSachMonAn");
                case "Xoa":
                    // Gọi logic để xóa món ăn
                    objMA.xoaMonAn(monAn);
                    return RedirectToAction("DanhSachMonAn");
                case "Sua":
                    // Gọi logic để sửa món ăn
                    objMA.suaMonAn(monAn);
                    return RedirectToAction("DanhSachMonAn");
                default:
                    return RedirectToAction("DanhSachMonAn");
            }
        }
    }
}