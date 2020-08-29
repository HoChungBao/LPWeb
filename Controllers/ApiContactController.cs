using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Helper;
using LienPhatERP.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LienPhatERP.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ApiContactController : Controller
    {
        [HttpPost("ContractUs")]
        public IActionResult ContractUs([FromForm] ContactApiModel model)
        {
            var rs = new HttpContentResult<dynamic>
            {
                Result = false,
                StatusCode = "404"
            };
            if (ModelState.IsValid)
            {
                rs.Message= string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return Ok(rs);
            }

            return Ok(rs);
        }
    }
}
