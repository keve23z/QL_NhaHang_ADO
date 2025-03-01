using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.WebPages;
namespace QL_NhaHang_ADO.Models
{
    public class XuLyThongTinKhachHang
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;

        public List<KhachHang> LayThongTinKhachHang()
        {
            List<KhachHang> listProduct = new List<KhachHang>();
            SqlConnection con = new SqlConnection(connectionString);

            // Truy vấn SQL để lấy và giải mã TenDangNhap bằng hàm decryptCaesarCipher
            string sql = @"
                         SELECT 
                             MAKH,
                             MATAIKHOAN,
                             HOTEN,
                             NGAYSINH,
                             SODT,
                             DIEMTHANHVIEN,
                             AVATAR
                         FROM KhachHang";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            con.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                KhachHang kh = new KhachHang();

                // Lấy kết quả giải mã từ cột TenDangNhapDecrypted
                kh.MaTaiKhoan = rdr.GetValue(1).ToString();

                kh.MaKH = rdr.GetValue(0).ToString();

                kh.HoTen = rdr.GetValue(2).ToString();

                // Giải mã mật khẩu trong C#
                kh.NgaySinh = rdr.GetValue(3).ToString();

                kh.SoDT = rdr.GetValue(4).ToString();

                kh.DiemThanhVien = Convert.ToInt32(rdr.GetValue(5).ToString());

                kh.Avatar = rdr.GetValue(6).ToString();

                listProduct.Add(kh);
            }
            con.Close();
            return listProduct;
        }

        public KhachHang LayThongTinTuSDT(string SDT)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string sql = @"
                         SELECT 
                             MAKH,
                             MATAIKHOAN,
                             HOTEN,
                             NGAYSINH,
                             SODT,
                             DIEMTHANHVIEN,
                             AVATAR
                         FROM KhachHang WHERE SODT = @SDT";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@SDT", SDT);
            con.Open();
            cmd.ExecuteNonQuery();
            KhachHang kh = new KhachHang();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                kh.MaTaiKhoan = rdr.GetValue(1).ToString();
                kh.MaKH = rdr.GetValue(0).ToString();
                kh.HoTen = rdr.GetValue(2).ToString();
                kh.NgaySinh = rdr.GetValue(3).ToString();
                kh.SoDT = rdr.GetValue(4).ToString();
                kh.DiemThanhVien = Convert.ToInt32(rdr.GetValue(5).ToString());
                kh.Avatar = rdr.GetValue(6).ToString();
            }
            con.Close();
            return kh;
        }


        public List<HoaDon> HoaDonTheoMaKhach(string ma)
        {
            List<HoaDon> listProduct = new List<HoaDon>();
            SqlConnection con = new SqlConnection(connectionString);
            // Truy vấn SQL để lấy và giải mã TenDangNhap bằng hàm decryptCaesarCipher
            string sql = @"
                         SELECT *
                         FROM HoaDon WHERE MAKH = @ma";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ma", ma);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                HoaDon hd = new HoaDon();
                hd.Ma = rdr.GetValue(0).ToString();
                hd.MaBan = rdr.GetValue(1).ToString();
                hd.MaKH = rdr.GetValue(2).ToString();
                hd.MaNV = rdr.GetValue(3).ToString();
                hd.MaGiamGia = rdr.GetValue(4).ToString();
                hd.NgayLap = DateTime.Parse(rdr.GetValue(5).ToString());
                hd.TongTien = Convert.ToInt32(rdr.GetValue(6).ToString());
                hd.HinhThuc = rdr.GetValue(7).ToString();
                hd.GiaGiam = Convert.ToInt32(rdr.GetValue(8).ToString());
                listProduct.Add(hd);
            }
            con.Close();
            return listProduct;
        }

        public void TruDiemThanhVien(string ma, int SoTien)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "UPDATE KhachHang SET DIEMTHANHVIEN = DIEMTHANHVIEN - @SoTien WHERE MAKH = @Ma";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Ma", ma);
            cmd.Parameters.AddWithValue("@SoTien", SoTien);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}