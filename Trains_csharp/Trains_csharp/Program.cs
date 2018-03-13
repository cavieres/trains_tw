using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trains_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var graphInput = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";

            var trainService = new ShortestRouteService(graphInput);

            var result = trainService.GetShortestRoute("A", "C");

            Console.WriteLine(string.Format("Item: 1; Pregunta: {0}; Salida: {1}", result.Pregunta, result.Salida));
        }

        private void Main2()
        {
            Graph<string> web = new Graph<string>();

            web.AddNode("Privacy.htm");
            web.AddNode("People.aspx");
            web.AddNode("About.htm");
            web.AddNode("Index.htm");
            web.AddNode("Products.aspx");
            web.AddNode("Contact.aspx");

            web.AddDirectedEdge((GraphNode<string>)web.Nodes[1], (GraphNode<string>)web.Nodes[0], 3); // People -> Privacy

            foreach (GraphNode<string> w in web.Nodes)
            {
                Console.WriteLine("Node: " + w.Value + "; Neighbors: ");

                for (int i = 0; i < w.Neighbors.Count; i++)
                {
                    Console.WriteLine(w.Neighbors[i].Value + "; Weight: " + w.Costs[i]);
                }

                Console.WriteLine("\n");
            }

            Console.ReadKey();

            //web.AddDirectedEdge("People.aspx", "Privacy.htm");  // People -> Privacy

            //web.AddDirectedEdge("Privacy.htm", "Index.htm");    // Privacy -> Index
            //web.AddDirectedEdge("Privacy.htm", "About.htm");    // Privacy -> About

            //web.AddDirectedEdge("About.htm", "Privacy.htm");    // About -> Privacy
            //web.AddDirectedEdge("About.htm", "People.aspx");    // About -> People
            //web.AddDirectedEdge("About.htm", "Contact.aspx");   // About -> Contact

            //web.AddDirectedEdge("Index.htm", "About.htm");      // Index -> About
            //web.AddDirectedEdge("Index.htm", "Contact.aspx");   // Index -> Contacts
            //web.AddDirectedEdge("Index.htm", "Products.aspx");  // Index -> Products

            //web.AddDirectedEdge("Products.aspx", "Index.htm");  // Products -> Index
            //web.AddDirectedEdge("Products.aspx", "People.aspx");// Products -> People
        }
    }
}
