using Microsoft.EntityFrameworkCore;
using StreamVid.Data;
using StreamVid.DTOs;
using StreamVid.Models;

namespace StreamVid.Services;

public class MovieService : IMovieService
{
    private readonly AppDbContext _context;

    public MovieService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GetMovieDto> AddMovie(NewMovieDto movie)
    {
        var notAvaible = await _context.Movies.AnyAsync(m => m.TmdbId == movie.TmdbId);
        if (notAvaible) throw new InvalidOperationException("Movie with the same TMDB ID already exists");

        var newMovie = new Movie
        {
            Name = movie.Name,
            FilePath = movie.FilePath,
            TmdbId = movie.TmdbId
        };

        _context.Movies.Add(newMovie);
        await _context.SaveChangesAsync();

        return new GetMovieDto
        {
            Id = newMovie.Id,
            Name = newMovie.Name,
            FilePath = newMovie.FilePath,
            TmdbId = newMovie.TmdbId
        };
    }

    public async Task<List<GetMovieDto>> GetAllMovies()
    {
        var movies = await _context.Movies.ToListAsync();

        return movies.Select(movie => new GetMovieDto
        {
            Id = movie.Id,
            Name = movie.Name,
            FilePath = movie.FilePath,
            TmdbId = movie.TmdbId
        }).ToList();
    }

    public async Task<GetMovieDto> GetMovieById(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null) throw new KeyNotFoundException("Movie not found");

        return new GetMovieDto
        {
            Id = movie.Id,
            Name = movie.Name,
            FilePath = movie.FilePath,
            TmdbId = movie.TmdbId
        };
    }

    public async Task<bool> DeleteMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null) return false;

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return true;
    }
}