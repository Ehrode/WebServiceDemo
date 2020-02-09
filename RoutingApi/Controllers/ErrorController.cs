using System.Web.Http;

namespace WebServiceDemo.Api.Controllers
{
    public class ErrorController : ApiController
    {
        [HttpGet]
        public IHttpActionResult RouteNotFound()
        {
            return NotFound();
        }
    }
}
