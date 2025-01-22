using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieBackendManagementSystem.Api.Model;
using MovieBackendManagementSystem.Api.Services.IServices;

namespace MovieBackendManagementSystem.Api.Controlllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IUploadService _uploadService;
        private readonly IGetNewsServices _getNewsServices;
        private readonly IDeleteNewService _deleteNewService;
        private readonly IUpdateNewService _updateNewService;

        public NewsController(
            IUploadService uploadService,
            IGetNewsServices getNewsServices,
            IDeleteNewService deleteNewService,
            IUpdateNewService updateNewService)
        {
            this._uploadService = uploadService;
            this._getNewsServices = getNewsServices;
            this._deleteNewService = deleteNewService;
            this._updateNewService = updateNewService;
        }
        [HttpGet("GetNewsById/{newsId}")]
        public NewInfo? GetNewsById(int newsId)
        {
            return _getNewsServices.GetNewsById(newsId);
        }

        [HttpGet("GetAllNews")]
        public List<NewInfo>? GetAllNews()
        {
            return _getNewsServices.GetAllNews();
        }

        [HttpGet("GetNewsByType/{type}")]
        public List<NewInfo>? GetNewsByType(string type)
        {
            return _getNewsServices.GetNewsByType(type);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> AddNews(IFormCollection newsInfo,IFormFile imageFile)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var cancellationToken = HttpContext.RequestAborted; // 获取请求的取消标记
            return await _uploadService.UploadNews(newsInfo, imageFile,cancellationToken);
        }

        [HttpPut("UpdateNew")]
        public string UpdateNews(NewInfo newsInfo)
        {
            return _updateNewService.UpdateNew(newsInfo);
        }

        [HttpDelete("DeleteNewById/{newsId}")]
        public string DeleteNews(int newsId)
        {
            return _deleteNewService.DeleteNewById(newsId);
        }

    }
}