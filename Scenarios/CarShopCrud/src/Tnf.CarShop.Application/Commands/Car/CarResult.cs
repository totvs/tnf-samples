using Tnf.CarShop.Domain.Dtos;

namespace Tnf.CarShop.Application.Commands.Car;
public class CarResult: AdminResult
{
    public CarDto CarDto { get; set; }
}
