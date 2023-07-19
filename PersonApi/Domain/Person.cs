using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonApi.Domain;

[Table("person")]
[Index(nameof(Identification), IsUnique = true)]
public class Person : BaseEntity
{
    public string Identification { get; set; } = "";
    public string FullName { get; set; } = "";
    [DataType(DataType.Date)]
    [Column(TypeName = "Date")]
    public DateTime BirthDate { get; set; }

}
