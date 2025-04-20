using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared.General.Builder
{
    public static class ExpressionBuilder
    {
        public static Expression<Func<T, bool>> BuildFilterExpression<T>(T filter)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression combined = null;

            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                var value = property.GetValue(filter);
                if (value == null || (property.PropertyType == typeof(string) && string.IsNullOrWhiteSpace(value.ToString())))
                    continue;

                var member = Expression.Property(parameter, property.Name);
                var constant = Expression.Constant(value);

                Expression condition;

                if (property.PropertyType == typeof(string))
                {
                    condition = Expression.Call(member, "Contains", null, constant);
                }
                else
                {
                    condition = Expression.Equal(member, constant);
                }

                combined = combined == null ? condition : Expression.AndAlso(combined, condition);
            }

            return combined != null
                ? Expression.Lambda<Func<T, bool>>(combined, parameter)
                : x => true;
        }
    }
}
