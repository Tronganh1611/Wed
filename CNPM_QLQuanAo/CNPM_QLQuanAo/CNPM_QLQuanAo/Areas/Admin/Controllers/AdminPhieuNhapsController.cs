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
    public class AdminPhieuNhapsController : Controller
    {
        private QLBHEntities1 db = new QLBHEntities1();

        // GET: Admin/AdminPhieuNhaps
        public ActionResult Index()
        {
            var phieuNhaps = db.PhieuNhaps.Include(p => p.NHAN_VIEN);
            return View(phieuNhaps.ToList());
        }

        // GET: Admin/AdminPhieuNhaps/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhap phieuNhap = db.PhieuNhaps.Find(id);
            if (phieuNhap == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhap);
        }

        // GET: Admin/AdminPhieuNhaps/Create
        public ActionResult Create(String idSP)
        {
            ViewBag.idSP = idSP;
            ViewBag.MaNV = new SelectList(db.NHAN_VIEN, "MaNV", "TenNV");
            return View();
        }

        // POST: Admin/AdminPhieuNhaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhieu,MaNV,NgayNhap,TenNhaCung,DiaChi")] PhieuNhap phieuNhap)
        {
            if (ModelState.IsValid)
            {
                db.PhieuNhaps.Add(phieuNhap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNV = new SelectList(db.NHAN_VIEN, "MaNV", "TenNV", phieuNhap.MaNV);
            return View(phieuNhap);
        }

        // GET: Admin/AdminPhieuNhaps/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhap phieuNhap = db.PhieuNhaps.Find(id);
            if (phieuNhap == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNV = new SelectList(db.NHAN_VIEN, "MaNV", "TenNV", phieuNhap.MaNV);
            return View(phieuNhap);
        }

        // POST: Admin/AdminPhieuNhaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhieu,MaNV,NgayNhap,TenNhaCung,DiaChi")] PhieuNhap phieuNhap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuNhap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNV = new SelectList(db.NHAN_VIEN, "MaNV", "TenNV", phieuNhap.MaNV);
            return View(phieuNhap);
        }

        // GET: Admin/AdminPhieuNhaps/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhap phieuNhap = db.PhieuNhaps.Find(id);
            if (phieuNhap == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhap);
        }

        // POST: Admin/AdminPhieuNhaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PhieuNhap phieuNhap = db.PhieuNhaps.Find(id);
            db.PhieuNhaps.Remove(phieuNhap);
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
