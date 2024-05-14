using System;
using System.Collections.Generic;

namespace RecipeApp
{
    class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<RecipeStep> Steps { get; set; } = new List<RecipeStep>();
    }
}
