using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BeerDev.Entities.Util
{
    public class ObjectFactory<T> where T :class 
    {
        public object CreateDataShapedObject(T entity, IEnumerable<string> listFields)
        {
            if (!listFields.Any())
            {
                return entity;
            }

            IDictionary<string, object> result = new Dictionary<string, object>();
            foreach (string field in listFields)
            {
                var propertyInfo = entity.GetType()
                    .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null) continue;

                var fieldValue = propertyInfo.GetValue(entity, null);
                result.Add(field, fieldValue);
            }

            return result;
        }
    }
}
