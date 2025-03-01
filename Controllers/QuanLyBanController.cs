using QL_NhaHang_ADO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_NhaHang_ADO.Controllers
{
    public class QuanLyBanController : Controller
    {
        // GET: QuanLyBan
        public ActionResult ShowBan(int? LocTheoSl = 0, int? LocBanTrong = 0)
        {
            ConnectBan ban = new ConnectBan();
            List<Ban> banList = ban.GetBans(); // Lấy tất cả danh sách bàn

            // Lọc theo sức chứa
            if (LocTheoSl.HasValue && LocTheoSl > 0)
            {
                switch (LocTheoSl)
                {
                    case 1: // 2 chỗ
                        banList = banList.Where(b => b.SucChua == "2").ToList();
                        break;
                    case 2: // 4 chỗ
                        banList = banList.Where(b => b.SucChua == "4").ToList();
                        break;
                    case 3: // 4+ chỗ
                        banList = banList.Where(b => b.SucChua == "4+").ToList();
                        break;
                }
            }

            // Lọc theo trạng thái
            if (LocBanTrong.HasValue && LocBanTrong > 0)
            {
                switch (LocBanTrong)
                {
                    case 1: // Bàn trống
                        banList = banList.Where(b => b.TrangThai == "Trống").ToList();
                        break;
                    case 2: // Bàn đầy
                        banList = banList.Where(b => b.TrangThai == "Đầy").ToList();
                        break;
                }
            }

            return View(banList);
        }


        public ActionResult ShowBanTrong()
        {
            ConnectBan ban = new ConnectBan();
            List<Ban> banList = ban.LoadBanTrong();
            return View(banList);
        }
        public ActionResult ChangeTrangThai(string maBan, string newTrangThai)
        {
            ConnectBan connectBan = new ConnectBan();
            bool isUpdated = connectBan.UpdateTrangThai(maBan, newTrangThai);

            if (isUpdated)
            {
                TempData["SuccessMessage"] = "Cập nhật trạng thái bàn thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Cập nhật trạng thái bàn thất bại!";
            }

            return RedirectToAction("ShowBan");
        }
        public ActionResult ChangeTrangThaiTrong(string maBan, string newTrangThai)
        {
            ConnectBan connectBan = new ConnectBan();
            bool isUpdated = connectBan.UpdateTrangThai(maBan, newTrangThai);

            if (isUpdated)
            {
                TempData["SuccessMessage"] = "Cập nhật trạng thái bàn thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Cập nhật trạng thái bàn thất bại!";
            }

            return RedirectToAction("ShowBanTrong");
        }
    }
}