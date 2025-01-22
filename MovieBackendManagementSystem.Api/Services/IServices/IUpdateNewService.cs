using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieBackendManagementSystem.Api.Model;

namespace MovieBackendManagementSystem.Api.Services.IServices
{
    public interface IUpdateNewService
    {
        string UpdateNew(NewInfo newInfo);
    }
}