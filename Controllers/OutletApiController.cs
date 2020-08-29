using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Contants;
using LienPhatERP.Helper;
using LienPhatERP.Services;
using LienPhatERP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LienPhatERP.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OutletApiController : Controller
    {
        private readonly IOutletStoreService _outletStoreService;
        private readonly ILogger _logger;
        public OutletApiController(IOutletStoreService outletStoreService, ILogger<OutletApiController> logger)
        {
            _outletStoreService= outletStoreService;
            _logger= logger;
        }
        // GET: api/<controller>
        [HttpGet("GetAddressOutlet")]
        public IActionResult GetAddressOutlet()
        {
            var rs = new HttpContentResult<dynamic>
            {
                Result = false,
                StatusCode = "404"
            };
            try
            {
                var outlet = _outletStoreService.GetAllDrugStoreNotLocation();
                if (outlet.Count>0)
                {
                    rs.Data = outlet.Select(x=> new MOutletStoreApiModel() { Id = x.Id, Address = $"{x.DrugStoreAddress} {x.Ward} {x.District} {x.Province}" }).ToList();
                    rs.Result = true;
                    rs.TotalItem = 1;
                    rs.Message = "Thành Công";
                    rs.StatusCode = "200";

                    return Ok(rs);
                }
                rs.Result = true;
                rs.StatusCode = "200";
                rs.Message = ResultStatus.Success;
                return Ok(rs);

            }
            catch (Exception ex)
            {
                rs.SysMessage = ex.ToString();
                rs.Message = ResultStatus.Fail;
                return Ok(rs);
            }
        }
        [HttpPost("UpdateLocationOutlet")]
        public IActionResult UpdateLocationOutlet(string model)
        {
            var rs = new HttpContentResult<dynamic>
            {
                Result = false,
                StatusCode = "404"
            };
            try
            {
                _outletStoreService.UpdateLocationOutlet(model);
                rs.Result = true;
                rs.StatusCode = "200";
                rs.Message = ResultStatus.Success;
                return Ok(rs);

            }
            catch (Exception ex)
            {
                rs.SysMessage = ex.ToString();
                rs.Message = ResultStatus.Fail;
                return Ok(rs);
            }
        }
    }
}
