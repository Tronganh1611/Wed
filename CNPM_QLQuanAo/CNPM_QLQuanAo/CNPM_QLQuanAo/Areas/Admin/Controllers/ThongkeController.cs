using CNPM_QLQuanAo.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CNPM_QLQuanAo.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        private QLBHEntities1 db = new QLBHEntities1();

        public ActionResult ThongKeBanHang(int? thang, int? nam)
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }
            if (thang == null || nam == null)
            {
                thang = DateTime.Now.Month;
                nam = DateTime.Now.Year;
            }

            var startDate = new DateTime(nam.Value, thang.Value, 1);
            var endDate = startDate.AddMonths(1);

            var hoaDons = db.HOA_DON
                .ToList()
                .Where(h =>
                {
                    DateTime ngayDat;
                    if (DateTime.TryParse(h.NgayDat, out ngayDat))
                    {
                        return ngayDat >= startDate && ngayDat < endDate;
                    }
                    return false;
                })
                .Select(h => new HoaDonViewModel
                {
                    MaHD = h.MaHD,
                    NgayTao = h.NgayDat,
                    TongTien = h.ChiTietHoaDons.Sum(ct => (ct.SoLuong ?? 0) * (ct.SANPHAM.Gia ?? 0))
                }).ToList();

            ViewBag.Thang = thang;
            ViewBag.Nam = nam;
            ViewBag.TongDoanhThu = hoaDons.Sum(hd => hd.TongTien);

            return View(hoaDons);
        }
        public  ActionResult InThongke(int? thang, int? nam)
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }
            if (thang == null || nam == null)
            {
                thang = DateTime.Now.Month;
                nam = DateTime.Now.Year;
            }

            var startDate = new DateTime(nam.Value, thang.Value, 1);
            var endDate = startDate.AddMonths(1);
            var maNhanVien = "MNV001";
            var tenNguoiThongKe = db.NHAN_VIEN.FirstOrDefault(nv => nv.MaNV == maNhanVien)?.TenNV;
            ViewBag.TenNguoiThongKe = tenNguoiThongKe;

            var hoaDons = db.HOA_DON
                .ToList()
                .Where(h =>
                {
                    DateTime ngayDat;
                    if (DateTime.TryParse(h.NgayDat, out ngayDat))
                    {
                        return ngayDat >= startDate && ngayDat < endDate;
                    }
                    return false;
                })
                .Select(h => new HoaDonViewModel
                {
                    MaHD = h.MaHD,
                    NgayTao = h.NgayDat,
                    TongTien = h.ChiTietHoaDons.Sum(ct => (ct.SoLuong ?? 0) * (ct.SANPHAM.Gia ?? 0))
                }).ToList();

            ViewBag.Thang = thang;
            ViewBag.Nam = nam;
            ViewBag.TongDoanhThu = hoaDons.Sum(hd => hd.TongTien);

            return View(hoaDons);
        }
    }

    public class HoaDonViewModel
    {
        public string MaHD { get; set; }
        public string NgayTao { get; set; }
        public int TongTien { get; set; }
    }
}
