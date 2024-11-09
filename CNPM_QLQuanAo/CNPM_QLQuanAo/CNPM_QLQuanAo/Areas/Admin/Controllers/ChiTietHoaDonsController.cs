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
    public class ChiTietHoaDonsController : Controller
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

        // GET: Admin/ChiTietHoaDons
        public ActionResult Index()
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }
            var chiTietHoaDons = db.ChiTietHoaDons.Include(c => c.HOA_DON).Include(c => c.SANPHAM);
            return View(chiTietHoaDons.ToList());
        }

        // GET: Admin/ChiTietHoaDons/Details/5
        public ActionResult Details(string MaHD, string MaSP)
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }
            if (MaHD == null || MaSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.SingleOrDefault(c => c.MaHD == MaHD && c.MaSP == MaSP);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHoaDon);
        }



        // GET: Admin/ChiTietHoaDons/Create
        public ActionResult Create()
        {
            ViewBag.MaHD = new SelectList(db.HOA_DON, "MaHD", "MaNV");
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "MaLSP");
            return View();
        }

        // POST: Admin/ChiTietHoaDons/Create
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

        // GET: Admin/ChiTietHoaDons/Edit/5
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

        // POST: Admin/ChiTietHoaDons/Edit/5
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

        // GET: Admin/ChiTietHoaDons/Delete/5
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

        // POST: Admin/ChiTietHoaDons/Delete/5
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
