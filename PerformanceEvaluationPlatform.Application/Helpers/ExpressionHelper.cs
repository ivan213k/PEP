using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PerformanceEvaluationPlatform.Application.Helpers
{
	public static class ExpressionHelper
	{
		public static string GetPropertyName<T>(System.Linq.Expressions.Expression<Func<T, object>> property)
		{
			System.Linq.Expressions.LambdaExpression lambda = (System.Linq.Expressions.LambdaExpression)property;
			System.Linq.Expressions.MemberExpression memberExpression;

			if (lambda.Body is System.Linq.Expressions.UnaryExpression)
			{
				System.Linq.Expressions.UnaryExpression unaryExpression = (System.Linq.Expressions.UnaryExpression)(lambda.Body);
				memberExpression = (System.Linq.Expressions.MemberExpression)(unaryExpression.Operand);
			}
			else
			{
				memberExpression = (System.Linq.Expressions.MemberExpression)(lambda.Body);
			}

			return ((PropertyInfo)memberExpression.Member).Name;
		}
    }
}
