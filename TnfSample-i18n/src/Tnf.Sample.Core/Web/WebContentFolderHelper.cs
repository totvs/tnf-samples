using System;
using System.IO;
using System.Linq;
using Tnf.Reflection.Extensions;

namespace Tnf.Sample.Web
{
    /// <summary>
    /// This class is used to find root path of the web project in;
    /// unit tests (to find views) and entity framework core command line commands (to find conn string).
    /// </summary>
    public static class WebContentDirectoryFinder
    {
        public static string CalculateContentRootFolder()
        {
            var coreAssemblyDirectoryPath = Path.GetDirectoryName(typeof(SampleCoreModule).GetAssembly().Location);
            if (coreAssemblyDirectoryPath == null)
            {
                // TODO: Levantava ApplicationException que só estará presente no .NETStandard 2.0 com ressalvas
                // Vide: https://github.com/dotnet/corefx/issues/4222
                throw new Exception("Could not find location of Tnf.Sample.Core assembly!");
            }

            var directoryInfo = new DirectoryInfo(coreAssemblyDirectoryPath);
            while (!DirectoryContains(directoryInfo.FullName, "Tnf.Sample.sln"))
            {
                if (directoryInfo.Parent == null)
                {
                    // TODO: Levantava ApplicationException que só estará presente no .NETStandard 2.0 com ressalvas
                    // Vide: https://github.com/dotnet/corefx/issues/4222
                    throw new Exception("Could not find content root folder!");
                }

                directoryInfo = directoryInfo.Parent;
            }

            return Path.Combine(directoryInfo.FullName, @"src\Tnf.Sample.Web");
        }

        private static bool DirectoryContains(string directory, string fileName)
        {
            return Directory.GetFiles(directory).Any(filePath => string.Equals(Path.GetFileName(filePath), fileName));
        }
    }
}
