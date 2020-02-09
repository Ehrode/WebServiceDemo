﻿using System;
using System.Linq.Expressions;

namespace WebServiceDemo.Core.Specifications {

    public abstract class Specification<T> {

        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy(T entity) {
            var predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public Specification<T> And(Specification<T> specification) {
            return new AndSpecification<T>(this, specification);
        }


        public Specification<T> Or(Specification<T> specification) {
            return new OrSpecification<T>(this, specification);
        }
    }


    public class AndSpecification<T> : Specification<T> {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;


        public AndSpecification(Specification<T> left, Specification<T> right) {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression() {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var paramExpr = Expression.Parameter(typeof(T));
            var exprBody = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
            exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
            var finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);

            return finalExpr;
        }
    }


    public class OrSpecification<T> : Specification<T> {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;


        public OrSpecification(Specification<T> left, Specification<T> right) {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression() {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();
            var paramExpr = Expression.Parameter(typeof(T));
            var exprBody = Expression.OrElse(leftExpression.Body, rightExpression.Body);
            exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
            var finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);

            return finalExpr;
        }
    }
}