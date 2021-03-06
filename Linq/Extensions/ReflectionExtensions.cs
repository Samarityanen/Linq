﻿namespace Bars.NuGet.Querying
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    internal static class ReflectionExtensions
    {
        internal static void SetValue(this MemberInfo memberInfo, object target, object value)
        {
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field:
                    ((FieldInfo)memberInfo).SetValue(target, value);
                    break;
                case MemberTypes.Property:
                    ((PropertyInfo)memberInfo).SetValue(target, value);
                    break;
                default: break;
            }
        }

        internal static Type MemberType(this MemberInfo memberInfo)
        {
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field: return ((FieldInfo)memberInfo).FieldType;
                case MemberTypes.Property: return ((PropertyInfo)memberInfo).PropertyType;
                default:return typeof(object);
            }
        }

        internal static T New<T>(this Type type,ConstructorInfo ctor, params object[] argsObj)
        {
            ParameterInfo[] par = ctor.GetParameters();
            Expression[] args = new Expression[par.Length];
            ParameterExpression param = Expression.Parameter(typeof(object[]));
            for (int i = 0; i != par.Length; ++i)
            {
                args[i] = Expression.Convert(Expression.ArrayIndex(param, Expression.Constant(i)), par[i].ParameterType);
            }
            var expression = Expression.Lambda<Func<object[], T>>(
                Expression.New(ctor, args), param
            );

            var func = expression.Compile();

            return func(argsObj);
        }
    }
}