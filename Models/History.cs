using System;

namespace StreamVid.Models;

public class History : BaseEntity
{
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
    public DateTime WatchedAt { get; set; }
}
