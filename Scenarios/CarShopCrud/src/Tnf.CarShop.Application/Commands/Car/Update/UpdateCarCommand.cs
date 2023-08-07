using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Car.Update;

public class UpdateCarCommand
{
   public CarDto Car { get; set; }
}

public class UpdateCarResult
{
   public UpdateCarResult( CarDto car)
   {
      Car = car;
   }
    
   public CarDto Car { get; set; }
}