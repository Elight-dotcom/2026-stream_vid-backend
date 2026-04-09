namespace StreamVid.Controllers;

using Microsoft.AspNetCore.Mvc;
using StreamVid.Data;
using StreamVid.DTOs;
using StreamVid.Models;
using StreamVid.Services;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetMovieDto>>> GetMovies()
    {
        var movies = await _movieService.GetAllMovies();
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetMovieDto>> GetMovie(int id)
    {
        var movie = await _movieService.GetMovieById(id);
        if (movie == null)
        {
            return NotFound();
        }
        return Ok(movie);
    }

    [HttpPost]
    public async Task<ActionResult> AddMovie(NewMovieDto newMovie)
    {
        try
        {
            var createdMovie = await _movieService.AddMovie(newMovie);
            return CreatedAtAction(nameof(GetMovie), new { id = createdMovie.Id }, createdMovie);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GetMovieDto>> UpdateMovie(int id, NewMovieDto updatedMovie)
    {
        try
        {
            var movie = await _movieService.UpdateMovie(id, updatedMovie);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMovie(int id)
    {
        var success = await _movieService.DeleteMovie(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}