namespace DrivingSchool.DataAccess.Entities
{
    public class CategoryEntity
    {
        public Guid Id { get; set; }
        public string? NameCategory { get; set; }
        public List<TestEntity>? Tests { get; set; }
    }
}
