using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace QL_NhaHang_ADO.Models
{
    public class XuLyGiamGia
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;

        public bool KiemTraMaGiamGia(string maGiamGia)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "SELECT COUNT(*) FROM GiamGia WHERE MAGIAMGIA = @MaGiamGia AND SOLUONG > 0 AND NGAYBD <= GETDATE() AND NGAYKT >= GETDATE()";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaGiamGia", maGiamGia);
            int count = (int)cmd.ExecuteScalar();
            conn.Close();
            if(count > 0)
            {
                return true;
            }
            return false;
        }

        public int LayGiaTriGiamGia(string maGiamGia)
        {
            if(!KiemTraMaGiamGia(maGiamGia))
            {
                return 0;
            }
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "SELECT SOTIEN FROM GiamGia WHERE MAGIAMGIA = @MaGiamGia";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaGiamGia", maGiamGia);
            int giatrigiamgia = (int)cmd.ExecuteScalar();
            conn.Close();
            return giatrigiamgia;
        }

        public void GiamSoLuongMaKhiThanhToan(string ma)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "UPDATE GiamGia SET SOLUONG = SOLUONG - 1 WHERE MAGIAMGIA = @Ma";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Ma", ma);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void ThemMaGiamGia(string ma ,DateTime ngayBD,DateTime ngayKT, int soLuong, int soTien)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            // đổi ngày tháng năm thành dạng yyyy-MM-dd
            ngayBD = new DateTime(ngayBD.Year, ngayBD.Month, ngayBD.Day);
            ngayKT = new DateTime(ngayKT.Year, ngayKT.Month, ngayKT.Day);
            conn.Open();
            string sql = "INSERT INTO GiamGia VALUES(@Ma, @NgayBD, @NgayKT, @SoLuong, @SoTien)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Ma", ma);
            cmd.Parameters.AddWithValue("@NgayBD", ngayBD);
            cmd.Parameters.AddWithValue("@NgayKT", ngayKT);
            cmd.Parameters.AddWithValue("@SoLuong", soLuong);
            cmd.Parameters.AddWithValue("@SoTien", soTien);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}