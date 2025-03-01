using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QL_NhaHang_ADO.Models
{
    public class MaGiamGia
    {
        public string MaGiamGia_Code { get; set; }
        public DateTime NgayBD { get; set; }
        public DateTime NgayKT { get; set; }
        public int SoLuong { get; set; }
        public int SoTien { get; set; }
    }

 }