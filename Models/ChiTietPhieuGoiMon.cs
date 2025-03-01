using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace QL_NhaHang_ADO.Models
{
    public class ChiTietPhieuGoiMon
    {
        public string MaMonAn { get; set; }
        public string MaPGM { get; set; }
        public int SoLuong { get; set; }
        public string TenMon { get; set; }
        public int DonGia { get; set; }
        public int ThanhTien { get; set; }
    }
}