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
    public class AdminSanPhamController : Controller
    {
        private QLBHEntities1 db = new QLBHEntities1();

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "KhachHang");
        }

        // GET: Admin/AdminSanPham
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

        public ActionResult InDanhSach()
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }
            var product = db.SANPHAMs.Include(s => s.LoaiSP);
            return View(product.ToList());
        }

        public ActionResult CapNhat()
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }
            var products = db.SANPHAMs.Select(p => new SelectListItem
            {
                Value = p.MaSP.ToString(),
                Text = p.TenSP
            }).ToList();

            ViewBag.MaSP = new SelectList(products, "Value", "Text");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhat(SANPHAM model)
        {
            if (ModelState.IsValid)
            {
                var product = db.SANPHAMs.Find(model.MaSP);
                if (product != null)
                {
                    product.SoLuong = product.SoLuong + model.SoLuong;
                    db.SaveChanges();
                    return RedirectToAction("Create", "AdminChiTietPhieuNhaps", new {soLuong = model.SoLuong, MaSP = model.MaSP});
                }
                else
                {
                    ModelState.AddModelError("", "Product not found.");
                }
            }
            var products = db.SANPHAMs.Select(p => new SelectListItem
            {
                Value = p.MaSP.ToString(),
                Text = p.TenSP
            }).ToList();
            ViewBag.MaSP = new SelectList(products, "Value", "Text");

            return View();
        }

        public ActionResult Index()
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }
            var sANPHAMs = db.SANPHAMs.Include(s => s.LoaiSP);
            return View(sANPHAMs.ToList());
        }
        [HttpPost]
        public ActionResult Index(string tenSP)
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }
            if (tenSP != null)
            {
                var tenSanPham = db.SANPHAMs.Where(s => s.TenSP.Contains(tenSP)).ToList();
                return View(tenSanPham);
            }
            var sANPHAMs = db.SANPHAMs.Include(s => s.LoaiSP);
            return View(sANPHAMs.ToList());
        }


        // GET: Admin/AdminSanPham/Details/5
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
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // GET: Admin/AdminSanPham/Create
        public ActionResult Create()
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }
            ViewBag.MaSP = LayMaSP();
            ViewBag.MaLSP = new SelectList(db.LoaiSPs, "MaLSP", "TenLoai");
            return View();
        }

        // POST: Admin/AdminSanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,MaLSP,TenSP,SoLuong,Size,Mau,Gia,AnhSP,TrangThai,MoTa")] SANPHAM sANPHAM)
        {
            var imgSach = Request.Files["Avatar"];
            string postedFileName = System.IO.Path.GetFileName(imgSach.FileName);
            var path = Server.MapPath("/Images/product/" + postedFileName);
            imgSach.SaveAs(path);

            if (ModelState.IsValid)
            {
                sANPHAM.MaSP = LayMaSP();
                sANPHAM.AnhSP = postedFileName;
                sANPHAM.TrangThai = true;
                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("Create", "AdminChiTietPhieuNhaps", new {soLuong = sANPHAM.SoLuong, MaSP = sANPHAM.MaSP});
            }

            ViewBag.MaLSP = new SelectList(db.LoaiSPs, "MaLSP", "TenLoai", sANPHAM.MaLSP);
            return View(sANPHAM);
        }

        // GET: Admin/AdminSanPham/Edit/5
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
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLSP = new SelectList(db.LoaiSPs, "MaLSP", "TenLoai", sANPHAM.MaLSP);
            return View(sANPHAM);
        }

        // POST: Admin/AdminSanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,MaLSP,TenSP,SoLuong,Size,Mau,Gia,AnhSP,TrangThai,MoTa")] SANPHAM sANPHAM)
        {
            var imgSach = Request.Files["Avatar"];
            try
            {
                sANPHAM.TrangThai = true;
                string postedFileName = System.IO.Path.GetFileName(imgSach.FileName);
                var path = Server.MapPath("/Images/product/" + postedFileName);
                imgSach.SaveAs(path);
            }
            catch
            { }
            if (ModelState.IsValid)
            {
                db.Entry(sANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLSP = new SelectList(db.LoaiSPs, "MaLSP", "TenLoai", sANPHAM.MaLSP);
            return View(sANPHAM);
        }

        // GET: Admin/AdminSanPham/Delete/5
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

        // POST: Admin/AdminSanPham/Delete/5
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
