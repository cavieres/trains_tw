﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trains_csharp
{
    public class DistanceRouteService
    {
        private Dictionary<string, int?> Distances;
        private Dictionary<string, string> Routes;

        private List<string> Cities;

        private Graph<string> TrainsGraph { get; set; }

        public DistanceRouteService(string graphInput)
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

        public RouteResponse GetRouteDistance(List<string> route)
        {
            var distance = 0;

                for (int i = 0; i < route.Count - 1; i++)
                {
                    var neighbors = GetNeighbors(route[i]);
                    var nextStop = neighbors.Where(n => n.Value == route[i + 1]).FirstOrDefault();

                    if (nextStop != null)
                        distance += nextStop.Cost;
                    else
                        distance = 0;
                }

            var pregunta = string.Format("The distance of the route {0}", string.Join("-", route.ToArray()));

            if (distance == 0)
                return new RouteResponse { Pregunta = pregunta, Salida = "NO SUCH ROUTE" };
            else
                return new RouteResponse { Pregunta = pregunta, Salida = distance.ToString() };
        }

    }
}
