﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using Microsoft.Azure.Templates.Analyzer.Types;
using Microsoft.Azure.Templates.Analyzer.Utilities;

namespace Microsoft.Azure.Templates.Analyzer.RuleEngines.JsonEngine.Expressions
{
    /// <summary>
    /// Represents an not expression in a JSON rule.
    /// </summary>
    internal class NotExpression : Expression
    {
        /// <summary>
        /// Gets the expressions to be negated.
        /// </summary>
        public Expression ExpressionToNegate { get; private set; }

        /// <summary>
        /// Creates a <see cref="NotExpression"/>.
        /// </summary>
        /// <param name="expressionToNegate">The expression to negate the result of.</param>
        /// <param name="commonProperties">The properties common across all <see cref="Expression"/> types.</param>
        public NotExpression(Expression expressionToNegate, ExpressionCommonProperties commonProperties)
            : base(commonProperties)
        {
            this.ExpressionToNegate = expressionToNegate ?? throw new ArgumentNullException(nameof(expressionToNegate));
        }

        /// <summary>
        /// Evaluates all expression and negates it in a final <see cref="JsonRuleEvaluation"/>.
        /// </summary>
        /// <param name="jsonScope">The json to evaluate.</param>
        /// <param name="jsonLineNumberResolver">An <see cref="ILineNumberResolver"/> to
        /// map JSON paths in the returned evaluation to the line number in the JSON evaluated.</param>
        /// <returns>A <see cref="JsonRuleEvaluation"/> with the results of the evaluation.</returns>
        public override JsonRuleEvaluation Evaluate(IJsonPathResolver jsonScope, ILineNumberResolver jsonLineNumberResolver)
        {
            return EvaluateInternal(jsonScope, scope =>
            {
                var evaluation = ExpressionToNegate.Evaluate(scope, jsonLineNumberResolver);

                return evaluation;
            });
        }
    }
}