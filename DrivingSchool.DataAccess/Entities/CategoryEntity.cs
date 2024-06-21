using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchool.DataAccess.Entities
{
    public class CategoryEntity
    {
        public Guid Id { get; set; }
        public string? NameCategory { get; set; }
        public List<TestEntity>? TestEntities { get; set; }
    }
}
