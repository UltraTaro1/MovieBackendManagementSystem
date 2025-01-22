using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;
using MovieBackendManagementSystem.Api.Common;
using MovieBackendManagementSystem.Api.DbContexts;
using MovieBackendManagementSystem.Api.Model;
using MovieBackendManagementSystem.Api.Services.IServices;

namespace MovieBackendManagementSystem.Api.Services
{
    public class UploadService : IUploadService
    {
        private readonly UserDbContext _dbContext;
        private const string TempFolder = @"Temp";

        public UploadService(UserDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        /// <summary>
        /// 上传影片
        /// </summary>
        /// <param name="form"></param>
        /// <param name="videoFile"></param>
        /// <param name="coverImage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IActionResult> UploadFilm(IFormCollection form, IFormFile videoFile, IFormFile coverImage, CancellationToken cancellationToken)
        {
            if (videoFile == null || coverImage == null)
            {
                return ResponseHelper.BadRequest("视频文件或封面图片未上传");
            }

            if (!videoFile.ContentType.StartsWith("video") || !coverImage.ContentType.StartsWith("image"))
            {
                return ResponseHelper.BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "文件类型错误",
                    Detail = "视频应为视频文件，封面应为图片文件"
                });
            }

            string videoFilePath = GenerateTemporaryFilePath(videoFile);
            string coverImagePath = GenerateTemporaryFilePath(coverImage);

            await SaveFileToTemporaryLocation(videoFile, videoFilePath, cancellationToken);
            await SaveFileToTemporaryLocation(coverImage, coverImagePath, cancellationToken);

            string filmType = form["filmType"];
            string filmTitle = form["filmTitle"];
            string filmIntroduction = form["filmIntroduction"];
            string filmDirector = form["filmDirector"];
            string filmPerformer = form["filmPerformer"];
            DateTime releaseTime = DateTime.Parse(form["releaseTime"]);

            var film = new Film
            {
                FilmType = filmType,
                FilmTitle = filmTitle,
                FilmIntroduction = filmIntroduction,
                FilmDirector = filmDirector,
                FilmPerformer = filmPerformer,
                ReleaseTime = releaseTime,
                VideoPath = videoFilePath,
                CoverImagePath = coverImagePath
            };

            _dbContext.Films.Add(film);
            await _dbContext.SaveChangesAsync();

            return ResponseHelper.Ok("上传成功");
        }
        /// <summary>
        /// 上传新闻
        /// </summary>
        /// <param name="form"></param>
        /// <param name="mainImage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IActionResult> UploadNews(IFormCollection newsInfo, IFormFile mainImage, CancellationToken cancellationToken)
        {
            if (mainImage == null)
            {
                return ResponseHelper.BadRequest("主图文件未上传");
            }

            if (!mainImage.ContentType.StartsWith("image"))
            {
                return ResponseHelper.BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "文件类型错误",
                    Detail = "主图应为图片文件"
                });
            }

            string mainImagePath = GenerateTemporaryFilePath(mainImage);

            await SaveFileToTemporaryLocation(mainImage, mainImagePath, cancellationToken);

            string newTitle = newsInfo["newTitle"];
            string newType = newsInfo["newType"];
            string newContent = newsInfo["newContent"];
            string summary = newsInfo["summary"];
            string newAuthor = newsInfo["newAuthor"];
            DateTime publishDatetime = DateTime.Parse(newsInfo["publishDatetime"]);

            var news = new NewInfo
            {
                NewTitle = newTitle,
                NewType = newType,
                NewContent = newContent,
                Summary = summary,
                NewAuthor = newAuthor,
                PublishDatetime = publishDatetime,
                MainImageUrl = mainImagePath
            };

            _dbContext.NewsInfos.Add(news);
            await _dbContext.SaveChangesAsync();

            return ResponseHelper.Ok("上传成功");
        }

        private string GenerateTemporaryFilePath(IFormFile file)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), TempFolder, $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}");
        }

        private async Task SaveFileToTemporaryLocation(IFormFile file, string filePath, CancellationToken cancellationToken)
        {
            try
            {
                if (!Directory.Exists(TempFolder))
                {
                    Directory.CreateDirectory(TempFolder); // 确保临时文件夹存在
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                // 请求被取消
                Console.WriteLine("File upload cancelled.");
                throw new BadRequestException("文件上传被取消");
            }
            catch (Exception ex)
            {
                // 记录日志或返回错误信息
                Console.WriteLine($"Error saving file: {ex.Message}");
                throw; // 或者返回一个自定义的错误响应
            }
        }
    }
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}