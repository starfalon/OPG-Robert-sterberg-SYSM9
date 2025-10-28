using Cookmaster.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookmaster.Models
{
    class AdminUser : User

    {
        public void RemoveAnyRecipe(RecipeManager manager, Recipe recipe)
        {
           manager.RemoveRecipe(recipe);
        }

        public List<Recipe> ViewAllRecipes(RecipeManager manager)
        {
            return manager.GetAllRecipes();
        }
    }
}
