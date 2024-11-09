using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CNPM_QLQuanAo.Models;

namespace CNPM_QLQuanAo.Controllers
{
    public class SanPhamsController : Controller
    {
        private QLBHEntities1 db = new QLBHEntities1();

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "SanPhams");
        }

        string LayMaHD()
        {
            var maMax = db.HOA_DON.Select(n => n.MaHD).OrderByDescending(ma => ma).FirstOrDefault();

            if (maMax != null)
            {
                int maSach = int.Parse(maMax.Substring(3)) + 1;
                string newMaSach = "MHD" + maSach.ToString("000");
                return newMaSach;
            }

            return "MHD001";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuyProduct(string MaSP, string TenLogin_KH, int soLuong)
        {
            var product = db.SANPHAMs.Find(MaSP);
            var customer = db.KHACH_HANG.FirstOrDefault(k => k.TenKH == TenLogin_KH);
            var nhanvien = db.NHAN_VIEN.FirstOrDefault();

            if (product == null)
            {
                return HttpNotFound("Product not found.");
            }

            if (customer == null)
            {
                return HttpNotFound("Customer not found.");
            }

            if (nhanvien == null)
            {
                return HttpNotFound("Employee not found.");
            }

            // giảm số lượng sản phẩm đó trong kho
            product.SoLuong -= soLuong;
            db.SaveChanges();

            // tạo một hóa đơn mới
            HOA_DON hoaDon = new HOA_DON
            {
                MaHD = LayMaHD(), 
                MaKH = customer.MaKH,
                MaNV = nhanvien.MaNV,
                NgayDat = DateTime.Now.ToString("yyyy-MM-dd"),
                TrangThai = false
            };
            db.HOA_DON.Add(hoaDon);
            db.SaveChanges();

            // tạo một chi tiết hóa đơn mới
            ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon
            {
                MaHD = hoaDon.MaHD,
                MaSP = MaSP,
                SoLuong = soLuong
            };
            db.ChiTietHoaDons.Add(chiTietHoaDon);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = MaSP });
        }


        public ActionResult ShowProductPrice(int minPrice, int maxPrice)
        {
            if (Session["TenLogin_KH"] != null)
            {
                ViewBag.TenLogin_KH = Session["TenLogin_KH"];
            }
            var sanPhams = db.SANPHAMs.SqlQuery("SELECT * FROM SANPHAM WHERE Gia < @maxPrice AND Gia >= @minPrice",
                            new SqlParameter("@maxPrice", maxPrice),
                            new SqlParameter("@minPrice", minPrice));

            return View(sanPhams.ToList());
        }

        // GET: SanPhams
        public ActionResult ShowProductSize(String size)
        {
            if (Session["TenLogin_KH"] != null)
            {
                ViewBag.TenLogin_KH = Session["TenLogin_KH"];
            }
            var sanPhams = db.SANPHAMs.SqlQuery("Select * from SANPHAM where Size like '" + size + "'");
            return View(sanPhams.ToList());
        }
        public ActionResult Index()
        {
            if (Session["TenLogin_KH"] != null)
            {
                ViewBag.TenLogin_KH = Session["TenLogin_KH"];
            }
            var sANPHAMs = db.SANPHAMs.Include(s => s.LoaiSP);
            return View(sANPHAMs.ToList());
        }

        public ActionResult AboutUs()
        {
            if (Session["TenLogin_KH"] != null)
            {
                ViewBag.TenLogin_KH = Session["TenLogin_KH"];
            }
            return View();
        }

        public ActionResult Contact()
        {
            if (Session["TenLogin_KH"] != null)
            {
                ViewBag.TenLogin_KH = Session["TenLogin_KH"];
            }
            return View();
        }

        public ActionResult Blog()
        {
            if (Session["TenLogin_KH"] != null)
            {
                ViewBag.TenLogin_KH = Session["TenLogin_KH"];
            }
            return View();
        }

        public ActionResult Shop()
        {
            if (Session["TenLogin_KH"] != null)
            {
                ViewBag.TenLogin_KH = Session["TenLogin_KH"];
            }
            var sANPHAMs = db.SANPHAMs.Include(s => s.LoaiSP);
            return View(sANPHAMs.ToList());
        }

        [HttpPost]
        public ActionResult Shop(String TenSP)
        {
            var sanPham = db.SANPHAMs.Where(s => s.TenSP.Contains(TenSP)).ToList();
            return View(sanPham);
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(string id)
        {
            if (Session["TenLogin_KH"] != null)
            {
                ViewBag.TenLogin_KH = Session["TenLogin_KH"];
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaLSP = new SelectList(db.LoaiSPs, "MaLSP", "TenLoai");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,MaLSP,TenSP,SoLuong,Size,Mau,Gia,AnhSP,TrangThai,MoTa")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLSP = new SelectList(db.LoaiSPs, "MaLSP", "TenLoai", sANPHAM.MaLSP);
            return View(sANPHAM);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLSP = new SelectList(db.LoaiSPs, "MaLSP", "TenLoai", sANPHAM.MaLSP);
            return View(sANPHAM);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,MaLSP,TenSP,SoLuong,Size,Mau,Gia,AnhSP,TrangThai,MoTa")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLSP = new SelectList(db.LoaiSPs, "MaLSP", "TenLoai", sANPHAM.MaLSP);
            return View(sANPHAM);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            db.SANPHAMs.Remove(sANPHAM);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
