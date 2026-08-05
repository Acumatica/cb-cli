using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Acumatica.RESTClient.AuthApi;
using Acumatica.RESTClient.Client;
using Acumatica.RESTClient.MaintenanceApi;
using CommandLine;
using CommandLine.Text;
using PX.Api.ContractBased.Maintenance.Cli.Utils;

namespace PX.Api.ContractBased.Maintenance.Cli.Verbs
{
    [Verb("getSchema")]
    internal class GetSchema
    {
        [Value(0, Required = true, MetaName = "acumaticaBaseUrl")]
        public string AcumaticaUrl { get; set; }

        [Option('u', "user", Default = "admin", Required = false)]
        public string Login { get; set; }

        [Option('p', "pwd", Required = true)]
        public string Password { get; set; }

        [Option('s', "service")]
        public string ServiceVersion { get; set; }

        [Value(1, Required = true, MetaName = "endpointName")]
        public string EndpointName { get; set; }

        [Value(2, Required = true, MetaName = "endpointVersion")]
        public string EndpointVersion { get; set; }

        [Usage]
        public static IEnumerable<Example> Usage
        {
            get
            {
                yield return new Example(
                    "Typical usage",
                    new UnParserSettings { PreferShortName = true },
                    new GetSchema
                    {
                        AcumaticaUrl = "http://acumatica-host/acumatica-dir",
                        EndpointName = "Default",
                        EndpointVersion = "5.30.001",
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
                    XDocument schema = XDocument.Parse((await client.GetSchemaAsync(EndpointName, EndpointVersion).ConfigureAwait(false)));
                    schema.Sort();

                    using (var xmlWriter = XmlWriter.Create(Console.Out, new XmlWriterSettings { Indent = true }))
                    {
                        schema.WriteTo(xmlWriter);
                    }
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
