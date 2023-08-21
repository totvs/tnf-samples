﻿namespace Tnf.CarShop.Application.Commands.Car.Delete;

public class DeleteCarCommand
{
    public DeleteCarCommand(Guid carId)
    {
        CarId = carId;
    }

    public Guid CarId { get; set; }
}

public class DeleteCarResult
{
    public DeleteCarResult(bool success)
    {
        Success = success;
    }

    public bool Success { get; set; }
}