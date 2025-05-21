using System;
using System.Linq.Expressions;
using ExpressionExtensions;
using NUnit.Framework;

namespace ExpressionExtensionsTests
{
    /// <summary>
    /// 包含針對 BindExtensions 擴充方法的單元測試。
    /// 測試多種參數數量下的 Bind（參數綁定/部分應用）行為。
    /// </summary>
    [TestFixture]
    public class BindExtensionsTests
    {
        /// <summary>
        /// 測試雙參數 Lambda 表達式經 Bind 擴充後，將第二個參數綁定為指定值的結果。
        /// </summary>
        /// <remarks>
        /// 測試條件：
        /// - (5) 綁定 "5"，5.ToString() == "5"，結果為 true。
        /// - (6) 綁定 "5"，6.ToString() == "5"，結果為 false。
        /// </remarks>
        [Test]
        public void Bind_TwoParameters_BindsSecond()
        {
            Expression<Func<int, string, bool>> expr = (x, y) => x.ToString() == y;
            var bound = expr.Bind("5");
            Assert.That(bound.Compile()(5), Is.True);
            Assert.That(bound.Compile()(6), Is.False);
        }

        /// <summary>
        /// 測試三參數 Lambda 表達式經 Bind 擴充後，將第三個參數綁定為指定值的結果。
        /// </summary>
        /// <remarks>
        /// 測試條件：
        /// - (1, "abc") 綁定 3，"abc".Length == 3，結果為 true。
        /// - (1, "ab") 綁定 3，"ab".Length == 3，結果為 false。
        /// </remarks>
        [Test]
        public void Bind_ThreeParameters_BindsThird()
        {
            Expression<Func<int, string, int, bool>> expr = (x, y, z) => y.Length == z;
            var bound = expr.Bind(3);
            Assert.That(bound.Compile()(1, "abc"), Is.True);
            Assert.That(bound.Compile()(1, "ab"), Is.False);
        }

        /// <summary>
        /// 測試四參數 Lambda 表達式經 Bind 擴充後，將第四個參數綁定為指定值的結果。
        /// </summary>
        /// <remarks>
        /// 測試條件：
        /// - (1, "x", 1) 綁定 2.0，2.0 > 1，結果為 true。
        /// - (1, "x", 3) 綁定 2.0，2.0 > 3，結果為 false。
        /// </remarks>
        [Test]
        public void Bind_FourParameters_BindsFourth()
        {
            Expression<Func<int, string, int, double, bool>> expr = (a, b, c, d) => d > c;
            var bound = expr.Bind(2.0);
            Assert.That(bound.Compile()(1, "x", 1), Is.True);
            Assert.That(bound.Compile()(1, "x", 3), Is.False);
        }
    }
}
