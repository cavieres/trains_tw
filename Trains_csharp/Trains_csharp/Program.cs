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
            [Option('r', "read", Required = true, HelpText = "Input text file with graph as described in problem description.")]
            public string InputFiles { get; set; }

            [Option('q', "question", Required = true, HelpText = "Question (get distance of route, trips between two nodes, shortest route or different routes).")]
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
                case "tmax":
                    var trips = ((string[])opts.Question)[1].ToUpper().Replace(" ", string.Empty).Split(',').ToList();
                    var maxStops = Convert.ToInt16(((string[])opts.Question)[2].ToUpper().Replace(" ", string.Empty));

                    response = new NumberOfTripsService(graphInput).GetMaxNumberOfTrips(trips[0], trips[1], maxStops);
                    break;
                case "texact":
                    trips = ((string[])opts.Question)[1].ToUpper().Replace(" ", string.Empty).Split(',').ToList();
                    var exactStops = Convert.ToInt16(((string[])opts.Question)[2].ToUpper().Replace(" ", string.Empty));

                    response = new NumberOfTripsService(graphInput).GetExactlyNumberOfTrips(trips[0], trips[1], exactStops);
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
