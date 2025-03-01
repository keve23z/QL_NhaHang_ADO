using QL_NhaHang_ADO.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_NhaHang_ADO.Controllers
{
    public class DatBanController : Controller
    {
        // GET: DatBan
        public ActionResult DatBan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DatBan(PhieuDatBan model)
        {
        

            bool isRegistered = DatBan_usser(model);
            if (isRegistered)
            {
                TempData["SignSuccess"] = true;
                var tmp = -1;
                if (Session["Cart"] == null)
                {
                    tmp = 0;
                }
                return RedirectToAction("Welcome", "Account", new { id = -1, cart = tmp });
            }
            else
            {
                return View(model);
            }
        }
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;
        public bool DatBan_usser(PhieuDatBan PhieuDatBan)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Truy vấn SQL để thêm phiếu đặt bàn
                string query = @"
INSERT INTO PhieuDatBan (MaPhieu, MAKH, NgayDat, TenKH, SoLuong, Email, SDT, GIODAT) 
VALUES (@MaPhieu, @MAKH, @NgayDat, @TenKH, @SoLuong, @Email, @SDT, @GIODAT)";

                SqlCommand cmd = new SqlCommand(query, conn);

                // Xử lý giá trị của MAKH
                object maKH = Session["MaKH"] ?? DBNull.Value;

                cmd.Parameters.Add(new SqlParameter("@MaPhieu", GeneratePhieuDatBan()));
                cmd.Parameters.Add(new SqlParameter("@MAKH", maKH));
                cmd.Parameters.Add(new SqlParameter("@NgayDat", PhieuDatBan.NgayDat));
                cmd.Parameters.Add(new SqlParameter("@TenKH", PhieuDatBan.TenKH));
                cmd.Parameters.Add(new SqlParameter("@SoLuong", PhieuDatBan.SoLuong));
                cmd.Parameters.Add(new SqlParameter("@Email", PhieuDatBan.Email));
                cmd.Parameters.Add(new SqlParameter("@SDT", PhieuDatBan.SDT));
                cmd.Parameters.Add(new SqlParameter("@GIODAT", PhieuDatBan.GIODAT));

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close();

                new XuLyDatBan().GuiMail(PhieuDatBan.Email, PhieuDatBan.TenKH, PhieuDatBan.NgayDat, PhieuDatBan.SDT, PhieuDatBan.SoLuong, PhieuDatBan.GIODAT);

                return result > 0;
            }
        }

        public int GetNextNumberFromDatabase()
        {
            int nextNumber = 1; // Giá trị mặc định nếu không có dữ liệu trong bảng
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connect1"].ConnectionString; // Chuỗi kết nối tới Oracle

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Truy vấn số thứ tự lớn nhất hiện có
                string query = "SELECT MAX(CAST(SUBSTRING(MaPhieu, 4, LEN(MaPhieu) - 3) AS INT)) AS MaxValue FROM PhieuDatBan;";

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
        public string GeneratePhieuDatBan()
        {
            string prefix = "PDB";
            int nextNumber = GetNextNumberFromDatabase(); // Hàm để lấy số tiếp theo từ CSDL
            string maTaiKhoan = prefix + nextNumber.ToString("D4"); // Format thành 'TK0001'

            return maTaiKhoan;
        }
    }
}