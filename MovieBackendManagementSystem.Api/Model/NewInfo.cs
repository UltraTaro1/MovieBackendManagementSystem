using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBackendManagementSystem.Api.Model
{
    public class NewInfo
    {
          public int NewInfoId { get; set; }
          public string? NewTitle { get; set; }//新闻标题 
          public string? NewType { get; set; }//新闻种类
          public string? NewContent { get; set; }//新闻内容
          public string? Summary { get; set; }//新闻摘要
          public required string NewAuthor { get; set; }//新闻作者
          public required DateTime PublishDatetime { get; set; }//发布时间
          public string? MainImageUrl { get; set; } // 主图URL

    }
}