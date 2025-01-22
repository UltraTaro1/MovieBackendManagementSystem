using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieBackendManagementSystem.Api.Model;

namespace MovieBackendManagementSystem.Api.Services.IServices
{
    public interface IUploadService
    {
        Task<IActionResult> UploadFilm(IFormCollection form, IFormFile videoFile, IFormFile coverImage, CancellationToken cancellationToken);
        Task<IActionResult> UploadNews(IFormCollection newsInfo, IFormFile mainImage, CancellationToken cancellationToken);
    }
}