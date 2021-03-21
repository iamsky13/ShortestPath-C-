using System;
using System.Text;
namespace ShortestPath
{
    public class Nodes
    {
        public int NodeId { get; set; }
        //uint has been used vecause all our distances 
        //are positive and uint has higher 
        //range compared to int for positive integer
        public uint Distance { get; set; } = uint.MaxValue;
        public Nodes Predecessor { get; set; } = null;
        public bool IsNodeChecked { get; set; }
    }
    class Program
    {
        //adjancy matrix of Graph
        static uint[][] Graph = new uint[][]
           {
           new uint[]{0, 2, 3, 0, 0, 0, 0,},
            new uint[]{7, 0, 0, 1, 0, 0, 3,},
            new uint[]{3, 0, 0, 0, 0, 0, 5,},
            new uint[]{0, 0, 0, 0, 0, 2, 2,},
            new uint[]{0, 0, 0, 0, 0, 0, 0,},
            new uint[]{0, 0, 0, 1, 11, 0, 7,},
            new uint[]{0, 0, 0, 0, 2, 7, 0,}
           };
        static void Main(string[] args)
        {
            //assign destination node and source node that belongs to Graph
            int destination = 3;
            int source = 2;

            var result = ApplyDijkstra(source);
           
            int hops = 0;
            int[] shortestPathNodes=new int[result.Length];
            shortestPathNodes[hops] = destination;


            while (true)
            {
                //if destination node has no predecessor then there will be no valid path
                if(!CheckPredecessorExist(result[shortestPathNodes[hops]]))
                {
                    Console.WriteLine("no valid path");
                    break;
                }
                //we went from destination to source to find the best path and hops
                else if(result[shortestPathNodes[hops]].Predecessor.NodeId != source)
                {
                    shortestPathNodes[hops + 1] = result[shortestPathNodes[hops]].Predecessor.NodeId;
                    ++hops;
                }
                //if we reach till here we have found the shortest path
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

            Console.ReadLine();
         }
            
        

        private static bool CheckPredecessorExist(Nodes node)
        {
            return node.Predecessor != null;
        }

        private static Nodes[] ApplyDijkstra(int source)
        {
            //Converted adjancy matrix to array of nodes
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
                //FindNewNearestNode will discover new shortest node
                var activeNode = FindNewNearestNode(Nodes);

                //assign this node as not new 
                Nodes[activeNode].IsNodeChecked = true;

                for (int n = 0; n < Nodes.Length; n++)
                {
                    //here we update the shortest path, predecessor if the new Distance is less than current best distance
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
                //IsNodeChecked will verify if it is new node or not
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
