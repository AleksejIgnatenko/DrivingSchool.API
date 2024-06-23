
namespace DrivingSchool.Core.Models
{
	public class TestModel
	{
		public Guid Id { get; }
		public CategoryModel? Category { get; }
		public string? NameTest { get; }
		public List<QuestionModel>? Questions { get; }

        private TestModel(Guid id, string? nameTest)
        {
            Id = id;
            NameTest = nameTest;
        }

        private TestModel(Guid id, CategoryModel? category, string? nameTest)
        {
            Id = id;
            Category = category;
            NameTest = nameTest;
        }

        private TestModel(Guid id, CategoryModel? category, string? nameTest, List<QuestionModel> questions)
        {
			Id = id;
			Category = category;
			NameTest = nameTest;
			Questions = questions;
        }

        public static (TestModel test, string error) Create(Guid id, string? nameTest)
        {
            string error = string.Empty;
            TestModel test = new TestModel(id, nameTest);
            return (test, error);
        }

        public static (TestModel test, string error) Create(Guid id, CategoryModel? category, string? nameTest)
        {
            string error = string.Empty;
            TestModel test = new TestModel(id, category, nameTest);
            return (test, error);
        }

        public static (TestModel test, string error) Create(Guid id, CategoryModel? category, string? nameTest, List<QuestionModel> questions)
		{
			string error = string.Empty;
			TestModel test = new TestModel(id, category, nameTest, questions);
			return (test, error);
		}
	}
}
