using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieBackendManagementSystem.Api.DbContexts;
using MovieBackendManagementSystem.Api.Services.IServices;

namespace MovieBackendManagementSystem.Api.Services
{
    public class DeleteNewService : IDeleteNewService
    {
        private readonly UserDbContext _dbContext;
        private readonly IGetNewsServices _getNewsServices;

        public DeleteNewService(UserDbContext dbContext,IGetNewsServices getNewsServices)
        {
            this._dbContext = dbContext;
            this._getNewsServices = getNewsServices;
        }
        public string DeleteNewById(int newsId)
        {
            var news = _getNewsServices.GetNewsById(newsId);
            if(news == null)
            {
                return "新闻不存在";
            }
            try
            {
                //检查新闻图片文件是否存在并删除
                //检查封面图片文件是否存在并删除
                if (!string.IsNullOrEmpty(news.MainImageUrl) && File.Exists(news.MainImageUrl))
                {
                    File.Delete(news.MainImageUrl);
                }
                
                //从数据库上下文中删除新闻
                _dbContext.NewsInfos.Remove(news);
                _dbContext.SaveChanges();

                return "新闻删除成功";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting news: {ex.Message}");
                return "新闻删除失败";
            }
        }
    }
}