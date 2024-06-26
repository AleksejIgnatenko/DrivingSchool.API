using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchool.MockData
{
    public static class ObjectExtensions
    {
        public static Task<List<T>> AsListTask<T>(this T value)
            => new List<T> { value }.AsTask();

        public static Task<T> AsTask<T>(this T value)
            => Task.FromResult(value);
    }
}
