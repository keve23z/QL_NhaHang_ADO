using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace QL_NhaHang_ADO.Models
{
    public class MonAn
    {
        public string connString = ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;
        public string MaMonAn { get; set; }    
        public string TenMon { get; set; }
        public string LoaiMon { get; set; }
        public float Gia { get; set; }
        public string AnhMon { get; set; }
        public string MoTa { get; set; }
    }
}