using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trains_csharp
{
    [TestFixture]
    class TrainsTest
    {
        private string GraphInput = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";

        [Test]
        public void should_get_distance_of_the_route_A_B_C()
        {
            var service = new DistanceRouteService(GraphInput);

            var result = service.GetRouteDistance(new List<string>() { "A", "B", "C" });

            Assert.AreEqual("The distance of the route A-B-C", result.Pregunta);
            Assert.AreEqual("9", result.Salida);
        }

        [Test]
        public void should_get_distance_of_the_route_A_D()
        {
            var service = new DistanceRouteService(GraphInput);

            var result = service.GetRouteDistance(new List<string>() { "A", "D" });

            Assert.AreEqual("The distance of the route A-D", result.Pregunta);
            Assert.AreEqual("5", result.Salida);
        }

        [Test]
        public void should_get_distance_of_the_route_A_D_C()
        {
            var service = new DistanceRouteService(GraphInput);

            var result = service.GetRouteDistance(new List<string>() { "A", "D", "C" });

            Assert.AreEqual("The distance of the route A-D-C", result.Pregunta);
            Assert.AreEqual("13", result.Salida);
        }

        [Test]
        public void should_get_distance_of_the_route_A_E_B_C_D()
        {
            var service = new DistanceRouteService(GraphInput);

            var result = service.GetRouteDistance(new List<string>() { "A", "E", "B", "C", "D" });

            Assert.AreEqual("The distance of the route A-E-B-C-D", result.Pregunta);
            Assert.AreEqual("22", result.Salida);
        }

        [Test]
        public void should_get_distance_of_the_route_A_E_D()
        {
            var service = new DistanceRouteService(GraphInput);

            var result = service.GetRouteDistance(new List<string>() { "A", "E", "D" });

            Assert.AreEqual("The distance of the route A-E-D", result.Pregunta);
            Assert.AreEqual("NO SUCH ROUTE", result.Salida);
        }

        [Test]
        public void should_get_shortest_route_from_A_TO_C()
        {
            var trainService = new ShortestRouteService(GraphInput);

            var result = trainService.GetShortestRoute("A", "C");

            Assert.AreEqual("The lenght of the shortest route (in terms of distance to travel) from A to C.", result.Pregunta);
            Assert.AreEqual("9", result.Salida);
        }

        [Test]
        public void should_get_shortest_route_from_B_TO_B()
        {
            var trainService = new ShortestRouteService(GraphInput);

            var result = trainService.GetShortestRoute("B", "B");

            Assert.AreEqual("The lenght of the shortest route (in terms of distance to travel) from B to B.", result.Pregunta);
            Assert.AreEqual("9", result.Salida);
        }
    }
}
