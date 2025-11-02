using Cookmaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookmaster.Managers
{
    public class RecipeManager
    {
        public List<Recipe> recipes = new List<Recipe>();


        public void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
        }

        public void RemoveRecipe(Recipe r)
        {
            recipes.Remove(r);
        }
        public List<Recipe> GetAllRecipes() //??
        {
            return recipes;
        }

        public List<string> GetAllCategories()
        {
            if (!recipes.Any())
                return new List<string>();

            // Returnera unika kategorier i bokstavsordning
            return recipes
                .Select(r => r.Category)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(c => c)
                .ToList();
        }

        public List<Recipe> GetByUser(User user)
        {
            List<Recipe> userRecipes = new List<Recipe>();
            foreach (var recipe in recipes)
            {
                if (recipe.CreatedBy == user.Username)
                {
                    userRecipes.Add(recipe);
                }
            
            }
            return userRecipes;
        }

        public List<Recipe> Filter(string search)
        {
            List<Recipe> filteredRecipes = new List<Recipe>();
            foreach (var recipe in recipes)
            {
                if (recipe.Title.Contains(search) || recipe.Category.Contains(search))
                {
                    filteredRecipes.Add(recipe);
                }
            }
            return filteredRecipes;
        }

        public void UpdateRecipe(Recipe updatedRecipe)
        {
            foreach (var recipe in recipes)
            {
                if (recipe.Title == updatedRecipe.Title)
                {
                    recipe.Ingredients = updatedRecipe.Ingredients;
                    recipe.Instructions = updatedRecipe.Instructions;
                    recipe.Category = updatedRecipe.Category;
                    recipe.Date = updatedRecipe.Date;
                    recipe.CreatedBy = updatedRecipe.CreatedBy;
                    break;
                }
            }
        }

    }
}
