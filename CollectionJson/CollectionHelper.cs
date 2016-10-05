using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CollectionJson
{
    public static class CollectionHelper
    {
        //public static DataInfo[] Convert<T>(T someObject)
        //{
        //    //http://stackoverflow.com/questions/9210428/how-to-convert-class-into-dictionarystring-string
        //    return someObject.GetType()
        //        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        //        .ToDictionary(prop => prop.Name, prop => prop.GetValue(someObject, null))
        //        .Select(item => new DataInfo { Name = item.Key, Value = item.Value })
        //        .ToArray();
        //}

        public static DataInfo[] Convert<T>(T someObject, params string[] excludes)
        {
            //http://stackoverflow.com/questions/9210428/how-to-convert-class-into-dictionarystring-string
            return someObject.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(prop => excludes.Any() && !excludes.Contains(prop.Name))
                .Select(prop => new DataInfo { Name = prop.Name, Value = prop.GetValue(someObject, null) })
                .ToArray();
        }
    }
}
