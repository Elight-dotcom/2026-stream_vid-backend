using System.ComponentModel.DataAnnotations;

namespace StreamVid.DTOs;

public class NewMovieDto
{
    [Required(ErrorMessage = "Nama film harus diisi.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Path file harus diisi.")]
    [RegularExpression(@"^[^\\]+$", ErrorMessage = "Gunakan format slash biasa (/) bukan backslash (\\).")]
    public string FilePath { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cari ID di TMDB dan masukkan di sini.")]
    public int TmdbId { get; set; }
}