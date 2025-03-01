using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace QL_NhaHang_ADO.Models
{
    public class HoaDon
    {
        [Display(Name = "Mã Hóa Đơn")]
        public string Ma { get; set; }

        [Display(Name = "Mã Bàn")]
        public string MaBan { get; set; }

        [Display(Name = "Mã Khách Hàng")]
        [Required(ErrorMessage = "Mã khách hàng không được để trống.")]
        public string MaKH { get; set; }

        [Display(Name = "Nhân Viên")]
        public string MaNV { get; set; }

        [Display(Name = "Giảm Giá")]
        public string MaGiamGia { get; set; }

        [Required(ErrorMessage = "Ngày lập không được để trống")]
        [DataType(DataType.Date)]
        public DateTime? NgayLap { get; set; }

        public int TongTien { get; set; }

        [Display(Name = "Hình Thức")]
        public string HinhThuc { get; set; }

        public string TenKhachHang { get; set; }

        public int GiaGiam { get; set; }
        public HoaDon() { }

        public HoaDon(string ma, string maBan, string maKH, string maNV, string maGiamGia, DateTime ngayLap, string hinhThuc)
        {
            Ma = ma;
            MaBan = maBan;
            MaKH = maKH;
            MaNV = maNV;
            MaGiamGia = maGiamGia;
            NgayLap = ngayLap;  // Không nullable
            HinhThuc = hinhThuc;
        }
    }
}