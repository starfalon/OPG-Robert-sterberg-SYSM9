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
            //if (recipe.Date == default)
            //    recipe.Date = DateTime.Now;
            //recipes.Add(recipe);

            if (recipe == null) return;

            if (recipe.Date == default)
                recipe.Date = DateTime.Now;

            // 🔒 Dublettskydd (Title + CreatedBy räcker ofta i skolprojekt)
            bool exists = recipes.Any(r =>
                string.Equals(r.Title, recipe.Title, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(r.CreatedBy, recipe.CreatedBy, StringComparison.OrdinalIgnoreCase));

            if (!exists)
                recipes.Add(recipe);
        }

        public void RemoveRecipe(Recipe recipe)
        {
            if (recipe == null) return;

            var match = recipes.FirstOrDefault(r =>
                string.Equals(r.Title, recipe.Title, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(r.CreatedBy, recipe.CreatedBy, StringComparison.OrdinalIgnoreCase));

            if (match != null)
                recipes.Remove(match);
        }
        public List<Recipe> GetAllRecipes() //??
        {
            return recipes;
        }

        public List<string> GetAllCategories()
        {
            if (!recipes.Any())
                return new List<string>();

            
            return recipes
                .Select(r => r.Category)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(c => c)
                .ToList();
        }

        public List<Recipe> GetByUser(User user)
        {
            //List<Recipe> userRecipes = new List<Recipe>();
            //foreach (var recipe in recipes)
            //{
            //    if (recipe.CreatedBy == user.Username)
            //    {
            //        userRecipes.Add(recipe);
            //    }

            //}
            //return userRecipes;

            return recipes
        .Where(r => string.Equals(r.CreatedBy, user.Username, StringComparison.OrdinalIgnoreCase))
        .ToList();
        }

        public List<Recipe> Filter(string search, string selectedCategory, string dateFilter)
        {
            var filtered = new List<Recipe>();

            if (recipes == null || recipes.Count == 0)
                return filtered;

            DateTime? minDate = null;

            if (dateFilter == "Last 7 days")
                minDate = DateTime.Now.AddDays(-7);
            else if (dateFilter == "Last 30 days")
                minDate = DateTime.Now.AddDays(-30);
            else if (dateFilter == "Today")
                minDate = DateTime.Today;

            foreach (var recipe in recipes)
            {
                bool match = true;

                
                if (!string.IsNullOrWhiteSpace(selectedCategory))
                {
                    if (string.IsNullOrEmpty(recipe.Category) ||
                        !recipe.Category.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase))
                    {
                        match = false;
                    }
                }

                
                if (match && !string.IsNullOrWhiteSpace(search))
                {
                    bool textMatch = false;

                    if (!string.IsNullOrEmpty(recipe.Title) &&
                        recipe.Title.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        textMatch = true;
                    }
                    else if (!string.IsNullOrEmpty(recipe.Category) &&
                             recipe.Category.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        textMatch = true;
                    }

                    if (!textMatch)
                        match = false;
                }

                if (match && minDate.HasValue)
                {
                    if (recipe.Date < minDate.Value)
                        match = false;
                }

                if (match)
                    filtered.Add(recipe);
            }

            return filtered;
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
