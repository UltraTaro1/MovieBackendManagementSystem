using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieBackendManagementSystem.Api.Model;

namespace MovieBackendManagementSystem.Api.Services.IServices
{
    public interface ISearchFilmService
    {
        public Film? GetFilmById(int filmId);
        List<Film> SearchFilmByTitle(string filmTitle);
        List<Film>? GetAllFilms();
        public string UpdateFilm(Film film);
    }
}