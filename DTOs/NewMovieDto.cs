using System.ComponentModel.DataAnnotations;

namespace StreamVid.DTOs;

public class NewMovieDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string FilePath { get; set; } = string.Empty;

    [Required]
    public int TmdbId { get; set; }
}