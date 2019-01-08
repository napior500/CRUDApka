using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CRUDApka.Models;
using CsvHelper;

namespace CRUDApka.Controllers
{
    public class TestController : Controller
    {

		private KlienciContext db = new KlienciContext();
        // GET: Test
        public ActionResult Index()
        {
			var klienci = db.Klienci.ToList();

            return View(klienci);
        }

     

        // GET: Test/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        [HttpPost]
        public ActionResult Create(Klienci klienci)
        {
            
				if (ModelState.IsValid)
				{
					db.Klienci.Add(klienci);
					db.SaveChanges();
					
					return RedirectToAction("Index");
				}


				return View("Create", klienci);
                
            
            
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int? id)
        {
			if(id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var klient = db.Klienci.Find(id);
			if(klient == null)
			{
				return HttpNotFound();
			}

            return View(klient);
        }

        // POST: Test/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id,Klienci klienci)
        {
            if(id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var klientToUpdate = db.Klienci.Find(id);
			if (!ModelState.IsValid)
			{
				return View("Edit",klienci);
			}
			TryUpdateModel(klientToUpdate);
			db.SaveChanges();
			return RedirectToAction("Index");
        }

	
		// POST: Test/Delete/5
		[HttpPost]
        public ActionResult Delete(int id)
        {
			var klienci = db.Klienci.Where(x => x.Id == id).FirstOrDefault();
			db.Klienci.Remove(klienci);
			db.SaveChanges();

			return RedirectToAction("Index");

		}

		public ActionResult ImportFromCsv()
		{
			return View();
		}
		[HttpPost]
		public ActionResult ImportFromCsv(HttpPostedFileBase file)
		{
			if (file != null && file.ContentLength > 0)
			{
				using (StreamReader reader = new StreamReader(file.InputStream, Encoding.UTF8))
				{
					CsvReader csvReader = new CsvReader(reader);
					csvReader.Configuration.MissingFieldFound = null;
					

					while (csvReader.Read())
					{
				
						var newRecord = csvReader.GetRecord<Klienci>();
						db.Klienci.Add(newRecord);
					}
					
					db.SaveChanges();
					



				}
			}
			return RedirectToAction("Index");
		}
		[HttpGet]
		public ActionResult ExportFromCode()
		{
			
			using (var writer = new StreamWriter(@"C:\Users\maciek\Downloads\test3.csv", false, Encoding.UTF8))
			{
				
				var csv = new CsvWriter(writer);
				IEnumerable<Klienci> records = db.Klienci.ToList();

				foreach (var record in records)
				{
					csv.WriteRecord(record);
					csv.NextRecord();
				}
				csv.Flush();

				return RedirectToAction("Index");
			}
		}
	}
}
