using Tnf.Application.Services;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Dto.Registration;
using Tnf.Dto.Interfaces;
using Tnf.Dto.Request;
using Tnf.Dto.Response;

namespace Tnf.Architecture.Application.Services
{
    public class SpecialtyAppService : ApplicationService, ISpecialtyAppService
    {
        private readonly ISpecialtyService _service;

        public SpecialtyAppService(ISpecialtyService service)
        {
            _service = service;
        }

        public IResponseDto GetAllSpecialties(GetAllSpecialtiesDto request)
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

            return _service.GetAllSpecialties(request);
        }

        public IResponseDto GetSpecialty(RequestDto<int> id)
        {
            var message = "";
            var detailedMssage = "";
            var builder = ErrorResponseDto.DefaultBuilder;

            if (id.Key <= 0)
            {
                message = "Invalid parameter";
                detailedMssage = $"Invalid parameter: {nameof(id)}";
            }

            if (!string.IsNullOrEmpty(message))
                return builder
                        .WithMessage(message)
                        .WithDetailedMessage(detailedMssage)
                        .Build();

            return _service.GetSpecialty(id);
        }

        public IResponseDto CreateSpecialty(SpecialtyDto specialty)
        {
            var message = "";
            var detailedMssage = "";
            var builder = ErrorResponseDto.DefaultBuilder;

            if (specialty == null)
            {
                message = "Invalid parameter";
                detailedMssage = $"Invalid parameter: {nameof(specialty)}";
            }

            if (!string.IsNullOrEmpty(message))
                return builder
                        .WithMessage(message)
                        .WithDetailedMessage(detailedMssage)
                        .Build();

            return _service.CreateSpecialty(specialty);
        }

        public IResponseDto UpdateSpecialty(int id, SpecialtyDto specialty)
        {
            var message = "";
            var detailedMssage = "";
            var builder = ErrorResponseDto.DefaultBuilder;

            if (id <= 0)
            {
                message = "Invalid parameter";
                detailedMssage = $"Invalid parameter: {nameof(id)}";
            }
            else if (specialty == null)
            {
                message = "Invalid parameter";
                detailedMssage = $"Invalid parameter: {nameof(specialty)}";
            }

            if (!string.IsNullOrEmpty(message))
                return builder
                        .WithMessage(message)
                        .WithDetailedMessage(detailedMssage)
                        .Build();

            specialty.Id = id;
            return _service.UpdateSpecialty(specialty);
        }

        public IResponseDto DeleteSpecialty(int id)
        {
            var message = "";
            var detailedMssage = "";
            var builder = ErrorResponseDto.DefaultBuilder;

            if (id <= 0)
            {
                message = "Invalid parameter";
                detailedMssage = $"Invalid parameter: {nameof(id)}";
            }

            if (!string.IsNullOrEmpty(message))
                return builder
                        .WithMessage(message)
                        .WithDetailedMessage(detailedMssage)
                        .Build();

            return _service.DeleteSpecialty(id);
        }
    }
}
