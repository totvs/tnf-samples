using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tnf.Configuration;

namespace Tnf.Sample
{
    public class AppSettingsDbProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions()
        {
            return new List<SettingDefinition>
                   {
                       new SettingDefinition("Setting1", "A"),
                       new SettingDefinition("Setting2", "B")
                   };
        }
    }
}
