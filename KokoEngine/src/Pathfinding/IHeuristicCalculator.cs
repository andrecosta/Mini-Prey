namespace KokoEngine
{
    public interface IHeuristicCalculator
    {
        float Calculate(IGraphNode node, IGraphNode target);
    }
}
