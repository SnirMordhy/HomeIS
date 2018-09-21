using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HomeIS.Models;

namespace HomeIS.Controllers
{
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Apartment);
            return View(transactions.ToList());
        }

        // Get: Transactions/TopSalingTable
        [Authorize(Roles = "Admin")]
        public ActionResult TopSalingTable()
        {
            Dictionary<ApplicationUser, int> transactions = db.Transactions.GroupBy(t => t.Saler)
                                                            .Select(g => new { g.Key, Count = g.Count() })
                                                            .OrderByDescending(s => s.Count)
                                                            .ToDictionary(s => s.Key, s => s.Count);
            return View("Statistics", transactions);
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.ApartmentID = new SelectList(db.Apartments, "ID", "Description");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SalerID,PurchaserEmail,TransactionDate,BuyingPrice,ApartmentID")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApartmentID = new SelectList(db.Apartments, "ID", "Description", transaction.ApartmentID);
            return View(transaction);
        }

        // POST: Transactions/CreateTransaction
        // Create apartment buying transaction;
        [HttpPost]
        public ActionResult CreateTransaction(Apartment apartment, int buyingPrice)
        {
            Transaction transaction = new Transaction();
            Apartment apartmentForSale = db.Apartments.Include(aprt => aprt.Owner).FirstOrDefault(aprt => aprt.ID == apartment.ID);
            ApplicationUser currentUser = db.Users.Where(user => user.UserName == User.Identity.Name).FirstOrDefault();

            if (apartmentForSale == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(currentUser==null || apartmentForSale.Owner.Id == currentUser.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }

            transaction.Saler = apartmentForSale.Owner;
            transaction.Purchaser = currentUser;
            transaction.TransactionDate = DateTime.Now;
            transaction.BuyingPrice = buyingPrice;
            transaction.Apartment = apartmentForSale;

            apartmentForSale.Owner = currentUser;
            apartmentForSale.IsForSale = false;

            db.Transactions.Add(transaction);
            db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.Created);

        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApartmentID = new SelectList(db.Apartments, "ID", "Description", transaction.ApartmentID);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SalerID,PurchaserEmail,TransactionDate,BuyingPrice,ApartmentID")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApartmentID = new SelectList(db.Apartments, "ID", "Description", transaction.ApartmentID);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
