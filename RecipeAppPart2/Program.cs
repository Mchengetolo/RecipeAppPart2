using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApp
{
    class Program
    {
        delegate void RecipeCalorieNotification(string recipeName, double totalCalories);

        static void Main(string[] args)
        {
            List<Recipe> recipes = new List<Recipe>();

            while (true)
            {
                Console.WriteLine("1. Add Recipe");
                Console.WriteLine("2. Delete Recipe");
                Console.WriteLine("3. View Recipes");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Choose an option:");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddRecipe(recipes);
                        break;
                    case "2":
                        DeleteRecipe(recipes);
                        break;
                    case "3":
                        ViewRecipes(recipes);
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddRecipe(List<Recipe> recipes)
        {
            Recipe recipe = new Recipe();

            Console.WriteLine("Enter the name of the recipe: ");
            string recipeName = Console.ReadLine();

            recipe.Name = recipeName;

            Console.WriteLine("Enter the number of ingredients for " + recipe.Name + ": ");
            int numIngredients = int.Parse(Console.ReadLine());

            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine("Enter the name of ingredient " + (i + 1) + ": ");
                string name = Console.ReadLine();

                Console.WriteLine("Enter the quantity of ingredient " + (i + 1) + ": ");
                double quantity = double.Parse(Console.ReadLine());

                Console.WriteLine("Enter the unit of measurement of ingredient " + (i + 1) + ": ");
                string unit = Console.ReadLine();

                Console.WriteLine("Enter the calories of ingredient " + (i + 1) + ": ");
                double calories = double.Parse(Console.ReadLine());

                Console.WriteLine("Enter the food group of ingredient " + (i + 1) + ": ");
                string foodGroup = Console.ReadLine();

                recipe.Ingredients.Add(new Ingredient { Name = name, Quantity = quantity, Unit = unit, Calories = calories, FoodGroup = foodGroup });
            }

            recipes.Add(recipe);
        }

        static void DeleteRecipe(List<Recipe> recipes)
        {
            Console.WriteLine("Enter the name of the recipe to delete: ");
            string recipeName = Console.ReadLine();

            Recipe recipeToDelete = recipes.FirstOrDefault(r => r.Name == recipeName);

            if (recipeToDelete != null)
            {
                recipes.Remove(recipeToDelete);
                Console.WriteLine($"Recipe '{recipeName}' deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Recipe '{recipeName}' not found.");
            }
        }

        static void ViewRecipes(List<Recipe> recipes)
        {
            if (recipes.Any())
            {
                // Sorting recipes alphabetically by name
                recipes.Sort((x, y) => string.Compare(x.Name, y.Name));

                // Displaying sorted recipes
                Console.WriteLine("\nRecipes (sorted by name):");
                foreach (var recipe in recipes)
                {
                    Console.WriteLine("\nRecipe: " + recipe.Name);
                    foreach (Ingredient ingredient in recipe.Ingredients)
                    {
                        Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} {ingredient.Name}, Calories: {ingredient.Calories}, Food Group: {ingredient.FoodGroup}");
                    }

                    // Calculate and display total calories
                    double totalCalories = CalculateTotalCalories(recipe);
                    Console.WriteLine($"Total Calories: {totalCalories}");

                    // Notify if total calories exceed 300
                    if (totalCalories > 300)
                    {
                        Console.WriteLine($"WARNING: The total calories for '{recipe.Name}' exceed 300.");
                    }
                }
            }
            else
            {
                Console.WriteLine("No recipes available.");
            }
        }

        static double CalculateTotalCalories(Recipe recipe)
        {
            double totalCalories = 0;
            foreach (var ingredient in recipe.Ingredients)
            {
                totalCalories += ingredient.Calories;
            }
            return totalCalories;
        }
    }
}