using System.Collections.Generic;

namespace KokoEngine
{
    public class Graph
    {
        public List<IGraphNode> nodes = new List<IGraphNode>();
        public List<GraphEdge> edges = new List<GraphEdge>();
        public Dictionary<IGraphNode, List<GraphEdge>> nodeFrontiers = new Dictionary<IGraphNode, List<GraphEdge>>();

        private bool directed;

        public Graph(bool directed)
        {
            this.directed = directed;
        }

        public bool AddNode(IGraphNode n)
        {
            if (nodes.Contains(n))
            {
                return false;
            }
            nodes.Add(n);
            nodeFrontiers.Add(n, new List<GraphEdge>());
            return true;
        }

        public IGraphNode GetNode(int index)
        {
            return nodes[index];
        }

        public bool RemoveNode(IGraphNode n)
        {
            if (nodes.Remove(n))
            {
                nodeFrontiers.Remove(n);
                return true;
            }
            return false;
        }

        public bool AddEdge(GraphEdge e)
        {
            if (edges.Contains(e))
                return false;

            edges.Add(e);
            AddToFrontier(e);

            if (directed == false)
            {
                GraphEdge inverted = new GraphEdge(e.to, e.from, e.cost);
                edges.Add(inverted);
                AddToFrontier(inverted);
            }

            return true;
        }

        public bool RemoveEdge(GraphEdge e)
        {
            if (edges.Remove(e))
            {
                List<GraphEdge> frontier = nodeFrontiers[e.from];
                frontier.Remove(e);
                if (directed == false)
                {
                    GraphEdge inverted = FindEdge(e.to, e.from);
                    if (inverted != null)
                    {
                        edges.Remove(inverted);
                        frontier = nodeFrontiers[inverted.from];
                        frontier.Remove(inverted);
                    }
                }
                return true;
            }
            return false;
        }

        private GraphEdge FindEdge(IGraphNode from, IGraphNode to)
        {
            for (int i = 0; i < edges.Count; i++)
            {
                GraphEdge e = edges[i];
                if (e.from == from && e.to == to)
                {
                    return e;
                }
            }
            return null;
        }

        public List<GraphEdge> GetFrontier(IGraphNode n)
        {
            return nodeFrontiers[n];
        }

        private void AddToFrontier(GraphEdge e)
        {
            if (nodeFrontiers.ContainsKey(e.from))
            {
                AddNode(e.from);
            }
            nodeFrontiers[e.from].Add(e);
        }
    }
}
