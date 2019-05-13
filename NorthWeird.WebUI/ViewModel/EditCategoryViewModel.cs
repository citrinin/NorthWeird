using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NorthWeird.Application.Models;
using NorthWeird.Domain.Entities;

namespace NorthWeird.WebUI.ViewModel
{
    public class EditCategoryViewModel
    {
        public CategoryDto Category { get; set; }

        public IFormFile FormFile { get; set; }
    }
}
