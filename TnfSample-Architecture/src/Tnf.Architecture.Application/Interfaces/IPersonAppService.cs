using Tnf.App.Application.Services;
using Tnf.Architecture.Dto.Registration;

namespace Tnf.Architecture.Application.Interfaces
{
    public interface IPersonAppService : IAppApplicationService<PersonDto, GetAllPeopleDto>
    {
    }
}
