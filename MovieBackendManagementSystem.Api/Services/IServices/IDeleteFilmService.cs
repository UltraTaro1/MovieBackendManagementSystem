using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBackendManagementSystem.Api.Services.IServices
{
    public interface IDeleteFilmService
    {
        public string DeleteFilmById(int filmId);
    }
}