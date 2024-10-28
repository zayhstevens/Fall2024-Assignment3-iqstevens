using System.ComponentModel.DataAnnotations;

namespace Fall2024_Assignment3_iqstevens.Models;

public class Movie
{
    [Key]
    public int Id {get; set;}
    [Required]
    public required string Name {get; set;}
    public string? IMDB {get; set;}
    [Required]
    public required string Genre { get; set; }
    public int ReleaseYear {get; set;}
    

    public byte[]? Poster { get; set; }

}