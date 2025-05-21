using System;
using System.Linq.Expressions;
using ExpressionExtensions;
using NUnit.Framework;

namespace ExpressionExtensionsTests
{
    /// <summary>
    /// 包含針對 AndAlsoExtensions 擴充方法的單元測試。
    /// 測試多種參數數量下的 AndAlso 合併行為。
    /// </summary>
    [TestFixture]
    public class AndAlsoExtensionsTests
    {
        /// <summary>
        /// 測試兩個單參數 Lambda 表達式以 AndAlso 合併時的結果。
        /// </summary>
        /// <remarks>
        /// 測試條件：
        /// - 5 應同時滿足 x &gt; 0 及 x &lt; 10，結果為 true。
        /// - -1 不滿足 x &gt; 0，結果為 false。
        /// - 15 不滿足 x &lt; 10，結果為 false。
        /// </remarks>
        [Test]
        public void AndAlso_TwoParameters_ReturnsCombined()
        {
            Expression<Func<int, bool>> expr1 = x => x > 0;
            Expression<Func<int, bool>> expr2 = x => x < 10;
            var combined = expr1.AndAlso(expr2);
            Assert.That(combined.Compile()(5), Is.True);
            Assert.That(combined.Compile()(-1), Is.False);
            Assert.That(combined.Compile()(15), Is.False);
        }

        /// <summary>
        /// 測試兩個雙參數 Lambda 表達式以 AndAlso 合併時的結果。
        /// </summary>
        /// <remarks>
        /// 測試條件：
        /// - (5, "abc") 同時滿足 x &gt; 0 及 y.Length &gt; 2，結果為 true。
        /// - (5, "a") 不滿足 y.Length &gt; 2，結果為 false。
        /// - (-1, "abc") 不滿足 x &gt; 0，結果為 false。
        /// </remarks>
        [Test]
        public void AndAlso_ThreeParameters_ReturnsCombined()
        {
            Expression<Func<int, string, bool>> expr1 = (x, y) => x > 0;
            Expression<Func<int, string, bool>> expr2 = (x, y) => y.Length > 2;
            var combined = expr1.AndAlso(expr2);
            Assert.That(combined.Compile()(5, "abc"), Is.True);
            Assert.That(combined.Compile()(5, "a"), Is.False);
            Assert.That(combined.Compile()(-1, "abc"), Is.False);
        }

        /// <summary>
        /// 測試兩個四參數 Lambda 表達式以 AndAlso 合併時的結果。
        /// </summary>
        /// <remarks>
        /// 測試條件：
        /// - (2, "x", now, 2.0) 同時滿足 a &gt; 0 及 d &gt; 1.5，結果為 true。
        /// - (2, "x", now, 1.0) 不滿足 d &gt; 1.5，結果為 false。
        /// - (-1, "x", now, 2.0) 不滿足 a &gt; 0，結果為 false。
        /// </remarks>
        [Test]
        public void AndAlso_FourParameters_ReturnsCombined()
        {
            Expression<Func<int, string, DateTime, double, bool>> expr1 = (a, b, c, d) => a > 0;
            Expression<Func<int, string, DateTime, double, bool>> expr2 = (a, b, c, d) => d > 1.5;
            var combined = expr1.AndAlso(expr2);
            Assert.That(combined.Compile()(2, "x", DateTime.Now, 2.0), Is.True);
            Assert.That(combined.Compile()(2, "x", DateTime.Now, 1.0), Is.False);
            Assert.That(combined.Compile()(-1, "x", DateTime.Now, 2.0), Is.False);
        }
    }
}
