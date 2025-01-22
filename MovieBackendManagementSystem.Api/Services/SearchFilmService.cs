using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieBackendManagementSystem.Api.DbContexts;
using MovieBackendManagementSystem.Api.Model;
using MovieBackendManagementSystem.Api.Services.IServices;

namespace MovieBackendManagementSystem.Api.Services
{
    public class SearchFilmService : ISearchFilmService
    {
        private readonly UserDbContext _dbContext;

        public SearchFilmService(UserDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        /// <summary>
        /// 通过电影Id查询电影信息
        /// </summary>
        /// <param name="filmId"></param>
        /// <returns></returns>
        public Film? GetFilmById(int filmId)
        {
            return _dbContext.Films.FirstOrDefault(m=>m.FilmId == filmId);
        }
        /// <summary>
        /// 通过影片名称搜索影片
        /// </summary>
        /// <param name="filmTitle"></param>
        /// <returns></returns>
        public List<Film>? SearchFilmByTitle(string filmTitle)
        {
            if(filmTitle == null)
            {
                return null;
            }
            else
            {
                var filmList = _dbContext.Films.Where(m=>m.FilmTitle == filmTitle).ToList();
                if(filmList == null)
                {
                    return null;
                }
                else
                {
                    return filmList;
                }
            }
        }
        /// <summary>
        /// 得到所有影片
        /// </summary>
        /// <returns></returns>
        public List<Film>? GetAllFilms()
        {
            var filmLists = _dbContext.Films.ToList();
            if(filmLists == null)
            {
                return null;
            }
            else
            {
                return filmLists;
            }
        }

        /// <summary>
        /// 通过影片Id找到影片，再更新此影片信息
        /// </summary>
        /// <param name="filmId"></param>
        /// <param name="film"></param>
        /// <returns></returns>
        public string UpdateFilm(Film film)
        {
            Film GetFilm = GetFilmById(film.FilmId);
            if (GetFilm != null)
            {
                if (!string.IsNullOrEmpty(film.FilmTitle)) GetFilm.FilmTitle = film.FilmTitle;
                if (!string.IsNullOrEmpty(film.FilmType)) GetFilm.FilmType = film.FilmType;
                if (!string.IsNullOrEmpty(film.FilmIntroduction)) GetFilm.FilmIntroduction = film.FilmIntroduction;
                if (!string.IsNullOrEmpty(film.FilmDirector)) GetFilm.FilmDirector = film.FilmDirector;
                if (!string.IsNullOrEmpty(film.FilmPerformer)) GetFilm.FilmPerformer = film.FilmPerformer;
                if (film.ReleaseTime != default) GetFilm.ReleaseTime = film.ReleaseTime;

                _dbContext.Films.Update(GetFilm);
                _dbContext.SaveChanges();
                return "更新成功";
            }
            else
            {
                return "更新失败";
            }
        }
    }
}