using System.ComponentModel.DataAnnotations;

namespace BookStore.Persistence.EFCore.Models;

public class Store
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(150)]
    public string Address { get; set; } = string.Empty;

    public ICollection<InventoryBalance> InventoryBalances { get; set; } = [];
}