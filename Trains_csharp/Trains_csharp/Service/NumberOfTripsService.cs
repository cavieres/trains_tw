using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trains_csharp
{
    public class NumberOfTripsService
    {
        private Dictionary<string, int?> Distances;
        private Dictionary<string, string> Routes;

        private List<string> Cities;

        private List<string> Route = new List<string>();

        private Graph<string> TrainsGraph { get; set; }

        public NumberOfTripsService(string graphInput)
        {
            Cities = new List<string>();
            Distances = new Dictionary<string, int?>();
            Routes = new Dictionary<string, string>();

            SetNodes(graphInput);
        }

        private void PushCity(char city)
        {
            if (!Cities.Contains(Convert.ToString(city)))
                Cities.Add(Convert.ToString(city));
        }

        public void SetNodes(string graphInput)
        {
            SetCities(graphInput);

            TrainsGraph = new Graph<string>();

            foreach (var city in Cities)
            {
                TrainsGraph.AddNode(city);
            }

            var routes = graphInput.Replace(" ", string.Empty).Split(',');

            foreach (var route in routes)
                TrainsGraph.AddDirectedEdge(
                    (GraphNode<string>)TrainsGraph.Nodes.FindByValue(route.Substring(0, 1)),
                    (GraphNode<string>)TrainsGraph.Nodes.FindByValue(route.Substring(1, 1)),
                    Convert.ToInt16(route.Substring(2, 1)));
        }

        private void SetCities(string graphInput)
        {
            var routes = graphInput.Replace(" ", string.Empty).Split(',');

            foreach (var route in routes)
            {
                PushCity(route[0]);
                PushCity(route[1]);
            }
        }

        private List<Neighbor> GetNeighbors(string city)
        {

            var node = (GraphNode<string>)(from n in TrainsGraph.Nodes
                                           where n.Value == city
                                           select n).FirstOrDefault();

            return (from neighbor in node.Neighbors
                    select new Neighbor
                    {
                        Value = neighbor.Value,
                        Cost = node.Costs[node.Neighbors.IndexOf(neighbor)]
                    }).ToList();
        }

        public RouteResponse GetMaxNumberOfTrips(string from, string to, int maxStops)
        {
            throw new NotImplementedException();

            // TODO: Finalize implementation.
            var response = GetNextStop(from, to, maxStops, 1);

            return new RouteResponse();

        }

        public RouteResponse GetExactlyNumberOfTrips(string from, string to, int maxStops)
        {
            throw new NotImplementedException();
        }

        private List<string> GetNextStop(string from, string to, int maxStops, int stopsCount)
        {


            //if (stopsCount > maxStops)
            //    return from;

            var neighbors = GetNeighbors(from);

            foreach (var n in neighbors)
            {
                if (stopsCount > maxStops)
                    break;

                Route.Add(n.Value + "-" + GetNextStop(n.Value, to, maxStops, stopsCount + 1));
            }

            return Route;
        }

    }
}
