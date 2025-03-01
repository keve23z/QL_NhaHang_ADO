using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace QL_NhaHang_ADO.Models
{
    public class XuLyThongTinNhanVien
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;

        public List<NhanVien> LayThongTinNhanVien()
        {
            List<NhanVien> listProduct = new List<NhanVien>();
            SqlConnection con = new SqlConnection(connectionString);

            // Truy vấn SQL để lấy và giải mã TenDangNhap bằng hàm decryptCaesarCipher
            string sql = @"
                         SELECT 
                             MANV,
                             MATAIKHOAN,
                             HOTEN,
                             SODT
                         FROM NhanVien";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            con.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                NhanVien kh = new NhanVien();

                // Lấy kết quả giải mã từ cột TenDangNhapDecrypted
                kh.MaTaiKhoan = rdr.GetValue(1).ToString();

                kh.MaNV = rdr.GetValue(0).ToString();

                kh.HoTen = rdr.GetValue(2).ToString();

                kh.SoDT = rdr.GetValue(3).ToString();

                listProduct.Add(kh);
            }
            con.Close();
            return listProduct;
        }
    }
}