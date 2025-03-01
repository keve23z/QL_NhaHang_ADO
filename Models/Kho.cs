using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QL_NhaHang_ADO.Models
{

        public class PhieuNhapKho
        {
            public string MaNhapKho { get; set; }
            public string MaNV { get; set; }
            public DateTime NgayNhapKho { get; set; }
            public int TongTien { get; set; }
        }
    public class PhieuXuatKho
    {
        public string MaXuatKho { get; set; }
        public string MaNV { get; set; }
        public DateTime NgayXuatKho { get; set; }
        public int TongTien { get; set; }
    }
    public class ChiTietNhapKho
        {
            public string MaNhapKho { get; set; }
            public string MaNguyenLieu { get; set; }
            public int SoLuong { get; set; }
            public int ThanhTien { get; set; }
        }

        public class ChiTietXuatKho
        {
            public string MaXuatKho { get; set; }
            public string MaNguyenLieu { get; set; }
            public int SoLuong { get; set; }
            public int ThanhTien { get; set; }
        }

    public class NguyenLieu
        {
            public string MaNguyenLieu { get; set; }
            public int SoLuongTon { get; set; }
            public string TenNguyenLieu { get; set; }
            public int DonGia { get; set; }
            public string DVT { get; set; }
        }
    
}