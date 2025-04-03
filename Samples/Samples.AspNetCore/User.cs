using Rum.Data.Annotations;

namespace Samples.AspNetCore;

public class User
{
    [Default("test")]
    [Required]
    public string? Username { get; set; }
}