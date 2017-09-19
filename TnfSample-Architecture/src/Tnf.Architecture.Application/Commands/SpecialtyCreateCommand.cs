using Tnf.App.Bus.Client;

namespace Tnf.Architecture.Application.Commands
{
    /// <summary>
    /// Command to Create a new Specialty
    /// </summary>
    public class SpecialtyCreateCommand : Message
    {
        // Specialty description
        public string Description { get; set; }
    }
}