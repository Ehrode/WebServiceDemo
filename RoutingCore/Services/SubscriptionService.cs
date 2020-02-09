using WebServiceDemo.Core.Interfaces;
using WebServiceDemo.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoutingCore;

namespace WebServiceDemo.Core.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IGenericRepository<StudentCourses> _registrationRepository;
        private readonly IGenericRepository<Student> _studentRepository;
        private readonly IGenericRepository<Course> _courseRepository;

        public SubscriptionService(
            IGenericRepository<StudentCourses> registrationRepository,
            IGenericRepository<Student> studentRepository,
            IGenericRepository<Course> courseRepository
            )
        {
            _registrationRepository = registrationRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public async Task<StudentCourses> RegisterStudentToCourse(int studentId, int courseId)
        {
            await _registrationRepository.Insert(new StudentCourses
            {
                IdStudent = studentId,
                IdCourse = courseId,
                Registered = DateTime.Now
            });

            var createdInstanceQuery =
                await _registrationRepository
                .Find(new RegistrationsFromIdsSpecification(studentId, courseId));

            return createdInstanceQuery.Single(sc => sc.IdStudent == studentId && sc.IdCourse == courseId);
        }

        public async Task<IEnumerable<Course>> GetStudentCourses(int studentId)
        {
            var studentWithCourses = await _studentRepository.GetById(studentId,
                new []{ "StudentCourses.Course" });

            if (studentWithCourses == null)
                throw new InvalidOperationException($"Student with id <{studentId}> does not exists");

            return studentWithCourses.StudentCourses.Select(sc => sc.Course);
        }
    }
}
