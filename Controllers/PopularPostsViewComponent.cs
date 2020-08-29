using LienPhatERP.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LienPhatERP.Controllers
{
    public class PopularPostsViewComponent: ViewComponent
    {
        private readonly ICommonService _commonService;

        public PopularPostsViewComponent(ICommonService commonService)
        {
            _commonService = commonService;
        }

       
    }
}
