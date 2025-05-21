using System;
using System.Linq.Expressions;
using ExpressionExtensions;
using NUnit.Framework;

namespace ExpressionExtensionsTests
{
    /// <summary>
    /// 包含針對 OrElseExtensions 擴充方法的單元測試。
    /// 測試多種參數數量下的 OrElse 合併行為。
    /// </summary>
    [TestFixture]
    public class OrElseExtensionsTests
    {
        /// <summary>
        /// 測試兩個單參數 Lambda 表達式以 OrElse 合併時的結果。
        /// </summary>
        /// <remarks>
        /// 測試條件：
        /// - -1 滿足 x &lt; 0，結果為 true。
        /// - 11 滿足 x &gt; 10，結果為 true。
        /// - 5 兩者皆不滿足，結果為 false。
        /// </remarks>
        [Test]
        public void OrElse_TwoParameters_ReturnsCombined()
        {
            Expression<Func<int, bool>> expr1 = x => x < 0;
            Expression<Func<int, bool>> expr2 = x => x > 10;
            var combined = expr1.OrElse(expr2);
            Assert.That(combined.Compile()(-1), Is.True);
            Assert.That(combined.Compile()(11), Is.True);
            Assert.That(combined.Compile()(5), Is.False);
        }

        /// <summary>
        /// 測試兩個雙參數 Lambda 表達式以 OrElse 合併時的結果。
        /// </summary>
        /// <remarks>
        /// 測試條件：
        /// - (-1, "no") 滿足 x &lt; 0，結果為 true。
        /// - (1, "ok") 滿足 y == "ok"，結果為 true。
        /// - (1, "no") 兩者皆不滿足，結果為 false。
        /// </remarks>
        [Test]
        public void OrElse_ThreeParameters_ReturnsCombined()
        {
            Expression<Func<int, string, bool>> expr1 = (x, y) => x < 0;
            Expression<Func<int, string, bool>> expr2 = (x, y) => y == "ok";
            var combined = expr1.OrElse(expr2);
            Assert.That(combined.Compile()(-1, "no"), Is.True);
            Assert.That(combined.Compile()(1, "ok"), Is.True);
            Assert.That(combined.Compile()(1, "no"), Is.False);
        }

        /// <summary>
        /// 測試兩個四參數 Lambda 表達式以 OrElse 合併時的結果。
        /// </summary>
        /// <remarks>
        /// 測試條件：
        /// - (-1, "x", now, 1.0) 滿足 a &lt; 0，結果為 true。
        /// - (1, "x", now, 2.0) 滿足 d &gt; 1.5，結果為 true。
        /// - (1, "x", now, 1.0) 兩者皆不滿足，結果為 false。
        /// </remarks>
        [Test]
        public void OrElse_FourParameters_ReturnsCombined()
        {
            Expression<Func<int, string, DateTime, double, bool>> expr1 = (a, b, c, d) => a < 0;
            Expression<Func<int, string, DateTime, double, bool>> expr2 = (a, b, c, d) => d > 1.5;
            var combined = expr1.OrElse(expr2);
            Assert.That(combined.Compile()(-1, "x", DateTime.Now, 1.0), Is.True);
            Assert.That(combined.Compile()(1, "x", DateTime.Now, 2.0), Is.True);
            Assert.That(combined.Compile()(1, "x", DateTime.Now, 1.0), Is.False);
        }
    }
}
