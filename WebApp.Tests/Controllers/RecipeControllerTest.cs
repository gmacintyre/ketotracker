using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp;
using WebApp.Classes;
using WebApp.Controllers;
using WebApp.Models;
using Moq;
using System.Data.Entity;

namespace WebApp.Tests.Controllers
{
    [TestClass]
    public class RecipeController
    {
        Mock<ketotrackerEntities> dbProvider = null;
        [TestInitialize]
        public void Setup()
        {
            dbProvider = new Mock<ketotrackerEntities>();
            dbProvider.Setup(r => r.Recipes.Add(null)).Returns(new Recipe());
            //dbProvider.Setup(r => r.SaveChanges()).Returns();
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            var recipe = new Recipe();
            recipe.RecipeId = 4;
            recipe.Name = "TRecipeName";
            recipe.CaloriesTotal = 500;
            recipe.FatTotal = 30;
            recipe.NetCarbsTotal = 4;
            recipe.ProteinTotal = 26;
            recipe.Servings = 4;
            
            // Act
            recipe.CalculateTotals();


            // Assert
            Assert.AreEqual(recipe.CaloriesPerServing, (decimal)125);
            Assert.AreEqual(recipe.FatPerServing, (decimal)7.5);
            Assert.AreEqual(recipe.NetCarbsPerServing, (decimal)1);
            Assert.AreEqual(recipe.ProteinPerServing, (decimal)6.5);
        }


    }
}
