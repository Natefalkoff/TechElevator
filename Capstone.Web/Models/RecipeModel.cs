﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class RecipeModel
    {
        public int RecipeID { get; set; }
        public string Name { get; set; }
        public string Directions { get; set; }
        public int UserID { get; set; }
        public string Ingredients { get; set; }

        //[DataType(DataType.Upload)]
        public string ImageName { get; set; }

        //[DataType(DataType.Upload)]
        //HttpPostedFileBase ImageUpload { get; set; }

        public string Tags { get; set; }
        public List<string> Categories { get; set; }
        public int Publics { get; set; }
        public List<bool> Chosen { get; set; }
        public Dictionary<string, bool> ChoseCategory { get; set; }
    }
}