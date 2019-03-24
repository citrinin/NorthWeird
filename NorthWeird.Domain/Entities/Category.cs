using System;
using System.Collections.Generic;
using System.Text;

namespace NorthWeird.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }
    }
}
