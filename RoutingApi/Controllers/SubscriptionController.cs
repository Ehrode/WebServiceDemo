using WebServiceDemo.Api.Dtos;
using WebServiceDemo.Core.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebServiceDemo.Api.Controllers
{
    [RoutePrefix("api/Subscription")]
    public class SubscriptionController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        [Route("GetCourses")]
        public async Task<IHttpActionResult> GetStudentCourses(int studentId)
        {
            try
            {
                var studentCourses =
                    await _subscriptionService.GetStudentCourses(studentId).ConfigureAwait(false);

                return Ok(studentCourses);
            }
            catch (Exception e)
            {
                log.Error($"Something went wrong when retrieving courses for student with id <{studentId}>", e);
                return InternalServerError(e);
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IHttpActionResult> SubscribeStudentToCourse([FromBody]StudentCoursesDto studentCoursesDto)
        {
            try
            {
                var studentCourse =
                    await _subscriptionService
                    .RegisterStudentToCourse(studentCoursesDto.StudentId, studentCoursesDto.CourseId)
                    .ConfigureAwait(false);

                return Created(
                    new Uri(Request.RequestUri, 
                        Url.Route(null, new { studentCoursesDto.StudentId, studentCoursesDto.CourseId })),
                    studentCourse);
            }
            catch (Exception e)
            {
                log.Error($"Impossible to register student with id <{studentCoursesDto.StudentId}>" +
                    $"for course with id <{studentCoursesDto.CourseId}>", e);
                return InternalServerError(e);
            }
        }
    }
}
