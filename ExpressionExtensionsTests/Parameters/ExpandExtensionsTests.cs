using System;
using System.Linq.Expressions;
using ExpressionExtensions;
using NUnit.Framework;

namespace ExpressionExtensionsTests
{
    /// <summary>
    /// 包含針對 ExpandExtensions 擴充方法的單元測試。
    /// 測試多種參數數量下的 Expand（參數擴展）行為。
    /// </summary>
    [TestFixture]
    public class ExpandExtensionsTests
    {
        /// <summary>
        /// 測試單參數 Lambda 表達式經 Expand 擴充後，擴展為雙參數 Lambda 的結果。
        /// </summary>
        /// <remarks>
        /// 測試條件：
        /// - (1, "a")，1 > 0，結果為 true。
        /// - (-1, "b")，-1 > 0，結果為 false。
        /// </remarks>
        [Test]
        public void Expand_OneToTwoParameters()
        {
            Expression<Func<int, bool>> expr = x => x > 0;
            var expanded = expr.Expand<int, string>();
            Assert.That(expanded.Compile()(1, "a"), Is.True);
            Assert.That(expanded.Compile()(-1, "b"), Is.False);
        }

        /// <summary>
        /// 測試雙參數 Lambda 表達式經 Expand 擴充後，擴展為三參數 Lambda 的結果。
        /// </summary>
        /// <remarks>
        /// 測試條件：
        /// - (1, "1", now)，1.ToString() == "1"，結果為 true。
        /// - (2, "1", now)，2.ToString() == "1"，結果為 false。
        /// </remarks>
        [Test]
        public void Expand_TwoToThreeParameters()
        {
            Expression<Func<int, string, bool>> expr = (x, y) => x.ToString() == y;
            var expanded = expr.Expand<int, string, DateTime>();
            Assert.That(expanded.Compile()(1, "1", DateTime.Now), Is.True);
            Assert.That(expanded.Compile()(2, "1", DateTime.Now), Is.False);
        }

        /// <summary>
        /// 測試三參數 Lambda 表達式經 Expand 擴充後，擴展為四參數 Lambda 的結果。
        /// </summary>
        /// <remarks>
        /// 測試條件：
        /// - (21, "a", 2024-05-21, 1.0)，21 == 21，結果為 true。
        /// - (1, "a", 2024-05-21, 1.0)，1 == 21，結果為 false。
        /// </remarks>
        [Test]
        public void Expand_ThreeToFourParameters()
        {
            Expression<Func<int, string, DateTime, bool>> expr = (x, y, z) => x == z.Day;
            var expanded = expr.Expand<int, string, DateTime, double>();
            Assert.That(expanded.Compile()(21, "a", new DateTime(2024, 5, 21), 1.0), Is.True);
            Assert.That(expanded.Compile()(1, "a", new DateTime(2024, 5, 21), 1.0), Is.False);
        }
    }
}
