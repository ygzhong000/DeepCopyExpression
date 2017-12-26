using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DeepCopyExpression
{
    /********************************************************************************
    ** auth： 杨光忠
    ** date： 2017/8/18 星期五 9:50:06
    ** desc： 尚未编写描述
    ** Ver.:  V1.0.0
    *********************************************************************************/
    sealed class DeepCopyExp<TIn, TOut> : MarshalByRefObject
    {
        private static readonly Func<TIn, TOut> Cache = GetExp();

        private static Func<TIn, TOut> GetExp()
        {
            ParameterExpression expression = Expression.Parameter(typeof(TIn), "p");
            List<MemberBinding> member = new List<MemberBinding>();

            foreach (var item in typeof(TOut).GetProperties())
            {
                if (!item.CanWrite)
                    continue;
                MemberExpression property = Expression.Property(expression, typeof(TIn).GetProperty(item.Name));
                MemberBinding memberBinding = Expression.Bind(item, property);
                member.Add(memberBinding);
            }

            MemberInitExpression memberInitExpression =
                Expression.MemberInit(Expression.New(typeof(TOut)), member.ToArray());
            Expression<Func<TIn, TOut>> lambda =
                Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new[] {expression});
            return lambda.Compile();
        }

        public static TOut Copy(TIn tIn)
        {
            return Cache(tIn);
        }
    }
}
