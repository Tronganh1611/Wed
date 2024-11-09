using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CNPM_QLQuanAo.Models;

namespace CNPM_QLQuanAo.Areas.Admin.Controllers
{
    public class AdminChiTietPhieuNhapsController : Controller
    {
        private QLBHEntities1 db = new QLBHEntities1();
        string LayMaSP()
        {
            var maMax = db.SANPHAMs.Select(n => n.MaSP).OrderByDescending(ma => ma).FirstOrDefault();

            if (maMax != null)
            {
                int maSach = int.Parse(maMax.Substring(3)) + 1;
                string newMaSach = "MSP" + maSach.ToString("000");
                return newMaSach;
            }

            return "MSP001";
        }

        string LayMaPN()
        {
            var maMax = db.PhieuNhaps.Select(n => n.MaPhieu).OrderByDescending(ma => ma).FirstOrDefault();

            if (maMax != null)
            {
                int maSach = int.Parse(maMax.Substring(3)) + 1;
                string newMaSach = "MPN" + maSach.ToString("000");
                return newMaSach;
            }

            return "MPN001";
        }

        // GET: Admin/AdminChiTietPhieuNhaps
        public ActionResult Index()
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }
            var chiTietPhieuNhaps = db.ChiTietPhieuNhaps.Include(c => c.PhieuNhap).Include(c => c.SANPHAM);
            return View(chiTietPhieuNhaps.ToList());
        }

        // GET: Admin/AdminChiTietPhieuNhaps/Details/5
        public ActionResult Details(string MaPhieu, string MaSP)
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }
            if (MaPhieu == null && MaSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuNhap chiTietPhieuNhap = db.ChiTietPhieuNhaps.SingleOrDefault(c => c.MaPhieu.Contains(MaPhieu) && c.MaSP.Contains(MaSP));
            if (chiTietPhieuNhap == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPhieuNhap);
        }

        // GET: Admin/AdminChiTietPhieuNhaps/Create
        public ActionResult Create(int soLuong, string MaSP)
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }
            String tenNhanVien = Session["TenLogin_NV"].ToString();
            var nhanVien = db.NHAN_VIEN.FirstOrDefault(nv => nv.TenNV == tenNhanVien);
            ViewBag.MaNV = nhanVien.MaNV;
            ViewBag.MaSP = MaSP;
            ViewBag.MaPhieu = LayMaPN();
            ViewBag.SoLuong = soLuong;
            ViewBag.MaLSP = new SelectList(db.LoaiSPs, "MaLSP", "TenLoai");
            return View();
        }

        // POST: Admin/AdminChiTietPhieuNhaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChiTietPhieuNhap chiTietPhieuNhap)
        {
            string maNV = Request.Form["MaNV"];
            string maSP = Request.Form["MaSP"];
            int soluong = int.Parse(Request.Form["SoLuong"]);

            if (ModelState.IsValid)
            {
                PhieuNhap phieuNhap = new PhieuNhap();

                if (TryUpdateModel(phieuNhap, "PhieuNhap", new[] { "MaPhieu", "MaNV", "NgayNhap", "TenNhaCung", "DiaChi" }))
                {
                    phieuNhap.MaPhieu = LayMaPN();
                    phieuNhap.MaNV = maNV;
                    phieuNhap.NgayNhap = DateTime.Now.ToString("yyyy-MM-dd");
                    db.PhieuNhaps.Add(phieuNhap);
                    db.SaveChanges();
                }

                ChiTietPhieuNhap chiTietPhieu = new ChiTietPhieuNhap();
                chiTietPhieu.MaPhieu = phieuNhap.MaPhieu;
                chiTietPhieu.MaSP = maSP;
                chiTietPhieu.MaSP = maSP;
                chiTietPhieu.SoLuong = soluong;
                chiTietPhieu.GiaGoc = chiTietPhieuNhap.GiaGoc;
                db.ChiTietPhieuNhaps.Add(chiTietPhieu);
                db.SaveChanges();
                return RedirectToAction("Index", "AdminSanPham");
            }

            ViewBag.MaPhieu = new SelectList(db.PhieuNhaps, "MaPhieu", "MaNV", chiTietPhieuNhap.MaPhieu);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "MaLSP", chiTietPhieuNhap.MaSP);
            return View(chiTietPhieuNhap);
        }

        // GET: Admin/AdminChiTietPhieuNhaps/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuNhap chiTietPhieuNhap = db.ChiTietPhieuNhaps.Find(id);
            if (chiTietPhieuNhap == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaPhieu = new SelectList(db.PhieuNhaps, "MaPhieu", "MaNV", chiTietPhieuNhap.MaPhieu);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "MaLSP", chiTietPhieuNhap.MaSP);
            return View(chiTietPhieuNhap);
        }

        // POST: Admin/AdminChiTietPhieuNhaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhieu,MaSP,SoLuong,GiaGoc")] ChiTietPhieuNhap chiTietPhieuNhap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietPhieuNhap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaPhieu = new SelectList(db.PhieuNhaps, "MaPhieu", "MaNV", chiTietPhieuNhap.MaPhieu);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "MaLSP", chiTietPhieuNhap.MaSP);
            return View(chiTietPhieuNhap);
        }

        // GET: Admin/AdminChiTietPhieuNhaps/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuNhap chiTietPhieuNhap = db.ChiTietPhieuNhaps.Find(id);
            if (chiTietPhieuNhap == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPhieuNhap);
        }

        // POST: Admin/AdminChiTietPhieuNhaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ChiTietPhieuNhap chiTietPhieuNhap = db.ChiTietPhieuNhaps.Find(id);
            db.ChiTietPhieuNhaps.Remove(chiTietPhieuNhap);
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
