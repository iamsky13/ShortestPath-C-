using System;
using System.Text;
namespace ShortestPath
{
    public class Nodes
    {
        public int NodeId { get; set; }
        public uint Distance { get; set; } = uint.MaxValue;
        public Nodes Predecessor { get; set; } = null;
        public bool IsNodeChecked { get; set; }
    }
    class Program
    {
        static uint[][] Graph = new uint[][]
        {
            new uint[]{0, 5, 0, 0, 0, 0, 0, 0},
            new uint[]{7, 0, 0, 11, 11, 0, 0, 0},
            new uint[]{0, 0, 0, 0, 0, 0, 42, 5},
            new uint[]{0, 3, 0, 0, 1, 0, 0, 0},
            new uint[]{11, 11, 3, 3, 0, 0, 0, 0},
            new uint[]{0, 0, 0, 0, 0, 0, 0, 0},
            new uint[]{0, 0, 42, 0, 0, 5, 0, 0},
            new uint[]{0, 0, 42, 0, 0, 0, 0, 0},
        };
        static void Main(string[] args)
        {
            int destination = 1;
            int source = 4;

            var result = ApplyDijkstra(source);
           
            int hops = 0;
            int[] shortestPathNodes=new int[result.Length];
            shortestPathNodes[hops] = destination;


            while (true)
            {
                if(!CheckPredecessorExist(result[shortestPathNodes[hops]]))
                {
                    Console.WriteLine("no valid path");
                    break;
                }
                else if(result[shortestPathNodes[hops]].Predecessor.NodeId != source)
                {
                    shortestPathNodes[hops + 1] = result[shortestPathNodes[hops]].Predecessor.NodeId;
                    ++hops;
                }
                else
                {
                    ++hops;
                    shortestPathNodes[hops] = source;
                    Console.WriteLine("Shortest Path = " + result[destination].Distance);
                    Console.Write("Path : " + source);
                    for (int i = hops - 1; i>=0; i--)
                    {
                        Console.Write(" --> " + shortestPathNodes[i]);
                    }

                    Console.WriteLine("\nhops : " + (hops));
                    break;
                }
               
            }

            
         }
            
        

        private static bool CheckPredecessorExist(Nodes node)
        {
            return node.Predecessor != null;
        }

        private static Nodes[] ApplyDijkstra(int source)
        {
            
            Nodes[] Nodes = new Nodes[Graph[1].Length];
            for(int i = 0; i < Nodes.Length; i++)
            {
                Nodes[i] = new Nodes
                {
                    Distance = uint.MaxValue,
                    NodeId=i
                };
            }
            Nodes[source].Distance = 0;
            Nodes[source].Predecessor = null;

            for (int count=0;count< Nodes.Length - 1; count++)
            {
                var activeNode = FindNewNearestNode(Nodes);

                Nodes[activeNode].IsNodeChecked = true;

                for (int n = 0; n < Nodes.Length; n++)
                {
                    if (!Nodes[n].IsNodeChecked &&
                        Graph[activeNode][n] != 0 &&
                        Nodes[activeNode].Distance != uint.MaxValue &&
                        Nodes[activeNode].Distance + Graph[activeNode][n] < Nodes[n].Distance)
                    {
                        Nodes[n].Distance = Nodes[activeNode].Distance + Graph[activeNode][n];
                        Nodes[n].Predecessor = Nodes[activeNode];
                    }
                }
            }
            return Nodes;
           
        }

        private static int FindNewNearestNode(Nodes[] nodes)
        {
            uint minPathDistance = uint.MaxValue;
            int newNode = -1;

            for(int n = 0; n < nodes.Length; n++)
            {
                if(!nodes[n].IsNodeChecked && nodes[n].Distance <= minPathDistance)
                {
                    minPathDistance = nodes[n].Distance;
                    newNode = n;
                }
            }
            return newNode;
        }
    }
}
