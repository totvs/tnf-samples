namespace TnfZero.Application.Commands.UpdateDog;

/// <summary>Command to update an existing Dog.</summary>
public record UpdateDogCommand(Guid Id, string Name);