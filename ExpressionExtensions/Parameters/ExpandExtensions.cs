using System;
using System.Linq.Expressions;

namespace ExpressionExtensions
{
    /// <summary>
    /// 提供 Expression 參數擴展相關的擴充方法。
    /// </summary>
    public static class ExpandExtensions
    {
        /// <summary>
        /// 將單參數的 Lambda 表達式擴展為雙參數的 Lambda 表達式。
        /// 新增的第二個參數不會在表達式主體中被使用。
        /// </summary>
        /// <typeparam name="T1">原始參數型別。</typeparam>
        /// <typeparam name="T2">新增參數的型別。</typeparam>
        /// <param name="source">
        /// 原始單參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, bool}}"/>。
        /// </param>
        /// <returns>
        /// 擴展後的雙參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T2, bool}}"/>。
        /// </returns>
        /// <example>
        /// <code>
        /// Expression&lt;Func&lt;int, bool&gt;&gt; expr = x =&gt; x &gt; 0;
        /// var expanded = expr.Expand&lt;int, string&gt;();
        /// // expanded: (x, x_1) =&gt; x &gt; 0
        /// </code>
        /// </example>
        public static Expression<Func<T1, T2, bool>> Expand<T1, T2>(
            this Expression<Func<T1, bool>> source)
        {
            ParameterExpression p0 = source.Parameters[0];
            ParameterExpression p1 = Expression.Parameter(typeof(T2), $"{source.Parameters[0].Name}_1");
            return Expression.Lambda<Func<T1, T2, bool>>(source.Body, p0, p1);
        }

        /// <summary>
        /// 將雙參數的 Lambda 表達式擴展為三參數的 Lambda 表達式。
        /// 新增的第三個參數不會在表達式主體中被使用。
        /// </summary>
        /// <typeparam name="T1">第一個參數型別。</typeparam>
        /// <typeparam name="T2">第二個參數型別。</typeparam>
        /// <typeparam name="T3">新增參數的型別。</typeparam>
        /// <param name="source">
        /// 原始雙參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T2, bool}}"/>。
        /// </param>
        /// <returns>
        /// 擴展後的三參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T2, T3, bool}}"/>。
        /// </returns>
        /// <example>
        /// <code>
        /// Expression&lt;Func&lt;int, string, bool&gt;&gt; expr = (x, y) =&gt; x.ToString() == y;
        /// var expanded = expr.Expand&lt;int, string, DateTime&gt;();
        /// // expanded: (x, y, y_2) =&gt; x.ToString() == y
        /// </code>
        /// </example>
        public static Expression<Func<T1, T2, T3, bool>> Expand<T1, T2, T3>(
            this Expression<Func<T1, T2, bool>> source)
        {
            ParameterExpression p0 = source.Parameters[0];
            ParameterExpression p1 = source.Parameters[1];
            ParameterExpression p2 = Expression.Parameter(typeof(T3), $"{source.Parameters[1].Name}_2");
            return Expression.Lambda<Func<T1, T2, T3, bool>>(source.Body, p0, p1, p2);
        }

        /// <summary>
        /// 將三參數的 Lambda 表達式擴展為四參數的 Lambda 表達式。
        /// 新增的第四個參數不會在表達式主體中被使用。
        /// </summary>
        /// <typeparam name="T1">第一個參數型別。</typeparam>
        /// <typeparam name="T2">第二個參數型別。</typeparam>
        /// <typeparam name="T3">第三個參數型別。</typeparam>
        /// <typeparam name="T4">新增參數的型別。</typeparam>
        /// <param name="source">
        /// 原始三參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T2, T3, bool}}"/>。
        /// </param>
        /// <returns>
        /// 擴展後的四參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T2, T3, T4, bool}}"/>。
        /// </returns>
        /// <example>
        /// <code>
        /// Expression&lt;Func&lt;int, string, DateTime, bool&gt;&gt; expr = (x, y, z) =&gt; x.ToString() == y;
        /// var expanded = expr.Expand&lt;int, string, DateTime, double&gt;();
        /// // expanded: (x, y, z, z_3) =&gt; x.ToString() == y
        /// </code>
        /// </example>
        public static Expression<Func<T1, T2, T3, T4, bool>> Expand<T1, T2, T3, T4>(
            this Expression<Func<T1, T2, T3, bool>> source)
        {
            ParameterExpression p0 = source.Parameters[0];
            ParameterExpression p1 = source.Parameters[1];
            ParameterExpression p2 = source.Parameters[2];
            ParameterExpression p3 = Expression.Parameter(typeof(T4), $"{source.Parameters[2].Name}_3");
            return Expression.Lambda<Func<T1, T2, T3, T4, bool>>(source.Body, p0, p1, p2, p3);
        }

    }
}
