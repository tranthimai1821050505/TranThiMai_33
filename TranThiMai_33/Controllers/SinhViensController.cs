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
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;

namespace TranThiMai_33.Controllers
{
    public class SinhViensController : Controller
    {
        private LTQLDb db = new LTQLDb();

        // GET: SinhViens
        public async Task<ActionResult> Index()
        {
            return View(await db.SinhViens.ToListAsync());
        }

        // GET: SinhViens/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = await db.SinhViens.FindAsync(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // GET: SinhViens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SinhViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaSinhVien,HoTen,MaLop")] SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                db.SinhViens.Add(sinhVien);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sinhVien);
        }

        // GET: SinhViens/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = await db.SinhViens.FindAsync(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // POST: SinhViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaSinhVien,HoTen,MaLop")] SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinhVien).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sinhVien);
        }

        // GET: SinhViens/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = await db.SinhViens.FindAsync(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // POST: SinhViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            SinhVien sinhVien = await db.SinhViens.FindAsync(id);
            db.SinhViens.Remove(sinhVien);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //upload file excel controller
        [HttpPost]
        [Authorize]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            //dat ten cho file
            string _FileName = "KhachHang.xlsx";
            //duong dan luu file
            string _path = Path.Combine(Server.MapPath("~/Uploads/Excels"), _FileName);
            //luu file len server
            file.SaveAs(_path);

            //doc du lieu tu file excel
            DataTable dt = ReadDataFromExcelFile(_path);
            //   CopyDataByBulk(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SinhVien sv = new SinhVien();
                sv.MaSinhVien = dt.Rows[i][0].ToString();
                sv.HoTen = dt.Rows[i][1].ToString();
              //  sv.MaLop = dt.Rows[i][2].ToString();
                db.SinhViens.Add(sv);
                db.SaveChanges();
            }

            // CopyDataByBulk(excel.ReadDataFromExcelFile(_path));
            //  return View("Index");
            return RedirectToAction("Index");
        }
        //upload file
        public DataTable ReadDataFromExcelFile(string filepath)
        {
            string connectionString = "";
            string fileExtention = filepath.Substring(filepath.Length - 4).ToLower();
            if (fileExtention.IndexOf("xlsx") == 0)
            {
                connectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + filepath + ";Extended Properties=\"Excel 12.0 Xml;HDR=NO\"";
            }
            else if (fileExtention.IndexOf("xls") == 0)
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties=Excel 8.0";
            }

            // Tạo đối tượng kết nối
            OleDbConnection oledbConn = new OleDbConnection(connectionString);
            DataTable data = null;
            try
            {
                // Mở kết nối
                oledbConn.Open();

                // Tạo đối tượng OleDBCommand và query data từ sheet có tên "Sheet1"
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn);

                // Tạo đối tượng OleDbDataAdapter để thực thi việc query lấy dữ liệu từ tập tin excel
                OleDbDataAdapter oleda = new OleDbDataAdapter();

                oleda.SelectCommand = cmd;

                // Tạo đối tượng DataSet để hứng dữ liệu từ tập tin excel
                DataSet ds = new DataSet();

                // Đổ đữ liệu từ tập excel vào DataSet
                oleda.Fill(ds);

                data = ds.Tables[0];
            }
            catch
            {
            }
            finally
            {
                // Đóng chuỗi kết nối
                oledbConn.Close();
            }
            return data;
        }


        //copy large data from datatable to sqlserver
        private void CopyDataByBulk(DataTable dt)
        {
            //lay ket noi voi database luu trong file webconfig
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LTQLDb"].ConnectionString);
            SqlBulkCopy bulkcopy = new SqlBulkCopy(con);
            bulkcopy.DestinationTableName = "SinhViens";
            bulkcopy.ColumnMappings.Add(0, "MaSinhVien");
            bulkcopy.ColumnMappings.Add(1, "HoTen");
            bulkcopy.ColumnMappings.Add(2, "MaLop");
            con.Open();
            bulkcopy.WriteToServer(dt);
            con.Close();
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
