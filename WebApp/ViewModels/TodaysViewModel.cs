using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class IntakeViewModel
    {

        public decimal MaxCalories { get; set; }
        public decimal MaxCarbs { get; set; }
        public decimal MaxProtein { get; set; }
        public decimal MaxFat { get; set; }

        public decimal CalorieIntake { get; set; }
        public decimal CarbIntake { get; set; }
        public decimal ProteinIntake { get; set; }
        public decimal FatIntake { get; set; }

        public static IntakeViewModel Create(IEnumerable<Meal> Meals, IEnumerable<Recipe> Recipes, UserProfile userProfile)
        {
            var intakeVM = new IntakeViewModel();

            intakeVM.MaxCalories = userProfile.CaloricIntakePerDay;
            intakeVM.MaxCarbs = userProfile.NetCarbsPerDay;
            intakeVM.MaxFat = userProfile.FatPerDay;
            intakeVM.MaxProtein = userProfile.ProteinPerDay;

            foreach(var meal in Meals)
            {
                var recipe = Recipes.FirstOrDefault(x => x.RecipeId == meal.RecipeId);
                if(recipe != null)
                {
                    intakeVM.CalorieIntake += recipe.CaloriesPerServing * meal.Servings;
                    intakeVM.CarbIntake += recipe.NetCarbsPerServing * meal.Servings;
                    intakeVM.ProteinIntake += recipe.ProteinPerServing * meal.Servings;
                    intakeVM.FatIntake += recipe.FatPerServing * meal.Servings;
                }
            }

            return intakeVM;
        }
    }
}