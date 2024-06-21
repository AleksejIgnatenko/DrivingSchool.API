using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchool.DataAccess.Entities
{
	public class TestEntity
	{
		public Guid Id { get; set; }
		public CategoryEntity? Category { get; set; }
		public string? NameTest { get; set; }
	}
}
