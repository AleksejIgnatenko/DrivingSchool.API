
namespace DrivingSchool.Core.Models
{
    public class CategoryModel
    {
        public Guid Id { get; }
        public string? NameCategory { get; }
        public List<TestModel>? Tests { get; }

        private CategoryModel(Guid idCategory, string? nameCategory)
        {
            Id = idCategory;
            NameCategory = nameCategory;
        }

        private CategoryModel(Guid idCategory, string? nameCategory, List<TestModel>? tests) : this(idCategory, nameCategory)
        {
            Tests = tests;
        }

        public static (CategoryModel category, string error) Create(Guid idCategory, string? nameCategory)
        {
            string error = string.Empty;
            CategoryModel category = new CategoryModel(idCategory, nameCategory);
            return (category, error);
        }

        public static (CategoryModel category, string error) Create(Guid idCategory, string? nameCategory, List<TestModel> tests)
        {
            string error = string.Empty;
            CategoryModel category = new CategoryModel(idCategory, nameCategory, tests);
            return (category, error);
        }
    }
}
