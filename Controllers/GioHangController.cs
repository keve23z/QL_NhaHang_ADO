using QL_NhaHang_ADO.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace QL_NhaHang_ADO.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        public ActionResult HienThiGioHang()
        {
            var cart = Session["Cart"] as List<GioHang> ?? new List<GioHang>();
            return View(cart);
        }


        [HttpPost]
        public JsonResult AddToCart(string maMonAn, string tenMonAn, double donGia, string anhMon, int soLuong = 1)
        {
            // Lấy giỏ hàng từ Session hoặc khởi tạo mới nếu chưa có
            List<GioHang> cart = Session["Cart"] as List<GioHang> ?? new List<GioHang>();

            // Kiểm tra món ăn đã tồn tại trong giỏ hàng bằng cách duyệt danh sách
            GioHang existingItem = null;
            foreach (var item in cart)
            {
                if (item.MaMonAn == maMonAn)
                {
                    existingItem = item;
                    break; // Thoát khỏi vòng lặp khi tìm thấy phần tử
                }
            }

            if (existingItem != null)
            {
                // Nếu món ăn đã tồn tại, tăng số lượng
                existingItem.SoLuong += soLuong;
            }
            else
            {
                // Nếu chưa tồn tại, thêm mới vào giỏ hàng
                GioHang newItem = new GioHang
                {
                    MaMonAn = maMonAn,
                    TenMonAn = tenMonAn,
                    DonGia = donGia,
                    AnhMon = anhMon,
                    SoLuong = soLuong

                };
                cart.Add(newItem);
            }

            // Cập nhật lại giỏ hàng vào Session
            Session["Cart"] = cart;
            var cartCount = Session["Cart"] as List<GioHang> ?? new List<GioHang>();
            var count = cartCount.Count;

            // Chuyển hướng đến trang Index
            return Json(new { success = true, cartCount = cart.Count });
        }

        public ActionResult GetCartItems()
        {
            var cart = Session["Cart"] as List<GioHang> ?? new List<GioHang>();
            return PartialView("CartItemsPartial", cart);
        }

        [HttpPost]
        public JsonResult CapNhatSoLuong(string maMonAn, int soLuong)
        {
            try
            {
                // Lấy giỏ hàng từ Session
                List<GioHang> cart = Session["Cart"] as List<GioHang> ?? new List<GioHang>();

                // Tìm món ăn trong giỏ hàng
                var item = cart.FirstOrDefault(x => x.MaMonAn == maMonAn);
                if (item != null)
                {
                    if (soLuong > 0)
                    {
                        item.SoLuong = soLuong;
                    }
                    else
                    {
                        // Nếu số lượng <= 0, xóa món ăn khỏi giỏ hàng
                        cart.Remove(item);
                    }
                }

                // Cập nhật giỏ hàng vào Session
                Session["Cart"] = cart;

                // Tính toán lại tổng tiền
                double tongTien = cart.Sum(x => x.SoLuong * x.DonGia);

                return Json(new { success = true, tongTien = tongTien });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult TangSoLuong(string maMonAn)
        {
            // Lấy giỏ hàng từ Session
            List<GioHang> cart = Session["Cart"] as List<GioHang> ?? new List<GioHang>();

            // Kiểm tra món ăn có trong giỏ hàng không
            var item = cart.FirstOrDefault(x => x.MaMonAn == maMonAn);
            if (item != null)
            {
                // Tăng số lượng trong danh sách
                item.SoLuong++;

            }

            // Lưu lại giỏ hàng trong Session
            Session["Cart"] = cart;

            // Tính toán tổng số lượng và tổng tiền
            var totalQuantity = cart.Sum(x => x.SoLuong);
            var totalPrice = cart.Sum(x => x.SoLuong * x.DonGia);

            return Json(new { totalQuantity, totalPrice });

        }

        [HttpPost]
        public JsonResult GiamSoLuong(string maMonAn)
        {

            // Lấy giỏ hàng từ Session
            List<GioHang> cart = Session["Cart"] as List<GioHang> ?? new List<GioHang>();

            // Kiểm tra món ăn trong giỏ hàng
            var item = cart.FirstOrDefault(x => x.MaMonAn == maMonAn);
            if (item != null)
            {
                item.SoLuong--;

                if (item.SoLuong <= 1)
                {
                    // Xóa sản phẩm nếu số lượng <= 0
                    item.SoLuong = 1;
                }
            }

            // Lưu lại giỏ hàng trong Session
            Session["Cart"] = cart;

            // Tính toán tổng số lượng và tổng tiền
            var totalQuantity = cart.Sum(x => x.SoLuong);
            var totalPrice = cart.Sum(x => x.SoLuong * x.DonGia);

            return Json(new { totalQuantity, totalPrice });
        }
        [HttpPost]
        public JsonResult RemoveItem(string maMonAn)
        {
            // Lấy giỏ hàng từ Session
            List<GioHang> cart = Session["Cart"] as List<GioHang> ?? new List<GioHang>();

            // Xóa sản phẩm khỏi danh sách trong Session
            var item = cart.FirstOrDefault(x => x.MaMonAn == maMonAn);
            if (item != null)
            {
                cart.Remove(item);

            }
            // Lưu lại danh sách vào Session
            Session["Cart"] = cart;

            // Tính toán lại tổng số lượng và tổng tiền
            var totalQuantity = cart.Sum(x => x.SoLuong);
            var totalPrice = cart.Sum(x => x.SoLuong * x.DonGia);

            return Json(new { totalQuantity, totalPrice });
        }
        
    [HttpPost]
        public ActionResult HienThiHoaDonVaQRThanhToan(FormCollection f)
        {
            if (Session["MaTaiKhoan"] == null)
                return RedirectToAction("DangNhap", "Account");
            // Lấy giỏ hàng từ Session
            List <GioHang> cart = Session["Cart"] as List<GioHang> ?? new List<GioHang>();

            // Tính toán tổng số lượng và tổng tiền
            var totalQuantity = cart.Sum(x => x.SoLuong);
            var totalPrice = cart.Sum(x => x.SoLuong * x.DonGia);
            XuLyGiamGia objGiamGia = new XuLyGiamGia();
            var giaGiam = totalPrice - objGiamGia.LayGiaTriGiamGia(f["MaGiamGia"]);
            // Tạo hóa đơn
            HoaDon hd = new HoaDon
            {
                Ma = GenerateMaHoaDon(),
                MaBan = null,
                MaKH = Session["MaKH"]?.ToString(),
                TenKhachHang = Session["HoTen"]?.ToString(),
                MaNV = null,
                MaGiamGia = f["MaGiamGia"],
                GiaGiam = (int)giaGiam,
                NgayLap = DateTime.Now,
                HinhThuc = "Chuyển khoản"
            };

            // Tạo danh sách chi tiết hóa đơn
            List<ChiTietHoaDon> chiTietHoaDonList = new List<ChiTietHoaDon>();
            foreach (var item in cart)
            {
                ChiTietHoaDon cthd = new ChiTietHoaDon
                {
                    MaHD = hd.Ma,
                    MaMA = item.MaMonAn,
                    TenMon = item.TenMonAn,
                    SoLuong = item.SoLuong,
                    GiaMon = (int)item.DonGia,
                    ThanhTien = (int)(item.SoLuong * item.DonGia),
                };
                chiTietHoaDonList.Add(cthd);
            }

            // Tạo ViewModel
            ViewThanhToan view = new ViewThanhToan
            {

                HoaDon = hd,
                ChiTietHoaDonList = chiTietHoaDonList,
                TongTien = totalPrice,
                SoTienSauGiamGia = (int)giaGiam
            };

            // Xóa giỏ hàng sau khi thanh toán
            Session["Cart"] = null;

            // Trả về view với ViewModel
            return View(view);
        }

        [HttpPost]
        public ActionResult DaThanhToan(FormCollection f)
        {
            // Lấy thông tin từ form
            string maHoaDon = f["MaHoaDon"];
            string maKH = f["MaKH"];
            string tongTien = f["TongTien"];
            string hinhThuc = f["HinhThuc"];
            string maGiamGia = f["MaGiamGia"];
            string giaGiam = f["SauGiam"];
            string[] strings = f.GetValues("ChiTietHoaDon[0].MaMon");
            string[] strings1 = f.GetValues("ChiTietHoaDon[0].SoLuong");
            string[] strings2 = f.GetValues("ChiTietHoaDon[0].GiaMon");
            string[] strings3 = f.GetValues("ChiTietHoaDon[0].ThanhTien");

            // Tạo danh sách chi tiết hóa đơn
            List<ChiTietHoaDon> chiTietHoaDonList = new List<ChiTietHoaDon>();
            for (int i = 0; i < strings.Length; i++)
            {
                ChiTietHoaDon cthd = new ChiTietHoaDon
                {
                    MaHD = maHoaDon,
                    MaMA = strings[i],
                    SoLuong = int.Parse(strings1[i]),
                    GiaMon = int.Parse(strings2[i]),
                    ThanhTien = int.Parse(strings3[i]),
                };
                chiTietHoaDonList.Add(cthd);
            }
            // Tạo hóa đơn
            HoaDon hd = new HoaDon
            {
                Ma = maHoaDon,
                MaBan = null,
                MaKH = maKH,
                TenKhachHang = null,
                MaNV = null,
                TongTien = int.Parse(tongTien),
                // nếu không có mã giảm giá thì gán No
                MaGiamGia = string.IsNullOrEmpty(maGiamGia) ? "No" : maGiamGia,
                NgayLap = DateTime.Now,
                HinhThuc = hinhThuc,
                GiaGiam = int.Parse(giaGiam)
            };
            XuLyGiamGia xuLyGiamGia = new XuLyGiamGia();
            xuLyGiamGia.GiamSoLuongMaKhiThanhToan(maGiamGia);
            // Thêm hóa đơn vào cơ sở dữ liệu
            Session["DiemThanhVien"] =  (int)Session["DiemThanhVien"] + hd.GiaGiam * 0.01;
            XuLyThanhToan objHD = new XuLyThanhToan();
            objHD.XuLyHoaDon(chiTietHoaDonList, hd);

            // xóa giỏ hàng sau khi thanh toán
            Session["Cart"] = null;
            TempData["Cart"] = 0;
            return RedirectToAction("Welcome", "Account");
        }
        [HttpPost]
        public JsonResult KiemTraMa(string discountCode)
        {
            XuLyGiamGia objGiamGia = new XuLyGiamGia();
            bool result = objGiamGia.KiemTraMaGiamGia(discountCode);
            return Json(result);
        }
        public int GetNextNumberFromDatabase()
        {
            int nextNumber = 1; // Giá trị mặc định nếu không có dữ liệu trong bảng
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connect1"].ConnectionString; // Chuỗi kết nối tới Oracle

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Truy vấn số thứ tự lớn nhất hiện có
                string query = "SELECT MAX(CAST(SUBSTRING(MaHoaDon, 3, LEN(MaHoaDon) - 2) AS INT)) AS MaxValue FROM HoaDon;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    var result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        nextNumber = Convert.ToInt32(result) + 1; // Số tiếp theo
                    }
                }
            }

            return nextNumber;
        }
        public string GenerateMaHoaDon()
        {
            string prefix = "HD";
            int nextNumber = GetNextNumberFromDatabase(); // Hàm để lấy số tiếp theo từ CSDL
            string maTaiKhoan = prefix + nextNumber.ToString("D4"); // Format thành 'TK0001'
            return maTaiKhoan;
        }
    }
}
