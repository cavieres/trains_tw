using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trains_csharp
{
    class Program
    {
        class Options
        {
            [Option('r', "read", Required = true, HelpText = "Input files to be processed.")]
            public string InputFiles { get; set; }

            [Option('q', "question", Required = false, HelpText = "Question")]
            public IEnumerable<string> Question { get; set; }
        }

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts))
                .WithNotParsed<Options>((errs) => HandleParseError(errs));
        }

        private static void RunOptionsAndReturnExitCode(Options opts)
        {
            var graphInput = System.IO.File.ReadAllText(opts.InputFiles).ToUpper().Replace(" ", string.Empty);
            RouteResponse response = new RouteResponse();

            switch(opts.Question.First())
            {
                case "d":
                    var route = ((string[])opts.Question)[1].ToUpper().Replace(" ", string.Empty).Split(',').ToList();

                    response = new DistanceRouteService(graphInput).GetRouteDistance(route);
                    break;
                case "t":
                    var trips = ((string[])opts.Question)[1].ToUpper().Replace(" ", string.Empty).Split(',').ToList();
                    var maxStops = Convert.ToInt16(((string[])opts.Question)[2].ToUpper().Replace(" ", string.Empty));

                    response = new NumberOfTripsService(graphInput).GetMaxNumberOfTrips(trips[0], trips[1], maxStops);
                    break;
                case "l":
                    route = ((string[])opts.Question)[1].ToUpper().Replace(" ", string.Empty).Split(',').ToList();

                    response = new ShortestRouteService(graphInput).GetShortestRoute(route[0], route[1]);
                    break;
                case "r":
                    route = ((string[])opts.Question)[1].ToUpper().Replace(" ", string.Empty).Split(',').ToList();
                    maxStops = Convert.ToInt16(((string[])opts.Question)[2].ToUpper().Replace(" ", string.Empty));

                    response = new NumberOfDiffRoutesService(graphInput).GetNumberDiffRoutes(route[0], route[1], maxStops);
                    break;
            }

            Console.WriteLine(string.Format("Pregunta: {0}; Salida: {1}", 
                response.Pregunta, response.Salida));
        }

        private static void HandleParseError(IEnumerable<Error> errs)
        {
        }
    }
}
