using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieBackendManagementSystem.Api.DbContexts;
using MovieBackendManagementSystem.Api.Services.IServices;

namespace MovieBackendManagementSystem.Api.Services
{
    public class GetFilmTypeService :IGetFilmTypeService
    {
        private readonly UserDbContext _dbContext;

        public GetFilmTypeService(UserDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        /// <summary>
        /// 获取所有去重的电影类型
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllUniqueFilmTypes()
        {
            // 使用 Distinct 方法对类型进行去重
            var uniqueFilmTypes = _dbContext.Films.Select(film => film.FilmType).Distinct();

            return uniqueFilmTypes;
        }
    }
}