using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieBackendManagementSystem.Api.DbContexts;
using MovieBackendManagementSystem.Api.Model;
using MovieBackendManagementSystem.Api.Services.IServices;

namespace MovieBackendManagementSystem.Api.Controlllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmController : ControllerBase
    {
        private readonly UserDbContext _dbContext;
        private readonly IUploadService _uploadService;
        private readonly ISearchFilmService _searchFilmService;
        private readonly IDeleteFilmService _deleteFilm;
        private readonly IGetFilmTypeService _getFilmTypeService;

        public FilmController(
            UserDbContext dbContext,
            IUploadService uploadService,
            ISearchFilmService searchFilmService,
            IDeleteFilmService deleteFilm,
            IGetFilmTypeService  getFilmTypeService
)
        {
            _dbContext = dbContext;
            this._uploadService = uploadService;
            this._searchFilmService = searchFilmService;
            this._deleteFilm = deleteFilm;
            this._getFilmTypeService = getFilmTypeService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFilm(IFormCollection form, IFormFile videoFile, IFormFile coverImage)
        {
             var cancellationToken = HttpContext.RequestAborted; // 获取请求的取消标记
            return await _uploadService.UploadFilm(form,videoFile,coverImage,cancellationToken);
        }

        [HttpGet("searchFilmById/{filmId}")]
        public Film? GetFilmBuId(int filmId)
        {
            return _searchFilmService.GetFilmById(filmId);
        }

        [HttpGet("searchchFilmByTitle/{filmTitle}")]
        public List<Film> SearchFilmByTitle(string filmTitle)
        {
            return _searchFilmService.SearchFilmByTitle(filmTitle);
        }

        [HttpGet("getAllFilms")]
        public List<Film>? GetAllFilms()
        {
            return _searchFilmService.GetAllFilms();
        }
        
        [HttpPut]
        public string UpdateFilm(Film film)
        {
            return _searchFilmService.UpdateFilm(film);
        }

        [HttpDelete("{filmId}")]
        public string DeleteFilmById(int filmId)
        {
            return _deleteFilm.DeleteFilmById(filmId);
        }

        [HttpGet]
        [Route("GetFilmTypes")]
        public IEnumerable<string> GetNews()
        {
            return _getFilmTypeService.GetAllUniqueFilmTypes();
        }
    }
}