using System.Collections.Generic;

namespace KokoEngine
{
    internal class AStarGraphSearch
    {
        public Dictionary<IPathfindingNode, GraphEdge> shortestPahTree = new Dictionary<IPathfindingNode, GraphEdge>();
        public Dictionary<IPathfindingNode, GraphEdge> searchFrontier = new Dictionary<IPathfindingNode, GraphEdge>();
        public List<IPathfindingNode> shortestPath = new List<IPathfindingNode>();

        GraphNodePriorityQueue pq = new GraphNodePriorityQueue();

        public bool Search(Graph graph, IPathfindingNode source, IPathfindingNode target, IHeuristicCalculator hc)
        {
            pq.Add(source);

            while (pq.count > 0)
            {
                IPathfindingNode nextClosestNode = pq.PopFirst();
                if (searchFrontier.ContainsKey(nextClosestNode))
                {
                    GraphEdge nearestEdge = searchFrontier[nextClosestNode];
                    if (shortestPahTree.ContainsKey(nextClosestNode))
                    {
                        shortestPahTree[nextClosestNode] = nearestEdge;
                    }
                    else
                    {
                        shortestPahTree.Add(nextClosestNode, nearestEdge);
                    }
                }

                if (nextClosestNode == target)
                {
                    ConstructPath(source, target);
                    return true;
                }

                List<GraphEdge> frontier = graph.GetFrontier(nextClosestNode);
                for (int i = 0; i < frontier.Count; i++)
                {
                    GraphEdge frontierEdge = frontier[i];
                    IPathfindingNode neighbour = frontierEdge.to;

                    float realCost = nextClosestNode.RealNodeCost + frontierEdge.cost;
                    float heuristicCost = hc.Calculate(neighbour, target);
                    float totalCost = nextClosestNode.TotalNodeCost + frontierEdge.cost;

                    if (searchFrontier.ContainsKey(neighbour) == false)
                    {
                        neighbour.TotalNodeCost = totalCost;
                        neighbour.RealNodeCost = realCost;
                        searchFrontier.Add(neighbour, frontierEdge);
                        pq.Add(neighbour);
                    }
                    else if (realCost < neighbour.RealNodeCost)
                    {
                        neighbour.TotalNodeCost = totalCost;
                        neighbour.RealNodeCost = realCost;
                        pq.MarkToSort();
                        searchFrontier[neighbour] = frontierEdge;
                    }
                }
            }
            return false;
        }

        private void ConstructPath(IPathfindingNode source, IPathfindingNode target)
        {
            GraphEdge edge = shortestPahTree[target];

            while (edge.from != source)
            {
                shortestPath.Add(edge.from);
                edge = shortestPahTree[edge.from];
            }

            shortestPath.Reverse();
        }
    }
}
