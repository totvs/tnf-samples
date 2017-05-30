using Tnf.Builder;
using Tnf.Architecture.Dto.Enumerables;

namespace Tnf.Architecture.Domain
{
    internal class Builder : Builder<object>
    {
        public Builder WithNotFound()
        {
            AddEnum(Error.NotFound);
            return this;
        }
    }
}
