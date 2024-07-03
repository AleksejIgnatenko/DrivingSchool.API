
namespace DrivingSchool.Core.Models
{
	public class TestModel
	{
		public Guid Id { get; }
		public CategoryModel? Category { get; }
        public List<QuestionModel>? Questions { get; }
        public List<AnswerUserTestModel>? Answers { get; }
        public string? NameTest { get; }

        private TestModel(Guid id, string? nameTest)
        {
            Id = id;
            NameTest = nameTest;
        }

        private TestModel(Guid id, CategoryModel? category, string? nameTest) : this(id, nameTest)
        {
            Category = category;
        }

        private TestModel(Guid id, CategoryModel? category, List<QuestionModel> questions, string? nameTest) : this(id, category, nameTest)
        {
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

        public static (TestModel test, string error) Create(Guid id, CategoryModel? category, List<QuestionModel> questions, string? nameTest)
		{
			string error = string.Empty;
			TestModel test = new TestModel(id, category, questions, nameTest);
			return (test, error);
		}
	}
}
