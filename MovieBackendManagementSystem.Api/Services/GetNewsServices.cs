using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieBackendManagementSystem.Api.DbContexts;
using MovieBackendManagementSystem.Api.Model;
using MovieBackendManagementSystem.Api.Services.IServices;

namespace MovieBackendManagementSystem.Api.Services
{
    public class GetNewsServices : IGetNewsServices
    {
        private readonly UserDbContext _dbContexts;

        public GetNewsServices(UserDbContext dbContexts)
        {
            this._dbContexts = dbContexts;
        }
        public List<NewInfo> GetAllNews()
        {
            var newsList = _dbContexts.NewsInfos.ToList();
            return newsList;
        }
        public List<NewInfo>? GetNewsByType(string type)
        {
            var newsList = _dbContexts.NewsInfos.Where(x => x.NewType == type).ToList();
            return newsList;
        }
        public NewInfo? GetNewsById(int newsId)
        {
            return _dbContexts.NewsInfos.FirstOrDefault(x => x.NewInfoId == newsId);
        }
    }
}