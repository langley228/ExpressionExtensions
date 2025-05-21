using System;
using System.Linq.Expressions;

namespace ExpressionExtensions
{
    /// <summary>
    /// 提供對 Lambda Expression 進行邏輯否定（NOT）運算的擴充方法。
    /// </summary>
    public static class NotExtensions
    {
        /// <summary>
        /// 產生原單參數 Lambda 表達式的邏輯相反（NOT）表達式。
        /// </summary>
        /// <typeparam name="T">Lambda 參數型別。</typeparam>
        /// <param name="source">
        /// 原始單參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T, bool}}"/>。
        /// </param>
        /// <returns>
        /// 回傳邏輯否定後的新 Lambda 表達式，其型別為 <see cref="Expression{Func{T, bool}}"/>。
        /// </returns>
        /// <example>
        /// <code>
        /// Expression&lt;Func&lt;int, bool&gt;&gt; expr = x =&gt; x &gt; 0;
        /// var negated = expr.Not();
        /// // negated: x =&gt; !(x &gt; 0)
        /// </code>
        /// </example>
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> source)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.Not(source.Body), source.Parameters[0]);
        }

        /// <summary>
        /// 產生原雙參數 Lambda 表達式的邏輯相反（NOT）表達式。
        /// </summary>
        /// <typeparam name="T1">第一個 Lambda 參數型別。</typeparam>
        /// <typeparam name="T2">第二個 Lambda 參數型別。</typeparam>
        /// <param name="source">
        /// 原始雙參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T2, bool}}"/>。
        /// </param>
        /// <returns>
        /// 回傳邏輯否定後的新 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T2, bool}}"/>。
        /// </returns>
        /// <example>
        /// <code>
        /// Expression&lt;Func&lt;int, string, bool&gt;&gt; expr = (x, y) =&gt; y.Length == x;
        /// var negated = expr.Not();
        /// // negated: (x, y) =&gt; !(y.Length == x)
        /// </code>
        /// </example>
        public static Expression<Func<T1, T2, bool>> Not<T1, T2>(this Expression<Func<T1, T2, bool>> source)
        {
            return Expression.Lambda<Func<T1, T2, bool>>(Expression.Not(source.Body), source.Parameters[0], source.Parameters[1]);
        }

        /// <summary>
        /// 產生原三參數 Lambda 表達式的邏輯相反（NOT）表達式。
        /// </summary>
        /// <typeparam name="T1">第一個 Lambda 參數型別。</typeparam>
        /// <typeparam name="T2">第二個 Lambda 參數型別。</typeparam>
        /// <typeparam name="T3">第三個 Lambda 參數型別。</typeparam>
        /// <param name="source">
        /// 原始三參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T2, T3, bool}}"/>。
        /// </param>
        /// <returns>
        /// 回傳邏輯否定後的新 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T2, T3, bool}}"/>。
        /// </returns>
        /// <example>
        /// <code>
        /// Expression&lt;Func&lt;int, string, DateTime, bool&gt;&gt; expr = (x, y, z) =&gt; y.Length == z.Day;
        /// var negated = expr.Not();
        /// // negated: (x, y, z) =&gt; !(y.Length == z.Day)
        /// </code>
        /// </example>
        public static Expression<Func<T1, T2, T3, bool>> Not<T1, T2, T3>(this Expression<Func<T1, T2, T3, bool>> source)
        {
            return Expression.Lambda<Func<T1, T2, T3, bool>>(
                Expression.Not(source.Body),
                source.Parameters[0], source.Parameters[1], source.Parameters[2]);
        }

        /// <summary>
        /// 產生原四參數 Lambda 表達式的邏輯相反（NOT）表達式。
        /// </summary>
        /// <typeparam name="T1">第一個 Lambda 參數型別。</typeparam>
        /// <typeparam name="T2">第二個 Lambda 參數型別。</typeparam>
        /// <typeparam name="T3">第三個 Lambda 參數型別。</typeparam>
        /// <typeparam name="T4">第四個 Lambda 參數型別。</typeparam>
        /// <param name="source">
        /// 原始四參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T2, T3, T4, bool}}"/>。
        /// </param>
        /// <returns>
        /// 回傳邏輯否定後的新 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T2, T3, T4, bool}}"/>。
        /// </returns>
        /// <example>
        /// <code>
        /// Expression&lt;Func&lt;int, string, DateTime, double, bool&gt;&gt; expr = (a, b, c, d) =&gt; d &gt; 1.5;
        /// var negated = expr.Not();
        /// // negated: (a, b, c, d) =&gt; !(d &gt; 1.5)
        /// </code>
        /// </example>
        public static Expression<Func<T1, T2, T3, T4, bool>> Not<T1, T2, T3, T4>(this Expression<Func<T1, T2, T3, T4, bool>> source)
        {
            return Expression.Lambda<Func<T1, T2, T3, T4, bool>>(
                Expression.Not(source.Body),
                source.Parameters[0], source.Parameters[1], source.Parameters[2], source.Parameters[3]);
        }

    }
}
