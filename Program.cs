using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace graph
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> test = new List<string>();
            Graph g = new Graph(13);

            g.NewEdge("A", "B");
            g.NewEdge("A", "C");
            g.NewEdge("A", "D");
            g.NewEdge("B", "C");
            g.NewEdge("B", "E");
            g.NewEdge("B", "F");
            g.NewEdge("C", "F");
            g.NewEdge("C", "G");
            g.NewEdge("D", "G");
            g.NewEdge("D", "F");
            g.NewEdge("E", "H");
            g.NewEdge("E", "F");
            g.NewEdge("F", "H");

            test = g.SearchPath("A","H");
            foreach (var val in test)
            {
                Console.Write(val + " ");
            }
        }
    }
}
