using Tnf.Builder;
using Tnf.Architecture.Dto.Enumerables;
using Tnf.Architecture.Dto;

namespace Tnf.Architecture.Domain
{
    internal class Builder : Builder<object>
    {
        public Builder WithNotFound()
        {
            AddEnum(AppConsts.LocalizationSourceName, Error.NotFound);
            return this;
        }
    }
}
