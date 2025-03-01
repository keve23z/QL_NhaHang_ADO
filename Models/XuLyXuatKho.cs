using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace QL_NhaHang_ADO.Models
{
    public class XuLyXuatKho
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;
        public List<PhieuXuatKho> LayThongTinXuatKho()
        {
            List<PhieuXuatKho> listProduct = new List<PhieuXuatKho>();
            SqlConnection con = new SqlConnection(connectionString);
            // Truy vấn SQL để lấy và giải mã TenDangNhap bằng hàm decryptCaesarCipher
            string sql = @"
                         SELECT 
                             MaXuatKho,                         
                             MANV,
                             NGAYXK,
                             TONGTIEN
                         FROM PhieuXuatKho";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                PhieuXuatKho Ma = new PhieuXuatKho();
                // Lấy kết quả giải mã từ cột TenDangNhapDecrypted             
                Ma.MaXuatKho = rdr.GetValue(0).ToString();
                Ma.MaNV = rdr.GetValue(1).ToString();
                // Giải mã mật Maẩu trong C#
                Ma.NgayXuatKho = DateTime.Parse(rdr.GetValue(2).ToString());
                Ma.TongTien = int.Parse(rdr.GetValue(3).ToString());
                listProduct.Add(Ma);
            }
            con.Close();
            return listProduct;
        }

        // hiển thị chi tiết hóa đơn
        public List<ChiTietXuatKho> LayThongTinChiTietHoaDon(string MaXuatKho)
        {
            List<ChiTietXuatKho> listProduct = new List<ChiTietXuatKho>();
            SqlConnection con = new SqlConnection(connectionString);
            // Truy vấn SQL để lấy và giải mã TenDangNhap bằng hàm decryptCaesarCipher
            string sql = @"
                         SELECT 
                             MaXuatKho,                         
                             MANGUYENLIEU,
                             SOLUONG,
                             THANHTIEN
                         FROM ChiTietXuatKho WHERE MaXuatKho = @MaXuatKho";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MaXuatKho", MaXuatKho);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ChiTietXuatKho Ma = new ChiTietXuatKho();
                // Lấy kết quả giải mã từ cột TenDangNhapDecrypted             
                Ma.MaXuatKho = rdr.GetValue(0).ToString();
                Ma.MaNguyenLieu = rdr.GetValue(1).ToString();
                // Giải mã mật Maẩu trong C#
                Ma.SoLuong = int.Parse(rdr.GetValue(2).ToString());
                Ma.ThanhTien = int.Parse(rdr.GetValue(3).ToString());
                listProduct.Add(Ma);
            }
            con.Close();
            return listProduct;
        }

        public void XuLyThemPhieuXuatKho(PhieuXuatKho Ma)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string sql = @"
                         INSERT INTO PhieuXuatKho (MaXuatKho, MANV, NGAYXK, TONGTIEN)
                           VALUES (@MaXuatKho, @MaNV, @NgayXK, @TongTien)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MaXuatKho", Ma.MaXuatKho);
            cmd.Parameters.AddWithValue("@MaNV", Ma.MaNV);
            cmd.Parameters.AddWithValue("@NgayXK", Ma.NgayXuatKho);
            cmd.Parameters.AddWithValue("@TongTien", Ma.TongTien);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }



        public void XuLyThemChiTietXuatKho(List<ChiTietXuatKho> listCTHD)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            foreach (var item in listCTHD)
            {
                string sql = @"
                 INSERT INTO ChiTietXuatKho (MaXuatKho, MANGUYENLIEU, SOLUONG, THANHTIEN)
                   VALUES (@MaXuatKho, @MaNguyenLieu, @SoLuong, @ThanhTien)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@MaXuatKho", item.MaXuatKho);
                cmd.Parameters.AddWithValue("@MaNguyenLieu", item.MaNguyenLieu);
                cmd.Parameters.AddWithValue("@SoLuong", item.SoLuong);
                cmd.Parameters.AddWithValue("@ThanhTien", item.ThanhTien);
                cmd.ExecuteNonQuery();
            }
            // xử lý cập nhật số lượng tồn của nguyên liệu
            foreach (var item in listCTHD)
            {
                string sql = @"
                 UPDATE NguyenLieu
                 SET SOLUONGTON = SOLUONGTON - @SoLuong
                 WHERE MANGUYENLIEU = @MaNguyenLieu";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@SoLuong", item.SoLuong);
                cmd.Parameters.AddWithValue("@MaNguyenLieu", item.MaNguyenLieu);
                cmd.ExecuteNonQuery();
            }
            // cập nhật tổng tiền của hóa đơn
            string sql1 = @"
                 UPDATE PhieuXuatKho
                 SET TONGTIEN = TONGTIEN + @TongTien
                 WHERE MaXuatKho = @MaXuatKho";
            SqlCommand cmd1 = new SqlCommand(sql1, con);
            cmd1.CommandType = CommandType.Text;
            cmd1.Parameters.AddWithValue("@TongTien", listCTHD.Sum(x => x.ThanhTien));
            cmd1.Parameters.AddWithValue("@MaXuatKho", listCTHD[0].MaXuatKho);
            cmd1.ExecuteNonQuery();
            con.Close();
        }
        public List<ChiTietXuatKhoViewModel> GetChiTietXuatKho(string MaXuatKho)
        {
            List<ChiTietXuatKhoViewModel> listProduct = new List<ChiTietXuatKhoViewModel>();
            SqlConnection con = new SqlConnection(connectionString);

            string sql = @"
        SELECT 
            ctnk.MaXuatKho,
            ctnk.MANGUYENLIEU,
            nl.TENNGUYENLIEU,
            ctnk.SOLUONG,
            ctnk.THANHTIEN
        FROM ChiTietXuatKho ctnk
        INNER JOIN NguyenLieu nl ON ctnk.MANGUYENLIEU = nl.MANGUYENLIEU
        WHERE ctnk.MaXuatKho = @MaXuatKho";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MaXuatKho", MaXuatKho);
            con.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ChiTietXuatKhoViewModel item = new ChiTietXuatKhoViewModel
                {
                    MaXuatKho = rdr["MaXuatKho"].ToString(),
                    MaNguyenLieu = rdr["MANGUYENLIEU"].ToString(),
                    TenNguyenLieu = rdr["TENNGUYENLIEU"].ToString(),
                    SoLuong = int.Parse(rdr["SOLUONG"].ToString()),
                    ThanhTien = int.Parse(rdr["THANHTIEN"].ToString())
                };
                listProduct.Add(item);
            }

            con.Close();
            return listProduct;
        }

    }
}