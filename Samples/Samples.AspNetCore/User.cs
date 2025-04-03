using Rum.Data.Annotations;

namespace Samples.AspNetCore;

public class User
{
    [Required]
    [Default("test")]
    [Enum([1, "test"])]
    public string? Username { get; set; }
}