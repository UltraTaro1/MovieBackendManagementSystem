using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBackendManagementSystem.Api.Model
{
    public class Film
    {
        public int FilmId { get; set; }
        public string? FilmType { get; set; }//影片类型
        public string? FilmTitle { get; set; }//影片标题
        public string? FilmIntroduction { get; set; }//影片简介
        public string? FilmDirector { get; set; }//影片导演
        public string? FilmPerformer { get; set; }//影片主演
        public DateTime ReleaseTime { get; set; }//上映时间
        public string? VideoPath { get; set; }//视频地址
        public string? CoverImagePath { get; set; }//封面图片地址
    }
}