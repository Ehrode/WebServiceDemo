using RoutingCore;
using System;
using System.Linq.Expressions;

namespace WebServiceDemo.Core.Specifications
{
    public class RegistrationsFromIdsSpecification : Specification<StudentCourses>
    {
        private readonly int _studentId;
        private readonly int _courseId;

        public RegistrationsFromIdsSpecification(int studentId, int courseId)
        {
            _studentId = studentId;
            _courseId = courseId;
        }

        public override Expression<Func<StudentCourses, bool>> ToExpression()
        {
            return studentCourse => studentCourse.IdStudent == _studentId && studentCourse.IdCourse == _courseId;
        }
    }
}
