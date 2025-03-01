using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QL_NhaHang_ADO.Models
{
    public class ViewThanhToan
    {
        public int SoTienSauGiamGia { get; set; }
        public HoaDon HoaDon { get; set; }
        public List<ChiTietHoaDon> ChiTietHoaDonList { get; set; }
        public double TongTien { get; set; }
    }
}