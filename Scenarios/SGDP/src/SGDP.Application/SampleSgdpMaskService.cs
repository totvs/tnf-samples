using System.Threading;
using System.Threading.Tasks;
using System;
using SGDP.Domain.Interfaces.Repositories;
using Tnf.Sgdp;

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
            var hasCpf = context.Identifiers.TryGetValue("CPF", out var cpf);
            var hasEmail = context.Identifiers.TryGetValue("EMAIL", out var email);
            var hasRg = context.Identifiers.TryGetValue("RG", out var rg);

            if (hasCpf || hasEmail || hasRg)
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
