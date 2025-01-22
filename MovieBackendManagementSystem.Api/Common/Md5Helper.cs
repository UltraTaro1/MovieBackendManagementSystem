using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MovieBackendManagementSystem.Api.Common
{
    public static class Md5Helper
    {
        public static string ToMd5(this string str)
        {
            MD5 mD5 = new MD5CryptoServiceProvider();
            byte[] output = mD5.ComputeHash(Encoding.Default.GetBytes(str));
            var md5Str = BitConverter.ToString(output).Replace("-","");
            return md5Str;
        }
    }
}