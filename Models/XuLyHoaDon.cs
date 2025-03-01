using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;


namespace QL_NhaHang_ADO.Models
{
    public class XuLyHoaDon
    {

        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;
        public List<PhieuNhapKho> LayThongTinHoaDon()
        {
            List<PhieuNhapKho> listProduct = new List<PhieuNhapKho>();
            SqlConnection con = new SqlConnection(connectionString);
            // Truy vấn SQL để lấy và giải mã TenDangNhap bằng hàm decryptCaesarCipher
            string sql = @"
                         SELECT 
                             MANHAPKHO,                         
                             MANV,
                             NGAYNK,
                             TONGTIEN
                         FROM PhieuNhapKho";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                PhieuNhapKho Ma = new PhieuNhapKho();
                // Lấy kết quả giải mã từ cột TenDangNhapDecrypted             
                Ma.MaNhapKho = rdr.GetValue(0).ToString();
                Ma.MaNV = rdr.GetValue(1).ToString();
                // Giải mã mật Maẩu trong C#
                Ma.NgayNhapKho = DateTime.Parse(rdr.GetValue(2).ToString());
                Ma.TongTien = int.Parse(rdr.GetValue(3).ToString());
                listProduct.Add(Ma);
            }
            con.Close();
            return listProduct;
        }

        // hiển thị chi tiết hóa đơn
        public List<ChiTietNhapKho> LayThongTinChiTietHoaDon(string MaNhapKho)
        {
            List<ChiTietNhapKho> listProduct = new List<ChiTietNhapKho>();
            SqlConnection con = new SqlConnection(connectionString);
            // Truy vấn SQL để lấy và giải mã TenDangNhap bằng hàm decryptCaesarCipher
            string sql = @"
                         SELECT 
                             MANHAPKHO,                         
                             MANGUYENLIEU,
                             SOLUONG,
                             THANHTIEN
                         FROM ChiTietNhapKho WHERE MANHAPKHO = @MaNhapKho";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MaNhapKho", MaNhapKho);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ChiTietNhapKho Ma = new ChiTietNhapKho();
                // Lấy kết quả giải mã từ cột TenDangNhapDecrypted             
                Ma.MaNhapKho = rdr.GetValue(0).ToString();
                Ma.MaNguyenLieu = rdr.GetValue(1).ToString();
                // Giải mã mật Maẩu trong C#
                Ma.SoLuong = int.Parse(rdr.GetValue(2).ToString());
                Ma.ThanhTien = int.Parse(rdr.GetValue(3).ToString());
                listProduct.Add(Ma);
            }
            con.Close();
            return listProduct;
        }

        public void XuLyThemHoaDon(PhieuNhapKho Ma)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string sql = @"
                         INSERT INTO PhieuNhapKho (MANHAPKHO, MANV, NGAYNK, TONGTIEN)
                           VALUES (@MaNhapKho, @MaNV, @NgayNhapKho, @TongTien)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MaNhapKho", Ma.MaNhapKho);
            cmd.Parameters.AddWithValue("@MaNV", Ma.MaNV);
            cmd.Parameters.AddWithValue("@NgayNhapKho", Ma.NgayNhapKho);
            cmd.Parameters.AddWithValue("@TongTien", Ma.TongTien);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }



        public void XuLyThemChiTietHoaDon(List<ChiTietNhapKho> listCTHD)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            foreach (var item in listCTHD)
            {
                string sql = @"
                 INSERT INTO ChiTietNhapKho (MANHAPKHO, MANGUYENLIEU, SOLUONG, THANHTIEN)
                   VALUES (@MaNhapKho, @MaNguyenLieu, @SoLuong, @ThanhTien)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@MaNhapKho", item.MaNhapKho);
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
                 SET SOLUONGTON = SOLUONGTON + @SoLuong
                 WHERE MANGUYENLIEU = @MaNguyenLieu";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@SoLuong", item.SoLuong);
                cmd.Parameters.AddWithValue("@MaNguyenLieu", item.MaNguyenLieu);
                cmd.ExecuteNonQuery();
            }
            // cập nhật tổng tiền của hóa đơn
            string sql1 = @"
                 UPDATE PhieuNhapKho
                 SET TONGTIEN = TONGTIEN + @TongTien
                 WHERE MANHAPKHO = @MaNhapKho";
            SqlCommand cmd1 = new SqlCommand(sql1, con);
            cmd1.CommandType = CommandType.Text;
            cmd1.Parameters.AddWithValue("@TongTien", listCTHD.Sum(x => x.ThanhTien));
            cmd1.Parameters.AddWithValue("@MaNhapKho", listCTHD[0].MaNhapKho);
            cmd1.ExecuteNonQuery();
            con.Close();
        }
        public List<ChiTietNhapKhoViewModel> GetChiTietNhapKho(string MaNhapKho)
        {
            List<ChiTietNhapKhoViewModel> listProduct = new List<ChiTietNhapKhoViewModel>();
            SqlConnection con = new SqlConnection(connectionString);

            string sql = @"
        SELECT 
            ctnk.MANHAPKHO,
            ctnk.MANGUYENLIEU,
            nl.TENNGUYENLIEU,
            ctnk.SOLUONG,
            ctnk.THANHTIEN
        FROM ChiTietNhapKho ctnk
        INNER JOIN NguyenLieu nl ON ctnk.MANGUYENLIEU = nl.MANGUYENLIEU
        WHERE ctnk.MANHAPKHO = @MaNhapKho";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MaNhapKho", MaNhapKho);
            con.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ChiTietNhapKhoViewModel item = new ChiTietNhapKhoViewModel
                {
                    MaNhapKho = rdr["MANHAPKHO"].ToString(),
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