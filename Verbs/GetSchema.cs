using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace PX.Api.ContractBase.Maintenance.Cli.Verbs
{
    [Verb("getSchema")]
    internal class GetSchema
    {
        [Option('u', "url", Required = true)]
        public string Url { get; set; }

        [Option('l', "login", Default = "admin", Required = false)]
        public string Login { get; set; }

        [Option('p', "pwd", Required = true)]
        public string Password { get; set; }

        [Option('s', "service")]
        public string ServiceVersion { get; set; }

        [Value(0, Required = true, MetaName = "endpointName")]
        public string EndpointName { get; set; }

        [Value(1, Required = true, MetaName = "endpointVersion")]
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
                        Url = "http://acumatica-host/acumatica-dir",
                        EndpointName = "Default",
                        EndpointVersion = "5.30.001",
                        Password = "<your admin password>"
                    });
            }
        }

        public int Invoke()
        {
            throw new System.NotImplementedException();
        }
    }
}