namespace KokoEngine
{
    public interface IHeuristicCalculator
    {
        float Calculate(IPathfindingNode node, IPathfindingNode target);
    }
}
