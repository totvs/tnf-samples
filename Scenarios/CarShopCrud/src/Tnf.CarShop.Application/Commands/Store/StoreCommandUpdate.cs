﻿using System.Runtime.Serialization;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Store;
public class StoreCommandUpdate : ICommand<StoreResult>, ITransactionCommand
{
    [IgnoreDataMember]
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Cnpj { get; set; }
}
