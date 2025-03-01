using QL_NhaHang_ADO.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static QL_NhaHang_ADO.Models.XuLyPhieuGoiMon;
using static System.Collections.Specialized.BitVector32;

namespace QL_NhaHang_ADO.Controllers
{
    public class PhieuGoiMonController : Controller
    {
        // GET: PhieuGoiMon
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DanhSachPhieuGoiMon()
        {
            XuLyPhieuGoiMon xuLyPhieuGoiMon = new XuLyPhieuGoiMon();
            List<PhieuGoiMon> listPGM = xuLyPhieuGoiMon.LayĐanhSachPhieu();
            return View(listPGM);
        }

        public ActionResult ThemPhieuGoiMon()
        {
            ViewBag.Ban = new ConnectBan().GetBanTrong(); // Ensure this method returns a valid list
            ViewBag.MaP = Guid.NewGuid().ToString().Substring(0, 10);
            return View();
        }
        public ActionResult ChiTietPhieu(string ma, string maBan)
        {
            ViewBag.MaPGM = ma;
            ViewBag.MaBan = maBan;

            XuLyPhieuGoiMon xuLyPhieuGoiMon = new XuLyPhieuGoiMon();
            var listCTPGM = xuLyPhieuGoiMon.LayChiTiet(ma);
            ViewBag.dsMonAn = listCTPGM;

            // Nếu muốn tính tổng hóa đơn và gửi qua View
            decimal tongHoaDon = listCTPGM.Sum(ct => ct.ThanhTien);
            ViewBag.TongHoaDon = tongHoaDon;
            return View(listCTPGM);
        }



        [HttpPost]
        public ActionResult ThemPhieu(FormCollection f)
        {
            PhieuGoiMon phieu = new PhieuGoiMon();
            phieu.MaPGM = f["MaPhieu"];
            phieu.MaBan = f["MaBan"];
            ViewBag.PhieuMoi = phieu;
            ViewBag.dsMonAn = new XuLyThongTinMonAn().LayThongTinMonAn();
            XuLyPhieuGoiMon xuLyPhieuGoiMon = new XuLyPhieuGoiMon();
            xuLyPhieuGoiMon.ThemPhieuMoi(phieu);
            return View("ThemPhieuGoiMon");
        }

        [HttpPost]
        public ActionResult ThemChiTietPhieuGoiMon(FormCollection f)
        {
            string maP = f["MaPhieu"];
            string maBan = f["MaBan"];
            string[] maMon = f.GetValues("MaMonAn");
            string[] soLuong = f.GetValues("SoLuong");
            new ConnectBan().XuLyTrangThaiBanThanhDay(maBan);
            List<ChiTietPhieuGoiMon> listCTPGM = new List<ChiTietPhieuGoiMon>();
            for (int i = 0; i < maMon.Length; i++)
            {
                ChiTietPhieuGoiMon ct = new ChiTietPhieuGoiMon();
                ct.MaMonAn = maMon[i];
                ct.MaPGM = maP;
                ct.SoLuong = int.Parse(soLuong[i]);
                listCTPGM.Add(ct);
            }
            XuLyPhieuGoiMon xuLyPhieuGoiMon = new XuLyPhieuGoiMon();
            xuLyPhieuGoiMon.ThemChiTietPhieu(maP, listCTPGM);
            return RedirectToAction("DanhSachPhieuGoiMon");
        }

        public ActionResult XoaPhieu(string ma)
        {
            string x = ma;
            new XuLyPhieuGoiMon().XoaChiTiet(ma);
            new XuLyPhieuGoiMon().XoaPhieu(ma);
            return RedirectToAction("DanhSachPhieuGoiMon");
        }

        [HttpPost]
        public ActionResult XuatHoaDon(FormCollection f)
        {
            string maP = f["MaPhieu"];
            ViewBag.MaP = maP;
            KhachHang kh = new KhachHang();
            string sdt = f["SDT_Khach"];
            if (sdt == "") sdt = "0000000000";
            string MaGG = f["MaGiamGia"];
            kh = new XuLyThongTinKhachHang().LayThongTinTuSDT(sdt);
            HoaDon hoaDon = new HoaDon();
            hoaDon.Ma = GenerateMaHoaDon();
            hoaDon.TongTien = int.Parse(f["Tong"]);
            hoaDon.MaBan = f["MaBan"];
            if (kh != null) hoaDon.MaKH = kh.MaKH;
            else hoaDon.MaKH = null;
            hoaDon.MaNV = f["MaNV"];
            if(f["MaGiamGia"] == "") hoaDon.MaGiamGia = "No";
            else hoaDon.MaGiamGia = f["MaGiamGia"];
            hoaDon.NgayLap = DateTime.Now;
            hoaDon.HinhThuc = f["PhuongThuc"];
            XuLyGiamGia xl = new XuLyGiamGia();
            int gia = xl.LayGiaTriGiamGia(f["MaGiamGia"]);
            hoaDon.GiaGiam = (int)(hoaDon.TongTien - gia);
            string[] maMA = f.GetValues("MaMonAn[]");
            string[] soluong = f.GetValues("SoLuong[]");
            List<ChiTietHoaDon> listCTHD = new List<ChiTietHoaDon>();
            for (int i = 0; i < maMA.Length; i++)
            {
                XuLyThongTinMonAn check = new XuLyThongTinMonAn();
                check.LayTheoMa(maMA[i]);
                listCTHD.Add(new ChiTietHoaDon
                {
                    MaHD = hoaDon.Ma,
                    MaMA = maMA[i],
                    SoLuong = int.Parse(soluong[i]),
                    GiaMon = (int)check.LayTheoMa(maMA[i]).Gia,
                    TenMon = check.LayTheoMa(maMA[i]).TenMon,
                    ThanhTien = (int)check.LayTheoMa(maMA[i]).Gia * int.Parse(soluong[i])
                });
            }
            ViewThanhToan view = new ViewThanhToan
            {
                HoaDon = hoaDon,
                ChiTietHoaDonList = listCTHD,
                TongTien = hoaDon.TongTien,
                SoTienSauGiamGia = hoaDon.GiaGiam
            };
            ViewBag.kh = kh;
            ViewBag.MaGG = MaGG;
            ViewBag.TG = DateTime.Now;
            ViewBag.listM = view.ChiTietHoaDonList;
            ViewBag.view = view;
            return View(); // Đảm bảo View nhận đúng đối tượng 'ViewThanhToan'
        }

        //<form method = "post" action="@Url.Action()">
        //        <input type = "hidden" name="MaHD" value="@ViewBag.view.HoaDon.Ma" />
        //        <input type = "hidden" name="MaBan" value="@ViewBag.view.HoaDon.MaBan" />
        //        <input type = "hidden" name="MaKH" value="@ViewBag.view.HoaDon.MaKH" />
        //        <input type = "hidden" name="MaNV" value="@Session["MaNV"]" />
        //        <input type = "hidden" name="TongTien" value="@ViewBag.view.TongTien" />
        //        <input type = "hidden" name="MaGG" value="@ViewBag.MaGG" />
        //        <input type = "hidden" name="HinhThuc" value="@ViewBag.view.HoaDon.HinhThuc" />
        //        <input type = "hidden" name="ThoiGiam" value="@ViewBag.TG" />
        //        <input type = "hidden" name="GiaGiam" value="@ViewBag.view.SoTienSauGiamGia" />
        //        @foreach(var cthd in ViewBag.listM)
        //{
        //            < input type = "hidden" name = "MaMonAn[]" value = "@cthd.MaMA" />
        //            < input type = "hidden" name = "SoLuong[]" value = "@cthd.SoLuong" />
        //            < input type = "hidden" name = "ThanhTien[]" value = "@cthd.ThanhTien" />
        //        }
        //        <a href = "@Url.Action("InHoaDon", "PhieuGoiMon")" class="btn btn-primary">Xác Nhận Thanh Toán</a>
        //    </form>

        // thêm vào csdl
        public int GetNextNumberFromDatabase()
        {
            int nextNumber = 1; // Giá trị mặc định nếu không có dữ liệu trong bảng
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connect1"].ConnectionString; // Chuỗi kết nối tới Oracle

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Truy vấn số thứ tự lớn nhất hiện có
                string query = "SELECT MAX(CAST(SUBSTRING(MaHoaDon, 3, LEN(MaHoaDon) - 2) AS INT)) AS MaxValue FROM HoaDon;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    var result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        nextNumber = Convert.ToInt32(result) + 1; // Số tiếp theo
                    }
                }
            }

            return nextNumber;
        }
        public string GenerateMaHoaDon()
        {
            string prefix = "HD";
            int nextNumber = GetNextNumberFromDatabase(); // Hàm để lấy số tiếp theo từ CSDL
            string maTaiKhoan = prefix + nextNumber.ToString("D4"); // Format thành 'TK0001'

            return maTaiKhoan;
        }
        [HttpPost]
        public ActionResult ThanhToan(FormCollection f)
        {
            string maP = f["MaPh"];
            HoaDon hd = new HoaDon();
            hd.Ma = f["MaHD"];
            hd.MaBan = f["MaBan"];
            hd.MaKH = f["MaKH"];
            hd.MaNV = f["MaNV"];
            hd.MaGiamGia = f["MaGG"];
            if(hd.MaGiamGia == "") hd.MaGiamGia = "No";
            hd.NgayLap = DateTime.Parse(f["ThoiGian"]);
            hd.TongTien = int.Parse(f["TongTien"]);
            hd.HinhThuc = f["HinhThuc"];
            hd.GiaGiam = int.Parse(f["GiaGiam"]);
            List<ChiTietHoaDon> listCTHD = new List<ChiTietHoaDon>();
            string[] maMA = f.GetValues("MaMonAn[]");
            string[] soLuong = f.GetValues("SoLuong[]");
            string[] thanhTien = f.GetValues("ThanhTien[]");
            for (int i = 0; i < maMA.Length; i++)
            {
                listCTHD.Add(new ChiTietHoaDon
                {
                    MaHD = hd.Ma,
                    MaMA = maMA[i],
                    SoLuong = int.Parse(soLuong[i]),
                    ThanhTien = int.Parse(thanhTien[i])
                });
            }
            new XuLyThanhToan().XuLyHoaDon(listCTHD, hd);
            new XuLyPhieuGoiMon().XoaChiTiet(maP);
            new XuLyPhieuGoiMon().XoaPhieu(maP);
            new XuLyGiamGia().GiamSoLuongMaKhiThanhToan(hd.MaGiamGia);
            new ConnectBan().XuLyTrangThaiBanThanhTrong(hd.MaBan);
            return RedirectToAction("DanhSachPhieuGoiMon");

        }
    }
}