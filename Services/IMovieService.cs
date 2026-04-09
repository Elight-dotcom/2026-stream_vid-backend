using Microsoft.AspNetCore.Mvc;
using StreamVid.DTOs;

namespace StreamVid.Services;

public interface IMovieService
{
    Task<GetMovieDto> AddMovie(NewMovieDto movie);
    Task<List<GetMovieDto>> GetAllMovies();
    Task<GetMovieDto> GetMovieById(int id);
    Task<GetMovieDto> UpdateMovie(int id, NewMovieDto updatedMovie);
    Task<bool> DeleteMovie(int id);
    Task<IActionResult> StreamVideo(int id);
}