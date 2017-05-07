namespace KokoEngine
{
    public interface IPathfindingNode : IBehaviour
    {
        float TotalNodeCost { get; set; }
        float RealNodeCost { get; set; }
    }
}
