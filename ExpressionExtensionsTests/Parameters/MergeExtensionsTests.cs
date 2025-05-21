using System;
using System.Linq.Expressions;
using ExpressionExtensions;
using NUnit.Framework;

namespace ExpressionExtensionsTests
{
    /// <summary>
    /// 包含針對 MergeExtensions 擴充方法的單元測試。
    /// 測試多種參數數量下的 Merge（參數合併）行為。
    /// </summary>
    [TestFixture]
    public class MergeExtensionsTests
    {
        /// <summary>
        /// 測試雙參數 Lambda 表達式經 Merge 擴充後，將第二個參數合併至第一個參數的結果。
        /// </summary>
        /// <remarks>
        /// 測試條件：
        /// - ("x")：合併後 a == a，"x" == "x"，結果為 true。
        /// - ("")：合併後 a == a，"" == ""，結果為 true。
        /// </remarks>
        [Test]
        public void Merge_TwoParameters_MergesSecondToFirst()
        {
            Expression<Func<string, string, bool>> expr = (a, b) => a == b;
            var merged = expr.Merge();
            Assert.That(merged.Compile()("x"), Is.True);
            Assert.That(merged.Compile()(""), Is.True);
        }

        /// <summary>
        /// 測試三參數 Lambda 表達式經 Merge 擴充後，將第三個參數合併至第二個參數的結果。
        /// </summary>
        /// <remarks>
        /// 測試條件：
        /// - (1, "1")：合併後 b == b && a.ToString() == b，"1" == "1" && "1" == "1"，結果為 true。
        /// - (1, "2")：合併後 b == b && a.ToString() == b，"2" == "2" && "1" == "2"，結果為 false。
        /// </remarks>
        [Test]
        public void Merge_ThreeParameters_MergesThirdToSecond()
        {
            Expression<Func<int, string, string, bool>> expr = (a, b, c) => b == c && a.ToString() == b;
            var merged = expr.Merge();
            Assert.That(merged.Compile()(1, "1"), Is.True);
            Assert.That(merged.Compile()(1, "2"), Is.False);
        }

        /// <summary>
        /// 測試四參數 Lambda 表達式經 Merge 擴充後，將第四個參數合併至第三個參數的結果。
        /// </summary>
        /// <remarks>
        /// 測試條件：
        /// - (1, "aaa", dt)：dt = 2024-05-21，"aaa".Length = 3，dt.Day = 21，3 == 21，結果為 false。
        /// - (1, "xxxxxxxxxxxxxxxxxxxxx", dt)：dt = 2024-05-21，長度 21，21 == 21，結果為 true。
        /// </remarks>
        [Test]
        public void Merge_FourParameters_MergesFourthToThird()
        {
            Expression<Func<int, string, DateTime, DateTime, bool>> expr = (a, b, c, d) => c == d && b.Length == c.Day;
            var merged = expr.Merge();
            var dt = new DateTime(2024, 5, 21);
            Assert.That(merged.Compile()(1, "aaa", dt), Is.False); // 3 == 21
            Assert.That(merged.Compile()(1, new string('x', 21), dt), Is.True); // 21 == 21
        }
    }
}
