using System;
using CommandLine;
using PX.Api.ContractBased.Maintenance.Cli.Verbs;

namespace PX.Api.ContractBased.Maintenance.Cli
{
    class Program
    {
        static int Main(string[] args)
        {
            return Parser.Default
                .ParseArguments(args,
                    typeof(GetSchema),
                    typeof(PutSchema)
                    )
                .MapResult(
                    (GetSchema opts) => opts.InvokeAsync().Result,
                    (PutSchema opts) => opts.InvokeAsync().Result,
                    errors => 2
                );
        }
    }
}
