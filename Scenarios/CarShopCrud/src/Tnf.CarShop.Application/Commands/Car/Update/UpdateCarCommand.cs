using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.Application.Commands.Car.Update;

public sealed record UpdateCarCommand
{
    //public UpdateCarCommand(Guid id, string brand, string model, int year, decimal price)
    //{
    //    Id = id;
    //    Brand = brand;
    //    Model = model;
    //    Year = year;
    //    Price = price;
    //}

    public Guid Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
}

public class UpdateCarResult
{

    public UpdateCarResult(CarDto car)
    {
        Car = car;
    }

    public CarDto Car { get; set; }
}
