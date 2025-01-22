using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBackendManagementSystem.Api.Model
{
    public class User
    {
        public int UserId { get; set; }
        [MaxLength(100)]
        public required string UserNo { get; set; }
        [MaxLength(225)]
        public required string Password { get; set; }
        [MaxLength(100)]
        public required string UserName { get; set; }
    }
}