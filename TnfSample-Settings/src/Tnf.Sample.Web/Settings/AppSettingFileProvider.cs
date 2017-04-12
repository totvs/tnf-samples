using System.Collections.Generic;
using Tnf.App.Configuration;

namespace Tnf.Sample
{
    public class AppSettingFileProvider : SettingsFileProvider
    {
        protected override IEnumerable<string> GetJsonFiles()
        {
            return new string[] { "SampleSetting.json" };
        }

        //protected override string GetRootSection()
        //{
        //    return "subsection";
        //}
    }
}
