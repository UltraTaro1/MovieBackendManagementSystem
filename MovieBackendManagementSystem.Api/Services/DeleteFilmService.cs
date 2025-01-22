using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieBackendManagementSystem.Api.DbContexts;
using MovieBackendManagementSystem.Api.Services.IServices;

namespace MovieBackendManagementSystem.Api.Services
{
    public class DeleteFilmService : IDeleteFilmService
    {
        private readonly UserDbContext _dbContext;
        private readonly ISearchFilmService _searchFilmService;

        public DeleteFilmService(UserDbContext dbContext,ISearchFilmService searchFilmService)
        {
            this._dbContext = dbContext;
            this._searchFilmService = searchFilmService;
        }
        public string DeleteFilmById(int filmId)
        {
            var film = _searchFilmService.GetFilmById(filmId);
            if (film == null)
            {
                return "Film not found.";
            }
            try
            {
                //检查封面图片文件是否存在并删除
                if (!string.IsNullOrEmpty(film.CoverImagePath) && File.Exists(film.CoverImagePath))
                {
                    File.Delete(film.CoverImagePath);
                }
                // 检查视频文件是否存在并删除
                if (!string.IsNullOrEmpty(film.VideoPath) && File.Exists(film.VideoPath))
                {
                    File.Delete(film.VideoPath);
                }

                // 从数据库上下文中删除电影
                _dbContext.Films.Remove(film);

                // Save the changes to the database
                _dbContext.SaveChanges();

                return "电影删除成功";
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes (in a real-world scenario)
                Console.WriteLine($"Error deleting film: {ex.Message}");
                return "电影删除失败";
            }
        }
    }
}