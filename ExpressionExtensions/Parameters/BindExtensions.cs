using System;
using System.Linq.Expressions;

namespace ExpressionExtensions
{
    /// <summary>
    /// 提供 Expression 參數綁定（Partial Application）相關的擴充方法。
    /// </summary>
    public static class BindExtensions
    {
        /// <summary>
        /// 將雙參數 Lambda 表達式的第二個參數綁定為指定值，產生僅有一個參數的新 Lambda 表達式。
        /// 這是一種部分應用（Partial Application）的實現方式。
        /// </summary>
        /// <typeparam name="T1">第一個參數型別。</typeparam>
        /// <typeparam name="T2">要綁定的第二個參數型別。</typeparam>
        /// <param name="source">
        /// 原始雙參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T2, bool}}"/>。
        /// </param>
        /// <param name="value">要綁定到第二個參數的值。</param>
        /// <returns>
        /// 綁定後的單參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, bool}}"/>。
        /// </returns>
        /// <example>
        /// <code>
        /// Expression&lt;Func&lt;int, string, bool&gt;&gt; expr = (x, y) =&gt; x.ToString() == y;
        /// var bound = expr.Bind("123");
        /// // bound: x =&gt; x.ToString() == "123"
        /// </code>
        /// </example>
        public static Expression<Func<T1, bool>> Bind<T1, T2>(
            this Expression<Func<T1, T2, bool>> source, T2 value)
        {
            var visitor = new ParameterReplacer { [source.Parameters[1]] = Expression.Constant(value) };
            return Expression.Lambda<Func<T1, bool>>(visitor.Visit(source.Body), source.Parameters[0]);
        }

        /// <summary>
        /// 將三參數 Lambda 表達式的第三個參數綁定為指定值，產生僅有前兩個參數的新 Lambda 表達式。
        /// 這是一種部分應用（Partial Application）的實現方式。
        /// </summary>
        /// <typeparam name="T1">第一個參數型別。</typeparam>
        /// <typeparam name="T2">第二個參數型別。</typeparam>
        /// <typeparam name="T3">要綁定的第三個參數型別。</typeparam>
        /// <param name="source">
        /// 原始三參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T2, T3, bool}}"/>。
        /// </param>
        /// <param name="value">要綁定到第三個參數的值。</param>
        /// <returns>
        /// 綁定後的雙參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T2, bool}}"/>。
        /// </returns>
        /// <example>
        /// <code>
        /// Expression&lt;Func&lt;int, string, DateTime, bool&gt;&gt; expr = (x, y, z) =&gt; y.Length == z.Day;
        /// var bound = expr.Bind(new DateTime(2024, 5, 21));
        /// // bound: (x, y) =&gt; y.Length == 21
        /// </code>
        /// </example>
        public static Expression<Func<T1, T2, bool>> Bind<T1, T2, T3>(
            this Expression<Func<T1, T2, T3, bool>> source, T3 value)
        {
            var visitor = new ParameterReplacer { [source.Parameters[2]] = Expression.Constant(value) };
            return Expression.Lambda<Func<T1, T2, bool>>(visitor.Visit(source.Body), source.Parameters[0], source.Parameters[1]);
        }

        /// <summary>
        /// 將四參數 Lambda 表達式的第四個參數綁定為指定值，產生僅有前三個參數的新 Lambda 表達式。
        /// 這是一種部分應用（Partial Application）的實現方式。
        /// </summary>
        /// <typeparam name="T1">第一個參數型別。</typeparam>
        /// <typeparam name="T2">第二個參數型別。</typeparam>
        /// <typeparam name="T3">第三個參數型別。</typeparam>
        /// <typeparam name="T4">要綁定的第四個參數型別。</typeparam>
        /// <param name="source">
        /// 原始四參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T2, T3, T4, bool}}"/>。
        /// </param>
        /// <param name="value">要綁定到第四個參數的值。</param>
        /// <returns>
        /// 綁定後的三參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T2, T3, bool}}"/>。
        /// </returns>
        /// <example>
        /// <code>
        /// Expression&lt;Func&lt;int, string, DateTime, double, bool&gt;&gt; expr = (a, b, c, d) =&gt; d &gt; 1.5;
        /// var bound = expr.Bind(2.0);
        /// // bound: (a, b, c) =&gt; 2.0 &gt; 1.5
        /// </code>
        /// </example>
        public static Expression<Func<T1, T2, T3, bool>> Bind<T1, T2, T3, T4>(
            this Expression<Func<T1, T2, T3, T4, bool>> source, T4 value)
        {
            var visitor = new ParameterReplacer { [source.Parameters[3]] = Expression.Constant(value) };
            return Expression.Lambda<Func<T1, T2, T3, bool>>(
                visitor.Visit(source.Body),
                source.Parameters[0], source.Parameters[1], source.Parameters[2]);
        }

    }
}
