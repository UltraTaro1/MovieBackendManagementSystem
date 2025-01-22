using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MovieBackendManagementSystem.Api.Common
{
    public static class ResponseHelper
    {
        public static BadRequestObjectResult BadRequest(object error)
        {
            return new BadRequestObjectResult(error);
        }

        public static OkObjectResult Ok(object content)
        {
            return new OkObjectResult(content);
        }
    }
}