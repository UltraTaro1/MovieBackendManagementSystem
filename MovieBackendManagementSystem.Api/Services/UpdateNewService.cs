using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieBackendManagementSystem.Api.DbContexts;
using MovieBackendManagementSystem.Api.Model;
using MovieBackendManagementSystem.Api.Services.IServices;

namespace MovieBackendManagementSystem.Api.Services
{
    public class UpdateNewService : IUpdateNewService
    {
        private readonly IGetNewsServices _getNewsServices;
        private readonly UserDbContext _dbContext;

        public UpdateNewService(IGetNewsServices getNewsServices,UserDbContext dbContext)
        {
            this._getNewsServices = getNewsServices;
            this._dbContext = dbContext;
        }
        public string UpdateNew(NewInfo newInfo)
        {
            NewInfo news = _getNewsServices.GetNewsById(newInfo.NewInfoId);
            if(news != null)
            {
                if (!string.IsNullOrEmpty(newInfo.NewTitle)) news.NewTitle = newInfo.NewTitle;
                if (!string.IsNullOrEmpty(newInfo.NewType)) news.NewType = newInfo.NewType;
                if (!string.IsNullOrEmpty(newInfo.Summary)) news.Summary = newInfo.Summary;
                if (!string.IsNullOrEmpty(newInfo.NewContent)) news.NewContent = newInfo.NewContent;
                if (!string.IsNullOrEmpty(newInfo.NewAuthor)) news.NewAuthor = newInfo.NewAuthor;
                if (newInfo.PublishDatetime != default) news.PublishDatetime = newInfo.PublishDatetime;

                _dbContext.NewsInfos.Update(news);
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