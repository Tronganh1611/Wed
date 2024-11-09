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
    public class KhachHangController : Controller
    {
        private QLBHEntities1 db = new QLBHEntities1();

        // GET: Admin/KhachHang
        // GET: Admin/KhachHang
        // GET: Admin/KhachHang
        public ActionResult Index()
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }
            return View(db.KHACH_HANG.ToList());
        }

        [HttpPost]
        public ActionResult Index(string tenKH)
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }

            if (!string.IsNullOrEmpty(tenKH))
            {
                // Loại bỏ khoảng trắng thừa ở đầu và cuối, và thay thế nhiều khoảng trắng liên tiếp bằng một khoảng trắng
                tenKH = System.Text.RegularExpressions.Regex.Replace(tenKH.Trim(), @"\s+", " ");

                var khachHangs = db.KHACH_HANG.Where(kh => kh.TenKH.Contains(tenKH)).ToList();
                return View(khachHangs);
            }

            return View(db.KHACH_HANG.ToList());
        }



        public ActionResult InDanhSachKH()
        {
            if (Session["TenLogin_NV"] != null)
            {
                ViewBag.TenLogin_NV = Session["TenLogin_NV"];
            }

            var khachhang = db.KHACH_HANG.ToList();
            return View(khachhang);
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.error = "Vui lòng nhập email và mật khẩu";
                return View();
            }

            var loginSingle = db.NHAN_VIEN.FirstOrDefault(nv => nv.Email == username && nv.MatKhau == password);
            var loginUser = db.KHACH_HANG.FirstOrDefault(kh => kh.Email == username && kh.MatKhau == password);

            if (loginSingle == null)
            {
                if (loginUser == null)
                {
                    ViewBag.error = "Email đăng nhập hoặc mật khẩu không đúng";
                    return View();
                }
                else
                {
                    Session["TenLogin_KH"] = loginUser.TenKH;
                    return RedirectToAction("Index", "SanPhams", new {area = ""});
                }
            }
            else
            {
                Session["TenLogin_NV"] = loginSingle.TenNV;
                return RedirectToAction("Index", "AdminSanPham");
            }
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "TenKH,SDT,DiaChi,Email,MatKhau,GioiTinh")] KHACH_HANG kHACH_HANG)
        {
                if (ModelState.IsValid)
                {
                    // Tạo mã Khách Hàng tự động
                    kHACH_HANG.MaKH = GenerateCustomerID();

                    // Thêm Khách hàng vào cơ sở dữ liệu
                    db.KHACH_HANG.Add(kHACH_HANG);
                    db.SaveChanges();
                    return RedirectToAction("Login", "KhachHang");
            }
            return View(kHACH_HANG);
        }


        private string GenerateCustomerID()
        {
            // Lấy danh sách tất cả các khách hàng từ cơ sở dữ liệu
            var allCustomers = db.KHACH_HANG.ToList();

            // Nếu không có khách hàng nào trong cơ sở dữ liệu, trả về mã đầu tiên
            if (allCustomers.Count == 0)
            {
                return "MKH001"; // hoặc bất kỳ định dạng nào bạn muốn áp dụng cho mã Khách hàng đầu tiên
            }

            // Nếu có khách hàng, tìm mã Khách hàng lớn nhất
            string latestCustomerID = allCustomers.Max(c => c.MaKH);

            // Tách phần số từ mã Khách hàng lớn nhất và tăng giá trị lên 1
            string latestCustomerNumberPart = latestCustomerID.Substring(3);
            int nextCustomerNumber = int.Parse(latestCustomerNumberPart) + 1;

            // Tạo lại mã Khách hàng với phần số được tăng lên và định dạng lại để có độ dài nhất định
            string nextCustomerID = "MKH" + nextCustomerNumber.ToString("D3");

            return nextCustomerID;
        }
           

        // GET: Admin/KhachHang/Details/5
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
            KHACH_HANG kHACH_HANG = db.KHACH_HANG.Find(id);
            if (kHACH_HANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACH_HANG);
        }

        // GET: Admin/KhachHang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KhachHang/Create
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

        // GET: Admin/KhachHang/Edit/5
        public ActionResult Edit(string id)
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

        // POST: Admin/KhachHang/Edit/5
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
                return RedirectToAction("Index");
            }
            return View(kHACH_HANG);
        }


        // GET: Admin/KhachHang/Delete/5
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

        // POST: Admin/KhachHang/Delete/5
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
