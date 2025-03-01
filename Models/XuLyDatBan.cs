using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace QL_NhaHang_ADO.Models
{
    public class XuLyDatBan
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;
        public List<PhieuDatBan> LayThongTinPhieuDatBan()
        {
            List<PhieuDatBan> listProduct = new List<PhieuDatBan>();
            SqlConnection con = new SqlConnection(connectionString);
            // Truy vấn SQL để lấy và giải mã TenDangNhap bằng hàm decryptCaesarCipher
            string sql = @"
    SELECT 
        MAPHIEU,                         
        MAKH,
        NGAYDAT,
        TENKH,
        SOLUONG,
        EMAIL,
        SDT,
        GIODAT
    FROM PhieuDatBan
    WHERE CAST(NGAYDAT AS DATE) BETWEEN CAST(GETDATE() AS DATE) AND CAST(DATEADD(DAY, 2, GETDATE()) AS DATE)";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                PhieuDatBan Ma = new PhieuDatBan();                    
                Ma.MaPhieu = rdr.GetValue(0).ToString();
                Ma.MaKH = rdr.GetValue(1).ToString();
                Ma.NgayDat = Convert.ToDateTime(rdr.GetValue(2)).ToString("yyyy -MM-dd");
                Ma.TenKH = rdr.GetValue(3).ToString();
                Ma.SoLuong = int.Parse(rdr.GetValue(4).ToString());
                Ma.Email = rdr.GetValue(5).ToString();
                Ma.SDT = rdr.GetValue(6).ToString();
                Ma.GIODAT = rdr.GetValue(7).ToString();
                listProduct.Add(Ma);
            }
            con.Close();
            return listProduct;
        }

            public void GuiMail(string email, string tenKH, string ngayDat, string sdt, int soLuong, string gioDat)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(email);  // Gửi đến địa chỉ email của khách hàng
                mail.From = new MailAddress("khangtuong040@gmail.com");  // Địa chỉ email gửi đi
                mail.Subject = "Xác nhận thông tin đặt bàn";  // Tiêu đề email
                mail.IsBodyHtml = true;  // Nội dung email ở định dạng HTML

                // Nội dung email, bao gồm thông tin đặt bàn
                mail.Body = $@"
    <h3>Kính gửi: {tenKH},</h3>
    <p>Chúng tôi xin xác nhận thông tin đặt bàn của bạn như sau:</p>
    <ul>
        <li><strong>Tên khách hàng: </strong>{tenKH}</li>
        <li><strong>Số điện thoại: </strong>{sdt}</li>
        <li><strong>Số lượng người: </strong>{soLuong} người</li>
        <li><strong>Ngày đặt: </strong>{ngayDat:dd/MM/yyyy}</li>
        <li><strong>Giờ đặt: </strong>{gioDat}</li>
    </ul>
    <p>Vui lòng đến đúng giờ để tránh ảnh hưởng đến trải nghiệm của bạn và những khách hàng khác.</p>
    <p>Chúng tôi rất mong được đón tiếp bạn tại nhà hàng!</p>
    <p>Trân trọng,</p>
    <p>Nhà hàng chay</p>
";

                // Cấu hình máy chủ SMTP
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("chaytue0203@gmail.com", "kctw ltds teaj luvb");  // Thay bằng thông tin thực
                smtp.EnableSsl = true;

                try
                {
                    // Gửi email
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu gửi email không thành công
                    Console.WriteLine("Lỗi khi gửi email: " + ex.Message);
                }
            }

    }
}