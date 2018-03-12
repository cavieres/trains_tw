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
        public void should_get_shortest_route_from_A_TO_C()
        {
            var trainService = new TrainService(GraphInput);

            var result = trainService.GetShortestRoute("A", "C");

            Assert.AreEqual("The lenght of the shortest route (in terms of distance to travel) from A to C.", result.Pregunta);
            Assert.AreEqual("9", result.Salida);
        }

        [Test]
        public void should_get_shortest_route_from_B_TO_B()
        {
            var trainService = new TrainService(GraphInput);

            var result = trainService.GetShortestRoute("B", "D");

            Assert.AreEqual("The lenght of the shortest route (in terms of distance to travel) from B to B.", result.Pregunta);
            Assert.AreEqual("9", result.Salida);
        }
    }
}
