namespace PersonApi.Models;

public class PersonDTO
{
    public Guid Id { get; set; }
    public string Identification { get; set; } = "";
    public string FullName { get; set; } = "";
    public DateTime BirthDate { get; set; }
    public int Age { get; set; }

}
