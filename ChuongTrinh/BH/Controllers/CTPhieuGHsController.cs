using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BH.Models;

namespace BH.Controllers
{
    public class CTPhieuGHsController : Controller
    {
        private dbBachHoa db = new dbBachHoa();

        // GET: CTPhieuGHs
        public async Task<ActionResult> Index()
        {
            var cTPhieuGHs = db.CTPhieuGHs.Include(c => c.MatHang).Include(c => c.PhieuGiaoHang);
            return View(await cTPhieuGHs.ToListAsync());
        }

        // GET: CTPhieuGHs/Details/5
        public async Task<ActionResult> Details(int? id)
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
        public ActionResult Create()
        {
            ViewBag.MSMH = new SelectList(db.MatHangs, "MSMH", "TenHang");
            ViewBag.SOPG = new SelectList(db.PhieuGiaoHangs, "SOPG", "TrangThai");
            return View();
        }

        // POST: CTPhieuGHs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SOPG,MSMH,SoLuongGiao,DonGia,ThanhTien")] CTPhieuGH cTPhieuGH)
        {
            if (ModelState.IsValid)
            {
                db.CTPhieuGHs.Add(cTPhieuGH);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MSMH = new SelectList(db.MatHangs, "MSMH", "TenHang", cTPhieuGH.MSMH);
            ViewBag.SOPG = new SelectList(db.PhieuGiaoHangs, "SOPG", "TrangThai", cTPhieuGH.SOPG);
            return View(cTPhieuGH);
        }

        // GET: CTPhieuGHs/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
            ViewBag.SOPG = new SelectList(db.PhieuGiaoHangs, "SOPG", "TrangThai", cTPhieuGH.SOPG);
            return View(cTPhieuGH);
        }

        // POST: CTPhieuGHs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SOPG,MSMH,SoLuongGiao,DonGia,ThanhTien")] CTPhieuGH cTPhieuGH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTPhieuGH).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MSMH = new SelectList(db.MatHangs, "MSMH", "TenHang", cTPhieuGH.MSMH);
            ViewBag.SOPG = new SelectList(db.PhieuGiaoHangs, "SOPG", "TrangThai", cTPhieuGH.SOPG);
            return View(cTPhieuGH);
        }

        // GET: CTPhieuGHs/Delete/5
        public async Task<ActionResult> Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CTPhieuGH cTPhieuGH = await db.CTPhieuGHs.FindAsync(id);
            db.CTPhieuGHs.Remove(cTPhieuGH);
            await db.SaveChangesAsync();
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
