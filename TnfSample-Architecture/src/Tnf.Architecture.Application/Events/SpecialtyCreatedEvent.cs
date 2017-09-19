using Tnf.App.Bus.Client;

namespace Tnf.Architecture.Application.Events
{
    /// <summary>
    /// Event message when Specialty has been created
    /// </summary>
    public class SpecialtyCreatedEvent : Message
    {
        // Specialty Id
        public int SpecialtyId { get; set; }
    }
}