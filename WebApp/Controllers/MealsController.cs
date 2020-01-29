using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class MealsController : Controller
    {
        private ketotrackerEntities db = new ketotrackerEntities();

        private string _currentUserId;
        public string CurrentUserId
        {
            get
            {
                if(_currentUserId == null)
                {
                    _currentUserId = User.Identity.GetUserId();
                }
                return _currentUserId;
            }
            set
            {
                _currentUserId = value;
            }
        }

        // GET: Meals
        public ActionResult Index(int? dayNum)
        {
            var meals = Enumerable.Empty<Meal>().AsQueryable();
            if (dayNum.HasValue && dayNum.Value <= 0)
            {
                try
                {
                    DateTime day = DateTime.Today.AddDays(dayNum.Value);
                    ViewBag.Day = dayNum;
                    ViewBag.PreviousDay = dayNum - 1;
                    ViewBag.NextDay = dayNum + 1;
                    ViewBag.Message = $"Meals for {day.DayOfWeek}, {day.Month} {day.Day}, {day.Year}";
                    var dateCompare = day.Date;
                    var nextDayCompare = day.AddDays(1).Date;

                    meals = db.Meals.Include(m => m.Recipe).Where(x => x.UserId == CurrentUserId && x.DateEaten >= dateCompare && x.DateEaten <= nextDayCompare);

                }
                catch(Exception ex)
                {
                    ViewBag.Message = "All Meals ever";
                    meals = db.Meals.Include(m => m.Recipe).Where(x => x.UserId == CurrentUserId);
                }
               }
            else
            {
                ViewBag.Message = "All Meals ever";
                meals = db.Meals.Include(m => m.Recipe).Where(x => x.UserId == CurrentUserId);
            }
            return View(meals);
        }

        // GET: Meals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.Meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }

        // GET: Meals/Create
        public ActionResult Create()
        {
            ViewBag.RecipeId = new SelectList(db.Recipes, "RecipeId", "Name");
            return View();
        }

        // POST: Meals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MealId,RecipeId,UserId,Servings,DateEaten")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                meal.UserId = CurrentUserId;
                db.Meals.Add(meal);
                db.SaveChanges();
                var addAnother = true;
                //TODO: make add another a query string param or vm prop via a button "Add Another"
                if (addAnother)
                {
                    ViewBag.RecipeId = new SelectList(db.Recipes, "RecipeId", "Name");
                    return View();
                }
                return RedirectToAction("Index");
            }

            ViewBag.RecipeId = new SelectList(db.Recipes, "RecipeId", "Name", meal.RecipeId);
            return View(meal);
        }

        // GET: Meals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.Meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            ViewBag.RecipeId = new SelectList(db.Recipes, "RecipeId", "Name", meal.RecipeId);
            return View(meal);
        }

        // POST: Meals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MealId,RecipeId,UserId")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RecipeId = new SelectList(db.Recipes, "RecipeId", "Name", meal.RecipeId);
            return View(meal);
        }

        // GET: Meals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.Meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meal meal = db.Meals.Find(id);
            db.Meals.Remove(meal);
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
