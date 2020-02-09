using System.Web.Http;

namespace WebServiceDemo.Api.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Welcome()
        {
            return Ok("Welcome to Test API");
        }
    }
}
