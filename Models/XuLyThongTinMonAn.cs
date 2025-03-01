using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace QL_NhaHang_ADO.Models
{
    public class XuLyThongTinMonAn
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;

        public List<MonAn> LayThongTinMonAn()
        {
            List<MonAn> listProduct = new List<MonAn>();
            SqlConnection con = new SqlConnection(connectionString);

            // Truy vấn SQL để lấy và giải mã TenDangNhap bằng hàm decryptCaesarCipher
            string sql = @"
                         SELECT 
                             MaMonAn,                         
                             TenMon,
                             LoaiMon,
                             Gia,
                             AnhMon,
                                MoTa    
                         FROM MonAn";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            con.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                MonAn Ma = new MonAn();

                // Lấy kết quả giải mã từ cột TenDangNhapDecrypted             

               Ma.MaMonAn = rdr.GetValue(0).ToString();

                Ma.TenMon = rdr.GetValue(1).ToString();

                // Giải mã mật Maẩu trong C#
                Ma.LoaiMon = rdr.GetValue(2).ToString();

                Ma.Gia = float.Parse(rdr.GetValue(3).ToString());

                Ma.AnhMon = rdr.GetValue(4).ToString();

                Ma.MoTa = rdr.GetValue(5).ToString();

                listProduct.Add(Ma);
            }
            con.Close();
            return listProduct;
        }
        public void suaMonAn(MonAn Ma)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string sql = @"
                         UPDATE MonAn
                         SET TenMon = @TenMon, LoaiMon = @LoaiMon, Gia = @Gia, AnhMon = @AnhMon, MoTa = @MoTa
                         WHERE MaMonAn = @MaMonAn";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MaMonAn", Ma.MaMonAn);
            cmd.Parameters.AddWithValue("@TenMon", Ma.TenMon);
            cmd.Parameters.AddWithValue("@LoaiMon", Ma.LoaiMon);
            cmd.Parameters.AddWithValue("@Gia", Ma.Gia);
            cmd.Parameters.AddWithValue("@AnhMon", Ma.AnhMon);
            cmd.Parameters.AddWithValue("@MoTa", Ma.MoTa);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        // thêm món ăn`
        public void themMonAn(MonAn monAn)
        {
            if (KiemTraMonAn(monAn))
            {
                return;
            }
            SqlConnection con = new SqlConnection(connectionString);
            string sql = @"
                         INSERT INTO MonAn(MaMonAn, TenMon, LoaiMon, Gia, AnhMon, MoTa)
                         VALUES(@MaMonAn, @TenMon, @LoaiMon, @Gia, @AnhMon, @MoTa)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MaMonAn", monAn.MaMonAn);
            cmd.Parameters.AddWithValue("@TenMon", monAn.TenMon);
            cmd.Parameters.AddWithValue("@LoaiMon", monAn.LoaiMon);
            cmd.Parameters.AddWithValue("@Gia", monAn.Gia);
            cmd.Parameters.AddWithValue("@AnhMon", monAn.AnhMon);
            cmd.Parameters.AddWithValue("@MoTa", monAn.MoTa);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void xoaMonAn(MonAn monAn)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string sql = @"
                         DELETE FROM MonAn
                         WHERE MaMonAn = @MaMonAn";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MaMonAn", monAn.MaMonAn);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void SuamonAn(MonAn Ma)
        {
            KiemTraMonAn(Ma);
            SqlConnection con = new SqlConnection(connectionString);
            string sql = @"
                         UPDATE MonAn
                         SET TenMon = @TenMon, LoaiMon = @LoaiMon, Gia = @Gia, AnhMon = @AnhMon, MoTa = @MoTa
                         WHERE MaMonAn = @MaMonAn";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MaMonAn", Ma.MaMonAn.Trim());
            cmd.Parameters.AddWithValue("@TenMon", Ma.TenMon);
            cmd.Parameters.AddWithValue("@LoaiMon", Ma.LoaiMon);
            cmd.Parameters.AddWithValue("@Gia", Ma.Gia);
            cmd.Parameters.AddWithValue("@AnhMon", Ma.AnhMon);
            cmd.Parameters.AddWithValue("@MoTa", Ma.MoTa);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        // kiểm tra món ăn đã tồn tại chưa và dữ liệu nhập vào có hợp lệ không
        public bool KiemTraMonAn(MonAn Ma)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string sql = @"
                         SELECT COUNT(*)
                         FROM MonAn
                         WHERE MaMonAn = @MaMonAn";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MaMonAn", Ma.MaMonAn);
            con.Open();
            int count = (int)cmd.ExecuteScalar();
            con.Close();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public MonAn LayTheoMa(string ma)
        {
            MonAn monAn = new MonAn();
            SqlConnection con = new SqlConnection(connectionString);
            string sql = @"
                         SELECT *
                         FROM MonAn
                         WHERE MaMonAn = @MaMonAn";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MaMonAn", ma);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                monAn.MaMonAn = rdr.GetValue(0).ToString();
                monAn.TenMon = rdr.GetValue(1).ToString();
                monAn.LoaiMon = rdr.GetValue(2).ToString();
                monAn.Gia = float.Parse(rdr.GetValue(3).ToString());
                monAn.AnhMon = rdr.GetValue(4).ToString();
                monAn.MoTa = rdr.GetValue(5).ToString();
            }
            con.Close();
            return monAn;
        }
    }
}