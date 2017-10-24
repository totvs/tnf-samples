using System;
using System.Linq;
using Tnf.App.Application.Services;
using Tnf.App.AutoMapper;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Common;
using Tnf.Architecture.Common.Enumerables;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.EntityFrameworkCore.ReadInterfaces;

namespace Tnf.Architecture.Application.Services
{
    public class ProfessionalAppService : AppApplicationService, IProfessionalAppService
    {
        private readonly IProfessionalService _service;
        private readonly IProfessionalReadRepository _readRepository;

        public ProfessionalAppService(IProfessionalService service, IProfessionalReadRepository readRepository)
        {
            _service = service;
            _readRepository = readRepository;
        }

        public IListDto<ProfessionalDto, ComposeKey<Guid, decimal>> GetAllProfessionals(GetAllProfessionalsDto request)
            => _readRepository.GetAllProfessionals(request);

        public ProfessionalDto GetProfessional(IRequestDto<ComposeKey<Guid, decimal>> keys)
        {
            ValidateRequestDto<IRequestDto<ComposeKey<Guid, decimal>>, ComposeKey<Guid, decimal>>(keys);

            if (Notification.HasNotification())
                return ProfessionalDto.NullInstance;

            var professionalId = keys.GetId().SecundaryKey;
            var code = keys.GetId().PrimaryKey;

            if (professionalId <= 0)
                RaiseNotification(nameof(professionalId));

            if (code == Guid.Empty)
                RaiseNotification(nameof(code));

            if (Notification.HasNotification())
                return ProfessionalDto.NullInstance;

            var entity = _service.GetProfessional(keys);
            var dto = entity.MapTo<ProfessionalDto>();

            dto.RemoveExpandable(keys);

            return dto;
        }

        public ProfessionalDto CreateProfessional(ProfessionalDto professional)
        {
            ValidateDto<ProfessionalDto, ComposeKey<Guid, decimal>>(professional);

            if (Notification.HasNotification())
                return ProfessionalDto.NullInstance;

            var professionalBuilder = new ProfessionalBuilder(Notification)
                .WithProfessionalId(professional.ProfessionalId)
                .WithCode(professional.Code)
                .WithName(professional.Name)
                .WithPhone(professional.Phone)
                .WithEmail(professional.Email)
                .WithAddress(professional.Address)
                .WithSpecialties(
                    professional
                    .Specialties
                    .Select(s =>
                        new SpecialtyBuilder(Notification)
                            .WithId(s.Id)
                            .WithDescription(s.Description)
                            .Build())
                            .ToList());

            var id = _service.CreateProfessional(professionalBuilder);

            professional.ProfessionalId = id.SecundaryKey;
            professional.Code = id.PrimaryKey;

            return professional;
        }

        public ProfessionalDto UpdateProfessional(ComposeKey<Guid, decimal> keys, ProfessionalDto professional)
        {
            ValidateDtoAndId(professional, keys);

            if (Notification.HasNotification())
                return ProfessionalDto.NullInstance;

            var professionalId = keys.SecundaryKey;
            var code = keys.PrimaryKey;

            if (professionalId <= 0)
                RaiseNotification(nameof(professionalId));

            if (code == Guid.Empty)
                RaiseNotification(nameof(code));

            if (professional == null)
                RaiseNotification(nameof(professional));

            if (Notification.HasNotification())
                return ProfessionalDto.NullInstance;

            var professionalBuilder = new ProfessionalBuilder(Notification)
                .WithProfessionalId(keys.SecundaryKey)
                .WithCode(keys.PrimaryKey)
                .WithName(professional.Name)
                .WithPhone(professional.Phone)
                .WithEmail(professional.Email)
                .WithAddress(professional.Address)
                .WithSpecialties(professional.Specialties.Select(s => new SpecialtyBuilder(Notification).WithId(s.Id).WithDescription(s.Description).Build()).ToList());

            _service.UpdateProfessional(professionalBuilder);

            professional.ProfessionalId = keys.SecundaryKey;
            professional.Code = keys.PrimaryKey;
            return professional;
        }

        public void DeleteProfessional(ComposeKey<Guid, decimal> keys)
        {
            ValidateId(keys);

            if (Notification.HasNotification())
                return;

            var professionalId = keys.SecundaryKey;
            var code = keys.PrimaryKey;

            if (professionalId <= 0)
                RaiseNotification(nameof(professionalId));

            if (code == Guid.Empty)
                RaiseNotification(nameof(code));

            if (Notification.HasNotification())
                return;

            _service.DeleteProfessional(keys);
        }

        private void RaiseNotification(params object[] parameter)
        {
            Notification.Raise(Notification.DefaultBuilder
                                                .WithMessage(AppConsts.LocalizationSourceName, Error.InvalidParameter)
                                                .WithDetailedMessage(AppConsts.LocalizationSourceName, Error.InvalidParameter)
                                                .WithMessageFormat(parameter)
                                                .Build());
        }
    }
}
