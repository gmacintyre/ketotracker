//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Meal
    {
        public int MealId { get; set; }
        public int RecipeId { get; set; }
        public string UserId { get; set; }
        public int Servings { get; set; }
        public DateTime DateEaten { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
