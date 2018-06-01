using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BH.Models;

namespace BH.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        //DANGNHAP
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            dbBachHoa db = new dbBachHoa();
            var tendn = collection["TaiKhoan"];
            var matkhau = collection["MatKhau"];

            NVPhuTrach nvPhuTrach = db.NVPhuTraches.SingleOrDefault(n => n.TaiKhoan == tendn && n.MatKhau == matkhau);
            NVThanhToan nvThanhToan = db.NVThanhToans.SingleOrDefault(n => n.TaiKhoan == tendn && n.MatKhau == matkhau);

            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Coloi"] = "Vui lòng nhập tên tài khoản";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Coloi1"] = "Vui lòng nhập mật khẩu";
            }
            else
            {
                if (nvPhuTrach != null)
                {
                    Session["TaiKhoan"] = nvPhuTrach.MSNV;
                    Session["HoTen"] = nvPhuTrach.HoTen.ToString();
                    Session["Phai"] = nvPhuTrach.Phai.ToString();
                    Session["NamSinh"] = nvPhuTrach.NamSinh.ToString();
                    Session["DiaChi"] = nvPhuTrach.DiaChi.ToString();
                    Session["SDT"] = nvPhuTrach.SDT.ToString();
                    Session["TaiKhoan"] = nvPhuTrach.TaiKhoan.ToString();
                    return RedirectToAction("Index", "NVPhuTrach");
                }
                else if (nvThanhToan != null)
                {
                    Session["TaiKhoan"] = nvThanhToan.MSNV;
                    Session["HoTen"] = nvThanhToan.HoTen.ToString();
                    Session["Phai"] = nvThanhToan.Phai.ToString();
                    Session["NamSinh"] = nvThanhToan.NamSinh.ToString();
                    Session["DiaChi"] = nvThanhToan.DiaChi.ToString();
                    Session["SDT"] = nvThanhToan.SDT.ToString();
                    Session["TaiKhoan"] = nvThanhToan.TaiKhoan.ToString();
                    return RedirectToAction("Index", "NVThanhToan");
                }
                else
                    ViewBag.Thongbao = "Tài khoản hoặc mật khẩu không đúng";
            }
            return View();
        }
        public ActionResult AfterLogin()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}