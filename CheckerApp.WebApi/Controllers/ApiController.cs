using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace CheckerApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => 
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        internal string UserId => !HttpContext.User.Identity.IsAuthenticated ?
            "NoName" : HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
