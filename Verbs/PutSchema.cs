using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Acumatica.RESTClient.AuthApi;
using Acumatica.RESTClient.Client;
using Acumatica.RESTClient.MaintenanceApi;
using CommandLine;
using CommandLine.Text;

namespace PX.Api.ContractBased.Maintenance.Cli.Verbs
{
    [Verb("putSchema")]
    internal class PutSchema
    {
        [Value(0, Required = true, MetaName = "acumaticaBaseUrl")]
        public string AcumaticaUrl { get; set; }

        [Option('u', "user", Default = "admin", Required = false)]
        public string Login { get; set; }

        [Option('p', "pwd", Required = true)]
        public string Password { get; set; }

        [Option('s', "service")]
        public string ServiceVersion { get; set; }

        [Value(1, Required = true, MetaName = "endpointDefinitionFile")]
        public string EndpointDefinitionFile { get; set; }

        [Usage]
        public static IEnumerable<Example> Usage
        {
            get
            {
                yield return new Example(
                    "Typical usage",
                    new UnParserSettings {PreferShortName = true},
                    new PutSchema
                    {
                        AcumaticaUrl = "http://acumatica-host/acumatica-dir",
                        EndpointDefinitionFile = "Custom-001.xml",
                        Password = "<your admin password>"
                    });
            }
        }

        public async Task<int> InvokeAsync()
        {
            using (var client = new ApiClient(AcumaticaUrl))
            {
#pragma warning disable CS0618 // username/password Login is obsolete in favor of OAuth 2.0, but it is what this tool's -u/-p syntax needs
                await client.LoginAsync(Login, Password, null, null, null).ConfigureAwait(false);
#pragma warning restore CS0618
                try
                {
                    await client.PutSchemaAsync(File.ReadAllText(EndpointDefinitionFile)).ConfigureAwait(false);
                }
                finally
                {
                    client.Logout();
                }
            }
            return 0;
        }
    }
}
