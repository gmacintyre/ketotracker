using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Classes
{
    public static class RecipleHelper
    {
        public static void CalculateTotals(this Recipe recipe)
        {
            recipe.CaloriesPerServing = recipe.CaloriesTotal / (decimal)recipe.Servings;
            recipe.ProteinPerServing = recipe.ProteinTotal / (decimal)recipe.Servings;
            recipe.NetCarbsPerServing = recipe.NetCarbsTotal / (decimal)recipe.Servings;
            recipe.FatPerServing = recipe.FatTotal / (decimal)recipe.Servings;
        }
    }
}