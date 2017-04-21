using System.Collections.Generic;

namespace KokoEngine
{
    public class AStarGraphSearch
    {
        public Dictionary<IGraphNode, GraphEdge> shortestPahTree = new Dictionary<IGraphNode, GraphEdge>();
        public Dictionary<IGraphNode, GraphEdge> searchFrontier = new Dictionary<IGraphNode, GraphEdge>();
        public List<IGraphNode> shortestPath = new List<IGraphNode>();

        GraphNodePriorityQueue pq = new GraphNodePriorityQueue();

        public bool Search(Graph graph, IGraphNode source, IGraphNode target, IHeuristicCalculator hc)
        {
            pq.Add(source);

            while (pq.count > 0)
            {
                IGraphNode nextClosestNode = pq.PopFirst();
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
                    IGraphNode neighbour = frontierEdge.to;

                    float realCost = nextClosestNode.realNodeCost + frontierEdge.cost;
                    float heuristicCost = hc.Calculate(neighbour, target);
                    float totalCost = nextClosestNode.totalNodeCost + frontierEdge.cost;

                    if (searchFrontier.ContainsKey(neighbour) == false)
                    {
                        neighbour.totalNodeCost = totalCost;
                        neighbour.realNodeCost = realCost;
                        searchFrontier.Add(neighbour, frontierEdge);
                        pq.Add(neighbour);
                    }
                    else if (realCost < neighbour.realNodeCost)
                    {
                        neighbour.totalNodeCost = totalCost;
                        neighbour.realNodeCost = realCost;
                        pq.MarkToSort();
                        searchFrontier[neighbour] = frontierEdge;
                    }
                }
            }
            return false;
        }

        private void ConstructPath(IGraphNode source, IGraphNode target)
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
