using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IMediator Mediator { get; }

        public BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
