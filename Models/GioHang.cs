using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QL_NhaHang_ADO.Models
{
    public class GioHang
    {

        public string MaMonAn { get; set; }
        public string TenMonAn { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public string AnhMon { get; set; }
        public double ThanhTien => SoLuong * DonGia; // Tính thành tiền
    }
}