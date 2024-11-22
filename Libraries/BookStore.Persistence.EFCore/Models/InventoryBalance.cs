using System.ComponentModel.DataAnnotations;

namespace BookStore.Persistence.EFCore.Models;

public class InventoryBalance
{
    // !: NOTE: No key attributes are necessary here, use the Fluent API in BookStoreDbContext.OnModelCreating().
    [Required]
    public int StoreId { get; set; }

    [Required]
    [StringLength(17)]
    public string Isbn13 { get; set; } = string.Empty;

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be 0 or greater.")]
    public int Quantity { get; set; }

    public Store Store { get; set; } = null!;
    public Book Book { get; set; } = null!;
}