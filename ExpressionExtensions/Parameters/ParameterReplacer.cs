using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionExtensions
{
    /// <summary>
    /// ExpressionParameterReplacer 用於替換 Lambda 表達式中的參數節點。
    /// 繼承自 ExpressionVisitor，透過 subst 字典將指定的參數節點替換為新的表達式。
    /// </summary>
    internal class ParameterReplacer : ExpressionVisitor
    {
        /// <summary>
        /// 儲存要替換的參數與對應的新表達式。
        /// Key: 原始參數節點；Value: 替換後的表達式。
        /// </summary>
        public Dictionary<Expression, Expression> subst = new();

        /// <summary>
        /// 透過索引子設定要替換的參數與新表達式。
        /// </summary>
        /// <param name="key">原始參數節點</param>
        /// <param name="value">替換後的表達式</param>
        public Expression this[Expression key]
        {
            set => subst[key] = value;
        }

        /// <summary>
        /// 當拜訪到 ParameterExpression 節點時，若在 subst 字典中有對應的替換，則回傳替換後的表達式；
        /// 否則回傳原始節點。
        /// </summary>
        /// <param name="node">目前拜訪的參數節點</param>
        /// <returns>替換後的表達式或原始節點</returns>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (subst.TryGetValue(node, out var newValue))
                return newValue;
            return node;
        }
    }
}
