using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QL_NhaHang_ADO.Models
{
    public class ChiTietXuatKhoViewModel
    {
        public string MaXuatKho { get; set; }

        public string MaNguyenLieu { get; set; }
        public string TenNguyenLieu { get; set; }
        public int SoLuong { get; set; }
        public int ThanhTien { get; set; }
    }
}