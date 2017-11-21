﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Web.Mvc;

namespace Capstone.Web.DAL
{
    public class SearchSqlDAL : ISearchSqlDAL
    {
        private readonly string connectionString;


        public SearchSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public HashSet<RecipeModel> GetSearchResults(SearchModel model)
        {
            HashSet<RecipeModel> results = new HashSet<RecipeModel>();
            
            string[] splitTags = model.TagSearch.Split(' ');
            List<string> list = new List<string>();
            foreach(KeyValuePair<string, bool> kvp in model.SearchCategories)
            {
                if(kvp.Value == true)
                {
                    list.Add(kvp.Key);
                }
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    if(list.Count > 0)
                    {
                        for(int i = 0; i < list.Count; i++)
                        {
                            RecipeModel r = new RecipeModel();
                            string categoryResults = string.Format(@"SELECT * FROM recipe JOIN recipe_category ON recipe.recipe_id = recipe_category.recipe_id JOIN category ON recipe_category.category_id = category.category_id WHERE category_name = @catName{0};", i);
                            SqlCommand cmd = new SqlCommand(categoryResults, conn);
                            cmd.Parameters.AddWithValue(string.Format("@catName{0}", i), list[i]);
                            SqlDataReader read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                r.Name = Convert.ToString(read["recipe_name"]);
                                r.Directions = Convert.ToString(read["directions"]);
                                r.ImageName = Convert.ToString(read["image_name"]);
                                r.Ingredients = Convert.ToString(read["ingredients"]);
                                r.RecipeID = Convert.ToInt32(read["recipe_id"]);
                                results.Add(r);
                            }
                        }
                    }
                    conn.Close();

                    conn.Open();
                    if(splitTags.Length > 0)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            RecipeModel r = new RecipeModel();
                            string categoryResults = string.Format(@"SELECT * FROM recipe JOIN recipe_tags ON recipe.recipe_id = recipe_tags.tag_id JOIN tags ON recipe_tags.tag_id = tags.tag_id WHERE tag_name = @tagName{0};", i);
                            SqlCommand cmd = new SqlCommand(categoryResults, conn);
                            cmd.Parameters.AddWithValue(string.Format("@tagName{0}", i), list[i]);
                            SqlDataReader read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                r.Name = Convert.ToString(read["recipe_name"]);
                                r.Directions = Convert.ToString(read["directions"]);
                                r.ImageName = Convert.ToString(read["image_name"]);
                                r.Ingredients = Convert.ToString(read["ingredients"]);
                                r.RecipeID = Convert.ToInt32(read["recipe_id"]);
                                results.Add(r);
                            }
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return results;
        }
    }
}