using System.ComponentModel.DataAnnotations;

namespace MinimalBookApi;

public class Book
{
    public int Id { get; set; }
    [MaxLength(255)]
    public required string Title { get; set; }
    [MaxLength(255)]
    public required string Author { get; set; }
}