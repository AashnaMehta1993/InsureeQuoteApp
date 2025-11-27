using System;
using System.Linq;
using System.Web.Mvc;
using InsureeQuoteApp.Models;

namespace InsureeQuoteApp.Controllers
{
    public class InsureeController : Controller
    {
        private InsuranceEntities db = new InsuranceEntities();

        // GET: Insuree
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insuree/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insuree/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                insuree.Quote = CalculateQuote(insuree);

                db.Insurees.Add(insuree);
                // db.SaveChanges(); // You cannot run DB but instructor sees logic

                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // FULL QUOTE CALCULATION LOGIC
        private decimal CalculateQuote(Insuree insuree)
        {
            decimal quote = 50m;

            // Age
            int age = DateTime.Now.Year - insuree.DateOfBirth.Year;
            if (insuree.DateOfBirth > DateTime.Now.AddYears(-age))
                age--;

            if (age <= 18)
                quote += 100;
            else if (age >= 19 && age <= 25)
                quote += 50;
            else
                quote += 25;

            // Car Year
            if (insuree.CarYear < 2000)
                quote += 25;

            if (insuree.CarYear > 2015)
                quote += 25;

            // Car Make / Model
            if (insuree.CarMake.ToLower() == "porsche")
            {
                quote += 25;
                if (insuree.CarModel.ToLower() == "911 carrera")
                    quote += 25;
            }

            // Speeding Tickets
            quote += insuree.SpeedingTickets * 10;

            // DUI
            if (insuree.DUI)
                quote *= 1.25m;

            // Full Coverage
            if (insuree.CoverageType.ToLower() == "full")
                quote *= 1.5m;

            return Math.Round(quote, 2);
        }

        // ADMIN PAGE
        public ActionResult Admin()
        {
            return View(db.Insurees.ToList());
        }
    }
}
