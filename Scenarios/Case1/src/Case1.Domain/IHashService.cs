using Tnf.Dependency;

namespace Case1.Domain
{
    public interface IHashService : ITransientDependency
    {
        string CalculateHash(string value);
    }
}