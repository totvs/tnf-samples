using System.Threading;
using System.Threading.Tasks;
using System;
using Tnf.Runtime.Session;
using Tnf.Sgdp;
using SGDP.Domain.Interfaces.Repositories;


namespace SGDP.Application
{
    public class SampleSgdpDataService : ISgdpDataService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITnfSession _session;

        public SampleSgdpDataService(ICustomerRepository customerRepository, ITnfSession session)
        {
            _customerRepository = customerRepository;
            _session = session;
        }

        public async Task ExecuteAsync(SgdpDataCommandContext context, CancellationToken cancellationToken = default)
        {
            var tenantId = _session.TenantId;

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
                    context.AddData(data);
                }
            }
        }
    }
}
