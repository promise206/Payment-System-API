using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace PaymentSystemAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class CustomerController : ControllerBase
    {
        
    }
}