using QL_NhaHang_ADO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Collections;

namespace QL_NhaHang_ADO.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        //==========================================================================================================================================
        //Đăng ký Tài Khoản
        //==========================================================================================================================================
        private KetNoiTaiKhoan taiKhoanRepo = new KetNoiTaiKhoan();


        public ActionResult Logout()
        {
            // Xóa Session
            Session.Clear();
            Session.Abandon();

            // Chuyển hướng về trang đăng nhập
            Session["Cart"] = null;
            return RedirectToAction("Welcome", "Account", new {id = -1, cart = 0});
        }
        public ActionResult XacThuc()
        {
            return View();
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(TaiKhoan model)
        {
            if (ModelState.IsValid)
            {
                KetNoiTaiKhoan objTK = new KetNoiTaiKhoan();
                List<TaiKhoan> listTK = objTK.DangNhap();
                bool isValidUser = true;

                foreach (TaiKhoan item in listTK)
                {
                    if (item.TenDangNhap.ToLower() == model.TenDangNhap.ToLower())
                    {
                        isValidUser = false;
                        TempData["ErrorMessageSignup"] = true;
                        break;
                    }

                }
                if (isValidUser == true)
                {
                    bool isRegistered = taiKhoanRepo.DangKy(model);
                    if (isRegistered)
                    {
                        TempData["SignSuccess"] = true;
                        return RedirectToAction("DangKy");
                    }
                    else
                    {
                        return View(model);
                    }
                }
            }
            else
            {

                TempData["ErrorMessage"] = true;

            }
            return View(model);

        }
        //==========================================================================================================================================
        //Xác thực Email
        //==========================================================================================================================================
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;

        public ActionResult AConfirmEmail()
        {
            return View();
        }
        public ActionResult ConfirmEmail(string email, string token)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Truy vấn để kiểm tra token và email
                string query = "SELECT COUNT(*) FROM TaiKhoan WHERE Email = @Email AND EmailConfirmationToken = @Token AND IsEmailConfirmed = 0";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add(new SqlParameter("@Email", email));
                cmd.Parameters.Add(new SqlParameter("@Token", token));

                conn.Open();
                object result = cmd.ExecuteScalar();
                conn.Close();

                // Kiểm tra xem result có phải là null hay không
                int count = result != null ? Convert.ToInt32(result) : 0;

                if (count > 0)
                {
                    // Cập nhật trạng thái xác nhận email mà không thay đổi địa chỉ email
                    string updateQuery = "UPDATE TaiKhoan SET IsEmailConfirmed = 1 WHERE Email = @Email";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                    updateCmd.Parameters.Add(new SqlParameter("@Email", email));

                    conn.Open();
                    int rowsAffected = updateCmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected > 0)
                    {
                        TempData["Message"] = "Xác nhận email thành công!";
                        //return RedirectToAction("ConfirmEmail");
                        return View();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Trạng thái xác nhận đã được cập nhật trước đó.";
                        return RedirectToAction("Error");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Liên kết xác nhận không hợp lệ.";
                    return RedirectToAction("Error");
                }
            }
        }


        //==========================================================================================================================================
        //Đăng Nhập Tài Khoản
        //==========================================================================================================================================

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(TaiKhoan model)
        {
            KetNoiTaiKhoan objTK = new KetNoiTaiKhoan();
            List<TaiKhoan> listTK = objTK.DangNhap();

            XuLyThongTinKhachHang objKH = new XuLyThongTinKhachHang();
            List<KhachHang> listKH = objKH.LayThongTinKhachHang();

            XuLyThongTinNhanVien objNV = new XuLyThongTinNhanVien();
            List<NhanVien> listNV = objNV.LayThongTinNhanVien();
            bool isValidUser = false;
            if (model.TenDangNhap != null)
            {
                foreach (TaiKhoan item in listTK)
                {
                    if (item.TenDangNhap.ToLower() == model.TenDangNhap.ToLower() && item.MatKhau == model.MatKhau)
                    {
                        if (item.IsEmailConfirmed == 0)
                        {
                            TempData["ErrorMessageComfirm"] = "Tài khoản chưa được xác thực qua email.";
                            return View(model);
                        }
                        Session["MaTaiKhoan"] = item.MaTaiKhoan;
                        Session["MaQuyen"] = item.MaQuyen;
                        foreach(KhachHang item2 in listKH)
                        {
                            if (item.MaTaiKhoan == item2.MaTaiKhoan)
                            {                              
                                Session["HoTen"] = item2.HoTen;
                                Session["Avatar"] = item2.Avatar; 
                                Session["MaKH"] = item2.MaKH;
                                Session["NgaySinh"] = item2.NgaySinh;
                                Session["SoDT"] = item2.SoDT;
                                Session["DiemThanhVien"] = item2.DiemThanhVien;
                            }
                        }
                        foreach (NhanVien item2 in listNV)
                        {
                            if (item.MaTaiKhoan == item2.MaTaiKhoan)
                            {
                                Session["HoTen"] = item2.HoTen;
                                Session["MaNV"] = item2.MaNV;
                                Session["SoDT"] = item2.SoDT;
                            }
                        }
                        foreach (TaiKhoan item3 in listTK)
                        {
                            if (item.MaTaiKhoan == item3.MaTaiKhoan)
                            {
                                Session["Mail"] = item3.Email;                           
                            }
                        }
                        //Session["FullName"] = item.HoTen;
                        //Session["Avatar"] = item.Avatar; // Nếu có cột avatar
                        //Session["CustomerID"] = khachHang.MaKH;
                        //Session["Role"] = item.MaQuyen;
                        //Session["Username"] = item.TenDangNhap;
                        isValidUser = true; // Đặt isValidUser thành true
                        break;
                    }
                }
            }

            if (isValidUser)
            {
                if (Session["MaQuyen"].ToString() == "Q001      ")
                {
                    TempData["LoginSuccess"] = true; // Thiết lập LoginSuccess
                    return RedirectToAction("DangNhap");
                }
                else
                {
                    return RedirectToAction("DanhSachMonAn", "Admin_MonAn");
                }
                    
            }
            else
            {
                TempData["ErrorMessage"] = "Thông tin đăng nhập không chính xác.";
                return View(model);
            }
        }
        //==========================================================================================================================================
        //Xác thực ma OTP
        //==========================================================================================================================================
        public ActionResult RequestOtp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RequestOtp(TaiKhoan taiKhoan)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int count = 0;
                KetNoiTaiKhoan objTK = new KetNoiTaiKhoan();
                List<TaiKhoan> listTK = objTK.DangNhap();
                // Kiểm tra email có tồn tại không
                foreach (TaiKhoan item in listTK)
                {
                    if (item.Email.ToLower() == taiKhoan.Email.ToLower())
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    TempData["ErrorMessage"] = "Email không tồn tại.";
                    return View();
                }

                // Tạo mã OTP ngẫu nhiên
                string otp = new Random().Next(100000, 999999).ToString();

                // Lưu OTP vào cơ sở dữ liệu với thời gian hết hạn
                string query = "UPDATE TaiKhoan SET OTP = @OTP WHERE Email = @Email";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add(new SqlParameter("@OTP", otp)); // Gán giá trị cho OTP
                cmd.Parameters.Add(new SqlParameter("@Email", taiKhoan.Email)); // Gán giá trị cho Email


                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close();

                // Gửi OTP qua email
                objTK.SendOtpEmail(taiKhoan.Email, otp);

                TempData["SuccessMessageOTP"] = "Mã OTP đã được gửi đến email của bạn.";
                return RedirectToAction("ResetPassword");

            }

        }
        //==========================================================================================================================================
        //Thay đổi mật khẩu
        //==========================================================================================================================================
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(TaiKhoan model)
        {
            int sum = 0;

            if (model.TenDangNhap == null || model.MatKhau == null || model.XacNhanMatKhau == null || model.otp == 0)
            {
                if (sum > 0)
                    TempData["ErrorMessage"] = "Thông tin đăng nhập không chính xác.";
                sum++;
                return View(model);
            }
            else
            {
                if (model.MatKhau.Count() < 6)
                {
                    return View(model);
                }
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    // Truy vấn kiểm tra tên đăng nhập và OTP

                    string query = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap AND OTP = @OTP";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add(new SqlParameter("@TenDangNhap",model.TenDangNhap));
                    cmd.Parameters.Add(new SqlParameter("@OTP", model.otp));

                    conn.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();

                    if (count > 0)
                    {
                        // Kiểm tra mật khẩu mới và xác nhận mật khẩu
                        if (model.MatKhau == model.XacNhanMatKhau)
                        {
                            // Cập nhật mật khẩu mới
                            string updateQuery = "UPDATE TaiKhoan SET MatKhau = @MatKhau WHERE TenDangNhap = @TenDangNhap";
                            SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                            updateCmd.Parameters.Add(new SqlParameter("@MatKhau", model.MatKhau));
                            updateCmd.Parameters.Add(new SqlParameter("@TenDangNhap",model.TenDangNhap));

                            conn.Open();
                            int rowsAffected = updateCmd.ExecuteNonQuery();
                            conn.Close();

                            if (rowsAffected > 0)
                            {
                                TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
                                return RedirectToAction("ResetPassword");
                            }
                            else
                            {
                                ModelState.AddModelError("", "Có lỗi xảy ra khi đổi mật khẩu.");
                            }
                        }

                    }
                    else
                    {
                        TempData["ErrorMessageOTP"] = "Tên đăng nhập hoặc mã OTP không chính xác.";
                    }
                }
            }
            return View(model);
        }
        //==========================================================================================================================================
        //Trang chủ
        public ActionResult Welcome(int id = 0, int cart = -1)
        {
            var count = TempData["Cart"];
            ViewBag.CartCount = count;
            if (cart == 0 && id == -1)
            {
                ViewBag.CartCount = 0;
            }
            return View();
        }

        //==========================================================================================================================================




    }
}