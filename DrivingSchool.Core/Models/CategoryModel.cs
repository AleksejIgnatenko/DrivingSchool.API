using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DrivingSchool.Core.Models
{
    public class CategoryModel
    {
        public Guid IdCategory { get; }
        public string? NameCategory { get; }

        private CategoryModel(Guid idCategory, string? nameCategory)
        {
            IdCategory = idCategory;
            NameCategory = nameCategory;
        }

        public static (CategoryModel category, string error) Create(Guid idCategory, string? nameCategory)
        {
            string error = string.Empty;
            CategoryModel user = new CategoryModel(idCategory, nameCategory);
            return (user, error);
        }
    }
}
