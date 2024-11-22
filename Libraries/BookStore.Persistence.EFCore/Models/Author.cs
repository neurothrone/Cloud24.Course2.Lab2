using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookStore.Persistence.EFCore.Validation;

namespace BookStore.Persistence.EFCore.Models;

public class Author
{
    public int Id { get; set; }

    [DisplayName("First Name")]
    [Required(ErrorMessage = "First Name is required.")]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [DisplayName("Last Name")]
    [Required(ErrorMessage = "Last Name is required.")]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [DisplayName("Birth Date")]
    [Required(ErrorMessage = "Birth Date is required.")]
    [DataType(DataType.Date)]
    [DateBeforeToday]
    public DateTime BirthDate { get; set; }

    public ICollection<Book> Books { get; set; } = [];

    public ICollection<InventoryBalance> InventoryBalances { get; set; } = [];

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
}