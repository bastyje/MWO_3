namespace Lab2.Data.Enitites;

public class Player : IEntityBase<string>
{
    public string Id { get; set; }
    public string CoachId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Country { get; set; }
    public DateTime DateOfBirth { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
}