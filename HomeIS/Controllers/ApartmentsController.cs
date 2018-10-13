using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeIS.Models;
using Microsoft.ApplicationInsights.Web;

namespace HomeIS.Controllers
{
    public class ApartmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Apartments
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Apartments.Where(ap =>
                ap.Owner == db.Users.FirstOrDefault<ApplicationUser>(user =>
                    user.UserName == this.User.Identity.Name)));
        }

        // GET: Apartments/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Apartment apartment = db.Apartments.FirstOrDefault(ap => (ap.ID == id) &&
                                                                      (ap.Owner.UserName == this.User.Identity.Name));

            if (apartment == null)
            {
                return HttpNotFound();
            }

            return View(apartment);
        }

        // GET: Apartments/Create
        [Authorize]
        public ActionResult Create()
        {
            return View(); 
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include =
                "ID,Owner,Location,Description,PropertyValue,PhotoList,Photos,Balcony,Size,FloorNumber,NumberOfRooms")]
            Apartment apartment)
        {
            db.Configuration.LazyLoadingEnabled = false;


            List<string> PhotoList = new List<string>();

            if (ModelState.IsValid && Request.Files.Count > 0)
            {
                for (int uploadID = 0; uploadID < Request.Files.Count; uploadID++)
                {
                    var uploadedFile = Request.Files[uploadID];

                    if (uploadedFile.HasFile())
                    {
                        PhotoList.Add(UploadApartmentPhoto(uploadedFile));
                    }
                }

                apartment.PhotoList = PhotoList;
                apartment.Owner = db.Users.FirstOrDefault(s => s.UserName == this.User.Identity.Name);

                db.Apartments.Add(apartment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(apartment);
        }

        private string UploadApartmentPhoto(HttpPostedFileBase uploadedFile)
        {
            string relativePath = "UserPhotos/" + this.User.Identity.Name.Replace("@", "------");
            string absolutePath = AppDomain.CurrentDomain.BaseDirectory + relativePath;
            Directory.CreateDirectory(absolutePath);

            string filename = DateTime.Now.ToFileTime() + (new Random().Next()).ToString() +
                              Path.GetExtension(uploadedFile.FileName);
            uploadedFile.SaveAs(Path.Combine(absolutePath, filename));

            return relativePath + '/' + filename;
        }

        // GET: Apartments/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            db.Configuration.LazyLoadingEnabled = false;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Apartment apartment = db.Apartments.FirstOrDefault(ap => (ap.ID == id) &&
                                                                      (ap.Owner.UserName == this.User.Identity.Name));

            if (apartment == null)
            {
                return HttpNotFound();
            }

            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
                "ID,Owner,Location,Description,PropertyValue,PhotoList,Photos,Balcony,Size,FloorNumber,NumberOfRooms")]
            Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                string baseFolder = "UserPhotos/" + this.User.Identity.Name.Replace("@", "------");

                for (int uploadID = 0; uploadID < Request.Files.Count; uploadID++)
                {
                    var uploadedFile = Request.Files[uploadID];

                    if (uploadedFile.HasFile())
                    {
                        apartment.PhotoList.Add(UploadApartmentPhoto(uploadedFile));
                    }
                }

                foreach (string file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + baseFolder))
                {
                    if (!apartment.PhotoList.Contains(baseFolder + "/" + Path.GetFileName(file)))
                    {
                        System.IO.File.Delete(file);
                    }
                }


                db.Entry(apartment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(apartment);
        }

        // GET: Apartments/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Apartment apartment = db.Apartments.FirstOrDefault(ap => (ap.ID == id) &&
                                                                      (ap.Owner.UserName == this.User.Identity.Name));

            if (apartment == null)
            {
                return HttpNotFound();
            }

            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apartment apartment = db.Apartments.FirstOrDefault(ap => (ap.ID == id) &&
                                                                      (ap.Owner.UserName == this.User.Identity.Name));

            if (apartment != null)
            {
                apartment.PhotoList.ForEach(photo => System.IO.File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, photo)));

                db.Apartments.Remove(apartment);
                db.SaveChanges();
            }

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

        public JsonResult AllApartmentsJSON()
        {
            var apartments = db.Apartments.ToList();
            return Json(apartments, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ApartmentCountGroupJSON()
        {
            var apartments = from ap in db.Apartments
                group ap by ap.Location.City
                into ApartmentGroup
                select new {City = ApartmentGroup.Key, Count = ApartmentGroup.Count()};

            return Json(apartments, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SizeBalconyMinOrMaxPriceJSON(int Size, bool Balcony, int MinimumPrice)
        {
            return Json(db.Apartments.Include(t => t.Owner).Where(p => p.Balcony == Balcony && p.Size == Size &&
                                                                       p.PropertyValue >= MinimumPrice).ToList(),
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult SizeBalconyPriceRangeJSON(int Size, bool Balcony, int MinimumPrice, int MaximumPrice)
        {
            var QuerySet = db.Apartments.Include(t => t.Owner).Where(p => p.Balcony == Balcony && p.Size == Size &&
                                                                          p.PropertyValue >= MinimumPrice &&
                                                                          p.PropertyValue <= MaximumPrice).ToList();
            return Json(QuerySet, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AmountPerCity(string CityName)
        {
            var QuerySet = db.Apartments.Where(t => t.Location.City == CityName).GroupBy(p => p.Location.City)
                .Select(g => new {count = g.Count()}).ToList();

            return Json(QuerySet, JsonRequestBehavior.AllowGet);
        }

        public bool PredictApartmentSale(int size, int value, int floor)
        {

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = System.AppDomain.CurrentDomain.BaseDirectory + @"..\debug\ML.exe",
                    Arguments = size.ToString() +" "+ value.ToString() +" "+ floor.ToString(),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = false
                }
            };

            proc.Start();
            proc.WaitForExit();
            string output = proc.StandardOutput.ReadToEnd();
            bool returnBool = output.TrimEnd() == "True";
            
            return returnBool;
        }
    }
}