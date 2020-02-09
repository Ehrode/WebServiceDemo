﻿using System.Linq.Expressions;

namespace WebServiceDemo.Core.Specifications {

    internal class ParameterReplacer : ExpressionVisitor {

        private readonly ParameterExpression _parameter;

        protected override Expression VisitParameter(ParameterExpression node) 
            => base.VisitParameter(_parameter);

        internal ParameterReplacer(ParameterExpression parameter) {
            _parameter = parameter;
        }
    }
}
