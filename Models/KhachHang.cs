using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QL_NhaHang_ADO.Models
{
 
        public class KhachHang
        {
            public string MaKH { get; set; }       
            public string MaTaiKhoan { get; set; }  
            public string HoTen { get; set; }     
            public string NgaySinh { get; set; } 
            public string SoDT { get; set; }
        public int DiemThanhVien { get; set; } = 0;
            public string Avatar { get; set; }      
        }

}