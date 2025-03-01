using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace QL_NhaHang_ADO.Models
{

        public class ConnectHoaDon
        {
            public SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connect1"].ConnectionString);

            public List<HoaDon> GetData()
            {
                List<HoaDon> hoaDons = new List<HoaDon>();

                try
                {
                    conn.Open();

                    // JOIN bảng HoaDon với KhachHang để lấy tên khách hàng
                    string queryHoaDon = @"
            SELECT HD.*, KH.HOTEN AS TenKhachHang
            FROM HoaDon HD
            LEFT JOIN KhachHang KH ON HD.MAKH = KH.MAKH";
                    SqlCommand cmdHoaDon = new SqlCommand(queryHoaDon, conn);
                    SqlDataReader readerHoaDon = cmdHoaDon.ExecuteReader();

                    while (readerHoaDon.Read())
                    {
                        HoaDon hoaDon = new HoaDon
                        {
                            Ma = readerHoaDon["MAHOADON"].ToString(),
                            MaBan = readerHoaDon["MABAN"].ToString(),
                            MaKH = readerHoaDon["MAKH"].ToString(),
                            MaNV = readerHoaDon["MANV"].ToString(),
                            MaGiamGia = readerHoaDon["MAGIAMGIA"].ToString(),
                            NgayLap = Convert.ToDateTime(readerHoaDon["NGAYLAP"]),
                            HinhThuc = readerHoaDon["HINHTHUC"].ToString(),
                            TenKhachHang = readerHoaDon["TenKhachHang"].ToString()
                        };
                        hoaDons.Add(hoaDon);
                    }
                    readerHoaDon.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }

                return hoaDons;
            }

            public List<ChiTietHoaDon> ListChiTiet(string MaHD)
            {
                if (string.IsNullOrEmpty(MaHD))
                {
                    throw new ArgumentException("Tham số MaHD không được null hoặc rỗng.");
                }

                List<ChiTietHoaDon> chiTietHoaDons = new List<ChiTietHoaDon>();

                string sql = "select * , MonAn.TENMON " +
                    "from ChiTietHoaDon join MonAn on ChiTietHoaDon.MAMONAN=MonAn.MAMONAN " +
                    "where ChiTietHoaDon.MAHOADON= @MaHD";

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connect1"].ConnectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHD", MaHD.Trim());

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ChiTietHoaDon chiTiet = new ChiTietHoaDon
                            {
                                MaHD = reader["MAHOADON"].ToString(),
                                MaMA = reader["MAMONAN"].ToString(),
                                SoLuong = Convert.ToInt32(reader["SOLUONG"]),
                                ThanhTien = Convert.ToInt32(reader["THANHTIEN"]),
                                TenMon = reader["TENMON"].ToString(),
                                GiaMon = Convert.ToInt32(reader["Gia"]),

                            };

                            chiTietHoaDons.Add(chiTiet);
                        }
                    }
                }
                return chiTietHoaDons;
            }
            public HoaDon GetHoaDonByMa(string ma)
            {
                HoaDon hoaDon = null;
                string query = "SELECT MAHOADON ,MANV , MABAN, HoaDon.MAKH, NGAYLAP, HINHTHUC, TONGTIEN, HOTEN, MAGIAMGIA, GiaGiam " +
                               "FROM HoaDon JOIN KhachHang ON HoaDon.MAKH = KhachHang.MAKH " +
                               "WHERE HoaDon.MAHOADON = @MaHD";

                try
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHD", ma);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                hoaDon = new HoaDon
                                {
                                    Ma = reader["MAHOADON"].ToString(),
                                    MaBan = reader["MABAN"].ToString(),
                                    MaKH = reader["MAKH"] as string ?? "",
                                    MaNV = reader["MANV"] as string ?? "",
                                    MaGiamGia = reader["MAGIAMGIA"] as string ?? "",
                                    NgayLap = reader["NGAYLAP"] as DateTime? ?? DateTime.MinValue,
                                    HinhThuc = reader["HINHTHUC"] as string ?? "",
                                    TenKhachHang = reader["HOTEN"] as string ?? "",
                                    TongTien = reader["TONGTIEN"] as int? ?? 0,
                                    GiaGiam = reader["GiaGiam"] as int? ?? 0
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                    Console.WriteLine($"Error when fetching HoaDon: {ex.Message}");
                }
                finally
                {
                    conn.Close();
                }

                return hoaDon;
            }



            public bool UpdateHoaDon(string maHD, string maBan, string maKH, string maNV, string maGiamGia, DateTime? ngayLap, string hinhThuc)
            {
                // Kiểm tra maHD có phải null không
                if (string.IsNullOrEmpty(maHD))
                {
                    throw new ArgumentException("Mã hóa đơn không hợp lệ", nameof(maHD));
                }

                string query = "UPDATE HoaDon " +
                               "SET MABAN = @MaBan, " +
                               "MAKH = @MaKH, " +
                               "MANV = @MaNV, " +
                               "MAGIAMGIA = @MaGiamGia, " +
                               "NGAYLAP = COALESCE(@NgayLap, GETDATE()), " +
                               "HINHTHUC = @HinhThuc " +
                               "WHERE MAHOADON = @MaHD";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Đảm bảo rằng tất cả các tham số đã được thêm vào câu lệnh
                    cmd.Parameters.AddWithValue("@MaHD", maHD.Trim());
                    cmd.Parameters.AddWithValue("@MaBan", maBan.Trim());
                    cmd.Parameters.AddWithValue("@MaKH", maKH.Trim());
                    cmd.Parameters.AddWithValue("@MaNV", maNV.Trim());
                    cmd.Parameters.AddWithValue("@MaGiamGia", maGiamGia.Trim());
                    cmd.Parameters.AddWithValue("@NgayLap", ngayLap ?? DateTime.MinValue); // Nếu NgayLap là null, sử dụng DateTime.MinValue
                    cmd.Parameters.AddWithValue("@HinhThuc", hinhThuc.Trim());

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    return rowsAffected > 0;
                }
            }


            public bool AddHoaDon(string MaHoaDon, string MaNV, string MaKH, DateTime NgayLap)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connect1"].ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("InsertHoaDon", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Thêm các tham số vào stored procedure
                            cmd.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);
                            cmd.Parameters.AddWithValue("@MaNV", MaNV);
                            cmd.Parameters.AddWithValue("@MaKH", MaKH);
                            cmd.Parameters.AddWithValue("@NgayLap", NgayLap);

                            // Thực thi stored procedure
                            int rowsAffected = cmd.ExecuteNonQuery();
                            return rowsAffected > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
            public List<HoaDon> Search(string txt_Search)
            {
                List<HoaDon> listHD = new List<HoaDon>();

                string sql = @"
                        SELECT * FROM HoaDon 
                        WHERE MAHOADON LIKE '%' + @search + '%' 
                        OR MABAN LIKE '%' + @search + '%' 
                        OR MAKH LIKE '%' + @search + '%' 
                        OR MANV LIKE '%' + @search + '%' 
                        OR MAGIAMGIA LIKE '%' + @search + '%' 
                        OR HINHTHUC LIKE '%' + @search + '%'";

                // Lấy chuỗi kết nối từ file cấu hình
                string connectionString = ConfigurationManager.ConnectionStrings["Connect1"].ConnectionString;

                // Sử dụng đúng chuỗi kết nối khi khởi tạo SqlConnection
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@search", txt_Search));

                        con.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                HoaDon hd = new HoaDon
                                {
                                    Ma = rdr["MAHOADON"].ToString(),
                                    MaBan = rdr["MABAN"].ToString(),
                                    MaKH = rdr["MAKH"].ToString(),
                                    MaNV = rdr["MANV"].ToString(),
                                    MaGiamGia = rdr["MAGIAMGIA"].ToString(),
                                    NgayLap = rdr.IsDBNull(rdr.GetOrdinal("NGAYLAP")) ? (DateTime?)null : rdr.GetDateTime(rdr.GetOrdinal("NgayLap")),
                                    HinhThuc = rdr["HINHTHUC"].ToString()
                                };

                                listHD.Add(hd);
                            }
                        }
                    }
                }

                return listHD;
            }

        public KhachHang LayThongTinKHTheoMaHD(string ma)
        {
            string sql = "SELECT * FROM KhachHang WHERE MAKH = (SELECT MAKH FROM HoaDon WHERE MAHOADON = @MaHD)";
            KhachHang kh = new KhachHang();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connect1"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaHD", ma);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        kh.HoTen = reader["HOTEN"].ToString();
                        kh.SoDT = reader["SODT"].ToString();
                        kh.MaKH = reader["MAKH"].ToString();
                    }
                }
            }
            return kh;
        }
    }
}
