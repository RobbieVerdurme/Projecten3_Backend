﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Models
{
    public class Category
    {
        #region Properties

        public int CategoryId { get; set; }

        public string Name { get; set; }


        #endregion

        #region Constructors

        public Category()
        {
        }

        public Category(string name)
        {
            Name = name;
        }

        #endregion

    }
}
