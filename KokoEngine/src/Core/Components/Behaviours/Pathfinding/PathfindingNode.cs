using System.Collections.Generic;

namespace KokoEngine
{
    public class PathfindingNode : Behaviour, IPathfindingNode
    {
        public bool IsDirected { get; set; }
        public float TotalNodeCost { get; set; }
        public float RealNodeCost { get; set; }
        public List<IPathfindingNode> GraphNodes => _graph.nodes;

        private Graph _graph;

        protected override void Awake()
        {
            _graph = new Graph(true);
        }
        
        public void AddNodeToGraph(IPathfindingNode node)
        {
            _graph.AddNode(node);
        }

        public List<IPathfindingNode> FindPath(IPathfindingNode source, IPathfindingNode target, IHeuristicCalculator h)
        {
            var shortestPath = new List<IPathfindingNode>();
            AStarGraphSearch graphSearch = new AStarGraphSearch();
            bool success = graphSearch.Search(_graph, source, target, h);

            if (success)
            {
                shortestPath.Clear();
                shortestPath.Add(source);
                shortestPath.AddRange(graphSearch.shortestPath);
                shortestPath.Add(target);
            }

            return shortestPath;
        }
    }
}
