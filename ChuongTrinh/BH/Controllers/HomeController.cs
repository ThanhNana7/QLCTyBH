using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BH.Models;
using System.Threading.Tasks;

namespace BH.Controllers
{
    public class HomeController : Controller
    {
        dbBachHoa data = new dbBachHoa();
        private List<MatHang> Laymathangmoi(int count)
        {
            return data.MatHangs.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        public ActionResult Index()
        {
            var mathangmoi = Laymathangmoi(8);
            return View(mathangmoi);
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
        public ActionResult LoaiHang()
        {
            var loaihang = from lh in data.LoaiHangs select lh;
            return PartialView(loaihang);
        }
        public ActionResult SPTheoloaihang(int id)
        {
            var mathang = from s in data.MatHangs where s.MSLH == id select s;
            return View(mathang);
        }

        public ActionResult ChiTiet(int id)
        {
            var mathang = from s in data.MatHangs where s.MSMH == id select s;
            return View(mathang.Single());
        }

        //Giỏ Hàng
        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì khởi tạo listGiohang
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }

        public ActionResult ThemGiohang(int iMSMH, string strURL)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.Find(n => n.iMSMH == iMSMH);
            if (sanpham == null)
            {
                sanpham = new Giohang(iMSMH);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }

        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }

        private double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }

        public ActionResult GioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }

        public ActionResult XoaGiohang(int iMaSP)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMSMH == iMaSP);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.iMSMH == iMaSP);
                return RedirectToAction("GioHang");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapnhatGiohang(int iMaSP, FormCollection f)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMSMH == iMaSP);
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Giohang");
        }

        public ActionResult XoaTatcaGiohang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Dathang()
        {
            //Kiểm tra đăng nhập
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "Home");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Lấy giỏ hàng từ Session
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();

            return View(lstGiohang);
        }

        public async Task<ActionResult> Dathang(FormCollection collection)
        {
            DonDatHang pgh = new DonDatHang();
            KhachHang kh = (KhachHang)Session["Taikhoan"];
            List<Giohang> gh = Laygiohang();
            pgh.SoHD = pgh.SoHD;
            pgh.MSKH = kh.MSKH;
            pgh.NgayDH = DateTime.Now;
            pgh.TenNgNhan = kh.TenKH;
            pgh.DiaChiNhan = kh.DiaChi;
            pgh.SDTNhan = kh.SDT;

            //Thêm chi tiết đơn hàng
            foreach (var item in gh)
            {
                CTDatHang ctdh = new CTDatHang();
                ctdh.SoHD = pgh.SoHD;
                ctdh.MSMH = item.iMSMH;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.DonGia = item.dDonGia;
                data.DonDatHangs.Add(pgh);
                await data.SaveChangesAsync();
                return RedirectToAction("Xacnhandonhang");
            }
            await data.SaveChangesAsync();
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "Giohang");
        }

        public ActionResult Xacnhandonhang()
        {
            return View();
        }

        //Dang nhap
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

            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoang == tendn && n.MatKhau == matkhau);
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
                if (kh != null)
                {
                    Session["TaiKhoan"] = kh;
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.Thongbao = "Tài khoản hoặc mật khẩu không đúng";
            }
            return View();
        }
    }
}