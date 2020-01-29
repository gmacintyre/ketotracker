using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ketotrackerEntities db = new ketotrackerEntities();
        private string _currentUserId;
        public string CurrentUserId
        {
            get
            {
                if (_currentUserId == null)
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

        public ActionResult Index()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            var meals = db.Meals.Where(x => x.UserId == CurrentUserId && x.DateEaten >= today && x.DateEaten <= tomorrow);
            var recipeIds = meals.Select(x => x.RecipeId).Distinct().ToList();
            var recipes = db.Recipes.Where(x => recipeIds.Contains(x.RecipeId));
            var userProfile = db.UserProfiles.FirstOrDefault(x => x.AspNetUserId == CurrentUserId);
            var todaysIntake = IntakeViewModel.Create(meals, recipes, userProfile);

            return View(todaysIntake);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}