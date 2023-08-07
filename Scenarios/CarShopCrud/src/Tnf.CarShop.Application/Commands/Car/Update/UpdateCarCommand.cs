public class UpdateCarCommand
{
    public Guid Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
    public Guid? DealerId { get; set; }
    public Guid? OwnerId { get; set; }
}