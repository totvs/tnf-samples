﻿namespace Tnf.CarShop.Application.Dtos;

public sealed record CarDto
{
    public CarDto(Guid id, string brand, string model, int year, decimal price)
    {
        Id = id;
        Brand = brand;
        Model = model;
        Year = year;
        Price = price;;
    }

    public CarDto(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
}