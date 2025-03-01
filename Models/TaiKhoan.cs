using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QL_NhaHang_ADO.Models
{
    public class TaiKhoan
    {
        public string MaTaiKhoan { get; set; }
        //--------------------------------------------------------------------------------------------
        public string MaQuyen { get; set; }
        //--------------------------------------------------------------------------------------------
        [Required(ErrorMessage = "*Tên đăng nhập không được để trống")]
        [StringLength(15, ErrorMessage = "Username không nhiều hơn 15 ký tự")]
        public string TenDangNhap { get; set; }
        //--------------------------------------------------------------------------------------------
        [Required(ErrorMessage = "*Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "*mật khẩu phải dài hơn 6 ký tự", MinimumLength = 6)]
        public string MatKhau { get; set; }
        //--------------------------------------------------------------------------------------------
        [Compare("MatKhau", ErrorMessage = "*Mật khẩu xác nhận không trùng")]
        [DataType(DataType.Password)]
        public string XacNhanMatKhau { get; set; }
        //--------------------------------------------------------------------------------------------
        [Required(ErrorMessage = "*Email không được để trống")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        //--------------------------------------------------------------------------------------------
        [Required(ErrorMessage = "*Mã xác minh không được để trống")]
        public long otp { get; set; }
        //--------------------------------------------------------------------------------------------
        public int IsEmailConfirmed { get; set; }
    }
}