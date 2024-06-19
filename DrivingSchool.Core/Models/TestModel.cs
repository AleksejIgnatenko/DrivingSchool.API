using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchool.Core.Models
{
	public class TestModel
	{
		public Guid IdTest { get; }
		public CategoryModel? Category { get; }
		public string? NameTest { get; }

        private TestModel(Guid id, CategoryModel? category, string? nameTest)
        {
			IdTest = id;
			Category = category;
			NameTest = nameTest;
        }

		public static (TestModel test, string error) Create(Guid id, CategoryModel? category, string? nameTest)
		{
			string error = string.Empty;
			TestModel test = new TestModel(id, category, nameTest);
			return (test, error);
		}
	}
}
