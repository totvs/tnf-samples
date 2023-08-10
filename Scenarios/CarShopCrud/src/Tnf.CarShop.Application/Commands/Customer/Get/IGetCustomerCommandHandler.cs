using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer.Get;

public interface IGetCustomerCommandHandler: ICommandHandler<GetCustomerCommand, GetCustomerResult>
{
}
