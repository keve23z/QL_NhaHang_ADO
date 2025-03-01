using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using QL_NhaHang_ADO.Models;
using System.Data.SqlClient;



namespace QL_NhaHang_ADO.Models
{

    public class XuLyPhieuGoiMon
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;

        //        CREATE TABLE PhieuGoiMon(
        //    MaPGM VARCHAR(10) PRIMARY KEY, -- Thêm khóa chính nếu cần
        //    MABAN CHAR(10),
        //    CONSTRAINT FK_PGM_BAN FOREIGN KEY(MABAN) REFERENCES Ban(MABAN)
        //);

        //CREATE TABLE ChiTietGoiMon(
        //    MaMonAn CHAR(10) NOT NULL,     -- Mã món ăn
        //    MaPGM VARCHAR(10) NOT NULL,       -- Mã phiếu gọi món
        //    SoLuong INT NOT NULL,          -- Số lượng món
        //    PRIMARY KEY(MaMonAn, MaPGM),  -- Khóa chính kết hợp
        //    FOREIGN KEY(MaMonAn) REFERENCES MonAn(MaMonAn), -- Khóa ngoại liên kết đến bảng MonAn
        //    FOREIGN KEY(MaPGM) REFERENCES PhieuGoiMon(MaPGM) -- Khóa ngoại liên kết đến bảng PhieuGoiMon
        //);

        public List<PhieuGoiMon> LayĐanhSachPhieu()
        {
            List<PhieuGoiMon> dsPhieu = new List<PhieuGoiMon>();
            string sql = "SELECT * FROM PhieuGoiMon";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                PhieuGoiMon phieu = new PhieuGoiMon();
                phieu.MaPGM = rdr.GetValue(0).ToString();
                phieu.MaBan = rdr.GetValue(1).ToString();
                dsPhieu.Add(phieu);
            }
            con.Close();

            return dsPhieu;
        }

        public List<ChiTietPhieuGoiMon> LayChiTiet(string ma)
        {
            List<ChiTietPhieuGoiMon> dsChiTiet = new List<ChiTietPhieuGoiMon>();
            string sql = @"
                SELECT 
                    cm.MaMonAn,
                    m.TENMON, 
                    cm.SoLuong, 
                    m.GIA, 
                    (cm.SoLuong * m.GIA) AS ThanhTien
                FROM 
                    ChiTietGoiMon cm
                JOIN 
                    MonAn m ON cm.MaMonAn = m.MaMonAn
                WHERE 
                    cm.MaPGM = @MaPGM";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@MaPGM", ma);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ChiTietPhieuGoiMon ct = new ChiTietPhieuGoiMon();
                ct.MaMonAn = rdr.GetValue(0).ToString(); // Lấy mã món ăn
                ct.TenMon = rdr.GetValue(1).ToString(); // Tên món ăn
                ct.SoLuong = int.Parse(rdr.GetValue(2).ToString()); // Số lượng
                ct.DonGia = int.Parse(rdr.GetValue(3).ToString()); // Đơn giá
                ct.ThanhTien = int.Parse(rdr.GetValue(4).ToString()); // Thành tiền
                dsChiTiet.Add(ct);
            }
            con.Close();
            return dsChiTiet;
        }




        public void ThemPhieuMoi(PhieuGoiMon phieu)
        {
            string sql = "INSERT INTO PhieuGoiMon VALUES(@MaPGM, @MaBan)";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            string s = phieu.MaPGM;
            cmd.Parameters.AddWithValue("@MaPGM", phieu.MaPGM);
            cmd.Parameters.AddWithValue("@MaBan", phieu.MaBan);
            cmd.CommandType = CommandType.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void ThemChiTietPhieu(string ma, List<ChiTietPhieuGoiMon> ct)
        {
            // Thêm chi tiết phiếu gọi món
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            foreach (var item in ct)
            {
                string sql = "INSERT INTO ChiTietGoiMon VALUES(@MaMonAn, @MaPGM, @SoLuong)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@MaMonAn", item.MaMonAn);
                cmd.Parameters.AddWithValue("@MaPGM", ma);
                cmd.Parameters.AddWithValue("@SoLuong", item.SoLuong);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        // xóa chi tiết
        public void XoaChiTiet(string ma)
        {
            string sql = "DELETE FROM ChiTietGoiMon WHERE MaPGM = @MaPGM";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@MaPGM", ma);
            cmd.CommandType = CommandType.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        // xóa phiếu
        public void XoaPhieu(string ma)
        {
            string sql = "DELETE FROM PhieuGoiMon WHERE MaPGM = @MaPGM";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@MaPGM", ma);
            cmd.CommandType = CommandType.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}