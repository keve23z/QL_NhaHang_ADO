using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

using QL_NhaHang_ADO.Models;

namespace QL_NhaHang_ADO.Controllers
{
    public class Admin_HoaDonController : Controller
    {
        // GET: Admin_HoaDon
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HienThiHoaDon()
        {
            XuLyHoaDon objHD = new XuLyHoaDon();
            List<PhieuNhapKho> listHD = objHD.LayThongTinHoaDon();
            return View(listHD);
        }

        public string GeneratePhieuNhapKho()
        {
            string prefix = "PNK";
            int nextNumber = GetNextNumberFromDatabase(); // Hàm để lấy số tiếp theo từ CSDL
            string maNhaoKho = prefix + nextNumber.ToString("D4"); // Format thành 'TK0001'

            return maNhaoKho;
        }
        public int GetNextNumberFromDatabase()
        {
            int nextNumber = 1; // Giá trị mặc định nếu không có dữ liệu trong bảng
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connect1"].ConnectionString; // Chuỗi kết nối tới Oracle

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Truy vấn số thứ tự lớn nhất hiện có
                string query = "SELECT MAX(CAST(SUBSTRING(MANHAPKHO, 4, LEN(MANHAPKHO) - 3) AS INT)) AS MaxValue FROM PhieuNhapKho;";

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
        public ActionResult ThemHoaDon()
        {
            string maPhieuNhap = GeneratePhieuNhapKho(); // Gọi hàm để lấy giá trị
            ViewBag.MaPhieuNhap = maPhieuNhap;
            XuLyNguyenLieu objHD = new XuLyNguyenLieu();      
            var danhSachNguyenLieu = objHD.LayThongTinNguyenLieu(); // Gọi phương thức lấy danh sách nguyên liệu
            // Truyền dữ liệu vào View
            return View(danhSachNguyenLieu);
        }

        [HttpPost]
        public ActionResult ThemHoaDonMoi(FormCollection f)
        {
            PhieuNhapKho hd = new PhieuNhapKho();
            hd.MaNhapKho = f["MaNhapKho"];
            hd.MaNV = f["MaNV"];
            hd.NgayNhapKho = DateTime.Parse(f["NgayNhapKho"]);
            hd.TongTien = 0;

            XuLyHoaDon objHD = new XuLyHoaDon();
            objHD.XuLyThemHoaDon(hd);

            // Lưu ID của hóa đơn vừa tạo vào ViewBag để hiển thị form nhập chi tiết
            ViewBag.HoaDonId = hd.MaNhapKho;

            // Trả về view lại với ViewBag chứa HoaDonId
            return View("ThemHoaDon");
        }


        [HttpPost]
        public ActionResult ThemChiTietHoaDon(FormCollection f)
        {
            string MaNhapKho = f["MaNhapKho"]; // Lấy MaNhapKho từ form
            if (string.IsNullOrEmpty(MaNhapKho))
            {
                // Kiểm tra nếu MaNhapKho là null hoặc rỗng, có thể thêm xử lý lỗi ở đây
                return RedirectToAction("Error"); // hoặc hiển thị thông báo lỗi
            }

            string[] MaNguyenLieu = f.GetValues("ChiTietHoaDon[][MaNguyenLieu]");
            string[] TenNguyenLieu = f.GetValues("ChiTietHoaDon[][TenNguyenLieu]");
            string[] SoLuong = f.GetValues("ChiTietHoaDon[][SoLuong]");
            string[] DVT = f.GetValues("ChiTietHoaDon[][DVT]");
            string[] DonGia = f.GetValues("ChiTietHoaDon[][DonGia]");
            string[] ThanhTien = f.GetValues("ChiTietHoaDon[][ThanhTien]");

            List<ChiTietNhapKho> listCTHD = new List<ChiTietNhapKho>();
            for (int i = 0; i < MaNguyenLieu.Length; i++)
            {
                ChiTietNhapKho cthd = new ChiTietNhapKho
                {
                    MaNhapKho = MaNhapKho,  // Đảm bảo MaNhapKho được gán đúng
                    MaNguyenLieu = MaNguyenLieu[i],
                    SoLuong = int.Parse(SoLuong[i]),
                    ThanhTien = int.Parse(ThanhTien[i])
                };
                listCTHD.Add(cthd);
            }

            XuLyHoaDon objHD = new XuLyHoaDon();
            objHD.XuLyThemChiTietHoaDon(listCTHD);

            return RedirectToAction("HienThiHoaDon");
        }
        public JsonResult GetNguyenLieuDetails(string tenNguyenLieu)
        {
            // Khởi tạo đối tượng xử lý dữ liệu
            XuLyNguyenLieu objHD = new XuLyNguyenLieu();

            try
            {
                // Lấy danh sách nguyên liệu
                List<NguyenLieu> nguyenLieuList = objHD.LayThongTinNguyenLieu();

                // Kiểm tra nếu không có dữ liệu
                if (nguyenLieuList == null || !nguyenLieuList.Any())
                {
                    return Json(new { error = "Không có nguyên liệu nào trong hệ thống." }, JsonRequestBehavior.AllowGet);
                }

                // Tìm nguyên liệu theo tên
                var nguyenLieu = nguyenLieuList.FirstOrDefault(n => n.TenNguyenLieu == tenNguyenLieu);

                if (nguyenLieu != null)
                {
                    // Trả về thông tin nguyên liệu nếu tìm thấy
                    return Json(new
                    {
                        MaNguyenLieu = nguyenLieu.MaNguyenLieu,
                        DVT = nguyenLieu.DVT,
                        DonGia = nguyenLieu.DonGia
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Trả về thông báo nếu không tìm thấy nguyên liệu
                    return Json(new { error = "Nguyên liệu không tìm thấy." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi bất ngờ và trả về thông báo lỗi
                return Json(new { error = $"Lỗi trong quá trình xử lý: {ex.Message}" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult _tableNguyenLieu()
        {
            string maPhieuNhap = GeneratePhieuNhapKho(); // Gọi hàm để lấy giá trị
            ViewBag.MaPhieuNhap = maPhieuNhap;
            XuLyNguyenLieu objHD = new XuLyNguyenLieu();
            var danhSachNguyenLieu = objHD.LayThongTinNguyenLieu(); // Gọi phương thức lấy danh sách nguyên liệu
            // Truyền dữ liệu vào View
            return View(danhSachNguyenLieu);
        }
        private XuLyHoaDon _repository = new XuLyHoaDon();

        public ActionResult chitietNhapKho(string MaNhapKho)
        {
            var chiTietNhapKho = _repository.GetChiTietNhapKho(MaNhapKho);
            return View(chiTietNhapKho);
        }
    }
}

