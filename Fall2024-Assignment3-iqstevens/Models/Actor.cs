using System.ComponentModel.DataAnnotations;

namespace Fall2024_Assignment3_iqstevens.Models;

public class Actor
{
    [Key]
    public int Id {get; set;}
    [Required]
    public required string Name {get; set;}
    [Required]
    public required string Gender {get; set;}
    public int Age {get; set;}
    public string? IMDB {get; set;}
    public byte[]? Photo { get; set; }

}