using System.Threading;
using System.Threading.Tasks;
using System.Linq;

using SGDP.Domain.Interfaces.Repositories;
using Tnf.Sgdp;
using SGDP.Dto.Customer;
using System;
using SGDP.Domain.Entities;

namespace SGDP.Application
{
    public class SampleSgdpMaskService : SgdpMaskService
    {
        private readonly ICustomerRepository _customerRepository;

        public SampleSgdpMaskService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public override async Task ExecuteAsync(SgdpMaskCommandContext context, CancellationToken cancellationToken = default)
        {
            var cpf = context.Identifiers["CPF"];
            var email = context.Identifiers["EMAIL"];
            var rg = context.Identifiers["RG"];

            if(!cpf.IsNullOrEmpty() || !cpf.IsNullOrEmpty() || !rg.IsNullOrEmpty())
            {
                var data = await _customerRepository.GetCustomerAsync(
                    customer => (cpf.IsNullOrEmpty() || customer.Cpf.Equals(cpf))
                        && (email.IsNullOrEmpty() || customer.Email.Equals(email))
                        && (rg.IsNullOrEmpty() || customer.Rg.Equals(rg))
                    );

                if (data != null)
                {
                    Mask(data, context);

                    if (!context.ToVerify)
                    {
                        await _customerRepository.UpdateCustomerAsync(data);
                    }
                }
            }

        }
    }
}
