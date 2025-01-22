using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieBackendManagementSystem.Api.Common;
using MovieBackendManagementSystem.Api.DbContexts;
using MovieBackendManagementSystem.Api.Model;
using MovieBackendManagementSystem.Api.Services.IServices;

namespace MovieBackendManagementSystem.Api.Services
{
    public class UserService : IUserService
    {
        /// <summary>
        /// 通过用户名和密码进行用户登录
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User? CheckLogin(string userNo,string password){
            using UserDbContext context = new UserDbContext();
            var user = context.Users.FirstOrDefault(m=>m.UserNo == userNo && m.Password == password.ToMd5());
            context.SaveChanges();
            if(user != null)
            {
                return user;
            }
            return null;
        }
        /// <summary>
        /// 通过插入用户信息进行用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string Insert(User user)
        {
            // 检查user参数是否为空
            if (user == null)
            {
                return "错误：用户信息不能为空";
            }

            using UserDbContext context = new();
            user.Password = user.Password.ToMd5();
            var result = context.Users.Add(user);
            context.SaveChanges();

            if (result != null)
            {
                return "注册成功";
            }

            return "注册失败";
        }
    }
}