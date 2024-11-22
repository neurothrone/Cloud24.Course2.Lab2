using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Persistence.EFCore.Models;

public class Book
{
    [Key]
    [Required(ErrorMessage = "ISBN must be either 13 digits or in the format XXX-X-XXX-XXXXX.")]
    [StringLength(17)]
    [RegularExpression(
        @"^(?:\d{13}|\d{3}-\d-\d{3}-\d{5})$",
        ErrorMessage = "ISBN must be either 13 digits or in the format XXX-X-XXX-XXXXX.")]
    public string Isbn13 { get; set; } = string.Empty;

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Language is required.")]
    [StringLength(25)]
    public string Language { get; set; } = string.Empty;

    [Column(TypeName = "decimal(6, 2)")]
    [Range(
        typeof(decimal), "0", "9999",
        ErrorMessage = "Price must be between 0 and 9999.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Pages is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "Pages must be 0 or greater.")]
    public int Pages { get; set; }

    [Required(ErrorMessage = "Release Date is required.")]
    [DataType(DataType.Date, ErrorMessage = "Release date must be a valid date.")]
    public DateTime ReleaseDate { get; set; }

    public int? AuthorId { get; set; }

    [Required(ErrorMessage = "Author is required.")]
    public Author? Author { get; set; }

    // TODO: Can be removed if this Navigation property is never used
    // Possible use case: From Books, list quantities in all stores. Button to navigate to each Store through
    // InventoryBalance Store navigation property.
    public ICollection<InventoryBalance> InventoryBalances { get; set; } = [];
}