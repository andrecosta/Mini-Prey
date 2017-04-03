namespace KokoEngine
{
    public class GraphEdge
    {
        public IGraphNode from, to;
        public float cost;

        public GraphEdge(IGraphNode from, IGraphNode to, float cost)
        {
            this.to = to;
            this.from = from;
            this.cost = cost;
        }
    }
}
