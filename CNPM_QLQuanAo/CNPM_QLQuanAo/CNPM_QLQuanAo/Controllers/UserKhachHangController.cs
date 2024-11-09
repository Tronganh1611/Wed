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
    public class UserKhachHangController : Controller
    {
        private QLBHEntities1 db = new QLBHEntities1();

        // GET: UserKhachHang
        public ActionResult Index()
        {
            return View(db.KHACH_HANG.ToList());
        }

        // GET: UserKhachHang/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACH_HANG kHACH_HANG = db.KHACH_HANG.Find(id);
            if (kHACH_HANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACH_HANG);
        }

        // GET: UserKhachHang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserKhachHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,TenKH,SDT,DiaChi,Email,MatKhau,GioiTinh")] KHACH_HANG kHACH_HANG)
        {
            if (ModelState.IsValid)
            {
                db.KHACH_HANG.Add(kHACH_HANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kHACH_HANG);
        }

        // GET: UserKhachHang/Edit/5
        public ActionResult Edit()
        {
            if (Session["TenLogin_KH"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string tenKh = Session["TenLogin_KH"].ToString();
            ViewBag.TenLogin_KH = tenKh;

            KHACH_HANG kHACH_HANG = db.KHACH_HANG.SingleOrDefault(k => k.TenKH == tenKh);

            if (kHACH_HANG == null)
            {
                return HttpNotFound();
            }

            return View(kHACH_HANG);
        }


        // POST: UserKhachHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,TenKH,SDT,DiaChi,Email,MatKhau,GioiTinh")] KHACH_HANG kHACH_HANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHACH_HANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "SanPhams");
            }
            return View(kHACH_HANG);
        }

        // GET: UserKhachHang/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACH_HANG kHACH_HANG = db.KHACH_HANG.Find(id);
            if (kHACH_HANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACH_HANG);
        }

        // POST: UserKhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KHACH_HANG kHACH_HANG = db.KHACH_HANG.Find(id);
            db.KHACH_HANG.Remove(kHACH_HANG);
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
