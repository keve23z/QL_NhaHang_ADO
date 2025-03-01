using QL_NhaHang_ADO.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_NhaHang_ADO.Controllers
{
    public class DanhGiaController : Controller
    {
        // GET: DanhGia
        private ConnectDanhGia danhGiaRepo = new ConnectDanhGia();

        // GET: DanhGia
        public ActionResult ShowDanhGia()
        {
            var danhGiaList = danhGiaRepo.GetData();
            if (danhGiaList == null || !danhGiaList.Any())
            {
                TempData["ErrorMessage"] = "Hiện chưa có đánh giá nào.";
            }

            return View(danhGiaList);
        }

        // GET: DanhGia/Delete/5
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "ID không hợp lệ.");
            }

            var danhGia = danhGiaRepo.GetData().FirstOrDefault(dg => dg.MaDGia.Trim() == id.Trim());
            if (danhGia == null)
            {
                return HttpNotFound("Không tìm thấy đánh giá với ID được cung cấp.");
            }

            return View(danhGia);
        }

        // POST: DanhGia/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["ErrorMessage"] = "ID không hợp lệ.";
                return RedirectToAction("ShowDanhGia");
            }

            try
            {
                int result = danhGiaRepo.DeleteDanhGia(id.Trim());
                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Xóa đánh giá thành công.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể xóa đánh giá. Đánh giá có thể đã bị xóa hoặc không tồn tại.";
                }
            }
            catch (SqlException ex)
            {
                TempData["ErrorMessage"] = $"Lỗi cơ sở dữ liệu: {ex.Message}";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi hệ thống: {ex.Message}";
            }

            return RedirectToAction("ShowDanhGia");
        }

        // Thêm phương thức GET cho DeleteConfirmed để xử lý trường hợp người dùng truy cập URL trực tiếp
        [HttpGet]
        public ActionResult DeleteConfirmedGet(string id)
        {
            TempData["ErrorMessage"] = "Phương thức không hợp lệ. Vui lòng sử dụng form xóa.";
            return RedirectToAction("ShowDanhGia");
        }
    }
}