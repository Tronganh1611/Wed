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
    public class AdminNHAN_VIENController : Controller
    {
        private QLBHEntities1 db = new QLBHEntities1();

        // GET: Admin/AdminNHAN_VIEN
        string LayMaNV()
        {
            var maMax = db.NHAN_VIEN.Select(n => n.MaNV).OrderByDescending(ma => ma).FirstOrDefault();

            if (maMax != null)
            {
                int ma = int.Parse(maMax.Substring(3)) + 1;
                string newMaNV = "MNV" + ma.ToString("000");
                return newMaNV;
            }

            return "MNV001";
        }
        public ActionResult Index()
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }
            return View(db.NHAN_VIEN.ToList());
        }
        [HttpPost]
        public ActionResult Index(string tenNV)
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }
            if (!string.IsNullOrEmpty(tenNV))
            {
                // Loại bỏ khoảng trắng thừa ở đầu và cuối, và thay thế nhiều khoảng trắng liên tiếp bằng một khoảng trắng
                tenNV = System.Text.RegularExpressions.Regex.Replace(tenNV.Trim(), @"\s+", " ");

                var nHAN_VIEN = db.NHAN_VIEN.Where(nv => nv.TenNV.Contains(tenNV)).ToList();
                return View(nHAN_VIEN);
            }

            return View(db.NHAN_VIEN.ToList());
        }
        
            public ActionResult Inds()
        {
            return View(db.NHAN_VIEN.ToList());
        }

        // GET: Admin/AdminNHAN_VIEN/Details/5
        public ActionResult Details(string id)
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAN_VIEN nHAN_VIEN = db.NHAN_VIEN.Find(id);
            if (nHAN_VIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHAN_VIEN);
        }

        // GET: Admin/AdminNHAN_VIEN/Create
        public ActionResult Create()
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }
            ViewBag.MaNV = LayMaNV();
            return View();
        }

        // POST: Admin/AdminNHAN_VIEN/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,TenNV,NgaySinh,GioiTinh,CMND,DiaChi,MatKhau,Email,ChucVu")] NHAN_VIEN nHAN_VIEN)
        {
            if (ModelState.IsValid)
            {
                nHAN_VIEN.MaNV = LayMaNV();
                db.NHAN_VIEN.Add(nHAN_VIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nHAN_VIEN);
        }

        // GET: Admin/AdminNHAN_VIEN/Edit/5
      
            public ActionResult Edit(string id)
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAN_VIEN nHAN_VIEN = db.NHAN_VIEN.Find(id);
            if (nHAN_VIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHAN_VIEN);
        }

        // POST: Admin/AdminNHAN_VIEN/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,TenNV,NgaySinh,GioiTinh,CMND,DiaChi,MatKhau,Email,ChucVu")] NHAN_VIEN nHAN_VIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHAN_VIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nHAN_VIEN);
        }

        // GET: Admin/AdminNHAN_VIEN/Delete/5
        public ActionResult Delete(string id)
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAN_VIEN nHAN_VIEN = db.NHAN_VIEN.Find(id);
            if (nHAN_VIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHAN_VIEN);
        }

        // POST: Admin/AdminNHAN_VIEN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NHAN_VIEN nHAN_VIEN = db.NHAN_VIEN.Find(id);
            db.NHAN_VIEN.Remove(nHAN_VIEN);
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
