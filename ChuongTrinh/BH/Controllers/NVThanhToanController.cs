using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BH.Models;
using PagedList;
using PagedList.Mvc;
using System.Threading.Tasks;
using System.Net;
using System.Data.Entity;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

namespace BH.Controllers
{
    public class NVThanhToanController : Controller
    {
        // GET: NVThanhToan
        dbBachHoa db = new dbBachHoa();

        //TRANG CHỦ
        public ActionResult Index()
        {
            //return View();
            if (Session["TaiKhoan"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public async Task<ActionResult> ChiTiet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NVThanhToan nVThanhToan = await db.NVThanhToans.FindAsync(id);
            if (nVThanhToan == null)
            {
                return HttpNotFound();
            }
            return View(nVThanhToan);
        }
        

        //CUAHANG
        // GET: CuaHangs
        public async Task<ActionResult> CuaHang()
        {
            return View(db.CuaHangs.ToList());
        }

        // GET: CuaHangs/Details/5
        public async Task<ActionResult> CH_ChiTiet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuaHang cuaHang = await db.CuaHangs.FindAsync(id);
            if (cuaHang == null)
            {
                return HttpNotFound();
            }
            return View(cuaHang);
        }
        // GET: CuaHangs/Create
        public ActionResult CH_Them()
        {
            ViewBag.MSLH = new SelectList(db.LoaiHangs, "MSLH", "TenLoaiHang");
            ViewBag.NvPhuTrach = new SelectList(db.NVPhuTraches, "MSNV", "HoTen");
            return View();
        }
        // POST: CuaHangs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CH_Them([Bind(Include = "MSCH,TenCH,MSLH,DiaChi,NvPhuTrach,SDT")] CuaHang cuaHang)
        {
            if (ModelState.IsValid)
            {
                db.CuaHangs.Add(cuaHang);
                await db.SaveChangesAsync();
                return RedirectToAction("CuaHang");
            }

            ViewBag.MSLH = new SelectList(db.LoaiHangs, "MSLH", "TenLoaiHang", cuaHang.MSLH);
            ViewBag.NvPhuTrach = new SelectList(db.NVPhuTraches, "MSNV", "HoTen", cuaHang.NvPhuTrach);
            return View(cuaHang);
        }
        // GET: CuaHangs/Edit/5
        public async Task<ActionResult> CH_Sua(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuaHang cuaHang = await db.CuaHangs.FindAsync(id);
            if (cuaHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MSLH = new SelectList(db.LoaiHangs, "MSLH", "TenLoaiHang", cuaHang.MSLH);
            ViewBag.NvPhuTrach = new SelectList(db.NVPhuTraches, "MSNV", "HoTen", cuaHang.NvPhuTrach);
            return View(cuaHang);
        }
        // POST: CuaHangs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CH_Sua([Bind(Include = "MSCH,TenCH,MSLH,DiaChi,NvPhuTrach,SDT")] CuaHang cuaHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuaHang).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("CuaHang");
            }
            ViewBag.MSLH = new SelectList(db.LoaiHangs, "MSLH", "TenLoaiHang", cuaHang.MSLH);
            ViewBag.NvPhuTrach = new SelectList(db.NVPhuTraches, "MSNV", "HoTen", cuaHang.NvPhuTrach);
            return View(cuaHang);
        }
        // GET: CuaHangs/Delete/5
        public async Task<ActionResult> CH_Xoa(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuaHang cuaHang = await db.CuaHangs.FindAsync(id);
            if (cuaHang == null)
            {
                return HttpNotFound();
            }
            return View(cuaHang);
        }
        // POST: CuaHangs/Delete/5
        [HttpPost, ActionName("CH_Xoa")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CH_XoaConfirmed(int id)
        {
            CuaHang cuaHang = await db.CuaHangs.FindAsync(id);
            db.CuaHangs.Remove(cuaHang);
            await db.SaveChangesAsync();
            return RedirectToAction("CuaHang");
        }


        //lOẠI HÀNG 
        // GET: LoaiHangs
        public async Task<ActionResult> LoaiHang()
        {
            return View(db.LoaiHangs.ToList());
        }
        // GET: LoaiHangs/Details/5
        public async Task<ActionResult> LH_ChiTiet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiHang loaiHang = await db.LoaiHangs.FindAsync(id);
            if (loaiHang == null)
            {
                return HttpNotFound();
            }
            return View(loaiHang);
        }
        // GET: LoaiHangs/Create
        public ActionResult LH_Them()
        {
            return View();
        }

        // POST: LoaiHangs/Create
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> LH_Them(LoaiHang lh, HttpPostedFileBase fileupload)
        {
            if (fileupload == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/HinhAnh/HinhAnh(LoaiHang)"), fileName);
                    if (System.IO.File.Exists(path))
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    lh.HinhAnh = fileName;
                    db.LoaiHangs.Add(lh);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("LoaiHang");
            }
        }
        // GET: LoaiHangs/Edit/5
        public async Task<ActionResult> LH_Sua(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiHang loaiHang = await db.LoaiHangs.FindAsync(id);
            if (loaiHang == null)
            {
                return HttpNotFound();
            }
            return View(loaiHang);
        }
        // POST: LoaiHangs/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> LH_Sua(LoaiHang lh, HttpPostedFileBase fileUpload)
        {
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/HinhAnh/HinhAnh(LoaiHang)"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    LoaiHang lhh = db.LoaiHangs.Where(x => x.MSLH == lh.MSLH).Single<LoaiHang>();
                    lhh.TenLoaiHang = lh.TenLoaiHang;
                    lhh.HinhAnh = fileName;
                    await db.SaveChangesAsync();
                }
            }
            return RedirectToAction("LoaiHang");
        }
        // GET: LoaiHangs/Delete/5
        public async Task<ActionResult> LH_Xoa(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiHang loaiHang = await db.LoaiHangs.FindAsync(id);
            if (loaiHang == null)
            {
                return HttpNotFound();
            }
            return View(loaiHang);
        }
        // POST: LoaiHangs/Delete/5
        [HttpPost, ActionName("LH_Xoa")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LH_XoaConfirmed(int id)
        {
            LoaiHang loaiHang = await db.LoaiHangs.FindAsync(id);
            db.LoaiHangs.Remove(loaiHang);
            await db.SaveChangesAsync();
            return RedirectToAction("LoaiHang");
        }


        //NHANVIEN
        // GET: NVPhuTraches
        public async Task<ActionResult> NhanVien()
        {
            return View(db.NVPhuTraches.ToList());
        }
        // GET: NVPhuTraches/Details/5
        public async Task<ActionResult> NV_ChiTiet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NVPhuTrach nVPhuTrach = await db.NVPhuTraches.FindAsync(id);
            if (nVPhuTrach == null)
            {
                return HttpNotFound();
            }
            return View(nVPhuTrach);
        }
        // GET: NVPhuTraches/Create
        public ActionResult NV_Them()
        {
            return View();
        }
        // POST: NVPhuTraches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NV_Them([Bind(Include = "MSNV,HoTen,Phai,NamSinh,DiaChi,SDT,TaiKhoan,MatKhau")] NVPhuTrach nVPhuTrach)
        {
            if (ModelState.IsValid)
            {
                db.NVPhuTraches.Add(nVPhuTrach);
                await db.SaveChangesAsync();
                return RedirectToAction("NhanVien");
            }

            return View(nVPhuTrach);
        }
        // GET: NVPhuTraches/Edit/5
        public async Task<ActionResult> NV_Sua(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NVPhuTrach nVPhuTrach = await db.NVPhuTraches.FindAsync(id);
            if (nVPhuTrach == null)
            {
                return HttpNotFound();
            }
            return View(nVPhuTrach);
        }
        // POST: NVPhuTraches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NV_Sua([Bind(Include = "MSNV,HoTen,Phai,NamSinh,DiaChi,SDT,TaiKhoan,MatKhau")] NVPhuTrach nVPhuTrach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nVPhuTrach).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("NhanVien");
            }
            return View(nVPhuTrach);
        }
        // GET: NVPhuTraches/Delete/5
        public async Task<ActionResult> NV_Xoa(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NVPhuTrach nVPhuTrach = await db.NVPhuTraches.FindAsync(id);
            if (nVPhuTrach == null)
            {
                return HttpNotFound();
            }
            return View(nVPhuTrach);
        }
        // POST: NVPhuTraches/Delete/5
        [HttpPost, ActionName("NV_Xoa")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NV_XoaConfirmed(int id)
        {
            NVPhuTrach nVPhuTrach = await db.NVPhuTraches.FindAsync(id);
            db.NVPhuTraches.Remove(nVPhuTrach);
            await db.SaveChangesAsync();
            return RedirectToAction("NhanVien");
        }


        //PhIEUTHU
        public async Task<ActionResult> PhieuThu()
        {
            var cTPhieuTTs = db.CTPhieuTTs.Include(c => c.MatHang).Include(c => c.PhieuThanhToan);
            return View(await cTPhieuTTs.ToListAsync());
        }
        // GET: CTPhieuTTs/Details/5
        public async Task<ActionResult> PT_ChiTiet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPhieuTT cTPhieuTT = await db.CTPhieuTTs.FindAsync(id);
            if (cTPhieuTT == null)
            {
                return HttpNotFound();
            }
            return View(cTPhieuTT);
        }
        // GET: CTPhieuTTs/Create
        public ActionResult PT_Them()
        {
            ViewBag.MSMH = new SelectList(db.MatHangs, "MSMH", "TenHang");
            ViewBag.SOPTT = new SelectList(db.PhieuThanhToans, "SOPTT", "SOPTT");
            return View();
        }
        // POST: CTPhieuTTs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PT_Them([Bind(Include = "SOCTPTT,SOPTT,MSMH,SoLuongBan,DonGia,ThanhTien")] CTPhieuTT cTPhieuTT)
        {
            if (ModelState.IsValid)
            {
                db.CTPhieuTTs.Add(cTPhieuTT);
                await db.SaveChangesAsync();
                return RedirectToAction("PhieuThu");
            }

            ViewBag.MSMH = new SelectList(db.MatHangs, "MSMH", "TenHang", cTPhieuTT.MSMH);
            ViewBag.SOPTT = new SelectList(db.PhieuThanhToans, "SOPTT", "SOPTT", cTPhieuTT.SOPTT);
            return View(cTPhieuTT);
        }
        // GET: CTPhieuTTs/Edit/5
        public async Task<ActionResult> Pt_Sua(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPhieuTT cTPhieuTT = await db.CTPhieuTTs.FindAsync(id);
            if (cTPhieuTT == null)
            {
                return HttpNotFound();
            }
            ViewBag.MSMH = new SelectList(db.MatHangs, "MSMH", "TenHang", cTPhieuTT.MSMH);
            ViewBag.SOPTT = new SelectList(db.PhieuThanhToans, "SOPTT", "SOPTT", cTPhieuTT.SOPTT);
            return View(cTPhieuTT);
        }

        // POST: CTPhieuTTs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PT_Sua([Bind(Include = "SOCTPTT, SOPTT,MSMH,SoLuongBan,DonGia,ThanhTien")] CTPhieuTT cTPhieuTT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTPhieuTT).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("PhieuThu");
            }
            ViewBag.MSMH = new SelectList(db.MatHangs, "MSMH", "TenHang", cTPhieuTT.MSMH);
            ViewBag.SOPTT = new SelectList(db.PhieuThanhToans, "SOPTT", "SOPTT", cTPhieuTT.SOPTT);
            return View(cTPhieuTT);
        }

        // GET: CTPhieuTTs/Delete/5
        public async Task<ActionResult> PT_Xoa(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPhieuTT cTPhieuTT = await db.CTPhieuTTs.FindAsync(id);
            if (cTPhieuTT == null)
            {
                return HttpNotFound();
            }
            return View(cTPhieuTT);
        }

        // POST: CTPhieuTTs/Delete/5
        [HttpPost, ActionName("PT_Xoa")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PT_XoaConfirmed(int id)
        {
            CTPhieuTT cTPhieuTT = await db.CTPhieuTTs.FindAsync(id);
            db.CTPhieuTTs.Remove(cTPhieuTT);
            await db.SaveChangesAsync();
            return RedirectToAction("PhieuThu");
        }

        //DOANHTHU
        public ActionResult DoanhThu()
        {
            ViewBag.ListDoanhThu = db.BaoCaos.ToList();
            return View();
        }
        public ActionResult InBaoCao()
        {
            List<CTBaoCao> allmh = new List<CTBaoCao>();
            allmh = db.CTBaoCaos.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/RP"), "DoanhThuRP.rpt"));

            rd.SetDataSource(allmh);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListDoanhThu.pdf");
        }


        //TAIKHOAN 
        public ActionResult TaiKhoan()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "NVThanhToan");
            }
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