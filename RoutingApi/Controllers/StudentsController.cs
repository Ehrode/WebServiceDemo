using WebServiceDemo.Core.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using RoutingCore;

namespace WebServiceDemo.Api.Controllers
{
    [RoutePrefix("api/Students")]
    public class StudentsController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        private readonly IGenericRepository<Student> _studentsRepository;
        public StudentsController(IGenericRepository<Student> studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IHttpActionResult> GetById(int studentId)
        {
            try
            {
                var student = await _studentsRepository.GetById(studentId).ConfigureAwait(false);

                if (student == null)
                {
                    log.Warn($"Student with id <{studentId}> was requested but not found in database");
                    return NotFound();
                }
                    
                return Ok(student);
            }
            catch (Exception e)
            {
                log.Error($"Something went wrong while getting the students list : {e.Message}", e);
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("ListAll")]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var students = await _studentsRepository.GetAll().ConfigureAwait(false);
                return Ok(students);
            }
            catch (Exception e)
            {
                log.Error($"Something went wrong while getting the students list : {e.Message}", e);
                return InternalServerError(e);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IHttpActionResult> Create([FromBody]Student student)
        {
            try
            {
                await _studentsRepository.Insert(student).ConfigureAwait(false);
                return Created(new Uri(Request.RequestUri, Url.Route("GetById", new { id = student.Id })), student);
            }
            catch (Exception e)
            {
                log.Error($"Something went wrong while trying to insert new client : {e.Message}", e);
                return InternalServerError(e);
            }
        }
    }
}
