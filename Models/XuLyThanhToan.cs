using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;



namespace QL_NhaHang_ADO.Models
{
    public class XuLyThanhToan
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;


        public void XuLyHoaDon(List<ChiTietHoaDon> chiTietHoaDonList, HoaDon hd)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string sql = "insert into HoaDon values(@Ma, @MaBan, @MaKH, @MaNV, @MaGiamGia, @NgayLap,@TongTien, @HinhThuc, @GiaGiam)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("Ma", hd.Ma);
            cmd.Parameters.AddWithValue("MaBan", hd.MaBan ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("MaKH", hd.MaKH ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("MaNV", hd.MaNV ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("MaGiamGia", hd.MaGiamGia);
            cmd.Parameters.AddWithValue("NgayLap", hd.NgayLap);
            cmd.Parameters.AddWithValue("TongTien", hd.TongTien);
            cmd.Parameters.AddWithValue("HinhThuc", hd.HinhThuc);
            cmd.Parameters.AddWithValue("GiaGiam", hd.GiaGiam);
            cmd.ExecuteNonQuery();
            con.Close();

            string sql2 = "update KhachHang set DIEMTHANHVIEN = DIEMTHANHVIEN + @TichDiem where MAKH = @MaKH";
            con.Open();
            cmd = new SqlCommand(sql2, con);
            cmd.Parameters.AddWithValue("TichDiem", hd.GiaGiam*0.01);
            cmd.Parameters.AddWithValue("MaKH", hd.MaKH);
            cmd.ExecuteNonQuery();
            con.Close();
            // Thêm chi tiết hóa đơn
            foreach (ChiTietHoaDon cthd in chiTietHoaDonList)
            {
                con.Open();
                sql = "insert into ChiTietHoaDon values(@MaHD, @MaMA, @SoLuong, @ThanhTien)";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("MaHD", cthd.MaHD);
                cmd.Parameters.AddWithValue("MaMA", cthd.MaMA);
                cmd.Parameters.AddWithValue("SoLuong", cthd.SoLuong);
                cmd.Parameters.AddWithValue("ThanhTien", cthd.ThanhTien);
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
    }
}