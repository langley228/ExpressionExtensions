using System;
using System.Linq.Expressions;

namespace ExpressionExtensions
{
    /// <summary>
    /// 提供 OrElse 合併 Expression 表達式的靜態方法。
    /// 可將多個 Lambda 條件以 OR 邏輯合併為單一條件表達式。
    /// </summary>
    public static class OrElseExtensions
    {
        /// <summary>
        /// 以 OrElse 合併多個 Expression&lt;Func&lt;T, bool&gt;&gt;。
        /// </summary>
        /// <typeparam name="T">Lambda 參數型別。</typeparam>
        /// <param name="source">第一個表達式。</param>
        /// <param name="expr">第二個表達式。</param>
        /// <param name="exprs">其餘要合併的表達式。</param>
        /// <returns>合併後的 Expression&lt;Func&lt;T, bool&gt;&gt;。</returns>
        /// <example>
        /// <code>
        /// Expression&lt;Func&lt;int, bool&gt;&gt; expr1 = x =&gt; x &lt; 0;
        /// Expression&lt;Func&lt;int, bool&gt;&gt; expr2 = x =&gt; x &gt; 100;
        /// Expression&lt;Func&lt;int, bool&gt;&gt; expr3 = x =&gt; x == 50;
        /// var combined = expr1.OrElse(expr2, expr3);
        /// // combined: x =&gt; (x &lt; 0) || (x &gt; 100) || (x == 50)
        /// </code>
        /// </example>
        public static Expression<Func<T, bool>> OrElse<T>(
            this Expression<Func<T, bool>> source,
            Expression<Func<T, bool>> expr, params Expression<Func<T, bool>>[] exprs)
        {
            Expression<Func<T, bool>> result = source.OrElse(expr);
            foreach (var param in exprs)
            {
                result = result.OrElse(param);
            }
            return result;
        }

        /// <summary>
        /// 以 OrElse 合併兩個 Expression&lt;Func&lt;T, bool&gt;&gt;。
        /// </summary>
        /// <typeparam name="T">Lambda 參數型別。</typeparam>
        /// <param name="source">第一個表達式。</param>
        /// <param name="expr">第二個表達式。</param>
        /// <returns>合併後的 Expression&lt;Func&lt;T, bool&gt;&gt;。</returns>
        /// <example>
        /// <code>
        /// Expression&lt;Func&lt;int, bool&gt;&gt; expr1 = x =&gt; x &lt; 0;
        /// Expression&lt;Func&lt;int, bool&gt;&gt; expr2 = x =&gt; x &gt; 100;
        /// var combined = expr1.OrElse(expr2);
        /// // combined: x =&gt; (x &lt; 0) || (x &gt; 100)
        /// </code>
        /// </example>
        public static Expression<Func<T, bool>> OrElse<T>(
            this Expression<Func<T, bool>> source,
            Expression<Func<T, bool>> expr)
        {
            ParameterExpression p = source.Parameters[0];
            var visitor = new ParameterReplacer { [expr.Parameters[0]] = p };
            Expression body = Expression.OrElse(source.Body, visitor.Visit(expr.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        /// <summary>
        /// 以 OrElse 合併兩個 Expression&lt;Func&lt;T1, T2, bool&gt;&gt;。
        /// </summary>
        /// <typeparam name="T1">第一個 Lambda 參數型別。</typeparam>
        /// <typeparam name="T2">第二個 Lambda 參數型別。</typeparam>
        /// <param name="source">第一個表達式。</param>
        /// <param name="expr">第二個表達式。</param>
        /// <returns>合併後的 Expression&lt;Func&lt;T1, T2, bool&gt;&gt;。</returns>
        /// <example>
        /// <code>
        /// Expression&lt;Func&lt;int, string, bool&gt;&gt; expr1 = (x, y) =&gt; x &lt; 0;
        /// Expression&lt;Func&lt;int, string, bool&gt;&gt; expr2 = (x, y) =&gt; y == "ok";
        /// var combined = expr1.OrElse(expr2);
        /// // combined: (x, y) =&gt; (x &lt; 0) || (y == "ok")
        /// </code>
        /// </example>
        public static Expression<Func<T1, T2, bool>> OrElse<T1, T2>(
            this Expression<Func<T1, T2, bool>> source,
            Expression<Func<T1, T2, bool>> expr)
        {
            ParameterExpression p0 = source.Parameters[0];
            ParameterExpression p1 = source.Parameters[1];
            var visitor = new ParameterReplacer
            {
                [expr.Parameters[0]] = p0,
                [expr.Parameters[1]] = p1
            };
            Expression body = Expression.OrElse(source.Body, visitor.Visit(expr.Body));
            return Expression.Lambda<Func<T1, T2, bool>>(body, p0, p1);
        }

        /// <summary>
        /// 以 OrElse 合併兩個 Expression&lt;Func&lt;T1, T2, T3, bool&gt;&gt;。
        /// </summary>
        /// <typeparam name="T1">第一個 Lambda 參數型別。</typeparam>
        /// <typeparam name="T2">第二個 Lambda 參數型別。</typeparam>
        /// <typeparam name="T3">第三個 Lambda 參數型別。</typeparam>
        /// <param name="source">第一個表達式。</param>
        /// <param name="expr">第二個表達式。</param>
        /// <returns>合併後的 Expression&lt;Func&lt;T1, T2, T3, bool&gt;&gt;。</returns>
        /// <example>
        /// <code>
        /// Expression&lt;Func&lt;int, string, DateTime, bool&gt;&gt; expr1 = (a, b, c) =&gt; a &gt; 0;
        /// Expression&lt;Func&lt;int, string, DateTime, bool&gt;&gt; expr2 = (a, b, c) =&gt; c.Year &gt; 2000;
        /// var combined = expr1.OrElse(expr2);
        /// // combined: (a, b, c) =&gt; (a &gt; 0) || (c.Year &gt; 2000)
        /// </code>
        /// </example>
        public static Expression<Func<T1, T2, T3, bool>> OrElse<T1, T2, T3>(
            this Expression<Func<T1, T2, T3, bool>> source,
            Expression<Func<T1, T2, T3, bool>> expr)
        {
            ParameterExpression p0 = source.Parameters[0];
            ParameterExpression p1 = source.Parameters[1];
            ParameterExpression p2 = source.Parameters[2];
            var visitor = new ParameterReplacer
            {
                [expr.Parameters[0]] = p0,
                [expr.Parameters[1]] = p1,
                [expr.Parameters[2]] = p2
            };
            Expression body = Expression.OrElse(source.Body, visitor.Visit(expr.Body));
            return Expression.Lambda<Func<T1, T2, T3, bool>>(body, p0, p1, p2);
        }

        /// <summary>
        /// 以 OrElse 合併兩個 Expression&lt;Func&lt;T1, T2, T3, T4, bool&gt;&gt;。
        /// </summary>
        /// <typeparam name="T1">第一個 Lambda 參數型別。</typeparam>
        /// <typeparam name="T2">第二個 Lambda 參數型別。</typeparam>
        /// <typeparam name="T3">第三個 Lambda 參數型別。</typeparam>
        /// <typeparam name="T4">第四個 Lambda 參數型別。</typeparam>
        /// <param name="source">第一個表達式。</param>
        /// <param name="expr">第二個表達式。</param>
        /// <returns>合併後的 Expression&lt;Func&lt;T1, T2, T3, T4, bool&gt;&gt;。</returns>
        /// <example>
        /// <code>
        /// Expression&lt;Func&lt;int, string, DateTime, double, bool&gt;&gt; expr1 = (a, b, c, d) =&gt; a &gt; 0;
        /// Expression&lt;Func&lt;int, string, DateTime, double, bool&gt;&gt; expr2 = (a, b, c, d) =&gt; d &gt; 1.5;
        /// var combined = expr1.OrElse(expr2);
        /// // combined: (a, b, c, d) =&gt; (a &gt; 0) || (d &gt; 1.5)
        /// </code>
        /// </example>
        public static Expression<Func<T1, T2, T3, T4, bool>> OrElse<T1, T2, T3, T4>(
            this Expression<Func<T1, T2, T3, T4, bool>> source,
            Expression<Func<T1, T2, T3, T4, bool>> expr)
        {
            ParameterExpression p0 = source.Parameters[0];
            ParameterExpression p1 = source.Parameters[1];
            ParameterExpression p2 = source.Parameters[2];
            ParameterExpression p3 = source.Parameters[3];
            var visitor = new ParameterReplacer
            {
                [expr.Parameters[0]] = p0,
                [expr.Parameters[1]] = p1,
                [expr.Parameters[2]] = p2,
                [expr.Parameters[3]] = p3
            };
            Expression body = Expression.OrElse(source.Body, visitor.Visit(expr.Body));
            return Expression.Lambda<Func<T1, T2, T3, T4, bool>>(body, p0, p1, p2, p3);
        }

    }
}
