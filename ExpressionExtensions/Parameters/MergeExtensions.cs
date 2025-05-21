using System;
using System.Linq.Expressions;

namespace ExpressionExtensions
{
    /// <summary>
    /// 提供 Expression 參數合併相關的擴充方法。
    /// </summary>
    public static class MergeExtensions
    {
        /// <summary>
        /// 合併雙參數 Lambda 表達式的第二個參數至第一個參數，產生僅有一個參數的新 Lambda 表達式。
        /// 將原本表達式中所有第二個參數的引用，替換為第一個參數。
        /// </summary>
        /// <typeparam name="T">兩個參數的型別（必須相同）。</typeparam>
        /// <param name="source">
        /// 原始雙參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T, T, bool}}"/>。
        /// </param>
        /// <returns>
        /// 合併後的單參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T, bool}}"/>。
        /// </returns>
        /// <example>
        /// <code>
        /// Expression&lt;Func&lt;string, string, bool&gt;&gt; expr = (a, b) =&gt; a == b;
        /// var merged = expr.Merge();
        /// // merged: a =&gt; a == a
        /// </code>
        /// </example>
        public static Expression<Func<T, bool>> Merge<T>(
            this Expression<Func<T, T, bool>> source)
        {
            var visitor = new ParameterReplacer { [source.Parameters[1]] = source.Parameters[0] };
            return Expression.Lambda<Func<T, bool>>(visitor.Visit(source.Body), source.Parameters[0]);
        }

        /// <summary>
        /// 合併三參數 Lambda 表達式的第二與第三個參數，產生僅有兩個參數的新 Lambda 表達式。
        /// 將原本表達式中所有第三個參數的引用，替換為第二個參數。
        /// </summary>
        /// <typeparam name="T1">第一個參數型別。</typeparam>
        /// <typeparam name="T23">第二與第三個參數的型別（必須相同）。</typeparam>
        /// <param name="source">
        /// 原始三參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T23, T23, bool}}"/>。
        /// </param>
        /// <returns>
        /// 合併後的兩參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T23, bool}}"/>。
        /// </returns>
        /// <example>
        /// <code>
        /// Expression&lt;Func&lt;int, string, string, bool&gt;&gt; expr = (a, b, c) =&gt; b == c &amp;&amp; a.ToString() == b;
        /// var merged = expr.Merge();
        /// // merged: (a, b) =&gt; b == b &amp;&amp; a.ToString() == b
        /// </code>
        /// </example>
        public static Expression<Func<T1, T23, bool>> Merge<T1, T23>(
            this Expression<Func<T1, T23, T23, bool>> source)
        {
            // 建立 ExpressionParameterReplacer，將第三個參數（source.Parameters[2]）替換為第二個參數（source.Parameters[1]）
            var visitor = new ParameterReplacer { [source.Parameters[2]] = source.Parameters[1] };
            // 產生新的 Lambda 表達式，僅包含第一與第二個參數
            return Expression.Lambda<Func<T1, T23, bool>>(visitor.Visit(source.Body), source.Parameters[0], source.Parameters[1]);
        }

        /// <summary>
        /// 合併四參數 Lambda 表達式的第三與第四個參數，產生僅有前三個參數的新 Lambda 表達式。
        /// 將原本表達式中所有第四個參數的引用，替換為第三個參數。
        /// </summary>
        /// <typeparam name="T1">第一個參數型別。</typeparam>
        /// <typeparam name="T2">第二個參數型別。</typeparam>
        /// <typeparam name="T34">第三與第四個參數的型別（必須相同）。</typeparam>
        /// <param name="source">
        /// 原始四參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T2, T34, T34, bool}}"/>。
        /// </param>
        /// <returns>
        /// 合併後的三參數 Lambda 表達式，其型別為 <see cref="Expression{Func{T1, T2, T34, bool}}"/>。
        /// </returns>
        /// <example>
        /// <code>
        /// Expression&lt;Func&lt;int, string, DateTime, DateTime, bool&gt;&gt; expr = (a, b, c, d) =&gt; c == d &amp;&amp; b.Length == c.Day;
        /// var merged = expr.Merge();
        /// // merged: (a, b, c) =&gt; c == c &amp;&amp; b.Length == c.Day
        /// </code>
        /// </example>
        public static Expression<Func<T1, T2, T34, bool>> Merge<T1, T2, T34>(
            this Expression<Func<T1, T2, T34, T34, bool>> source)
        {
            var visitor = new ParameterReplacer { [source.Parameters[3]] = source.Parameters[2] };
            return Expression.Lambda<Func<T1, T2, T34, bool>>(
                visitor.Visit(source.Body),
                source.Parameters[0], source.Parameters[1], source.Parameters[2]);
        }

    }
}
