using System;
using System.Collections.Generic;
using System.Text;

namespace Tnf.Architecture.Queue.Professional.Events
{
    public class LimiteDeCreditoAlteradoEvent : Message
    {
        public Guid ClientGuid { get; set; }
        public double Valor { get; set; }
    }
}
