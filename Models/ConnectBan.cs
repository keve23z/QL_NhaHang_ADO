using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QL_NhaHang_ADO.Models
{
    public class ConnectBan
    {
        public SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connect1"].ConnectionString);
        public List<Ban> GetBans()
        {
            List<Ban> bans = new List<Ban>();

            try
            {
                conn.Open();

                // Truy vấn để lấy danh sách bàn
                string queryBan = @"SELECT * FROM Ban";

                SqlCommand cmdBan = new SqlCommand(queryBan, conn);
                SqlDataReader readerBan = cmdBan.ExecuteReader();

                while (readerBan.Read())
                {
                    Ban ban = new Ban
                    {
                        MaBan = readerBan["MABAN"].ToString(),
                        TrangThai = readerBan["TRANGTHAI"].ToString(),
                        SucChua = readerBan["SUCCHUA"].ToString()
                    };
                    bans.Add(ban);
                }
                readerBan.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            // sắp xếp theo mã bàn theo số thứ tự
            bans.Sort((x, y) =>
            {
                int maBanX = int.TryParse(x.MaBan, out int resultX) ? resultX : int.MaxValue;
                int maBanY = int.TryParse(y.MaBan, out int resultY) ? resultY : int.MaxValue;
                return maBanX.CompareTo(maBanY);
            });

            return bans;
        }

        public List<Ban> LoadBanTrong()
        {
            List<Ban> bans = new List<Ban>();

            try
            {
                conn.Open();

                // JOIN bảng HoaDon với KhachHang để lấy tên khách hàng
                string queryBan = @"select * from Ban where TRANGTHAI = N'Trống'";

                SqlCommand cmdBan = new SqlCommand(queryBan, conn);
                SqlDataReader readerBan = cmdBan.ExecuteReader();

                while (readerBan.Read())
                {
                    Ban ban = new Ban
                    {
                        MaBan = readerBan["MABAN"].ToString(),
                        TrangThai = readerBan["TRANGTHAI"].ToString()

                    };
                    bans.Add(ban);
                }
                readerBan.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            // sắp xếp theo mã bàn
            bans.Sort((x, y) =>
            {
                int maBanX = int.TryParse(x.MaBan, out int resultX) ? resultX : int.MaxValue;
                int maBanY = int.TryParse(y.MaBan, out int resultY) ? resultY : int.MaxValue;
                return maBanX.CompareTo(maBanY);
            });

            return bans;
        }
        public bool UpdateTrangThai(string maBan, string trangThai)
        {
            try
            {
                conn.Open();

                string query = "UPDATE Ban SET TRANGTHAI = @TrangThai WHERE MABAN = @MaBan";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaBan", maBan);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        public List<Ban> GetBanTrong()
        {
            List<Ban> bans = new List<Ban>();

            try
            {
                conn.Open();
                string queryBan = @"SELECT * 
                                    FROM Ban 
                                    WHERE MABAN NOT IN (SELECT MABAN FROM PhieuGoiMon) 
                                    AND TRANGTHAI = N'Trống'";

                SqlCommand cmdBan = new SqlCommand(queryBan, conn);
                SqlDataReader readerBan = cmdBan.ExecuteReader();
                while (readerBan.Read())
                {
                    Ban ban = new Ban
                    {
                        MaBan = readerBan["MABAN"].ToString(),
                        TrangThai = readerBan["TRANGTHAI"].ToString()
                    };
                    bans.Add(ban);
                }
                readerBan.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            //sắp xếp theo mã bàn  
            bans.Sort((x, y) =>
            {
                int maBanX = int.TryParse(x.MaBan, out int resultX) ? resultX : int.MaxValue;
                int maBanY = int.TryParse(y.MaBan, out int resultY) ? resultY : int.MaxValue;
                return maBanX.CompareTo(maBanY);
            });

            return bans;
        }

        public void XuLyTrangThaiBanThanhDay(string ma)
        { 
            string sql = @"UPDATE Ban
                            SET TRANGTHAI = N'Đầy'
                            WHERE MABAN = @ma";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connect1"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ma", ma);
            cmd.CommandType = System.Data.CommandType.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void XuLyTrangThaiBanThanhTrong(string ma)
        {
            string sql = @"UPDATE Ban
                            SET TRANGTHAI = N'Trống'
                            WHERE MABAN = @ma";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connect1"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ma", ma);
            cmd.CommandType = System.Data.CommandType.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}