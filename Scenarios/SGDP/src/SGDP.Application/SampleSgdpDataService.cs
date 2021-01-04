using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tnf.Runtime.Session;
using Tnf.Sgdp;
using SGDP.Domain.Interfaces.Repositories;
using System;

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

            var cpf = context.Identifiers["CPF"];
            var email = context.Identifiers["EMAIL"];
            var rg = context.Identifiers["RG"];

            if (!cpf.IsNullOrEmpty() || !cpf.IsNullOrEmpty() || !rg.IsNullOrEmpty())
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
