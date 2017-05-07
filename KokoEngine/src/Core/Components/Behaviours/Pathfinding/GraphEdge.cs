namespace KokoEngine
{
    internal class GraphEdge
    {
        public IPathfindingNode from, to;
        public float cost;

        public GraphEdge(IPathfindingNode from, IPathfindingNode to, float cost)
        {
            this.to = to;
            this.from = from;
            this.cost = cost;
        }
    }
}
