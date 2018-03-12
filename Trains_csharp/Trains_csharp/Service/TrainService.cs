using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trains_csharp
{
    public class TrainService
    {
        private Dictionary<string, int?> Distances;
        private Dictionary<string, string> Routes;

        private List<string> Cities;

        private Graph<string> TrainsGraph { get; set; }

        public TrainService(string graphInput)
        {
            Cities = new List<string>();
            Distances = new Dictionary<string, int?>();
            Routes = new Dictionary<string, string>();

            SetNodes(graphInput);
        }
        
        /// <summary>
        /// Dijkstra algorithm.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public RouteResponse GetShortestRoute(string from, string to)
        {
            InitializeTables();

            do
            {
                var city = PopCity();

                foreach (var neighbor in GetNeighbors(city))
                {
                    var distanceToEvaluate = (Distances[city] ?? 0) + neighbor.Cost;

                    if ((distanceToEvaluate < Distances[neighbor.Value]) || (Distances[neighbor.Value] == null))
                    {
                        Distances[neighbor.Value] = distanceToEvaluate;
                        Routes[neighbor.Value] = city;
                    }
                        
                }

            } while (Cities.Count != 0);

            return new RouteResponse {
                Pregunta = string.Format("The lenght of the shortest route (in terms of distance to travel) from {0} to {1}.", from, to),
                Salida = Convert.ToString(Distances[to]) };
        }

        private void InitializeTables()
        {
            foreach(var t in TrainsGraph.Nodes)
            {
                Distances.Add(t.Value, null);
                Routes.Add(t.Value, null);
                //Cities.Add(t.Value);
            }
            
        }
        
        private List<Neighbor> GetNeighbors(string city)
        {
            
            var node = (GraphNode<string>)(from n in TrainsGraph.Nodes
                            where n.Value == city
                            select n).FirstOrDefault();
            
            return (from neighbor in node.Neighbors
                    select new Neighbor {
                        Value = neighbor.Value,
                        Cost = node.Costs[node.Neighbors.IndexOf(neighbor)]
                    }).ToList();
        }

        private string PopCity()
        {
            if (Cities.Count == 0)
                return null;

            var city = Cities[0];
            Cities.RemoveAt(0);

            return city;
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

            foreach(var route in routes)
            {
                PushCity(route[0]);
                PushCity(route[1]);
            }
        }

    }
}
