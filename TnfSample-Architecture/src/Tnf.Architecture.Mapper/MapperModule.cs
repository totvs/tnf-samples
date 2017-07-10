using Tnf.Architecture.Mapper.Profiles;
using Tnf.AutoMapper;
using Tnf.Modules;

namespace Tnf.Architecture.Mapper
{
    [DependsOn(typeof(TnfAutoMapperModule))]
    public class MapperModule : TnfModule
    {
        public override void PreInitialize()
        {
            base.PreInitialize();

            Configuration.Modules
                .TnfAutoMapper()
                .Configurators
                .Add(config =>
                {
                    config.AddProfile(new DtoToPocoProfile());
                    config.AddProfile(new PocoToDtoProfile());
                    config.AddProfile(new EntityToPocoProfile());
                });
        }
    }
}
