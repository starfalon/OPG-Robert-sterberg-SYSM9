using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookmaster.Models
{
    public class Recipe
    {
        public string Title { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; } //?
        public string CreatedBy { get; set; }
        

        public void EditRecipe (string title, string ingredients, string instructions, string category) 
        {
            Title = title;
            Ingredients = ingredients;
            Instructions = instructions;
            Category = category;
            
        }
        public Recipe CopyRecipe()
        {
            return new Recipe
            {
                Title = Title,
                Ingredients = Ingredients,
                Instructions = Instructions,
                Category = Category,
                Date = Date,
                CreatedBy = CreatedBy,
                
            };
        }

    }

}
