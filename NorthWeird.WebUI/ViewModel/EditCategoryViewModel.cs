using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NorthWeird.Domain.Entities;

namespace NorthWeird.WebUI.ViewModel
{
    public class EditCategoryViewModel
    {
        public Category Category { get; set; }

        public IFormFile FormFile { get; set; }
    }
}
