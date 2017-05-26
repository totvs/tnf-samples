using System.Threading.Tasks;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Application.Services;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Dto.Interfaces;
using Tnf.Dto.Response;
using Tnf.Dto.Request;

namespace Tnf.Architecture.Application.Services
{
    [RemoteService(false)]
    public class WhiteHouseAppService : ApplicationService, IWhiteHouseAppService
    {
        private readonly IWhiteHouseService _whiteHouserService;

        public WhiteHouseAppService(IWhiteHouseService whiteHouserService)
        {
            _whiteHouserService = whiteHouserService;
        }

        public async Task<IResponseDto> GetAllPresidents(GetAllPresidentsDto request)
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

            return await _whiteHouserService.GetAllPresidents(request);
        }

        public async Task<IResponseDto> GetPresidentById(RequestDto<string> id)
        {
            var message = "";
            var detailedMssage = "";
            var builder = ErrorResponseDto.DefaultBuilder;

            if (string.IsNullOrWhiteSpace(id.Id))
            {
                message = "Invalid parameter";
                detailedMssage = $"Invalid parameter: {nameof(id)}";
            }

            if (!string.IsNullOrEmpty(message))
                return builder
                        .WithMessage(message)
                        .WithDetailedMessage(detailedMssage)
                        .Build();

            return await _whiteHouserService.GetPresidentById(id);
        }

        public async Task<IResponseDto> InsertPresidentAsync(PresidentDto president, bool sync = true)
        {
            var message = "";
            var detailedMssage = "";
            var builder = ErrorResponseDto.DefaultBuilder;

            if (president == null)
            {
                message = "Invalid parameter";
                detailedMssage = $"Invalid parameter: {nameof(president)}";
            }

            if (!string.IsNullOrEmpty(message))
                return builder
                        .WithMessage(message)
                        .WithDetailedMessage(detailedMssage)
                        .Build();

            return await _whiteHouserService.InsertPresidentAsync(president, sync);
        }

        public async Task<IResponseDto> UpdatePresidentAsync(string id, PresidentDto president)
        {
            var message = "";
            var detailedMssage = "";
            var builder = ErrorResponseDto.DefaultBuilder;

            if (string.IsNullOrEmpty(id))
            {
                message = "Invalid parameter";
                detailedMssage = $"Invalid parameter: {nameof(id)}";
            }
            else if (president == null)
            {
                message = "Invalid parameter";
                detailedMssage = $"Invalid parameter: {nameof(president)}";
            }

            if (!string.IsNullOrEmpty(message))
                return builder
                        .WithMessage(message)
                        .WithDetailedMessage(detailedMssage)
                        .Build();

            president.Id = id;
            return await _whiteHouserService.UpdatePresidentAsync(president);
        }

        public async Task<IResponseDto> DeletePresidentAsync(string id)
        {
            var message = "";
            var detailedMssage = "";
            var builder = ErrorResponseDto.DefaultBuilder;

            if (string.IsNullOrWhiteSpace(id))
            {
                message = "Invalid parameter";
                detailedMssage = $"Invalid parameter: {nameof(id)}";
            }

            if (!string.IsNullOrEmpty(message))
                return builder
                        .WithMessage(message)
                        .WithDetailedMessage(detailedMssage)
                        .Build();

            return await _whiteHouserService.DeletePresidentAsync(id);
        }
    }
}
