using QL_NhaHang_ADO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_NhaHang_ADO.Controllers
{
    public class Admin_GiamGiaController : Controller
    {
        // GET: Admin_GiamGia
        public ActionResult TaoMaGiamGia()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TaoMaGiamGia(MaGiamGia model)
        {


            bool isRegistered = GhiMaGiamGia(model);
            if (isRegistered)
            {
                TempData["SignSuccess"] = true;
                return View(model);
            }
            else
            {
                return View(model);
            }
        }
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;
        public bool GhiMaGiamGia(MaGiamGia MaGiamGia)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Truy vấn SQL để thêm phiếu đặt bàn
                string query = @"
INSERT INTO GiamGia (MAGIAMGIA, NGAYBD, NGAYKT, SOLUONG, SOTIEN) 
VALUES (@MAGIAMGIA, @NGAYBD, @NGAYKT, @SOLUONG, @SOTIEN)";

                SqlCommand cmd = new SqlCommand(query, conn);

                string ma = Guid.NewGuid().ToString().Substring(0, 10);

                cmd.Parameters.Add(new SqlParameter("@MAGIAMGIA", ma));
                cmd.Parameters.Add(new SqlParameter("@NGAYBD", MaGiamGia.NgayBD));
                cmd.Parameters.Add(new SqlParameter("@NGAYKT", MaGiamGia.NgayKT));
                cmd.Parameters.Add(new SqlParameter("@SOLUONG", MaGiamGia.SoLuong));
                cmd.Parameters.Add(new SqlParameter("@SOTIEN", MaGiamGia.SoTien));

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close();

                return result > 0;
            }
        }

        public ActionResult HienThiMaGiamGia()
        {
            List<MaGiamGia> listMA = LayThongTinGiamGia();
            return View(listMA);
        }

        public List<MaGiamGia> LayThongTinGiamGia()
        {
            List<MaGiamGia> listProduct = new List<MaGiamGia>();
            SqlConnection con = new SqlConnection(connectionString);

            // Truy vấn SQL để lấy và giải mã TenDangNhap bằng hàm decryptCaesarCipher
            string sql = @"
                        SELECT
                        MAGIAMGIA,
                        NGAYBD,
                        NGAYKT,
                        SOLUONG,
                        SOTIEN
                    FROM GiamGia
                    WHERE NGAYKT >= GETDATE() AND SOLUONG > 0";


            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            con.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                MaGiamGia kh = new MaGiamGia();

                // Lấy kết quả giải mã từ cột TenDangNhapDecrypted
                kh.MaGiamGia_Code = rdr.GetValue(0).ToString();

                kh.NgayBD = DateTime.Parse(rdr.GetValue(1).ToString());

                kh.NgayKT = DateTime.Parse(rdr.GetValue(2).ToString());

                kh.SoLuong = Convert.ToInt32(rdr.GetValue(3).ToString());

                kh.SoTien = Convert.ToInt32(rdr.GetValue(4).ToString());

                listProduct.Add(kh);
            }
            con.Close();
            return listProduct;
        }
    }
}