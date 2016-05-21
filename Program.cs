using System;
using CommandLine;
using PX.Api.ContractBase.Maintenance.Cli.Verbs;

namespace PX.Api.ContractBase.Maintenance.Cli
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
                    (GetSchema opts) => 0,
                    (PutSchema opts) => 0,
                    errors => 2
                );
        }
    }
}
