﻿using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Host.Commands.Car.Get;

public class GetCarCommand
{
    public Guid? CarId { get; set; }
}

public class GetCarResult
{
    public GetCarResult( CarDto car)
    {
        Car = car;
    }
    
    public CarDto Car { get; set; }
}