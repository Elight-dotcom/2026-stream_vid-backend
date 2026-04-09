namespace StreamVid.Models;

public class Movie : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public int TmdbId { get; set; }

    public ICollection<History> Histories { get; set; } = new List<History>();
}
