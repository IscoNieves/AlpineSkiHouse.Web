using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace AlpineSkiHouse.Web.Configuration
{
    public static class CsrInformationExtensions
    {
        public static IConfigurationBuilder AddCsrInformationFile(this IConfigurationBuilder builder, string path, bool reloadOnChange)
        {
            return AddCsrInformationFile(builder, null, path, false, reloadOnChange);
        }

        private static IConfigurationBuilder AddCsrInformationFile(IConfigurationBuilder builder, IFileProvider provider, string path, bool optional, bool reloadOnChange)
        {
            // Guard clauses
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("The name of the CSR information file must be provided.", nameof(path));
            }

            // Ensure provider is configured
            if (provider == null)
            {
                if (Path.IsPathRooted(path))
                {
                    provider = new PhysicalFileProvider(Path.GetDirectoryName(path));
                    path = Path.GetFileName(path);
                }
            }

            // Create the configuration source
            var source = new CsrInformationConfigurationSource
            {
                FileProvider = provider,
                Path = path,
                Optional = optional,
                ReloadOnChange = reloadOnChange
            };

            // Add the source to the builder
            builder.Add(source);

            return builder;
        }
    }
}
