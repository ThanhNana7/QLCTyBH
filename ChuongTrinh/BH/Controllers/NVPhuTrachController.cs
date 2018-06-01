using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BH.Models;
using PagedList;
using PagedList.Mvc;
using System.Net;
using System.Data.Entity;
using System.IO;
using BH.RP;
using CrystalDecisions.CrystalReports.Engine;

namespace BH.Controllers
{
    public class NVPhuTrachController : Controller
    {
        dbBachHoa db = new dbBachHoa();
        // GET: NVPhuTrach
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

        //MATHANG
        // GET: MatHangs
        //public ActionResult MatHang()
        //{  
        //    return View();
        //}
        ////loaddata
        //public ActionResult loaddata()
        //{
        //    using (dbBachHoa db = new dbBachHoa())
        //    {
        //        var data = db.MatHangs.OrderBy(a => a.MSMH).ToList();
        //        return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        public async Task<ActionResult> MatHang()
        {
            return View(db.MatHangs.ToList());
        }

        // GET: MatHangs/Details/5
        public async Task<ActionResult> MH_ChiTiet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatHang matHang = await db.MatHangs.FindAsync(id);
            if (matHang == null)
            {
                return HttpNotFound();
            }
            return View(matHang);
        }
        // GET: MatHangs/Create
        public ActionResult MH_Them()
        {
            ViewBag.MSLH = new SelectList(db.LoaiHangs, "MSLH", "TenLoaiHang");
            return View();
        }
        // POST: MatHangs/Create
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> MH_Them(MatHang mh, HttpPostedFileBase fileupload)
        {
            ViewBag.MSLH = new SelectList(db.LoaiHangs, "MSLH", "TenLoaiHang", mh.MSLH);
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
                    var path = Path.Combine(Server.MapPath("~/HinhAnh/HinhAnh(MatHang)"), fileName);
                    if (System.IO.File.Exists(path))
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    mh.HinhAnh = fileName;
                    db.MatHangs.Add(mh);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("MatHang");
            }
        }
        // GET: MatHangs/Edit/5
        public async Task<ActionResult> MH_Sua(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatHang matHang = await db.MatHangs.FindAsync(id);
            if (matHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MSLH = new SelectList(db.LoaiHangs, "MSLH", "TenLoaiHang", matHang.MSLH);
            return View(matHang);
        }
        // POST: MatHangs/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> MH_Sua(MatHang mh, HttpPostedFileBase fileUpload)
        {
            ViewBag.MSLH = new SelectList(db.LoaiHangs, "MSLH", "TenLoaiHang", mh.MSLH);
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
                    var path = Path.Combine(Server.MapPath("~/HinhAnh/HinhAnh(MatHang)"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    MatHang mhh = db.MatHangs.Where(x => x.MSMH == mh.MSMH).Single<MatHang>();
                    mhh.TenHang = mh.TenHang;
                    mhh.MSLH = mh.MSLH;
                    mhh.SoLuong = mh.SoLuong;
                    mhh.TrangThai = mh.TrangThai;
                    mhh.DonGia = mh.DonGia;
                    mhh.HinhAnh = fileName;
                    mhh.NgayCapNhat = mh.NgayCapNhat;
                    await db.SaveChangesAsync();
                }
            }
            return RedirectToAction("MatHang");
        }
        // GET: MatHangs/Delete/5
        public async Task<ActionResult> MH_Xoa(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatHang matHang = await db.MatHangs.FindAsync(id);
            if (matHang == null)
            {
                return HttpNotFound();
            }
            return View(matHang);
        }

        // POST: MatHangs/Delete/5
        [HttpPost, ActionName("MH_Xoa")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MH_XoaConfirmed(int id)
        {
            MatHang matHang = await db.MatHangs.FindAsync(id);
            db.MatHangs.Remove(matHang);
            await db.SaveChangesAsync();
            return RedirectToAction("MatHang");
        }

        //PHIEUGIAO
        // GET: CTPhieuGHs
        public async Task<ActionResult> PhieuGiao()
        {
            var cTPhieuGHs = db.CTPhieuGHs.Include(c => c.MatHang).Include(c => c.PhieuGiaoHang);
            return View(await cTPhieuGHs.ToListAsync());
        }
        

        // GET: CTPhieuGHs/Details/5
        public async Task<ActionResult> PG_ChiTiet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPhieuGH cTPhieuGH = await db.CTPhieuGHs.FindAsync(id);
            if (cTPhieuGH == null)
            {
                return HttpNotFound();
            }
            return View(cTPhieuGH);
        }

        // GET: CTPhieuGHs/Create
        public ActionResult PG_Them()
        {
            ViewBag.MSMH = new SelectList(db.MatHangs, "MSMH", "TenHang");
            ViewBag.SOPG = new SelectList(db.PhieuGiaoHangs, "SOPG", "SOPG");
            ViewBag.MSCH = new SelectList(db.CuaHangs, "MSCH", "TenCH");
            ViewBag.MSVN = new SelectList(db.NVPhuTraches, "MSNV", "HoTenNV");
            return View();
        }

        // POST: CTPhieuGHs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PG_Them([Bind(Include = "SOCTPG,SOPG,MSMH,SoLuongGiao,DonGia,ThanhTien")] CTPhieuGH cTPhieuGH, PhieuGiaoHang PhieuGH)
        {
            if (ModelState.IsValid)
            {
                db.CTPhieuGHs.Add(cTPhieuGH);
                await db.SaveChangesAsync();
                return RedirectToAction("PhieuGiao");
            }

            ViewBag.MSMH = new SelectList(db.MatHangs, "MSMH", "TenHang", cTPhieuGH.MSMH);
            ViewBag.SOPG = new SelectList(db.PhieuGiaoHangs, "SOPG", "SOPG", cTPhieuGH.SOPG);
            ViewBag.MSCH = new SelectList(db.CuaHangs, "MSCH", "TenCH", PhieuGH.MSCH);
            ViewBag.MSVN = new SelectList(db.NVPhuTraches, "MSNV", "HoTenNV", PhieuGH.MSNV);
            return View(cTPhieuGH);
        }

        // GET: CTPhieuGHs/Edit/5
        public async Task<ActionResult> PG_Sua(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPhieuGH cTPhieuGH = await db.CTPhieuGHs.FindAsync(id);
            if (cTPhieuGH == null)
            {
                return HttpNotFound();
            }
            ViewBag.MSMH = new SelectList(db.MatHangs, "MSMH", "TenHang", cTPhieuGH.MSMH);
            ViewBag.SOPG = new SelectList(db.PhieuGiaoHangs, "SOPG", "SOPG", cTPhieuGH.SOPG);
            return View(cTPhieuGH);
        }

        // POST: CTPhieuGHs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PG_Sua([Bind(Include = "SOCTPG,SOPG,MSMH,SoLuongGiao,DonGia,ThanhTien")] CTPhieuGH cTPhieuGH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTPhieuGH).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("PhieuGiao");
            }
            ViewBag.MSMH = new SelectList(db.MatHangs, "MSMH", "TenHang", cTPhieuGH.MSMH);
            ViewBag.SOPG = new SelectList(db.PhieuGiaoHangs, "SOPG", "SOPG", cTPhieuGH.SOPG);
            return View(cTPhieuGH);
        }

        // GET: CTPhieuGHs/Delete/5
        public async Task<ActionResult> PG_Xoa(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPhieuGH cTPhieuGH = await db.CTPhieuGHs.FindAsync(id);
            if (cTPhieuGH == null)
            {
                return HttpNotFound();
            }
            return View(cTPhieuGH);
        }

        // POST: CTPhieuGHs/Delete/5
        [HttpPost, ActionName("PG_Xoa")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PG_XoaConfirmed(int id)
        {
            CTPhieuGH cTPhieuGH = await db.CTPhieuGHs.FindAsync(id);
            db.CTPhieuGHs.Remove(cTPhieuGH);
            await db.SaveChangesAsync();
            return RedirectToAction("PhieuGiao");
        }

        //BAOCAO
        public ActionResult BaoCao()
        {
            ViewBag.ListMatHang = db.MatHangs.ToList();
            return View();
        }
        public ActionResult InBaoCao()
        {
            List<MatHang> allmh = new List<MatHang>();
            allmh = db.MatHangs.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/RP"), "KhoHangRP.rpt"));

            rd.SetDataSource(allmh);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders(); 
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ListMatHang.pdf");
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
                return RedirectToAction("Index", "NVPhuTrach");
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