using RoutingCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebServiceDemo.Core.Interfaces
{
    public interface ISubscriptionService
    {
        Task<StudentCourses> RegisterStudentToCourse(int studentId, int courseId);
        Task<IEnumerable<Course>> GetStudentCourses(int studentId);
    }
}
