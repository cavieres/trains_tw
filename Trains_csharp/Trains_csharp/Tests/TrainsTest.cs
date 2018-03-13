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

        #region Distance of the route.
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
        #endregion

        #region Number of trips
        [Test]
        public void should_get_max_number_of_trips_C_to_C()
        {
            var service = new NumberOfTripsService(GraphInput);

            var result = service.GetMaxNumberOfTrips("C", "C", 3);

            Assert.AreEqual("The number of trips starting at C and ending at C with maximum of 3 stops. In the sample data below, there are two such trips: C-D-C (2 stops). and C-E-B-C (3 stops)", result.Pregunta);
            Assert.AreEqual("2", result.Salida);
        }

        [Test]
        public void should_get_exactly_number_of_trips_A_to_C()
        {
            var service = new NumberOfTripsService(GraphInput);

            var result = service.GetExactlyNumberOfTrips("A", "C", 4);

            Assert.AreEqual("The number of trips starting at A and ending at C with exactly 4 stops. In the sample data below, there are three such trips: A to C (via B,C,D); A to C (via D,C,D); and A to C (via D,E,B).", result.Pregunta);
            Assert.AreEqual("3", result.Salida);
        }
        #endregion

        #region Shortest route
        [Test]
        public void should_get_shortest_route_from_A_to_C()
        {
            var trainService = new ShortestRouteService(GraphInput);

            var result = trainService.GetShortestRoute("A", "C");

            Assert.AreEqual("The lenght of the shortest route (in terms of distance to travel) from A to C.", result.Pregunta);
            Assert.AreEqual("9", result.Salida);
        }

        [Test]
        public void should_get_shortest_route_from_B_to_B()
        {
            var trainService = new ShortestRouteService(GraphInput);

            var result = trainService.GetShortestRoute("B", "B");

            Assert.AreEqual("The lenght of the shortest route (in terms of distance to travel) from B to B.", result.Pregunta);
            Assert.AreEqual("9", result.Salida);
        }
        #endregion

        #region Different routes
        [Test]
        public void should_get_different_routes_from_C_to_C()
        {
            var trainService = new NumberOfDiffRoutesService(GraphInput);

            var result = trainService.GetNumberDiffRoutes("C", "C", 30);

            Assert.AreEqual("The number of different routes from C to C with a distance of less than 30. In the sample data, the trips are: CDC, CEBC, CEBCDC, CDCEBC, CDEBC, CEBCEBCEBC, CEBCEBCEBC", result.Pregunta);
            Assert.AreEqual("7", result.Salida);
        }
        #endregion
    }
}
