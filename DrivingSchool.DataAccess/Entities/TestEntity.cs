
namespace DrivingSchool.DataAccess.Entities
{
	public class TestEntity
	{
		public Guid Id { get; set; }
		public CategoryEntity? Category { get; set; }
		public List<QuestionEntity>? Questions { get; set; }
		public string? NameTest { get; set; }
	}
}
