using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CNPM_QLQuanAo.Models;

namespace CNPM_QLQuanAo.Controllers
{
    public class UserChiTietHoaDonsController : Controller
    {
        private QLBHEntities1 db = new QLBHEntities1();

        // GET: UserChiTietHoaDons
        public ActionResult Index()
        {
            string tenLoginKH = null;
            if (Session["TenLogin_KH"] != null)
            {
                ViewBag.TenLogin_KH = Session["TenLogin_KH"];
                tenLoginKH = Session["TenLogin_KH"].ToString();
            }

            // Lấy danh sách chi tiết hóa đơn theo tên khách hàng
            var chiTietHoaDons = db.ChiTietHoaDons
                .Where(s => s.HOA_DON.KHACH_HANG.TenKH == tenLoginKH && s.HOA_DON.TrangThai == false)
                .ToList();
            return View(chiTietHoaDons.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pay(string TenLogin_KH)
        {
            var customer = db.KHACH_HANG.Where(k => k.TenKH == TenLogin_KH).FirstOrDefault();
            var HoaDon = db.HOA_DON.Where(h => h.MaKH == customer.MaKH && h.TrangThai == false).ToList();
            if (customer == null)
            {
                return HttpNotFound("Customer not found.");
            }
            if (HoaDon == null)
            {
                return HttpNotFound("hoa don not found.");
            }

            foreach(HOA_DON h in HoaDon)
            {
                h.TrangThai = true;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: UserChiTietHoaDons/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHoaDon);
        }

        // GET: UserChiTietHoaDons/Create
        public ActionResult Create()
        {
            ViewBag.MaHD = new SelectList(db.HOA_DON, "MaHD", "MaNV");
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "MaLSP");
            return View();
        }

        // POST: UserChiTietHoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,MaSP,SoLuong")] ChiTietHoaDon chiTietHoaDon)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietHoaDons.Add(chiTietHoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHD = new SelectList(db.HOA_DON, "MaHD", "MaNV", chiTietHoaDon.MaHD);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "MaLSP", chiTietHoaDon.MaSP);
            return View(chiTietHoaDon);
        }

        // GET: UserChiTietHoaDons/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHD = new SelectList(db.HOA_DON, "MaHD", "MaNV", chiTietHoaDon.MaHD);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "MaLSP", chiTietHoaDon.MaSP);
            return View(chiTietHoaDon);
        }

        // POST: UserChiTietHoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,MaSP,SoLuong")] ChiTietHoaDon chiTietHoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietHoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHD = new SelectList(db.HOA_DON, "MaHD", "MaNV", chiTietHoaDon.MaHD);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "MaLSP", chiTietHoaDon.MaSP);
            return View(chiTietHoaDon);
        }

        // GET: UserChiTietHoaDons/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHoaDon);
        }

        // POST: UserChiTietHoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            db.ChiTietHoaDons.Remove(chiTietHoaDon);
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
