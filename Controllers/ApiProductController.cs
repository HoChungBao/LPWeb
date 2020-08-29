using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LienPhatERP.Contants;
using LienPhatERP.Helper;
using LienPhatERP.Services;
using LienPhatERP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LienPhatERP.Models ;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LienPhatERP.Controllers
{
    [Route("api/Product")]
    public class ApiProductController : Controller
    {
        private readonly ICommonService _commonService;

        public ApiProductController(ICommonService commonService)
        {
            _commonService = commonService;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("GetProductById/{id}")]
        public IActionResult GetProductById(long id)
        {
            var rs = new HttpContentResult<dynamic>
            {
                Result = false,
                StatusCode = "404"
            };
            try
            {
                var i = new ProductApiModel();
                i.prodcutModel = _commonService.GeteEProductById(id);
                i.arrayProduct = _commonService.GetAllEProduct()
                    .Where(x => x.MetaTitle.Contains(i.prodcutModel.MetaTitle)).Select(m =>
                        new RelatedProduct() {Id = m.Id, Image = m.ThumbnailImage, Title = m.MetaTitle}).Take(4).ToList();
                var data =i;

                if (data.prodcutModel !=null)
                {
                    rs.Data = data;
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
        [HttpGet("GetProductBySlug/slug")]
        public IActionResult GetProductBySlug(string slug)
        {
            var rs = new HttpContentResult<dynamic>
            {
                Result = false,
                StatusCode = "404"
            };
            try
            {
                var data = _commonService.GetProductbySlug(slug);

                if (data != null)
                {
                    rs.Data = data;
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
        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
