using System;
using System.Linq.Expressions;
using ExpressionExtensions;
using NUnit.Framework;

namespace ExpressionExtensionsTests
{
    /// <summary>
    /// 包含針對 NotExtensions 擴充方法的單元測試。
    /// 測試多種參數數量下的 Not（邏輯否定）行為。
    /// </summary>
    [TestFixture]
    public class NotExtensionsTests
    {
        /// <summary>
        /// 測試單參數 Lambda 表達式經 Not 擴充後的邏輯否定結果。
        /// </summary>
        /// <remarks>
        /// 測試條件：
        /// - -1 不滿足 x &gt; 0，否定後為 true。
        /// - 1 滿足 x &gt; 0，否定後為 false。
        /// </remarks>
        [Test]
        public void Not_OneParameter_ReturnsNegated()
        {
            Expression<Func<int, bool>> expr = x => x > 0;
            var negated = expr.Not();
            Assert.That(negated.Compile()(-1), Is.True);
            Assert.That(negated.Compile()(1), Is.False);
        }

        /// <summary>
        /// 測試雙參數 Lambda 表達式經 Not 擴充後的邏輯否定結果。
        /// </summary>
        /// <remarks>
        /// 測試條件：
        /// - (1, "ab") 不滿足 x == y.Length，否定後為 true。
        /// - (2, "ab") 滿足 x == y.Length，否定後為 false。
        /// </remarks>
        [Test]
        public void Not_TwoParameters_ReturnsNegated()
        {
            Expression<Func<int, string, bool>> expr = (x, y) => x == y.Length;
            var negated = expr.Not();
            Assert.That(negated.Compile()(1, "ab"), Is.True);
            Assert.That(negated.Compile()(2, "ab"), Is.False);
        }

        /// <summary>
        /// 測試三參數 Lambda 表達式經 Not 擴充後的邏輯否定結果。
        /// </summary>
        /// <remarks>
        /// 測試條件：
        /// - (1, "a", 2024-05-21) 不滿足 x == z.Day，否定後為 true。
        /// - (21, "a", 2024-05-21) 滿足 x == z.Day，否定後為 false。
        /// </remarks>
        [Test]
        public void Not_ThreeParameters_ReturnsNegated()
        {
            Expression<Func<int, string, DateTime, bool>> expr = (x, y, z) => x == z.Day;
            var negated = expr.Not();
            Assert.That(negated.Compile()(1, "a", new DateTime(2024, 5, 21)), Is.True);
            Assert.That(negated.Compile()(21, "a", new DateTime(2024, 5, 21)), Is.False);
        }

        /// <summary>
        /// 測試四參數 Lambda 表達式經 Not 擴充後的邏輯否定結果。
        /// </summary>
        /// <remarks>
        /// 測試條件：
        /// - (1, "x", now, 1.0) 不滿足 d &gt; 1.5，否定後為 true。
        /// - (1, "x", now, 2.0) 滿足 d &gt; 1.5，否定後為 false。
        /// </remarks>
        [Test]
        public void Not_FourParameters_ReturnsNegated()
        {
            Expression<Func<int, string, DateTime, double, bool>> expr = (a, b, c, d) => d > 1.5;
            var negated = expr.Not();
            Assert.That(negated.Compile()(1, "x", DateTime.Now, 1.0), Is.True);
            Assert.That(negated.Compile()(1, "x", DateTime.Now, 2.0), Is.False);
        }
    }
}
