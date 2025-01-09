using API.Errors;
using Core.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;
        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretText() {
            return "secret stuff";
        }
        public BuggyController(StoreContext context){
            _context = context;
        }
        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest() {
            var thing = _context.Products.Find(50);
            if (thing == null) {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }
        [HttpGet("servererror")]
        public ActionResult GetServerErrorRequest()
        {
            var thing = _context.Products.Find(50);

            var thingToReturn = thing.ToString();

            return BadRequest(new ApiResponse(500));
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }
    }
}