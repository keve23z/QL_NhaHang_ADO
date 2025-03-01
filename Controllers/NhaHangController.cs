using QL_NhaHang_ADO.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_NhaHang_ADO.Controllers
{
    public class NhaHangController : Controller
    {
        // GET: NhaHang
        public SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connect1"].ConnectionString);

        public ActionResult ShowHD(string ngayBD = null, string ngayKT = null)
        {
            ConnectHoaDon connectHoaDon = new ConnectHoaDon();
            List<HoaDon> hoaDons = connectHoaDon.GetData();

            // Nếu có ngày bắt đầu và ngày kết thúc, thực hiện lọc
            if (!string.IsNullOrEmpty(ngayBD) && !string.IsNullOrEmpty(ngayKT))
            {
                DateTime startDate = DateTime.Parse(ngayBD);
                DateTime endDate = DateTime.Parse(ngayKT).AddDays(1).AddTicks(-1); // Chuyển ngày kết thúc thành cuối ngày

                hoaDons = hoaDons
                            .Where(hd => hd.NgayLap >= startDate && hd.NgayLap <= endDate)
                            .ToList();
            }


            // Truyền số lượng và danh sách hóa đơn vào View
            ViewBag.SL = hoaDons.Count;
            return View(hoaDons);
        }

        [HttpPost]
        public ActionResult ShowHD(string ma)
        {
            if (string.IsNullOrEmpty(ma))
            {
                return RedirectToAction("Error");
            }

            return RedirectToAction("ShowCTHD", new { ma = ma });
        }
        public ActionResult ShowCTHD(string ma)
        {
            if (string.IsNullOrEmpty(ma))
            {
                return RedirectToAction("Error");
            }
            // lấy thông tin khách hàng theo mã hóa đơn

            ConnectHoaDon connectHoaDon = new ConnectHoaDon();
            ConnectDanhGia connectDanhGia = new ConnectDanhGia();
            DanhGia DG = connectDanhGia.LayDanhGiaTuMaHD(ma);
            KhachHang khach = new KhachHang();
            HoaDon hoaDon = connectHoaDon.GetHoaDonByMa(ma);
            khach = connectHoaDon.LayThongTinKHTheoMaHD(ma);
            List<ChiTietHoaDon> chiTiets = connectHoaDon.ListChiTiet(ma);
            ViewBag.DanhGia = DG;
            ViewBag.HoaDon = hoaDon;
            ViewBag.Khach = khach;
            ViewBag.MaHD = ma;
            ViewBag.Tong = chiTiets.Sum(t => t.ThanhTien);
            return View(chiTiets);
        }
        public string MaHDNew(List<HoaDon> hoaDons)
        {
            if (hoaDons == null || hoaDons.Count == 0)
            {

                return "HD001";
            }
            string MaMax = hoaDons.OrderByDescending(hd => hd.Ma).First().Ma;
            int nextNumber = int.Parse(MaMax.Substring(3)) + 1;
            MaMax = $"HD{nextNumber.ToString("D3")}";
            return MaMax;
        }
        public ActionResult ThemHD()
        {
            ConnectHoaDon connectHoaDon = new ConnectHoaDon();
            List<HoaDon> hoaDons = connectHoaDon.GetData();  // Lấy danh sách hóa đơn từ cơ sở dữ liệu

            // Tạo mã hóa đơn mới từ danh sách đã lấy
            string maHD = MaHDNew(hoaDons);

            // Gán mã hóa đơn mới vào ViewBag để hiển thị trong View
            ViewBag.MaHD = maHD;

            return View();
        }

        [HttpPost]
        public ActionResult ThemHD(string MaHD, string MaNV, string MaKH, DateTime NgayLap)
        {
            ConnectHoaDon connectHoaDon = new ConnectHoaDon();

            // Kiểm tra mã nhân viên
            if (!CheckNhanVienExists(MaNV))
            {
                TempData["ErrorMessage"] = "Mã nhân viên không tồn tại!";
                ViewBag.MaHD = MaHD; // Giữ lại mã hóa đơn cũ khi có lỗi
                return View();
            }

            // Kiểm tra mã khách hàng
            if (!CheckKhachHangExists(MaKH))
            {
                TempData["ErrorMessage"] = "Mã khách hàng không tồn tại!";
                ViewBag.MaHD = MaHD; // Giữ lại mã hóa đơn cũ khi có lỗi
                return View();
            }

            // Thực hiện thêm hóa đơn vào CSDL
            bool isAdded = connectHoaDon.AddHoaDon(MaHD, MaNV, MaKH, NgayLap);

            if (isAdded)
            {
                TempData["SuccessMessage"] = "Thêm hóa đơn thành công!";
                return RedirectToAction("ThemHD");
            }
            else
            {
                TempData["ErrorMessage"] = "Thêm hóa đơn thất bại!";
                ViewBag.MaHD = MaHD; // Giữ lại mã hóa đơn cũ khi có lỗi
                return View();
            }
        }



        private bool CheckKhachHangExists(string MaKH)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connect1"].ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM KhachHang WHERE MAKH = @MaKH";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", MaKH);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;  // Nếu có ít nhất 1 khách hàng với mã đó thì tồn tại
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }





        // Kiểm tra xem mã nhân viên có tồn tại trong bảng NhanVien không
        private bool CheckNhanVienExists(string maNV)
        {
            if (string.IsNullOrEmpty(maNV)) return true; // Nếu không có mã nhân viên thì không cần kiểm tra

            string query = "SELECT COUNT(1) FROM NhanVien WHERE MANV = @MANV";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MANV", maNV);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                conn.Close();
                return count > 0;
            }
        }

        // Kiểm tra xem mã bàn có tồn tại trong bảng Ban không
        private bool CheckBanExists(string maBan)
        {
            if (string.IsNullOrEmpty(maBan)) return true; // Nếu không có mã bàn thì không cần kiểm tra

            string query = "SELECT COUNT(1) FROM Ban WHERE MABAN = @MABAN AND TRANGTHAI = N'Trống'";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MABAN", maBan);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                conn.Close();
                return count > 0;
            }
        }

        // Kiểm tra xem mã giảm giá có tồn tại và đủ số lượng hay không
        private bool CheckGiamGiaExists(string maGiamGia)
        {
            if (string.IsNullOrEmpty(maGiamGia)) return true; // Nếu không có mã giảm giá thì không cần kiểm tra

            string query = "SELECT COUNT(1) FROM GiamGia WHERE MAGIAMGIA = @MAGIAMGIA AND SOLUONG > 0";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MAGIAMGIA", maGiamGia);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                conn.Close();
                return count > 0;
            }
        }

        // Phương thức thêm hóa đơn vào cơ sở dữ liệu
        public ActionResult Edit(string id)
        {
            // Lấy thông tin hóa đơn
            ConnectHoaDon connectHoaDon = new ConnectHoaDon();
            HoaDon model = connectHoaDon.GetHoaDonByMa(id);
            return View(model);
        }

        public ActionResult Temp()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Edit(HoaDon model)
        {
            // Kiểm tra xem maHD có null không
            string a = model.Ma;

            if (string.IsNullOrEmpty(a.Trim()))
            {
                TempData["ErrorMessage"] = "Mã hóa đơn không hợp lệ!";
                return RedirectToAction("ShowHD"); // Hoặc quay lại trang danh sách nếu cần
            }

            // Kiểm tra tính hợp lệ của mô hình
            if (!ModelState.IsValid)
            {
                // Ghi lại các lỗi xác thực
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Debug.WriteLine(error.ErrorMessage);
                }

                // Trả lại view với các lỗi xác thực
                return View(model);
            }


            DateTime ngayLap = model.NgayLap ?? DateTime.MinValue;

            ConnectHoaDon connectHoaDon = new ConnectHoaDon();
            bool isUpdated = connectHoaDon.UpdateHoaDon(model.Ma, model.MaBan, model.MaKH, model.MaNV, model.MaGiamGia, ngayLap, model.HinhThuc);

            // Kiểm tra kết quả cập nhật
            if (isUpdated)
            {
                TempData["SuccessMessage"] = "Cập nhật hóa đơn thành công!";
                return RedirectToAction("ShowHD");
            }
            else
            {
                TempData["ErrorMessage"] = "Cập nhật hóa đơn thất bại!";
                return RedirectToAction("Edit", new { id = model.Ma });
            }
        }

        public ActionResult ListTimKiem(string txt_Search)
        {
            ConnectHoaDon connectHoaDon = new ConnectHoaDon();
            List<HoaDon> hoaDons = connectHoaDon.Search(txt_Search);
            ViewBag.SL = hoaDons.Count;
            return View(hoaDons);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}