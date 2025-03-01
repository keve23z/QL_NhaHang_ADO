using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace QL_NhaHang_ADO.Models
{
    public class XuLyNguyenLieu
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;

        public List<NguyenLieu> LayThongTinNguyenLieu()
        {
            List<NguyenLieu> listProduct = new List<NguyenLieu>();
            SqlConnection con = new SqlConnection(connectionString);

            // Truy vấn SQL để lấy và giải mã TenDangNhap bằng hàm decryptCaesarCipher
            string sql = @"
                         SELECT 
                             MANGUYENLIEU,
                             TENNGUYENLIEU,
                            DONGIA,
                              DVT,
                             SOLUONGTON
                         FROM NguyenLieu";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            con.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                NguyenLieu kh = new NguyenLieu();

                // Lấy kết quả giải mã từ cột TenDangNhapDecrypted
                kh.MaNguyenLieu = rdr.GetValue(0).ToString();

                kh.TenNguyenLieu = rdr.GetValue(1).ToString();

                kh.DonGia = Convert.ToInt32(rdr.GetValue(2).ToString());

                kh.DVT = rdr.GetValue(3).ToString();

                kh.SoLuongTon = Convert.ToInt32(rdr.GetValue(4).ToString());

                listProduct.Add(kh);
            }
            con.Close();
            return listProduct;
        }
    }
}