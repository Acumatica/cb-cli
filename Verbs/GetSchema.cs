using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml;
using CommandLine;
using CommandLine.Text;

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
                    new UnParserSettings {PreferShortName = true},
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
            using (var client = new Maintenance531.EntityMaintenanceSoapClient(new BasicHttpBinding
            {
                AllowCookies = true,
                MaxReceivedMessageSize = 1024*1024,
            }, new EndpointAddress(AcumaticaUrl + "/entity/maintenance/5.31")))
            {
                await client.LoginAsync(Login, Password, null, null, null).ConfigureAwait(false);
                try
                {
                    var schemaText = (await client.GetSchemaAsync(EndpointVersion, EndpointName).ConfigureAwait(false)).Body.GetSchemaResult;
                    var schema = new XmlDocument();
                    schema.LoadXml(schemaText);
                    using (var xmlWriter = XmlWriter.Create(Console.Out, new XmlWriterSettings {Indent = true}))
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