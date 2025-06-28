using System.ComponentModel.DataAnnotations;

namespace RetroSum.Data.Models;

public class RetroItem : BaseEntity
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [MaxLength(250)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    [MaxLength(1000)]
    public string? Description { get; set; } = string.Empty;
}