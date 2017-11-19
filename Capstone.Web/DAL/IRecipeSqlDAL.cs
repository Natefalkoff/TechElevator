﻿using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface IRecipeSqlDAL
    {
       bool NewRecipe(RecipeModel recipe);
       List<RecipeModel> GetRecipes();
       RecipeModel RecipeDetail(int id);
    }
}
