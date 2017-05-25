using Tnf.Application.Services;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto.Registration;
using Tnf.Dto.Response;
using Tnf.Dto.Interfaces;
using Tnf.Dto.Request;
using System;

namespace Tnf.Architecture.Application.Services
{
    public class ProfessionalAppService : ApplicationService, IProfessionalAppService
    {
        private readonly IProfessionalService _service;

        public ProfessionalAppService(IProfessionalService service)
        {
            _service = service;
        }

        public IResponseDto GetAllProfessionals(GetAllProfessionalsDto request)
        {
            var message = "";
            var detailedMssage = "";
            var builder = ErrorResponseDto.DefaultBuilder;

            if (request.PageSize <= 0)
            {
                message = "Invalid parameter";
                detailedMssage = $"Invalid parameter: {nameof(request.PageSize)}";
            }

            if (!string.IsNullOrEmpty(message))
                return builder
                        .WithMessage(message)
                        .WithDetailedMessage(detailedMssage)
                        .Build();

            return _service.GetAllProfessionals(request);
        }

        public IResponseDto GetProfessional(RequestDto<ProfessionalKeysDto> keys)
        {
            var message = "";
            var detailedMssage = "";
            var builder = ErrorResponseDto.DefaultBuilder;

            if (keys.Key.ProfessionalId <= 0)
            {
                message = "Invalid parameter";
                detailedMssage = $"Invalid parameter: {nameof(keys.Key.ProfessionalId)}";
            }
            else if (keys.Key.Code == Guid.Empty)
            {
                message = "Invalid parameter";
                detailedMssage = $"Invalid parameter: {nameof(keys.Key.Code)}";
            }

            if (!string.IsNullOrEmpty(message))
                return builder
                        .WithMessage(message)
                        .WithDetailedMessage(detailedMssage)
                        .Build();

            return _service.GetProfessional(keys);
        }

        public IResponseDto CreateProfessional(ProfessionalDto professional)
        {
            var message = "";
            var detailedMssage = "";
            var builder = ErrorResponseDto.DefaultBuilder;

            if (professional == null)
            {
                message = "Invalid parameter";
                detailedMssage = $"Invalid parameter: {nameof(professional)}";
            }

            if (!string.IsNullOrEmpty(message))
                return builder
                        .WithMessage(message)
                        .WithDetailedMessage(detailedMssage)
                        .Build();

            return _service.CreateProfessional(professional);
        }

        public IResponseDto UpdateProfessional(ProfessionalKeysDto keys, ProfessionalDto professional)
        {
            var message = "";
            var detailedMssage = "";
            var builder = ErrorResponseDto.DefaultBuilder;

            if (keys.ProfessionalId <= 0)
            {
                message = "Invalid parameter";
                detailedMssage = $"Invalid parameter: {nameof(keys.ProfessionalId)}";
            }
            else if (keys.Code == Guid.Empty)
            {
                message = "Invalid parameter";
                detailedMssage = $"Invalid parameter: {nameof(keys.Code)}";
            }
            else if (professional == null)
            {
                message = "Invalid parameter";
                detailedMssage = $"Invalid parameter: {nameof(professional)}";
            }

            if (!string.IsNullOrEmpty(message))
                return builder
                        .WithMessage(message)
                        .WithDetailedMessage(detailedMssage)
                        .Build();

            professional.ProfessionalId = keys.ProfessionalId;
            professional.Code = keys.Code;
            return _service.UpdateProfessional(professional);
        }

        public IResponseDto DeleteProfessional(ProfessionalKeysDto keys)
        {
            var message = "";
            var detailedMssage = "";
            var builder = ErrorResponseDto.DefaultBuilder;

            if (keys.ProfessionalId <= 0)
            {
                message = "Invalid parameter";
                detailedMssage = $"Invalid parameter: {nameof(keys.ProfessionalId)}";
            }
            else if (keys.Code == Guid.Empty)
            {
                message = "Invalid parameter";
                detailedMssage = $"Invalid parameter: {nameof(keys.Code)}";
            }

            if (!string.IsNullOrEmpty(message))
                return builder
                        .WithMessage(message)
                        .WithDetailedMessage(detailedMssage)
                        .Build();

            return _service.DeleteProfessional(keys);
        }
    }
}
