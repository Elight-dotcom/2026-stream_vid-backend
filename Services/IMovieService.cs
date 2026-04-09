using StreamVid.DTOs;

namespace StreamVid.Services;

public interface IMovieService
{
    Task<GetMovieDto> AddMovie(NewMovieDto movie);
    Task<List<GetMovieDto>> GetAllMovies();
    Task<GetMovieDto> GetMovieById(int id);
    Task<bool> DeleteMovie(int id);
}