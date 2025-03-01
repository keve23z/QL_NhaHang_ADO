using QL_NhaHang_ADO.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_NhaHang_ADO.Controllers
{
    public class Admin_XuatKhoController : Controller
    {
        // GET: Admin_XuatKho
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HienThiXuatKho()
        {
            XuLyXuatKho objHD = new XuLyXuatKho();
            List<PhieuXuatKho> listHD = objHD.LayThongTinXuatKho();
            return View(listHD);
        }

        public string GenerateXuatKho()
        {
            string prefix = "PXK";
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
                string query = "SELECT MAX(CAST(SUBSTRING(MAXUATKHO, 4, LEN(MAXUATKHO) - 3) AS INT)) AS MaxValue FROM PhieuXuatKho;";

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
        public ActionResult ThemPhieuXuatKho()
        {
            string maPhieuNhap = GenerateXuatKho(); // Gọi hàm để lấy giá trị
            ViewBag.MaPhieuNhap = maPhieuNhap;
            XuLyNguyenLieu objHD = new XuLyNguyenLieu();
            var danhSachNguyenLieu = objHD.LayThongTinNguyenLieu(); // Gọi phương thức lấy danh sách nguyên liệu
            // Truyền dữ liệu vào View
            return View(danhSachNguyenLieu);
        }

        [HttpPost]
        public ActionResult ThemPhieXuatKhoMoi(FormCollection f)
        {
            PhieuXuatKho hd = new PhieuXuatKho();
            hd.MaXuatKho = f["MaNhapKho"];
            hd.MaNV = f["MaNV"];
            hd.NgayXuatKho = DateTime.Parse(f["NgayXuatKho"]);
            hd.TongTien = 0;

            XuLyXuatKho objHD = new XuLyXuatKho();
            objHD.XuLyThemPhieuXuatKho(hd);

            // Lưu ID của hóa đơn vừa tạo vào ViewBag để hiển thị form nhập chi tiết
            ViewBag.HoaDonId = hd.MaXuatKho;

            // Trả về view lại với ViewBag chứa HoaDonId
            return View("ThemPhieuXuatKho");
        }


        [HttpPost]
        public ActionResult ThemChiTietXuatKho(FormCollection f)
        {
            string MaXuatKho = f["MaNhapKho"]; // Lấy MaNhapKho từ form
            if (string.IsNullOrEmpty(MaXuatKho))
            {
                // Kiểm tra nếu MaNhapKho là null hoặc rỗng, có thể thêm xử lý lỗi ở đây
                return RedirectToAction("Error"); // hoặc hiển thị thông báo lỗi
            }

            string[] MaNguyenLieu = f.GetValues("ChiTietXuatKho[][MaNguyenLieu]");
            string[] TenNguyenLieu = f.GetValues("ChiTietXuatKho[][TenNguyenLieu]");
            string[] SoLuong = f.GetValues("ChiTietXuatKho[][SoLuong]");
            string[] DVT = f.GetValues("ChiTietXuatKho[][DVT]");
            string[] DonGia = f.GetValues("ChiTietXuatKho[][DonGia]");
            string[] ThanhTien = f.GetValues("ChiTietXuatKho[][ThanhTien]");

            List<ChiTietXuatKho> listCTHD = new List<ChiTietXuatKho>();
            for (int i = 0; i < MaNguyenLieu.Length; i++)
            {
                ChiTietXuatKho cthd = new ChiTietXuatKho
                {
                    MaXuatKho = MaXuatKho,  // Đảm bảo MaNhapKho được gán đúng
                    MaNguyenLieu = MaNguyenLieu[i],
                    SoLuong = int.Parse(SoLuong[i]),
                    ThanhTien = int.Parse(ThanhTien[i])
                };
                listCTHD.Add(cthd);
            }

            XuLyXuatKho objHD = new XuLyXuatKho();
            objHD.XuLyThemChiTietXuatKho(listCTHD);

            return RedirectToAction("HienThiXuatKho");
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
            string maPhieuXuat = GenerateXuatKho(); // Gọi hàm để lấy giá trị
            ViewBag.maPhieuXuat = maPhieuXuat;
            XuLyNguyenLieu objHD = new XuLyNguyenLieu();
            var danhSachNguyenLieu = objHD.LayThongTinNguyenLieu(); // Gọi phương thức lấy danh sách nguyên liệu
            // Truyền dữ liệu vào View
            return View(danhSachNguyenLieu);
        }
        private XuLyXuatKho _repository = new XuLyXuatKho();

        public ActionResult chitietXuatKho(string MaNhapKho)
        {
            var chiTietNhapKho = _repository.GetChiTietXuatKho(MaNhapKho);
            return View(chiTietNhapKho);
        }
    }
}