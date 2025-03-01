using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QL_NhaHang_ADO.Models
{
    public class ConnectDanhGia
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Connect1"].ConnectionString;

        public List<DanhGia> GetData()
        {
            List<DanhGia> danhGias = new List<DanhGia>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string queryDangGia = @"
                SELECT 
                    DanhGia.MADANHGIA,
                    DanhGia.MAHOADON,
                    DanhGia.DANHGIA,
                    KhachHang.HOTEN AS TenKH
                FROM DanhGia
                JOIN HoaDon ON DanhGia.MAHOADON = HoaDon.MAHOADON
                JOIN KhachHang ON HoaDon.MAKH = KhachHang.MAKH";

                    using (SqlCommand cmdDangGia = new SqlCommand(queryDangGia, conn))
                    using (SqlDataReader readerDangGia = cmdDangGia.ExecuteReader())
                    {
                        while (readerDangGia.Read())
                        {
                            DanhGia danhGia = new DanhGia
                            {
                                MaDGia = readerDangGia["MADANHGIA"].ToString(),
                                MaHD = readerDangGia["MAHOADON"].ToString(),
                                TenKH = readerDangGia["TenKH"].ToString(),
                                NoiDung = readerDangGia["DANHGIA"].ToString(),
                            };
                            danhGias.Add(danhGia);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return danhGias;
        }

        public DanhGia LayDanhGiaTuMaHD(string ma)
        {
            DanhGia danhGia = new DanhGia();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string queryDangGia = @"
                SELECT 
                    DanhGia.MADANHGIA,
                    DanhGia.MAHOADON,
                    DanhGia.DANHGIA,
                    KhachHang.HOTEN AS TenKH
                FROM DanhGia
                JOIN HoaDon ON DanhGia.MAHOADON = HoaDon.MAHOADON
                JOIN KhachHang ON HoaDon.MAKH = KhachHang.MAKH
                WHERE DanhGia.MAHOADON = @ma";
                    using (SqlCommand cmdDangGia = new SqlCommand(queryDangGia, conn))
                    {
                        cmdDangGia.Parameters.AddWithValue("@ma", ma);
                        using (SqlDataReader readerDangGia = cmdDangGia.ExecuteReader())
                        {
                            while (readerDangGia.Read())
                            {
                                danhGia.MaDGia = readerDangGia["MADANHGIA"].ToString();
                                danhGia.MaHD = readerDangGia["MAHOADON"].ToString();
                                danhGia.TenKH = readerDangGia["TenKH"].ToString();
                                danhGia.NoiDung = readerDangGia["DANHGIA"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return danhGia;
        }

        public int DeleteDanhGia(string ma)
        {
            int kt = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string sql = "DELETE FROM DanhGia WHERE MADANHGIA = @ma;";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ma", ma);
                        kt = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return kt;
        }
    }
}