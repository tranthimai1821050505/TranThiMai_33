using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TranThiMai_33.Models;

namespace TranThiMai_33.Controllers
{
    public class LopHocsController : Controller
    {
        private LTQLDb db = new LTQLDb();

        // GET: LopHocs
        public async Task<ActionResult> Index()
        {
            return View(await db.LopHocs.ToListAsync());
        }

        // GET: LopHocs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LopHoc lopHoc = await db.LopHocs.FindAsync(id);
            if (lopHoc == null)
            {
                return HttpNotFound();
            }
            return View(lopHoc);
        }

        // GET: LopHocs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LopHocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaLop,TenLop")] LopHoc lopHoc)
        {
            if (ModelState.IsValid)
            {
                db.LopHocs.Add(lopHoc);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(lopHoc);
        }

        // GET: LopHocs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LopHoc lopHoc = await db.LopHocs.FindAsync(id);
            if (lopHoc == null)
            {
                return HttpNotFound();
            }
            return View(lopHoc);
        }

        // POST: LopHocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaLop,TenLop")] LopHoc lopHoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lopHoc).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(lopHoc);
        }

        // GET: LopHocs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LopHoc lopHoc = await db.LopHocs.FindAsync(id);
            if (lopHoc == null)
            {
                return HttpNotFound();
            }
            return View(lopHoc);
        }

        // POST: LopHocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LopHoc lopHoc = await db.LopHocs.FindAsync(id);
            db.LopHocs.Remove(lopHoc);
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
